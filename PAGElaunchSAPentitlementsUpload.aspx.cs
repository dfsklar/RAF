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

using RBSR_AUFW.DB.ITcodeAssignmentSet;



namespace _6MAR_WebApplication
{
  public partial class WebForm112 : AFWACpage
  {



    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      PANELcond_AbortUpload.Visible = false;
      PANELcond_AllowUpload.Visible = false;

      System.Data.Odbc.OdbcConnection conn = HELPERS.NewOdbcConn();



      try
      {
          session.ObtainWorkspaceContext_SAP();
      }
      catch (Exception ex)
      {
          if (ex.Message.Contains("more than one workspace"))
          {
              Response.Redirect("Page_SAP_History.aspx");
              return;
          }
      }



      
      if ((session.idWorkspace_SAP >= 0) && session.isWorkspaceOwner_SAP)
        {
          PANELcond_AllowUpload.Visible = true;
          PANELcond_AbortUpload.Visible = false;
        }
      else
        {
          PANELcond_AbortUpload.Visible = true;
          PANELcond_AllowUpload.Visible = false;
        }
    }
  




      /* 
       * uploadtype =
       *    "TCODE-ENTS"
       *    "ORGVALS1252"
       *    "AUTHVALS1251"
       */
    protected void REACTlaunchUpload(object sender, EventArgs e, FileUpload uploadengine, string uploadtype)
    {

      // For safety, do yet another verification that this user owns the
      // workspace.
      session.ObtainWorkspaceContext_SAP();

      if (!((session.idWorkspace_SAP >= 0) && session.isWorkspaceOwner_SAP))
        {
          throw new Exception("There is no active workspace, owned by you, for this subprocess.");
        }



        if (uploadengine.HasFile)
        {
          string pathTempFolder = System.IO.Path.GetTempPath();
          string pathTempFile = System.IO.Path.GetTempFileName();

	  /*
          if (this.COMBOXchooseSAProle.SelectedIndex < 0)
            {
              throw new Exception("Select an SAP role from the dropdown list!");
            }
          int idSAProle = int.Parse(this.COMBOXchooseSAProle.SelectedItem.Value);
	  */

          uploadengine.SaveAs(pathTempFile);
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


                  int IDneweaset = session.idWorkspace_SAP;


                  int maxrowcountperserverroundtrip = 2500;

                  switch (uploadtype)
                  {
                      case "TCODE-ENTS":
                          // As of Aug 2010, this is being done via a separate ASHX that knows how to break long operations up into segments.
                              Response.Redirect("utilities/UploadSAPEntitlementsViaChain.ashx?" +
                                  "action=initiate&origfilename=" + HttpUtility.UrlEncode(uploadengine.FileName) +
                                  "&startat=0&count=" + maxrowcountperserverroundtrip.ToString() + "&csvfolder=" + HttpUtility.UrlEncode(pathTempFolder)
                                  + "&handlenonregtc=" + this.RADIOhowHandleNonregTcodes.SelectedValue 
                                  + "&csvfilename=" +
                                  HttpUtility.UrlEncode(System.IO.Path.GetFileName(pathTempFile)));
                              return;
                          break;

                      case "ORGVALS1252":
                          SAP_HELPERS.ImportSAPOrgVectorsFromCSV
                            (dt, session.idUser, session.idSubprocess, conn, RETmsgs,

                             "UPLOAD FROM CSV " +
                             uploadengine.FileName + " performed at: " +
                             DateTime.Now, session.idWorkspace_SAP);
                          break;

                      case "AUTHVALS1251":
                          break;
                  }
                      

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
                      Response.Redirect("PAGE_SAP_History.aspx");
                    }
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        REACTlaunchUpload(sender, e, this.FileUpload2, "TCODE-ENTS");
    }


    protected void Button1A_Click(object sender, EventArgs e)
    {
      REACTlaunchUpload(sender, e, this.FileUpload2A, "ORGVALS1252");
    }

  }
}
