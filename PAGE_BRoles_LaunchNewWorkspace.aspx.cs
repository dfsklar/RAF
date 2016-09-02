using System;
using System.Data;
using System.Configuration;
//using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections;

namespace _6MAR_WebApplication
{
  public partial class WebForm15 : AFWACpage
  {

    private int IDeasetToClone;


    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);


      IEnumerable rslt;
      IEnumerator x;


      switch(Page.Request.QueryString.Get("choose")) {




      case "blank":
	IDeasetToClone = -1;

	break;
		  



      case "latest":

		  
	rslt = DSfindLatestEASetToClone.Select(DataSourceSelectArguments.Empty);
	x = rslt.GetEnumerator();
	if (!x.MoveNext())
	  {
	    IDeasetToClone = -1;
	    //                    throw new Exception("PROBLEM: the currently active subprocess does not have any entitlement set yet.");
	  }
	else
	  {
	    IDeasetToClone = int.Parse(((x.Current as DataRowView)["c_id"]).ToString());
	  }
                
		  

	break;



      case "active":

	//
	// SELECT THE ACTIVE ONE
	//


	// find out which EASet to clone
		  
	rslt = DSfindActiveEASetToClone.Select(DataSourceSelectArguments.Empty);
	x = rslt.GetEnumerator();
	if (!x.MoveNext())
	  {
	    throw new Exception("PROBLEM: the currently active subprocess does not have any ACTIVE entitlement set yet.");
	  }

	IDeasetToClone = int.Parse(((x.Current as DataRowView)["c_id"]).ToString());
	if (x.MoveNext())
	  {
	    throw new Exception("PROBLEM: the currently active subprocess has more than one ACTIVE entitlement set!");
	  }
	break;	  
      }
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      throw new Exception("SHOULD NOT GET HERE");
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {

      int IDws =
	HELPERS.WorkspaceCreate(HELPERS.NewOdbcConn(),
				session.idSubprocess, session.idUser, IDeasetToClone,
				TextBox1.Text);
      session.idWorkspace = IDws;
      Response.Redirect("ListBRoles.aspx?WSID=" + IDws);
    }
  }
}
