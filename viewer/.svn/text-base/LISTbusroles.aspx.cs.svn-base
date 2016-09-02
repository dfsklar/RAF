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
using RBSR_AUFW.DB.IProcess;
using RBSR_AUFW.DB.ISubProcess;
using RBSR_AUFW.DB.IBusRole;
using System.Text;
using System.Data.Odbc;
using ComponentArt.Web.UI;
using RBSR_AUFW.DB.IEntAssignmentSet;

namespace _6MAR_WebApplication.viewer
{
  public partial class WebForm11 : System.Web.UI.Page
  {

    int idPR = -1;



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Params["mode"] == "owner")
            {
                Response.Redirect("LISTbusroles_byOwner.aspx?mode=owner&srch=.");
                return;
            }
            if (Request.Params["mode"] == "search")
            {
                Response.Redirect("LISTbusroles_byOwner.aspx?mode=search&srch=" + 
                    HttpUtility.UrlEncode(Request.Params["srch"]));
                return;
            }
        }
        catch (Exception ignoreme) { }

      try
        {
          idPR = int.Parse(Request.Params["idPR"]);
        }
      catch (Exception ee)
        {
          idPR = -1;
        }


      string thesql = "SELECT c_id, c_u_Name, c_u_Description FROM [t_RBSR_AUFW_u_Process] WHERE c_u_Name NOT LIKE '%internal use%' AND c_u_Name NOT LIKE 'test' ORDER BY [c_u_Name]";
      OdbcDataReader DR = HELPERS.RunSqlSelect(thesql);
      while (DR.Read())
        {
          ComboBoxItem newitem = new ComboBoxItem();
	  newitem.Text = DR.GetString(1);
	  try
	  {
	      newitem.Text = DR.GetString(2);
	  }
	  catch (Exception) { }
          newitem.Value = DR.GetInt32(0).ToString();
          this.COMBOXchooseProcess.Items.Add(newitem);
        }

      if (!IsPostBack)
        if (idPR >= 0)
          {
            this.COMBOXchooseProcess.SelectedValue = idPR.ToString();
          }

    }





    public string RENDER()
    {
      int idproc = this.idPR;

      if (idproc < 0)
        return "";


      IProcess engineProcess = new IProcess(HELPERS.NewOdbcConn());
      ISubProcess engineSubProcess = new ISubProcess(HELPERS.NewOdbcConn());
      IBusRole engineBR = new IBusRole(HELPERS.NewOdbcConn());

      IEntAssignmentSet engineWS = new IEntAssignmentSet(HELPERS.NewOdbcConn());



      returnGetProcess detailProcess = engineProcess.GetProcess(idproc);

      StringBuilder BUFFER = new StringBuilder();


      /*
      BUFFER.Append(
                    "<tr><th colspan='2'>Process: " + detailProcess.Name + "</th></tr>");
      */

      returnListSubProcessByProcess[] subprocesses =
        engineSubProcess.ListSubProcessByProcess
        (null, 
         "\"Status\" = ?", new string[]{"Active"}, "\"NAME\"", idproc);

      foreach (returnListSubProcessByProcess cursubpr in subprocesses)
        {
            int idActiveEAset = -1;

          returnListEntAssignmentSetBySubProcess[] listWS;

            listWS =
    engineWS.ListEntAssignmentSetBySubProcess
    (null, "\"Status\" = ?", new string[] { "ACTIVE" }, "", cursubpr.ID);

            if (listWS.Length > 1)
            {
                throw new Exception("Internal error: more than one ACTIVE Ent Assignment Set for subprocess "
                                    + cursubpr.ID);
            }

            if (listWS.Length == 1)
            {
                idActiveEAset = listWS[0].ID;
            }


	  string sql = @"

SELECT
  BR.c_id, BR.c_u_Name, BR.c_u_Description,
  BR.c_u_RoleType as RoleTypeAbbrev, 
  (select Displayable from DICT_BusRoleType where Abbrev=c_u_RoleType) as RoleTypeDisplayable,

(
SELECT COUNT(DISTINCT ENT.c_u_Application)
   FROM t_RBSR_AUFW_u_EntAssignment EA
LEFT OUTER JOIN t_RBSR_AUFW_u_Entitlement ENT
   ON EA.c_r_Entitlement = ENT.c_id
WHERE
EA.c_r_BusRole = BR.c_id
AND
EA.c_r_EntAssignmentSet = " + idActiveEAset + @"
AND
EA.c_u_Status NOT IN ('X')
) as KOUNTAPPS,

USR.c_u_NameSurname, USR.c_u_NameFirst

FROM t_RBSR_AUFW_u_BusRole BR


LEFT OUTER JOIN 
t_RBSR_AUFW_u_BusRoleOwner BRPRIMOWN ON  BRPRIMOWN.c_r_BusRole=BR.c_id AND BRPRIMOWN.c_u_Rank = 'OWNprim'
LEFT OUTER JOIN 
t_RBSR_AUFW_u_User USR ON  BRPRIMOWN.c_u_EID = USR.c_u_EID


WHERE c_r_SubProcess=" + cursubpr.ID + " order by c_u_Name";


          BUFFER.Append("<tr><td><table class='subprocess'><caption>Subprocess: " 
                        + cursubpr.Name + @"</caption>
                                                                        <thead>
                                                                        <tr>
                                                                                <th scope='col' width='25%'>Role Name</th>
                                                                                <th scope='col' width='25%'>Role Description</th>
                                                                                <th scope='col' width='20%'>Role Type</th>
                                                                                <th scope='col' width='30%'>Primary Owner</th>
                                                                        </tr>
                                                                </thead><tbody>");

          OdbcDataReader DR = HELPERS.RunSqlSelect(sql);

          while (DR.Read())
          {
              int idBRole = DR.GetInt32(0);
              string name = DR.GetString(1);
              string descr = DR.GetString(2);
              string roletype = DR.GetString(4);
              int appcount = DR.GetInt32(5);
	      string surname = "";
	      string firstname = "";
	      try
		{
		  surname = HELPERS.SafeObjToString(DR.GetString(6));
		  firstname = HELPERS.SafeObjToString(DR.GetString(7));
		}
	      catch (Exception) { }
          
          
              BUFFER.Append("<tr><td><a href='DETAILbusrole.aspx?"
                            +  "idBR=" + idBRole
                            + "&idSUBPR=" + cursubpr.ID
                            + "&idPR=" + idproc
                            + "'>"
                            + name + "</a></td><td>" + descr
                            + "</td><td>" + roletype + "</td><td>"
			    + ((surname!="") ?  (surname.ToString()+", "+firstname.ToString())   :   "")
			    + "</td></tr>");
            }

          BUFFER.Append("</table></td></tr>\n");
        }



      returnGetSubProcess detailSubProcess;

      return BUFFER.ToString();
    }



    public void COMBOXchooseProcess_SelectedIndexChanged(Object sender, EventArgs e)
    {
      if (COMBOXchooseProcess.SelectedValue != null)
        {
          Session["STRcurAppScope"] = COMBOXchooseProcess.SelectedItem.Text;
          Session["COMBOIDXcurAppScope"] = COMBOXchooseProcess.SelectedIndex;
        }
      this.Response.Redirect("LISTbusroles.aspx?idPR=" + COMBOXchooseProcess.SelectedValue);
      return;

    }



  }
}
