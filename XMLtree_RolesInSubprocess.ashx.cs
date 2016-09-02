using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using RBSR_AUFW.DB.ISubProcess;
using RBSR_AUFW.DB.IBusRole;
using RBSR_AUFW.DB.IEntAssignmentSet;


namespace _6MAR_WebApplication
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler3 : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
        string strIdSubpr = context.Request.Params["subproc"];
        int idSubpr = int.Parse(strIdSubpr);

        string mode="regular";
        try
        {
            mode = context.Request.Params["mode"];
        }
        catch (Exception eee) { }



      // try valiantly to ensure no cacheing of this response
      context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-100);
      context.Response.AddHeader("pragma", "no-cache");
      context.Response.AddHeader("cache-control", "private");
      context.Response.CacheControl = "no-cache";




      ComponentArt.Web.UI.TreeView tview = new ComponentArt.Web.UI.TreeView();


      IBusRole engine = new IBusRole(HELPERS.NewOdbcConn());
      IEntAssignmentSet engineWS = new IEntAssignmentSet(HELPERS.NewOdbcConn());


      AFWACsession fakesession = new AFWACsession(context.Request);
      fakesession.idSubprocess = idSubpr;
      fakesession.idUser = -1;
      fakesession.ObtainWorkspaceContext();


      ComponentArt.Web.UI.TreeViewNode rootNode;


      if (mode == "ListEASetRetired")
      {
          returnListEntAssignmentSetBySubProcess[] listWS =
  engineWS.ListEntAssignmentSetBySubProcess
  (null, "\"Status\" IN ('archived')", new string[]{}, "c_u_DATETIMElock DESC", idSubpr);

          for (int i = 0; i < listWS.Length; i++)
          {
          rootNode = new ComponentArt.Web.UI.TreeViewNode();
          rootNode.Text = 
	    listWS[i].UserLoginName + " " + listWS[i].DATETIMElock + " - " + listWS[i].Commentary;
          rootNode.Expanded = false;
          rootNode.ImageUrl = "cal_nextMonth.gif";
          rootNode.ShowCheckBox = true;
          rootNode.ID = "EntSet/ARCHIVE/" + listWS[i].SubProcessID;
          rootNode.Value = listWS[i].ID.ToString();
          rootNode.RowCssClass = "TreeRow_EASet"; 
          rootNode.Checked = false;
          tview.Nodes.Add(rootNode);
        }

          }
    
      else 
  {

      bool alreadyCheckedChoiceEASet = false;

      // The very first node always represents the active set of entitlements
      if ( (fakesession.idActiveEAset >= 0) && (mode != "rolesonly") )
        {
          rootNode = new ComponentArt.Web.UI.TreeViewNode();
          rootNode.Text = "ACTIVE Entitlements";
          rootNode.Expanded = false;
          rootNode.ImageUrl = "cal_nextMonth.gif";
          rootNode.ShowCheckBox = true;
          rootNode.ID = "EntSet/ACT/" + idSubpr;
          rootNode.Value = fakesession.idActiveEAset.ToString();
          rootNode.RowCssClass = "TreeRow_EASet"; 
          rootNode.Checked = true;
          tview.Nodes.Add(rootNode);
          alreadyCheckedChoiceEASet = true;
        }
        
        
      // The very next node will exist only if a workspace exists
      if ( (fakesession.idWorkspace >= 0) && (mode != "rolesonly") )
        {
          rootNode = new ComponentArt.Web.UI.TreeViewNode();
          rootNode.Text = "WORKSPACE owned by " + fakesession.nameUserWorkspaceOwner;
          rootNode.Expanded = false;
          rootNode.ImageUrl = "cal_nextMonth.gif";
          rootNode.ShowCheckBox = true;
          rootNode.ID = "EntSet/WS/" + idSubpr;
          rootNode.Value =  fakesession.idWorkspace.ToString();
          rootNode.Checked =  ! alreadyCheckedChoiceEASet;
          rootNode.RowCssClass = "TreeRow_EASet";
          tview.Nodes.Add(rootNode);
          alreadyCheckedChoiceEASet = true;
        }


      if ( ( ! alreadyCheckedChoiceEASet) && (mode != "rolesonly") )  {
        rootNode = new ComponentArt.Web.UI.TreeViewNode();
        rootNode.Text = "WARNING: no entitlements yet - export will be empty";
        rootNode.Expanded = false;
        rootNode.ImageUrl = "close.gif";
        rootNode.ID = "EntSet/NONE/" + idSubpr;
        rootNode.Value =  fakesession.idWorkspace.ToString();
        rootNode.Checked =  ! alreadyCheckedChoiceEASet;
        rootNode.RowCssClass = "TreeRow_EASet";
        tview.Nodes.Add(rootNode);
        alreadyCheckedChoiceEASet = true;
      }


      if ( (mode != "rolesonly") )  {
	rootNode = new ComponentArt.Web.UI.TreeViewNode();
	rootNode.Text = "Retired/archived entitlement sets:";
	rootNode.Expanded = false;
	rootNode.ImageUrl = "folder.gif";
	rootNode.ID = "EntSet/FOLDERarchive/" + idSubpr;
	rootNode.ContentCallbackUrl = "XMLtree_RolesInSubprocess.ashx?mode=ListEASetRetired&subproc=" + idSubpr;
	tview.Nodes.Add(rootNode);
      }


      returnListBusRoleBySubProcess[] allroles = engine.ListBusRoleBySubProcess
        (null, "", new string[] { }, "c_u_Name asc", (idSubpr));


      // Added 6 July 2009:  if number of roles exceeds a certain amount, only show an "ALL".
      if (allroles.Length > 50) {
          rootNode = new ComponentArt.Web.UI.TreeViewNode();
          rootNode.Text = "ALL ROLES (too many to show)";
          rootNode.Expanded = false;
          rootNode.ImageUrl = "icon_flag.gif";
          rootNode.ShowCheckBox = true;
          rootNode.RowCssClass = "TreeRow_Role";
          rootNode.ID = "BR/" + idSubpr + "/*ALL*";
          rootNode.Checked = true;
          tview.Nodes.Add(rootNode);
      }
      else {
      foreach (returnListBusRoleBySubProcess cur in allroles)
        {
          rootNode = new ComponentArt.Web.UI.TreeViewNode();
          rootNode.Text = cur.Name;
          rootNode.Expanded = false;
          rootNode.ImageUrl = "icon_flag.gif";
          rootNode.ShowCheckBox = true;
          rootNode.RowCssClass = "TreeRow_Role";
          rootNode.ID = "BR/" + idSubpr + "/" + cur.ID;
          rootNode.Checked = false;

          tview.Nodes.Add(rootNode);
        }
      }
}

      context.Response.ContentType = "text/xml";
      context.Response.Write(tview.GetXml());
    }



    public bool IsReusable
    {
      get
        {
          return false;
        }
    }
  }
}
