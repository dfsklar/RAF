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
    public partial class WebForm131 : AFWACpage
    {

        public string strSaproleName;
	public string strSaprolePlatform;
        public int idSaprole;


        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }










        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
	}


        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            this.Page.Title = "SAP 1252 Entitlements - " + session.nameProcess + "/" + session.nameSubprocess;
            strSaproleName = Request.QueryString.Get("RoleName");
            if (Request.QueryString.Get("RoleID") != null)
            {
                this.idSaprole = int.Parse(Request.QueryString.Get("RoleID"));
            }
            else
            {
                throw new Exception("No identification of the SAP role being edited.");
            }


            if (Page.IsPostBack)
            {
                return;
            }


            DBrefresh();
            
        }


        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }



        private string mode = "UNK";


        private void DBrefresh()
        {
            // Some security checking
            DataView DV = (DataView)this.SQL_WorkspaceDetails.Select(new DataSourceSelectArguments());
            DataTable DVT = DV.ToTable("WORKSPACE");

            // 1. This workspace still in "WORKSPACE" status?
            if ("WORKSPACE" != (DVT.Rows[0]["c_u_Status"] as string))
            {
                this.mode = "READONLY";
            }
            else
            {

                // 2. This workspace is owned by the current user?
                if (this.session.idUser !=
                    (int)(DVT.Rows[0]["c_r_User"]))
                {
                    // Whoa!  This user is not the owner!
                    this.mode = "READONLY";
                }
                else
                {
                    this.mode = "EDIT";
                }
            }

            this.HIDDENeditmode.Value = this.mode;
	}

    }
}
