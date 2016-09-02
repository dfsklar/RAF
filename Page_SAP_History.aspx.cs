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
  public partial class WebForm119 : AFWACpage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      PANELcond_InviteCreateWS.Visible = false;
      PANELcond_Locked.Visible = false;
      PANELcond_NewSubpr.Visible = false;
      PANELcond_IsOwnerOfCurrentWS.Visible = false;
        PANELcond_MultWorkspaces.Visible = false;

      try
      {
          session.ObtainWorkspaceContext_SAP();
      }
      catch (Exception ex)
      {
          if (ex.Message.Contains("more than one workspace"))
          {
              PANELcond_MultWorkspaces.Visible = true;
          }
      }
            
   

      if (session.idWorkspace_SAP >= 0) {

        // A WORKSPACE ALREADY EXISTS FOR THIS SUBPROCESS.
        if (session.isWorkspaceOwner_SAP) {
            if (PANELcond_MultWorkspaces.Visible == false)
              PANELcond_IsOwnerOfCurrentWS.Visible = true;
        }
        else {
          PANELcond_Locked.Visible = true;
        }

      }else{

        // Invite them to create a new workspace by cloning the
        // most recent locked EASet.

        ITcodeAssignmentSet engineWS = new ITcodeAssignmentSet(HELPERS.NewOdbcConn());
             
        returnListTcodeAssignmentSetBySubProcess[] listEAS =
          engineWS.ListTcodeAssignmentSetBySubProcess
          (null, "", new string[]{}, "c_u_tstamp DESC", session.idSubprocess);
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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      // Create a new workspace.
      ITcodeAssignmentSet Itaset = new ITcodeAssignmentSet(HELPERS.NewOdbcConn());
      int IDnewWS =
        Itaset.NewTcodeAssignmentSet(DateTime.Now, session.idSubprocess, session.idUser, "WORKSPACE");
      Itaset.SetTcodeAssignmentSet
        (IDnewWS, DateTime.Now, "Creation of new empty workspace", session.idSubprocess, session.idUser, "WORKSPACE");

      this.Response.Redirect("Page_SAP_History.aspx", true);
    }
  


    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        SAP_HELPERS.WorkspaceCreate
            (HELPERS.NewOdbcConn(),
            session.idSubprocess, session.idUser, -1, 
            "(Please edit this to describe this workspace)");

      this.Response.Redirect("Page_SAP_History.aspx", true);
    }
  }


}
