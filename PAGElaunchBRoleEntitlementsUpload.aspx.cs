using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using RBSR_AUFW.DB.IBusRoleOwner;
using RBSR_AUFW.DB.IBusRole;
using RBSR_AUFW.DB.IEntitlement;
using RBSR_AUFW.DB.IEntAssignment;

namespace _6MAR_WebApplication
{
  public partial class WebForm121 : AFWACpage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      PANELcond_AbortUpload.Visible = false;
      PANELcond_AllowUpload.Visible = false;

      System.Data.Odbc.OdbcConnection conn = HELPERS.NewOdbcConn();

      session.ObtainWorkspaceContext();

      if ((session.idWorkspace >= 0) && session.isWorkspaceOwner)
        {
          PANELcond_AllowUpload.Visible = true;
          PANELcond_AbortUpload.Visible = false;
        }
      else
        {
          PANELcond_AbortUpload.Visible = true;
          PANELcond_AllowUpload.Visible = false;
        }

      // This next line exposes a bug in WEB.UI:
      // If we do this, the selected index is LOCKED to 0.
      // If user changes the selection and then submits,
      // the selindex is still 0 from within the C# code.
      // This has to be conditionalized: not in a postback?
      if ( ! IsPostBack) {
        COMBOXchooseUnregEntAction.SelectedIndex = 0;
      }
    }














    protected void
    ONCLICK_BulkUploadNewBusRoles(object sender, EventArgs e)
    {
      IBusRoleOwner ENGINEbrown = new IBusRoleOwner(HELPERS.NewOdbcConn());
      IBusRole ENGINEbr = new IBusRole(HELPERS.NewOdbcConn());


      if (this.FileUpload_AddRoles.HasFile)
        {
          string pathTempFolder = System.IO.Path.GetTempPath();
          string pathTempFile = System.IO.Path.GetTempFileName();
          FileUpload_AddRoles.SaveAs(pathTempFile);
          DataTable dt = HELPERS.LoadCsv(pathTempFolder,
                                         System.IO.Path.GetFileName(pathTempFile));
          if (dt != null)
            {
              if (dt.Columns.Count < 2)
                {
                  throw new Exception("The uploaded CSV file must have at least the name and description columns.  (It can optionally also have the two approver/owner columns.)");
                }


              Queue RETmsgs = new Queue();

              IEnumerator<System.Data.DataRow> x =
                (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();

              int recordseq = 0;
              int okCount = 0;
              while (x.MoveNext())
                {
                  recordseq++;

                  string rolename = x.Current[0].ToString().Trim();
                  string description = x.Current[1].ToString().Trim();
                  string roletype = x.Current[2].ToString().Trim();
                  string primaryApprover = x.Current[3].ToString().Trim().ToUpper();
                  string primaryOwner = x.Current[4].ToString().Trim().ToUpper();


                  int IDrole = -1;
                  try
                  {
                      IDrole = HELPERS.FindBusRoleByName(rolename);
                  }
                  catch (Exception)
                  {
                      /* The exception is what we WANT!  If this does not throw an exception, this row must be ignored! */
                  }


                  bool doRoleCreation = true;

                  if (IDrole >= 0) {
                    // This role name is already in use somewhere in the system (not nec this subpr).
                    RETmsgs.Enqueue("REC#" + recordseq.ToString() + ": Role already exists, so the only action will be updating of owner/approver info.  Role name: " + rolename);
                    doRoleCreation = false;
                  }

                  int newID = (doRoleCreation ? -1 : IDrole);
                  if (doRoleCreation)
                  {
                      switch (roletype)
                      {
                          case "A":
                              break;
                          case "F":
                              break;
                          case "E":
                              break;
                          default:
                              RETmsgs.Enqueue("REC#" + recordseq.ToString() + ": line ignored due to unknown role-type code: " + roletype);
                              continue;
                      }

                      try
                      {
                          newID = ENGINEbr.NewBusRole(rolename, description, session.idSubprocess);
                          ENGINEbr.SetBusRole(newID, roletype);
                          okCount++;
                      }
                      catch (Exception ee)
                      {
                          RETmsgs.Enqueue("REC#" + recordseq.ToString() + ": " + ee.Message);
                      }
                  }

                  if (newID >= 0)
                  {
                      // New code added Thanksgiving 2012: supporting approver/owner
                      if (primaryOwner.Length > 2)
                      {
                          string __eid = primaryOwner;
                          string __rank = "OWNprim";
                          string __rankPretty = "Primary Owner";
                          try
                          {

                              HELPERS.FindUser(HELPERS.NewOdbcConn(), __eid, __eid, false);
                              returnListBusRoleOwnerByBusRole[] theList = ENGINEbrown.ListBusRoleOwnerByBusRole(null, newID);
                              if (theList.Length < 1)
                                  ENGINEbrown.NewBusRoleOwner(__eid, "", __rank, newID);
                              else
                              {
                                  foreach (returnListBusRoleOwnerByBusRole roleowner in theList)
                                  {
                                      if (roleowner.Rank == __rank)
                                         ENGINEbrown.DeleteBusRoleOwner(roleowner.ID);
                                  }
                              }
                          }
                          catch (Exception ee)
                          {
                              RETmsgs.Enqueue("REC#" + recordseq.ToString() + ": ignoring setting of " + __rankPretty + " due to unknown employee ID: " + __eid);
                          }                          
                      }


                      if (primaryApprover.Length > 2)
                      {
                          string __eid = primaryApprover;
                          string __rank = "appr";
                          string __rankPretty = "Primary Approver";
                          try
                          {

                              HELPERS.FindUser(HELPERS.NewOdbcConn(), __eid, __eid, false);
                              returnListBusRoleOwnerByBusRole[] theList = ENGINEbrown.ListBusRoleOwnerByBusRole(null, newID);
                              if (theList.Length < 1)
                                  ENGINEbrown.NewBusRoleOwner(__eid, "", __rank, newID);
                              else
                              {
                                  foreach (returnListBusRoleOwnerByBusRole roleowner in theList)
                                  {
                                      if (roleowner.Rank == __rank)
                                         ENGINEbrown.DeleteBusRoleOwner(roleowner.ID);
                                  }
                              }
                          }
                          catch (Exception ee)
                          {
                              RETmsgs.Enqueue("REC#" + recordseq.ToString() + ": ignoring setting of " + __rankPretty + " due to unknown employee ID: " + __eid);
                          }                          

                      }
                  }
                }

              // -----------------------------------------------

              RETmsgs.Enqueue("------------------");
              RETmsgs.Enqueue("Number of NEW business roles created successfully: " + okCount.ToString());
              if (RETmsgs.Count > 0)
                {
                  string strMsgs = "";
                  foreach (object objMsg in RETmsgs.ToArray())
                    {
                      strMsgs += "\n" + objMsg.ToString();
                    }
                  TXTimportEngineMessages.Text = strMsgs;
                  DIVimportFeeback.Visible = true;
                  PANELcond_AbortUpload.Visible = false;
                  PANELcond_AllowUpload.Visible = false;
                }

            }
        }
    }



















    protected void
    ONCLICK_BulkUploadPersonnel(object sender, EventArgs e)
    {
      Dictionary<string, bool> DOESEXISTbyEid = new Dictionary<string,bool>();
      Dictionary<string, int> DICTbusrole = new Dictionary<string,int>();

      IBusRoleOwner ENGINEbrown = new IBusRoleOwner(HELPERS.NewOdbcConn());
      IBusRole ENGINEbr = new IBusRole(HELPERS.NewOdbcConn());


      if (this.FileUpload_PersonnelMappings.HasFile)
        {
          string pathTempFolder = System.IO.Path.GetTempPath();
          string pathTempFile = System.IO.Path.GetTempFileName();
          FileUpload_PersonnelMappings.SaveAs(pathTempFile);
          DataTable dt = HELPERS.LoadCsv(pathTempFolder,
                                         System.IO.Path.GetFileName(pathTempFile));
          if (dt != null)
            {
              if (dt.Columns.Count < 3)
                {
                  throw new Exception("The uploaded CSV file does not have at least 3 columns.");
                }


              Queue RETmsgs = new Queue();

              IEnumerator<System.Data.DataRow> x =
                (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();

              int recordseq = 0;
              int okCount = 0;
              while (x.MoveNext())
                {
                  recordseq++;

                  string rolename = x.Current[0].ToString().Trim();
                  string rank = x.Current[1].ToString().Trim();
                  string eid = x.Current[2].ToString().Trim().ToUpper();


                  switch (rank)
                    {
                    case "Primary Owner":
                      rank = "OWNprim";
                      break;
                    case "Delegate Owner":
                      rank = "OWNdele";
                      break;
                    case "Primary Approver":
                      rank = "appr";
                      break;
                    case "Delegate Approver":
                      rank = "delegate";
                      break;
                    default:
                      RETmsgs.Enqueue("REC#" + recordseq.ToString() + ": line ignored due to unknown rank name: " + rank);
                      continue;
                    }


                  if (recordseq == 1)
                    {
                      // Upon seeing at least one row in the CSV, erase the entire personnel mapping table!
                      HELPERS.DestroyAllBusroleToPersonnelMappings();
                    }

                  if (DOESEXISTbyEid.ContainsKey(eid) == false)
                    {
                      HELPERS.FindUser(HELPERS.NewOdbcConn(), eid, eid, true);
                      DOESEXISTbyEid.Add(eid, true);
                    }


                  int IDrole = -1;
                  if (DICTbusrole.ContainsKey(rolename))
                    IDrole = DICTbusrole[rolename];
                  else
                    {
                      try
                        {
                          IDrole = HELPERS.FindBusRoleByName(rolename);
                          DICTbusrole.Add(rolename, IDrole);
                        }
                      catch (Exception)
                        {
                        }
                    }


                  if (IDrole < 0)
                    {
                      RETmsgs.Enqueue("REC#" + recordseq.ToString() + ": line ignored due to unknown role: " + rolename);
                      continue;
                    }


                  try
                    {
                      ENGINEbrown.NewBusRoleOwner(eid, "", rank, IDrole);
                      okCount++;
                    }
                  catch (Exception ee)
                    {
                      RETmsgs.Enqueue("REC#" + recordseq.ToString() + ": " + ee.Message);
                    }
                }

              RETmsgs.Enqueue("------------------");
              RETmsgs.Enqueue("Number of records processed successfully: " + okCount.ToString());
              if (RETmsgs.Count > 0)
                {
                  string strMsgs = "";
                  foreach (object objMsg in RETmsgs.ToArray())
                    {
                      strMsgs += "\n" + objMsg.ToString();
                    }
                  TXTimportEngineMessages.Text = strMsgs;
                  DIVimportFeeback.Visible = true;
                  PANELcond_AbortUpload.Visible = false;
                  PANELcond_AllowUpload.Visible = false;
                }

            }
        }
    }








    protected void ONCLICK_BulkRemove(object sender, EventArgs e)
    {
      if (this.FileUpload_BulkRemove.HasFile)
        {
          string pathTempFolder = System.IO.Path.GetTempPath();
          string pathTempFile = System.IO.Path.GetTempFileName();
          FileUpload_BulkRemove.SaveAs(pathTempFile);
          DataTable dt = HELPERS.LoadCsv(pathTempFolder,
                                         System.IO.Path.GetFileName(pathTempFile));
          if (dt != null)
            {
              if (dt.Columns.Count != 2)
                {
                  throw new Exception("The uploaded CSV file has more than two columns.");
                }


              Queue RETmsgs = new Queue();

              IEnumerator<System.Data.DataRow> x =
                (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();

              int recordseq = 0;
              int okCount = 0;

              IBusRole ENGINEbusrole = new IBusRole(HELPERS.NewOdbcConn());
              IEntitlement ENGINE = new IEntitlement(HELPERS.NewOdbcConn());
              IEntAssignment IEA = new IEntAssignment(HELPERS.NewOdbcConn());


              while (x.MoveNext())
                {
                  recordseq++;

                  string rolename = x.Current[0].ToString();
                  string privstring = x.Current[1].ToString();

                  try
                    {
                      HELPERS.RemoveEntitlementFromRole(rolename, privstring, ENGINEbusrole, ENGINE, IEA);
                      okCount++;
                    }
                  catch (Exception ee)
                    {
                      RETmsgs.Enqueue ("REC#" + recordseq.ToString() + ": " + ee.Message);
                    }
                }
              RETmsgs.Enqueue("------------------");
              RETmsgs.Enqueue("Number of records processed without message: " + okCount.ToString());
              if (RETmsgs.Count > 0)
                {
                  string strMsgs = "";
                  foreach (object objMsg in RETmsgs.ToArray())
                    {
                      strMsgs += "\n" + objMsg.ToString();
                    }
                  TXTimportEngineMessages.Text = strMsgs;
                  DIVimportFeeback.Visible = true;
                  PANELcond_AbortUpload.Visible = false;
                  PANELcond_AllowUpload.Visible = false;
                }

            }
        }
    }







    protected void Button1_Click(object sender, EventArgs e)
    {
      REACTlaunchUpload(sender, e);
    }




    protected void REACTlaunchUpload(object sender, EventArgs e)
    {

      // For safety, do yet another verification that this user owns the
      // workspace.
      session.ObtainWorkspaceContext();

      if (!((session.idWorkspace >= 0) && session.isWorkspaceOwner))
        {
          throw new Exception("There is no active workspace, owned by you, for this subprocess.");
        }

        
        
      if (this.FileUpload2.HasFile)
        {
          string pathTempFolder = System.IO.Path.GetTempPath();
          string pathTempFile = System.IO.Path.GetTempFileName();



          /*
            if (this.COMBOXchooseBrole.SelectedIndex < 0)
            {
            throw new Exception("Select a role from the dropdown list!");
            }
          */



          /*
           * TURNS OUT THAT idBusRole was never being used after being set.
           * 
           int idBusRole = -1;
           if (this.COMBOXchooseBrole.SelectedItem != null)
           {
           idBusRole = int.Parse(this.COMBOXchooseBrole.SelectedItem.Value);
           }
          */


          FileUpload2.SaveAs(pathTempFile);
          DataTable dt = HELPERS.LoadCsv(pathTempFolder,
                                         System.IO.Path.GetFileName(pathTempFile));
          if (dt != null)
            {
              if (dt.Columns.Count > 1)
                {
                  System.Data.Odbc.OdbcConnection conn =

                    new System.Data.Odbc.OdbcConnection(
                                                        ConfigurationManager.AppSettings["DBconnstr"]);

                  conn.Open();

                  Queue RETmsgs = new Queue();


                  HELPERS.ImportBusRoleAssignmentsFromDataTable
                    (dt, session.idUser, session.idWorkspace,
                     this.COMBOXchooseUnregEntAction.SelectedValue,
                     this.COMBOXchooseBrole.SelectedValue, RETmsgs,
                     session.idSubprocess);
                      

                  if (RETmsgs.Count > 0)
                    {
                      string strMsgs = "";
                      foreach (object objMsg in RETmsgs.ToArray())
                        {
                          strMsgs += "\n" + objMsg.ToString();
                        }
                      TXTimportEngineMessages.Text = strMsgs;
                      DIVimportFeeback.Visible = true;
                      PANELcond_AbortUpload.Visible = false;
                      PANELcond_AllowUpload.Visible = false;
                    }
                  else
                    {
                      Response.Redirect("ListBRoles.aspx");
                    }
                }
            }
        }
    }


  }
}
