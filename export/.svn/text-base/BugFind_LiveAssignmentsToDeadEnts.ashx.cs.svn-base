using System;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

namespace _6MAR_WebApplication.export
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class BugFind_LiveAssignmentsToDeadEnts : IHttpHandler
  {

	 public void ProcessRequest(HttpContext context)
	 {
		context.Response.ContentType = "text/csv";
		context.Response.AddHeader("Content-Disposition",
											"filename=export.csv;attachment");

		Boolean boolDoFix = false;
        try
        {
            boolDoFix = Boolean.Parse(context.Request.Params["autofix"]);
        }
        catch (Exception) { }

		OdbcCommand cmd = new OdbcCommand();
		cmd.Connection = HELPERS.NewOdbcConn();
        cmd.CommandText =
@"
select  
EA.c_id as EAid,
EA.c_r_BusRole as EAbrole, 
EA.c_r_Entitlement as EAentref, 
EA.c_u_Status as EAstatus,
BR.c_u_Name as BusRole,
EA.c_r_EntAssignmentSet as EASetID,
EASET.c_u_Status as EASetStatus,
ENT.*

from t_RBSR_AUFW_u_EntAssignment EA

left outer join t_RBSR_AUFW_u_Entitlement ENT
on  ENT.c_id=EA.c_r_Entitlement
left outer join t_RBSR_AUFW_u_EntAssignmentSet EASET
on  EASET.c_id=EA.c_r_EntAssignmentSet
left outer join t_RBSR_AUFW_u_BusRole BR
on  BR.c_id=EA.c_r_BusRole

WHERE 

ENT.c_u_Status = 'X' 
AND
EASET.c_u_Status in ('archived','ACTIVE','WORKSPACE')
AND
EA.c_u_Status in ('N','A')


order by EA.c_id
";


        context.Response.Write("EntAssgnID,BRoleID,EntID,EntAssignStatus,BRoleName,EASetID,EASetStatus,ENTITLEMENTVECTOR\n");


		OdbcDataReader dr = cmd.ExecuteReader();




		while (dr.Read())
		  {
              int IDea = dr.GetInt32(0);
              int IDbrole = dr.GetInt32(1);
              int IDent = dr.GetInt32(2);
              string STATUSea = dr.GetString(3);

              string key = IDbrole.ToString() + "/" + IDent.ToString();
              if (key == key)
              {   
                  // WE HAVE A LIVE ASSIGNMENT TO A DEAD THING?
                  for (int i = 0; i < dr.VisibleFieldCount; i++)
                  {
                          context.Response.Write(CSVquoteize(dr.GetValue(i).ToString()) + ",");
                  }
                  context.Response.Write("\n");
                
              }
              else
              {
                  if (STATUSea == "X")
                  {
                    
                  }
              }

      
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
