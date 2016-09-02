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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TXTeid.Text = TXTeid.Text.Replace(" ", "");
            if (TXTeid.Text.Length == 0)
            {
                try
                {
                    TXTeid.Text = Session["RAFLOGINbusOwnerEID"].ToString();
                }
                catch (Exception eignore) { }
            }
        }

        protected void BTNtryByOwner_Click(object sender, EventArgs e)
        {
//            Session["RAFLOGINbusOwnerUserID"] = TXTeid.Text;
//            Session["RAFLOGINbusOwnerEID"] = TXTeid.Text;
            if (TXTeid.Text.Length == 0)
            {
                Response.Redirect("LISTbusroles_byOwner.aspx?mode=owner&srch=.");
            }
            else
            {
                Response.Redirect("LISTbusroles_byOwner.aspx?mode=searcheid&srch=" + TXTeid.Text);
            }
        }



        // This is called if SEARCH BY ROLE
        protected void BTNtry_Click(object sender, EventArgs e)
        {
            switch (this.CBOXroletype.SelectedValue)
            {
                case "IDM":
                    Response.Redirect
                        ("LISTbusroles_byOwner.aspx?mode=searchrolename" +
                         "&srch="
                            + HttpUtility.UrlEncode(this.TXTrolenamesrch.Text));
                    break;
                case "SAP":
                    Response.Redirect
                        ("LISTsaproles.aspx?mode=search" +
                         "&srch="
                            + HttpUtility.UrlEncode(this.TXTrolenamesrch.Text));
                    break;
            }
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }




        protected void BTNtryByTcode_Click(object sender, EventArgs e)
        {
            Response.Redirect
                ("LISTsaproles_byTcode.aspx?mode=search" +
                 "&srch="
                    + HttpUtility.UrlEncode(this.TXTtcode.Text));
        }




        protected void BTNsearchByAppName_Click(object sender, EventArgs e)
        {
            Response.Redirect
    ("LISTbusroles_byAppl.aspx?mode=search" +
     "&srch="
        + HttpUtility.UrlEncode(this.TXTappname.Text));

        }
    }
}
