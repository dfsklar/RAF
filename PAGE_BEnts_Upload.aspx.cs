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
  public partial class WebForm116 : AFWACpage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      DIVimportFeeback.Visible = false;
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
      REACTlaunchUpload(sender, e);
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
              if (dt.Columns.Count > 1)
                {
                  System.Data.Odbc.OdbcConnection conn =
                    new System.Data.Odbc.OdbcConnection(
                                                        ConfigurationManager.AppSettings["DBconnstr"]);

                  conn.Open();

                  Queue RETmsgs = new Queue();


                  if (this.COMBOXchooseApp.SelectedIndex < 0) {
                      this.PANELchooseApp.Style[HtmlTextWriterStyle.BackgroundColor] = "yellow";
                      return;
                  }
                

                  string strNameApplication =
                    this.COMBOXchooseApp.SelectedItem.Text;


                  int numChanges =
                    HELPERS.ImportNewEntitlementsFromDataTable
                    (dt, session.idUser, 
                     /* the application to attach these to */ strNameApplication,
                     this.COMBOXinitStatus.SelectedValue,
                     conn, RETmsgs);

                  if (RETmsgs.Count > 0)
                    {
                      string strMsgs = "";
                      foreach (object objMsg in RETmsgs.ToArray())
                        {
                          strMsgs += "\n" + objMsg.ToString();
                        }
                      TXTimportEngineMessages.Text = strMsgs;
                      DIVimportFeeback.Visible = true;
                                        
                      PANELcond_AllowUpload.Visible = false;
                    }
                  else
                    {
                      throw new Exception("SUCCESS!!!!");
                    }
                }
            }
        }
    }

  }



}
