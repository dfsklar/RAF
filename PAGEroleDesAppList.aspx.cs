using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using RBSR_AUFW.DB.IBusRole;
using System;

namespace _6MAR_WebApplication
{
  public partial class WebForm115 : AFWACpage
  {

    public int IDrole;

    public returnGetBusRole brole;


    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      try
	{
	  IDrole = int.Parse(this.Request.Params["RoleID"]);
	  this.session.idRole = IDrole;
	}
      catch (Exception ex)
	{
	  IDrole = this.session.idRole;
	}

      IBusRole IFACEbrole = new IBusRole(HELPERS.NewOdbcConn());
      brole = IFACEbrole.GetBusRole(IDrole);

      // We are now doing some sticky stuff so you can dive
      // into an old non-workspace EASet, and bounce up/down
      // without worrying about getting reset back to looking
      // at the workspace.
      //Session["INTcurWS"] = session.idWorkspace;


      Session["INTcurBUSROLE"] = session.idRole;

    }
  }
}
