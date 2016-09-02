using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using RBSR_AUFW.DB.ITcodeDictionary;
using RBSR_AUFW.DB.IBusRole;
using RBSR_AUFW.DB.IEntAssignment;
using RBSR_AUFW.DB.IUser;
using System.Data.Odbc;
using RBSR_AUFW.DB.IChangeManagementEvent;
using RBSR_AUFW.DB.IAdditionalBusRole;
using RBSR_AUFW.DB.IMETADATA_SubprToActivityList;
using RBSR_AUFW.DB.IBusRoleOwner;
using RBSR_AUFW.DB.IFuncApplNotes;
using RBSR_AUFW.DB.ISAPChangeManagementEvent;
using RBSR_AUFW.DB.ISAPRoleOwner;
using RBSR_AUFW.DB.ITcodeAssignment;
using RBSR_AUFW.DB.ISAPsecurityOrgAxis;
using System.Web.SessionState;
using RBSR_AUFW.DB.IEntitlement;
using RBSR_AUFW.DB.ISAProle;
using RBSR_AUFW.DB.ITcodeEntitlement;
using RBSR_AUFW.DB.IReconcDiffItem;
using RBSR_AUFW.DB.IOrgValue1252;
using RBSR_AUFW.DB.ISAPauthField;
using RBSR_AUFW.DB.IAuthRow1251;
using RBSR_AUFW.DB.ISAPauthClass;
using RBSR_AUFW.DB.ISAPauthObj;
using RBSR_AUFW.DB.ISubProcess;
using RBSR_AUFW.DB.IProcess;
using System.Configuration;







namespace _6MAR_WebApplication.GuidedEditor
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler2 : IHttpHandler, IRequiresSessionState
  {

    private OdbcConnection CONNdedicated;



    protected AFWACsession session;


      
      
      
    public void ProcessRequest(HttpContext context)
    {


      string cmd = (context.Request.Params["cmd"]);


      if (cmd != "EmitAuthFieldListForGivenAuthObj")
        {


          if (context.Session["AFWACSESSION"] == null)
            {
              throw new Exception("Must be in a legal R-AF session");
            }

          session = context.Session["AFWACSESSION"] as AFWACsession;
        }


        
      context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-100);
      context.Response.AddHeader("pragma", "no-cache");
      context.Response.AddHeader("cache-control", "private");
      context.Response.CacheControl = "no-cache";
      context.Response.ContentType = "text/plain";



      CONNdedicated = HELPERS.NewOdbcConn_FORCE();


      string arg1 = (context.Request.Params["arg1"]);

      string arg2 = "";
      try
        {
          arg2 = (context.Request.Params["arg2"]);
        }
      catch (Exception ex2)
        {
          ;
        }

      string arg3 = "";
      try
        {
          arg3 = (context.Request.Params["arg3"]);
        }
      catch (Exception ex3)
        {
          ;
        }

      string arg4 = "";
      try
        {
          arg4 = (context.Request.Params["arg4"]);
        }
      catch (Exception ex4)
        {
          ;
        }



      try
        {
          switch (cmd)
            {

            case "SAP_AddObjsTo1251":
              SAP_AddObjsTo1251(context.Request.Params, context.Response);
              break;



              case "Edit1251row":
                  SAP_EditObj1251(context.Request.Params, context.Response);
                  break;



              case "Delete1251row":
                  this.SAP_ToggleDeleteStatusOf1251row(context.Request.Params, context.Response);
                  break;
              case "UnDelete1251row":
                  this.SAP_ToggleDeleteStatusOf1251row(context.Request.Params, context.Response);
                  break;





            case "EDIT_SingleRowSingleColumn":
              EDIT_SingleRowSingleColumn(arg1, arg2, arg3, arg4);
              break;

            case "EDIT_MultiRowsSingleColumn":
              EDIT_MultiRowsSingleColumn(arg1, arg2, arg3, arg4);
              break;

            case "JQDLGeditOrgAxis":
              JQDLGeditOrgAxis(int.Parse(arg1), arg2, arg3, arg4);
              break;

            case "JQDLGdeleteOrgAxis":
              JQDLGdeleteOrgAxis(int.Parse(arg1));
              break;



              case "JQDLGnamePlusDescr":
                  JQDLGnamePlusDescr(context.Request.Params, context.Response);
                  break;


              case "DeleteSAPRole":
                  SAP_deleteSapRole(context.Request.Params, context.Response);
                  break;

            case "Edit1252row":
              Edit1252row(context.Request.Params, context.Response);
              break;
            case "Delete1252row":
              Manip1252row(context.Request.Params, context.Response, "DEL");
              break;
            case "Undelete1252row":
              Manip1252row(context.Request.Params, context.Response, "UNDEL");
              break;

            case "ComputeManifestString":
              ComputeManifestString(context.Response, arg1, arg2, arg3, arg4);
              break;

            case "ComputeSampleManifestStringsForApp":
              // arg1 is the application's numeric c_ID
              // arg2 is the formula to be used
              ComputeSampleManifestStringsForApp(context.Response, int.Parse(arg1), arg2);
              break;         

            case "CreateWorkspaceOnEntAssSet":
              CreateWorkspaceOnEntAssSet(int.Parse(arg1)/*subprocessID*/, int.Parse(arg2)/*userID*/, arg3/*comment*/);
              break;

            case "CloneBusRole":
              CloneBusRole(int.Parse(arg1), arg2, int.Parse(arg3), context.Response);
              break;

            case "FindUserByEID":
              FindUserByEID(arg1, arg2, context.Response);
              break;

            case "PublishEntAssSet":
              PublishEntAssSet(int.Parse(arg1), arg2);
              break;

            case "PublishSAPEntAssSet":
              PublishSAPEntAssSet(int.Parse(arg1), arg2);
              break;

            case "SetCommentForBroleApplPair":
              SetCommentForBroleApplPair(context.Request.Params, context.Response);
              break;

            case "JQDLGbusroleProperties":
              JQDLGbusroleProperties(context.Request.Params, context.Response);
              break;
            case "JQDLGsaproleProperties":
              JQDLGsaproleProperties(context.Request.Params, context.Response);
              break;

            case "JQDLGeditChgMgmt":
              JQDLGeditChgMgmt(context.Request.Params, context.Response);
              break;
            case "JQDLGeditChgMgmt_SAP":
              JQDLGeditChgMgmt_SAP(context.Request.Params, context.Response);
              break;

            case "JQDLGeditRoleOwner":
              JQDLGeditRoleOwner(context.Request.Params, context.Response);
              break;

            case "JQDLGassignReconcDiff":
              JQDLGassignReconcDiff(context.Request.Params, context.Response);
              break;

            case "JQDLGeditSAPRoleOwner":
              JQDLGeditSAPRoleOwner(context.Request.Params, context.Response);
              break;

            case "JQDLGeditAdditionalRoleRow":
              JQDLGeditAdditionalRoleRow(context.Request.Params, context.Response);
              break;


            case "RequenceSubprActivityRecord":
              RequenceSubprActivityRecord(context.Request.Params, context.Response);
              break;

            case "UpdateSubprActivityRecord":
              UpdateSubprActivityRecord(context.Request.Params, context.Response);
              break;



            case "EditSAPentitlement":
              EditSAPentitlement(context.Request.Params, context.Response);
              break;


            case "RegisterNewTcode":
              RegisterNewTcode(context.Request.Params, context.Response, arg1, arg2);
              break;

            case "FindTcode":
              // Returns the description for the tcode, or an HTML-encoded not-found message
              // suitable for display.
              ITcodeDictionary x = new ITcodeDictionary(HELPERS.NewOdbcConn_FORCE());
              returnListTcodeDictionary[] entry =
                x.ListTcodeDictionary(null, " \"TcodeID\" = ? ", new string[] { arg1 }, "");
              if (entry.Length == 0)
                {
                  context.Response.Write("<span style='color:red'>NOT FOUND</span>");
                  return;
                }
              else
                {
                  context.Response.Write(entry[0].Description);
                }
              break;

            case "EmitAuthFieldListForGivenAuthObj":
              EmitFieldList(int.Parse(arg1), context.Response);
              break;

            default:
              throw new Exception("Unknown action cmd");
              break;
            }
        }
      catch (Exception e)
        {
          context.Response.Write(e.Message);
          context.Response.StatusCode = 500;
          
        }
        CONNdedicated.Close();
    }





      private void JQDLGnamePlusDescr(System.Collections.Specialized.NameValueCollection P, HttpResponse httpResponse)
      {
          int targetid = -1;
          try
          {
              targetid = int.Parse(P["targetid"]);
          }
          catch (Exception eignore) { }
          string targettype = P["targettype"];
          string itemname = P["name"];
          string itemdescr = P["descr"];
          switch (targettype)
          {
              case "AuthObj Class":
                  {
                      ISAPauthClass engine = new ISAPauthClass(CONNdedicated);
                      if (targetid < 0)
                      {
                          engine.NewSAPauthClass(itemname, itemdescr, "N");
                      }
                  }
                  break;

              case "TCode":
                  break;

              case "Auth Object":
                  {
                      ISAPauthObj engine = new ISAPauthObj(CONNdedicated);
                      if (targetid < 0)
                      {
                          int IDauthclass = int.Parse(P["authclassid"]);
                          engine.NewSAPauthObj(itemname, itemdescr, IDauthclass, "N");
                      }
                  }
                  break;

              default:
                  throw new Exception("Unknown target type: " + targettype);
          }
      }










      private void SAP_ToggleDeleteStatusOf1251row(System.Collections.Specialized.NameValueCollection P, HttpResponse httpResponse)
      {
          System.Data.Odbc.OdbcConnection conn = HELPERS.NewOdbcConn_FORCE();
          IAuthRow1251 engine1251 = new IAuthRow1251(conn);

          int IDobjtoedit = int.Parse(P["IDentit"]);

          returnGetAuthRow1251 curobj =
             engine1251.GetAuthRow1251(IDobjtoedit);

          int newEditStatus;
          if (0 != (curobj.EditStatus & 4))
          {
              newEditStatus = curobj.EditStatus & (8 + 2 + 1);
          }
          else
          {
              newEditStatus = curobj.EditStatus + 4;
          }

          engine1251.SetAuthRow1251
          (IDobjtoedit, curobj.RangeLow, curobj.RangeHigh, curobj.SAPauthObjID, curobj.SAPauthFieldID, curobj.TcodeAssignmentSetID, curobj.SAProleID,
             newEditStatus);
      }

      private void SAP_EditObj1251(System.Collections.Specialized.NameValueCollection P, HttpResponse httpResponse)
      {
          System.Data.Odbc.OdbcConnection conn = HELPERS.NewOdbcConn_FORCE();
          IAuthRow1251 engine1251 = new IAuthRow1251(conn);

          int IDobjtoedit = int.Parse(P["IDentit"]);

          returnGetAuthRow1251 curobj = 
             engine1251.GetAuthRow1251(IDobjtoedit);

          if (
              (curobj.RangeLow == P["c_u_RangeLow"])
              &&
              (curobj.RangeHigh == P["c_u_RangeHigh"])
              )
          {
              httpResponse.Write("No change was noted, so no action was performed.");
              return;
          }


          engine1251.SetAuthRow1251
          (IDobjtoedit, P["c_u_RangeLow"], P["c_u_RangeHigh"], curobj.SAPauthObjID, curobj.SAPauthFieldID, curobj.TcodeAssignmentSetID, curobj.SAProleID,
             curobj.EditStatus | 8/*modified*/);

      }




    private void SAP_AddObjsTo1251(System.Collections.Specialized.NameValueCollection P, HttpResponse httpResponse)
    {
        System.Data.Odbc.OdbcConnection conn = HELPERS.NewOdbcConn_FORCE();

        int IDauthobj = int.Parse(P["IDauthobj"]);
      int IDsaprole = int.Parse(P["IDsaprole"]);

      System.Data.Odbc.OdbcConnection conn2 = HELPERS.NewOdbcConn_FORCE();
      IAuthRow1251 engine1251 = new IAuthRow1251(conn2);
         
      OdbcDataReader dr = InquireFieldListForAuthObj(IDauthobj, conn);
      while (dr.Read())
      {
          int idSAPauthField = dr.GetInt32(2);

          // Does this obj+field combination already exist?
          returnListAuthRow1251ByTcodeAssignmentSet[] listAuthRows =
          engine1251.ListAuthRow1251ByTcodeAssignmentSet
          (null, " \"SAPauthObj\" = ?  AND \"SAPauthField\" = ?", new string[] { IDauthobj.ToString(), idSAPauthField.ToString() }, "",
              /*TcodeAssignmentSet is the WorkspaceID: */  session.idWorkspace_SAP);

          if (listAuthRows.GetLength(0) < 1)
          {
              // OK, we're ready to add a row
              engine1251.NewAuthRow1251
              (IDauthobj, idSAPauthField, session.idWorkspace_SAP, IDsaprole, 2);
          }
          else
          {
              // A row for this combo already exists, but we will make sure it is not
              // with its deletion bit set to delete.
              engine1251.SetAuthRow1251
                  (listAuthRows[0].ID, listAuthRows[0].RangeLow, listAuthRows[0].RangeHigh,
                   listAuthRows[0].SAPauthObjID, listAuthRows[0].SAPauthFieldID, listAuthRows[0].TcodeAssignmentSetID,
                   listAuthRows[0].SAProleID,
                   listAuthRows[0].EditStatus & (8 + 2 + 1) /* to turn OFF the deletion bit if it happens to be on */);
          }        
      }
    }









    private void Manip1252row
      (System.Collections.Specialized.NameValueCollection P, 
       HttpResponse httpResponse, 
       string cmd)
    {

      int idExistingRow = int.Parse(P["IDentit"]);

      int IDrole = int.Parse(P["IDrole"]);
      int IDuser = int.Parse(P["IDuser"]);
      int IDws = int.Parse(P["IDws"]);

      IOrgValue1252 engineTCEnt = new IOrgValue1252(HELPERS.NewOdbcConn());


      returnGetOrgValue1252 existingrow =
        engineTCEnt.GetOrgValue1252(idExistingRow);

      switch (cmd)
        {
        case "DEL":
          existingrow.EditStatus |= 4;
          break;
        case "UNDEL":
          existingrow.EditStatus &= 11;
          break;
        }

      engineTCEnt.SetOrgValue1252
        (idExistingRow, existingrow.FieldName, existingrow.RangeLow, existingrow.RangeHigh,
         existingrow.TcodeAssignmentSetID, existingrow.SAProleID, existingrow.EditStatus);
    }





    // This will be patterned after EditSapEntitlement
    private void Edit1252row(System.Collections.Specialized.NameValueCollection P, HttpResponse httpResponse)
    {
      string strIDtass = P["IDtass"];
      // if (strIDtass == "ADD") then this is indeed an add, not an edit

      int IDrole = int.Parse(P["IDrole"]);
      int IDuser = int.Parse(P["IDuser"]);
      int IDws = int.Parse(P["IDws"]);

      IOrgValue1252 engineTCEnt = new IOrgValue1252(HELPERS.NewOdbcConn());


      // First, we want to see if there is already, attached to this role,
      // a row with these same characteristics.
      // there is no comment field on this kind of row so this doesn't need the
      // "only comment changed" logic.

      int returnIDof1252row;

      char returnstat = 
        SAP_HELPERS.CreateOrgValueAssignmentIfNotExists
        (IDws, engineTCEnt, IDrole, P["c_u_Field"], P["c_u_RangeLow"], P["c_u_RangeHigh"], 2,
         out returnIDof1252row);

      switch (returnstat)
        {
        case 'A':
          // This 1252 row did not yet exist for this SAP role so it was added.
          // If this was an attempt to edit an existing 1252 row, then the
          // existing one should be deleted.
          if (strIDtass != "ADD")
            {
              // Existing row must be marked as deleted.
              int idExisting1252row = int.Parse(strIDtass);
              returnGetOrgValue1252 rowExisting =
                engineTCEnt.GetOrgValue1252(idExisting1252row);
              engineTCEnt.SetOrgValue1252
                (idExisting1252row,
                 rowExisting.FieldName,
                 rowExisting.RangeLow,
                 rowExisting.RangeHigh,
                 rowExisting.TcodeAssignmentSetID,
                 rowExisting.SAProleID,
                 (rowExisting.EditStatus | 4/*deletionbit*/));
            }             
          break;


        }
    }











    private void ComputeSampleManifestStringsForApp
      (HttpResponse resp, int IDapplication, string formula)
    {
      // Get a list of active entitlements for the given application
      string sql =
        @"
SELECT TOP 20 c_id FROM t_RBSR_AUFW_u_Entitlement
WHERE c_u_Application = (SELECT c_u_Name FROM t_RBSR_AUFW_u_Application WHERE c_id=" + IDapplication +
        ") AND c_u_Status = 'A';";
      OdbcDataReader reader = HELPERS.RunSqlSelect(sql);

      IEntitlement engine = new IEntitlement(HELPERS.NewOdbcConn_FORCE());
      
      while (reader.Read())
        {
          int IDentitlement = reader.GetInt32(0);
          returnGetEntitlement seed = new returnGetEntitlement();
          bool hasBeenSeeded = false;

          try
            {
              seed = engine.GetEntitlement(IDentitlement);
              hasBeenSeeded = true;
            }
          catch (Exception eeeeignore) { }

          resp.Write(HELPERS.CalcManifestString(seed, formula) + "<BR/>\n");
        }
    }





    // If arg1 is a string representation of an integer, then that int is an entitlement ID
    // and the fields should come straight from the sql database.
    // Otherwise, arg1 is a JSON dictionary where the field values should be taken from.
    //
    // If arg2 is a non-empty string, it is the formula to be used, instead of the formula
    // being extracted from the database.
      // 
      //
      // W A R N I N G ! ! !   
      // GENERATES HTML.
    // THIS MUST **NEVER** BE USED FOR DATA PROCESSING, for generating manifests, for reconciliation, for export-to-spreadsheet, etc!!!!
      //
    private void ComputeManifestString(HttpResponse resp, string arg1, string arg2, string arg3, string arg4)
    {
      returnGetEntitlement seed = new returnGetEntitlement();
      bool hasBeenSeeded = false;

      try
        {
          int IDentitlement = int.Parse(arg1);
          IEntitlement engine = new IEntitlement(HELPERS.NewOdbcConn());
          seed = engine.GetEntitlement(IDentitlement);
          hasBeenSeeded = true;
        }
      catch (Exception eeeeignore) { }


      if ( ! hasBeenSeeded)
        {
          System.Web.Script.Serialization.JavaScriptSerializer UTIL =
            new System.Web.Script.Serialization.JavaScriptSerializer();
          Object whatami = UTIL.DeserializeObject(arg1);
          System.Collections.Generic.Dictionary<string, Object> www =
            whatami as System.Collections.Generic.Dictionary<string, Object>;
          seed = new returnGetEntitlement();
          seed.Application = HELPERS.SafeGenericDictionaryLookup(www, "Application");
          seed.AuthObjName = HELPERS.SafeGenericDictionaryLookup(www, "AuthObjName");
          seed.AuthObjValue = HELPERS.SafeGenericDictionaryLookup(www, "AuthObjValue");
          seed.EntitlementName = HELPERS.SafeGenericDictionaryLookup(www, "EntitlementName");
          seed.EntitlementValue = HELPERS.SafeGenericDictionaryLookup(www, "EntitlementValue");
          seed.FieldSecName = HELPERS.SafeGenericDictionaryLookup(www, "FieldSecName");
          seed.FieldSecValue = HELPERS.SafeGenericDictionaryLookup(www, "FieldSecValue");
          seed.Level4SecName = HELPERS.SafeGenericDictionaryLookup(www, "Level4SecName");
          seed.Level4SecValue = HELPERS.SafeGenericDictionaryLookup(www, "Level4SecValue");
          seed.Platform = HELPERS.SafeGenericDictionaryLookup(www, "Platform");
          seed.RoleType = HELPERS.SafeGenericDictionaryLookup(www, "RoleType");
          seed.StandardActivity = HELPERS.SafeGenericDictionaryLookup(www, "StandardActivity");
          seed.System = HELPERS.SafeGenericDictionaryLookup(www, "System");
        }

      string THEMANIFEST;
      if (arg2 != null) 
        {
          THEMANIFEST = HELPERS.CalcManifestString(seed, arg2);
        }
      else
        {
          THEMANIFEST = HELPERS.CalcManifestString(seed);
        }


        resp.Write(THEMANIFEST.Replace("&", "&amp;").Replace("<","&lt;").Replace(">","&gt;"));
    }

 

    // Can create new as well as edit existing.
    // If id < zero, means create
    private void JQDLGeditOrgAxis(int id, string name, string descr, string legalvals)
    {
      ISAPsecurityOrgAxis engine = new ISAPsecurityOrgAxis(HELPERS.NewOdbcConn_FORCE());
      if (id >= 0)
        engine.SetSAPsecurityOrgAxis(id, descr, name, legalvals);
      else
        {
          id = engine.NewSAPsecurityOrgAxis(descr, name);
          engine.SetSAPsecurityOrgAxis(id, descr, name, legalvals);
        }

    }

    private void JQDLGdeleteOrgAxis(int id)
    {
      ISAPsecurityOrgAxis engine = new ISAPsecurityOrgAxis(HELPERS.NewOdbcConn_FORCE());
      if (id >= 0)
        {
          returnGetSAPsecurityOrgAxis ret = engine.GetSAPsecurityOrgAxis(id);
          string newdelname = "DeL_" + ret.SAP_Name + "_" + id.ToString();
          engine.SetSAPsecurityOrgAxis(id, ret.English_Name, newdelname, ret.LegalValues);
        }
    }



    private void EditSAPentitlement
      (System.Collections.Specialized.NameValueCollection P, 
       HttpResponse httpResponse)
    {
      ITcodeEntitlement engineTCEnt = new ITcodeEntitlement(HELPERS.NewOdbcConn());


      bool RETURNnewOneWasCreated;
      int RETURNid;

        // NOTE: change made on 27 May.
        // The below call *will* update the comment field even if the Tcode entitlement
        // is found to exist already.  It's a "side-effect" in a sense.
      SAP_HELPERS.CreateTcodeEntitlementIfNotExists
        (engineTCEnt, P["c_u_TCode"],
         P["c_u_ActivityFolder"],
         P["c_u_Type"],
         P["c_u_AccessLevel"],
         P["c_u_Comment"],
         out RETURNnewOneWasCreated, out RETURNid);


      int IDentitlement = RETURNid;


      /*        
                if ( ! RETURNnewOneWasCreated)
                {
                if (P["IDentit"].Length > 1) {
                if (IDentitlement != int.Parse(P["IDentit"])) {
                throw new Exception("Database Internal Error: multiple identical TC entitlements: " 
                + IDentitlement + " and " + int.Parse(P["IDentit"]));
                }
                }
                }
      */


      // It really doesn't matter if this is a new TC entitlement.

      // The workspace ID is in session paarm INTcurWS_SAP
      // The role is being sent in.
      ITcodeAssignment engine = new ITcodeAssignment(HELPERS.NewOdbcConn());
      string strIDtass = P["IDtass"];
      int intIDtass;

      int IDrole = int.Parse(P["IDrole"]);
      int IDuser = int.Parse(P["IDuser"]);
      int IDws = int.Parse(P["IDws"]);
      string tcode = (P["c_u_TCode"]);




      char subjunctiveTestTcodeAssignment =
        SAP_HELPERS.CreateTcodeAssignmentIfNotExists
        (IDws, engine, IDrole, RETURNnewOneWasCreated, IDentitlement, true, out intIDtass);



      if (strIDtass == "ADD")
        {
          //
          // The user is trying to create a NEW assignment.
          //
          // Fail non-violently if already exists in this workspace
          if (subjunctiveTestTcodeAssignment == 'E')
            {
              throw new Exception("That particular assignment is already in place.");
            }

          SAP_HELPERS.CreateTcodeAssignmentIfNotExists
            (IDws, engine, IDrole, RETURNnewOneWasCreated, IDentitlement, false, out intIDtass);
        }

      else

        {
          //
          // The user is trying to edit an EXISTING assignment.
          //

          if (subjunctiveTestTcodeAssignment == 'E')
            {
              // We have to check for the situation that the edit was just to the commentary,
              // in which place this is indeed a case of the assignment already being in place
              // but this is a case in which that is not something to be reported/alarmed.
                if (int.Parse(P["IDtass"]) == intIDtass)
                {
                    return;
                }
              throw new Exception("That particular assignment is already in place.");
            }

          intIDtass = int.Parse(strIDtass);

          SAP_HELPERS.RecordChangeInEntitlementAssignmentRow
            (intIDtass, IDws, IDentitlement, IDuser, IDrole,
             P["ipaddr"], P);
        }
    }





    private void RegisterNewTcode
      (System.Collections.Specialized.NameValueCollection P,
       HttpResponse httpResponse, string newname, string newdescr)
    {
      ITcodeDictionary engine = new ITcodeDictionary(HELPERS.NewOdbcConn());
      try
        {
          int IDbaby = engine.NewTcodeDictionary(newname);
          engine.SetTcodeDescription(IDbaby, newdescr);
        }
      catch (Exception eee)
        {
          throw new Exception("That TCode name is already registered.");
        }
    }







    private void SetCommentForBroleApplPair
      (System.Collections.Specialized.NameValueCollection P, HttpResponse httpResponse)
    {
      IFuncApplNotes engine = new IFuncApplNotes(HELPERS.NewOdbcConn());
          
      string strIDcuredit = P["idcuredit"];

      int i = 0;

      /*
        P["FANtext"]
        null
        P["text"]
        "BASIS"
        P["idbrole"]
        "218"
        P["idappl"]
        "77"
        * */

      int idBRole = int.Parse(P["idbrole"]);
      int idAppl = int.Parse(P["idappl"]);

      int idcuredit;

      if (strIDcuredit == "")
        {
          idcuredit = engine.NewFuncApplNotes(idBRole, idAppl, P["text"]);
        }
      else
        {
          idcuredit = int.Parse(strIDcuredit);
          engine.SetFuncApplNotes(idcuredit, idAppl, P["text"], idBRole);
        }

    }






    private void RequenceSubprActivityRecord(System.Collections.Specialized.NameValueCollection P, HttpResponse httpResponse)
    {
        IMETADATA_SubprToActivityList engine = new IMETADATA_SubprToActivityList(CONNdedicated);

      engine.SetSequence(int.Parse(P["idcuredit"]), float.Parse(P["newseqnum"]));
    }

      

    private void UpdateSubprActivityRecord(System.Collections.Specialized.NameValueCollection P, HttpResponse httpResponse)
    {
      IMETADATA_SubprToActivityList engine = new IMETADATA_SubprToActivityList(CONNdedicated);

      engine.SetMETADATA_SubprToActivityList
        (int.Parse(P["idcuredit"]), P["nodetype"],
         (P["boolIsKey"] == "on"), P["text"],
         P["listSelBRoles"], P["listSelApps"], int.Parse(P["idsubpr"]));
    }




    private void JQDLGassignReconcDiff
      (System.Collections.Specialized.NameValueCollection pars, HttpResponse httpResponse)
    {
        IReconcDiffItem engine = new IReconcDiffItem(CONNdedicated);

      string EID = pars["eid"].Trim();
      if (pars["deleteme"] == "true") {
        EID = null;
      }


      EDIT_MultiRowsSingleColumn
        ("ReconcDiffItem", pars["idcuredit"], "c_u_AssignedUser", EID);
            
      if (EID != null) {
        CreateOrUpdateUserName(EID,
                               pars["NAMEsur"], pars["NAMEfirst"], true);
      }
    }



    private void JQDLGeditRoleOwner
      (System.Collections.Specialized.NameValueCollection pars, HttpResponse httpResponse)
    {
        IBusRoleOwner engine = new IBusRoleOwner(CONNdedicated);

      int idBusRoleBeingEdited = int.Parse(pars["JQDLGbp_id"]);

      string EID = pars["eid"].Trim();

      string strIdCurEdit = pars["idcuredit"];
      int idCurEdit;

      if (strIdCurEdit == "ADD")
        {
          idCurEdit = engine.NewBusRoleOwner
            (EID, pars["geo"], pars["rank"], idBusRoleBeingEdited);
        }
      else
        {
          idCurEdit = int.Parse(strIdCurEdit);
        }

      if (pars["deleteme"] == "true")
        {
          // DELETION OF THIS ROW IS REQUESTED.
          engine.DeleteBusRoleOwner(idCurEdit);
        }
      else
        {
          engine.SetBusRoleOwner
            (idCurEdit,
             EID, pars["geo"], pars["rank"], idBusRoleBeingEdited);
        }


      CreateOrUpdateUserName(EID,
                             pars["NAMEsur"], pars["NAMEfirst"], true);
    }






    private void JQDLGeditSAPRoleOwner
      (System.Collections.Specialized.NameValueCollection pars, HttpResponse httpResponse)
    {
        ISAPRoleOwner engine = new ISAPRoleOwner(CONNdedicated);

      int idSAPRoleBeingEdited = int.Parse(pars["idsaprole"]);

      string EID = pars["eid"].Trim();

      string strIdCurEdit = pars["idcuredit"];
      int idCurEdit;

      if (strIdCurEdit == "ADD")
        {
          idCurEdit = engine.NewSAPRoleOwner
            (EID, pars["geo"], pars["rank"], idSAPRoleBeingEdited);
        }
      else
        {
          idCurEdit = int.Parse(strIdCurEdit);
        }

      if (pars["deleteme"] == "true")
        {
          // DELETION OF THIS ROW IS REQUESTED.
          engine.DeleteSAPRoleOwner(idCurEdit);
        }
      else
        {
          engine.SetSAPRoleOwner
            (idCurEdit,
             EID, pars["geo"], pars["rank"], idSAPRoleBeingEdited);
        }


      CreateOrUpdateUserName(EID,
                             pars["NAMEsur"], pars["NAMEfirst"], true);
    }



    private void JQDLGeditAdditionalRoleRow
      (System.Collections.Specialized.NameValueCollection pars, HttpResponse httpResponse)
    {
        IAdditionalBusRole engine = new IAdditionalBusRole(CONNdedicated);

      int idBusRoleBeingEdited = int.Parse(pars["JQDLGbp_id"]);


      int idSupplementalRowToEdit;

      int idSupplementalRole = int.Parse(pars["idSupplBrole"]);

      string strDateExpir = pars["editaddrole_INPUT_EXPIRDATE"];
      DateTime? dateExpir = HELPERS.ParseDate(strDateExpir);

      string intervalRecert = pars["editaddrole_SELECT_RECERTINTERVAL"];
        
      string strDateIntervalStart = pars["editaddrole_INPUT_RECERTSTARTDATE"];
      DateTime? dateIntervalStart = HELPERS.ParseDate(strDateIntervalStart);

      string comment = pars["comment"];


      if (pars["editaddrole_HIDDEN_ID"] == "ADD")
        {
          idSupplementalRowToEdit = engine.NewAdditionalBusRole(idSupplementalRole, idBusRoleBeingEdited);
        }
      else
        {
          idSupplementalRowToEdit = int.Parse(pars["editaddrole_HIDDEN_ID"]);
        }


      if (pars["deleteme"] == "true")
        {
          // DELETION OF THIS ROW IS REQUESTED.
          engine.DeleteAdditionalBusRole(idSupplementalRowToEdit);
        }
      else
        {
          engine.SetAdditionalBusRole
            (idSupplementalRowToEdit,
             idSupplementalRole, idBusRoleBeingEdited, comment, intervalRecert,
             dateIntervalStart, dateExpir);
        }
            
    }




    private void JQDLGaddAdditionalRoleRow(System.Collections.Specialized.NameValueCollection pars, HttpResponse httpResponse)
    {
      throw new Exception("The method or operation is not implemented.");
    }



    private void JQDLGeditChgMgmt
      (System.Collections.Specialized.NameValueCollection pars, HttpResponse httpResponse)
    {
        IChangeManagementEvent engine = new IChangeManagementEvent(CONNdedicated);
      int theid = int.Parse(pars["editchgmgmtrow_HIDDEN_ID"]);

      string dateFromJavaScript = pars["editchgmgmtrow_INPUT_WHEN"];

      if (dateFromJavaScript != "") {
        DateTime dt = DateTime.Parse(dateFromJavaScript);
        engine.SetChangeManagementEvent
          (theid,
           pars["editchgmgmtrow_INPUT_WHO"], dt,
           pars["comment"]);
      }else{
        engine.SetChangeManagementEvent
          (theid,
           pars["editchgmgmtrow_INPUT_WHO"], null,
           pars["comment"]);
      }
    }


    private void JQDLGeditChgMgmt_SAP
      (System.Collections.Specialized.NameValueCollection pars, HttpResponse httpResponse)
    {
        ISAPChangeManagementEvent engine = new ISAPChangeManagementEvent(CONNdedicated);
      int theid = int.Parse(pars["editchgmgmtrow_HIDDEN_ID"]);

      string dateFromJavaScript = pars["editchgmgmtrow_INPUT_WHEN"];

      if (dateFromJavaScript != "")
        {
          DateTime dt = DateTime.Parse(dateFromJavaScript);
          engine.SetSAPChangeManagementEvent
            (theid,
             pars["editchgmgmtrow_INPUT_WHO"], dt,
             pars["comment"]);
        }
      else
        {
          engine.SetSAPChangeManagementEvent
            (theid,
             pars["editchgmgmtrow_INPUT_WHO"], null,
             pars["comment"]);
        }
    }



    // Updates a business role object and if necessary creates new user objects for role ownership
    private void JQDLGbusroleProperties(System.Collections.Specialized.NameValueCollection pars, HttpResponse httpResponse)
    {
        IBusRole engineBR = new IBusRole(CONNdedicated);
      IBusRole engine = engineBR;

      int idbusrole;

      if (pars["JQDLGbp_id"].Length == 0)
        {

          // WE ARE CREATING A NEW BUS ROLE, NOT JUST UPDATING EXISTING

          int babyid = -1;

          try
            {
              babyid =
                engine.NewBusRole(
                                  pars["JQDLGbp_name"],
                                  pars["JQDLGbp_descr"], session.idSubprocess);
            }
          catch (Exception eee)
            {
              throw new Exception("Addition of new role failed: check for name already in use.");
            }
          idbusrole = babyid;
          // Default role type: Functional
          engineBR.SetBusRole(idbusrole, "F");
        }
      else
        {
          returnListBusRole[] chk =
            engineBR.ListBusRole(null, "(\"id\" <> ?)  AND (\"Name\" = ?)",
                                 new string[] { pars["JQDLGbp_id"], pars["JQDLGbp_name"] }, "");
          if (chk.Length > 0)
            {
              throw new Exception("Name '" + pars["JQDLGbp_name"] + "' is already in use for an existing business role.");
            }

          idbusrole = int.Parse(pars["JQDLGbp_id"]);
        }




      engineBR.SetBusRole(
                          idbusrole,
                          pars["JQDLGbp_name"],
                          pars["JQDLGbp_descr"],
                          pars["JQDLGbp_primown_eid"],
                          pars["JQDLGbp_secown_eid"],
                          pars["JQDLGbp_designdetails"]);
      engineBR.SetBusRole(idbusrole, pars["JQDLGbp_roletype"]);

      httpResponse.Write("OK");

    }









      private void SAP_deleteSapRole
        (System.Collections.Specialized.NameValueCollection P, HttpResponse httpResponse)
      {
          // SHOULD WE DISALLOW THIS IF THE MIRROR ITEM IS STILL IN USE ON BUSROLE SIDE?
          ISAProle engine = new ISAProle(CONNdedicated);
          returnGetSAProle trashee = engine.GetSAProle(int.Parse(P["id"]));

          // STEP 1: Try to delete its matching mirror image on the business side.
          // This is a TRUE deletion, not just an "X" status.
          // But this will THROW AN EXCEPTION if the item is in use in any way, even historically.
          SAP_HELPERS.DeleteMatchingBusinessEntitlementForSAPRole (trashee);



          // STEP 1: Rename the subprocess by adding the //DEL_... suffix, and also
          // move it into the trashcan subprocess.
          engine.SetSAProle(
              trashee.ID,
              trashee.Name + "//DEL_" + trashee.ID.ToString() + "//SUBPR_" + trashee.SubProcessID,
              trashee.Description,
              int.Parse(ConfigurationManager.AppSettings["IDsubprocessTrashcan"]),
              trashee.System,
              trashee.Platform,
              trashee.RoleActivity,
              trashee.RoleType, trashee.Comment);

      }




    // Creates or Updates a business role object.
    // Alos, if necessary, creates new user objects for role ownership
    private void JQDLGsaproleProperties
      (System.Collections.Specialized.NameValueCollection pars, HttpResponse httpResponse)
    {
        ISAProle engineBR = new ISAProle(CONNdedicated);
      ISAProle engine = engineBR;

      int idsaprole;



      if (pars["JQDLGbp_id"].Length == 0)
        {

          // WE ARE CREATING A NEW ROLE, NOT JUST UPDATING EXISTING

          int babyid = -1;

          // Make sure not already existing
          returnListSAProle[] chk =
            engineBR.ListSAProle(null, "(\"Name\" = ?) and (\"System\" = ?) and (\"Platform\" = ?)",
                                 new string[] { pars["JQDLGbp_name"], pars["JQDLGbp_system"], pars["JQDLGbp_platform"] }, "");
          if (chk.Length > 0)
          {
              // Already in existence!
              returnListSAProle details = chk[0];

              if (details.SubProcessID == session.idSubprocess)
              {
                  // And this existing role is even in the same subprocess!
               
                  throw new Exception("This SAP role already exists and is registered with this very same subprocess.");
              }
              else
              {
                  // Oh, belongs to a different subprocess.
                  IProcess engineProcess = new IProcess(engineBR.DbConnection);
                  ISubProcess engineSubProcess = new ISubProcess(engineBR.DbConnection);

                  returnGetSubProcess theSubProcess = engineSubProcess.GetSubProcess(details.SubProcessID);
                  returnGetProcess theProcess = engineProcess.GetProcess(theSubProcess.ProcessID);
                
                  throw new Exception
                      ("This SAP role already exists, in a different subprocess: " + theProcess.Name + " / " + theSubProcess.Name);
              }      
          }



          try
            {
              babyid =
                engine.NewSAProle(
                                  pars["JQDLGbp_name"],
                                  session.idSubprocess, pars["JQDLGbp_system"], pars["JQDLGbp_platform"]);
            }
          catch (Exception eee)
            {
              throw new Exception("Addition of new role failed: check for name already in use.");
            }
          idsaprole = babyid;
        }
      else
        {

          returnListSAProle[] chk =
            engineBR.ListSAProle(null, "(\"id\" <> ?)  AND (\"Name\" = ?)",
                                 new string[] { pars["JQDLGbp_id"], pars["JQDLGbp_name"] }, "");
          if (chk.Length > 0)
            {
              throw new Exception("Name '" + pars["JQDLGbp_name"] + "' is already in use for an existing SAP role.");
            }

          idsaprole = int.Parse(pars["JQDLGbp_id"]);
        }


      engineBR.SetSAProle(
                          idsaprole,
                          pars["JQDLGbp_name"],
                          pars["JQDLGbp_description"], session.idSubprocess,
                          pars["JQDLGbp_system"],
                          pars["JQDLGbp_platform"],
                          pars["JQDLGbp_roleactivity"],
                          pars["JQDLGbp_roletype"], "");

      // ADDED to fix regression, on 09MAR2010:
      returnGetSAProle saprolePostAction = engine.GetSAProle(idsaprole);
      SAP_HELPERS.MaintainMatchingBusinessEntitlementForSAPRole(saprolePostAction, saprolePostAction);

      httpResponse.Write("OK");
    }






    private void CreateWorkspaceOnEntAssSet(int idSubpr, int idUser, string comment)
    {
      HELPERS.WorkspaceCreate(HELPERS.NewOdbcConn_FORCE(), idSubpr, idUser, -1/*Use latest ACTIVE*/, comment);
    }




    private void PublishEntAssSet(int IDeas, string cmt)
    {
      HELPERS.PublishEntAssSet(IDeas, cmt);
    }

    private void PublishSAPEntAssSet(int IDeas, string cmt)
    {
      SAP_HELPERS.PublishSAPEntAssSet(IDeas, cmt);
    }


    private void CreateOrUpdateUserName
      (string eid, string surname, string firstname,
       bool doNotChangeExistingEntry)
    {
      IUser engine = new IUser(HELPERS.NewOdbcConn_FORCE());
      returnListUser[] ret = engine.ListUser(null, "\"EID\" LIKE ?", new string[] { eid.Trim() }, "");
      if (ret.Length == 1)
        {
          bool doCreate = false;
          if ( (ret[0].NameSurname == null) || (ret[0].NameFirst == null) ) {
            doCreate = true;
          }
          else if              (  (ret[0].NameSurname.Trim().Length < 2) ||
                                  (ret[0].NameFirst.Trim().Length < 2) ) {
         
            doCreate = true;
          }
          else if (!doNotChangeExistingEntry)
            {
              doCreate = true;
            }


          if (doCreate) {
            returnListUser extant = ret[0];
            engine.SetUser(extant.ID, eid, extant.Name, extant.PrivilegeLevel, surname, firstname);
          }
        }
      else if (ret.Length == 0)
        {
          int iduser = engine.NewUser(eid, eid);
          engine.SetUser(iduser, eid, eid, "roleowner", surname, firstname);
        }
    }




    // Responsible for emitting HTML that will be shown onscreen.
    // Given the name of an SAP Auth Obj, it generates a list of its registered fields.
    // Use dr.GetString(0) to obtain the field's name, and ...GetString(1) to obtain its descr.
    // Use dr.GetInt(2) to obtain its UUID.
      private OdbcDataReader InquireFieldListForAuthObj(int IDsapobj, OdbcConnection conn)
      {
          OdbcCommand cmd = new OdbcCommand();

          cmd.Connection = conn;

          cmd.CommandText = "SELECT SAF.c_u_Name, SAF.c_u_Description, SAF.c_id FROM t_RBSR_AUFW_r_SAPauthObjSAPauthField LINKAGE " +
            " LEFT OUTER JOIN t_RBSR_AUFW_u_SAPauthField SAF on SAF.c_id = LINKAGE.c_r_SAPauthField " +
            " WHERE LINKAGE.c_r_SAPauthObj = ? AND SAF.c_u_Status='A' ORDER BY SAF.c_u_Name;";
          cmd.Parameters.Add("c_r_SAPauthObj", OdbcType.Int);
          cmd.Parameters["c_r_SAPauthObj"].Value = (object)IDsapobj;

          OdbcDataReader dr = cmd.ExecuteReader();

          return dr;
      }



    private void EmitFieldList(int IDsapobjfield, HttpResponse OUT)
    {
      System.Data.Odbc.OdbcConnection conn =  HELPERS.NewOdbcConn_FORCE();
      OdbcDataReader dr = InquireFieldListForAuthObj(IDsapobjfield, conn);
      while (dr.Read())
        {
          OUT.Write(dr.GetString(0) + 
                    (
                     (dr.GetString(1).Length > 0) ?
                     " <span class='graydescr'>(" + dr.GetString(1) + ")</span>" : "" 
                     )
                    +
                    "<br/>\n");
        }
    }





    private void FindUserByEID(string eid, string callerdata, HttpResponse OUT)
    {
      IUser engine = new IUser(HELPERS.NewOdbcConn_FORCE());
      returnListUser[] ret = engine.ListUser(null, "\"EID\" LIKE ?", new string[] { eid }, "");
      if (ret.Length != 1)
        {
          OUT.Write(callerdata + "|" + "NOTFOUND");
        }
      else if (ret.Length == 1)
        {
          if (ret[0].NameSurname == null)
            {
              OUT.Write(callerdata + "|" + "USERID: " + ret[0].Name + "|" + "?");
            }
          else
            {
              OUT.Write(callerdata + "|" + ret[0].NameSurname + "|" + ret[0].NameFirst);
            }
        }
      else
        {
          OUT.Write(callerdata + "|" + "NOTFOUND");
        }
    }




    private void CloneBusRole(int idBusRole, string newBusRoleName, int idWorkspace, HttpResponse response)
    {
      IBusRole engineBR = new IBusRole(HELPERS.NewOdbcConn_FORCE());
      IEntAssignment engineEAS = new IEntAssignment(HELPERS.NewOdbcConn_FORCE());

      returnGetBusRole detailsBusRole = engineBR.GetBusRole(idBusRole);

      int idNewBRole;

      try
        {
          idNewBRole = engineBR.NewBusRole(newBusRoleName,
                                           detailsBusRole.Description, detailsBusRole.SubProcessID);
          // Also copy over the notes and the owner info as well.
          engineBR.SetBusRole
            (idNewBRole, newBusRoleName, detailsBusRole.Description, 
             detailsBusRole.OwnerPrimaryEID, detailsBusRole.OwnerSecondaryEID, detailsBusRole.DesignDetails);
          engineBR.SetBusRole(idNewBRole, detailsBusRole.RoleType_Abbrev);          
        }
      catch (Exception exxx)
        {
          response.Write("ERROR: The given name was found to already be in use.");
          return;
        }

      returnListEntAssignmentByEntAssignmentSet[] ret =
        engineEAS.ListEntAssignmentByEntAssignmentSet(null, " \"BusRole\" = ? AND \"Status\" NOT IN ('X') ",
                                                      new string[] { idBusRole.ToString() }, "", idWorkspace);

      for (int i = 0; i < ret.Length; i++)
        {
          engineEAS.NewEntAssignment
            (
             idWorkspace, idNewBRole, ret[i].EntitlementID, "N");
        }

      response.Write("The role was successfully cloned.  Number of entitlement assignments: " +
                     ret.Length);
    }



    public void EDIT_SingleRowSingleColumn
      (string tblname, string rowid, string colname, string newval)
    {
      string thesql =
        "UPDATE t_RBSR_AUFW_u_" + tblname +
        " SET " + colname + " = '" + newval.Replace("\'","\'\'") + "' WHERE c_id=" + rowid + ";";

      HELPERS.RunSql(thesql);
    }


    public void EDIT_MultiRowsSingleColumn
      (string tblname, string rowidlist, string colname, string newval)
    {
      if (rowidlist.StartsWith(","))
        rowidlist = "-3" + rowidlist;

      if (newval == null)
        {
          newval = " NULL ";
        }else{
        newval = "'" + newval.Replace("\'", "\'\'") + "'";
      }

      string thesql =
        "UPDATE t_RBSR_AUFW_u_" + tblname +
        " SET " + colname + " = " + newval + " WHERE c_id in (" + rowidlist + ");";

      HELPERS.RunSql(thesql);
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
