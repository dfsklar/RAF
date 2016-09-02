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
using RBSR_AUFW.DB.IEASfileAttachment;

namespace _6MAR_WebApplication
{
    public partial class WebForm124 : AFWACpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile)
            {
                string pathTempFolder = System.IO.Path.GetTempPath();
                string pathTempFile = System.IO.Path.GetTempFileName();

                FileUpload1.SaveAs(pathTempFile);

                IEASfileAttachment engine =
                    new IEASfileAttachment(HELPERS.NewOdbcConn_FORCE());
                int baby = engine.NewEASfileAttachment(
                    this.FileUpload1.FileName,
                    TXTcomment.Text, DateTime.Now,
                    this.session.idWorkspace);
                engine.UploadEASfileAttachmentContent
                    (baby, pathTempFile);
                Response.Redirect("PAGE_attachments.aspx?mode=success");
            }
        }
    }
}
