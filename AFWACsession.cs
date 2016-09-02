/*
To control which user and subprocess is used for AUTO-LOGIN during development,
open AFWACpage.cs
 */
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RBSR_AUFW.DB.IEntAssignmentSet;
using RBSR_AUFW.DB.IUser;
using RBSR_AUFW.DB.ITcodeAssignmentSet;
using RBSR_AUFW.DB.IEventLog;
using RBSR_AUFW.DB.ISubProcess;
using RBSR_AUFW.DB.IProcess;

namespace _6MAR_WebApplication
{

  [Serializable]
  public class AFWACsession
  {
    public DateTime creationTime;
    public string username = null;
    public int idUser = 1;


    // Dynamic state that changes as subpr changes
    public int    idSubprocess = -1;
    public string nameProcess = null;
    public string nameSubprocess = null;
    //
    // Bus-role workspace
    public int    idWorkspace = -1;  // active workspace for cur subpr even if not owned by user
    public bool   isWorkspaceOwner = false;
    public int    idUserWorkspaceOwner = -1;
    public string nameUserWorkspaceOwner;
    public string commentWorkspace;
    public int idActiveEAset = -1;

    //
    // SAP workspace
    public int    idWorkspace_SAP = -1;  // active workspace for cur subpr even if not owned by user
    public bool   isWorkspaceOwner_SAP = false;
    public int    idUserWorkspaceOwner_SAP = -1;
    public string nameUserWorkspaceOwner_SAP;
    // SAP Tcode Assignment set
    public int    idActiveTASS_SAP = -1;


    // Highly dynamic state during editing activities
    public int idRole = -1;
    public int idAppl = -1;

    public string strIPaddr;


    public AFWACsession(HttpRequest req)
    {
      creationTime = new DateTime();
	if (req != null)
	    strIPaddr = req.ServerVariables["REMOTE_ADDR"];
    }



      public void LOG
          (string objtype, string action, int objID, string detail1, string detail2)
      {
          IEventLog engine = new IEventLog(HELPERS.NewOdbcConn_FORCE());
          int babylogentry =
              engine.NewEventLog
                   (DateTime.Now, this.idUser, this.strIPaddr, 
                   action, objtype);
          engine.SetEventLog
          (babylogentry,
               DateTime.Now, this.idUser, this.strIPaddr, 
                   action, objID, detail1, detail2, objtype);
      }






    /*
      Assumes the subprocess info is already in place
    */
    public void ObtainWorkspaceContext() {

      System.Data.Odbc.OdbcConnection conn = HELPERS.NewOdbcConn();
      IEntAssignmentSet engineWS = new IEntAssignmentSet(conn);


      returnListEntAssignmentSetBySubProcess[] listWS;

      if (this.nameSubprocess == null)
      {
          ISubProcess Ispr = new ISubProcess(conn);
          returnGetSubProcess ret = Ispr.GetSubProcess(idSubprocess);
          this.nameSubprocess = ret.Name;
          IProcess Ipr = new IProcess(conn);
          returnGetProcess ret2 = Ipr.GetProcess(ret.ProcessID);
          this.nameProcess = ret2.Name;
      }

      // Is there an active "live" easet for this subpr?
      listWS = 
        engineWS.ListEntAssignmentSetBySubProcess
        (null, "\"Status\" = ?", new string[]{"ACTIVE"}, "", idSubprocess);

      if (listWS.Length > 1)
        {
          throw new Exception("Internal error: more than one ACTIVE Ent Assignment Set for subprocess "
                              + idSubprocess);
        }

        if (listWS.Length == 1)
        {
            idActiveEAset = listWS[0].ID;
        }





      // Is there a currently active ed workspace for this subprocess?

      listWS = 
        engineWS.ListEntAssignmentSetBySubProcess
        (null, "\"Status\" = ?", new string[]{"WORKSPACE"}, "", idSubprocess);

      if (listWS.Length > 1)
        {
          throw new Exception("Internal error: more than one workspace for subprocess "
                              + idSubprocess);
        }

      if (listWS.Length == 1)
        {
          idWorkspace = listWS[0].ID;
          commentWorkspace = listWS[0].Commentary;
          idUserWorkspaceOwner = listWS[0].UserID;
          isWorkspaceOwner = (idUser == listWS[0].UserID);
          if (isWorkspaceOwner)
            {
              nameUserWorkspaceOwner = username;
            }
          else
            {
              IUser engineUser = new IUser(conn);
              returnGetUser wsowner = engineUser.GetUser(idUserWorkspaceOwner);
              nameUserWorkspaceOwner =
                wsowner.Name;
            }
        }
      else
        {
          idWorkspace = -1;
          idUserWorkspaceOwner = -1;
          isWorkspaceOwner = false;
          nameUserWorkspaceOwner = "";
          commentWorkspace = "";
        }

    }







    /*
      Assumes the subprocess info is already in place
    */
    public void ObtainWorkspaceContext_SAP() {

      System.Data.Odbc.OdbcConnection conn = HELPERS.NewOdbcConn();
      ITcodeAssignmentSet engineWS = new ITcodeAssignmentSet(conn);


      // Is there a currently active ed workspace for this subprocess?
      returnListTcodeAssignmentSetBySubProcess[] listWS = 
        engineWS.ListTcodeAssignmentSetBySubProcess
        (null, "\"Status\" = ?", new string[]{"WORKSPACE"}, "", idSubprocess);

      if (listWS.Length > 1)
        {
          throw new Exception("Internal error: more than one workspace for subprocess "
                              + idSubprocess);
        }

      if (listWS.Length == 1)
        {
          idWorkspace_SAP = listWS[0].ID;
          idUserWorkspaceOwner_SAP = listWS[0].UserID;
          isWorkspaceOwner_SAP = (idUser == listWS[0].UserID);
          if (isWorkspaceOwner_SAP)
            {
              nameUserWorkspaceOwner_SAP = username;
            }
          else
            {
              IUser engineUser = new IUser(conn);
              returnGetUser wsowner = engineUser.GetUser(idUserWorkspaceOwner_SAP);
              nameUserWorkspaceOwner_SAP = wsowner.Name;
            }
        }
      else
        {
          idWorkspace_SAP = -1;
          idUserWorkspaceOwner_SAP = -1;
          isWorkspaceOwner_SAP = false;
          nameUserWorkspaceOwner_SAP = "";
        }





      // Is there a currently active TASS (live) for this subprocess?
      returnListTcodeAssignmentSetBySubProcess[] listSAPWS = 
        engineWS.ListTcodeAssignmentSetBySubProcess
        (null, "\"Status\" = ?", new string[]{"ACTIVE"}, "", idSubprocess);

      if (listSAPWS.Length > 1)
        {
          throw new Exception("Internal error: more than one ACTIVE TcodeAssignmentSet for subprocess "
                              + idSubprocess);
        }

        if (listSAPWS.Length == 1)
        {
            idActiveTASS_SAP = listSAPWS[0].ID;
        }
      else
        {
          idActiveTASS_SAP = -1;
        }
    }




  }
}
