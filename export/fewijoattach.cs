using System;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using RBSR_AUFW.DB.IMVFormula;
using RBSR_AUFW.DB.IEntitlement;
using Eval3;

using CarlosAg.ExcelXmlWriter;
using RBSR_AUFW.DB.IBusRoleOwner;
using RBSR_AUFW.DB.IUser;
using RBSR_AUFW.DB.IEntAssignmentSet;
using RBSR_AUFW.DB.IBusRole;



namespace _6MAR_WebApplication.export
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler2 : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {

      int id = Int32.Parse(context.Request.Params["id"]);

      context.Response.ContentType = "binary";
      context.Response.AddHeader("Content-Disposition",
                                   "filename=export.bin;attachment");


      IEASfileAttachment engine =
	new IEASfileAttachment(HELPERS.NewOdbcConn_FORCE());
      

      //context.Response.
    }
  }
}
