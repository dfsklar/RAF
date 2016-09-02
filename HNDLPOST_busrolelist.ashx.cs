using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using RBSR_AUFW.DB.IBusRole;
using System.Data.Odbc;



namespace _6MAR_WebApplication
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            OdbcConnection conn = HELPERS.NewOdbcConn();

            int IDsubpr = int.Parse(context.Request.Params["IDsubprocess"]);

            string linkedbusroles =
                " " + context.Request.Params["linkedbusroles"] + " ";

            IBusRole Ibr = new IBusRole(conn);

            context.Response.Write("<select ID='SELECTlinkedBRoles' multiple='multiple' class='geomfill2d'>");


            returnListBusRoleBySubProcess[] result =
                Ibr.ListBusRoleBySubProcess(null, "", 
                new string[] {}, "c_u_Abbrev ASC", IDsubpr);

            foreach (returnListBusRoleBySubProcess brole in result) {
                context.Response.Write("<option ");
                if (linkedbusroles.Contains(" " + brole.Abbrev + " ")) {
                    context.Response.Write("selected='1'");
                }
                context.Response.Write(">" + brole.Abbrev + "</option>");
            }
            context.Response.Write("</select>");
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
