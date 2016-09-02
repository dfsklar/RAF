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
    public partial class WebForm19 : AFWACpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        protected void REACTlaunchUpload(object sender, EventArgs e)
        {
            if (this.FileUpload2.HasFile)
            {
                string pathTempFolder = System.IO.Path.GetTempPath();
                string pathTempFile = System.IO.Path.GetTempFileName();

                FileUpload2.SaveAs(pathTempFile);
                DataTable dt = HELPERS.LoadCsv(pathTempFolder,
                    System.IO.Path.GetFileName(pathTempFile));
                if (dt != null)
                {
                    if (dt.Columns.Count >= 1)
                    {

                        System.Data.Odbc.OdbcConnection conn =
                            HELPERS.NewOdbcConn();

                        int subprVisibleToAll =
                            int.Parse(ConfigurationManager.AppSettings["IDsubprocessVisibleToALL"] as string);


                        Queue RETmsgs = SAP_HELPERS.ImportListSAProlenames(
                            conn,
                            dt,
                            this.session.idSubprocess);

                        if (RETmsgs.Count > 0)
                        {
                            string strMsgs = "";
                            foreach (object objMsg in RETmsgs.ToArray())
                            {
                                strMsgs += "\n" + objMsg.ToString();
                            }
                            TXTimportEngineMessages.Text = strMsgs;
                            DIVimportFeeback.Visible = true;
                            DIVlaunchpad.Visible = false;
                            //PANELcond_AllowUpload.Visible = false;
                        }
                        else
                        {
                            Response.Redirect("ListSAPRoles.aspx");
                        }

                        return;
                    }
                }
                throw new Exception("The SAP rolename list file does not appear to be a simple single-column, no-header list of SAP role names.");
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            REACTlaunchUpload(sender, e);
        }


    }
}
