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
using ComponentArt.Web.UI;
using RBSR_AUFW.DB.ISubProcess;
using RBSR_AUFW.DB.IProcess;



namespace _6MAR_WebApplication
{
  public partial class WebForm129 : AFWACpage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      if (MEGATREE.Nodes.Count == 0)
        {
          LoadProcesses();
        }

      //CHKuseAllRoles.Attributes.Add("onclick", "EVTHNDLchkUseAllRoles(this);");
    }


    private void LoadProcesses()
    {
      IProcess engineProcess = new IProcess(HELPERS.NewOdbcConn());
      returnListProcess[] allprocs = engineProcess.ListProcess(null, "", new string[] { }, "c_u_Name asc");
      foreach (returnListProcess cur in allprocs)
		  {
			 if (cur.Name == "-")
				{
				  continue;
				}
			 if (cur.Name.ToLower() == "(internal use)")
				{
				  continue;
				}
			 if (cur.Name.ToLower() == "test") { continue; }

			 ComponentArt.Web.UI.TreeViewNode rootNode = new ComponentArt.Web.UI.TreeViewNode();
			 rootNode.Text = cur.Name;
			 rootNode.Expanded = false;
			 rootNode.ImageUrl = "folder.gif";
			 rootNode.ShowCheckBox = true;
			 rootNode.ID = "PR/" + cur.ID;
			 MEGATREE.Nodes.Add(rootNode);

			 LoadSubProcesses(cur.ID, rootNode);
		  }
    }



    private void LoadSubProcesses(int idProc, TreeViewNode nodeProc)
    {
      ISubProcess engineProcess = new ISubProcess(HELPERS.NewOdbcConn());
      returnListSubProcessByProcess[] allprocs = engineProcess.ListSubProcessByProcess
		  (null, "\"Status\" = ?", new string[] { "Active" }, "c_u_Name asc", idProc);
      foreach (returnListSubProcessByProcess cur in allprocs)
		  {
			 ComponentArt.Web.UI.TreeViewNode rootNode = new ComponentArt.Web.UI.TreeViewNode();
			 rootNode.Text = cur.Name;
			 rootNode.Expanded = false;
			 rootNode.ImageUrl = "spacer.gif";
			 rootNode.ShowCheckBox = true;
			 rootNode.ID = "SP/" + cur.ID;

			 //rootNode.ContentCallbackUrl = "XMLtree_RolesInSubprocess.ashx?mode=rolesonly&subproc=" + cur.ID;
              
			 nodeProc.Nodes.Add(rootNode);

		  }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      Context.Response.Redirect("export/ReconcileIDM.ashx?mode=rafdump");
    }





protected string gleanListofCheckedNodes_CSVformat()
	 {
      // We need to get the list of checked role IDs from the tree of checkboxes
      TreeViewNode[] checkednodes = MEGATREE.CheckedNodes;
      string CSVlistofroles = "-1";
      foreach (TreeViewNode curnode in checkednodes)
		  {
			 //string srchfor = "BR/";
			 if (curnode.ID.StartsWith("SP/"))
				{
				  // This node represents a subprocess.
				  // If it was never given its web-loaded (deferred loading) set of
				  // busrole children, then this node's checkmark immplicates
				  // all the busroles!
				  if (curnode.Nodes.Count == 0)
					 {
						CSVlistofroles += ("," + curnode.ID);
					 }
				}
			 else if (curnode.ID.StartsWith("BR/"))
				{
				  // This node represents a business role.
				  if (curnode.Checked)
					 {
						string[] parts = curnode.ID.Split(new char[] { '/' });

						// Special value: parts[1] == "*ALL*" means all roles is implied, we do not have an explicit list.
						// The ARRidNode array will thus be left empty and that signals "show all roles" to the export engine.
                    
						{
						  int subpr = int.Parse(parts[1]);
						  int broleID = -1;  // -1 is special sentinel meaning all business roles *ALL*
						  if (parts[2] != "*ALL*")
							 {
								broleID = int.Parse(parts[2]);
							 }

						  if (broleID >= 0)
							 {
								CSVlistofroles += ("," + broleID.ToString());
							 }
						}
					 }
				}
		  }
		return CSVlistofroles;
	 }




    protected void ButtonExport_Click(object sender, EventArgs e)
    {
		string CSVlistofroles = gleanListofCheckedNodes_CSVformat();
		Context.Response.Redirect("export/ReconcileSAP.ashx?mode=dumpraf" 
										  + "&save=" + this.CHKsaveToHistory.Checked.ToString()
										  + "&usereid=" + session.idUser 
										  + "&username=" + Uri.EscapeUriString(session.username)
										  + "&roles=" + CSVlistofroles);
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
		string CSVlistofroles = gleanListofCheckedNodes_CSVformat();


      if (this.FileUpload1.HasFile)
		  {
			 string pathTempFolder = System.IO.Path.GetTempPath();
			 string pathTempFile = System.IO.Path.GetTempFileName();

			 FileUpload1.SaveAs(pathTempFile);

			 Context.Response.Redirect("export/ReconcileSAP.ashx?mode=compare" 
				    
												+ "&save=" + this.CHKsaveToHistory.Checked.ToString()
												+ "&usereid=" + session.idUser 
												+ "&username=" + Uri.EscapeUriString(session.username)
												+ "&roles=" + CSVlistofroles
												+ "&pathinput=" + Uri.EscapeUriString(pathTempFile));
		  }
    }
  }
 
}
