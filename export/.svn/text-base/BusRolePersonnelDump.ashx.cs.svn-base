using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;

namespace _6MAR_WebApplication.export
{
    /// <summary>
    /// Summary description for Handler6
    /// </summary>
    public class Handler6 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition",
                                                "filename=raf_busrolepersonnel_mappings_complete.csv;attachment");


            OdbcCommand cmd = new OdbcCommand();
            cmd.Connection = HELPERS.NewOdbcConn();

            // IF YOU MAKE ANY CHANGE TO THE NUMBER OF FIELDS BEING DISPLAYED,
            // BE SURE TO CHANGE THE BELOW FOR LOOP SO IT KNOWS HOW MANY COLUMNS TO DISPLAY.
            cmd.CommandText =
    @"
select  
BROLE.c_u_Name,
BROTYPE.ForDisplay,
BROWN.c_u_EID, 
BROWN.c_u_Geography,
UUSER.c_u_NameSurname,
UUSER.c_u_NameFirst,
UUSER.c_u_Name

FROM

t_RBSR_AUFW_u_BusRoleOwner BROWN

left outer join t_RBSR_AUFW_u_User UUSER
   on  BROWN.c_u_EID = UUSER.c_u_EID

left outer join t_RBSR_AUFW_u_BusRole BROLE
   on  BROWN.c_r_BusRole = BROLE.c_id

left outer join DICT_RoleOwnerType BROTYPE
   on  BROWN.c_u_Rank = BROTYPE.Abbrev

WHERE BROLE.c_u_Name NOT LIKE '%//DEL%'

order by BROLE.c_u_Name, BROTYPE.Abbrev, BROWN.c_u_EID
";


            context.Response.Write("RoleName,Rank,EID,Geography,Surname,FirstName,RAFusername\n");


            OdbcDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {
                for (int i = 0; i < 7; i++)
                {
                    context.Response.Write(CSVquoteize(dr.GetValue(i).ToString()) + ",");
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