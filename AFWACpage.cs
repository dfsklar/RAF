using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace _6MAR_WebApplication
{
  public partial class AFWACpage : System.Web.UI.Page
  {
    protected AFWACsession session;

    protected  void Page_Load(object sender, EventArgs e)
    {
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      Response.Cache.SetNoStore();
      Response.Cache.SetExpires(DateTime.MinValue);
      if (Session["AFWACSESSION"] == null)
        {
          if (ConfigurationManager.AppSettings["BOOLautoLoginDevel"] == "1")
            {
              Session["AFWACSESSION"] = new AFWACsession(this.Request);
              session = Session["AFWACSESSION"] as AFWACsession;



				  // ------------------------------------------
              // THE SUBPROCESS FOR AUTO-LOGIN FOR TESTING

              session.idSubprocess = 10;
              session.nameProcess = "Common";
              session.nameSubprocess = "Exception Roles";




				  // ------------------------------------------
              // THE USER FOR AUTO-LOGIN FOR TESTING

              //session.idUser = 196;
              //session.username = "AnneVer";

              session.idUser = 233;
              session.username = "ChandraM";



              Session["UUIDSUBPROCESS"] = session.idSubprocess;
              Session["INTcurWS_SAP"] = "";
              Session["INTcurWS"] = "";
            }
          else
            {
              Response.Redirect("MSGnosess.htm");
            }
        }
      else
        {
          session = Session["AFWACSESSION"] as AFWACsession;
          session.strIPaddr = Context.Request.ServerVariables["REMOTE_ADDR"];
        }

    }





  }
}
