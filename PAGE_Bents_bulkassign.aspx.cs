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
using RBSR_AUFW.DB.IEntitlement;
using ComponentArt.Web.UI;
using RBSR_AUFW.DB.IBusRole;
using System.Collections.Generic;
using RBSR_AUFW.DB.IEntAssignmentSet;
using RBSR_AUFW.DB.IEntAssignment;

namespace _6MAR_WebApplication
{
  public partial class WebForm126 : AFWACpage
  {

    public int IDentitlement;
    public returnGetEntitlement THEent;

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

      Grid1.ItemCheckChanged += 
        new ComponentArt.Web.UI.Grid.ItemCheckChangedEventHandler(this.OnItemCheckChanged);

      Grid1.BeforeCallback +=
        new ComponentArt.Web.UI.Grid.BeforeCallbackEventHandler(this.OnBeforeCallback);
      Grid1.AfterCallback +=
        new ComponentArt.Web.UI.Grid.AfterCallbackEventHandler(this.OnAfterCallback);
    }




    private Dictionary<int, HELPERS.infoEASet> MAPsubprToEASet;
    // Key is ID of subprocess

     private Dictionary<int, int> MAPbroleToSubproc = new Dictionary<int, int>();
      //Key is ID of brole

      private Dictionary<int, string> MAPbroleIdToName = new Dictionary<int, string>();
      
      private Queue<int> QUEUEidsBusrolesToADD;
    private Queue<int> QUEUEidsBusrolesToREMOVE;



    protected void Page_Load(object sender, EventArgs e)
    {
      base.Page_Load(sender, e);

      try
        {
          IDentitlement = int.Parse(this.Request.Params["entid"]);
        }
      catch (Exception)
        {
          // Whilst debugging
          IDentitlement = 64622;
        }

      IEntitlement engine = new IEntitlement(HELPERS.NewOdbcConn());
      THEent = engine.GetEntitlement(IDentitlement);

      // There is currently no concept of this page being "read only".
      // It creates workspaces as needed; it is not performed in
      // any particular workspace.
      this.PANELcond_readonly.Visible = false;
    }




      public void OnBeforeCallback(object sender, EventArgs oArgs)
      {
          boolMustAbort = false;
          MAPsubprToEASet =
            new Dictionary<int, HELPERS.infoEASet>();
          MAPbroleToSubproc =
              new Dictionary<int, int>();
          MAPbroleIdToName =
              new Dictionary<int, string>();

          QUEUEidsBusrolesToADD = new Queue<int>();
          QUEUEidsBusrolesToREMOVE = new Queue<int>();
          engineBusRole = new IBusRole(HELPERS.NewOdbcConn());
          engineEASet = new IEntAssignmentSet(HELPERS.NewOdbcConn());
          messages = "";
      }




    /* This is called when the user submits the page's changes.
       It is called once for each checkbox change being submitted.
       If necessary, a workspace is launched for the relevant subprocess.
       If the subprocess already contains a workspace but it's owned
       by some other user, the change is not made and the user
       is alerted, but the process of executing the set of changes is
       not aborted.

       mimic this to create the automatic workspace creation:
       AutoRemoveEntitlementStatus in GuidedEditor/...
    */

    public void OnAfterCallback(object sender, EventArgs oArgs)
    {
      if (this.boolMustAbort) {
        Grid1.CallbackParameter = "OPERATION ABORTED:\n" + messages;
        return;
      }


      Dictionary<int, HELPERS.infoEASet> readyToUseWorkspaces = new Dictionary<int, HELPERS.infoEASet>();
      Dictionary<int, HELPERS.infoEASet> lockedWorkspaces = new Dictionary<int, HELPERS.infoEASet>();

      if (messages.Length > 0)
        {
          messages += "\n-------------------------\n\n";
        }
  
      HELPERS.AutoGenWorkspacesInBulk
        (session.idUser, ref messages, MAPsubprToEASet, readyToUseWorkspaces, lockedWorkspaces,
         "facilitate bulk role assignment/unassignment for a particular entitlement");

        // If any problem occurred at all, we abort!
      if (lockedWorkspaces.Count > 0)
      {
          Grid1.CallbackParameter = messages;
          return;
      }


        // We now proceed with the actual edits.
      IEntAssignment ENGINEentass = new IEntAssignment(HELPERS.NewOdbcConn());
      while (QUEUEidsBusrolesToREMOVE.Count > 0)
      {
          int idBusrole = QUEUEidsBusrolesToREMOVE.Dequeue();
          int idSubpr = MAPbroleToSubproc[idBusrole];
          messages += "Detaching from this role: " + MAPbroleIdToName[idBusrole] + "\n";
          HELPERS.infoEASet wstouse = readyToUseWorkspaces[idSubpr];

          returnListEntAssignmentByEntAssignmentSet[] theEAss =
          ENGINEentass.ListEntAssignmentByEntAssignmentSet
          (null, "\"BusRole\" = ? AND \"Entitlement\" = ? ", new string[] { idBusrole.ToString(), IDentitlement.ToString() }, "", wstouse.idEntAssSet);

          ENGINEentass.SetEntAssignment 
              (theEAss[0].ID, theEAss[0].EntAssignmentSetID, theEAss[0].BusRoleID, 
              theEAss[0].EntitlementID, "X");
      }
      while (QUEUEidsBusrolesToADD.Count > 0)
      {
          int idBusrole = QUEUEidsBusrolesToADD.Dequeue();
          int idSubpr = MAPbroleToSubproc[idBusrole];
          messages += "Attaching to this role: " + MAPbroleIdToName[idBusrole] + "\n";
          HELPERS.infoEASet wstouse = readyToUseWorkspaces[idSubpr];

          returnListEntAssignmentByEntAssignmentSet[] theEAss =
          ENGINEentass.ListEntAssignmentByEntAssignmentSet
          (null, "\"BusRole\" = ? AND \"Entitlement\" = ? ", new string[] { idBusrole.ToString(), IDentitlement.ToString() }, "", wstouse.idEntAssSet);

          if (theEAss.GetLength(0) > 1) {
              throw new Exception("Workspace " + wstouse.idEntAssSet + " contains multiple entitlement assignment records for business role " + idBusrole);
          }
          else if (theEAss.GetLength(0) == 1)
          {
              switch (theEAss[0].Status)
              {
                  case "A":
                      messages += "Nothing to do. Workspace already has this match in place.\n";
                      break;
                  case "N":
                      messages += "Nothing to do. Workspace already has this match in place.\n";
                      break;
                  case "X":
                      ENGINEentass.SetEntAssignment
                          (theEAss[0].ID, theEAss[0].EntAssignmentSetID, theEAss[0].BusRoleID,
                           theEAss[0].EntitlementID, "N");
                      break;
              }
          }
          else
          {
              int baby = ENGINEentass.NewEntAssignment
                  (wstouse.idEntAssSet, idBusrole, IDentitlement, "N");
          }
      }



      if (messages.Length == 0)
        {
          messages = "NO MESSAGES TO REPORT.";
        }
      Grid1.CallbackParameter = messages;
    }


    private IBusRole engineBusRole;
    private IEntAssignmentSet engineEASet;


        
        
        
        
        
    private String messages;
    private bool boolMustAbort;
        
    public void OnItemCheckChanged(object sender, GridItemCheckChangedEventArgs oArgs)
    {
      int IDbusrole = int.Parse(oArgs.Item["c_id"].ToString());
  
      returnGetBusRole detailsBusRole = engineBusRole.GetBusRole(IDbusrole);

      MAPbroleIdToName.Add(IDbusrole, detailsBusRole.Name);
      MAPbroleToSubproc.Add(IDbusrole, detailsBusRole.SubProcessID);

      if (oArgs.Checked)
        {
          QUEUEidsBusrolesToADD.Enqueue(IDbusrole);
        }
      else
        {
          QUEUEidsBusrolesToREMOVE.Enqueue(IDbusrole);
        }

      if (MAPsubprToEASet.ContainsKey(detailsBusRole.SubProcessID))
        return;

      // This role is in a subprocess we have not yet analyzed.
      // Must determine if in workspace or ACTIVE status.
      // If that subpr is in a workspace owned by someone else, 
      //   the entire operation is aborted with no change being made.
      returnListEntAssignmentSetBySubProcess[] ret = engineEASet.ListEntAssignmentSetBySubProcess
        (null, "\"Status\" = ?", new string[] { "WORKSPACE" }, "", detailsBusRole.SubProcessID);
      if (ret.Length > 1)
        {
          throw new Exception("More than one WORKSPACE open simultaneously for subprocess " +
                              oArgs.Item["NamePr"].ToString());
        }
      if (ret.Length == 1)
        {
          // A workspace is open for this subprocess.
          // OK if userID matches this session's user
          if (ret[0].UserID == session.idUser)
            {
              // It's a match, we can use this workspace.
              HELPERS.infoEASet EAS = new HELPERS.infoEASet();
              EAS.idEntAssSet = ret[0].ID;
              EAS.idEntAssSetCreator = ret[0].UserID;
              EAS.nameEntAssSetCreator = ret[0].UserLoginName;
              EAS.nameSubprocess = oArgs.Item["NamePr"].ToString();
              EAS.strEntAssSetStatus = "WORKSPACE";
              MAPsubprToEASet.Add(detailsBusRole.SubProcessID, EAS);
            }
          else
            {
              boolMustAbort = true;
              messages +=
                "ERROR: Cannot make modifications regarding role " +
                oArgs.Item["c_u_Name"] + ".  The workspace for " +
                oArgs.Item["NamePr"] + " is owned by a 3rd party (" +
                ret[0].UserLoginName + ").\n";
            }
        }
      else
        {
          // Make sure there is an ACTIVE EASet to build a workspace from.
          ret = engineEASet.ListEntAssignmentSetBySubProcess
            (null, "\"Status\" = ?", new string[] { "ACTIVE" }, "", detailsBusRole.SubProcessID);
          if (ret.Length > 1)
            {
              throw new Exception("More than one ACTIVE entitlement-assignment set for " +
                                  oArgs.Item["NamePr"].ToString());
            }
          if (ret.Length < 1)
            {
              throw new Exception("Subprocess has NO active entitlement-assignment set yet: " +
                                  oArgs.Item["NamePr"].ToString());
            }
          if (ret.Length == 1)
            {
              HELPERS.infoEASet EAS = new HELPERS.infoEASet();
              EAS.idEntAssSet = ret[0].ID;
              EAS.idEntAssSetCreator = ret[0].UserID;
              EAS.nameEntAssSetCreator = ret[0].UserLoginName;
              EAS.nameSubprocess = oArgs.Item["NamePr"].ToString();
              EAS.strEntAssSetStatus = "ACTIVE";
              MAPsubprToEASet.Add(detailsBusRole.SubProcessID, EAS);
            }
        }
    }


    /*

      IEntAssignment ENGINEentass = new IEntAssignment(HELPERS.NewOdbcConn());

      int IDentitlement = int.Parse(oArgs.Item["c_id"].ToString());

      string editstatusEAss = "-";
      int idEntAss = -1;
      if (oArgs.Item["TEassEditStatus"] != null)
      {
      if (oArgs.Item["TEassEditStatus"].ToString() != "")
      {
      editstatusEAss = oArgs.Item["TEassEditStatus"].ToString();
      idEntAss = int.Parse(oArgs.Item["TEassRowId"].ToString());
      }
      }


      if (oArgs.Checked)
      {
      if (idEntAss >= 0)
      {
      returnGetEntAssignment curval = ENGINEentass.GetEntAssignment(idEntAss);
      ENGINEentass.SetEntAssignment(idEntAss, curval.EntAssignmentSetID, curval.BusRoleID, curval.EntitlementID,
      "A");//ACTIVE
      }
      else
      {
      // Create a EAss
      int baby = ENGINEentass.NewEntAssignment(session.idWorkspace, IDrole, IDentitlement, "N");
      MAPentIdToBabyEAssId.Add(IDentitlement, baby);
      }
      }
      else
      {
      if (idEntAss < 0)
      {
      if (MAPentIdToBabyEAssId.ContainsKey(IDentitlement))
      {
      idEntAss = MAPentIdToBabyEAssId[IDentitlement];
      editstatusEAss = "N";
      MAPentIdToBabyEAssId.Remove(IDentitlement);
      }
      }
      if (editstatusEAss == "N")
      {
      try
      {
      ENGINEentass.DeleteEntAssignment(idEntAss);
      }
      catch (Exception eee)
      {
      // Exceptions will occur here naturally, if during a session where row R was
      // initially unchecked, someone checked it, and then unchecked it back to its
      // original unchecked state, and then hits submit.
      ENGINEentass.DeleteEntAssignment(
      MAPentIdToBabyEAssId[IDentitlement]);
      MAPentIdToBabyEAssId.Remove(IDentitlement);
      }
      }
      else
      {
      returnGetEntAssignment curval = ENGINEentass.GetEntAssignment(idEntAss);
      ENGINEentass.SetEntAssignment(idEntAss, curval.EntAssignmentSetID, curval.BusRoleID, curval.EntitlementID,
      "X");
      }
      }

      } 
    */

  }
}
