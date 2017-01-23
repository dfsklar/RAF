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
using RBSR_AUFW.DB.ISAProle;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RBSR_AUFW.DB.IApplication;
using RBSR_AUFW.DB.IChangeManagementEvent;
using System.IO;
using Eval3;

namespace _6MAR_WebApplication
{




  public class HELPERS
  {

    public static System.Data.Odbc.OdbcConnection conn = null;




    public static void AutoRegisterAppsInManifestFormulaTable()
    {
      IEnumerator xenum = SelectDistinctApplications();
      IMVFormula Iface = new IMVFormula(NewOdbcConn());
      while (xenum.MoveNext())
        {
          string thestr = (xenum.Current as DataRowView).Row[0] as string;

          returnListMVFormula[] RET =
            Iface.ListMVFormula(null, "\"KEYapplication\" = ?",
                                new string[] { thestr }, "");
          if (RET.Length == 0)
            {

              Iface.NewMVFormula(thestr);
            }

        }

    }



    // The enumerator returns objects of type DataRowView
    static IEnumerator SelectDistinctApplications()
    {
      SqlDataSource DS = null;

      DS = new SqlDataSource(
                             ConfigurationManager.ConnectionStrings["afwac_sv6ConnectionString"].ConnectionString,
                             "SELECT c_u_Name FROM [t_RBSR_AUFW_u_Application] ORDER BY c_u_Name");

      IEnumerable result = DS.Select(new DataSourceSelectArguments("c_u_Name"));

      IEnumerator xenum = result.GetEnumerator();

      return xenum;
    }








    public static OdbcConnection NewOdbcConn()
    {
      if (conn != null)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
          return conn;
        }
      else
        {
          string connstr = ConfigurationManager.AppSettings["DBconnstr"];
          conn = new OdbcConnection(connstr);
          conn.Open();
          return conn;
        }

    }



    public static OdbcConnection NewOdbcConn_FORCE()
    {

      {
        string connstr = ConfigurationManager.AppSettings["DBconnstr"];
        conn = new OdbcConnection(connstr);
        conn.Open();
        return conn;
      }

    }





    public static int FindBusRoleByName(string name)
    {
        if (name == "---")
        {
            // SPECIAL CASE: This special dummy busrole (invisible placeholder) is number 6
            return 6;
        }

        IBusRole Ibrole = new IBusRole(NewOdbcConn());

      returnListBusRole[]
        RET = Ibrole.ListBusRole(null, "\"name\" = ?",
                                             new string[] { name }, "");

        if (RET.Length == 0)
        {
            throw new Exception("Across all subprocesses, " +
                                " there is no business role with name " + name);
        }
        if (RET.Length > 1)
        {
            throw new Exception("Across all subprocesses, " +
                                " there are multiple business roles with name " + name);
        }
        // If we get here, we found the one business role that meets the criteria.
        return RET[0].ID;
    }




      /*
    public static int FindBusRoleByAbbrev(string abbrev, int IDsubprocess)
    {
      if (abbrev == "---")
        {
          // SPECIAL CASE: This special dummy busrole (invisible placeholder) is number 6
          return 6;
        }



      IBusRole Ibrole = new IBusRole(NewOdbcConn());
      returnListBusRoleBySubProcess[]
        RET = Ibrole.ListBusRoleBySubProcess(null, "\"name\" = ?",
                                             new string[] { abbrev }, "", IDsubprocess);
      if (RET.Length == 0)
        {
          throw new Exception("In subprocess " + IDsubprocess.ToString() +
                              " there is no business roles with name " + abbrev);
        }
      if (RET.Length > 1)
        {
          throw new Exception("In subprocess " + IDsubprocess.ToString() +
                              " there are multiple business roles with name " + abbrev);
        }
      // If we get here, we found the one business role that meets the criteria.
      return RET[0].ID;
    }
      */








    public static void InitNewDB(OdbcConnection conn)
    {
      // Creates one process, one subpr, one user
      IProcess Ipr = new IProcess(conn);
      ISubProcess Isubpr = new ISubProcess(conn);
      IUser Iuser = new IUser(conn);

      int IDprocessCCM = HELPERS.FindProcess(conn, "CCM", true);
      int IDsubpr = HELPERS.FindSubProcess(conn, IDprocessCCM, "Pricing", true);
      int IDuser = HELPERS.FindUser(conn, "E16034", "David Sklar", true);
    }



    public static int FindProcess(OdbcConnection conn, string name, bool buildIfNeeded)
    {
      IProcess Ipr = new IProcess(conn);
      returnListProcess[] ret =
        Ipr.ListProcess(null, "\"Name\" like ?", new string[] { name }, "");
      if (ret.Length == 1)
        {
          return ret[0].ID;
        }
      else
        {
          if (buildIfNeeded)
            {
              return Ipr.NewProcess(name);
            }
          else
            {
              throw new System.NullReferenceException("No process found with this name: " + name);
            }
        }
    }


    public static void EmptyTable(OdbcConnection conn, string tblname)
    {
      if (!tblname.StartsWith("t_"))
        {
          tblname = "t_RBSR_AUFW_u_" + tblname;
        }
      int rv = 0;
      OdbcCommand cmd = new OdbcCommand();
      cmd.CommandText = "delete from \"" + tblname + "\"";
      cmd.Connection = conn;
      rv = cmd.ExecuteNonQuery();
      //   if (rv == 0) throw new Exception("EMPTY resulted in " + rv.ToString() + " objects being deleted!");
      cmd.Dispose();
    }

    public static void DiscardFromTable
    (OdbcConnection conn, string tblname, string whereclause)
    {
      if (!tblname.StartsWith("t_"))
        {
          tblname = "t_RBSR_AUFW_u_" + tblname;
        }
      int rv = 0;
      OdbcCommand cmd = new OdbcCommand();
      cmd.CommandText = "delete from \"" + tblname + "\" where " + whereclause;
      cmd.Connection = conn;
      rv = cmd.ExecuteNonQuery();
      //   if (rv == 0) throw new Exception("EMPTY resulted in " + rv.ToString() + " objects being deleted!");
      cmd.Dispose();
    }




    /*
     * As of schema v7, creating a workspace from an existing EAset is nothing
     * more than makig copies of all the EAssignments in the existing EASet (given 
     * as a parameter).
     * The Status of the new EASet will be of course "WORKSPACE".
     * It is assumed the caller will ensure number of workspaces kept to 1 at most, etc.
     */

    public static int WorkspaceCreate
    (OdbcConnection conn, int IDsubprocess, int IDuser, int IDeas, string commentary)
    {
      IEntAssignmentSet Ieaset = new IEntAssignmentSet(conn);

      // FIRST STEP: MAKE EXTRA SURE THERE IS NOT ALREADY A WORKSPACE FOR THIS SUBPR.
      returnListEntAssignmentSetBySubProcess[] verif =
        Ieaset.ListEntAssignmentSetBySubProcess(null, "\"Status\" LIKE 'WORKSPACE'", new string[] { },
                                                "", IDsubprocess);
      if (verif.Length > 0)
        {
          // This message must include the word "already" for recognition purposes.
          throw new Exception("Workspace creation aborted: workspace already present for this subprocess.");
        }


      // Would be nice:  OdbcTransaction taction = conn.BeginTransaction(System.Data.IsolationLevel.Snapshot);

      /* Find the list of EAssignments to bring over to the Workspace */
      IEntAssignment Iea = new IEntAssignment(conn);

      if (IDeas < 0)
        {
          /* Caller wants us to find the very latest ACTIVE EAset for this subpr */
          returnListEntAssignmentSetBySubProcess[] _easets =
            Ieaset.ListEntAssignmentSetBySubProcess
            (null, " \"Status\" = ?", new string[] { "ACTIVE" }, "", IDsubprocess);
          if (_easets.Length > 1)
            {
              throw new Exception("More than one ACTIVE entitlement set in this subprocess #" + IDsubprocess);
            }
          else if (_easets.Length == 1)
            {
              IDeas = _easets[0].ID;
            }
        }


      int IDnewWS = Ieaset.NewEntAssignmentSet(IDsubprocess, IDuser);

      Ieaset.SetEntAssignmentSet(IDnewWS, "DONOTUSE", null, commentary, IDsubprocess,
                                 IDuser, DateTime.Now);

      if (IDeas >= 0)
        {
          returnListEntAssignmentByEntAssignmentSet[] _IDea =
            Iea.ListEntAssignmentByEntAssignmentSet(null, 
            " \"Status\" NOT IN ('X') ", new string[] { }, "", IDeas);
          int numToConvert = _IDea.Length;

          System.Collections.Hashtable dictEntVectorClones = new Hashtable();

          foreach (returnListEntAssignmentByEntAssignmentSet i in _IDea)
            {
              int IDentitlementVector = i.EntitlementID;
              int IDbusrole = i.BusRoleID;

              int IDbaby = Iea.NewEntAssignment(IDnewWS, IDbusrole, IDentitlementVector, "A");
            }
        }

      Ieaset.SetEntAssignmentSet(IDnewWS, "WORKSPACE", null, commentary, IDsubprocess,
                                 IDuser, DateTime.Now);
      
      conn.Close();

      return IDnewWS;
    }




    // HELPERS FOR THE ENTITLEMENT AREA










    /*
     * Given a workspace entitlement object, find or create a true Entitlement object
     * that matches.  Checksums are used to optimize the search but the returned objects
     * are then subjected to a field-by-field check including the commentary field.  NO MATCH
     * unless the commentary field also matches.
     * Entitlement objs are read-only once created, so if no exact match found, a new one
     * is created.
     * Returns the ID of the entitlement obj.
     */
    static public int FindOrCreateEntitlementMatchingWSEntitlement
    (
     IEntitlement Ient,
     IEntAssignmentSet Ieaset,
     int /*returnListWorkspaceEntitlementByEditingWorkspace*/  WSE)
    {
      throw new Exception("NYI");
      return 0;
    }






    // Returns -1 if no entitlement has this checksum
    static public int FindEntitlementByVector
    (
     DataRow vector,
     out string targetchecksum
     )
    {

      // The checksum is used to find possible matches, but if more
      // than one row returned, it is essential that exact matching be done.

      targetchecksum
        = HELPERS.ENTCHECKSUM
        (
         vector["Standard Activity"].ToString(),
         vector["Type"].ToString(),
         vector["Application"].ToString(),
         vector["System"].ToString(),
         vector["Platform"].ToString(),
         vector["Entitlement Description"].ToString(),
         vector["Entitlement Value"].ToString(),
         "",
         vector["Authorization Object"].ToString(),
         vector["Field-Level Security Name"].ToString(),
         vector["Field-Level Security Value"].ToString(),
         vector["4th Level Security Name"].ToString(),
         vector["4th Level Security Value"].ToString());

      return FindEntitlementByChecksum(targetchecksum);
    }




    static public int FindEntitlementByChecksum(string targetchecksum)
    {

      OdbcConnection conn = NewOdbcConn();

      IEntitlement Iwse = new IEntitlement(conn);

      returnListEntitlement[] RET =
        Iwse.ListEntitlement
        (null, "\"CHECKSUM\" = ?",
         new string[] { targetchecksum }, "");


      if (RET.Length == 0)
        {
          // There is nothing else with this checksum in this workspace.
          return -1;
        }
      else
        {
          if (RET.Length > 1)
            {
              throw new Exception("Entitlement universe contains multiple rows with identical checksum.  Contact Sklar.");
            }
          else
            {
              return (RET[0].ID);
            }
        }

    }




      public static string SafeObjToString(Object x)
      {
          if (x.GetType().ToString() == "System.DBNull")
          {
              return "";
          }
          else
          {
              return (string)x;
          }
      }



      public static string ENTCHECKSUM
      (Object stdactivity, Object roletype, Object appl, Object system, Object platform, Object ename, Object evalue, Object AOname, Object AOvalue, Object FSname, Object FSvalue, Object L4name, Object L4value)
      {
	return ENTCHECKSUM
	  (
	   SafeObjToString(stdactivity),
	   SafeObjToString(roletype),
	   SafeObjToString(appl),
	   SafeObjToString(system),
	   SafeObjToString(platform),
	   SafeObjToString(ename),
	   SafeObjToString(evalue),
	   SafeObjToString(AOname),
	   SafeObjToString(AOvalue),
	   SafeObjToString(FSname),
	   SafeObjToString(FSvalue),
	   SafeObjToString(L4name),
	   SafeObjToString(L4value));
      }
    



    public static string ENTCHECKSUM
    (string stdactivity, string roletype, string appl, string system, string platform, string ename, string evalue, string AOname, string AOvalue, string FSname, string FSvalue, string L4name, string L4value)
    {

        if (appl == "SAP")
        {
            return "SAP|" + evalue + "|" + platform;
        }

        else
        {
            MD5CryptoServiceProvider engine = new MD5CryptoServiceProvider();
            UnicodeEncoding uniengine = new UnicodeEncoding();
            byte[] readyToHash = uniengine.GetBytes(
                                                    stdactivity + "|@|" +
                                                    roletype + "|@|" +
                                                    appl + "|@|" +
                                                    system + "|@|" +
                                                    platform + "|@|" +
                                                    ename + "|@|" +
                                                    evalue + "|@|" +
                                                    AOname + "|@|" +
                                                    AOvalue + "|@|" +
                                                    FSname + "|@|" +
                                                    FSvalue + "|@|" +
                                                    L4name + "|@|" +
                                                    L4value);

            byte[] hashed = engine.ComputeHash(readyToHash);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashed)
            {
                sb.Append(b.ToString("x2").ToUpper());
            }
            return sb.ToString();
        }

    }









    // Returns true if a change to the DB was indeed necesary
    public static bool RecordLinkFromSAPauthobjToSAPauthfield
    (OdbcConnection conn, int IDauthobj, int IDauthfield)
    {
        int rv = 0;
        OdbcCommand cmd = new OdbcCommand();

        cmd.Connection = conn;


        // STEP 1:  DOES THIS RELATIONSHIP ALREADY EXISTS?  If so, do nothing!

        cmd.CommandText = "SELECT c_id FROM \"t_RBSR_AUFW_r_SAPauthObjSAPauthField\" " +
          " WHERE c_r_SAPauthObj = ? AND c_r_SAPauthField = ? ;";
        cmd.Parameters.Add("c_r_SAPauthObj", OdbcType.Int);
        cmd.Parameters["c_r_SAPauthObj"].Value = (object)IDauthobj;
        cmd.Parameters.Add("c_r_SAPauthField", OdbcType.Int);
        cmd.Parameters["c_r_SAPauthField"].Value = (object)IDauthfield;

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
        cmd.Parameters["c_r_SAPauthObj"].Value = (object)IDauthobj;
        cmd.Parameters.Add("c_r_SAPauthField", OdbcType.Int);
        cmd.Parameters["c_r_SAPauthField"].Value = (object)IDauthfield;

        rv = cmd.ExecuteNonQuery();
        if (rv == 0) throw new Exception("INSERT failed: " + cmd.CommandText);
        cmd.Dispose();
        return true;
    }









    // Returns true if a change to the DB was indeed necesary
    public static bool RecordLinkFromBusRoleToEntitlementVector
    (OdbcConnection conn, int IDbusrole, int IDcloneEntVector, int IDworkspace)
    {
      int rv = 0;
      OdbcCommand cmd = new OdbcCommand();

      cmd.Connection = conn;


      // STEP 1:  DOES THIS RELATIONSHIP ALREADY EXISTS?  If so, do nothing!

      cmd.CommandText = "SELECT c_id FROM \"t_r_BusRoleWorkspaceEntitlement\" " +
        " WHERE c_r_BusRole = ? AND c_r_WorkspaceEntitlement = ? ;";
      cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
      cmd.Parameters["c_r_BusRole"].Value = (object)IDbusrole;
      cmd.Parameters.Add("c_r_WorkspaceEntitlement", OdbcType.Int);
      cmd.Parameters["c_r_WorkspaceEntitlement"].Value = (object)IDcloneEntVector;
      cmd.Parameters.Add("c_r_EditingWorkspace", OdbcType.Int);
      cmd.Parameters["c_r_EditingWorkspace"].Value = (object)IDworkspace;

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
      cmd.CommandText = "insert into \"t_r_BusRoleWorkspaceEntitlement\" (\"c_r_BusRole\",\"c_r_WorkspaceEntitlement\", \"c_r_EditingWorkspace\") values(?,?,?)  " +
        "select convert(int,SCOPE_IDENTITY())";
      cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
      cmd.Parameters["c_r_BusRole"].Value = (object)IDbusrole;
      cmd.Parameters.Add("c_r_WorkspaceEntitlement", OdbcType.Int);
      cmd.Parameters["c_r_WorkspaceEntitlement"].Value = (object)IDcloneEntVector;
      cmd.Parameters.Add("c_r_EditingWorkspace", OdbcType.Int);
      cmd.Parameters["c_r_EditingWorkspace"].Value = (object)IDworkspace;

      rv = cmd.ExecuteNonQuery();
      if (rv == 0) throw new Exception("INSERT failed: " + cmd.CommandText);
      cmd.Dispose();
      return true;
    }




      public static void DestroyAllBusroleToPersonnelMappings
          ()
    {
        OdbcCommand cmd = new OdbcCommand();

        cmd.CommandText = "delete from \"t_RBSR_AUFW_u_BusRoleOwner\"";

        cmd.Connection = conn;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
      }







    public static bool DestroyLinkFromBusRoleToWSEntitVector
    (OdbcConnection conn, int IDbusrole, int IDcloneEntVector, int IDworkspace)
    {
        // ******************************
        // ******************************
        throw new Exception("NO LONGER IN USE");
        // ******************************
        // ******************************
        // ******************************

      int rv = 0;
      OdbcCommand cmd = new OdbcCommand();

      cmd.CommandText = "delete from \"t_r_BusRoleWorkspaceEntitlement\" WHERE " +
        "c_r_BusRole = ? AND c_r_WorkspaceEntitlement = ? AND c_r_EditingWorkspace = ?";
      cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
      cmd.Parameters["c_r_BusRole"].Value = (object)IDbusrole;
      cmd.Parameters.Add("c_r_WorkspaceEntitlement", OdbcType.Int);
      cmd.Parameters["c_r_WorkspaceEntitlement"].Value = (object)IDcloneEntVector;
      cmd.Parameters.Add("c_r_EditingWorkspace", OdbcType.Int);
      cmd.Parameters["c_r_EditingWorkspace"].Value = (object)IDworkspace;

      cmd.Connection = conn;
      rv = cmd.ExecuteNonQuery();
      cmd.Dispose();
      if (rv == 0)
        {
          // No change was needed - this link did not exist.
          return false;
        }
      return true;
    }




    public static void ValidateAbbrev(string abbrev, int busroleIdToIgnore, int idSubpr)
    {
      return;


      if (!Regex.IsMatch(abbrev, @"^[A-Za-z0-9_\-]+$"))
        {
          throw new Exception("Specified abbreviation is not valid.  Only uppercase letters are allowed.  Refresh the web page to restore the table, and try again.");
        }


      IBusRole engine = new IBusRole(HELPERS.NewOdbcConn());
      returnListBusRoleBySubProcess[] res
        =
        engine.ListBusRoleBySubProcess
        (null, "\"abbrev\" like ?",
         new string[] { abbrev },
         "", idSubpr);

      if (res.GetLength(0) > 0)
        {
          if (res[0].ID != busroleIdToIgnore)
            throw new Exception("The specified abbreviation is already in use for this subprocess.  Refresh the web page to restore the table, and try again.");
        }
    }



    public static DataTable LoadCsv(string importFolder, string strFileName)
    {
        // Create the schema.ini file for the given file.
        // Read the first line of the file to get the 
        // heading names.
        StreamReader FIN = new StreamReader(importFolder + "\\" + strFileName);
        string aline = FIN.ReadLine();
        FIN.Close();

        string[] colheaders = aline.Split(new char[] { ',' });

        StreamWriter FOUT = new StreamWriter(importFolder + "\\" + "schema.ini");
        FOUT.WriteLine("[" + strFileName + "]");
        FOUT.WriteLine("Format=CSVDelimited");
        FOUT.WriteLine("ColNameHeader=True");
        int idxColheader = 0;
        foreach (string colnameheader in colheaders)
        {
            idxColheader++;
            string colname = colnameheader;
            if (colnameheader.Length < 1)
            {
                colname = "Col" + idxColheader;
            }
            FOUT.WriteLine("Col" + idxColheader + "=\"" + colname + "\" Text Width 500");
        }
        FOUT.Close();

      //in some function
      System.Data.Odbc.OdbcConnection conn;
      DataTable dt = new DataTable();
      System.Data.Odbc.OdbcDataAdapter da;
      string connectionString;


        /*
         * In order to control the datatypes from a csv being read,
         * you put a schema.ini file in the same directory.
         * Microsoft's Knowledge Base 187670 has some exampes but no spec.
         * Google search: How to use RDO and ODBC text driver to open a delimited text
         * First line is: [NAMEOFFILE.XXX]   I'm shocked that's required!
         * ColNameHeader=True
         * Format = CsvDelimited
         * CharacterSet = ANSI
         * Col1=Col1 text width 350
         * Col2=....    
         * Note the column names will come from the .csv, not here
         * 
         * 
         * */

      connectionString = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + importFolder + ";";
      conn = new System.Data.Odbc.OdbcConnection(connectionString);

      //we only pass it the folder.  The csv file import is in the query which follows

      da = new System.Data.Odbc.OdbcDataAdapter  ("select * from [" + strFileName + "]", conn);
      da.Fill(dt);

        // We want to make sure the user had a schema.ini file in place.
        // The way to verify is to check every column and make sure it's string datatype
      foreach (DataColumn x in dt.Columns)
        {
            if (x.DataType.FullName != "System.String")
            {
                throw new Exception("The .csv file did not have a corresponding schema.ini file/section."); 
            }
      }

      return dt;
    }


    /*
     * The version 1 did not care about coalescing vectors that are identical
     * except for the business role.  The number of Entitlement objects
     * created was exactly the number of rows imported from the CSV.
     */
    /*
      public static void ImportDataTableAsNewEntAssignmentSet_v1(
      DataTable dt, int IDuser, int IDsubpr, System.Data.Odbc.OdbcConnection conn)
      {
      IUser Iuser = new IUser(conn);
      ISubProcess Isubpr = new ISubProcess(conn);
      IEntAssignmentSet Ieaset = new IEntAssignmentSet(conn);
      IEntAssignment Iea = new IEntAssignment(conn);
      IEntitlement Ientit = new IEntitlement(conn);
      IBusRole Ibr = new IBusRole(conn);

      int IDneweas =
      Ieaset.NewEntAssignmentSet(0, DateTime.Now, "TEST", IDsubpr, IDuser);

      // These Get function are only useful for a read-only view of fields;
      // not for generating subordinate entities.
      returnGetEntAssignmentSet neweas = Ieaset.GetEntAssignmentSet(IDneweas);

      IEnumerator<System.Data.DataRow> x =
      (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();


      while (x.MoveNext())
      {
      int IDbusrole;

      if (x.Current[0].Equals(System.DBNull.Value))
      {
      // Ignore any line with no value in the first field.
      // Often the end of the csv file has just lots of blank rows.
      continue;
      }


      // BUSINESS ROLE IS NOT REQUIRED IN THE INPUT CSV !
      if (x.Current["BusRole"].Equals(System.DBNull.Value))
      {
      IDbusrole = 6;  //The special null business role
      }
      else
      {
      // Make sure business role object exists; create if not.
      string busrole = (string)(x.Current["BusRole"]);
      returnListBusRole[] xx = Ibr.ListBusRole(null, "\"Name\" like ?",
      new string[] { busrole }, "");
      if (xx.Length < 1)
      {
      // MUST ADD NEW ONE
      IDbusrole =
      Ibr.NewBusRole(busrole, x.Current["Business Role Description"] as string, IDsubpr);
      }
      else
      {
      IDbusrole = xx[0].ID;
      }
      }



      int IDnewent = Ientit.NewEntitlement(
      x.Current["Standard Activity"] as string,
      x.Current["RoleType"] as string,
      x.Current["System"] as string,
      x.Current["Platform"] as string,
      x.Current["Entitlement Description"] as string,
      x.Current["Entitlement Value"] as string,
      x.Current["Application"] as string);

      Ientit.SetEntitlement(IDnewent,
      x.Current["Standard Activity"] as string,
      x.Current["Type"] as string,
      x.Current["System"] as string,
      x.Current["Platform"] as string,
      x.Current["Entitlement Description"] as string,
      x.Current["Entitlement Value"] as string,
      "",
      x.Current["Authorization Object"] as string,
      x.Current["Field-Level Security Name"] as string,
      x.Current["Field-Level Security Value"] as string,
      x.Current["4th Level Security Name"] as string,
      x.Current["4th Level Security Value"] as string,
      x.Current["Additional Comments"] as string,
      x.Current["Manifest Value"] as string,
      x.Current["Application"] as string);

      int IDnewentass = Iea.NewEntAssignment
      (IDneweas, IDbusrole, IDnewent);

      }

      }
    */




    /*
     * The version 2 coalesces vectors that are identical
     * except for the business role.  Right now it uses a checksum that
     * ignores any insignificant field (e.g. commentary).
     * It however does NOT ignore minor variations like whitespace differences!  Should it??
     * 
     * Returns how many were added.
     */

    public static int ImportNewEntitlementsFromDataTable(
                                                         DataTable dt, int IDuser,
                                                         string strNameApp,
                                                         string initStatus,
                                                         System.Data.Odbc.OdbcConnection conn,
                                                         Queue messagesToReturn)
    {
      IUser Iuser = new IUser(conn);
      IEntitlement Ientit = new IEntitlement(conn);


      dt.Columns.Add("Application");

      IEnumerator<System.Data.DataRow> x =
        (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();


      int recordseq = 0;

      Queue msgsWarn = messagesToReturn;

      Hashtable signatures = new Hashtable();

      int countNew = 0;
      int countExtant = 0;

      while (x.MoveNext())
        {
          recordseq++;

          int IDbusrole;

          if (x.Current[0].Equals(System.DBNull.Value))
            {
              // Ignore any line with no value in the first field.
              // Often the end of the csv file has just lots of blank rows.
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because first field blank");
              continue;
            }


          ImportSingleEntitlementFromDataRow(strNameApp, initStatus, Ientit, x, ref countNew, ref countExtant);
        }

      msgsWarn.Enqueue("---------------------");
      msgsWarn.Enqueue("NUMBER OF NEW ENTITLEMENTS CREATED: " + countNew);
      msgsWarn.Enqueue("Number of rows ignored (already exists): " + countExtant);

      return countNew;
    }






    private static void ImportSingleEntitlementFromDataRow
    (string strNameApp, string initStatus, IEntitlement Ientit, IEnumerator<System.Data.DataRow> x, ref int countNew, ref int countExtant)
    {
      // Obtain the entitlement's checksum                    
      string curChecksum = "---";

      x.Current["Application"] = strNameApp;

      int alreadyKnown =
        HELPERS.FindEntitlementByVector(x.Current, out curChecksum);

      if (alreadyKnown >= 0)
        {
          countExtant++;
          // Entitlement is already known!
          // Silently ignore.
        }

      else
        {
          countNew++;

          int IDnewent = Ientit.NewEntitlement(
                                               x.Current["Standard Activity"].ToString(),
                                               x.Current["Type"].ToString(),
                                               x.Current["System"].ToString(),
                                               x.Current["Platform"].ToString(),
                                               x.Current["Entitlement Description"].ToString(),
                                               x.Current["Entitlement Value"].ToString(),
                                               strNameApp,
                                               curChecksum
                                               );

          Ientit.SetEntitlement(IDnewent,
                                x.Current["Standard Activity"].ToString(),
                                x.Current["Type"].ToString(),
                                x.Current["System"].ToString(),
                                x.Current["Platform"].ToString(),
                                x.Current["Entitlement Description"].ToString(),
                                x.Current["Entitlement Value"].ToString(),
                                "",
                                x.Current["Authorization Object"].ToString(),
                                x.Current["Field-Level Security Name"].ToString(),
                                x.Current["Field-Level Security Value"].ToString(),
                                x.Current["4th Level Security Name"].ToString(),
                                x.Current["4th Level Security Value"].ToString(),
                                x.Current["Additional Comments"].ToString(),
                                "",
                                strNameApp,
                                curChecksum,
                                initStatus);

          // SPECIAL CASE: Automatically add an SAP role to be the "mirror" for this.
          // WHOA: it could be this particular method is only used for importing
          // entitlements outside of the scope of any subprocess.
          /*
            if (strNameApp == "SAP") {
            ISAProle engine = new ISAProle(HELPERS.NewOdbcConn());
            SAP_HELPERS.CreateSAPRole
            (engine, 
            x.Current["Entitlement Value"] as string,
            * */

        }
    }
          








    public static int FindSubProcess
    (OdbcConnection conn, int IDprocessCCM, string name, bool buildIfNeeded)
    {
      ISubProcess Ispr = new ISubProcess(conn);
      returnListSubProcess[] ret =
        Ispr.ListSubProcess(null, "\"Name\" like ?", new string[] { name }, "");
      if (ret.Length == 1)
        {
          return ret[0].ID;
        }
      else
        {
          if (buildIfNeeded)
            {
              return Ispr.NewSubProcess(name, IDprocessCCM);
            }
          else
            {
              throw new System.NullReferenceException("No subprocess found with this name: " + name);
            }
        }
    }





      /*
       * The given "name" param is IGNORED if the EID is already registered in the DB.
       * This cannot be used to "update"/"repair" the name associated with an existing user.
       */
    public static int FindUser
    (OdbcConnection conn, string EID, string name, bool buildIfNeeded)
    {
      IUser Ispr = new IUser(conn);
      returnListUser[] ret =
        Ispr.ListUser(null, "\"EID\" like ?", new string[] { EID }, "");
      if (ret.Length == 1)
        {
          return ret[0].ID;
        }
      else
        {
          if (buildIfNeeded)
            {
              return Ispr.NewUser(EID, name);
            }
          else
            {
              throw new System.NullReferenceException("No user found with this EID: " + EID);
            }
        }
    }





    public static void EntitlementVectorUpdate
    (int IDwserow,
     Dictionary<string, object> THERESULT,
     Dictionary<string, object> THEOLDVECTOR,
     bool changeWasOnlyCosmetic, int IDuser, string strIPADDR,
     System.Web.Script.Serialization.JavaScriptSerializer UTIL
     )
    {

      OdbcConnection conn = NewOdbcConn_FORCE();
      IEntitlement Iwserows = new IEntitlement(conn);


      // CHANGING THE ENTITLEMENT VECTOR ITSELF
      Iwserows.SetEntitlement
        (
         IDwserow,
         THERESULT["StandardActivity"] as string,
         THERESULT["RoleType"] as string,
         THERESULT["System"] as string,
         THERESULT["Platform"] as string,
         THERESULT["EntitlementName"] as string,
         THERESULT["EntitlementValue"] as string,
         null, // THERESULT["AuthObjName"] as string,
         THERESULT["AuthObjValue"] as string,
         THERESULT["FieldSecName"] as string,
         THERESULT["FieldSecValue"] as string,
         THERESULT["Level4SecName"] as string,
         THERESULT["Level4SecValue"] as string,
         THERESULT["Commentary"] as string,
         "",
         THERESULT["Application"] as string,
         ENTCHECKSUM(
                     THERESULT["StandardActivity"] as string,
                     THERESULT["RoleType"] as string,
                     THERESULT["Application"] as string,
                     THERESULT["System"] as string,
                     THERESULT["Platform"] as string,
                     THERESULT["EntitlementName"] as string,
                     THERESULT["EntitlementValue"] as string,
                     "", //THERESULT["AuthObjName"] as string,
                     THERESULT["AuthObjValue"] as string,
                     THERESULT["FieldSecName"] as string,
                     THERESULT["FieldSecValue"] as string,
                     THERESULT["Level4SecName"] as string,
                     THERESULT["Level4SecValue"] as string
                     ), "P"
         );


    }







    public static int EntitlementCreate
    (Dictionary<string, object> THERESULT, string checksum)
    {
      OdbcConnection conn = NewOdbcConn();
      IEntitlement engine = new IEntitlement(conn);

      int IDnewrow = engine.NewEntitlement
        (
         THERESULT["StandardActivity"] as string,
         THERESULT["RoleType"] as string,
         THERESULT["System"] as string,
         THERESULT["Platform"] as string,
         THERESULT["EntitlementName"] as string,
         THERESULT["EntitlementValue"] as string,
         THERESULT["Application"] as string, checksum);



      engine.SetEntitlement
        (
         IDnewrow,
         THERESULT["StandardActivity"] as string,
         THERESULT["RoleType"] as string,
         THERESULT["System"] as string,
         THERESULT["Platform"] as string,
         THERESULT["EntitlementName"] as string,
         THERESULT["EntitlementValue"] as string,
         null, /* THERESULT["AuthObjName"] as string, */
         THERESULT["AuthObjValue"] as string,
         THERESULT["FieldSecName"] as string,
         THERESULT["FieldSecValue"] as string,
         THERESULT["Level4SecName"] as string, 
         THERESULT["Level4SecValue"] as string,
         THERESULT["Commentary"] as string,
         null,
         THERESULT["Application"] as string,
         checksum, "P");



      return IDnewrow;

    }






    // Returns the number of rows affected
    public static int RunSql(string p)
    {
      OdbcCommand cmd = new OdbcCommand();
      cmd.CommandText = p;
      cmd.Connection = HELPERS.NewOdbcConn_FORCE();
      int rv = cmd.ExecuteNonQuery();
      cmd.Connection.Close();
      cmd.Dispose();
      return rv;
    }


    public static OdbcDataReader RunSqlSelect(string p)
    {
      OdbcCommand cmd = new OdbcCommand();
      cmd.CommandText = p;
      cmd.Connection = HELPERS.NewOdbcConn_FORCE();
      OdbcDataReader dri = cmd.ExecuteReader();
      return dri;
    }






    static public string HumanReadableRoleList(string commasepIDlist, string delimStart, string delimEnd)
    {
        if (commasepIDlist.Length == 0)
            return "";

        if (commasepIDlist.StartsWith(","))
        {
            commasepIDlist = "-123" + commasepIDlist;
        }

        OdbcDataReader result =
          HELPERS.RunSqlSelect
          (
           "SELECT c_u_Name FROM t_RBSR_AUFW_u_BusRole WHERE c_id IN (" + commasepIDlist + ") AND c_u_Name NOT LIKE '%//DEL_%' ORDER BY c_u_Name");


        string retval = "";
        while (result.Read())
        {
            retval += delimStart;
            retval += result.GetString(0);
            retval += delimEnd;
        }

        return retval;
    }





    static public string HumanReadableAppList(string commasepIDlist, string delimStart, string delimEnd)
    {
        if (commasepIDlist.Length == 0)
            return "";

        if (commasepIDlist.StartsWith(","))
        {
            commasepIDlist = "-123" + commasepIDlist;
        }

        OdbcDataReader result =
          HELPERS.RunSqlSelect
          (
           "SELECT c_u_Name FROM t_RBSR_AUFW_u_Application WHERE c_id IN (" + commasepIDlist + ") ORDER BY c_u_Name");


        string retval = "";
        while (result.Read())
        {
            retval += delimStart;
            retval += result.GetString(0);
            retval += delimEnd;
        }

        return retval;
    }






    public static void PublishEntAssSet(int wsid, string comment)
    {
      IEntAssignmentSet engine = new IEntAssignmentSet(NewOdbcConn());

      // All that needs to be done is changing the status of the EASet.
      // No changes to the EntAssignment rows at all.


      returnGetEntAssignmentSet curval = engine.GetEntAssignmentSet(wsid);

      // Find all currently active EASets for this subprocess and for any
      // of status "active", change to "archived".
      returnListEntAssignmentSetBySubProcess[] ret =
        engine.ListEntAssignmentSetBySubProcess
        (null, "", new string[] { }, "", curval.SubProcessID);
      for (int i = 0; i < ret.Length; i++)
        {
          if (ret[i].Status.ToLower() == "active")
            {
              engine.SetEntAssignmentSet
                (ret[i].ID,
                 "archived", ret[i].DATETIMElock, ret[i].Commentary,
                 ret[i].SubProcessID, ret[i].UserID, ret[i].DATETIMEbirth);
            }
        }





      if (comment == null)
        {
          comment = curval.Commentary;
        }

      engine.SetEntAssignmentSet(wsid, "ACTIVE", DateTime.Now, comment,
                                 curval.SubProcessID, curval.UserID, curval.DATETIMEbirth);

    }



    public static void RenameApplication
    (OdbcConnection conn, string origname, string newname)
    {
      // No need to test the new name for uniqueness because the 
      // database constraint ensures no duplicate app names.

      OdbcCommand cmd = conn.CreateCommand();
      cmd.CommandText =
        "UPDATE t_RBSR_AUFW_u_Application SET c_u_Name=? WHERE c_u_Name=?";
      cmd.Parameters.Add("newname", OdbcType.NVarChar);
      cmd.Parameters["newname"].Value = (object)newname;
      cmd.Parameters.Add("oldname", OdbcType.NVarChar);
      cmd.Parameters["oldname"].Value = (object)origname;
      cmd.Connection = conn;
      int rv = cmd.ExecuteNonQuery();
      cmd.Dispose();
      if (rv != 1)
        {
          throw new Exception("Attempt to change application name failed.");
        }




        IEntitlement engine = new IEntitlement(conn);
        returnListEntitlement[] list =
            engine.ListEntitlement(null, "\"Application\" = ?", new string[] { origname }, "");
        foreach (returnListEntitlement x in list)
        {
            string newchecksum =
                ENTCHECKSUM(
                x.StandardActivity, x.RoleType, newname,
                x.System, x.Platform, x.EntitlementName, x.EntitlementValue,
                x.AuthObjName, x.AuthObjValue, x.FieldSecName, x.FieldSecValue, x.Level4SecName, x.Level4SecValue);
            engine.SetEntitlement(x.ID,
                x.StandardActivity, x.RoleType, x.System,
                x.Platform, x.EntitlementName, x.EntitlementValue,
                x.AuthObjName, x.AuthObjValue, x.FieldSecName, x.FieldSecValue, x.Level4SecName, x.Level4SecValue,
                x.Commentary, x.GENmanifestValue, newname, newchecksum, x.Status);
        }





      cmd = conn.CreateCommand();
      cmd.CommandText =
        "UPDATE t_RBSR_AUFW_u_MVFormula SET c_u_KEYapplication=? WHERE c_u_KEYapplication=?";
      cmd.Parameters.Add("newname", OdbcType.NVarChar);
      cmd.Parameters["newname"].Value = (object)newname;
      cmd.Parameters.Add("oldname", OdbcType.NVarChar);
      cmd.Parameters["oldname"].Value = (object)origname;
      cmd.Connection = conn;
      rv = cmd.ExecuteNonQuery();
      cmd.Dispose();

    }






    public static void ImportBusRoleAssignmentsFromDataTable
    (DataTable dt, int idUser, int idWorkspace,
     string reqBehaviorIfEntUnknown, string strBusRoleId, Queue RETmsgs,
     int idSubprocess)
    {
        bool variableBusRole;

        int idBusRole;
        if (strBusRoleId == null)
        {
            // The role info will be in the data-table rows, i.e. will vary per-row
            idBusRole = -1;
            variableBusRole = true;
        }
        else
        {
            idBusRole = int.Parse(strBusRoleId);
            variableBusRole = false;
        }

      System.Data.Odbc.OdbcConnection conn = HELPERS.NewOdbcConn();

      IEntitlement Ientit = new IEntitlement(conn);
      IEntAssignment Ientass = new IEntAssignment(conn);
      IBusRole ENGINEbusrole = new IBusRole(conn);

      IEnumerator<System.Data.DataRow> x =
        (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();
      int recordseq = 0;
      Queue msgsWarn = RETmsgs;

      int countNewEntitRegistrations = 0;
      int countNewAssignments = 0;
      int countAssignmentsExtant = 0;
      int countAssignmentsNeedingRescue = 0;
      int countRejectDueToDisallowNewEntitlements = 0;
      int countRejectUnknownApp = 0;

      Dictionary<string,int> DICTknownApplications 
        = new Dictionary<string,int>();

      if (reqBehaviorIfEntUnknown != "REJECT")
        {
          IApplication engineApplist = new IApplication(conn);
          returnListApplication[] ret = engineApplist.ListApplication(null);
          for (int i = 0; i < ret.Length; i++)
            {
              DICTknownApplications.Add(ret[i].Name, 1);
            }
        }


      while (x.MoveNext())
        {
          recordseq++;


          if (x.Current["Application"] == null)
          {
              // Often the end of the csv file has just lots of blank rows.
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because Application field is blank");
              continue;
          }


/*
 *        if (x.Current[0].Equals(System.DBNull.Value))
            {
              // Ignore any line with no value in the first field.
              // Often the end of the csv file has just lots of blank rows.
              msgsWarn.Enqueue("WARNING: Record " + recordseq + ": Ignored because first field blank");
              continue;
            }
          */



          string curChecksum;

          int idEntitlementVector =
            HELPERS.FindEntitlementByVector(x.Current, out curChecksum);

          if (idEntitlementVector < 0)
            {
              // Ent vector is not yet known.
              bool doRegister = true;
              string autoregisterStatus = "";
              switch (reqBehaviorIfEntUnknown)
                {
                case "REJECT":
                  msgsWarn.Enqueue("ERROR: Record " + recordseq + " rejected because entitlement not previously registered.");
                  countRejectDueToDisallowNewEntitlements++;
                  doRegister = false;
                  break;
                case "ADDp":
                  autoregisterStatus = "P";
                  break;
                case "ADDa":
                  autoregisterStatus = "A";
                  break;
                }

              if (doRegister)
                {
                  countNewEntitRegistrations++;

                  string theAppName = x.Current["Application"].ToString();

                  // Verify the application name:
                  if ( ! DICTknownApplications.ContainsKey(theAppName)) {
                    msgsWarn.Enqueue("ERROR: Record " + recordseq + " rejected because application '" + theAppName + "' not known to system.");
                    countRejectUnknownApp++;
                    continue;
                  }






                  idEntitlementVector = Ientit.NewEntitlement
                    (
                     x.Current["Standard Activity"] as string,
                     x.Current["Type"] as string,
                     x.Current["System"] as string,
                     x.Current["Platform"] as string,
                     x.Current["Entitlement Description"] as string,
                     x.Current["Entitlement Value"] as string,
                     x.Current["Application"] as string,
                     curChecksum
                     );

                  Ientit.SetEntitlement
                    (idEntitlementVector,
                     x.Current["Standard Activity"] as string,
                     x.Current["Type"] as string,
                     x.Current["System"] as string,
                     x.Current["Platform"] as string,
                     x.Current["Entitlement Description"] as string,
                     x.Current["Entitlement Value"] as string,
                     "",
                     x.Current["Authorization Object"] as string,
                     x.Current["Field-Level Security Name"] as string,
                     x.Current["Field-Level Security Value"] as string,
                     x.Current["4th Level Security Name"] as string,
                     x.Current["4th Level Security Value"] as string,
                     x.Current["Additional Comments"] as string,
                     "",
                     x.Current["Application"] as string,
                     curChecksum,
                     autoregisterStatus);

                  if ((x.Current["Application"] as string) == "SAP")
                    {
                      ISAProle engine = new ISAProle(HELPERS.NewOdbcConn());
                      bool wasCreated;
                      SAP_HELPERS.CreateSAPRole
                        (engine,
                         x.Current["Entitlement Value"] as string,
                         idSubprocess,
                         x.Current["System"] as string,
                         "Auto-created from a business-role importation",
                         x.Current["Platform"] as string, "",
                         "-", "-", out wasCreated);
                      if (wasCreated)
                        {
                          msgsWarn.Enqueue
                            ("Automatically created matching SAP role named: " +
                             x.Current["Entitlement Value"] as string + "\n");
                        }
                      else
                        {
                          msgsWarn.Enqueue
                            ("Note: matching SAP role was already present: " +
                             x.Current["Entitlement Value"] as string + "\n");
                        }
                    }
                  
                  
                }
              else
                {
                  continue;
                }

            }




          // If bus role is variable, identify the bus role associated with this row.
          if (variableBusRole)
          {
              string rolename = x.Current["Role"] as string;
              // Find the role that corresponds to the given rolename.
              returnListBusRole[] retbusrole
                  = ENGINEbusrole.ListBusRole(null, " \"Name\" like ?", new string[] { rolename }, "");
              if (retbusrole.GetLength(0) > 1)
              {
                  throw new Exception("Role name " + rolename + " is used multiple times.");
              }
              if (retbusrole.GetLength(0) < 1)
              {
                  // ERROR: the role name is unknown.
                  msgsWarn.Enqueue("WARNING: Record " + recordseq + " rejected because role name '" + rolename + "' is unknown.");
                  continue;
              }

              idBusRole = retbusrole[0].ID;
          }




          // Here is where you make the actual entassignment record
          // We want to avoid creating one that already exists.
          returnListEntAssignmentByBusRole[] listeass = Ientass.ListEntAssignmentByBusRole
            (
             null,
             " \"EntAssignmentSet\"=? AND \"Entitlement\"=?",
             new string[] { idWorkspace.ToString(), idEntitlementVector.ToString() },
             "", idBusRole
             );
          if (listeass.Length == 0)
            {
              // ADD NEW ENTASSIGNMENT!
              Ientass.NewEntAssignment
                (idWorkspace, idBusRole, idEntitlementVector, "N");
              countNewAssignments++;
            }
          else if (listeass.Length == 1)
            {
              switch (listeass[0].Status)
                {
                case "X":
                  // Strange case: this assignment already existed in last ACTIVE easet for this subpr,
                  // but has been deleted in *this* WORKSPACE.
                  // We will not override the workspace deletion, but we will announce this.
                  msgsWarn.Enqueue("URGENT: Record " + recordseq + " ignored because assignment has been specifically DELETED in this workspace.");
                  countAssignmentsNeedingRescue++;
                  break;
                default:
                  msgsWarn.Enqueue("Warning: Record " + recordseq + " ignored because assignment already present in workspace.");
                  countAssignmentsExtant++;
                  break;
                }
            }
          else
            {
              throw new Exception("FATAL ERROR: Multiple ent-assignment rows with identical characteristics - see record " + recordseq);
            }
          
          
        }

      msgsWarn.Enqueue("---------------------");
      msgsWarn.Enqueue("NUMBER OF NEW ENTITLEMENTS REGISTERED: " + countNewEntitRegistrations);
      msgsWarn.Enqueue("NUMBER OF NEW ASSIGNMENTS CREATED: " + countNewAssignments);
      msgsWarn.Enqueue("Number of rows ignored (already assigned): " + countAssignmentsExtant);
      msgsWarn.Enqueue("Number of errors of type: entitlement unknown but no permission to auto-create: " + countRejectDueToDisallowNewEntitlements);
      msgsWarn.Enqueue("Number of errors of type: was assigned but has been explicitly UN-assigned by this workspace's owner: " + countAssignmentsNeedingRescue);
      msgsWarn.Enqueue("Number of errors of type: unknown application: " +  countRejectUnknownApp);
    }





      // Throws exception if any kind of failure occurs.
      public static string CalcManifestString
          (returnGetEntitlement OBJwsent)
      {
          string appname = OBJwsent.Application;

          IMVFormula ENGINEmanif = new IMVFormula(HELPERS.NewOdbcConn());
          returnListMVFormula[] LISTformulas =
            ENGINEmanif.ListMVFormula(null, "\"KEYapplication\" = ?",
                                      new string[] { appname }, "");

          string STRformula = "";

          try
          {
              returnListMVFormula TheFormula = LISTformulas[0];
              if (TheFormula.Formula == null)
              {
                  TheFormula.Formula = "";
              }

              STRformula = HttpUtility.HtmlDecode(TheFormula.Formula.Trim());
          }
          catch (Exception eee) { }

          if (STRformula == "")
          {
              STRformula = "\"TBD - " + appname + "\"";
          }


          return CalcManifestString(OBJwsent, STRformula);
      }



      public static string CalcManifestString
          (returnGetEntitlement OBJwsent, string STRformula)
      {
          string appname = OBJwsent.Application;

          // We have the formula; now we can evaluate.
          Evaluator ev = new Evaluator(Eval3.eParserSyntax.cSharp, false);
          //?????// ev.AddEnvironmentFunctions(this);
          ev.AddEnvironmentFunctions(new ManifestFormulaEvaluatorFunctions(OBJwsent));

          opCode lCode;

          bool doMakeNullRepair2ndTry = false;

          try
          {
              lCode = ev.Parse(STRformula);
          }
          catch (Exception e)
          {
              throw new Exception
                 ("The formula for " + appname + " has parse errors: " + e.ToString());

          }


          object RESLT = null;
          try
          {
              RESLT = lCode.value;
              return RESLT.ToString();
          }
          catch (NullReferenceException enull)
          {
              //row.Cells.Add("Interpreting the formula for this application resulted in an error because of references to one or more fields that are NULL in value.");
              //RESLT = "[ERROR: reference to one or more fields with null values]";
              //row = sheet.Table.Rows.Add();
              doMakeNullRepair2ndTry = true;
          }
          catch (Exception e)
          {
              throw new Exception("Interpreting the formula for this application resulted in this exception: " + e.ToString());
          }



          if (doMakeNullRepair2ndTry)
          {

              int repairCount = 0;
              // NULLs caused failures in the 1st try. 
              // We will thus now repair all nulls and try again.
              // If succeeds, we will generate a useable row but the final column
              //   will alert the users to the fact that autorepair was performed.

              TurnNullsToEmptyStrings(ref OBJwsent, ref repairCount);
              if (repairCount == 0)
              {
                  throw new Exception("ASSERTION FAILURE: null fields expected but not found");
              }


              opCode lCode2;

              ev = new Evaluator(Eval3.eParserSyntax.cSharp, false);
              //?????//ev.AddEnvironmentFunctions(this);
              ev.AddEnvironmentFunctions(new ManifestFormulaEvaluatorFunctions(OBJwsent));
              lCode2 = ev.Parse(STRformula);

              try
              {
                  RESLT = lCode2.value;
              }
              catch (Exception e)
              {
                  throw new Exception("2nd try: Interpreting the formula for this application resulted in this exception: " + e.ToString());

              }

              return RESLT.ToString();
          }

          return "INTERNAL ERROR";
      }




      /*
       * This assumes an up-to-date set of privstrings for the entire entitlement database.
       */
      public static void RemoveEntitlementFromRole(string rolename, string privstring, IBusRole ENGINEbusrole, IEntitlement ENGINE, IEntAssignment IEA)
      {
          // Find the role that corresponds to the given rolename.
          returnListBusRole[] retbusrole
              = ENGINEbusrole.ListBusRole(null, " \"Name\" like ?", new string[] { rolename }, "");
          if (retbusrole.GetLength(0) > 1)
          {
              throw new Exception("Role name " + rolename + " is used multiple times.");
          }
          if (retbusrole.GetLength(0) < 1)
          {
              throw new Exception("Role name " + rolename + " is unknown.");
          }

          int IDbusrole = retbusrole[0].ID;

          // Find its subprocess and make sure there is an open workspace
          int IDsubpr = retbusrole[0].SubProcessID;

          AFWACsession fakesession = new AFWACsession(null);
          fakesession.idSubprocess = IDsubpr;
          fakesession.idUser = -1;
          fakesession.ObtainWorkspaceContext();

          if (fakesession.idWorkspace > 0)
          {
              // Congrats!  There is already a workspace open!
              // We don't care if it belongs to this user or not.
          }
          else
          {
              throw new Exception("Cannot modify role " + rolename + " because its subprocess " + fakesession.nameSubprocess + " is not currently workspaced.");
          }


          // NOW, WE LOOK FOR THE ENTITLEMENT THAT HAS THIS PRIVSTRING.
          returnListEntitlement[] RET = ENGINE.ListEntitlement(null, " \"GENmanifestValue\" = ? ", new string[] { privstring }, "");
          if (RET.GetLength(0) < 1)
          {
              throw new Exception("Privstring " + privstring + " is unknown.");
          }


          /*
           * This had to be turned off because we have a checksum-calc error somewhere that is pervasive
           * and thus causes many duplicates to be constructed.
           *
          if (RET.GetLength(0) > 1)
          {
              throw new Exception("Privstring " + privstring + " is used multiple times.");
          }
           */



          int numAssigns = 0;

          foreach (returnListEntitlement _RET in RET)
          {
              int IDentitlement = _RET.ID;

              // NOW WE LOOK FOR THE ENT-ASSIGNMENT IN THIS WORKSPACE THAT LINKS THIS ROLE TO THIS PRIV
              returnListEntAssignmentByEntAssignmentSet[] returnedEA =
              IEA.ListEntAssignmentByEntAssignmentSet
                  (null, " \"BusRole\"=? AND \"Entitlement\"=? ",
              new string[] { IDbusrole.ToString(), IDentitlement.ToString() }, "", fakesession.idWorkspace);

              numAssigns += returnedEA.GetLength(0);

              if (returnedEA.GetLength(0) == 1)
              {

                  if (returnedEA[0].Status == "X")
                  {
                      throw new Exception("Workspace has already experienced a removal of " + privstring + " from " + rolename);
                  }
                  if (returnedEA[0].Status == "N")
                  {
                      throw new Exception("The assignment of " + privstring + " to " + rolename + " is brand-new in this workspace; this is unexpected and must be handled manually.");
                  }

                  IEA.SetEntAssignment(returnedEA[0].ID, returnedEA[0].EntAssignmentSetID, returnedEA[0].BusRoleID, returnedEA[0].EntitlementID,
                                      "X");
              }
          }

          if (numAssigns == 0)
          {
              throw new Exception("Workspace does not assign " + privstring + " to " + rolename + " so no action to perform.");
          }

          if (numAssigns > 1)
          {
              throw new Exception("Workspace multiply assigns " + privstring + " to " + rolename + "; this is an unexpected situation.");
          }


        
      }

    





      public static string SafeGenericDictionaryLookup
          (Dictionary<string, Object> DICT, string key)
      {
          if (DICT.ContainsKey(key))
          {
              return DICT[key] as string;
          }
          else
          {
              return "";
          }
      }


      private static void TurnNullsToEmptyStrings(ref returnGetEntitlement OBJwsent, ref int repairCount)
      {
          if (OBJwsent.AuthObjName == null)
          {
              OBJwsent.AuthObjName = "";
              repairCount++;
          }
          if (OBJwsent.AuthObjValue == null)
          {
              OBJwsent.AuthObjValue = "";
              repairCount++;
          }
          if (OBJwsent.EntitlementName == null)
          {
              OBJwsent.EntitlementName = "";
              repairCount++;
          }
          if (OBJwsent.EntitlementValue == null)
          {
              OBJwsent.EntitlementValue = "";
              repairCount++;
          }
          if (OBJwsent.FieldSecName == null)
          {
              OBJwsent.FieldSecName = "";
              repairCount++;
          }
          if (OBJwsent.FieldSecValue == null)
          {
              OBJwsent.FieldSecValue = "";
              repairCount++;
          }
          if (OBJwsent.Level4SecName == null)
          {
              OBJwsent.Level4SecName = "";
              repairCount++;
          }
          if (OBJwsent.Level4SecValue == null)
          {
              OBJwsent.Level4SecValue = "";
              repairCount++;
          }
          if (OBJwsent.Platform == null)
          {
              OBJwsent.Platform = "";
              repairCount++;
          }
          if (OBJwsent.RoleType == null)
          {
              OBJwsent.RoleType = "";
              repairCount++;
          }
          if (OBJwsent.StandardActivity == null)
          {
              OBJwsent.StandardActivity = "";
              repairCount++;
          }
          if (OBJwsent.System == null)
          {
              OBJwsent.System = "";
              repairCount++;
          }
      }



    public static DateTime? ParseDate(string thedate)
    {
      if (thedate.Trim() == "")
        {
          return null;
        }
      else
        {
          try
            {
              return DateTime.Parse(thedate);
            }
          catch (Exception e)
            {
              return null;
            }
        }
    }






      public static OdbcDataReader EnumerateAllAppsInScope()
      {
          string sql = @"


SELECT DISTINCT

ENT.c_u_Application


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




WHERE "

    // + " ENT.c_u_Application LIKE '%" + srch + @"%' AND" 

    + @"
  SUBPR.c_u_Status LIKE 'Active'      
AND    BROLE.c_u_Name NOT LIKE '%//DEL_%'     

AND EASSET.c_u_Status='ACTIVE'
AND (EASS.c_u_Status != 'X')


ORDER BY  ENT.c_u_Application";





          OdbcDataReader DR = HELPERS.RunSqlSelect(sql);

          return DR;
      }



    public static void InitChangeMgmtForWS(int idWorkspace)
    {
      IChangeManagementEvent engine = new IChangeManagementEvent(HELPERS.NewOdbcConn());
      returnListChangeManagementEventByEntAssignmentSet[] curExist =
        engine.ListChangeManagementEventByEntAssignmentSet
        (null, idWorkspace);
      if (curExist.Length == 0)
        {
          // We need to create them
          engine.NewChangeManagementEvent("Conceptual Design", idWorkspace);
          engine.NewChangeManagementEvent("Technical Design", idWorkspace);
          engine.NewChangeManagementEvent("Build Verification", idWorkspace);
          engine.NewChangeManagementEvent("Unit Test", idWorkspace);
          // engine.NewChangeManagementEvent("User Acceptance Test - SAP", idWorkspace);
          engine.NewChangeManagementEvent("User Acceptance Test - Non-SAP", idWorkspace);
        }

    }









    public struct infoEA
    {
        public int idEntAss;
        public int idSubpr;
        public int idEntAssSet;
        public string strEntAssSetStatus;
        public int idEntAssSetCreator;
        public string nameEntAssSetCreator;
        public string nameSubprocess;  // includes name of process + subpr
        public int idEntitlement;
        public string nameBusRole;
    };

    public struct infoEASet
    {
        public int idEntAssSet;
        public string strEntAssSetStatus;
        public int idEntAssSetCreator;
        public string nameEntAssSetCreator;
        public string nameSubprocess;  // includes name of process + subpr
    }



      public static void AutoGenWorkspacesInBulk
          (int idThisUser, ref String theResponse, Dictionary<int, infoEASet> MAPsubprToEASet, 
          Dictionary<int, infoEASet> readyToUseWorkspaces, Dictionary<int, infoEASet> lockedWorkspaces,
          string purposeOfEdit)
      {
          foreach (int thiskey in MAPsubprToEASet.Keys)
          {
              infoEASet thisinfo = MAPsubprToEASet[thiskey];
              if (thisinfo.strEntAssSetStatus == "ACTIVE")
              {
                  // Easy!  Just create a new workspace.
                  theResponse += ("CREATING WORKSPACE for " + thisinfo.nameSubprocess + ".\n");
                  int babyWS;
                  try
                  {
                      babyWS =
                        HELPERS.WorkspaceCreate(HELPERS.NewOdbcConn(), thiskey,
                                                idThisUser, thisinfo.idEntAssSet,
                                                "Automatically created to " + purposeOfEdit);
                  }
                  catch (Exception e3)
                  {
                      if (e3.ToString().Contains("already"))
                      {
                          // This is BENIGN.  Simply means that the active WS for this SUBPR already
                          // is known to have NO reference to this entitlement at all.

                          theResponse += ("  Workspace creation not needed - workspace already present.\n");
                          theResponse += ("  Note: the existing workspace already clear of references to this entitlement.\n");
                          continue;
                      }
                      else
                      {
                          theResponse += (e3.ToString());
                          continue;
                      }
                  }
                  thisinfo.idEntAssSet = babyWS;
                  thisinfo.strEntAssSetStatus = "WORKSPACE";
                  thisinfo.idEntAssSetCreator = idThisUser;
                  readyToUseWorkspaces.Add(thiskey, thisinfo);

              }
              else
              {
                  // Here we already have a workspace. 
                  // If already owned by "me", it's ready for use.
                  if (thisinfo.idEntAssSetCreator == idThisUser)
                      readyToUseWorkspaces.Add(thiskey, thisinfo);
                  else
                  {
                      // Owned by a different party.  This is an error situation and
                      // must be logged and reported.
                      theResponse+=("ERROR: Subprocess " + thisinfo.nameSubprocess + " is locked by a workspace owned by " + thisinfo.nameEntAssSetCreator + "\n");
                      theResponse+=("   It will not be possible to make any of the assignment retirements for this subprocess.\n");
                      theResponse+=("   Each assignment that needs to be retired 'manually' will be listed as errors below.\n");
                      theResponse+=("   IMPORTANT: After resolving the workspace issue, you can simply come back and change this entitlement to 'X' status AGAIN.\n");
                      theResponse+=("   (You can make as many re-attempts as you want, until no errors occur.)\n");
                      lockedWorkspaces.Add(thiskey, thisinfo);
                  }
              }
          }
      }




    public static string ReturnListOfBusroleNames(string commasepIdList)
    {
      string retval = "";

      OdbcDataReader reader = RunSqlSelect
	("SELECT c_u_Name FROM t_RBSR_AUFW_u_BusRole WHERE BR.c_id IN (" +
     commasepIdList + ") ORDER BY c_u_Name");

      while (reader.Read())
      {
          retval += " " + reader.GetString(0);
      }

      return retval;
    }




    internal static void EnsureEidInUserDB(string eid)
    {
        throw new NotImplementedException();
    }
  }


}
