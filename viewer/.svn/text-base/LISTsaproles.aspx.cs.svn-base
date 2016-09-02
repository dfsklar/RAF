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
    public partial class WebForm14 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {

                if (Request.Params["mode"] == "search")
                {
                    this.TXTrolenamesrch.Text = Request.Params["srch"];
                    this.PNLsearch.Visible = true;
                }
                else
                {
                    this.PNLsearch.Visible = false;
                }
            }

        }


        public string BuildWhereClause()
        {
            if (Request.Params["mode"] == "owner")
            {
                return "BROWN.c_u_EID LIKE '" + Session["RAFLOGINbusOwnerEID"] + "'";
            }
            else
            {
                return "SAPROLE.c_u_Name LIKE '%" + this.TXTrolenamesrch.Text + "%'";
            }
        }


        // ACROSS ALL PROCESSES!
        public string RENDER()
        {



            StringBuilder BUFFER = new StringBuilder();





            string sql = @"

SELECT DISTINCT

PR.c_id,      
PR.c_u_Description,      

SUBPR.c_id,      
SUBPR.c_u_Name,         

BROLE.c_id as IDbusrole,
BROLE.c_u_Name as NAMEbusrole,

SAPROLE.c_id as IDsaprole,
SAPROLE.c_u_Name as NAMEsaprole,

SAPROLE.c_u_Description,      
SAPROLE.c_u_System,      
SAPROLE.c_u_Platform,      
SAPROLE.c_u_RoleActivity,      
SAPROLE.c_u_RoleType as RoleTypeAbbrev,

BRUSR.c_u_NameSurname as BOWNERsur, BRUSR.c_u_NameFirst as BOWNERfirst,      
SAPUSR.c_u_NameSurname as SAPOWNERsur, SAPUSR.c_u_NameFirst as SAPOWNERfirst

FROM t_RBSR_AUFW_u_SAProle SAPROLE

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

LEFT OUTER JOIN 
t_RBSR_AUFW_u_SAPRoleOwner SAPPRIMOWN ON  SAPPRIMOWN.c_r_SAProle=SAPROLE.c_id AND SAPPRIMOWN.c_u_Rank = 'OWNprim'

LEFT OUTER JOIN     t_RBSR_AUFW_u_User BRUSR ON  BRPRIMOWN.c_u_EID = BRUSR.c_u_EID
LEFT OUTER JOIN     t_RBSR_AUFW_u_User SAPUSR ON  SAPPRIMOWN.c_u_EID = SAPUSR.c_u_EID
 


LEFT OUTER JOIN t_RBSR_AUFW_u_SubProcess SUBPR ON SUBPR.c_id = BROLE.c_r_SubProcess    
LEFT OUTER JOIN t_RBSR_AUFW_u_Process       PR ON    PR.c_id = SUBPR.c_r_Process            


WHERE " + BuildWhereClause() + @"

AND    SUBPR.c_u_Status LIKE 'Active'      
AND    SAPROLE.c_u_Name NOT LIKE '%//DEL_%'     
AND    BROLE.c_u_Name NOT LIKE '%//DEL_%'     

AND EASSET.c_u_Status='ACTIVE'
AND (EASS.c_u_Status != 'X')


ORDER BY    PR.c_u_Description, SUBPR.c_u_Name, BROLE.c_u_Name, SAPROLE.c_u_Name ;

";


            OdbcDataReader DR = HELPERS.RunSqlSelect(sql);

            string curSubPr = "";

            while (DR.Read())
            {

                int idPR = DR.GetInt32(0);
                string descrPR = "";
					 if ( ! DR.IsDBNull(1)) {
					 	 descrPR = DR.GetString(1);
					 }
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

                string BOWNsurname = "";
                string BOWNfirstname = "";
                try
                {
                    BOWNsurname = HELPERS.SafeObjToString(DR.GetString(++colnum));
                    BOWNfirstname = HELPERS.SafeObjToString(DR.GetString(++colnum));
                }
                catch (Exception) { }

                string SAPOWNsurname = "";
                string SAPOWNfirstname = "";
                try
                {
                    SAPOWNsurname = HELPERS.SafeObjToString(DR.GetString(++colnum));
                    SAPOWNfirstname = HELPERS.SafeObjToString(DR.GetString(++colnum));
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
 <th scope='col' width='30%'>IDM Owner</th>
 <th scope='col' width='25%'>SAP Role</th>
 <th scope='col' width='30%'>SAP Owner</th>
 <th scope='col' width='20%'>Platform</th>
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
                              + nameBrole + "</a></td>"
										+ "<td>"
                              + ((BOWNsurname != "") ? (BOWNsurname.ToString() + ", " + BOWNfirstname.ToString()) : "")
                              + "</td>" 
										+ "<td>" + nameSAPRole + "</td>"
										+ "<td>"
                              + ((SAPOWNsurname != "") ? (SAPOWNsurname.ToString() + ", " + SAPOWNfirstname.ToString()) : "")
                              + "</td>" 
										+ "<td>" + platformSAPRole + "</td>"
										+ "</tr>");
            }

            BUFFER.Append("</table>");


            return BUFFER.ToString();
        }

        
        protected void TXTrolenamesrch_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
