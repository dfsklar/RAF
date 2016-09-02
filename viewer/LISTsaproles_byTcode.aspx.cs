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

    public partial class WebForm15 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if ( ! this.IsPostBack)
            {
                this.TXTtcodesrch.Text = Request.Params["srch"];
             
            }
            if (Request.Params["mode"] == "search")
            {
                this.PNLsearch.Visible = true;
            }
            else
            {
                this.PNLsearch.Visible = false;
            }

        }



        // Returns an expression with an auto-appended "AND" operator
        public string BuildWhereClause()
        {
            if (Request.Params["mode"] == "owner")
            {
                return "BROWN.c_u_EID LIKE '" + Session["RAFLOGINbusOwnerEID"] + "' AND ";
            }
            else
            {
                return " "; //SAPROLE.c_u_Name LIKE '%" + Request.Params["srch"] + "%'";
            }
        }


        // ACROSS ALL PROCESSES!
        public string RENDER()
        {



            StringBuilder BUFFER = new StringBuilder();


            string srch = this.TXTtcodesrch.Text; // Request.Params["srch"].Replace(" ", "");



            if (srch.Length < 3)
            {
                return ("<H2>The TCode fragment must be at least three characters in length.</H2>");
                
            }


            string sql = @"



SELECT DISTINCT

PR.c_id,      
ISNULL(PR.c_u_Description,PR.c_u_Name) as ProcessDescription,

SUBPR.c_id,      
SUBPR.c_u_Name,         

BROLE.c_id as IDbusrole,
BROLE.c_u_Name as NAMEbusrole,

SAPROLE.c_id as IDsaprole,
SAPROLE.c_u_Name as NAMEsaprole,

ISNULL(SAPROLE.c_u_Description,SAPROLE.c_u_Name) as DESCRsaprole,      
SAPROLE.c_u_System,      
SAPROLE.c_u_Platform,      
SAPROLE.c_u_RoleActivity,      
SAPROLE.c_u_RoleType as RoleTypeAbbrev,

TENT.c_u_TCode,

USR.c_u_NameSurname, USR.c_u_NameFirst
     


FROM t_RBSR_AUFW_u_TcodeEntitlement TENT



LEFT OUTER JOIN   t_RBSR_AUFW_u_TcodeAssignment TASS
   ON TASS.c_r_TcodeEntitlement=TENT.c_id 
LEFT OUTER JOIN   t_RBSR_AUFW_u_TcodeAssignmentSet TASSET
   ON TASS.c_r_TcodeAssignmentSet=TASSET.c_id 
LEFT OUTER JOIN t_RBSR_AUFW_u_SAProle SAPROLE
   ON SAPROLE.c_id = TASS.c_r_SAProle


LEFT OUTER JOIN   t_RBSR_AUFW_u_Entitlement MIRRENT 
   ON MIRRENT.c_u_Application='SAP' AND MIRRENT.c_u_EntitlementValue=SAPROLE.c_u_Name

LEFT OUTER JOIN   t_RBSR_AUFW_u_EntAssignment EASS
   ON EASS.c_r_Entitlement=MIRRENT.c_id 

LEFT OUTER JOIN   t_RBSR_AUFW_u_EntAssignmentSet EASSET
   ON EASS.c_r_EntAssignmentSet=EASSET.c_id 

LEFT OUTER JOIN   t_RBSR_AUFW_u_BusRole BROLE 
   ON BROLE.c_id = EASS.c_r_BusRole

 
LEFT OUTER JOIN 
t_RBSR_AUFW_u_BusRoleOwner BRPRIMOWN ON  BRPRIMOWN.c_r_BusRole=BROLE.c_id AND BRPRIMOWN.c_u_Rank = 'OWNprim'

LEFT OUTER JOIN     t_RBSR_AUFW_u_User USR ON  BRPRIMOWN.c_u_EID = USR.c_u_EID            
 


LEFT OUTER JOIN t_RBSR_AUFW_u_SubProcess SUBPR ON SUBPR.c_id = BROLE.c_r_SubProcess    
LEFT OUTER JOIN t_RBSR_AUFW_u_Process       PR ON    PR.c_id = SUBPR.c_r_Process            




WHERE " + BuildWhereClause() + @"

       TENT.c_u_TCode LIKE '%" + srch + @"%'

AND    SUBPR.c_u_Status LIKE 'Active'      
AND    SAPROLE.c_u_Name NOT LIKE '%//DEL_%'     
AND    BROLE.c_u_Name NOT LIKE '%//DEL_%'     

AND EASSET.c_u_Status='ACTIVE'
AND (EASS.c_u_Status != 'X')

AND TASSET.c_u_Status='ACTIVE'
AND (TASS.c_u_EditStatus & 4) = 0

ORDER BY  ProcessDescription, SUBPR.c_u_Name, BROLE.c_u_Name, SAPROLE.c_u_Name ;

";


            OdbcDataReader DR = HELPERS.RunSqlSelect(sql);

            string curSubPr = "";

            if (!DR.HasRows)
            {
                return "<h3>This search resulted in no results.</h3>";
            }

            while (DR.Read())
            {

                int idPR = DR.GetInt32(0);
                string descrPR = DR.GetString(1);
                int idSUBPR = DR.GetInt32(2);
                string nameSUBPR = DR.GetString(3);
                int idBRole = DR.GetInt32(4);
                string nameBrole = DR.GetString(5);
                int idSAPRole = DR.GetInt32(6);
                string nameSAPRole = DR.GetString(7);
                int colnum = 7;
                string descrSAPRole = DR.GetString(++colnum);
                string systemSAPRole = DR.GetString(++colnum);
                string platformSAPRole = DR.GetString(++colnum);
                string roleactivitySAPRole = DR.GetString(++colnum);
                string roletypeSAPRole = DR.GetString(++colnum);

                string tcodename = DR.GetString(++colnum);

                string surname = "";
                string firstname = "";
                try
                {
                    surname = HELPERS.SafeObjToString(DR.GetString(++colnum));
                    firstname = HELPERS.SafeObjToString(DR.GetString(++colnum));
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
 <th scope='col' width='25%'>IDM Role</th>
 <th scope='col' width='25%'>SAP Role</th>
 <th scope='col' width='20%'>Platform</th>
 <th scope='col' width='30%'>Primary Owner</th>
 <th scope='col' width='30%'>TCode</th> 
 </tr>
 </thead><tbody>");
                }

                BUFFER.Append("<tr><td><a href='DETAILbusrole.aspx?"
                              + "mode=" + Request.Params["mode"]
                              + "&srch=" + Request.Params["srch"]
                              + "&idSUBPR=" + idSUBPR.ToString()
                              + "&idPR=" + idPR.ToString()
                              + "&idBR=" + idBRole.ToString() + "#ANCHORsaproles"
                              + "'>"
                              + nameBrole + "</a></td><td>" + nameSAPRole
                              + "</td><td>" + platformSAPRole + "</td><td>"
                              + ((surname != "") ? (surname.ToString() + ", " + firstname.ToString()) : "")
                              + "</td><td>" + tcodename + "</td><td>"
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
