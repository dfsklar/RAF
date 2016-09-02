/*

  This can be used for several purposes.

  If called with:
  mode=activeentlist
  this will simply emit a CSV with the entire
  set of active SAP entitlements for every subprocess
  except those in the TEST process.  Null fields
  are automatically turned to blank strings uncond'ly.

  mode=compare + save=true:
  Does a comparison and saves it to the reconciliation history
  so it can enter workflow.

  mode=compare + save=false:
  Does a comparison and emits an Excel spreadsheet.
  No history saving occurs.


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



	 public class ReconcileSAP : IHttpHandler, IRequiresSessionState
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
		  string pathInput = context.Request.Params["pathinput"];

		  bool boolReportInternalIdmRoles = false;

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

		  DataTable dt_dumpFromSAP = null;


		  if (boolDoCompare)
			 {

				string pathTempFolder = Path.GetDirectoryName(pathInput);
				string pathTempFile = Path.GetFileName(pathInput);


				// Load the CSV file that shows the world as SAP claims it should be.
				dt_dumpFromSAP = HELPERS.LoadCsv(pathTempFolder,
															System.IO.Path.GetFileName(pathTempFile));
			 }



		  OdbcCommand cmd = new OdbcCommand();
		  cmd.Connection = HELPERS.NewOdbcConn();


		  Workbook book = null;
		  WorksheetRow row;
		  Worksheet sheetMetadata = null;
		  Worksheet sheetDeltas = null;




		  if (boolSaveToHistory)
			 {
				ENGINEreport = new IReconcReport(HELPERS.NewOdbcConn());
				IDreport = ENGINEreport.NewReconcReport(DateTime.Now, session.idUser);
				ENGINEreport.SetReconcReport
				  (IDreport, DateTime.Now,
					"Comparison via upload of file " + pathInput, session.idUser, "SAP");
				ENGINEreportDiffItem = new IReconcDiffItem(HELPERS.NewOdbcConn());
			 }

		  else if (boolDoCompare)
			 {
				book = new Workbook();
				context.Response.ContentType = "text/xml";
				context.Response.AddHeader("Content-Disposition",
													"filename=RAFsapReconcile.xls;attachment");

				sheetMetadata = book.Worksheets.Add("Metadata");
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
				row = sheetMetadata.Table.Rows.Add();
				row.Cells.Add("STATISTICS");


				sheetDeltas = book.Worksheets.Add("Reconciliation");
				row = RecordDelta(sheetDeltas,
										"Role Name", "Platform", "Difference", "Action", "Difference Type", "");
			 }
		  else {
			 // This is for mode==dumpraf (export entitlements only)
				context.Response.ContentType = "text/csv";
				context.Response.AddHeader("Content-Disposition",
													"filename=RAF_SAPexport.csv;attachment");
		  }



		  OdbcConnection conn2 = HELPERS.NewOdbcConn_FORCE();



		  string extraconds = " AND ((TEASS.c_u_EditStatus & 4)<>4) " ;

		  // Perform a massive SQL select to obtain all active TCODE entitlements in R-AF
		  string SQL =
			 @"
SELECT

TENT.c_id as EntID,
BROL.c_u_Name as SAProlename,
RTRIM(LTRIM(BROL.c_u_Description)) as SAProleDescription,
BROL.c_u_Platform as platform,
UPPER(TENT.c_u_TCode) as tcode,
BROL.c_u_System as sapsystem,
SUBPR.c_u_Name as subprname

FROM 
   t_RBSR_AUFW_u_TcodeAssignment TEASS

LEFT OUTER JOIN 
   t_RBSR_AUFW_u_TcodeEntitlement TENT
ON
   TEASS.c_r_TcodeEntitlement = TENT.c_id

LEFT OUTER JOIN 
   t_RBSR_AUFW_u_SAProle BROL
ON
   BROL.c_id = TEASS.c_r_SAPRole

LEFT OUTER JOIN
   t_RBSR_AUFW_u_TcodeAssignmentSet TEASET
ON
   TEASET.c_id = TEASS.c_r_TcodeAssignmentSet

LEFT OUTER JOIN 
   t_RBSR_AUFW_u_SubProcess SUBPR
ON
   TEASET.c_r_SubProcess = SUBPR.c_id

WHERE

   (TEASET.c_u_Status = 'ACTIVE')
AND
   (SUBPR.c_r_Process NOT IN (7))
AND
  (
   (BROL.c_id IN (" + CSVlistOfInterestingRoles + ")) OR " +
			 "(BROL.c_r_SubProcess IN (" + CSVlistOfInterestingSubprocesses + ")) ) "

			 + extraconds + 
			 " ORDER BY TEASS.c_r_SAProle;";


		  cmd.CommandText = SQL;
		  OdbcDataReader dr = cmd.ExecuteReader();



		  // Key: SAProlename + '\001' + platform + "\001" + tcodeautomappedtoUPPERCASE
		  // Value: code meaning:
		  //      1 means active in RAF but not (yet) seen in incoming data from SAP
		  //      This is really the only value ever seen.
		  //      If one of these is seen in the incoming data, this item is removed from the dict.
		  Dictionary<string, int> DICTactiveRoleplatToTcode = 
			 new Dictionary<string, int>();

		  string keyDelimiter = "|=|";

		  // These map role names to role descriptions
		  //Dictionary<string, string> DICTactiveBroles = new Dictionary<string, string>();
		  //Dictionary<string, string> DICTidmBroles = new Dictionary<string, string>();


		  while (dr.Read())
			 {
				int IDwsentrow = (int)(dr.GetValue(0));
				string brolename = dr.GetValue(1).ToString();
				string bdescr = dr.GetValue(2).ToString();
				string platform = dr.GetValue(3).ToString();
				string tcode = dr.GetValue(4).ToString().ToUpper();
				string sapsystem = dr.GetValue(5).ToString();
				string subpr = dr.GetValue(6).ToString();

				string key = brolename + keyDelimiter + platform + keyDelimiter + tcode;

      

				if (!boolDoCompare)
				  {
					 context.Response.Write(CSVquoteize(brolename));
					 context.Response.Write(",");
					 context.Response.Write(CSVquoteize(sapsystem));
					 context.Response.Write(",");
					 context.Response.Write(CSVquoteize(platform));
					 context.Response.Write(",");
					 context.Response.Write(CSVquoteize(tcode));
					 context.Response.Write(",");
					 context.Response.Write(CSVquoteize(subpr));
					 context.Response.Write("\n");
				  }
				else
				  {
					 if (!DICTactiveRoleplatToTcode.ContainsKey(key))
						{
						  DICTactiveRoleplatToTcode.Add(key, 1);
						}
				  }
			 }




		  // We have completed the reading of the output from the SELECT.

		  // Now comes the comparison.
		  if (boolDoCompare)
			 {
				Queue<string> QUEUE_idmRowsLackingActiveMatch = new Queue<string>();

				foreach (DataRow idmrow in dt_dumpFromSAP.Rows)
				  {
					 string idmrsrcRolename = idmrow[0].ToString().Trim();
					 string idmrsrcPlatform = idmrow[1].ToString().Trim();
					 string idmrsrcValue = idmrow[2].ToString().Trim();
					 string idmrsrcObjtype = idmrow[3].ToString().Trim().ToLower();

                switch (idmrsrcObjtype)
						{
						case "tcode":
						  idmrsrcValue = idmrsrcValue.ToUpper();
						  break;
						default:
						  throw new Exception("Only TCode data is currently supported in the incoming data from SAP");
						}


					 string idmrsrcPrivForCompare =
						idmrsrcRolename + keyDelimiter + idmrsrcPlatform + keyDelimiter + idmrsrcValue;

					 if (DICTactiveRoleplatToTcode.ContainsKey(idmrsrcPrivForCompare))
						{
						  DICTactiveRoleplatToTcode.Remove(idmrsrcPrivForCompare);
						}
					 else
						{
						  RecordDelta
							 (sheetDeltas, idmrsrcRolename, idmrsrcPlatform, idmrsrcValue, "Remove", "tcode", null);
						  errcountEntitlements++;
						}
				  }



				// Now we are looking for info that was in RAF but unmatched in the incoming.
				// These turn into "ADD" instructions.
				foreach (string keytoadd in DICTactiveRoleplatToTcode.Keys)
				  {
					 StringTok.StringTokenizer TK2 =
						new StringTok.StringTokenizer(keytoadd, keyDelimiter);
					 string curnode2;
					 string reportRolename = TK2.NextToken();
					 string reportPlatform = TK2.NextToken();
					 string reportTcode    = TK2.NextToken();

					 RecordDelta(sheetDeltas, reportRolename, reportPlatform, reportTcode, "Add", "tcode", null);
					 errcountEntitlements++;
				  }



				if (book != null)
				  {
					 row = sheetMetadata.Table.Rows.Add();
					 row.Cells.Add(" - Tcode deltas:");
					 row.Cells.Add(errcountEntitlements.ToString());

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
	  string col1, string col2, string col3, string col4, string col5, string col6optional)
		{
		  if (sheetDeltas != null)
			 {
				WorksheetRow row;
				row = sheetDeltas.Table.Rows.Add();
				row.Cells.Add(col1);
				row.Cells.Add(col2);
				row.Cells.Add(col3);
				row.Cells.Add(col4);
				row.Cells.Add(col5);
				if (col6optional != null)
				  {
					 row.Cells.Add(col6optional);
				  }
				return row;
			 }
		  else
			 {
				RecordDeltaInSavedReport(col1,col2,col3,col4,col5,col6optional);
				return null;
			 }
		}



    private void RecordDeltaInSavedReport
	 (string rolename, string platform, string detail, string verb, string obj, string col6optional)
		{
		  if (verb == "Remove")
			 {
				verb = "Del";
			 }

		  switch (obj)
			 {
			 case "tcode":
				obj = "tcode";
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
