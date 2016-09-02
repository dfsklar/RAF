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
using RBSR_AUFW.DB.IEntAssignmentSet;
using System.Data.Odbc;
using System.Text;

namespace _6MAR_WebApplication.viewer
{
  public partial class WEBFORMentvectorbusrole : UIPAGE_DETAILbusrole
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);
    }


    public int FindIdOfActiveEntitlementSet()
    {
      // 1(a). Find the ID of the ACTIVE entitlement set for this subprocess.
      IEntAssignmentSet engineEASet =
        new IEntAssignmentSet(HELPERS.NewOdbcConn());
      returnListEntAssignmentSet[] ret =
        engineEASet.ListEntAssignmentSet(null, "\"Status\" = ? AND \"SubProcess\"=?", new string[] { "ACTIVE", theSubProcess.ID.ToString() }, "");
      if (ret.Length > 1)
        {
          throw new Exception("Database error: more than one ACTIVE entitlement set for this subprocess");
        }
      if (ret.Length < 1)
        {
          return -1;
        }
      return ret[0].ID;
    }



    public string RENDER()
    {

      int theActiveEASetID = FindIdOfActiveEntitlementSet();


      string thesql = @"

SELECT 

    TENT.c_u_Application,
    TENT.c_u_StandardActivity,
    TENT.c_u_RoleType,
    TENT.c_u_System,
    TENT.c_u_Platform,
    TENT.c_u_EntitlementName,
    TENT.c_u_EntitlementValue,
    TENT.c_u_AuthObjValue,
    TENT.c_u_FieldSecName,
    TENT.c_u_FieldSecValue,
    TENT.c_u_Level4SecName,
    TENT.c_u_Level4SecValue

FROM 
   t_RBSR_AUFW_u_Entitlement TENT

LEFT OUTER JOIN 
   t_RBSR_AUFW_u_EntAssignment TEASS 
    ON 
            TEASS.c_r_EntAssignmentSet = " + theActiveEASetID + @"
        AND TEASS.c_r_BusRole = " + theBR.ID + @"
        AND TEASS.c_r_Entitlement = TENT.c_id

WHERE 

    TEASS.c_u_Status NOT IN ('X')

ORDER BY   

    TENT.c_u_Application,
    TENT.c_u_StandardActivity,
    TENT.c_u_RoleType,
    TENT.c_u_System,
    TENT.c_u_Platform,
    TENT.c_u_EntitlementName,
    TENT.c_u_EntitlementValue,
    TENT.c_u_AuthObjValue,
    TENT.c_u_FieldSecName,
    TENT.c_u_FieldSecValue,
    TENT.c_u_Level4SecName,
    TENT.c_u_Level4SecValue
;";

      OdbcDataReader DR = HELPERS.RunSqlSelect(thesql);

      StringBuilder BUFFER = new StringBuilder();
      while (DR.Read())
	{
	  BUFFER.Append("<tr>");
	  for (int i = 0; i < 12; i++)
	    {
	      BUFFER.Append("<td>" +
			    (  (DR.GetValue(i).GetType() != DBNull.Value.GetType()) ? DR.GetString(i) : "") + "</td>");
              
	    }
	  BUFFER.Append("</tr>");
	}

      return BUFFER.ToString();
    }
  }
}
