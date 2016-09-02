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
using RBSR_AUFW.DB.IBusRoleOwner;
using RBSR_AUFW.DB.IUser;
using RBSR_AUFW.DB.IMETADATA_SubprToActivityList;
using RBSR_AUFW.DB.IAdditionalBusRole;
using RBSR_AUFW.DB.IEntAssignmentSet;
using RBSR_AUFW.DB.IEntAssignment;
using System.Data.Odbc;
using RBSR_AUFW.DB.ITcodeAssignmentSet;
using RBSR_AUFW.DB.IFuncApplNotes;


namespace _6MAR_WebApplication.viewer
{
  public partial class UIPAGE_DETAILbusrole : System.Web.UI.Page
  {

    public returnGetProcess theProcess;
    public returnGetSubProcess theSubProcess;
    public returnGetBusRole theBR;
    public int idBR;




              override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }




      private void InitializeComponent()
      {
          this.Load += new System.EventHandler(this.Page_Load);


          if (this.GRIDsapdetails != null) 
              this.GRIDsapdetails.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(OnNeedRebind);

      }


      public void OnNeedRebind(object sender, EventArgs oArgs)
      {
          OdbcDataReader DR;
          string msg;

          if (!PANEL_tcodelistingmode_interactive.Visible)
              return;



          DR = ExtractSAPdataForDisplay(out msg);

          if (DR != null)
          {

              // Do the query, then bind the dataset
              DataSet DSET = new DataSet();
              DataTable TAB = DSET.Tables.Add();
              TAB.Columns.Add("c_id");
              TAB.Columns.Add("RoleName");
              TAB.Columns.Add("Platform");
              TAB.Columns.Add("TCode");
              TAB.Columns.Add("Description");
              TAB.Columns.Add("ActiveFolder");
              TAB.Columns.Add("Access");
             

              while (DR.Read())
              {
                  TAB.Rows.Add(new string[] { SafeDRGetString(DR, 10),
                       SafeDRGetString(DR, 3),
                 SafeDRGetString(DR, 2),
                   SafeDRGetString(DR, 0),
                   SafeDRGetString(DR, 4),
                   SafeDRGetString(DR, 8),
                   SafeDRGetString(DR, 9)});
              }

              /*
        BUFFER.Append("<tr>");
        BUFFER.Append("<td>" + SafeDRGetString(DR,3) + "</td>"); //SAP role name
        BUFFER.Append("<td>" + SafeDRGetString(DR, 2) + "</td>"); // PLATFORM
        BUFFER.Append("<td>" + SafeDRGetString(DR, 0) + "</td>"); // TCode
        BUFFER.Append("<td>" + SafeDRGetString(DR, 4) + "</td>"); // TCode Description
        BUFFER.Append("<td>" + SafeDRGetString(DR, 8) + "</td>"); // Activity Folder
        BUFFER.Append("<td>" + SafeDRGetString(DR, 9) + "</td>"); // AccessLevel
               * field #10 is next, with the C-ID of the assignment object
               * */

              GRIDsapdetails.DataSource = DSET;
              GRIDsapdetails.DataBind();
          }

      }




      string sapviewmode = "I";  //interactive



      protected void Page_Load(object sender, EventArgs e)
      {
          try
          {
              idBR = int.Parse(Request.Params["idBR"]);
          }
          catch (Exception) { idBR = -432; }


          // Special viewing mode param added in May of 2010:
          //     sapview=interactive   or   html
          // The default is the former
          sapviewmode = "I";
          try
          {
              string mode = Request.Params["sapview"];
              sapviewmode = mode.ToUpper().Substring(0, 1);
          }
          catch (Exception) { }


          if (PANEL_tcodelistingmode_HtmlTable != null)
          {
              switch (sapviewmode)
              {
                  case "I":
                      PANEL_tcodelistingmode_HtmlTable.Visible = false;
                      break;
                  case "H":
                      PANEL_tcodelistingmode_interactive.Visible = false;
                      break;
              }
          }




          IProcess engineProcess = new IProcess(HELPERS.NewOdbcConn());
          ISubProcess engineSubProcess = new ISubProcess(HELPERS.NewOdbcConn());
          IBusRole engineBR = new IBusRole(HELPERS.NewOdbcConn());

          theBR = engineBR.GetBusRole(idBR);
          theSubProcess = engineSubProcess.GetSubProcess(theBR.SubProcessID);
          theProcess = engineProcess.GetProcess(theSubProcess.ProcessID);

          // Initialize dynamically-generated portions of the page
          if (PANELsapdesignnote != null)
          {
              IFuncApplNotes engine = new IFuncApplNotes(HELPERS.NewOdbcConn());
              returnListFuncApplNotes[] returned =
                engine.ListFuncApplNotes
                  (null, "\"REFapplication\" = ?  AND  \"BusRole\" = ?" /* SAP application is ID 57 */ ,
                  new string[] { "57", idBR.ToString() }, "");
              if (returned.Length == 0)
              {
                  PANELsapdesignnote.Visible = false;
              }
              else
              {
                  STATICTXTsapfuncappdesignnote.Text =
                      returned[0].Comment;
              }
          }
      }
         
   
    public string nameOfProcess()
    {
      return this.theProcess.Name;
    }

    public string nameOfSubProcess()
    {
      return this.theSubProcess.Name;
    }

    public string nameOfRole()
    {
      return this.theBR.Name;
    }
    public string descrOfRole()
    {
      return this.theBR.Description;
    }



    public string rowsOwnersApprovers()
    {
      StringBuilder BUFFER = new StringBuilder();
      IBusRoleOwner engine = new IBusRoleOwner(HELPERS.NewOdbcConn());
      IUser DBuser = new IUser(HELPERS.NewOdbcConn());

      returnListBusRoleOwnerByBusRole[] listowners
        = engine.ListBusRoleOwnerByBusRole(null, idBR);

      foreach (returnListBusRoleOwnerByBusRole cur in listowners)
        {
          returnListUser[] theUser =
            DBuser.ListUser(null, "\"EID\" LIKE ?", new string[] { cur.EID }, "");
          BUFFER.Append("<tr><td>" + cur.RankFriendly + "</td>");
          BUFFER.Append("    <td>");
          if (theUser.Length == 1)
            {
              BUFFER.Append(theUser[0].NameFirst + " " + theUser[0].NameSurname);
            }
          BUFFER.Append("</td>\n");
          BUFFER.Append("<td>" + cur.EID + "</td>");
          BUFFER.Append("</tr>\n");
        }
      return BUFFER.ToString();
    }


    public string ROWSadditionalBusinessRoles()
    {
      IAdditionalBusRole engine = new IAdditionalBusRole(HELPERS.NewOdbcConn());
      IBusRole engineBR = new IBusRole(HELPERS.NewOdbcConn());
      returnListAdditionalBusRoleByBusRole[] list = 
        engine.ListAdditionalBusRoleByBusRole(null, idBR);

      StringBuilder BUFFER = new StringBuilder();

      foreach (returnListAdditionalBusRoleByBusRole cur in list)
        {
          returnGetBusRole detail = engineBR.GetBusRole(cur.idAdditionalBusRole);
          if (!detail.Name.Contains("//DEL_"))
          {
              BUFFER.Append("<tr>");
              BUFFER.Append("<td>" + detail.RoleType_Displayable + "</td>");
              BUFFER.Append("<td><a href='DETAILbusrole.aspx?idBR=" + detail.ID + "'>" + detail.Name + "</a></td>");
              BUFFER.Append("<td>" + detail.Description + "</td>");
              BUFFER.Append("</tr>");
          }
        }
      return BUFFER.ToString();

    }







        
        
        
        
    public string ROWStcodes()
    {
        if (PANEL_tcodelistingmode_HtmlTable.Visible == false)
            return "-" ;


        OdbcDataReader DR;
        string msg;
      StringBuilder BUFFER = new StringBuilder();

       DR =  ExtractSAPdataForDisplay(out msg);

       if (DR == null)
       {
        
           return msg;
       }


      while (DR.Read()) {
        BUFFER.Append("<tr>");
        BUFFER.Append("<td>" + SafeDRGetString(DR,3) + "</td>"); //SAP role name
        BUFFER.Append("<td>" + SafeDRGetString(DR, 2) + "</td>"); // PLATFORM
        BUFFER.Append("<td>" + SafeDRGetString(DR, 0) + "</td>"); // TCode
        BUFFER.Append("<td>" + SafeDRGetString(DR, 4) + "</td>"); // TCode Description
        BUFFER.Append("<td>" + SafeDRGetString(DR, 8) + "</td>"); // Activity Folder
        BUFFER.Append("<td>" + SafeDRGetString(DR, 9) + "</td>"); // AccessLevel
	//
	// DEBUG:
          /*
        BUFFER.Append("<td>" + DR.GetInt32(10).ToString() + "</td>"); // 
        BUFFER.Append("<td>" + DR.GetInt32(11).ToString() + "</td>"); // 
        BUFFER.Append("<td>" + DR.GetInt32(12).ToString() + "</td>"); // 
           */
        //
        BUFFER.Append("</tr>");
      }

      /*
        IEntAssignment engineEA = new IEntAssignment(HELPERS.NewOdbcConn());
        engineEA.ListEntAssignmentByEntAssignmentSet
        (null, "\"Application\" = ?", new string[]{"SAP"}, "EntitlementVector", 
      */

      return BUFFER.ToString();
    }






      private OdbcDataReader ExtractSAPdataForDisplay(out string msg)
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
              msg = "There are no SAP entitlements to display for this subprocess.";
              return null; 
          }
          int theActiveEASetID = ret[0].ID;


          // 1(b). Find the ID of the ACTIVE TCode entitlement set for this subprocess.
          ITcodeAssignmentSet engineTCSet =
            new ITcodeAssignmentSet(HELPERS.NewOdbcConn());
          returnListTcodeAssignmentSetBySubProcess[] TCret =
            engineTCSet.ListTcodeAssignmentSetBySubProcess
            (null, "\"Status\" = ? AND \"SubProcess\"=?", new string[] { "ACTIVE", theSubProcess.ID.ToString() }, "", theSubProcess.ID);
          if (TCret.Length > 1)
          {
              throw new Exception("Database error: more than one ACTIVE TCode entitlement set for this subprocess");
          }
          if (TCret.Length < 1)
          {
              msg =  "There are no published TCode entitlement sets for this subprocess, so there are no results to report here.";
              return null;
          }
          int theActiveTCodeSetID = TCret[0].ID;


          // 2. Grab all of the SAP-application entitlements in that active set
          string thesql =
            @"
SELECT
TCENT.c_u_TCode, SAPROLE.c_u_System, SAPROLE.c_u_Platform, SAPROLE.c_u_Name as SAPROLENAME,
TDICT.c_u_Description as TCDESCR, 
TCA.c_u_EditStatus, EA.c_r_EntAssignmentSet, TCA.c_r_TcodeAssignmentSet,
TCENT.c_u_ActivityFolder, 
(SELECT SAL.Name FROM DICT_SAPentitlementAccessLevel SAL WHERE SAL.Abbrev=TCENT.c_u_AccessLevel) as AccLevel
,
TCA.c_id as TCAID,
TCENT.c_id as TCENTID,
SAPROLE.c_id as SAPROLEID

FROM t_RBSR_AUFW_u_EntAssignment EA

LEFT OUTER JOIN t_RBSR_AUFW_u_Entitlement ENT 
   ON ENT.c_id=EA.c_r_Entitlement 

LEFT OUTER JOIN t_RBSR_AUFW_u_SAProle SAPROLE 
   ON SAPROLE.c_u_Name = ENT.c_u_EntitlementValue

LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeAssignment TCA 
   ON TCA.c_r_SAProle = SAPROLE.c_id

LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeAssignmentSet TCASET 
   ON TCA.c_r_TcodeAssignmentSet = TCASET.c_id

LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeEntitlement TCENT
   ON TCA.c_r_TcodeEntitlement = TCENT.c_id

LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeDictionary TDICT  ON TDICT.c_u_TcodeID = TCENT.c_u_TCode

WHERE 
EA.c_u_Status in ('N','A') 
AND
ENT.c_u_Application='SAP'
AND
ENT.c_u_EntitlementValue = SAPROLE.c_u_Name
AND
ENT.c_u_Platform = SAPROLE.c_u_Platform
AND 
((TCA.c_u_EditStatus & 4) = 0)
AND
EA.c_r_BusRole = " + this.theBR.ID + @"
AND
EA.c_r_EntAssignmentSet = "
            +
            theActiveEASetID
            + @"
AND 
TCASET.c_u_Status = 'ACTIVE' "
            + " ORDER BY SAPROLENAME, c_u_System, c_u_TCode;";

          OdbcDataReader DR;

          DR = HELPERS.RunSqlSelect(thesql);

          msg = "";
          return DR;
      }







    public string ActiveBusinessEntitlements()
    {
      IMETADATA_SubprToActivityList engineactlist =
        new IMETADATA_SubprToActivityList(HELPERS.NewOdbcConn());

      returnListMETADATA_SubprToActivityListBySubProcess[] actlist =
        engineactlist.ListMETADATA_SubprToActivityListBySubProcess
        (null, theSubProcess.ID);

      StringBuilder BUFFER = new StringBuilder();

      foreach (returnListMETADATA_SubprToActivityListBySubProcess curact in actlist)
        {
          StringBuilder PREBUFFER = new StringBuilder();
          bool hasContent = false;
          PREBUFFER.Append("<tr class='textalignleft'>");
          if (curact.Text.Trim().Length > 0)
            {
              hasContent = true;
            }
          if (curact.NodeType == "HEAD")
            {
              PREBUFFER.Append("<td colspan=3 class='ActivityListHeader'>" + curact.Text + "</td>");
            }
          else
            {
                string flag = "<IMG src='../images/IsHead_false__IsKeyPoint_false.gif' width='12'/> ";
              if ((bool) (curact.BOOLisKeyPoint))
                {
                    flag = "<IMG src='../images/IsHead_false__IsKeyPoint_true.gif' width='12'/> ";
                }
              PREBUFFER.Append("<td>" + flag + curact.Text + "&nbsp;</td>");
              PREBUFFER.Append("<td><ol>");
              PREBUFFER.Append(HELPERS.HumanReadableRoleList(curact.ListIdsBusRoles, "<li>", "</li>"));
              PREBUFFER.Append("</ol>&nbsp;</td>");
              PREBUFFER.Append("<td><ol>");
              PREBUFFER.Append(HELPERS.HumanReadableAppList(curact.ListIdsApps, "<li>", "</li>"));
              PREBUFFER.Append("</ol>&nbsp;</td>");
              if (curact.ListIdsBusRoles.Length > 1)
                {
                  hasContent = true;
                }
              if (curact.ListIdsApps.Length > 1)
                {
                  hasContent = true;
                }
            }
          PREBUFFER.Append("</tr>");
          if (hasContent)
            {
              BUFFER.Append(PREBUFFER.ToString());
            }
        }
      return BUFFER.ToString();
    }




      static string SafeDRGetString(OdbcDataReader DR, int idx)
      {
          try
          {
              return DR.GetString(idx);
          }
          catch (Exception)
          {
              return "";
          }
      }

  }
}
