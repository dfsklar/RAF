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
    public partial class WebForm130 : AFWACpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            // this.DropDownList1.SelectedIndex = ... (this does work!)
            if (!this.Page.IsPostBack)
            {
                try
                {
                    this.DropDownList1.SelectedValue =
                        this.Request.QueryString.Get("type");
                    DropDownList1_SelectedIndexChanged(sender, e);
                }
                catch (Exception) { }
            }
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


                        Queue RETmsgs = null;

                        switch (this.DropDownList1.SelectedValue)
                        {
                            case "AOCLASS":
                                RETmsgs = SAP_HELPERS.ImportListSAPauthobjClasses(
                                    conn,
                                    dt,
                                    this.session.idSubprocess);
                                break;
                            case "AOBJ":
                                RETmsgs = SAP_HELPERS.ImportListSAPauthobjs(
                                    conn,
                                    dt,
                                    this.session.idSubprocess);
                                break;
                            case "AFLD":
                                RETmsgs = SAP_HELPERS.ImportDictSAPauthobjFields(
                                    conn,
                                    dt,
                                    this.session.idSubprocess);
                                break;
                            case "TC":
                                RETmsgs = SAP_HELPERS.ImportTcodeList(conn, dt, this.session.idSubprocess);
                                break;
                        }

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
                            Response.Redirect("NoMessages.aspx");
                        }

                        return;
                    }
                }
                throw new Exception("INTERNAL ERR");
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            REACTlaunchUpload(sender, e);
        }



        /*
         * Note that dropdown widgets by default  do not report changes
         * immediately/automatically without a special postback setting
         * turned on in the aspx.
         */
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedvalue = this.DropDownList1.SelectedValue;

            switch (selectedvalue)
            {
                case "AOCLASS":
                    this.LITERALcsvspec.Text = "Name,Description";
                    break;
                case "AOBJ":
                    this.LITERALcsvspec.Text = "Class,Name,Description";
                    break;
                case "AFLD":
                    this.LITERALcsvspec.Text = "Object,Name,Description";
                    break;
                case "TC":
                    this.LITERALcsvspec.Text = "Name,Description";
                    break;
                /*
                 * case "ROLEPLAT":
                    this.LITERALcsvspec.Text = "(column spec not available yet)";
                    break;
                 */
                default:
                    throw new Exception("NYI");
            }
      
            
        }



        // User wants to download a template for the selected upload type.
        protected void Button2_Click(object sender, EventArgs e)
        {

        }


    }
}
