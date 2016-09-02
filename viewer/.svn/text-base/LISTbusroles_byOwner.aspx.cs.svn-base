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
using System.Text;
using System.Data.Odbc;

namespace _6MAR_WebApplication.viewer
{
  public partial class WebForm13 : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.IsPostBack)
        {
          Response.Redirect
            ("LISTbusroles_byOwner.aspx?mode=searchrolename&srch="
             + HttpUtility.UrlEncode(this.TXTrolenamesrch.Text));
          return;
        }
      if (Request.Params["mode"] == "searchrolename")
        {
          this.TXTrolenamesrch.Text = Request.Params["srch"];
          this.PNLsearch.Visible = true;
        }
      else
        {
          this.PNLsearch.Visible = false;
        }
    }
    


    public string BuildWhereClause()
    {
        switch (Request.Params["mode"])
        {
            case "owner":
            return "BROWN.c_u_EID LIKE '" + Session["RAFLOGINbusOwnerEID"] + "'";
                

            case "searchrolename":
            case "search":
                return "BR.c_u_Name LIKE '%" + Request.Params["srch"] + "%'";
                

            case "searcheid":
                return "BROWN.c_u_EID LIKE '" + Request.Params["srch"] + "'";

            default:
                return " (5==5) ";
      }
    }







    // ACROSS ALL PROCESSES!
    public string RENDER()
    {



      StringBuilder BUFFER = new StringBuilder();


      /*
        BUFFER.Append(
        "<tr><th colspan='2'>Process: " + detailProcess.Name + "</th></tr>");
      */




      string sql = @"

SELECT DISTINCT
  PR.c_id,
  ISNULL(PR.c_u_Description,PR.c_u_Name) as ProcessDescription,
  SUBPR.c_id,
  SUBPR.c_u_Name,
  BR.c_id, 
  BR.c_u_Name, 
  ISNULL(BR.c_u_Description,BR.c_u_Name) as BRoleDescription,
  BR.c_u_RoleType as RoleTypeAbbrev, 
  (select Displayable from DICT_BusRoleType where Abbrev=c_u_RoleType) as RoleTypeDisplayable,
(
SELECT COUNT(DISTINCT ENT.c_u_Application)
   FROM t_RBSR_AUFW_u_EntAssignment EA
LEFT OUTER JOIN t_RBSR_AUFW_u_Entitlement ENT
   ON EA.c_r_Entitlement = ENT.c_id
LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignmentSet EASET
   ON EA.c_r_EntAssignmentSet = EASET.c_id 
WHERE
EA.c_r_BusRole = BR.c_id
AND
EASET.c_u_Status IN ('ACTIVE')
AND
EA.c_u_Status NOT IN ('X')
) as KOUNTAPPS,

USR.c_u_NameSurname, USR.c_u_NameFirst

FROM t_RBSR_AUFW_u_BusRole BR 

LEFT OUTER JOIN t_RBSR_AUFW_u_SubProcess SUBPR ON SUBPR.c_id = BR.c_r_SubProcess
LEFT OUTER JOIN t_RBSR_AUFW_u_Process       PR ON    PR.c_id = SUBPR.c_r_Process


LEFT OUTER JOIN 
t_RBSR_AUFW_u_BusRoleOwner BRPRIMOWN ON  BRPRIMOWN.c_r_BusRole=BR.c_id AND BRPRIMOWN.c_u_Rank = 'OWNprim'
LEFT OUTER JOIN 
t_RBSR_AUFW_u_User USR ON  BRPRIMOWN.c_u_EID = USR.c_u_EID


INNER JOIN 
t_RBSR_AUFW_u_BusRoleOwner BROWN ON  BROWN.c_r_BusRole=BR.c_id

WHERE " + BuildWhereClause() + @"
  AND
SUBPR.c_u_Status LIKE 'Active'
  AND
BR.c_u_Name NOT LIKE '%//DEL_%'

ORDER BY
ProcessDescription, SUBPR.c_u_Name, BR.c_u_Name

";


      OdbcDataReader DR = HELPERS.RunSqlSelect(sql);

      string curSubPr = "";

      while (DR.Read())
        {

          int idPR = DR.GetInt32(0);
          string descrPR = DR.GetString(1);
          int idSUBPR = DR.GetInt32(2);
          string nameSUBPR = DR.GetString(3);
          int idBRole = DR.GetInt32(4);
          string name = DR.GetString(5);
          string descr = DR.GetString(6);
          string roletype = DR.GetString(8);
          int appcount = DR.GetInt32(9);
          string surname = "";
          string firstname = "";
          try
          {
              surname = HELPERS.SafeObjToString(DR.GetString(10));
              firstname = HELPERS.SafeObjToString(DR.GetString(11));
          }
          catch (Exception) { }

          if (nameSUBPR != curSubPr)
            {
              // We are starting a new subprocess context
              if (curSubPr != "")
                {
                  BUFFER.Append("</table>\n\n");
                }
              curSubPr = nameSUBPR;

              BUFFER.Append("<P style='font-size:16px;font-weight:bold'>" + descrPR + " / " + nameSUBPR + "</P>");


              BUFFER.Append(@"

<table class='subprocess'>
 <thead>
 <tr>
 <th scope='col' width='25%'>Role Name</th>
 <th scope='col' width='25%'>Role Description</th>
 <th scope='col' width='20%'>Role Type</th>
 <th scope='col' width='30%'>Primary Owner</th>
 </tr>
 </thead><tbody>");
            }
          
          BUFFER.Append("<tr><td><a href='DETAILbusrole.aspx?"
                        +  "mode=" + Request.Params["mode"]
                        + "&srch=" + Request.Params["srch"]
                        + "&idSUBPR=" + idSUBPR.ToString()
                        + "&idPR=" + idPR.ToString()
                        + "&idBR=" + idBRole.ToString()
                        + "'>"
                        + name + "</a></td><td>" + descr
                        + "</td><td>" + roletype + "</td><td>" 
                        + ((surname!="") ?  (surname.ToString()+", "+firstname.ToString())   :   "")
                        + "</td></tr>");
        }

      BUFFER.Append("</table>");


      return BUFFER.ToString();
    }
          protected void TXTrolenamesrch_TextChanged(object sender, EventArgs e)
    {

    }




  }
}
