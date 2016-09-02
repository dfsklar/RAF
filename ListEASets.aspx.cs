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
    public partial class WebForm1 : AFWACpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView gv = sender as GridView;
            int selectedIdEas = (int)(gv.SelectedDataKey.Value);
            Response.Redirect("ViewEASet.aspx?IDeas=" + selectedIdEas);
        }


    }
}
