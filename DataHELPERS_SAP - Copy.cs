using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using System.Security.Cryptography;
using RBSR_AUFW.DB.IProcess;
using RBSR_AUFW.DB.ISubProcess;
using RBSR_AUFW.DB.IUser;
using RBSR_AUFW.DB.IEditingWorkspace;
using RBSR_AUFW.DB.IEventLog;
using RBSR_AUFW.DB.IWorkspaceEntitlement;
using RBSR_AUFW.DB.IEntAssignmentSet;
using RBSR_AUFW.DB.IEntitlement;
using RBSR_AUFW.DB.IEntAssignment;
using RBSR_AUFW.DB.IBusRole;
using RBSR_AUFW.DB.IMVFormula;
using System.Collections;
using RBSR_AUFW.DB.ISAProle;
using RBSR_AUFW.DB.ITcodeAssignmentSet;
using RBSR_AUFW.DB.ITcodeAssignment;
using RBSR_AUFW.DB.ITcodeEntitlement;
using RBSR_AUFW.DB.ITcodeDictionary;
using System.Collections.Generic;
using System.Text;
using RBSR_AUFW.DB.ISAPChangeManagementEvent;
using RBSR_AUFW.DB.ISAPauthClass;
using RBSR_AUFW.DB.ISAPauthObj;
using RBSR_AUFW.DB.ISAPauthField;
using RBSR_AUFW.DB.IOrgValue1252;
using RBSR_AUFW.DB.IAuthRow1251;

namespace _6MAR_WebApplication
{

  public class SAP_HELPERS
  {


    // DO NOT CHANGE THIS UNLESS YOU ALSO CHANGE THE 
    // prevSignature COMPUTATION CODE BELOW!!!
    public static string[] _entitlementFields =
      {
        "TCode",  /* [0] */
        "Activity",
        "ActivityFolder",
        "Type",
        "AccessLevel" /* [4] */
      };




    /*
      Returns an array:
      * entry 0:
      *   -1 : unknonw (not yet registered)
      *   else: the ID of the SAProle(name+plat) row
      * entry 1:
      *   ID of subprocess (if yes already registered)
      */
    public static int[] StatusOfSAPRoleNamePlatRegistration(string strSapRoleName, string strPlatform, OdbcConnection conn)
    {

      int[] RET = new int[2];
      ISAProle engine = new ISAProle(conn);
      returnListSAProle[] result = engine.ListSAProle
        (null, "\"name\"=? AND \"platform\"=? ", new string[] { strSapRoleName, strPlatform }, "");
      if (result.Length == 0)
        {
          RET[0] = -1;
          RET[1] = -1;
          return RET;
        }
      if (result.Length > 1)
        {
          throw new Exception("Multiple SAP roles with exact same name+platform combination: " + strSapRoleName + "(" + strPlatform + ")");
        }
      RET[0] = result[0].ID;
      RET[1] = result[0].SubProcessID;
      return RET;
    }





    // Returns the ID of the new SAP role.
    // If already exists, it simply retursn the ID of the existing role and returns 
    //    false in the ref parameter.
    public static int CreateSAPRole
    (ISAProle engine, string rolename, int idSubprocess,
     string system, string description, string platform,
     string roleactivity, string roletype, string comment, out bool wasCreated)
    {
      returnListSAProle[] ret =
        engine.ListSAProle(null, "\"Name\" = ?", new string[] { rolename }, "");
      if (ret.Length == 1)
        {
          wasCreated = false;
          return ret[0].ID;
        }

      // IF NEEDS TO BE CREATED
      wasCreated = true;
      int IDnew = engine.NewSAProle(rolename, idSubprocess,
                                    system, platform);

      engine.SetSAProle(IDnew, rolename, description, idSubprocess,
                        system, platform, roleactivity, roletype, comment);

      return IDnew;
    }
      










    /* Returns an array of error messages */
    /* Special situations:
       If class already known and description is unchanged, silently ignore.
       If class already known and description is different:
       Issue warning
       Overwrite the existing description
       If class unknown, silently add.
    */
    public static Queue ImportListSAPauthobjClasses(OdbcConnection conn, DataTable dt, int IDsubpr)
    {
      ISAProle engine = new ISAProle(conn);
      Queue errMsgs = new Queue();
      int countNewCreated = 0;

      IEnumerator<System.Data.DataRow> x =
        (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();


      ISAPauthClass localengine = new ISAPauthClass(conn);

      while (x.MoveNext())
        {

          if (x.Current[0].Equals(System.DBNull.Value))
            {
              // Ignore any line with no value in the first field.
              // Often the end of the csv file has just lots of blank rows.
              continue;
            }


          // [0]: Class Name
          //      Description
          string s = x.Current["Name"].ToString();
          string descr = x.Current["Description"].ToString();


          returnListSAPauthClass[] retval =
            localengine.ListSAPauthClass(null, "\"Name\" = ?", new string[] { s }, "");

          if (retval.Length == 1)
            {
              // This is already in the dictionary.
              // But did the description change?
              if (descr != retval[0].Description)
                {
                  // Yes the description did change.
                  // Issue a warning, and honor the NEW description.
                  errMsgs.Enqueue
                    (
                     "Warning: class '" + s + "' has been given an updated description:");
                  errMsgs.Enqueue
                    ("   Previous: " + retval[0].Description);
                  errMsgs.Enqueue
                    ("   New: " + descr);
                  localengine.SetSAPauthClass
                    (retval[0].ID, s, descr, retval[0].Status);
                }
            }
          else {
            // CREATING A NEW DICT ENTRY
            int IDnew = localengine.NewSAPauthClass(s, descr, "A");
            countNewCreated++;
          }
        }
      errMsgs.Enqueue("-------------");
      errMsgs.Enqueue("Number of new classes registered: " + countNewCreated.ToString());

      return errMsgs; 
    }











    /* Returns an array of error messages */
    /* This is not just filling a regular table.
     * It is also adding linkage rows to a "r"-style Rise table
     * to record the N-to-N linkage between authobjs and authfields.
     * The upload process does not ever *delete* a linkage, it only
     * adds linkage rows to the "r" table.
     /* Special situations:
     If field already known and description is unchanged, silently ignore.
     If field already known and description is different:
     Issue warning
     Overwrite the existing description
     If obj unknown, log ERROR message, add nothing for that row, continue.
    */
    public static Queue ImportDictSAPauthobjFields(OdbcConnection conn, DataTable dt, int IDsubpr)
    {
      ISAProle engine = new ISAProle(conn);
      Queue errMsgs = new Queue();
      int countNewCreated = 0;
      int countRegistrationFailed = 0;

      System.Data.Odbc.OdbcConnection conn2 =
	new System.Data.Odbc.OdbcConnection(
					    ConfigurationManager.AppSettings["DBconnstr"]);
      conn2.Open();

     

      IEnumerator<System.Data.DataRow> x =
        (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();


      ISAPauthClass localengineClass = new ISAPauthClass(conn);
      ISAPauthObj localengineAObj = new ISAPauthObj(conn);
      ISAPauthField localengine = new ISAPauthField(conn);

      while (x.MoveNext())
        {

          if (x.Current[0].Equals(System.DBNull.Value))
            {
              // Ignore any line with no value in the first field.
              // Often the end of the csv file has just lots of blank rows.
              continue;
            }


          // [0]: Object Name
          // [1]: Field name
          // [2]: Description
          string theObject = x.Current["Object"].ToString();
          string s = x.Current["Name"].ToString();
          string descr = x.Current["Description"].ToString();

          int IDobj;

          returnListSAPauthObj[] retvalObjLookup =
            localengineAObj.ListSAPauthObj(null, "\"Name\" = ?", new string[] { theObject }, "");
          if (retvalObjLookup.Length == 0)
            {
              // ERROR: the object name is unknown!
              errMsgs.Enqueue
                (
                 "ERROR: auth object '" + theObject + "' is unknown:");
              countRegistrationFailed++;
            }
          else
            {
                IDobj = retvalObjLookup[0].ID;

              returnListSAPauthField[] retval =
                localengine.ListSAPauthField(null, "\"Name\" = ?", new string[] { s }, "");

              int IDfield;

              if (retval.Length == 1)
                {
                  // This is already in the dictionary.
                  // But did the description change?
                  IDfield = retval[0].ID;
                  if (descr != retval[0].Description)
                    {
                      // Yes the description did change.
                      // Issue a warning, and honor the NEW description.
                      errMsgs.Enqueue
                        (
                         "Warning: auth field '" + s + "' has been given an updated description:");
                      errMsgs.Enqueue
                        ("   Previous: " + retval[0].Description);
                      errMsgs.Enqueue
                        ("   New: " + descr);
                      localengine.SetSAPauthField
                        (retval[0].ID, s, descr, retval[0].Status);
                    }
                }
              else
                {
                  // CREATING A NEW DICT ENTRY
                  int IDnew = localengine.NewSAPauthField(s,"A");
                  localengine.SetSAPauthField(IDnew, s, descr, "A");
                  IDfield = IDnew;
                  countNewCreated++;
                }

              // If not yet present, create the linkage.
                RecordLinkFromAuthFieldToAuthObj(conn, IDfield, IDobj);
            }
        }
      errMsgs.Enqueue("-------------");
      errMsgs.Enqueue("Number of new fields registered: " + countNewCreated.ToString());
      errMsgs.Enqueue("Number of rows ignored due to error: " + countRegistrationFailed.ToString());

      return errMsgs;
    }








    /* Returns an array of error messages */
    /* Special situations:
       If obj already known and description is unchanged, silently ignore.
       If obj already known and description is different:
       Issue warning
       Overwrite the existing description
       If class unknown, log ERROR message, add nothing for that row, continue.
    */
    public static Queue ImportListSAPauthobjs(OdbcConnection conn, DataTable dt, int IDsubpr)
    {
      ISAProle engine = new ISAProle(conn);
      Queue errMsgs = new Queue();
      int countNewCreated = 0;
      int countRegistrationFailed = 0;

      IEnumerator<System.Data.DataRow> x =
        (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();


      ISAPauthClass localengineClass = new ISAPauthClass(conn);
      ISAPauthObj localengine = new ISAPauthObj(conn);

      while (x.MoveNext())
        {

          if (x.Current[0].Equals(System.DBNull.Value))
            {
              // Ignore any line with no value in the first field.
              // Often the end of the csv file has just lots of blank rows.
              continue;
            }


          // [0]: Class Name
          // [1]: Object name
          // [2]: Description
          string theclass = x.Current["Class"].ToString();
          string s = x.Current["Name"].ToString();
          string descr = x.Current["Description"].ToString();


          returnListSAPauthClass[] retvalClassLookup =
            localengineClass.ListSAPauthClass(null, "\"Name\" = ?", new string[] { theclass }, "");
          if (retvalClassLookup.Length == 0)
            {
              // ERROR: the class name is unknown!
              errMsgs.Enqueue
                (
                 "ERROR: class '" + theclass + "' is unknown.");
              countRegistrationFailed++;
            }
          else
            {

              returnListSAPauthObj[] retval =
                localengine.ListSAPauthObj(null, "\"Name\" = ?", new string[] { s }, "");


              if (retval.Length == 1)
                {
                  // This is already in the dictionary.
                  // But did the description change?
                  if (descr != retval[0].Description)
                    {
                      // Yes the description did change.
                      // Issue a warning, and honor the NEW description.
                      errMsgs.Enqueue
                        (
                         "Warning: auth object '" + s + "' has been given an updated description:");
                      errMsgs.Enqueue
                        ("   Previous: " + retval[0].Description);
                      errMsgs.Enqueue
                        ("   New: " + descr);
                      localengine.SetSAPauthObj
                        (retval[0].ID, s, descr, retval[0].SAPauthClassID,
                            
                         retval[0].Status);
                    }
                }
              else
                {
                  // CREATING A NEW DICT ENTRY
                  int IDnew = localengine.NewSAPauthObj(s, descr, retvalClassLookup[0].ID, "A");
                  countNewCreated++;
                }
            }
        }
      errMsgs.Enqueue("-------------");
      errMsgs.Enqueue("Number of new objects registered: " + countNewCreated.ToString());
      errMsgs.Enqueue("Number of rows ignored due to error: " + countRegistrationFailed.ToString());

      return errMsgs;
    }







    /* Returns an array of error messages */
    public static Queue ImportListSAProlenames(OdbcConnection conn, DataTable dt, int IDsubpr)
    {
      ISAProle engine = new ISAProle(conn);
      Queue errMsgs = new Queue();

      IEnumerator<System.Data.DataRow> x =
        (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();

      while (x.MoveNext())
        {

          if (x.Current[0].Equals(System.DBNull.Value))
            {
              // Ignore any line with no value in the first field.
              // Often the end of the csv file has just lots of blank rows.
              continue;
            }

          // [0]: Name
          //      Description
          //      System
          // [3]: Platform
          string s = x.Current["SAPRoleName"].ToString();

          int[] stat = StatusOfSAPRoleNamePlatRegistration(s, x.Current["Platform"].ToString(), conn);

          if (stat[0] == -1)
            {

              // CREATING A NEW SAP ROLE
              int IDnew = engine.NewSAProle(s, IDsubpr, x.Current["System"].ToString(), x.Current["Platform"].ToString());
              engine.SetSAProle(IDnew, s, x.Current["Description"].ToString(), IDsubpr,
                                x.Current["System"].ToString(), x.Current["Platform"].ToString(),
                                x.Current["StandardActivity"].ToString(), 
                                x.Current["RoleType"].ToString(), "");

              // CREATING ITS BROTHER ON THE BUS.ENTITLEMENT SIDE OF THE FENCE
              returnGetSAProle newval = engine.GetSAProle(IDnew);
              MaintainMatchingBusinessEntitlementForSAPRole(newval,newval);
            }
          else
            {
              if (stat[1] != IDsubpr)
                {
                  errMsgs.Enqueue
                    (
                     "ERROR: SAP role " + s + " is already registered with a different subprocess and thus cannot be registered with this subprocess."
                     );
                }
            }
        }
      return errMsgs;
    }






      /*
       * This method is invoked from the "Import TCode Entitlements" GUI. 
       * 
       * This returns TRUE if completed entire input set, or FALSE if caller needs to call again to continue processing.
       */
    public static bool ImportSAPAuthFrameworkFromCSV
    (DataTable dt, int IDuser, int IDsubpr, System.Data.Odbc.OdbcConnection conn,
     Queue messagesToReturn, /*string comment,*/ int IDnewWS,
        int startat, int count)
    {
      
        
      IUser Iuser = new IUser(conn);
      ISubProcess Isubpr = new ISubProcess(conn);
      ITcodeAssignmentSet Itaset = new ITcodeAssignmentSet(conn);
      ITcodeAssignment Itass = new ITcodeAssignment(conn);
      ITcodeEntitlement Itce = new ITcodeEntitlement(conn);
      ITcodeDictionary Itdict = new ITcodeDictionary(conn);
      ISAProle engineSaprole = new ISAProle(conn);


      System.Data.Odbc.OdbcConnection conn2 =
        new System.Data.Odbc.OdbcConnection(
                                            ConfigurationManager.AppSettings["DBconnstr"]);
      conn2.Open();


      if (dt == null)
        {
          return;
        }



      // The RISE-generated Get functions are only useful for a read-only view of fields;
      // not for generating subordinate entities.
      returnGetTcodeAssignmentSet neweas = Itaset.GetTcodeAssignmentSet(IDnewWS);

      IEnumerator<System.Data.DataRow> x =
        (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();

      //                Hashtable signatures = new Hashtable();

      int recordseq = 0;

      Queue msgsWarn = messagesToReturn;

      int countAdded = 0;  // count of EntAssignments added, not of Ents added
      int countIgnored = 0;
      int countAlreadyExtant = 0;
      int countEchosDeleted = 0;

      while (x.MoveNext())
        {
            if (recordseq < startat)
            {
                recordseq++;
                continue;
            }

          recordseq++;

          if ( (count>0) && ((recordseq - startat) > count) )
          {
              return false;
          }


          if (x.Current[0].Equals(System.DBNull.Value))
            {
              // Ignore any line with no value in the first field.
              // Often the end of the csv file has just lots of blank rows.
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because first field blank");
              countIgnored++;
              continue;
            }


          string strTcodeDescr = null;
          string strTcode = null;
          try
            {
              strTcode = (string)(x.Current["TCode"]);
            }
          catch (Exception eBusrole)
            {
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because required TCode field blank.");
              countIgnored++;
              continue;
            }



          /* BACK WHEN WE ADDED TCODES AUTOMATICALLY WHEN NEEDED:

             strTcodeDescr = GrabOptionalField(x, "Entitlement Description");
             // Make sure the TCode exist in the dictionary
             // The TCode dictionary is global, not scoped in any way
             if (RegisterTCode(Itdict, strTcode, strTcodeDescr))
             {
             msgsWarn.Enqueue("NEW: TCode " + strTcode + " was unfamiliar and has been registered in our dictionary.");
             }
           
          */


          if (!KnownTCode(Itdict, strTcode))
            {
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because unknown TCode ["
                               + strTcode + "]");
              countIgnored++;
              continue;
            }


          // Grab the other fields.
          string FLDactivityfolder = GrabOptionalField(x, "ActivityFolder");
          string FLDplatform = GrabOptionalField(x, "Platform");
          string FLDrolename = GrabOptionalField(x, "RoleName");
          string FLDtype = GrabOptionalField(x, "Type");
          string FLDaccesslevel = GrabOptionalField(x, "AccessLevel");
          string FLDcommentary = GrabOptionalField(x, "Comment");

          // Access level is required and not-defaulted
          switch (FLDaccesslevel[0])
            {
            case 'U':
            case 'D':
              break;
            default:
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because missing or invalid AccessLevel");
              countIgnored++;
              continue;
            }

          // Type (Menu or Background) is required and not-defaulted
          switch (FLDtype[0])
            {
            case 'M':
            case 'B':
              break;
            default:
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because missing or invalid Type");
              countIgnored++;
              continue;
            }







          /*
           * We require that the SAP role + platform combo already be known
           */

          returnListSAProle[] foundsaproles =
            engineSaprole.ListSAProle
            (null, " \"Name\"=?  AND  \"SubProcess\"=?  AND   \"Platform\"=?   ",
             new string[] { FLDrolename, IDsubpr.ToString(), FLDplatform }, "");
          if (foundsaproles.Length < 1)
            {
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because SAP role+platform [" +
                               FLDrolename + " + " + FLDplatform + "] unknown to this subprocess.");
              countIgnored++;
              continue;
            }
          if (foundsaproles.Length > 1)
            {
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because SAP role+platform [" +
                               FLDrolename + " + " + FLDplatform + "] is represented twice in this subprocess.");
              countIgnored++;
              continue;
            }



          // Check to see if the entitlement already exists.
          // This has NOTHING to do with the entitlement's being assigned (yet), or not, to this workspace.
          bool isNewTcEnt;
          int IDtent;
          CreateTcodeEntitlementIfNotExists
            (Itce, strTcode, FLDactivityfolder, FLDtype, FLDaccesslevel, FLDcommentary,
             out isNewTcEnt, out IDtent);





          int IDtass;
          char createTassStatus =
            CreateTcodeAssignmentIfNotExists(IDnewWS, Itass, foundsaproles[0].ID, isNewTcEnt, IDtent, false, out IDtass);
          // Return value: "A" means it was added, "E" means it already existed
          if (createTassStatus == 'E')
            {
              msgsWarn.Enqueue("NOTE: Record " + recordseq + ": Ignored because already present in assignment set.");
              countAlreadyExtant++;
            }
          if (createTassStatus == 'A')
            {
              countAdded++;
            }




          // We now have the ID of the "golden assignment vector", i.e. the assignment vector created or found
          //   by CreateTcodeAssignmentIfNotExists above.
          // ADDED: 31 March 2010:  We must make sure that NO OTHER
          // assignment in this assignmentset refers to the same TCode as the golden assvec.
          // Any other assignment that refers to the same TCode must be automatically killed!
           countEchosDeleted = RemoveOtherAssignmentsOfThisTCodeToThisRole
            (conn, IDnewWS,
             foundsaproles[0].ID, strTcode,
                /*the golden one, to be protected:*/ IDtass,
            msgsWarn);


        }

        return true;

        /*
      msgsWarn.Enqueue("-------------------");
      msgsWarn.Enqueue("Number of entitlements added to this role: " + countAdded);
      msgsWarn.Enqueue("Number of records ignored: " + countIgnored);
      msgsWarn.Enqueue("Number of entitlements not added because already present: " + countAlreadyExtant);
      msgsWarn.Enqueue("Number of previous assignments deleted due to TCode override: " + countEchosDeleted);
         */
    }





      /*
       * Added on 31 March 2010: 
       * Simplifies the ensuring that each workspace or TcodeAssSet has at most
       * one reference to any particular 
       */
      private static int RemoveOtherAssignmentsOfThisTCodeToThisRole
          (OdbcConnection conn, 
           int IDtassSet, 
          int IDsapRole,
          string strTcode, int IDtassToProtect, Queue msgsWarn)
      {
          int toReturn = 0;


          OdbcCommand cmd = new OdbcCommand();
          cmd.Connection = conn;


          cmd.CommandText =
           @"
SELECT 

TASS.c_id

FROM t_RBSR_AUFW_u_TcodeAssignment  TASS

LEFT OUTER JOIN t_RBSR_AUFW_u_TcodeEntitlement TENT
    ON TENT.c_id = TASS.c_r_TcodeEntitlement


WHERE "
          + " (TASS.c_u_EditStatus & 4) = 0 "
          + " AND "
          + " (TASS.c_r_TcodeAssignmentSet = " + IDtassSet.ToString() + ")"
          + " AND "
          + " (TASS.c_r_SAProle = " + IDsapRole.ToString() + ")"
          + " AND "
          + " (TENT.c_u_TCode = '" + strTcode + "')"
          + " AND "
          + " (TASS.c_id <> " + IDtassToProtect.ToString() + ")"
          + " ;";



          Queue<int> QtoBeDeleted = new Queue<int>();
               OdbcDataReader dr = cmd.ExecuteReader();
      while (dr.Read())
      {
          int tobedeleted = dr.GetInt32(0);
          QtoBeDeleted.Enqueue(tobedeleted);
      }
      dr.Close();
     



          // Now we have the list of assignments that need to be deleted.

      cmd.CommandText = @"
UPDATE t_RBSR_AUFW_u_TcodeAssignment SET c_u_EditStatus = c_u_EditStatus | 4 WHERE c_id IN (";

      toReturn = QtoBeDeleted.Count;
      while (QtoBeDeleted.Count > 0)
      {
          cmd.CommandText += QtoBeDeleted.Dequeue();
          cmd.CommandText += ", ";
      }
      cmd.CommandText += " -321);";
      cmd.ExecuteNonQuery();
      cmd.Dispose();

      return toReturn;
      }





















    public static void ImportSAPOrgVectorsFromCSV
    (DataTable dt, int IDuser, int IDsubpr, System.Data.Odbc.OdbcConnection conn,
     Queue messagesToReturn, string comment, int IDnewWS)
    {
      IUser Iuser = new IUser(conn);
      ISubProcess Isubpr = new ISubProcess(conn);
      ITcodeAssignmentSet Itaset = new ITcodeAssignmentSet(conn);
      ISAProle engineSaprole = new ISAProle(conn);
      IOrgValue1252 engine1252 = new IOrgValue1252(conn);


      System.Data.Odbc.OdbcConnection conn2 =
        new System.Data.Odbc.OdbcConnection(
                                            ConfigurationManager.AppSettings["DBconnstr"]);
      conn2.Open();


      if (dt == null)
        {
          return;
        }



      // The RISE-generated Get functions are only useful for a read-only view of fields;
      // not for generating subordinate entities.
      returnGetTcodeAssignmentSet neweas = Itaset.GetTcodeAssignmentSet(IDnewWS);

      IEnumerator<System.Data.DataRow> x =
        (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();

      int recordseq = 0;

      Queue msgsWarn = messagesToReturn;

      int countAdded = 0;  // count of vectors added to the 1252 table
      int countIgnored = 0;  // e.g. SAP role unknown or not registered in this subprocess scope
      int countAlreadyExtant = 0;

      while (x.MoveNext())
        {
          recordseq++;

          if (x.Current[0].Equals(System.DBNull.Value))
            {
              // Ignore any line with no value in the first field.
              // Often the end of the csv file has just lots of blank rows.
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because first field blank");
              countIgnored++;
              continue;
            }


          string nameSAProle = null;
          try
            {
              nameSAProle = (string)(x.Current["AGR_NAME"]);
            }
          catch (Exception eBusrole)
            {
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because required AGR_NAME field blank.");
              countIgnored++;
              continue;
            }


          // Grab the other fields.
          string FLDvariablename = GrabOptionalField(x, "VARBL");
          string FLDlow = GrabOptionalField(x, "LOW");
          string FLDhigh = GrabOptionalField(x, "HIGH");


          /*
           * We require that the SAP role already be known
           */

          returnListSAProle[] foundsaproles =
            engineSaprole.ListSAProle
            (null, " \"Name\"=?  AND  \"SubProcess\"=?  ",
             new string[] { nameSAProle, IDsubpr.ToString() }, "");
          if (foundsaproles.Length < 1)
            {
              countIgnored++;
              if (countIgnored == 100)
                {
                  msgsWarn.Enqueue
                    (" ** No longer emitting unknown-SAP-role warnings - exceeding max warning count ** ");
                }
              if (countIgnored < 100)
                {
                  msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because SAP role [" +
                                   nameSAProle + "] is unknown to this subprocess.");
                }
              continue;
            }
          if (foundsaproles.Length > 1)
            {
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Multiple SAP role+plat pairs are present for SAP rolename [" +
                               nameSAProle + "]. One is being chosen at random to 'house' this uploaded vector.");
              continue;
            }


          // "A" means it was added, "E" means it already existed
          int IDtass;
          char createTassStatus =
            CreateOrgValueAssignmentIfNotExists
            (IDnewWS, engine1252, foundsaproles[0].ID, FLDvariablename, FLDlow, FLDhigh,
             2, out IDtass);
          if (createTassStatus == 'E')
            {
              //msgsWarn.Enqueue("NOTE: Record " + recordseq + ": Ignored because already present.");
              countAlreadyExtant++;
            }
          if (createTassStatus == 'A')
            {
              countAdded++;
            }

        }

      msgsWarn.Enqueue("-------------------");
      msgsWarn.Enqueue("Number of entitlements added: " + countAdded);
      msgsWarn.Enqueue("Number of records ignored: " + countIgnored);
      msgsWarn.Enqueue("Number not added because already present: " + countAlreadyExtant);

    }





    /* Returns:
     * "A" if added
     * "E" if already was present
     * 
     * Logic added on 10MAR2010: If already present but with
     * editstatus of DELETED, this should simply remove the
     * DELETED status bit from the existing row.
     */
    public static char CreateOrgValueAssignmentIfNotExists
    (int IDnewWS, IOrgValue1252 engine1252, int idSAProle, 
     string FLDvariablename, string FLDlow, string FLDhigh,
     int editStatusIfIndeedCreated, out int IDtass)
    {
      string criteria = " \"FieldName\" LIKE ? ";
      Queue<string> critvals = new Queue<string>();
      critvals.Enqueue(FLDvariablename);

      // string[] critvals = new string[]{FLDvariablename};

      if (SafeString(FLDlow).Length > 0) {
        critvals.Enqueue(FLDlow);
        criteria += " AND \"RangeLow\" LIKE ? ";
      }
      else {
        criteria += " AND \"RangeLow\" IS NULL ";
        FLDlow = null;
      }

      if (SafeString(FLDhigh).Length > 0) {
        critvals.Enqueue(FLDhigh);
        criteria += " AND \"RangeHigh\" LIKE ? ";
      }
      else {
        criteria += " AND \"RangeHigh\" IS NULL ";
        FLDhigh = null;
      }


      returnListOrgValue1252ByTcodeAssignmentSet[] retval =
        engine1252.ListOrgValue1252ByTcodeAssignmentSet
        (null, criteria, critvals.ToArray(), "", IDnewWS);
      if (retval.Length > 0)
        {
          IDtass = retval[0].ID;
          // Caveat: this row found by the search facility may have its
          // edit-status "DELETE" bit turned on!  If so, it must be turned off,
          // i.e. an "undelete" operation must occur.
          if ((retval[0].EditStatus & 4) != 0)
            {
              engine1252.SetOrgValue1252
                (IDtass, retval[0].FieldName, retval[0].RangeLow, retval[0].RangeHigh,
                 retval[0].TcodeAssignmentSetID, retval[0].SAProleID,
                 (retval[0].EditStatus & 11));
            }
          return 'E';
        }

      int baby = engine1252.NewOrgValue1252
        (FLDvariablename, IDnewWS, idSAProle, editStatusIfIndeedCreated);
      engine1252.SetOrgValue1252
        (baby, FLDvariablename, FLDlow, FLDhigh, IDnewWS, idSAProle, editStatusIfIndeedCreated);

      IDtass = baby;
      return 'A';
    }






    public static char CreateTcodeAssignmentIfNotExists
    (int IDtcassSet, ITcodeAssignment Itass, int IDsaprole, bool isNewTcEnt, int IDtent, 
     bool testonly /* if true, does no write operations but simply returns the correct action code */ ,
     out int IDtcass)
    {
      IDtcass = -1;  /* this is the return value if testonly==true */

      if (!isNewTcEnt)
        {
          // Check to see if this workspace already assigns this tcent to this saprole
          returnListTcodeAssignmentByTcodeAssignmentSet[] listTass
            = Itass.ListTcodeAssignmentByTcodeAssignmentSet(
                                                            null,
                                                            " (\"TcodeEntitlement\" = ?) AND (\"SAProle\" = ?)",
                                                            new string[] { IDtent.ToString(), IDsaprole.ToString() },
                                                            "", IDtcassSet);
          if (listTass.Length == 1)
            {
              // Workspace already knows about this particular assignment.
              // But perhaps it is deleted and needs to be undeleted?
              IDtcass = listTass[0].ID;
              if ((listTass[0].EditStatus & 4) > 0)
                {
                  // Indeed, it needed to be undeleted.  So this is just like a successful addition.
                  if (!testonly)
                    RecordDeletionOfEntitlementAssignmentRow(IDtcass, 'U'); //Force UNdeletion ('U')
                  return 'A';
                }
              else
                {
                  return 'E';
                }
            }
        }
      else
        {
          // If we are being told the TCentvector is brand new, then
          // by definition, there is no existing TCassignment and thus
          // no logic need be done here.  We just uncond'ly go on to the
          // birthing logic.
        }


      //-----------------
      // GIVE BIRTH !
      //
      if (!testonly)
        {
          int babytass = Itass.NewTcodeAssignment(IDtcassSet, IDsaprole, IDtent);
          Itass.SetTcodeAssignment(babytass, IDtcassSet, IDsaprole, IDtent,
                                   2,  // Status of NEW 
                                   "Uploaded");
          IDtcass = babytass;
        }

      return 'A';
    }













    public static void CreateTcodeEntitlementIfNotExists
    (ITcodeEntitlement Itce, string strTcode, 
     string FLDactivityfolder,
     string FLDtype,
     string FLDaccesslevel, 
     string comment,
     out bool isNewTcEnt, out int IDtent)
    {

      // Ensure these one-letter fields are truncated: "Display">>"D"
      FLDtype = FLDtype.Substring(0, 1);
      FLDaccesslevel = FLDaccesslevel.Substring(0, 1);

      string checksum =
        SAP_HELPERS.ENTCHECKSUM(strTcode, FLDactivityfolder, FLDtype, FLDaccesslevel);
        
      isNewTcEnt = false;


      returnListTcodeEntitlement[] listtent =
        Itce.ListTcodeEntitlement(null, " \"CHECKSUM\" = ?", new string[] { checksum }, "");
      if (listtent.Length > 0)
        {
          IDtent = listtent[0].ID;
        }
      else
        {
          IDtent = Itce.NewTcodeEntitlement(strTcode);
         
          Itce.SetTcodeEntitlement
            (IDtent, strTcode, checksum, FLDactivityfolder, FLDtype, FLDaccesslevel, comment);

          isNewTcEnt = true;
        }
    }



    // returns null if nothing in that particular field
    private static string GrabOptionalField(IEnumerator<DataRow> x, string p)
    {
      try
        {
          return (string)(x.Current[p]);
        }
      catch (Exception ee)
        {
          return null;
        }
    }




    // Returns true if indeed this was a new TCode.
    private static bool RegisterTCode
    (ITcodeDictionary Idict, string strTcode, string strTcodeDescr)
    {

      string tcodeshortname = strTcode;
      returnListTcodeDictionary[] xxx = Idict.ListTcodeDictionary
        (null, "\"TcodeID\" like ?",
         new string[] { tcodeshortname }, "");
      if (xxx.Length < 1)
        {
          // MUST ADD NEW ONE
          int baby = Idict.NewTcodeDictionary(tcodeshortname);
          Idict.SetTcodeDescription(baby, strTcodeDescr);
          return true;
        }
      return false;
    }





    // Returns true if indeed this was a new TCode.
    private static bool KnownTCode
    (ITcodeDictionary Idict, string strTcode)
    {
      string tcodeshortname = strTcode;
      returnListTcodeDictionary[] xxx = Idict.ListTcodeDictionary
        (null, "\"TcodeID\" like ?",
         new string[] { tcodeshortname }, "");
      if (xxx.Length < 1)
        {
          return false;
        }
      else {
        return true;
      }
    }



    public static string ENTCHECKSUM
    (string tcode, string activityfolder, 
     string type /* Menu, Display */, 
     string accesslevel)
    {
      MD5CryptoServiceProvider engine = new MD5CryptoServiceProvider();
      UnicodeEncoding uniengine = new UnicodeEncoding();

      /* This was a field that was in use a short while, now retired */
      string activity = "";


      byte[] readyToHash = uniengine.GetBytes(
                                              tcode + "|@|" +
                                              activity + "|@|" +
                                              activityfolder + "|@|" +
                                              type + "|@|" +
                                              accesslevel);

      byte[] hashed = engine.ComputeHash(readyToHash);
      StringBuilder sb = new StringBuilder();
      foreach (byte b in hashed)
        {
          sb.Append(b.ToString("x2").ToUpper());
        }
      return sb.ToString();

    }




      /*
       * This version was found to be too strict.
       * If the entitlement had ever been in use in even an old archived workspace,
       * deletion was not allowed and this failed.
       * 
       * This version is being kept only for historical sake.
       */
    public static void DeleteMatchingBusinessEntitlementForSAPRole____OBSOLETE____
    (returnGetSAProle prevdef)
    {
      IEntitlement engineBusEnts = new IEntitlement(HELPERS.NewOdbcConn());
      returnListEntitlement[] matchingBusEnts = engineBusEnts.ListEntitlement
        (null,
         " \"Application\"=? AND \"Platform\" = ? AND \"EntitlementValue\"=? ",
         new string[] { "SAP", prevdef.Platform, prevdef.Name },
         "");
      if (matchingBusEnts.Length > 1)
        {
          throw new Exception("Multiple business entitlements exist represent SAP role+platform: "
                              + prevdef.Name + "+" + prevdef.Platform);
        }

      int IDmatchingBusEnt;

      if (matchingBusEnts.Length == 1)
        {
          // Already exists, so just DELETE if possible.
          IDmatchingBusEnt = matchingBusEnts[0].ID;
          try
            {
              engineBusEnts.DeleteEntitlement
                (IDmatchingBusEnt);
            }
          catch (Exception exc)
            {
              throw new Exception("Could not delete matching business entitlement due to in-use status");
            }
        }
    }







      public static void DeleteMatchingBusinessEntitlementForSAPRole
(returnGetSAProle prevdef)
      {
          IEntitlement engineBusEnts = new IEntitlement(HELPERS.NewOdbcConn());
          returnListEntitlement[] matchingBusEnts = engineBusEnts.ListEntitlement
            (null,
             " \"Application\"=? AND \"Platform\" = ? AND \"EntitlementValue\"=? ",
             new string[] { "SAP", prevdef.Platform, prevdef.Name },
             "");
          if (matchingBusEnts.Length > 1)
          {
              throw new Exception("Multiple business entitlements exist representing this SAP role+platform: "
                                  + prevdef.Name + "+" + prevdef.Platform);
          }

          int IDmatchingBusEnt;

          if (matchingBusEnts.Length == 1)
          {

              // THIS SECTION CHANGED RADICALLY ON 19 MAY 2010
              // See the google document "R-AF TECHNICAL DIARY" for details.

              // Our only goal is to make sure there is no currently in-use
              // assignment (in an active ass-set) that refers to the mirror
              // entitlement.

              IDmatchingBusEnt = matchingBusEnts[0].ID;

              string thesql = @"
              SELECT

EA.*


FROM t_RBSR_AUFW_u_EntAssignment EA

LEFT OUTER JOIN t_RBSR_AUFW_u_Entitlement ENT 
   ON ENT.c_id=EA.c_r_Entitlement 

LEFT OUTER JOIN t_RBSR_AUFW_u_EntAssignmentSet EASET 
   ON EASET.c_id=EA.c_r_EntAssignmentSet

WHERE 
EA.c_u_Status in ('N','A') 
AND
ENT.c_id = " + IDmatchingBusEnt + @"
AND
EASET.c_u_Status = 'ACTIVE' ";

              OdbcDataReader DR;

              DR = HELPERS.RunSqlSelect(thesql);

              if (DR.HasRows) {
                  throw new Exception("Could not delete matching business entitlement due to in-use status");                  
              }
          }
      }
  
       





    /*
      This is called in both the case of a known-to-be-new SAP role
      and in the case of a modification of the characteristics of an extant
      SAP role. 
    */
    public static void MaintainMatchingBusinessEntitlementForSAPRole
    (returnGetSAProle prevdef, returnGetSAProle newdef)
    {
      // Obtain details about the SAP role, all its characteristics
      ISAProle engineSAPRole = new ISAProle(HELPERS.NewOdbcConn());



      // Some of the SAP role optional fields are not optional on the BusEnt side.
      // Fill them with a null-meaning value.
      if ((newdef.RoleActivity == null) || (newdef.RoleActivity == ""))
        {
          newdef.RoleActivity = "n/a";
        }


      // Check to see if already present in the list of business role entitlements
      IEntitlement engineBusEnts = new IEntitlement(HELPERS.NewOdbcConn());
      returnListEntitlement[] matchingBusEnts = engineBusEnts.ListEntitlement
        (null,
         " \"Application\"=? AND \"Platform\" = ? AND \"EntitlementValue\"=? ",
         new string[] { "SAP", prevdef.Platform, prevdef.Name },
         "");
      if (matchingBusEnts.Length > 1)
        {
          throw new Exception("Multiple business entitlements exist represent SAP role+platform: " 
                              + prevdef.Name + "+" + prevdef.Platform);
        }

      int IDmatchingBusEnt;

      string checksum = "SAP|" + newdef.Name + "|" + newdef.Platform;

      if (matchingBusEnts.Length == 1)
        {
          // Already exists
          // All we need to do is update its characteristics. 
          // We will keep its EditStatus AS-IS.
          IDmatchingBusEnt = matchingBusEnts[0].ID;
          engineBusEnts.SetEntitlement
            (IDmatchingBusEnt,
             newdef.RoleActivity, newdef.RoleType, newdef.System, newdef.Platform,
             newdef.Description, newdef.Name,
             null, null, null, null, null, null, null, null,
             "SAP", checksum , matchingBusEnts[0].Status);
        }
      else
        {
          // Must create new entitlement. 
          // Note that I am going to make the "checksum" be simply based on the
          // SAP role's name itself:  "SAP|rolename"
          // The status will be set to "Pending".
          IDmatchingBusEnt = engineBusEnts.NewEntitlement
            (newdef.RoleActivity, newdef.RoleType, newdef.System,
             newdef.Platform, newdef.Description, newdef.Name,
             "SAP", checksum);
          engineBusEnts.SetEntitlement
            (IDmatchingBusEnt,
             newdef.RoleActivity, newdef.RoleType, newdef.System, newdef.Platform,
             newdef.Description, newdef.Name,
             null, null, null, null, null, null, null, null,
             "SAP", checksum, "A"/*Active*/);
        }
    }




    internal static void PublishSAPEntAssSet(int IDeas, string comment)
    {
      ITcodeAssignmentSet engine = new ITcodeAssignmentSet(HELPERS.NewOdbcConn());

      // All that needs to be done is changing the status of the ASet.
      // No changes to the assignment rows at all.

      returnGetTcodeAssignmentSet curval = engine.GetTcodeAssignmentSet(IDeas);

      // Find all currently active EASets for this subprocess and for any
      // of status "active", change to "archived".
      returnListTcodeAssignmentSetBySubProcess[] ret =
        engine.ListTcodeAssignmentSetBySubProcess
        (null, "", new string[] { }, "", curval.SubProcessID);
      for (int i = 0; i < ret.Length; i++)
        {
          if (ret[i].Status.ToLower() == "active")
            {
              engine.SetTcodeAssignmentSet
                (ret[i].ID,
                 ret[i].tstamp, ret[i].Commentary,
                 ret[i].SubProcessID, ret[i].UserID, "archived");
            }
        }

      if (comment == null)
        {
          comment = curval.Commentary;
        }

      engine.SetTcodeAssignmentSet
        (IDeas, DateTime.Now, comment,
         curval.SubProcessID, curval.UserID, "ACTIVE");

    }













    public static int WorkspaceCreate
    (OdbcConnection conn, int IDsubprocess, int IDuser, int IDeas, string commentary)
    {
      ITcodeAssignmentSet Ieaset = new ITcodeAssignmentSet(conn);
      IOrgValue1252 engine1252 = new IOrgValue1252(conn);
      IAuthRow1251 engine1251 = new IAuthRow1251(conn);

      int IDnewWS =
        Ieaset.NewTcodeAssignmentSet(DateTime.Now, IDsubprocess, IDuser, "WORKSPACE");

      if (commentary != null)
        {
          Ieaset.SetTcodeAssignmentSet
            (IDnewWS, DateTime.Now, commentary, IDsubprocess, IDuser, "WORKSPACE");
        }


      /* Find the list of EAssignments to bring over to the Workspace */
      ITcodeAssignment Iea = new ITcodeAssignment(conn);

      if (IDeas < 0) {
        /* Caller wants us to find the very latest ACTIVE EAset for this subpr */
        returnListTcodeAssignmentSetBySubProcess[] _easets =
          Ieaset.ListTcodeAssignmentSetBySubProcess
          (null, " \"Status\" = ?", new string[] {"ACTIVE"}, "", IDsubprocess); 
        if (_easets.Length > 1) {
          throw new Exception("More than one ACTIVE entitlement set in this subprocess #" + IDsubprocess);
        }
        else if (_easets.Length == 1)
          {
            IDeas = _easets[0].ID;
          }
      }

      if (IDeas >= 0)
        {


          /* STEP 1 of 3: Cloning all the Tcode Assignment records. */
          returnListTcodeAssignmentByTcodeAssignmentSet[] _IDea =
            Iea.ListTcodeAssignmentByTcodeAssignmentSet
            (null, "", new string[] { }, "", IDeas);
          int numToConvert = _IDea.Length;

          System.Collections.Hashtable dictEntVectorClones = new Hashtable();
          
          foreach (returnListTcodeAssignmentByTcodeAssignmentSet i in _IDea)
            {
              if ((i.EditStatus & 4) == 0)
                {
                  int IDbaby =
                    Iea.NewTcodeAssignment
                    (IDnewWS, i.SAProleID, i.TcodeEntitlementID);
                  // One more step: necessary to ensure the edit status starts life as 0 ("no change yet").
                  Iea.SetTcodeAssignment
                    (IDbaby, IDnewWS, i.SAProleID, i.TcodeEntitlementID, 0, "");
                }
            }
        




          /* STEP 2: Copying the 1252 table. */
          returnListOrgValue1252ByTcodeAssignmentSet[] ToClone1252 =
            engine1252.ListOrgValue1252ByTcodeAssignmentSet
            (null, "", new string[] { }, "", IDeas);
          foreach (returnListOrgValue1252ByTcodeAssignmentSet i in ToClone1252)
            {
              if ((i.EditStatus & 4) == 0)
                {
                  int IDbaby =
                    engine1252.NewOrgValue1252
                    (i.FieldName, IDnewWS, i.SAProleID, 0/*resetting the editstatus*/);
                  engine1252.SetOrgValue1252(IDbaby,
                      i.FieldName, i.RangeLow, i.RangeHigh, IDnewWS, i.SAProleID, 0);
                }
            }



            /* STEP 3: Copying the 1251 table. */
            returnListAuthRow1251ByTcodeAssignmentSet[] ToClone1251 =
               engine1251.ListAuthRow1251ByTcodeAssignmentSet
              (null, "", new string[] { }, "", IDeas);
            foreach (returnListAuthRow1251ByTcodeAssignmentSet i in ToClone1251)
            {
                if ((i.EditStatus & 4) == 0)
                {
                    int IDbaby =
                      engine1251.NewAuthRow1251
                      (i.SAPauthObjID, i.SAPauthFieldID, IDnewWS, i.SAProleID, 0/*resetting the editstatus*/);
                    engine1251.SetAuthRow1251(IDbaby,
                        i.RangeLow, i.RangeHigh, i.SAPauthObjID, i.SAPauthFieldID, IDnewWS, i.SAProleID, 0);
                }
            }

          // //
        }
      return IDnewWS;
    }






    public static void InitChangeMgmtForWS(int idWorkspace)
    {
      ISAPChangeManagementEvent engine = new ISAPChangeManagementEvent(HELPERS.NewOdbcConn());
      returnListSAPChangeManagementEventByTcodeAssignmentSet[] curExist =
        engine.ListSAPChangeManagementEventByTcodeAssignmentSet
        (null, idWorkspace);
      if (curExist.Length == 0)
        {
          // We need to create them
          engine.NewSAPChangeManagementEvent(idWorkspace, "Conceptual Design");
          engine.NewSAPChangeManagementEvent(idWorkspace, "Technical Design");
          engine.NewSAPChangeManagementEvent(idWorkspace, "Build Verification");
          engine.NewSAPChangeManagementEvent(idWorkspace, "Unit Test");
          engine.NewSAPChangeManagementEvent(idWorkspace, "User Acceptance Test - SAP");
        }

    }








    public static void RecordChangeInEntitlementAssignmentRow
    (int IDtass, int idWS,  int IDentit, int IDuser, int idSaprole, string strIPaddress,
     System.Collections.Specialized.NameValueCollection P)
    {
      ITcodeEntitlement Itent
        = new ITcodeEntitlement(HELPERS.NewOdbcConn());
      ITcodeAssignment Itass
        = new ITcodeAssignment(HELPERS.NewOdbcConn());

      returnGetTcodeEntitlement ret =
        Itent.GetTcodeEntitlement(IDentit);

      returnGetTcodeAssignment theTass = Itass.GetTcodeAssignment(IDtass);


      // The "previous/current" entitlement vector:
      int prevValueVector = theTass.TcodeEntitlementID;
      int newValueVector = IDentit;

      // If the two vectors' IDs match, then this is apparently a no-delta edit.
      bool changeOccurred = false;
      bool significantChangeOccurred = false;
      if (prevValueVector != newValueVector) {
        changeOccurred = true;
        significantChangeOccurred = true;
      }


      if (!changeOccurred)
        {
          return;
        }


      bool isNewTcEnt;
      int IDnewtent;
      int changeBit;

      if (significantChangeOccurred)
        {
          changeBit = 8;/*MODIFIED*/

          IDnewtent = IDentit;

        }
      else
        {
          IDnewtent = theTass.TcodeEntitlementID;
          changeBit = 1;/*COMMENTARY CHANGED*/
        }


      Itass.SetTcodeAssignment
        (IDtass,
         theTass.TcodeAssignmentSetID,
         theTass.SAProleID,
         IDnewtent,
         theTass.EditStatus | changeBit,
         "");   //SafeString(P["c_u_" + _entitlementFields[4]]) /*comment*/
         
    }










    public static void RecordInsertOfEntitlementAssignmentRow
    (int idWS, int IDuser, int idSaprole, string srIPaddress,
     System.Collections.Specialized.NameValueCollection P)
                 
    {

      throw new Exception("HAS NOT BEEN UPDATED FOR NEW PARADIGM");

      /* Content of this was deleted on 2 Feb 2010 */

    }




    // Default action is to TOGGLE (if specialaction is null).
    // If specialaction=='D', then it ENSURES marking as deleted.
    // If specialaction=='U', then it ENSURES it is not marked as deleted.
    public static void RecordDeletionOfEntitlementAssignmentRow
    (int idTassToDel, char specialaction)
    {
      if (specialaction == null)
        {
          specialaction = 'T';
        }

      ITcodeAssignment Itass
        = new ITcodeAssignment(HELPERS.NewOdbcConn());
      returnGetTcodeAssignment theTass = Itass.GetTcodeAssignment(idTassToDel);

      int newEditStatus = theTass.EditStatus;

      switch (specialaction)
        {
        case 'T':
          if ((newEditStatus & 4) == 0)
            {
              // DELETE
              if ((newEditStatus & 2/*NEW*/) > 0)
                {
                  newEditStatus = 6; /*NEW,DELETED*/
                }
              else
                {
                  newEditStatus = newEditStatus | 4/*DELETED*/;
                }
            }
          else
            {
              // UNDELETE
              newEditStatus = newEditStatus - 4;
            }
          break;

        case 'U':
          if ((newEditStatus & 4) > 0)
            newEditStatus = newEditStatus - 4;
          break;

        case 'D':
          if ((newEditStatus & 4) == 0)
            newEditStatus = newEditStatus | 4;
          break;
        }


      Itass.SetTcodeAssignment
        (idTassToDel,
         theTass.TcodeAssignmentSetID,
         theTass.SAProleID,
         theTass.TcodeEntitlementID,
         newEditStatus,
         theTass.Commentary);
    }









    // Returns true if a change to the DB was indeed necesary
    public static bool RecordLinkFromAuthFieldToAuthObj
    (OdbcConnection conn, int IDfield, int IDobj)
    {
      int rv = 0;
      OdbcCommand cmd = new OdbcCommand();

      cmd.Connection = conn;


      // STEP 1:  DOES THIS RELATIONSHIP ALREADY EXISTS?  If so, do nothing!

      cmd.CommandText = "SELECT c_id FROM \"t_RBSR_AUFW_r_SAPauthObjSAPauthField\" " +
        " WHERE c_r_SAPauthObj = ? AND c_r_SAPauthField = ? ;";
      cmd.Parameters.Add("c_r_SAPauthObj", OdbcType.Int);
      cmd.Parameters["c_r_SAPauthObj"].Value = (object)IDobj;
      cmd.Parameters.Add("c_r_SAPauthField", OdbcType.Int);
      cmd.Parameters["c_r_SAPauthField"].Value = (object)IDfield;

      OdbcDataReader dr = cmd.ExecuteReader();
      bool linkAlreadyKnown = false;
      while (dr.Read())
        {
          linkAlreadyKnown = true;
        }
      if (linkAlreadyKnown)
        {
          return false;
        }
      dr.Close();


      // STEP 2:  IF NEEDED, DO THE INSERT TO FORM THE RELATIONSHIP
      cmd.CommandText = "insert into \"t_RBSR_AUFW_r_SAPauthObjSAPauthField\" (\"c_r_SAPauthObj\",\"c_r_SAPauthField\") values(?,?)  " +
        "select convert(int,SCOPE_IDENTITY())";
      cmd.Parameters.Add("c_r_SAPauthObj", OdbcType.Int);
      cmd.Parameters["c_r_SAPauthObj"].Value = (object)IDobj;
      cmd.Parameters.Add("c_r_SAPauthField", OdbcType.Int);
      cmd.Parameters["c_r_SAPauthField"].Value = (object)IDfield;

      rv = cmd.ExecuteNonQuery();
      if (rv == 0) throw new Exception("INSERT failed: " + cmd.CommandText);
      cmd.Dispose();
      return true;
    }







    private static string SafeString(object p)
    {
      if (p == null)
        {
          return "";
        }
      else
        {
          return p.ToString();
        }
    }



      public static Queue ImportTcodeList(OdbcConnection conn, DataTable dt, int IDsubpr)
    {
        Queue errMsgs = new Queue();
        int countNewCreated = 0;
        int countAlreadyExisted = 0;

                ITcodeDictionary Itdict = new ITcodeDictionary(conn);


        IEnumerator<System.Data.DataRow> x =
(IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();
        while (x.MoveNext())
        {

            if (x.Current[0].Equals(System.DBNull.Value))
            {
                // Ignore any line with no value in the first field.
                // Often the end of the csv file has just lots of blank rows.
                continue;
            }

            string theName = x.Current["Name"].ToString();
            string theDescr = x.Current["Description"].ToString();

            if (true ==
            SAP_HELPERS.RegisterTCode(Itdict, theName, theDescr))
            {
                countNewCreated++;
            }
            else
            {
                countAlreadyExisted++;
            }

        }

          errMsgs.Enqueue("Number of new registrations: " + countNewCreated.ToString());
          errMsgs.Enqueue("Number of TCodes previously registered: " + countAlreadyExisted.ToString());
          if (countAlreadyExisted > 0) {
              errMsgs.Enqueue("- - Warning: description changes were NOT performed for previously-registered TCodes.");
          }

          return errMsgs;

        }



    }
}
