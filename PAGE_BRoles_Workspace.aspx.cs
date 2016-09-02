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

using RBSR_AUFW.DB.IEntAssignmentSet;


namespace _6MAR_WebApplication
{
  public partial class WebForm118 : AFWACpage
  {

    public int idActiveWS;

    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);


      //return;  // Does the following code cause the JScript errrors?

      PANELcond_InviteCreateWS.Visible = false;
      PANELcond_Locked.Visible = false;
      PANELcond_NewSubpr.Visible = false;
      PANELcond_IsOwnerOfCurrentWS.Visible = false;


      session.ObtainWorkspaceContext();

      if (session.idWorkspace>=0) {

        // A WORKSPACE ALREADY EXISTS FOR THIS SUBPROCESS.
        if (session.isWorkspaceOwner) {
          PANELcond_IsOwnerOfCurrentWS.Visible = true;
        }
	else {
          PANELcond_Locked.Visible = true;
	}

      }else{

        // Invite them to create a new workspace by cloning the
        // most recent locked EASet.

        IEntAssignmentSet engineWS = new IEntAssignmentSet(HELPERS.NewOdbcConn());
             
        returnListEntAssignmentSetBySubProcess[] listEAS =
          engineWS.ListEntAssignmentSetBySubProcess
          (null, "", new string[]{}, "c_u_DATETIMElock DESC", session.idSubprocess);
        if (listEAS.Length == 0)
          {
            PANELcond_NewSubpr.Visible = true;
          }
        else
          {
            PANELcond_InviteCreateWS.Visible = true;
          }
      }

    }





    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView gv = sender as GridView;
      int selectedId = (int)(gv.SelectedDataKey.Value);
      Response.Redirect("EntitlementWorkspace.aspx?ID=" + selectedId);
    }

  }
}
