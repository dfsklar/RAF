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


            if (Page.Request.QueryString.Get("choose") == "latest")
            {
                IEnumerable rslt = DSfindLatestEASetToClone.Select(DataSourceSelectArguments.Empty);
                IEnumerator x = rslt.GetEnumerator();
                if (!x.MoveNext())
                {
                    IDeasetToClone = -1;
                    //                    throw new Exception("PROBLEM: the currently active subprocess does not have any entitlement set yet.");
                }
                else
                {
                    IDeasetToClone = int.Parse(((x.Current as DataRowView)["c_id"]).ToString());
                }
            }
            else
            {

                //
                // SELECT THE ACTIVE ONE
                //


                // find out which EASet to clone
                IEnumerable rslt = DSfindActiveEASetToClone.Select(DataSourceSelectArguments.Empty);
                IEnumerator x = rslt.GetEnumerator();
                if (!x.MoveNext())
                {
                    throw new Exception("PROBLEM: the currently active subprocess does not have any ACTIVE entitlement set yet.");
                }

                IDeasetToClone = int.Parse(((x.Current as DataRowView)["c_id"]).ToString());
                if (x.MoveNext())
                {
                    throw new Exception("PROBLEM: the currently active subprocess has more than one ACTIVE entitlement set!");
                }
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
     

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

            HELPERS.WorkspaceCreate(HELPERS.NewOdbcConn(),
                session.idSubprocess, session.idUser, IDeasetToClone,
                TextBox1.Text);
            Response.Redirect("ListEWorkspaces.aspx");
        }
    }
}
