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

using RBSR_AUFW.DB.ISAProle;
using RBSR_AUFW.DB.ITcodeAssignmentSet;



namespace _6MAR_WebApplication
{
  public partial class ListSAProles : AFWACpage
  {



    int IDsaprole = -1;



    public returnGetTcodeAssignmentSet targetEASet;


    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      Grid1.UpdateCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_UpdateCommand);
      Grid1.DeleteCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_DeleteCommand);
      Grid1.InsertCommand += new ComponentArt.Web.UI.Grid.GridItemEventHandler(this.Grid1_InsertCommand);



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



      Session["UUIDSAPTASSSET"] = session.idWorkspace_SAP;

      CALLBACKselectCurRole.Callback +=
        new ComponentArt.Web.UI.CallBack.CallbackEventHandler(this.OnCallback_SelectCurRole);

      GRIDroleowners.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(this.OnNeedRebind_GRIDroleowners);


      PANELcond_Editable.Visible = false;
      PANELcond_ReadOnly.Visible = false;
      PANELcond_ViewOtherUserWorkspace.Visible = false;
      PANELcond_PublishWorkspace.Visible = false;
      PANELcond_ViewActiveReadOnly.Visible = false;
      PANELcond_ViewHistoricalReadOnly.Visible = false;




      int IDeaset = -1;


      ITcodeAssignmentSet engineEASET = new ITcodeAssignmentSet(HELPERS.NewOdbcConn());


      // STICKINESS: If the URL does not specifically demand a particular
      // workspace via query param, then draw it in from the session parameter.
      // Only if no session preference for a particular workspace would you then
      // default to the current open-for-editing workspace.
      if (this.Request.Params["WSID"] == null)
        {
          // URL does not explicitly request a particular EASet
          if (Session["INTcurWS_SAP"].ToString() != "")
            {
              IDeaset = int.Parse(Session["INTcurWS_SAP"].ToString());
            }
        } else {
        IDeaset = int.Parse(this.Request.Params["WSID"].ToString());
      }

      if (IDeaset < 0)
        {
          // LAST RESORT:
          IDeaset = session.idWorkspace_SAP;
        }





      if (IDeaset != session.idWorkspace_SAP) {
          
        // IMPORTANT: we are going back in time and looking at a frozen state.
        // This is NOT a workspace!
        
        Session["INTcurWS_SAP"] = IDeaset;


        // Get info about this snapshot
        targetEASet = engineEASET.GetTcodeAssignmentSet(IDeaset);

  
        if (targetEASet.Status.ToLower() == "active")
          {
            PANELcond_ViewActiveReadOnly.Visible = true;
          }else{
          PANELcond_ViewHistoricalReadOnly.Visible = true;
        }

      }
      else 
        {

          Session["INTcurWS_SAP"] = IDeaset;
      
          if (IDeaset >= 0)
            {
              // THIS IS A WORKSPACE !!

              // THIS IS A WORKSPACE !!
              // THIS IS A WORKSPACE !!
              // It might not belong to "me" but it is definitely a workspace
              targetEASet = engineEASET.GetTcodeAssignmentSet(session.idWorkspace_SAP);
              if (session.isWorkspaceOwner_SAP) {
                SAP_HELPERS.InitChangeMgmtForWS(session.idWorkspace_SAP);

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




    public void OnNeedRebind_GRIDroleowners(object sender, EventArgs oArgs)
    {
      GRIDroleowners.DataBind();
    }




    protected void INTERCEPTsqldatasource_roleowners_select(object sender, SqlDataSourceSelectingEventArgs e) 
    {
      e.Command.Parameters[0].Value = this.IDsaprole;
    }



    public void OnCallback_SelectCurRole
      (object sender,
       ComponentArt.Web.UI.CallBackEventArgs e)
    {
      this.IDsaprole = int.Parse(e.Parameter);
      this.GRIDroleowners.DataBind();
      this.GRIDroleowners.RenderControl(e.Output);
    }



    public void EVTHNDL_hidfield_curroleid_changed (Object sender, EventArgs e)
    {
      EventArgs ee = e;
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






/* 
 * Although most editing occurs in the ashx handler in the guidededitor folder,
 * a few edit activities are still being done via events reported directly from
 * the grid widget.  This handles all such requests.
 */
    private void UpdateDb(ComponentArt.Web.UI.GridItem item, string command)
    {

      ISAProle engine = new ISAProle(HELPERS.NewOdbcConn());


      switch (command)
        {
        case "INSERT":
          int newid;
          try
            {
              newid =
                engine.NewSAProle(
                                  item["c_u_Name"] as string, 
                                  this.session.idSubprocess,
                                  item["c_u_System"] as string,
                                  item["c_u_Platform"] as string
                                  );
            }
          catch (Exception e)
            {
              throw new Exception("Failure occurred.  That rolename + platform combination is already in use, whether in this or some other subprocess.  Refresh your browser to continue working.");
            }
          engine.SetSAProle
            (
             newid,
             item["c_u_Name"] as string,
             item["c_u_Description"] as string,
             this.session.idSubprocess,
             item["c_u_System"] as string,
             item["c_u_Platform"] as string,
             item["c_u_RoleActivity"] as string,
             item["c_u_RoleType"] as string, ""
             );

          returnGetSAProle newval = engine.GetSAProle(newid);
          SAP_HELPERS.MaintainMatchingBusinessEntitlementForSAPRole(newval, newval);
          break;


        case "UPDATE":
            if ((item["c_id"] as string) == "")
            {
                UpdateDb(item, "INSERT");
                return;
            }
          try
            {
              int IDsaprole = int.Parse(item["c_id"] as string);
              returnGetSAProle prevvals = engine.GetSAProle(IDsaprole);
              engine.SetSAProle
                (IDsaprole,
                 item["c_u_Name"] as string,
                 item["c_u_Description"] as string,
                 this.session.idSubprocess,
                 item["c_u_System"] as string,
                 item["c_u_Platform"] as string,
                 item["c_u_RoleActivity"] as string,
                 item["c_u_RoleType"] as string, ""
                 );
              returnGetSAProle newvals = engine.GetSAProle(IDsaprole);
              SAP_HELPERS.MaintainMatchingBusinessEntitlementForSAPRole
                (prevvals, newvals);
            }
          catch (Exception e)
            {
              if (e.ToString().Contains("duplicate key"))
                {
                  throw new Exception("That SAP role name + platform combination is already registered, either in this subprocess or another subprocess.  Please refresh the webpage to restore the grid contents and try again.");
                }
              else
                {
                  throw e;
                }
            }
          break;



        case "DELETE":
          int idToKill = int.Parse(item["c_id"] as string);
          returnGetSAProle prevvals2 = engine.GetSAProle(idToKill);

          // First, try to delete the matching business entitlement, and alert if that fails due to in-use.
          SAP_HELPERS.DeleteMatchingBusinessEntitlementForSAPRole(prevvals2);
          // If the above line fails, the rest of this method won't be executed anyway.

          try
            {
              engine.DeleteSAProle(idToKill);
            }
          catch (Exception exc323)
            {
              throw new Exception("This SAP role cannot be deleted because it has TCode assignments associated with it (possibly in other workspaces). NOTE: its matching business entitlement HAS been deleted!  Refresh this webpage to restore the tabular data, and then continue working.");
            }
          break;



          default:
          throw new NotImplementedException();
          break;
        }
    }




  }
}
