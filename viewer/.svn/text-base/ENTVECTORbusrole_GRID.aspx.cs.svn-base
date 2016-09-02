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

namespace _6MAR_WebApplication.viewer
{
    public partial class WebForm12 : WEBFORMentvectorbusrole
    {

      int idOfActiveEntitSet = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
	  base.Page_Load(sender, e);
        }



	protected void INTERCEPTsqldatasource
	  (object sender, SqlDataSourceSelectingEventArgs e)
	{
	  int id = FindIdOfActiveEntitlementSet();
	  e.Command.Parameters[0].Value = id;
	}

    }
}
