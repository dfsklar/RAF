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
  public partial class WebForm111 : AFWACpage
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



      session.ObtainWorkspaceContext_SAP();


      if (session.idWorkspace_SAP >= 0) 
	{
	  // A workspace already exists for this subprocess.
	  PANELcond_IsOwnerOfCurrentWS.Visible = session.isWorkspaceOwner_SAP;
	  PANELcond_IsOwnerOfCurrentWS.Visible =  ! session.isWorkspaceOwner_SAP;
	}
      else 
	{
	  if (session.idActiveTASS_SAP >= 0) 
	    {
	      PANELcond_InviteCreateWS.Visible = true;
	    }
	  else
	    {
	      PANELcond_NewSubpr.Visible = true;
	    }
	}

    }


		


    protected void ACTcreateBlankWorkspace(object sender, EventArgs e)
    {
        // For safety, re-check that there is not already a WS for this subpr.
        session.ObtainWorkspaceContext_SAP();
        if (session.idWorkspace_SAP >= 0)
        {
            throw new Exception("There is already a workspace for this subprocess.");
        }

        // Create a new workspace.
        ITcodeAssignmentSet Itaset = new ITcodeAssignmentSet(HELPERS.NewOdbcConn());
        int IDnewWS =
                Itaset.NewTcodeAssignmentSet(DateTime.Now, session.idSubprocess, session.idUser, "WORKSPACE");
        Itaset.SetTcodeAssignmentSet
           (IDnewWS, DateTime.Now, "Creation of new empty workspace", session.idSubprocess, session.idUser, "WORKSPACE");
    

    this.Response.Redirect("ListSAPWorkspaces.aspx", true);			 

  }

  }

}
