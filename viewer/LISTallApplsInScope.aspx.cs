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
using System.Text;
using System.Data.Odbc;

namespace _6MAR_WebApplication.viewer
{
    public partial class WebForm17 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public string RENDER()
        {
            StringBuilder BUFFER = new StringBuilder();



            OdbcDataReader DR = HELPERS.EnumerateAllAppsInScope();


            while (DR.Read())
            {
                string appname = DR.GetString(0);
                BUFFER.Append("<A href='LISTbusroles_byAppl.aspx?mode=search&fuzzy=no&srch="+appname+"'>"+appname + "</A>\n");
            }

            return BUFFER.ToString();
        }
    }
}
