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

namespace _6MAR_WebApplication
{
  public partial class WebForm128 : AFWACpage
  {
      
    protected int IDreport;


    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      if (this.Request.Params["id"] != null)
        {
            IDreport = int.Parse(this.Request.Params["id"]);
        }
      else 
	{
	  // If null, there is no way this page is of any use.
	  // You should really require the user to go to the History and pick a snapshot.
	  this.Response.Redirect("Page_Reconc_History.aspx");
	}
    }


    protected void INTERCEPTsqldatasource_IDreport
      (object sender, SqlDataSourceSelectingEventArgs e)
    {
      e.Command.Parameters[0].Value = IDreport;
    }

  }
}
