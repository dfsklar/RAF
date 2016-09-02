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


/*
 * 
 * 
 * 
 this.User.Identity ::
 {System.Security.Principal.WindowsIdentity}
 [System.Security.Principal.WindowsIdentity]: {System.Security.Principal.WindowsIdentity}
 AuthenticationType: "NTLM"
 IsAuthenticated: true
 Name: "XP-CCEDEV\\David"
 * 
 * 
 */


namespace _6MAR_WebApplication
{
  public partial class _Default : System.Web.UI.Page
  {
	 protected void Page_Load(object sender, EventArgs e)
	 {
		Response.Redirect("HOME.aspx");
	 }
  }
}
