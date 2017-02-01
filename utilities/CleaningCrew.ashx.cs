using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Web.SessionState;
using RBSR_AUFW.DB.IEntAssignmentSet;
using RBSR_AUFW.DB.ITcodeAssignmentSet;
using RBSR_AUFW.DB.IEntitlement;
using RBSR_AUFW.DB.IEntAssignment;

namespace _6MAR_WebApplication.utilities
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler1 : IHttpHandler, IRequiresSessionState
  {

    protected AFWACsession session;


    public void ProcessRequest(HttpContext context)
    {
      try
        {


            if (context.Session["AFWACSESSION"] == null)
            {
                throw new Exception("Must be in a legal R-AF session");
            }
            else
            {

                session = context.Session["AFWACSESSION"] as AFWACsession;
            }





          context.Response.ContentType = "text/plain";

          string cmd = (context.Request.Params["cmd"]);

          string arg1 = (context.Request.Params["arg1"]);

          switch (cmd)
            {

            case "FINDduplicateBEnts":
              FINDduplicateBEnts(context.Response);
              break;

          case "BEnts_Recompute_Checksums":
              BEnts_Recompute_Checksums(context.Response);
              break;

          case "BEnts_Recompute_PrivStrings":
              this.BEnts_Recompute_Privstring();
              break;


            case "WSdel":
              WSdel(context, int.Parse(arg1));
              break;



            case "SAPWSdel":
	      SAPWSdel(context, int.Parse(arg1));
	      break;


            case "SAPWSpurge":
              HELPERS.RunSql
                ("DELETE FROM t_RBSR_AUFW_u_TcodeAssignment WHERE c_r_TcodeAssignmentSet=" + arg1);
              HELPERS.RunSql
                ("DELETE FROM t_RBSR_AUFW_u_TcodeAssignmentSet WHERE c_u_Status='WORKSPACE' AND c_id=" + arg1);
              context.Response.Write("DONE.  The workspace has been completely deleted.");
              break;


            case "SUBPRpurge":
              HELPERS.RunSql
                ("DELETE FROM t_RBSR_AUFW_u_BusRole WHERE c_r_SubProcess=" + arg1);
              HELPERS.RunSql
                ("DELETE FROM t_RBSR_AUFW_u_SAProle WHERE c_r_SubProcess=" + arg1);
              context.Response.Write("DONE.  Any roles associated with that subprocess has been deleted and freed for reuse.");
              break;

            default:
              throw new Exception("Unknown command: " + cmd);
            }
        }

      catch (Exception megaexc)
        {
          context.Response.Write(megaexc.Message);
          context.Response.StatusCode = 500;
        }
    }






      private void BEnts_Recompute_Privstring()
      {
          IEntitlement ENGINE = new IEntitlement(HELPERS.NewOdbcConn());
          returnListEntitlement[] RET = ENGINE.ListEntitlement(null, "", new string[] { }, "");
          foreach (returnListEntitlement x in RET)
          {
              returnGetEntitlement cur = ENGINE.GetEntitlement(x.ID);
              string privstring = HELPERS.CalcManifestString(cur);
              ENGINE.SetEntitlementPrivstring(x.ID, privstring);
          }
      }


      private void BEnts_Recompute_Checksums(HttpResponse httpResponse)
      {
      OdbcDataReader r = HELPERS.RunSqlSelect
        (@"

SELECT *,
c_id as ENTID
FROM t_RBSR_AUFW_u_Entitlement AUFWENT
WHERE c_u_Application<>'SAP' AND    c_u_CHECKSUM NOT LIKE 'XXredund%'
ORDER BY AUFWENT.c_id

");


      while (r.Read())
        {

          string intraRowChecksum =
         
                          (string)r["c_u_CHECKSUM"]
                          ;



          string actualComputedChecksum =
                           HELPERS.ENTCHECKSUM
                           (
                            (r["c_u_StandardActivity"]),
                            (r["c_u_RoleType"]),
                            (r["c_u_Application"]),
                            (r["c_u_System"]),
                            (r["c_u_Platform"]),
                            (r["c_u_EntitlementName"]),
                            (r["c_u_EntitlementValue"]),
                            (r["c_u_AuthObjName"]),
                            (r["c_u_AuthObjValue"]),
                            (r["c_u_FieldSecName"]),
                            (r["c_u_FieldSecValue"]),
                            (r["c_u_Level4SecName"]),
                            (r["c_u_Level4SecValue"]));
          
          
          if (intraRowChecksum != actualComputedChecksum) {
              httpResponse.Write("UPDATE t_RBSR_AUFW_u_Entitlement SET c_u_CHECKSUM='" + actualComputedChecksum + "' WHERE c_id=" + r["c_id"] + ";\n");
          }
      }
      }



















    private  void WSdel(HttpContext context, int idEASet)
    {
      IEntAssignmentSet engine = new IEntAssignmentSet(HELPERS.NewOdbcConn());
      returnGetEntAssignmentSet props =  engine.GetEntAssignmentSet(idEASet);
      if (props.Status != "WORKSPACE")
        {
          throw new Exception("Only open workspaces may be deleted.  Archived or active entitlement sets may not be deleted.");
        }
      if (props.UserID != this.session.idUser)
        {
          throw new Exception("Only the owner has permission to delete a workspace.");
        }

      engine.SetEntAssignmentSet
        (idEASet, "deleted", DateTime.Now,
         props.Commentary, props.SubProcessID, props.UserID, props.DATETIMEbirth);

      if (idEASet == this.session.idWorkspace)
      {
          // Make sure this user is not still "logged in" to the now-dead workspace
          this.session.idWorkspace = -1;
          this.session.idUserWorkspaceOwner = -1;
      }
      context.Response.Write("OK\n");
    }





    private  void SAPWSdel(HttpContext context, int idEASet)
    {
        ITcodeAssignmentSet engine = new ITcodeAssignmentSet(HELPERS.NewOdbcConn());
      returnGetTcodeAssignmentSet props =  engine.GetTcodeAssignmentSet(idEASet);
      if (props.Status != "WORKSPACE")
        {
          throw new Exception("Only open workspaces may be deleted.  Archived or active entitlement sets may not be deleted.");
        }
      if (props.UserID != this.session.idUser)
        {
          throw new Exception("Only the owner has permission to delete a workspace.");
        }

      engine.SetTcodeAssignmentSet
        (idEASet, DateTime.Now,
         props.Commentary+" (DELETED)", props.SubProcessID, props.UserID, "deleted");

      if (idEASet == this.session.idWorkspace)
      {
          // Make sure this user is not still "logged in" to the now-dead workspace
          this.session.idWorkspace_SAP = -1;
          this.session.idUserWorkspaceOwner_SAP = -1;
      }
      context.Response.Write("OK\n");
    }













    private void FINDduplicateBEnts(HttpResponse response)
    {
      OdbcDataReader r = HELPERS.RunSqlSelect
        (@"


SELECT *,
                ISNULL([c_u_Application],'') + '|.|' + ISNULL([c_u_StandardActivity],'') + '|.|' +
        ISNULL([c_u_RoleType],'') + '|.|' +
        ISNULL([c_u_System],'') + '|.|' +
        ISNULL([c_u_Platform],'') + '|.|' +
        ISNULL([c_u_EntitlementName],'') + '|.|' +
        ISNULL([c_u_EntitlementValue],'') + '|.|' +
        ISNULL([c_u_AuthObjName],'') + '|.|' +
        ISNULL([c_u_AuthObjValue],'') + '|.|' +
        ISNULL([c_u_FieldSecName],'') + '|.|' +
        ISNULL([c_u_FieldSecValue],'') + '|.|' +
        ISNULL([c_u_Level4SecName],'') + '|.|' +
        ISNULL([c_u_Level4SecValue],'') as fingerprint,
c_id as ENTID,

(SELECT COUNT(*) FROM t_RBSR_AUFW_u_EntAssignment EA where EA.c_r_Entitlement=AUFWENT.c_id and EA.c_u_Status NOT IN ('X') ) as KOUNT
               FROM t_RBSR_AUFW_u_Entitlement AUFWENT  ORDER BY AUFWENT.c_id

");


        

      // maps a fingerprint to an entitlement UUID
      Dictionary<string, int> dict = new Dictionary<string, int>();


      // MAPs an ent UUID to its chksum
      Dictionary<int, string> MAPtochksumExtended = new Dictionary<int, string>();
      Dictionary<int, string> MAPtochksum = new Dictionary<int, string>();

      // maps a row's checksum to that row's  UUID
      Dictionary<string, int> MAPfromchksum = new Dictionary<string, int>();



      while (r.Read())
        {
          string fprint = r["fingerprint"] as string;

          MAPtochksumExtended.Add((int)r["c_id"],
                          (string)r["c_u_CHECKSUM"]
                          + "..." +
                          (string)r["c_u_Status"]
                          + "..." +
                          ((int)r["KOUNT"]).ToString()
                          );
          MAPtochksum.Add((int)r["c_id"],
                          (string)r["c_u_CHECKSUM"]);


          if (MAPfromchksum.ContainsKey( (string)(r["c_u_CHECKSUM"]) ) ) {
            // throw new Exception("\n\nFATAL ERROR: CHECKSUM FOUND IN MORE THAN ONE ROW\n\n");
          }else{
            MAPfromchksum.Add( (string)(r["c_u_CHECKSUM"]), (int)r["c_id"] );
          }

          if (dict.ContainsKey(fprint)) {
            int origuuid = dict[fprint];
            response.Write("--"+fprint+"\n--"+"OrigUUID   =" + origuuid.ToString() + " = " + MAPtochksumExtended[origuuid] + "\n");
            string actualValidChecksum = HELPERS.ENTCHECKSUM
                         (
                          (r["c_u_StandardActivity"]),
                          (r["c_u_RoleType"]),
                          (r["c_u_Application"]),
                          (r["c_u_System"]),
                          (r["c_u_Platform"]),
                          (r["c_u_EntitlementName"]),
                          (r["c_u_EntitlementValue"]),
                          (r["c_u_AuthObjName"]),
                          (r["c_u_AuthObjValue"]),
                          (r["c_u_FieldSecName"]),
                          (r["c_u_FieldSecValue"]),
                          (r["c_u_Level4SecName"]),
                          (r["c_u_Level4SecValue"]));


            response.Write("--" + "CulpritUUID=" + r["c_id"].ToString() + " = " +
                           (r["c_u_CHECKSUM"].ToString()) + "..." + (string)(r["c_u_Status"])
                           + "..." +
                           ((int)r["KOUNT"]).ToString()
                           
                           + "\n" +
                           "--  Actual valid checksum: " + actualValidChecksum
                            +
                           "\n");

            if ( MAPtochksum[origuuid].Equals(r["c_u_CHECKSUM"].ToString()) )
            {
                response.Write
                ("UPDATE t_RBSR_AUFW_u_Entitlement SET c_u_CHECKSUM='XXredundXX_" + r["c_u_CHECKSUM"] + "' WHERE c_id = " + origuuid.ToString() + ";\n\n\n");
            }
            else
            {
                response.Write("--  ( no need for any checksum correction; they already differ )\n\n\n");
            }
          }
          else {
            dict.Add(fprint, (int)r["c_id"]);
          }
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
