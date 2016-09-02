using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using RBSR_AUFW.DB.IBusRole;
using RBSR_AUFW.DB.IApplication;
using ComponentArt.Web.UI;
using RBSR_AUFW.DB.IEntAssignment;


namespace _6MAR_WebApplication
{
  public partial class WebForm114 : AFWACpage
  {


    private Dictionary<int,int> MAPentIdToBabyEAssId;




    override protected void OnInit(EventArgs e)
    {
      //
      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      //
      InitializeComponent();
      base.OnInit(e);
    }



    public void COMBOXchooseApp_SelectedIndexChanged(Object sender, EventArgs e)
    {
    }

    private void InitializeComponent()
    {
      this.Load += new System.EventHandler(this.Page_Load);

      Grid1.ItemCheckChanged += 
        new ComponentArt.Web.UI.Grid.ItemCheckChangedEventHandler(this.OnItemCheckChanged);
      /*
        Grid1.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(OnNeedRebind);
        Grid1.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(OnNeedDataSource);
        Grid1.PageIndexChanged += new ComponentArt.Web.UI.Grid.PageIndexChangedEventHandler(OnPageChanged);
        Grid1.SortCommand += new ComponentArt.Web.UI.Grid.SortCommandEventHandler(OnSort);
      */
        
      Grid1.UpdateCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_UpdateCommand);
      /*  Grid1.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_DeleteCommand);
          Grid1.InsertCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_InsertCommand);
          Grid1.SelectCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_SelectCommand);
      */
      Grid1.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(this.OnNeedRebind);
    }


    public void OnItemCheckChanged(object sender, GridItemCheckChangedEventArgs oArgs)
    {
      IEntAssignment ENGINEentass = new IEntAssignment(HELPERS.NewOdbcConn());

      int IDentitlement = int.Parse(oArgs.Item["c_id"].ToString());

      string editstatusEAss = "-";
      int idEntAss = -1;
      if (oArgs.Item["TEassEditStatus"] != null)
        {
          if (oArgs.Item["TEassEditStatus"].ToString() != "")
            {
              editstatusEAss = oArgs.Item["TEassEditStatus"].ToString();
              idEntAss = int.Parse(oArgs.Item["TEassRowId"].ToString());
            }
        }


      if (oArgs.Checked)
        {
          if (idEntAss >= 0)
            {
              returnGetEntAssignment curval = ENGINEentass.GetEntAssignment(idEntAss);
              ENGINEentass.SetEntAssignment(idEntAss, curval.EntAssignmentSetID, curval.BusRoleID, curval.EntitlementID,
                                            "A");/*ACTIVE*/
            }
          else
            {
              // Create a EAss
              int baby = ENGINEentass.NewEntAssignment(session.idWorkspace, IDrole, IDentitlement, "N");
              MAPentIdToBabyEAssId.Add(IDentitlement, baby);
            }
        }
      else
        {
          if (idEntAss < 0)
            {
              if (MAPentIdToBabyEAssId.ContainsKey(IDentitlement))
                {
                  idEntAss = MAPentIdToBabyEAssId[IDentitlement];
                  editstatusEAss = "N";
                  MAPentIdToBabyEAssId.Remove(IDentitlement);
                }
            }
          if (editstatusEAss == "N")
            {
              try
                {
                  ENGINEentass.DeleteEntAssignment(idEntAss);
                }
              catch (Exception eee)
                {
                  // Exceptions will occur here naturally, if during a session where row R was
                  // initially unchecked, someone checked it, and then unchecked it back to its
                  // original unchecked state, and then hits submit.
                  ENGINEentass.DeleteEntAssignment(
                                                   MAPentIdToBabyEAssId[IDentitlement]);
                  MAPentIdToBabyEAssId.Remove(IDentitlement);
                }
            }
          else
            {
              returnGetEntAssignment curval =  ENGINEentass.GetEntAssignment(idEntAss);
              ENGINEentass.SetEntAssignment(idEntAss, curval.EntAssignmentSetID, curval.BusRoleID, curval.EntitlementID,
                                            "X");
            }
        }
          
    }

    public void Grid1_UpdateCommand(object sender, EventArgs oArgs)
    {
      int x = 3;
    }


    public void OnNeedRebind(object sender, EventArgs oArgs)
    {
     Grid1.DataBind();
    }


    public int IDrole;
    public int IDappl;

    public returnGetBusRole brole;
    public returnGetApplication applDetails;




    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);


      MAPentIdToBabyEAssId = new Dictionary<int, int>();

      try
        {
          IDrole = int.Parse(this.Request.Params["RoleID"]);
        }
      catch (Exception ex)
        {
          // Whilst debugging
          IDrole = 1;
        }

      IBusRole IFACEbrole = new IBusRole(HELPERS.NewOdbcConn());
      brole = IFACEbrole.GetBusRole(IDrole);
      Session["STRcurBusRoleScope"] = IDrole;


      
      // NO LONGER USED:
      //   Session["STRcurWS"] = this.session.idWorkspace;


      int IDeaset = -1;
      try {
        IDeaset = int.Parse(Session["INTcurWS"].ToString());
      }catch(Exception){}



      bool isreadonly = true;

      if (IDeaset == this.session.idWorkspace)
        {
          isreadonly = ( ! this.session.isWorkspaceOwner);
        }

      if (isreadonly)
        {
          // READ-ONLY - not in a workspace
          this.Grid1.AllowEditing = false;
          this.PANELcond_readonly.Visible = true;
        }
      else {
        this.PANELcond_readonly.Visible = false;
      }
     



      try
        {
          IDappl = int.Parse(this.Request.Params["AppID"]);
          session.idAppl = IDappl;
        }
      catch (Exception ex)
        {
          // Whilst debugging
          IDappl = session.idAppl;
        }
      IApplication IFACEappl = new IApplication(HELPERS.NewOdbcConn());
      applDetails = IFACEappl.GetApplication(IDappl);
      this.Session["STRcurAppScope"] = applDetails.Name;


    }


  }
}
