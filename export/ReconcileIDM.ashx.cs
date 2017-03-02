/*

  This can be used for several purposes.

  If called with:
  mode=activeentlist
  this will simply emit a CSV with the entire
  set of active entitlements for every subprocess
  except those in the TEST process.  Null fields
  are automatically turned to blank strings uncond'ly.

  mode=compare + save=true:
  Does a comparison and saves it to the reconciliation history
  so it can enter workflow.

  mode=compare + save=false:
  Does a comparison and emits an Excel spreadsheet.
  No history saving occurs.

  -----------
 * 
 * Note that the MVFormula is used afresh to create the privilege-string
 * used for the comparison.  I.e. the pre-generated priv string in
 * the entitlements table is IGNORED.

*/



using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.Odbc;
using RBSR_AUFW.DB.IMVFormula;
using RBSR_AUFW.DB.IEntitlement;
using RBSR_AUFW.DB.IReconcReport;
using RBSR_AUFW.DB.IReconcDiffItem;
using Eval3;
using System.Collections.Generic;
using CarlosAg.ExcelXmlWriter;
using System.IO;
using System.Web.SessionState;
using CSharp.Utils;

namespace _6MAR_WebApplication.export
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]



  public class Handler5 : IHttpHandler, IRequiresSessionState
  {


    protected AFWACsession session;


    // The currently open reconc report (if in "save" mode)
    protected IReconcReport ENGINEreport = null;
    protected int IDreport = 0;
    protected IReconcDiffItem ENGINEreportDiffItem = null;





    public void ProcessRequest(HttpContext context)
    {

      if (context.Session["AFWACSESSION"] == null)
        {
          throw new Exception("Must be in a legal R-AF session");
        }

      session = context.Session["AFWACSESSION"] as AFWACsession;        


      string mode = context.Request.Params["mode"];
      // mode=dumpraf
      // mode=compare


      string requestorUserEID = context.Request.Params["usereid"];
      string requestorUserName = context.Request.Params["username"];
      string pathIdmInput = context.Request.Params["pathidminput"];

      bool boolReportInternalIdmRoles = 
        bool.Parse(context.Request.Params["showinternal"]);
      bool boolSaveToHistory =
        bool.Parse(context.Request.Params["save"]);

      bool boolDoCompare = (mode == "compare");
      bool boolCaseSens = false;

      string CSVlistOfInterestingRoles = "-3";
      string CSVlistOfInterestingSubprocesses = "-3";
        
      //
      // New format for the CSVlist:
      // Still comma-separated.
      // But now each item is either:
      //    an integer
      //    "SP/###" where ### is the ID number for a subprocess
      // Our goal is to split this into two CSV lists:
      //   list of interesting roles
      //   list of interesting subprocesses (meaning all its roles are interesting)
      // So here we must tokenize the incoming role list.


      StringTok.StringTokenizer TK = new StringTok.StringTokenizer(context.Request.Params["roles"], ",");
      string curnode;
      while (TK.HasMoreTokens()) 
        {
          curnode = TK.NextToken();
          if (curnode.StartsWith("SP/"))
            {
              curnode = curnode.Substring(3);
              CSVlistOfInterestingSubprocesses += ("," + curnode);
            }
          else
            {
              CSVlistOfInterestingRoles += ("," + curnode);
            }
        
        }
        

      int errcountEntitlements = 0;
      int errcountRoleMetadata = 0;

      DataTable dt_idmdump = null;






      if (boolDoCompare)
        {

          string pathTempFolder = Path.GetDirectoryName(pathIdmInput);
          string pathTempFile = Path.GetFileName(pathIdmInput);


          // Load the IDM-dump CSV file.
          // Load the IDM-dump CSV file.
          // Load the IDM-dump CSV file.
          // Load the IDM-dump CSV file.
          // Load the IDM-dump CSV file.
          dt_idmdump = HELPERS.LoadCsv(pathTempFolder,
                                       System.IO.Path.GetFileName(pathTempFile));
        }





      OdbcCommand cmd = new OdbcCommand();
      cmd.Connection = HELPERS.NewOdbcConn();


      Workbook book = null;
      WorksheetRow row;
      Worksheet sheetMetadata = null;
      Worksheet sheetDeltas = null;
      Worksheet sheetErrors = null;




      if (boolSaveToHistory)
        {
          ENGINEreport = new IReconcReport(HELPERS.NewOdbcConn());
          IDreport = ENGINEreport.NewReconcReport(DateTime.Now, session.idUser);
          ENGINEreport.SetReconcReport
            (IDreport, DateTime.Now,
             "Comparison via upload of file " + pathIdmInput, session.idUser, "IDM");
          ENGINEreportDiffItem = new IReconcDiffItem(HELPERS.NewOdbcConn());
        }

      else

        {
          book = new Workbook();
          context.Response.ContentType = "application/vnd.xls";
          context.Response.AddHeader("Content-Disposition",
                                     "filename=RAFidmReconcile.xls;attachment");

          sheetMetadata = book.Worksheets.Add("Metadata");
          sheetErrors = book.Worksheets.Add("Errors");

          row = sheetMetadata.Table.Rows.Add();
          row.Cells.Add("Run Date:");
          row.Cells.Add(DateTime.Now.ToUniversalTime().ToLongDateString() + " UTC");
          row = sheetMetadata.Table.Rows.Add();
          row.Cells.Add("Run Time:");
          row.Cells.Add(DateTime.Now.ToUniversalTime().ToLongTimeString() + " UTC");
          // row = sheetMetadata.Table.Rows.Add();
          //      row.Cells.Add("User EID:");
          //      row.Cells.Add(requestorUserEID);
          row = sheetMetadata.Table.Rows.Add();
          row.Cells.Add("User Name:");
          row.Cells.Add(requestorUserName);
          row = sheetMetadata.Table.Rows.Add();
          row.Cells.Add("Include list of IDM ELE_INT_ and INT_ roles");
          row.Cells.Add(boolReportInternalIdmRoles.ToString());
          row = sheetMetadata.Table.Rows.Add();
          row = sheetMetadata.Table.Rows.Add();
          row.Cells.Add("STATISTICS");


          sheetDeltas = book.Worksheets.Add("Reconciliation");
          row = RecordDelta(sheetDeltas,
                            "Role Name", "Difference", "Action", "Difference Type", "");
        }





      OdbcConnection conn2 = HELPERS.NewOdbcConn_FORCE();
      IEntitlement ENGINEwsent = new IEntitlement(conn2);



	  string SQLsubpr = @" SELECT c_r_SubProcess, c_u_Name FROM t_RBSR_AUFW_u_BusRole;";
      cmd.CommandText = SQLsubpr;
      OdbcDataReader drSubpr = cmd.ExecuteReader();

      Dictionary<string, string> MAPbrolenamesToSubprids = new Dictionary<string, string>();
      while (drSubpr.Read())
        {
          int IDsubprid = (int)(drSubpr.GetValue(0));
          string brolename = drSubpr.GetValue(1).ToString().ToLower().Trim();
          if (!MAPbrolenamesToSubprids.ContainsKey(brolename))
          {
              MAPbrolenamesToSubprids.Add(brolename, IDsubprid.ToString());  // + " = " + STRsubpr);
          }
		}
      drSubpr.Close();
			 


      string extraconds = " AND (TEASS.c_u_Status NOT IN ('X')) ";

      // Perform a massive SQL select to obtain all active entitlements in R-AF
      string SQL =
        @"
SELECT
TENT.c_id as EntID,
BROL.c_u_Name as BusRole,
RTRIM(LTRIM(BROL.c_u_Description)) as BusRoleDescr,
MVF.c_u_Formula as Formula,
MVF.c_u_KEYapplication as Appl,
OWNERS.c_u_EID as EIDprimaryApprover,
SUBPR.c_u_Name as SubprocessName,
BROL.c_r_SubProcess as SubprocessID

FROM 
   t_RBSR_AUFW_u_EntAssignment TEASS

LEFT OUTER JOIN 
   t_RBSR_AUFW_u_Entitlement TENT
ON
   TEASS.c_r_Entitlement = TENT.c_id

LEFT OUTER JOIN 
   t_RBSR_AUFW_u_BusRole BROL
ON
   BROL.c_id = TEASS.c_r_BusRole

LEFT OUTER JOIN
   t_RBSR_AUFW_u_MVFormula MVF
ON
   MVF.c_u_KEYapplication = TENT.c_u_Application

LEFT OUTER JOIN
   t_RBSR_AUFW_u_EntAssignmentSet TEASET
ON
   TEASET.c_id = TEASS.c_r_EntAssignmentSet

LEFT OUTER JOIN 
   t_RBSR_AUFW_u_SubProcess SUBPR
ON
   TEASET.c_r_SubProcess = SUBPR.c_id

LEFT OUTER JOIN
  t_RBSR_AUFW_u_BusRoleOwner OWNERS
ON
 (  (OWNERS.c_r_BusRole = BROL.c_id)  AND (OWNERS.c_u_Rank='appr') )

WHERE

   (TEASET.c_u_Status = 'ACTIVE')
AND
   (SUBPR.c_r_Process NOT IN (7))
AND
  (
   (BROL.c_id IN (" + CSVlistOfInterestingRoles + ")) OR " +
        "(BROL.c_r_SubProcess IN (" + CSVlistOfInterestingSubprocesses + ")) ) "

        + extraconds + 
        " ORDER BY TEASS.c_r_BusRole;";


      cmd.CommandText = SQL;
      OdbcDataReader dr = cmd.ExecuteReader();


      Dictionary<string, string> DICTactiveEnts = new Dictionary<string, string>();

      // These map role names to role descriptions
      Dictionary<string, string> DICTactiveBroles = new Dictionary<string, string>();
      Dictionary<string, string> DICTidmBroles = new Dictionary<string, string>();

      // Key = rolename + EID of primary approver
      // Value = 1
      Dictionary<string, string> DICTroleApprover = new Dictionary<string, string>();



      Queue<string> QUEUEmsgsWarning = new Queue<string>();



      // WE ARE NOW READING THE DATA COMING FROM R-AF VIA SQL QUERY
      // WE ARE NOW READING THE DATA COMING FROM R-AF VIA SQL QUERY
      // WE ARE NOW READING THE DATA COMING FROM R-AF VIA SQL QUERY
      // WE ARE NOW READING THE DATA COMING FROM R-AF VIA SQL QUERY
      // WE ARE NOW READING THE DATA COMING FROM R-AF VIA SQL QUERY

      while (dr.Read())
        {
          int IDwsentrow = (int)(dr.GetValue(0));
          string brolename = dr.GetValue(1).ToString().ToLower().Trim();
          string broledescr = dr.GetValue(2).ToString();
          string STRformula = dr.GetValue(3).ToString();
          string STRapp = dr.GetValue(4).ToString();
          string STRapproverEID = dr.GetValue(5).ToString();
          string STRsubpr = dr.GetValue(6).ToString();
          int IDsubpr = (int)(dr.GetValue(7));


          if ( ! DICTactiveBroles.ContainsKey(brolename))
         
            {
              DICTactiveBroles.Add(brolename, broledescr);
            }

            if (STRapproverEID.Length > 1)
            {
                try
                {
                    DICTroleApprover.Add(
                        brolename + (char)1 + STRapproverEID, "1");
                }
                catch (Exception) { }
            }

          returnGetEntitlement OBJwsent =
            ENGINEwsent.GetEntitlement(IDwsentrow);

          // SKLAR NOTE: the returnGetEntitlement struct has every column/field already segregated

          int repaircount = 0;
          TurnNullsToEmptyStrings(ref OBJwsent, ref repaircount);

          if (STRformula == "")
            {
              STRformula = "\"TBD - " + dr.GetValue(4).ToString() + "\"";
            }
          else
            {
              STRformula = HttpUtility.HtmlDecode(STRformula.Trim());
            }


          // We have the formula; now we can evaluate.
          Evaluator ev = new Evaluator(Eval3.eParserSyntax.cSharp, false);
          ev.AddEnvironmentFunctions(this);
          ev.AddEnvironmentFunctions(new ManifestFormulaEvaluatorFunctions(OBJwsent));

          opCode lCode;
          try
            {
              lCode = ev.Parse(STRformula);
            }
          catch (Exception e)
            {
                if (book != null)
                {
                    sheetMetadata.Table.Rows.Add().Cells.Add("The formula [[" + STRformula + "]] for this app [[" + STRapp + "]] has parse errors: " + e.ToString());
                }
                else
                {
                    context.Response.Write("The formula [[" + STRformula + "]] for this app [[" + STRapp + "]] has parse errors: " + e.ToString());
                }
              continue;
            }

          string RESLT;
          try
            {
              RESLT = lCode.value.ToString();
            }
          catch (Exception e)
            {
              RESLT = "NULL";
              //context.Response.Write("Interpreting the formula for this app resulted in errors: " + e.ToString());
              //return;
            }


          string RESLTforCompare = RESLT;
          if (!boolCaseSens) {
                    RESLTforCompare = RESLT.ToLower().Replace(" ", "");
          }


          if (!boolDoCompare)
            {
              context.Response.Write(CSVquoteize(brolename));
              context.Response.Write(",");
              context.Response.Write(CSVquoteize(broledescr));
              context.Response.Write(",");
              context.Response.Write(CSVquoteize(RESLTforCompare));
              context.Response.Write("\n");
            }
          else
            {
              // Enter the role name and the privilegestring into a dictionary
              try {
                DICTactiveEnts.Add(brolename + (char)1 + RESLTforCompare, RESLT);
              }
              catch (Exception eduringprivadd) {
                // This situation occurs when two or more distinct entitlements in RAF
                // generate the very same priv string (because they differ in a field that
                // "does not count" towards generation of the priv string.
                QUEUEmsgsWarning.Enqueue
                  ("Role " + brolename + ": privilege was registered redundantly: " + RESLTforCompare);
              }
            }
        }



      // Now comes the comparison.
      if (boolDoCompare)
        {
          Queue<string> QUEUE_idmRowsLackingActiveMatch = new Queue<string>();



          // HERE WE ARE ACTUALLY LOOKING AT THE DATA COMING IN FROM THE IDM DUMP FILE
          // HERE WE ARE ACTUALLY LOOKING AT THE DATA COMING IN FROM THE IDM DUMP FILE
          // HERE WE ARE ACTUALLY LOOKING AT THE DATA COMING IN FROM THE IDM DUMP FILE
          // HERE WE ARE ACTUALLY LOOKING AT THE DATA COMING IN FROM THE IDM DUMP FILE
          // HERE WE ARE ACTUALLY LOOKING AT THE DATA COMING IN FROM THE IDM DUMP FILE
          // HERE WE ARE ACTUALLY LOOKING AT THE DATA COMING IN FROM THE IDM DUMP FILE
          // HERE WE ARE ACTUALLY LOOKING AT THE DATA COMING IN FROM THE IDM DUMP FILE
          // HERE WE ARE ACTUALLY LOOKING AT THE DATA COMING IN FROM THE IDM DUMP FILE

          foreach (DataRow idmrow in dt_idmdump.Rows)
            {
              string idmrsrcRolename = idmrow[0].ToString().ToLower().Trim();
              string idmrsrcValue = idmrow[1].ToString().Trim();
              string idmrsrcObjType = idmrow[2].ToString().Trim();

              switch (idmrsrcObjType) 
                {

                  case "PrimaryApprover":
		          case "RoleApprover":
                      string target = idmrsrcRolename + (char)1 + idmrsrcValue;
                      if (DICTroleApprover.ContainsKey(target))
                      {
                          // Was also found in RAF, so nothing to report.
                          DICTroleApprover.Remove(target);
                      }
                      else
                      {
                          // Was not found in RAF, so report a need for removal
                          RecordDelta(sheetDeltas, idmrsrcRolename, idmrsrcValue, "Remove", "PrimaryApprover", null);
                      }
                      break;



                case "RoleDescription":
                  if ( ! DICTidmBroles.ContainsKey(idmrsrcRolename))
                    {
                      DICTidmBroles.Add(idmrsrcRolename, idmrsrcValue);
                    }
                  break;

                case "Entitlement":
                  string idmrsrcPrivForCompare = idmrsrcValue;
                  if (!boolCaseSens) {
                                idmrsrcPrivForCompare = idmrsrcValue.ToLower().Replace(" ", "");
                  }
                  string idmrsrcKey = idmrsrcRolename + (char)1 + idmrsrcPrivForCompare;
                  if (DICTactiveEnts.ContainsKey(idmrsrcKey)) {
                    DICTactiveEnts.Remove(idmrsrcKey);
                  }else{
                    QUEUE_idmRowsLackingActiveMatch.Enqueue(idmrsrcKey);
                    /*
                      int idxSep = idmrsrcKey.IndexOf((char)1);
                      string reportRolename = idmrsrcKey.Substring(0,idxSep);
                      string reportPriv = idmrsrcKey.Substring(idxSep+1);
                      * */
                    if (!boolReportInternalIdmRoles)
                      {
                        if (idmrsrcRolename.StartsWith("ELE_INT_")
                            ||
                            idmrsrcRolename.StartsWith("INT_"))
                          {
                            continue;
                          }
                      }
                    try
                    {
                        RecordDelta(sheetDeltas, idmrsrcRolename, idmrsrcValue, "Remove", "Entitlement", MAPbrolenamesToSubprids[idmrsrcRolename]);
                    }
                    catch (Exception) {
                        RecordDelta(sheetDeltas, idmrsrcRolename, idmrsrcValue, "Remove", "Entitlement", "Note: this role is not known to the RAF system at all.");
                    }
                    errcountEntitlements++;
                  }
                  break;
                }
            }

          foreach (string keytoadd in DICTactiveEnts.Keys)
            {
              int idxSep = keytoadd.IndexOf((char)1);
              string reportRolename = keytoadd.Substring(0, idxSep);
              string reportPriv = DICTactiveEnts[keytoadd];
              if (!boolReportInternalIdmRoles)
                {
                  if (reportRolename.StartsWith("ELE_INT_")
                      ||
                      reportRolename.StartsWith("INT_"))
                    {
                      continue;
                    }
                }
              RecordDelta(sheetDeltas, reportRolename, reportPriv, "Add", "Entitlement", null);
              errcountEntitlements++;
            }




            foreach (string keytoadd in DICTroleApprover.Keys)
            {
                int idxSep = keytoadd.IndexOf((char)1);
                string reportRolename = keytoadd.Substring(0, idxSep);
                string reportEID = keytoadd.Substring(idxSep+1);
                if (!boolReportInternalIdmRoles)
                {
                    if (reportRolename.StartsWith("ELE_INT_")
                        ||
                        reportRolename.StartsWith("INT_"))
                    {
                        continue;
                    }
                }
                RecordDelta(sheetDeltas, reportRolename, reportEID, "Add", "PrimaryApprover", null);
            }




          // Roles as a whole
          foreach (string rolePresentInRAF in DICTactiveBroles.Keys)
            {
              if (DICTidmBroles.ContainsKey(rolePresentInRAF))
                {
                  // This role is present in both RAF and IDM.
                  // But perhaps differs in description?
                  if (DICTactiveBroles[rolePresentInRAF] != DICTidmBroles[rolePresentInRAF])
                    {
                      // 1. Do a remove
                      RecordDelta(sheetDeltas, 
                                  rolePresentInRAF, 
                                  DICTidmBroles[rolePresentInRAF],
                                  "Remove", "RoleDescription","");
                      // 2. Do an add
                      RecordDelta(sheetDeltas,
                                  rolePresentInRAF, 
                                  DICTactiveBroles[rolePresentInRAF],
                                  "Add", "RoleDescription","");
                      errcountRoleMetadata++;
                    }

                  //Record having seen this.
                  DICTidmBroles.Remove(rolePresentInRAF);
                }
              else
                {
                  RecordDelta(sheetDeltas, rolePresentInRAF, DICTactiveBroles[rolePresentInRAF], "Add", "Role",null);
                  errcountRoleMetadata++;
                }
            }


          foreach (string rolePresentInIDM in DICTidmBroles.Keys)
            {
              if (!boolReportInternalIdmRoles)
                {
                  if (rolePresentInIDM.StartsWith("ELE_INT_")
                      ||
                      rolePresentInIDM.StartsWith("INT_"))
                    {
                      continue;
                    }
                }
              RecordDelta(sheetDeltas, rolePresentInIDM, rolePresentInIDM, "Remove", "Role", null);
              errcountRoleMetadata++;
            }


          if (book != null)
            {
              row = sheetMetadata.Table.Rows.Add();
              row.Cells.Add(" - Entitlement deltas:");
              row.Cells.Add(errcountEntitlements.ToString());

              row = sheetMetadata.Table.Rows.Add();
              row.Cells.Add(" - Role deltas:");
              row.Cells.Add(errcountRoleMetadata.ToString());


              if (QUEUEmsgsWarning.Count > 0) 
                {
                  row = sheetMetadata.Table.Rows.Add();
                  row.Cells.Add(" - Warning messages regarding RAF-side data:");
             
                  while (QUEUEmsgsWarning.Count > 0) 
                    {
                      string msg = QUEUEmsgsWarning.Dequeue();
                      row = sheetMetadata.Table.Rows.Add();
                      row.Cells.Add("");
                      row.Cells.Add(msg);
                    }
                }

              
              book.Save(context.Response.OutputStream);
            }
          else
            {
              context.Response.Redirect("../Page_Reconc_History.aspx");
            }
        }

    }






    private WorksheetRow RecordDelta
      (Worksheet sheetDeltas,
       string col1, string col2, string col3, string col4, string col5optional)
    {
      if (sheetDeltas != null)
        {
          WorksheetRow row;
          row = sheetDeltas.Table.Rows.Add();
          row.Cells.Add(col1);
          row.Cells.Add(col2);
          row.Cells.Add(col3);
          row.Cells.Add(col4);
          if (col5optional != null)
            {
              row.Cells.Add(col5optional);
            }
          return row;
        }
      else
        {
          RecordDeltaInSavedReport(col1,col2,col3,col4,col5optional);
          return null;
        }
    }



    private void RecordDeltaInSavedReport
      (string rolename, string detail, string verb, string obj, string col5optional)
    {
      if (verb == "Remove")
        {
          verb = "Del";
        }

      switch (obj)
        {
        case "RoleDescription":
          obj = "Desc";
          break;
        case "Entitlement":
          obj = "Ent";
          break;
        case "Role":
          obj = "Role";
          break;
        }

      int IDdiff = ENGINEreportDiffItem.NewReconcDiffItem
        (verb, IDreport, obj);
      ENGINEreportDiffItem.SetReconcDiffItem
        (IDdiff, verb, "", "P"/*Pending*/, null, IDreport, obj, rolename, detail, null);      
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



    private string CSVquoteize(string strIN)
    {
      if (strIN == null)
        {
          return "\"\"";
        }
      else
        {
          return "\"" + strIN.Replace("\"", "\"\"") + "\"";
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
