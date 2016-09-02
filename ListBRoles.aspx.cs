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

using RBSR_AUFW.DB.IBusRole;
using System.Text.RegularExpressions;
using RBSR_AUFW.DB.IEntAssignmentSet;
using RBSR_AUFW.DB.IEntAssignment;



namespace _6MAR_WebApplication
{
  public partial class WebForm16 : AFWACpage
  {


    returnGetEntAssignmentSet targetEASet;





    private void OnCallback_additionalRoles
      (object sender,
       ComponentArt.Web.UI.CallBackEventArgs e)
    {
      int idBusRole = int.Parse(e.Parameter);
      Session["intFILTERBROLE"] = idBusRole;

      this.SQL_additionalRoles.DataBind();

      GRIDadditionalRoles.DataBind();
    }






      // This call ensures that the six rows of chg-mgmt already exist
      // because the human is about to bring up that popup modal dlog!
    private void OnCallback_initChangeMgmt
      (object sender,
       ComponentArt.Web.UI.CallBackEventArgs e)
    {
        HELPERS.InitChangeMgmtForWS(session.idWorkspace);
    }




    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      session.ObtainWorkspaceContext();


      if (IsPostBack)
      {
       
          return;
      }



      // UH-OH, THESE ARE DONE MULT TIMES!?
      Grid1.UpdateCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_UpdateCommand);
      Grid1.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_DeleteCommand);
      Grid1.InsertCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_InsertCommand);
      Grid1.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(this.OnNeedRebind);

      /*
      GRIDadditionalRoles.NeedRebind +=
        new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(this.OnNeedRebind_GRIDadditionalRoles);
      */

      CALLBACKadditionalRoles.Callback +=
        new ComponentArt.Web.UI.CallBack.CallbackEventHandler(this.OnCallback_additionalRoles);
      CALLBACKinitChangeMgmt.Callback +=
        new ComponentArt.Web.UI.CallBack.CallbackEventHandler(this.OnCallback_initChangeMgmt);



      PANELcond_Editable.Visible = false;
      PANELcond_ReadOnly.Visible = false;
      PANELcond_ViewOtherUserWorkspace.Visible = false;

      PANELcond_ViewActiveReadOnly.Visible = false;
      PANELcond_ViewHistoricalReadOnly.Visible = false;

      IEntAssignmentSet engineEASET = new IEntAssignmentSet(HELPERS.NewOdbcConn());

        
      int IDeaset = -1;
        

      // STICKINESS: If the URL does not specifically demand a particular
      // workspace via query param, then draw it in from the session parameter.
      // Only if no session preference for a particular workspace would you then
      // default to the current open-for-editing workspace.
      if (this.Request.Params["WSID"] == null)
        {
          // URL does not explicitly request a particular EASet
          if (Session["INTcurWS"].ToString() != "")
            {
              IDeaset = int.Parse(Session["INTcurWS"].ToString());
            }
        } else {
        IDeaset = int.Parse(this.Request.Params["WSID"].ToString());
      }

      if (IDeaset < 0)
        {
          // LAST RESORT:
          IDeaset = session.idWorkspace;
        }





      if (IDeaset != session.idWorkspace) {
          
        // IMPORTANT: we are going back in time and looking at a frozen state.
        // This is NOT a workspace!
        
        Session["INTcurWS"] = IDeaset;


        // Get info about this snapshot
        targetEASet = engineEASET.GetEntAssignmentSet(IDeaset);

  
        if (targetEASet.Status.ToLower() == "active")
          {
            PANELcond_ViewActiveReadOnly.Visible = true;
          }else{
          PANELcond_ViewHistoricalReadOnly.Visible = true;
        }

      }
      else 
        {

          Session["INTcurWS"] = IDeaset;
      
          if (IDeaset >= 0)
            {
              // THIS IS A WORKSPACE !!
              // THIS IS A WORKSPACE !!
              // THIS IS A WORKSPACE !!
              // It might not belong to "me" but it is definitely a workspace
              targetEASet = engineEASET.GetEntAssignmentSet(session.idWorkspace);
              if (session.isWorkspaceOwner) {
                PANELcond_Editable.Visible = true;
              }
              else{
                PANELcond_ViewOtherUserWorkspace.Visible = true;
              }
            }
          else
            {
              // There IS no workspace!
              PANELcond_ReadOnly.Visible = true;
              Grid1.Visible = false;
            }
        }
    }





    public void OnNeedRebind_GRIDadditionalRoles(object sender, EventArgs oArgs)
    {
      // Without this, nothing works:
      GRIDadditionalRoles.DataBind();
    }






    private void Grid1_InsertCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
      UpdateDb(e.Item, "INSERT");
    }

    private void Grid1_UpdateCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
      UpdateDb(e.Item, "UPDATE");
    }

    private void Grid1_DeleteCommand(object sender, ComponentArt.Web.UI.GridItemEventArgs e)
    {
      UpdateDb(e.Item, "DELETE");
    }


    public void OnNeedRebind(object sender, EventArgs oArgs)
    {
      Grid1.DataBind();
    }











    private void UpdateDb(ComponentArt.Web.UI.GridItem item, string command)
    {
      IBusRole engine = new IBusRole(HELPERS.NewOdbcConn());
      IEntAssignment engineEA = new IEntAssignment(HELPERS.NewOdbcConn());

       

      switch (command)
        {
        case "INSERT":
          ValidateAbbrev(item["c_u_Abbrev"] as string, -1);
          try
          {
              item["c_id"] =
                engine.NewBusRole(
                                  item["c_u_Name"] as string,
                                  item["c_u_Description"] as string,
                                  this.session.idSubprocess);
          }
          catch (Exception eee)
          {
              throw new Exception("Addition of new role failed: check for name already in use.");
          }

          engine.SetBusRole
            (
             (int)(item["c_id"]),
             item["c_u_Name"] as string,
             item["c_u_Description"] as string,
             this.session.idSubprocess,
             item["c_u_Abbrev"] as string, null, null, null);
          break;                    

        case "UPDATE":
          ValidateAbbrev(item["c_u_Abbrev"] as string,
                         int.Parse(item["c_id"] as string));
          engine.SetBusRole
            (int.Parse(item["c_id"] as string),
             item["c_u_Name"] as string,
             item["c_u_Description"] as string,
             this.session.idSubprocess,
             item["c_u_Abbrev"] as string);
          break;

        case "DELETE":
          int IDbusrole = int.Parse(item["c_id"] as string);


          // Deleting a role is forbidden only if *this* workspace
          // still has non-"X" entitlement assignments to it.
          //
          // 1. Count the number of non-X entass in this workspace.
          returnListEntAssignmentByBusRole[] ret = engineEA.ListEntAssignmentByBusRole
            (null, " (\"status\" <> ?) AND (\"entassignmentset\" = ?) ", new string[]{"X",this.session.idWorkspace.ToString()}, "", IDbusrole);
          int refsStillPresent = ret.Length;


          // 2. Check to see if there are any more references to this bus role
          if (refsStillPresent > 0) {
            throw new Exception
              ("This business role cannot be deleted at this time, because there is/are still " + refsStillPresent + " reference(s) to this business role, in other workspaces or historical snapshots.");
          }

          // 3. Delete the business role, but hiding it away in the special
          // "trashcan" subprocess.
          engine.MoveBusRoleToTrashcan(IDbusrole,
              int.Parse(ConfigurationManager.AppSettings["IDsubprocessTrashcan"]));

          break;
        }
    }



    // No return value; this is designed to throw an exception if there is a problem.
    // Limit the serach to: this.session.idSubprocess
    private void ValidateAbbrev(string abbrev, int currID)
    {
      HELPERS.ValidateAbbrev(abbrev, currID, this.session.idSubprocess);
    }


  }
}
