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

using RBSR_AUFW.DB.IEditingWorkspace;
using RBSR_AUFW.DB.IEntAssignmentSet;



namespace _6MAR_WebApplication
{
  public partial class WebForm14 : AFWACpage
  {

	 public int idActiveWS;

	 protected void Page_Load(object sender, EventArgs e)
	 {
		base.Page_Load(sender, e);

		PANELcond_InviteCreateWS.Visible = false;
		PANELcond_Locked.Visible = false;
		PANELcond_NewSubpr.Visible = false;
		PANELcond_IsOwnerOfCurrentWS.Visible = false;

		System.Data.Odbc.OdbcConnection conn = HELPERS.NewOdbcConn();
		// Is there a currently active ed workspace for this subprocess?\
		IEditingWorkspace engineWS = new IEditingWorkspace(conn);
            
		returnListEditingWorkspaceBySubProcess[] listWS = 
		  engineWS.ListEditingWorkspaceBySubProcess(null, session.idSubprocess);
		if (listWS.Length > 1)
		  {
			 throw new Exception("Internal error: more than one workspace for subprocess "
										+ session.idSubprocess);
		  }
		if (listWS.Length == 1)
		  {
			 // A workspace already exists for this subprocess.
			 // If the WS owner matches this user, invite them to visit the WS.
			 if (session.idUser == listWS[0].UserID)
				{
				  this.idActiveWS = listWS[0].ID;
				  PANELcond_IsOwnerOfCurrentWS.Visible = true;
				}
		  }
		if (listWS.Length == 0) {
		  IEntAssignmentSet engineEAS = new IEntAssignmentSet(conn);
		  returnListEntAssignmentSetBySubProcess[] listEAS =
			 engineEAS.ListEntAssignmentSetBySubProcess
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
