using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using RBSR_AUFW.DB.IEASfileAttachment;

namespace _6MAR_WebApplication.export
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler4 : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {

      int id = Int32.Parse(context.Request.Params["id"]);

      returnDownloadEASfileAttachmentContent down = new returnDownloadEASfileAttachmentContent();


      IEASfileAttachment engine =
        new IEASfileAttachment(HELPERS.NewOdbcConn_FORCE());

      returnGetEASfileAttachment details = engine.GetEASfileAttachment(id);





      context.Response.ContentType = "binary";
      context.Response.AddHeader("Content-Disposition",
                                 "filename=" + details.Filename + ";attachment");

        
          down =
            engine.DownloadEASfileAttachmentContent(id);
      context.Response.OutputStream.Write(down.Content, 0, down.Content.Length);
    }



    public bool IsReusable
    {
      get
        {
          return false;
        }
    }
  }
}
