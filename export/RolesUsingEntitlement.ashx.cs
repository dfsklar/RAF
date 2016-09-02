using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.Odbc;

namespace _6MAR_WebApplication.export
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler3 : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {

      context.Response.ContentType = "text/csv";
      context.Response.AddHeader("Content-Disposition",
                                 "filename=entitlementusage.csv;attachment");

      int idEAss = Int32.Parse(context.Request.Params["entid"]);

      OdbcCommand cmd = new OdbcCommand();
      cmd.Connection = HELPERS.NewOdbcConn();

      cmd.CommandText =
        @"
SELECT EA.c_id, PR.c_u_Name, SUBPR.c_u_Name, EASET.c_u_Status, EASET.c_u_DATETIMElock, BROL.c_u_Name
FROM t_RBSR_AUFW_u_EntAssignment EA
LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignmentSet EASET
   ON EA.c_r_EntAssignmentSet = EASET.c_id
LEFT OUTER JOIN t_RBSR_AUFW_u_BusRole BROL
   ON EA.c_r_BusRole = BROL.c_id
LEFT OUTER JOIN t_RBSR_AUFW_u_SubProcess SUBPR
   ON EASET.c_r_SubProcess = SUBPR.c_id
LEFT OUTER JOIN t_RBSR_AUFW_u_Process PR
   ON SUBPR.c_r_Process = PR.c_id
WHERE
   EASET.c_u_Status IN ('ACTIVE','WORKSPACE')
AND
   EA.c_u_Status NOT IN ('X') 
AND
   EA.c_r_Entitlement = ? 
ORDER BY
   PR.c_u_Name, SUBPR.c_u_Name, EASET.c_id;";

      cmd.Parameters.Add("ea", OdbcType.Int);
      cmd.Parameters["ea"].Value = (object)idEAss;

      OdbcDataReader dr = cmd.ExecuteReader();

      context.Response.Write("Process,Subprocess,SpaceType,FreezeDate,BusinessRole\n");

      while (dr.Read())
        {
          context.Response.Write(CSVquoteize(dr.GetValue(1) as string) + ",");
          context.Response.Write(CSVquoteize(dr.GetValue(2) as string) + ",");
          context.Response.Write(CSVquoteize(dr.GetValue(3) as string) + ",");
          context.Response.Write(CSVquoteize(dr.GetValue(4).ToString()) + ",");
          context.Response.Write(CSVquoteize(dr.GetValue(5) as string));
          context.Response.Write("\n");
        }
      dr.Close();
    }




    private string CSVquoteize(string strIN)
    {
      if (strIN == null)
	{
	  return "\"\"";
	}
      else
	{
	  return "\"" + strIN.Replace("\"", "\"\"") + "\"";
	}
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
