using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace _6MAR_WebApplication
{




        public class HELPERS
    {

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



        /* Warning: this is still singleton-based */
        /* This just takes whatever is the active EAS for the given subprocess,
         * but probably should receive the EAS ID as input. 
         */
        public static void WorkspaceCreate(OdbcConnection conn, int IDsubprocess, string commentary) 
        {
            IEditingWorkspace Iws = new IEditingWorkspace(conn);

            // BUG: hardwired user ID!
            int IDnewWS = Iws.NewEditingWorkspace(commentary, DateTime.Now, IDsubprocess, 1);


            // Still singleton!!
            EmptyTable(conn, "t_r_BusRoleWorkspaceEntitlement");
            EmptyTable(conn, "WorkspaceEntitlement");


            IWorkspaceEntitlement Iwserows = new IWorkspaceEntitlement(conn);
            IEntAssignmentSet Ieas = new IEntAssignmentSet(conn);
            IEntitlement Ient = new IEntitlement(conn);
            
            /* See above comment: this hardwared active=true logic is not good */
            returnListEntAssignmentSetBySubProcess[] _IDeas =
                Ieas.ListEntAssignmentSetBySubProcess (null, "\"BOOLisActive\" = 1", new string[] { }, "", IDsubprocess);
            int IDeas = _IDeas[0].ID;


            /* Find the list of EAssignments to bring over to the Workspace */
            IEntAssignment Iea = new IEntAssignment(conn);
            returnListEntAssignmentByEntAssignmentSet[] _IDea = 
                Iea.ListEntAssignmentByEntAssignmentSet(null, "", new string[]{}, "", IDeas);
            int numToConvert = _IDea.Length;

            Hashtable dictEntVectorClones = new Hashtable();

            foreach (returnListEntAssignmentByEntAssignmentSet i in _IDea)
            {
                int IDentitlementVector = i.EntitlementID;
                int IDbusrole = i.BusRoleID;
                int IDcloneEntVector = -88;

                if (dictEntVectorClones.ContainsKey(IDentitlementVector))
                {
                    // The entitlement vector was already cloned in the workspace.
                    IDcloneEntVector =
                        (int)dictEntVectorClones[IDentitlementVector];
                }
                else
                {
                    // MUST CLONE THE VECTOR
                    returnGetEntitlement theE = Ient.GetEntitlement(IDentitlementVector);

                    IDcloneEntVector =
                    Iwserows.NewWorkspaceEntitlement
                    (theE.StandardActivity,
                    theE.RoleType,
                    theE.System,
                    theE.Platform,
                    theE.EntitlementName,
                    theE.EntitlementValue,
                    IDnewWS);

                    dictEntVectorClones.Add(IDentitlementVector, IDcloneEntVector);
                }

                // We have the bus.role ID and we now have the ID of the
                // WorkspaceEntitlement object.
                // We now create the tie that binds them.
                RecordLinkFromBusRoleToWSEntitVector
                (conn, IDbusrole, IDcloneEntVector, IDnewWS);

            }

        }

        private static void RecordLinkFromBusRoleToWSEntitVector
            (OdbcConnection conn, int IDbusrole, int IDcloneEntVector, int IDworkspace)
        {
            int rv = 0;
            OdbcCommand cmd = new OdbcCommand();

            cmd.CommandText = "insert into \"t_r_BusRoleWorkspaceEntitlement\" (\"c_r_BusRole\",\"c_r_WorkspaceEntitlement\", \"c_r_EditingWorkspace\") values(?,?,?)  " +
                "select convert(int,SCOPE_IDENTITY())";
            cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
            cmd.Parameters["c_r_BusRole"].Value = (object)IDbusrole;
            cmd.Parameters.Add("c_r_WorkspaceEntitlement", OdbcType.Int);
            cmd.Parameters["c_r_WorkspaceEntitlement"].Value = (object)IDcloneEntVector;
            cmd.Parameters.Add("c_r_EditingWorkspace", OdbcType.Int);
            cmd.Parameters["c_r_EditingWorkspace"].Value = (object)IDworkspace;

            cmd.Connection = conn;
            rv = cmd.ExecuteNonQuery();
            if (rv == 0) throw new Exception("INSERT failed: " + cmd.CommandText);
            cmd.Dispose();
        }

 



        public static DataTable LoadCsv(string importFolder, string strFileName)
        {
            //in some function
            System.Data.Odbc.OdbcConnection conn;
            DataTable dt = new DataTable();
            System.Data.Odbc.OdbcDataAdapter da;
            string connectionString;


            connectionString = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + importFolder + ";";
            conn = new System.Data.Odbc.OdbcConnection(connectionString);

            //we only pass it the folder.  The csv file import is in the query which follows

            da = new System.Data.Odbc.OdbcDataAdapter("select * from [" + strFileName + "]", conn);
            da.Fill(dt);

            return dt;
        }


        /*
         * The version 1 did not care about coalescing vectors that are identical
         * except for the business role.  The number of Entitlement objects
         * created was exactly the number of rows imported from the CSV.
         */
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
                Ieaset.NewEntAssignmentSet(false, DateTime.Now, "TEST", IDsubpr, IDuser);

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

                int IDnewent = Ientit.NewEntitlement(
                    x.Current["Standard Activity"] as string,
                    x.Current["Type"] as string,
                    x.Current["System"] as string,
                    x.Current["Platform"] as string,
                    x.Current["Entitlement Description"] as string,
                    x.Current["Entitlement Value"] as string);

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
                    x.Current["Manifest Value"] as string);

                int IDnewentass = Iea.NewEntAssignment
                    (IDneweas, IDbusrole, IDnewent);

            }

        }




        /*
         * The version 2 coalesces vectors that are identical
         * except for the business role.  Right now it uses a simplistic
         * algorithm that includes the commentary column (not a good thing)
         * and that doesn't look for variations that would be considered
         * not important (whitespaces?).
         */
        public static void ImportDataTableAsNewEntAssignmentSet_v2(
            DataTable dt, int IDuser, int IDsubpr, System.Data.Odbc.OdbcConnection conn)
        {
            IUser Iuser = new IUser(conn);
            ISubProcess Isubpr = new ISubProcess(conn);
            IEntAssignmentSet Ieaset = new IEntAssignmentSet(conn);
            IEntAssignment Iea = new IEntAssignment(conn);
            IEntitlement Ientit = new IEntitlement(conn);
            IBusRole Ibr = new IBusRole(conn);

            int IDneweas =
                Ieaset.NewEntAssignmentSet(false, DateTime.Now, "Import from CSV", IDsubpr, IDuser);

            // These Get function are only useful for a read-only view of fields;
            // not for generating subordinate entities.
            returnGetEntAssignmentSet neweas = Ieaset.GetEntAssignmentSet(IDneweas);

            IEnumerator<System.Data.DataRow> x =
                (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();


            Hashtable signatures = new Hashtable();

            while (x.MoveNext())
            {
                int IDbusrole;

                if (x.Current[0].Equals(System.DBNull.Value))
                {
                    // Ignore any line with no value in the first field.
                    // Often the end of the csv file has just lots of blank rows.
                    continue;
                }

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


                /* Create a "signature" for this entitlement vector so we can
                 * register it and also see if it's been seen before.
                 */
                string thisSignature =
                    x.Current["Standard Activity"] as string + "/\\" +
                    x.Current["Type"] as string + "/\\" +
                    x.Current["System"] as string + "/\\" +
                    x.Current["Platform"] as string + "/\\" +
                    x.Current["Entitlement Description"] as string + "/\\" +
                    x.Current["Entitlement Value"] as string + "/\\" +
                    "" + "/\\" +
                    x.Current["Authorization Object"] as string + "/\\" +
                    x.Current["Field-Level Security Name"] as string + "/\\" +
                    x.Current["Field-Level Security Value"] as string + "/\\" +
                    x.Current["4th Level Security Name"] as string + "/\\" +
                    x.Current["4th Level Security Value"] as string;

                if (signatures.ContainsKey(thisSignature))
                {
                    // Already seen before, so just add this bus.role
                    // to the list of busroles tied to this.
                    int IDnewent = (int) signatures[thisSignature];

                    int IDnewentass = Iea.NewEntAssignment
                    (IDneweas, IDbusrole, IDnewent);

                }
                else
                {
                    int IDnewent = Ientit.NewEntitlement(
                        x.Current["Standard Activity"] as string,
                        x.Current["Type"] as string,
                        x.Current["System"] as string,
                        x.Current["Platform"] as string,
                        x.Current["Entitlement Description"] as string,
                        x.Current["Entitlement Value"] as string);

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
                        x.Current["Manifest Value"] as string);

                    int IDnewentass = Iea.NewEntAssignment
                        (IDneweas, IDbusrole, IDnewent);

                    signatures.Add(thisSignature, IDnewent);
                }
            }
        }


        public static void ImportDataTableAsNewTcodeAssignmentSet(
            DataTable dt, int IDuser, int IDsubpr, System.Data.Odbc.OdbcConnection conn)
        {
            IUser Iuser = new IUser(conn);
            ISubProcess Isubpr = new ISubProcess(conn);
            ITcodeAssignmentSet Ieaset = new ITcodeAssignmentSet(conn);
            ITcodeAssignment Iea = new ITcodeAssignment(conn);
            ITcodeEntitlement Ientit = new ITcodeEntitlement(conn);
            ITcodeDictionary Idict = new ITcodeDictionary(conn);
            ISAProle Isr = new ISAProle(conn);

            int IDneweas =
                Ieaset.NewTcodeAssignmentSet
                (false, DateTime.Now, "Import from CSV", IDsubpr, IDuser);

            // These Get function are only useful for a read-only view of fields;
            // not for generating subordinate entities.
            returnGetTcodeAssignmentSet neweas = Ieaset.GetTcodeAssignmentSet(IDneweas);


            IEnumerator<System.Data.DataRow> x =
                (IEnumerator<System.Data.DataRow>)dt.Rows.GetEnumerator();


            while (x.MoveNext())
            {
                int IDsaprole;
                int IDtcode;

                if (x.Current[0].Equals(System.DBNull.Value))
                {
                    // Ignore any line with no value in the first field.
                    // Often the end of the csv file has just lots of blank rows.
                    continue;
                }

                // Make sure SAP role object exists; create if not.
                string saprole = (string)(x.Current["SAProle"]);
                RBSR_AUFW.DB.ISAProle.returnListSAProle[] xx = Isr.ListSAProle(null, "\"Name\" like ?",
                    new string[] { saprole }, "");
                if (xx.Length < 1)
                {
                    // MUST ADD NEW ONE
                    IDsaprole =
                        Isr.NewSAProle(saprole, IDsubpr);
                }
                else
                {
                    IDsaprole = xx[0].ID;
                }



                // Make sure Tcode exists in the Tcode dictionary
                string tcodeshortname = (string)(x.Current["TCODE Value"]);
                returnListTcodeDictionary[] xxx = Idict.ListTcodeDictionary
                    (null, "\"TcodeID\" like ?",
                    new string[] { tcodeshortname }, "");
                if (xxx.Length < 1)
                {
                    // MUST ADD NEW ONE
                    IDtcode =
                        Idict.NewTcodeDictionary(tcodeshortname, 
                        x.Current["TCODE Description"] as string);
                }
                else
                {
                    IDtcode = xxx[0].ID;
                }
                

                int IDnewent = Ientit.NewTcodeEntitlement(
                    x.Current["Standard Activity"] as string,
                    x.Current["Type"] as string,
                    x.Current["System"] as string,
                    x.Current["Platform"] as string,IDtcode);

                
                    Ientit.SetTcodeEntitlement(IDnewent,
                    x.Current["Standard Activity"] as string,
                    x.Current["Type"] as string,
                    x.Current["System"] as string,
                    x.Current["Platform"] as string,
                    x.Current["AuthObj Name"] as string,
                    x.Current["AuthObj Description"] as string,
                    x.Current["Field-Level Security Value Description"] as string,
                    x.Current["Field-Level Security Value"] as string,
                    x.Current["Additional Comments"] as string,
                    IDtcode);

                
                int IDnewentass = Iea.NewTcodeAssignment(IDneweas, IDsaprole, IDnewent);
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
    }

}
