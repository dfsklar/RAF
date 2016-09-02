using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using RBSR_AUFW.DB.IEntitlement;
using RBSR_AUFW.DB.IEntAssignment;
using System.Data.Odbc;
using System.Web.SessionState;
using System.Collections.Generic;



namespace _6MAR_WebApplication.GuidedEditor
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler1 : IHttpHandler, IReadOnlySessionState
  {

    public void ProcessRequest(HttpContext context)
    {

      AFWACsession session = context.Session["AFWACSESSION"] as AFWACsession;

      context.Response.ContentType = "text/plain";

      System.Web.Script.Serialization.JavaScriptSerializer UTIL =
        new System.Web.Script.Serialization.JavaScriptSerializer();

      // entids  (JSON array)
      // newstat

      string JSONidsOfSelRows = (context.Request.Params["entids"]);

      string newval = (context.Request.Params["newstat"]);
      newval = newval.Substring(0, 1);  //just first char is the status code (P,A,I,X,etc.)

      string oldval = (context.Request.Params["oldstat"]);

      Object OBJarrofids = UTIL.DeserializeObject(JSONidsOfSelRows);
      Array arrofids = OBJarrofids as Array;


            
      // VALIDATIONS:

      // If trying to go from "A" to "X", the goal is to actually instantly
      // automatically uninstall the entitlement from any ACTIVE subprocess entset.  
      // If the active entset is not occluded by a workspace, a workspace is
      // autoconstructed and edited and saved to become active.

      /*
       * 
       * For any subprocess that needs this done:
       *   If no workspace active, new workspace created and edited but kept open.
       *   If workspace active and I am the owner, the edit is made but WS kept open.
       *   If workspace active and I am not the owner, WARNING issued that includes
       *      the subpr name and the identity of the owner.
       *   
       * This requires the list of subpr (opening screen) to have a column specifying
       * whether there is a WS open and who owns it, so we can easily get to the
       * ones that need it closing.
       * 
       * 
       */




      // IEntitlement engine = new IEntitlement(HELPERS.NewOdbcConn());
      for (int i = 0; i < arrofids.Length; i++)
        {
          int curRowID = int.Parse(arrofids.GetValue(i) as string);

          if (newval == "X")
            {
              AutoRemoveEntitlementStatus(curRowID, session.idUser,
                                          context.Response);
            }

          HELPERS.RunSql(
                         "UPDATE t_RBSR_AUFW_u_Entitlement " +
                         "   SET c_u_Status = '" + newval + "' " +
                         "   WHERE c_id = " + curRowID + ";");

        }

    }









    private void AutoRemoveEntitlementStatus
      (int idEntit, int idThisUser, HttpResponse theResponse)
    {
      // Get complete info about this entitlement
      IEntitlement engine = new IEntitlement(HELPERS.NewOdbcConn());
      returnGetEntitlement thisEnt = engine.GetEntitlement(idEntit);

      // Find every entAssignment that refers to this entitlement
      // and that is in an ACTIVE entset or a WORKSPACE entset.
      // Requires a JOIN
      string sqlFindEA =
        "SELECT EA.c_id, EAS.c_r_SubProcess as EASsubpr, EAS.c_id as EASid, EAS.c_u_Status as EASstatus, EAS.c_r_User as EASiduser, " + 
        " USR.c_u_Name as EASnameuser, PR.c_u_Name + ' / ' + SUBPR.c_u_Name as SUBPRname, EA.c_r_Entitlement as EAentit, " + 
        " BROLE.c_u_Name as BRname " +
        " FROM t_RBSR_AUFW_u_EntAssignment EA" +
        " LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignmentSet EAS ON EAS.c_id = EA.c_r_EntAssignmentSet " +
        " LEFT OUTER JOIN t_RBSR_AUFW_u_Subprocess SUBPR ON SUBPR.c_id = EAS.c_r_SubProcess " +
        " LEFT OUTER JOIN t_RBSR_AUFW_u_Process PR ON PR.c_id = SUBPR.c_r_Process " +
        " LEFT OUTER JOIN t_RBSR_AUFW_u_User USR ON USR.c_id = EAS.c_r_User " +
        " LEFT OUTER JOIN t_RBSR_AUFW_u_BusRole BROLE ON BROLE.c_id = EA.c_r_BusRole " +
        " WHERE EAS.c_u_Status IN ('WORKSPACE','ACTIVE') AND EA.c_r_Entitlement=" + idEntit;
      OdbcDataReader reader = HELPERS.RunSqlSelect(sqlFindEA);

      Dictionary<int, HELPERS.infoEASet> MAPsubprToEASet =
        new Dictionary<int, HELPERS.infoEASet>();



      Queue queueOfEA = new Queue();
      while (reader.Read()) {
          HELPERS.infoEA baby = new HELPERS.infoEA();
        baby.idEntAss = reader.GetInt32(0);
        baby.idSubpr = reader.GetInt32(1);
        baby.idEntAssSet = reader.GetInt32(2);
        baby.strEntAssSetStatus = reader.GetString(3);
        baby.idEntAssSetCreator = reader.GetInt32(4);
        baby.nameEntAssSetCreator = reader.GetString(5);
        baby.nameSubprocess = reader.GetString(6);
        baby.idEntitlement = reader.GetInt32(7);
        baby.nameBusRole = reader.GetString(8);
        queueOfEA.Enqueue(baby);

        // An existing workspace OVERRULES an ACTIVE EASet.
        if ( ! MAPsubprToEASet.ContainsKey(baby.idSubpr))
          {
              HELPERS.infoEASet infoeaset = new HELPERS.infoEASet();
            infoeaset.idEntAssSet = baby.idEntAssSet;
            infoeaset.strEntAssSetStatus = baby.strEntAssSetStatus;
            infoeaset.idEntAssSetCreator = baby.idEntAssSetCreator;
            infoeaset.nameEntAssSetCreator = baby.nameEntAssSetCreator;
            infoeaset.nameSubprocess = baby.nameSubprocess;
            MAPsubprToEASet.Add(baby.idSubpr, infoeaset);
          }
        else
          {
            if (MAPsubprToEASet[baby.idSubpr].idEntAssSet == baby.idEntAssSet)
              {
                // This is another copy of the same registered EASet.
                // Ignore!
              }
            else if (MAPsubprToEASet[baby.idSubpr].strEntAssSetStatus == "WORKSPACE")
              {
                // The registered EASet for this subprocess is already a workspace.
                // This newcomer must be the ACTIVE easet and we are not interested.
                if (baby.strEntAssSetStatus != "ACTIVE")
                  {
                    throw new Exception("DATABASE ERROR: Multiple workspaces are active for subprocess #" + 
                                        baby.idSubpr + "(" + baby.nameSubprocess + ")");
                  }
              }
            else
              {
                if (baby.strEntAssSetStatus != "WORKSPACE")
                  {
                    throw new Exception("DATABASE ERROR: Multiple EASets are marked as 'ACTIVE' for subprocess #" + 
                                        baby.idSubpr + "(" + baby.nameSubprocess + ")");
                  }
                  HELPERS.infoEASet infoeaset = new HELPERS.infoEASet();
                infoeaset.idEntAssSet = baby.idEntAssSet;
                infoeaset.strEntAssSetStatus = baby.strEntAssSetStatus;
                infoeaset.idEntAssSetCreator = baby.idEntAssSetCreator;
                infoeaset.nameEntAssSetCreator = baby.nameEntAssSetCreator;
                MAPsubprToEASet[baby.idSubpr] = infoeaset;
              }
          }
      }
      reader.Dispose();


      // We now have the list of all subprocesses involved, and info about
      // either the ACTIVE or WORKSPACE EASet to be actually edited.
           
      // We now want to build any needed workspaces, i.e. if we are looking
      // at an EASet of status "ACTIVE".
      //
      // CAREFUL!!! Before 13 July, there was a BUG here.  It was possible to have
      // the EASet be an ACTIVE one (if the workspace for that subpr simply didn't
        // have any ref to the target entitlement), which would cause construction
        // of an unneeded workspace.
        // BEFORE CONSTRUCTING ANY WORKSPACE, AN EXTRA CHECK MUST BE PERFORMED
        // TO ENSURE THERE IS NOT ALREADY A WS FOR THAT SUBPR!!!!

      Dictionary<int, HELPERS.infoEASet> readyToUseWorkspaces = new Dictionary<int, HELPERS.infoEASet>();
      Dictionary<int, HELPERS.infoEASet> lockedWorkspaces = new Dictionary<int, HELPERS.infoEASet>();
      String messages = "";
      HELPERS.AutoGenWorkspacesInBulk(idThisUser, ref messages, MAPsubprToEASet, readyToUseWorkspaces, lockedWorkspaces,
          "facilitate retirement of an entitlement");
      theResponse.Write(messages);
            
      // We are now ready to do the processing of the EAssignments.
      // There are two situations.
      // If the EA's own status is "N", the proper thing to do is to delete the EA entirely.
      // If the EA's status is "A", the proper thing to do is turn it to "X".
      // No change should be made of course if it's already "X".
      // Running these updates multiple times is no problem, as it will turn into a no-op
      //   after the first time.
      //
      // ON SECOND THOUGHT: I want to turn even status "N" to "X" because that is self-documenting
      // and better for auditing.  We want it to appear with a verboten sign in the designer view.

        Dictionary<int, int> MAPsubprToNumAffected = new Dictionary<int, int>();

      while (queueOfEA.Count > 0)
        {
            HELPERS.infoEA curEA = (HELPERS.infoEA)(queueOfEA.Dequeue());
          int idSubpr = curEA.idSubpr;
          if (MAPsubprToNumAffected.ContainsKey(idSubpr)) {
              continue;
          }
          if (readyToUseWorkspaces.ContainsKey(idSubpr))
            {
                HELPERS.infoEASet curWS = readyToUseWorkspaces[idSubpr];

	      // Handling status="A" and "N"
              string cmdsql =
                "UPDATE t_RBSR_AUFW_u_EntAssignment " +
                " SET c_u_Status='X' WHERE c_u_Status IN ('A','N','P') AND " +
                " c_r_EntAssignmentSet=" + curWS.idEntAssSet + " AND " +
                " c_r_Entitlement=" + curEA.idEntitlement;
              int resultRunSql = HELPERS.RunSql(cmdsql);
              MAPsubprToNumAffected[idSubpr] = resultRunSql;
              theResponse.Write(
                  "OK: " + curEA.nameSubprocess + ": " + resultRunSql + " business roles affected.\n");
            }
          else
            {
                HELPERS.infoEASet lockedWS = lockedWorkspaces[idSubpr];
              theResponse.Write(
                                "<B>ERROR: </B> Entitlement #" + curEA.idEntitlement + " could not be removed from " + curEA.nameBusRole +
                                " -- workspace locked by " + lockedWS.nameEntAssSetCreator + ".\n");
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
