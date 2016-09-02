using System;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace _6MAR_WebApplication.export
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
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition",
                                                "filename=export.csv;attachment");

            int idWS = Int32.Parse(context.Request.Params["id"]);

            OdbcCommand cmd = new OdbcCommand();
            cmd.Connection = HELPERS.NewOdbcConn();
            cmd.CommandText =
              "SELECT ROLE.c_u_Name as RoleName, " +
	      "ROLE.c_u_System as System, " +
	      "ROLE.c_u_Platform as Platform, " +
	      "ROLE.c_u_RoleType as RoleType, " +
	      "ROLE.c_u_RoleActivity as RoleActivity, " +
              //"VEC.c_u_StandardActivity as StandardActivity, " +
              "VEC.c_u_TCode as TCode, " +
              "ENGLISH.c_u_Description as TCodeDescription, " +
              //"VEC.c_u_AuthObj as AuthorizationObject, " +
              "VEC.c_u_ActivityFolder as ActivityFolder, " +
//              "VEC.c_u_OrgAxisList as OrganizationCategory, " +
//              "VEC.c_u_OrgValue as OrganizationValue, " +
              "(CASE WHEN VEC.c_u_Type='M' THEN 'Menu' ELSE 'Background' END) as ViewType, " +
              "(CASE WHEN VEC.c_u_AccessLevel='U' THEN 'Update' ELSE 'Display' END) as AccessLevel, " +
              "LINK.c_u_Commentary as Commentary " +
              "FROM t_RBSR_AUFW_u_TcodeAssignment LINK " +
              "LEFT OUTER JOIN t_RBSR_AUFW_u_SAProle ROLE ON LINK.c_r_SAProle = ROLE.c_id " +
              "LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeEntitlement VEC ON LINK.c_r_TcodeEntitlement = VEC.c_id " +
	      "LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeDictionary ENGLISH ON VEC.c_u_TCode = ENGLISH.c_u_TcodeID " +
              "WHERE LINK.c_r_TcodeAssignmentSet = ? AND (LINK.c_u_EditStatus & 4)=0 ORDER BY RoleName;";
            cmd.Parameters.Add("c_r_EditingWorkspace", OdbcType.Int);
            cmd.Parameters["c_r_EditingWorkspace"].Value = (object)idWS;


            OdbcDataReader dr = cmd.ExecuteReader();

            int indexChecksum = -32;

            for (int i = 0; i < dr.VisibleFieldCount; i++)
            {
                context.Response.Write(CSVquoteize(dr.GetName(i)) + ",");
                if (dr.GetName(i) == "c_u_CHECKSUM")
                {
                    indexChecksum = i;
                }
            }
            context.Response.Write("\n");
            while (dr.Read())
            {
                for (int i = 0; i < dr.VisibleFieldCount; i++)
                {
                    if (i != indexChecksum)
                    {
                        context.Response.Write(CSVquoteize(dr.GetValue(i) as string) + ",");
                    }
                }
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
