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
  public partial class Site1 : System.Web.UI.MasterPage
  {

    //Version info:
    static public string AFWACRELNUM = "R-AF release 17.02.01";


    protected AFWACsession THESESSION = null;




        

    // I M P O R T A N T
    // NOTE: This page_load is called AFTER the actual content page page_load is
    // called, so don't expect this to be able to prep things for the
    // content page's page_load logic to use.
    protected void Page_Load(object sender, EventArgs e)
    {
        if (null != ConfigurationManager.AppSettings["IPADDRopenOnlyTo"])
        {
            if (ConfigurationManager.AppSettings["IPADDRopenOnlyTo"] !=
                Request.UserHostAddress)
            {
                Response.Redirect("LOCKOUT.aspx");
                return;
            }
        }


      if (Session["AFWACSESSION"] == null)
	{
	  Response.Redirect("SessionLOGIN.aspx");
	  return;
	}

      THESESSION = Session["AFWACSESSION"] as AFWACsession;
      this.HIDDENidSubpr.Value = THESESSION.idSubprocess.ToString();
      HIDDENidUser.Value = THESESSION.idUser.ToString();

      if (Page.Title.StartsWith("Untitled") )
	Page.Title = AFWACRELNUM;

      /*
      if (this.Request.Url.LocalPath.EndsWith("EntitlementWorkspace.aspx"))
	{
	  TabStrip1.Visible = false;
	}
       */


      if (this.Request.Url.LocalPath.EndsWith("/SAPEntitlementWorkspace.aspx"))
	{
	  TabStrip1.SelectedTab = TabStrip1.FindItemById("TAB_SAP_Designer");
	}
      else if (this.Request.Url.LocalPath.EndsWith("/PAGEroleDesigner.aspx"))
	{
	  TabStrip1.SelectedTab = TabStrip1.FindItemById("TAB_BUS_Designer");
	}
      else if (this.Request.Url.LocalPath.EndsWith("/PAGEroleDesAppList.aspx"))
	{
	  TabStrip1.SelectedTab = TabStrip1.FindItemById("TAB_BUS_Designer");
	}
      
    }



    protected void HiddenField1_ValueChanged(object sender, EventArgs e)
    {

    }
  }
}
