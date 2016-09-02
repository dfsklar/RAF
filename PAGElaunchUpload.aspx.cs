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

namespace _6MAR_WebApplication
{
  public partial class PAGElaunchUpload : AFWACpage
  {


	 protected new void Page_Load(object sender, EventArgs e)
		{
		  base.Page_Load(sender, e);

		  PANELcond_AbortUpload.Visible = false;
		  PANELcond_AllowUpload.Visible = false;

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
				PANELcond_AbortUpload.Visible = true;
			 }
		  else 
			 {
				PANELcond_AllowUpload.Visible = true;
			 }

	 }



		protected void REACTlaunchUpload(object sender, EventArgs e)
	 {
            /*
		if (this.FileUpload2.HasFile)
		  {
			 string pathTempFolder = System.IO.Path.GetTempPath();
			 string pathTempFile = System.IO.Path.GetTempFileName();

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
						int IDneweaset
						  =
                          HELPERS.ImportNewEntitlementsFromDataTable
						  (dt, session.idUser, conn, RETmsgs);

						// We have a new EASet successfully.
						// Now create a new workspace.
						int IDnewws = 
						  HELPERS.WorkspaceCreate(conn, session.idSubprocess,
														  session.idUser, IDneweaset,
														  "UPLOAD FROM CSV " +
														  FileUpload2.FileName + " performed at: " +
														  DateTime.Now);

						// Delete the temporary EASet.
						HELPERS.RunSql
						  ("DELETE FROM t_RBSR_AUFW_u_EntAssignment WHERE c_r_EntAssignmentSet=" + IDneweaset);
						HELPERS.RunSql
						  ("DELETE FROM t_RBSR_AUFW_u_EntAssignmentSet WHERE c_id=" + IDneweaset);

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
                            Response.Redirect("ListEWorkspaces.aspx");
                        }
					 }
				}
		  }
             */
	 }

	 protected void Button1_Click(object sender, EventArgs e)
	 {
		REACTlaunchUpload(sender, e);
	 }


  }
}
