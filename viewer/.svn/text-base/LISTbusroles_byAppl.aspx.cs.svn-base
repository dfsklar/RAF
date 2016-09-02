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
    public partial class WebForm16 : System.Web.UI.Page
    {
        bool useFuzzySearch = true; // Meaning use SQL "LIKE" instead of "="



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.TXTsrch.Text = Request.Params["srch"];

            }
            if (Request.Params["mode"] == "search")
            {
                this.PNLsearch.Visible = true;
            }
            else
            {
                this.PNLsearch.Visible = false;
            }


            try {
            if (Request.Params["fuzzy"] == "no") {
                useFuzzySearch = false;
            }
            }
                catch(Exception){}

        }





        public string RENDER()
        {

            StringBuilder BUFFER = new StringBuilder();


            string srch = this.TXTsrch.Text; // Request.Params["srch"].Replace(" ", "");



            if (srch.Length < 3)
            {
                return ("<H2>The application-name fragment must be at least three characters in length.</H2>");

            }


            string sql = @"



SELECT DISTINCT

PR.c_id,      
ISNULL(PR.c_u_Description,PR.c_u_Name) as ProcessDescription,

SUBPR.c_id,      
SUBPR.c_u_Name,         

BROLE.c_id as IDbusrole,
BROLE.c_u_Name as NAMEbusrole,
ISNULL(BROLE.c_u_Description,BROLE.c_u_Name) as DESCRbusrole,
BROLE.c_u_RoleType as RoleTypeAbbrev, 
(select Displayable from DICT_BusRoleType where Abbrev=BROLE.c_u_RoleType) as RoleTypeDisplayable,


USR.c_u_NameSurname, USR.c_u_NameFirst
     


FROM t_RBSR_AUFW_u_Entitlement ENT

LEFT OUTER JOIN   t_RBSR_AUFW_u_EntAssignment EASS
   ON EASS.c_r_Entitlement=ENT.c_id 

LEFT OUTER JOIN   t_RBSR_AUFW_u_EntAssignmentSet EASSET
   ON EASS.c_r_EntAssignmentSet=EASSET.c_id 

LEFT OUTER JOIN   t_RBSR_AUFW_u_BusRole BROLE 
   ON BROLE.c_id = EASS.c_r_BusRole

 
LEFT OUTER JOIN 
t_RBSR_AUFW_u_BusRoleOwner BRPRIMOWN ON  BRPRIMOWN.c_r_BusRole=BROLE.c_id AND BRPRIMOWN.c_u_Rank = 'OWNprim'

LEFT OUTER JOIN     t_RBSR_AUFW_u_User USR ON  BRPRIMOWN.c_u_EID = USR.c_u_EID            
 


LEFT OUTER JOIN t_RBSR_AUFW_u_SubProcess SUBPR ON SUBPR.c_id = BROLE.c_r_SubProcess    
LEFT OUTER JOIN t_RBSR_AUFW_u_Process       PR ON    PR.c_id = SUBPR.c_r_Process            




WHERE " + BuildWhereClause() + " ENT.c_u_Application " +
        
        (useFuzzySearch ? " LIKE '%" + srch + @"%' "  : " = '" + srch + "'")

            + @"

AND    SUBPR.c_u_Status LIKE 'Active'      
AND    BROLE.c_u_Name NOT LIKE '%//DEL_%'     

AND EASSET.c_u_Status='ACTIVE'
AND (EASS.c_u_Status != 'X')


ORDER BY    ProcessDescription, SUBPR.c_u_Name, BROLE.c_u_Name ;


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
                string descrBrole = DR.GetString(6);
                // Field 7 is unused.
                string typeBrole = DR.GetString(8);
                int colnum = 8;

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
                                                                                <th scope='col' width='25%'>Role Name</th>
                                                                                <th scope='col' width='25%'>Role Description</th>
                                                                                <th scope='col' width='20%'>Role Type</th>
                                                                                <th scope='col' width='30%'>Primary Owner</th>
                                                                        </tr>
 </thead><tbody>");
                }

                BUFFER.Append("<tr><td><a href='DETAILbusrole.aspx?"
                              + "mode=" + Request.Params["mode"]
                              + "&srch=" + Request.Params["srch"]
                              + "&idSUBPR=" + idSUBPR.ToString()
                              + "&idPR=" + idPR.ToString()
                              + "&idBR=" + idBRole.ToString() 
                              + "'>"
                              + nameBrole + "</a></td>"
                              + "<td>" + descrBrole + "</td>"
                              + "<td>" + typeBrole + "</td>"
                              + "<td>"
                              + ((surname != "") ? (surname.ToString() + ", " + firstname.ToString()) : "")
                              + "</td>"
                              + "</tr>");
            }

            BUFFER.Append("</table>");


            return BUFFER.ToString();
        }





        public string BuildWhereClause()
        {
            if (Request.Params["mode"] == "owner")
            {
                return "BROWN.c_u_EID LIKE '" + Session["RAFLOGINbusOwnerEID"] + "' AND ";
            }
            else
            {
                return " "; 
            }
        }









        protected void TXTrolenamesrch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
