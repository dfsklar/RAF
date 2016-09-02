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
  public class WORKSPACE : IHttpHandler
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
		  "VEC.c_u_StandardActivity as StandardActivity, " +
		  "VEC.c_u_RoleType as RoleType, " +
		  "VEC.c_u_Application as Application, " +
		  "VEC.c_u_System as System, " +
		  "VEC.c_u_Platform as Platform, " +
		  "VEC.c_u_EntitlementName as EntitlementName, " +
		  "VEC.c_u_EntitlementValue as EntitlementValue, " +
		  "VEC.c_u_AuthObjValue as AuthObjValue, " +
		  "VEC.c_u_FieldSecName as FieldSecName, " +
		  "VEC.c_u_FieldSecValue as FieldSecValue, " +
		  "VEC.c_u_Commentary as Commentary " +
		  //		  "VEC.c_u_EditStatus as EditStatus " +
		  "FROM t_r_BusRoleWorkspaceEntitlement LINK " +
		  "LEFT OUTER JOIN t_RBSR_AUFW_u_BusRole ROLE ON LINK.c_r_BusRole = ROLE.c_id " +
		  "LEFT OUTER JOIN t_RBSR_AUFW_u_WorkspaceEntitlement VEC ON LINK.c_r_WorkspaceEntitlement = VEC.c_id " +
		  "WHERE LINK.c_r_EditingWorkspace = ?;";
		cmd.Parameters.Add("c_r_EditingWorkspace", OdbcType.Int);
		cmd.Parameters["c_r_EditingWorkspace"].Value = (object)idWS;

		OdbcDataReader dr = cmd.ExecuteReader();

		int indexChecksum = -32;

		for (int i=0; i<dr.VisibleFieldCount; i++) {
		  context.Response.Write(CSVquoteize(dr.GetName(i)) + ",");
		  if (dr.GetName(i) == "c_u_CHECKSUM") {
			 indexChecksum = i;
		  }
		}
		context.Response.Write("\n");
		while (dr.Read())
		  {
			 for (int i=0; i<dr.VisibleFieldCount; i++) {
				if (i != indexChecksum) {
				  context.Response.Write(CSVquoteize(dr.GetValue(i) as string) + ",");
				}
			 }
			 context.Response.Write("\n");
		  }
		dr.Close();
	 }


	 private string CSVquoteize(string strIN) {
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
