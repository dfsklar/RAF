using System;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using RBSR_AUFW.DB.IMVFormula;
using RBSR_AUFW.DB.IEntitlement;
using Eval3;

using CarlosAg.ExcelXmlWriter;
using RBSR_AUFW.DB.IBusRoleOwner;
using RBSR_AUFW.DB.IUser;
using RBSR_AUFW.DB.IEntAssignmentSet;
using RBSR_AUFW.DB.IBusRole;
using System.Collections.Generic;



namespace _6MAR_WebApplication.export
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler2 : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {

      int idWS = Int32.Parse(context.Request.Params["id"]);

      // Legal formats are:  CSV and XLSX
      string strFormat = "XLSX";
      try
        {
          strFormat = context.Request.Params["fmt"].ToUpper();
        }
      catch (Exception eign) { }


      string annotation = context.Request.Params["name"];

      // Comma-separated list of busrole ID numbers e.g. 34,38,47
      string strRoleList = "";
      try { strRoleList = context.Request.Params["brol"]; }
      catch (Exception eignore) { strRoleList = ""; }


      // Default TRUE
      bool singlesheet = true;
      try { singlesheet = bool.Parse(context.Request.Params["singlesheet"]); }
      catch (Exception eignore) { singlesheet=true; }

      // Default FALSE
      bool showOnlyDeltas = false;
      try { showOnlyDeltas = bool.Parse(context.Request.Params["deltasonly"]); }
      catch (Exception eignore) { showOnlyDeltas=false; }

      // Default TRUE
      bool showStatusColumn = true;
      try { showStatusColumn = bool.Parse(context.Request.Params["showstatus"]); }
      catch (Exception eignore) { showStatusColumn=true; }




      // THE FULL-DETAIL FORMATTING OPTION OVERRIDES ALL OTHER OPTIONS!

      // Default FALSE
      bool BOOLformatFullDetail = false;
      try { BOOLformatFullDetail = bool.Parse(context.Request.Params["FMTfulldetail"]); }
      catch (Exception eignore) { BOOLformatFullDetail=false; }



      bool BOOLformatIdmReconcil = false;
      try { BOOLformatIdmReconcil = bool.Parse(context.Request.Params["FMTidm3col"]); }
      catch (Exception eignore) { BOOLformatIdmReconcil = false; }




      if (BOOLformatFullDetail || BOOLformatIdmReconcil)
      {
        strFormat = "CSV";
        showOnlyDeltas = false;
        singlesheet = true;
        showStatusColumn = false;
      }


      
      OdbcCommand cmd = new OdbcCommand();
      cmd.Connection = HELPERS.NewOdbcConn();


      if (strFormat == "CSV") {
        context.Response.ContentType = "text/csv";
        context.Response.AddHeader("Content-Disposition",
                                   "filename=export.csv;attachment");
      }


      string extraconds = "";

      if (strRoleList != null)
        {
          if (strRoleList != "")
            {
              extraconds = " AND (TEASS.c_r_BusRole IN (" + strRoleList + ")) ";
            }
        }


      if (showOnlyDeltas)
        {
          extraconds +=
            " AND (TEASS.c_u_Status NOT IN ('A')) ";
        }
      else
        {
          extraconds +=
            " AND (TENT.c_u_Status NOT IN ('X')) ";
        }





      cmd.CommandText = @"

SELECT 
TENT.c_id as EntID,";




      if (showStatusColumn) {
        cmd.CommandText +=
          "TEASS.c_u_Status as Mod,";
      }else{
        // If not showing status column, this should be an active entitlement set, not a workspace.
        // Thus, eliminate rows that are marked as having been deleted when that active EASet was published.
        extraconds +=
          " AND (TEASS.c_u_Status NOT IN ('X')) ";
      }


      cmd.CommandText += @"
BROL.c_u_Name as Business_Role_Name,
BROL.c_u_Description as Business_Role_Description,
TENT.c_u_StandardActivity as Standard_Activity,
TENT.c_u_RoleType as Role_Type,
TENT.c_u_Application as Application,
TENT.c_u_System as System,
TENT.c_u_Platform as Platform,
TENT.c_u_EntitlementName as Entitlement_Name,
TENT.c_u_EntitlementValue as Entitlement_Value,
TENT.c_u_AuthObjValue as Authorization_Object_Value,
TENT.c_u_FieldSecName as Field_Security_Name,
TENT.c_u_FieldSecValue as Field_Security_Value,
TENT.c_u_Level4SecName as Fourth_Level_Security_Name,
TENT.c_u_Level4SecValue as Fourth_Level_Security_Value,
TENT.c_u_Commentary as Commentary,
MVF.c_u_Formula as Formula,
SUBPR.c_u_Name as NameSubProcess,
SUBPR.c_id as IDSubProcess,
PR.c_u_Name as NameProcess

FROM 
   t_RBSR_AUFW_u_EntAssignment TEASS


LEFT OUTER JOIN 
   t_RBSR_AUFW_u_Entitlement TENT
ON
   TEASS.c_r_Entitlement = TENT.c_id


LEFT OUTER JOIN 
   t_RBSR_AUFW_u_EntAssignmentSet TEASET
ON
   TEASS.c_r_EntAssignmentSet = TEASET.c_id


LEFT OUTER JOIN 
   t_RBSR_AUFW_u_BusRole BROL
ON
   BROL.c_id = TEASS.c_r_BusRole


LEFT OUTER JOIN 
   t_RBSR_AUFW_u_SubProcess SUBPR
ON
   SUBPR.c_id = TEASET.c_r_SubProcess


LEFT OUTER JOIN 
   t_RBSR_AUFW_u_Process PR
ON
   PR.c_id = SUBPR.c_r_Process


LEFT OUTER JOIN
   t_RBSR_AUFW_u_MVFormula MVF
ON
   MVF.c_u_KEYapplication = TENT.c_u_Application


WHERE
   (TEASS.c_r_EntAssignmentSet = ?) 
" + extraconds + " ORDER BY TEASS.c_r_BusRole;";



      cmd.Parameters.Add("c_r_EditingWorkspace", OdbcType.Int);
      cmd.Parameters["c_r_EditingWorkspace"].Value = (object)idWS;
      OdbcDataReader dr = cmd.ExecuteReader();


      OdbcConnection conn2 = HELPERS.NewOdbcConn_FORCE();
      IMVFormula ENGINEmanif = new IMVFormula(conn2);
      IEntitlement ENGINEwsent = new IEntitlement(conn2);



      if (strFormat == "CSV")
        {
          if (BOOLformatFullDetail)
              CSVgenerate_FullDetail(context, dr, ENGINEmanif, ENGINEwsent, true/*generate header row*/);
          if (BOOLformatIdmReconcil)
              CSVgenerate_IDMreconciliation_3column
                  (context, dr, ENGINEmanif, ENGINEwsent, true/*generate header row*/);

        }

      if (strFormat == "XLSX")
        {

          Workbook book = XLSXgenerate(context, dr, ENGINEmanif, ENGINEwsent, singlesheet, idWS, strRoleList);
          context.Response.ContentType = "application/vnd.xls";  // used to be "text/xml" but never worked in chrome

          // If the suffix is xlsx, it works in OpenOffice but not in MSOffice!
          // Now trying xls !
          context.Response.AddHeader("Content-Disposition",
                                     "filename=export.xls;attachment");

          book.Save(context.Response.OutputStream);

        }
      

      dr.Close();
    }









      private void CSVgenerate_IDMreconciliation_3column
          (HttpContext context, OdbcDataReader dr, IMVFormula ENGINEmanif, IEntitlement ENGINEwsent,
                                bool BOOLgenerateHeader)
      {

          int colnumFormula = -1;
          int colnumRolename = -1;
          int colnumRoledescr = -1;

          OdbcConnection tempconn = HELPERS.NewOdbcConn_FORCE();
          IBusRole engineBR = new IBusRole(tempconn);
          IBusRoleOwner engineBRole = new IBusRoleOwner(tempconn);

          Dictionary<string, bool> DICTboolHaveSeenThisRole = new Dictionary<string, bool>();

          for (int i = 0; i < dr.VisibleFieldCount; i++)
          {
              switch (dr.GetName(i) as string)
              {
                  case "Formula":
                      colnumFormula = i;
                      break;
                  case "Business_Role_Name":
                      colnumRolename = i;
                      break;
                  case "Business_Role_Description":
                      colnumRoledescr = i;
                      break;

              }
          }

          if (BOOLgenerateHeader)
          {
              context.Response.Write("RoleName,Entitlement,ComparisonObject\n");
          }


                // FOR EACH ROW

      while (dr.Read())
        {
          try
            {

              int IDwsentrow = (int)(dr.GetValue(0));

              object RESLT;
              ComputePrivilegeString(context, ENGINEmanif, ENGINEwsent, IDwsentrow, out RESLT);
              string resultAsStr = RESLT.ToString().Replace(" ", "");

                    // If the first time we are seeing this role, emit its description text.
                    if (!DICTboolHaveSeenThisRole.ContainsKey(dr.GetValue(colnumRolename) as string))
                {
                    string rolename = dr.GetValue(colnumRolename) as string; 
                    context.Response.Write(CSVquoteize(rolename) + ",");
                    context.Response.Write(CSVquoteize(dr.GetValue(colnumRoledescr) as string) + ",");
                    context.Response.Write("RoleDescription");
                    context.Response.Write("\n");
                    DICTboolHaveSeenThisRole[rolename] = true;
                    returnListBusRole[] retFindBRole
                      = engineBR.ListBusRole(null, "\"Name\" = ?", new string[] { rolename }, "");
                    int roleID = retFindBRole[0].ID;
                    returnListBusRoleOwnerByBusRole[] roleowners
                      = engineBRole.ListBusRoleOwnerByBusRole(null, roleID);
                    foreach (returnListBusRoleOwnerByBusRole roleowner in roleowners)
                    {
                        context.Response.Write(CSVquoteize(rolename) + ",");
                        context.Response.Write(roleowner.EID + ",");
                        context.Response.Write(roleowner.RankFriendly.Replace(" ", ""));
                        context.Response.Write("\n");
                    }
                }

              context.Response.Write(CSVquoteize(dr.GetValue(colnumRolename) as string) + ",");

              context.Response.Write(CSVquoteize(resultAsStr.Replace(" ","")) + ",");
              context.Response.Write("Entitlement");
              context.Response.Write("\n");
            }

          catch (Exception e)
            {
              context.Response.Write(e.ToString() + e.StackTrace.ToString() + "\n");
            }
        }

      return;

    }






      private void ComputePrivilegeString(HttpContext context, IMVFormula ENGINEmanif, IEntitlement ENGINEwsent, int IDwsentrow, out object RESLT)
      {
          RESLT = "";

          returnGetEntitlement OBJwsent =
            ENGINEwsent.GetEntitlement(IDwsentrow);

          int repaircount = 0;
          TurnNullsToEmptyStrings(ref OBJwsent, ref repaircount);

          string appname = OBJwsent.Application;

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
              //context.Response.Write("Error: the manifest formula for this application has NOT been specified.");
              //return;
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
              context.Response.Write("The formula [["+STRformula+"]] for this app [["+appname+"]] has parse errors: " + e.ToString());
              return;
          }


          try
          {
              RESLT = lCode.value;
          }
          catch (Exception e)
          {
              RESLT = "NULL";
              //context.Response.Write("Interpreting the formula for this app resulted in errors: " + e.ToString());
              //return;
          }
          return;
      }

 













    private void CSVgenerate_FullDetail(HttpContext context, OdbcDataReader dr, IMVFormula ENGINEmanif, IEntitlement ENGINEwsent,
                             bool BOOLgenerateHeader)
    {
      int colnumFormula = -1;


      if (BOOLgenerateHeader)
        {
          for (int i = 0; i < dr.VisibleFieldCount; i++)
            {
              switch (dr.GetName(i) as string)
                {
                case "Formula":
                  colnumFormula = i;
                  break;
                  /*
                    case "EntID":
                    break;
                  */
                default:
                  context.Response.Write(CSVquoteize(dr.GetName(i).Replace('_',' ')) + ",");
                  break;
                }
            }
          context.Response.Write("Privilege String");
          context.Response.Write("\n");
        }


      // FOR EACH ROW

      while (dr.Read())
        {
          try
            {


              int IDwsentrow = (int)(dr.GetValue(0));

              object RESLT;
              ComputePrivilegeString(context, ENGINEmanif, ENGINEwsent, IDwsentrow, out RESLT);

              for (int i = 0; i < dr.VisibleFieldCount; i++)
                {
                  if (i != 0)
                    {
                      if (i != colnumFormula)
                        {
                          context.Response.Write(CSVquoteize(dr.GetValue(i) as string) + ",");
                        }
                    }
                }
              string resultAsStr = RESLT.ToString().Replace(" ", "");
              context.Response.Write(CSVquoteize(resultAsStr));
              context.Response.Write("\n");
            }

          catch (Exception e)
            {
              context.Response.Write(e.ToString() + e.StackTrace.ToString() + "\n");
            }



        }

      return;

    }





    private Workbook XLSXgenerate
      (HttpContext context, OdbcDataReader dr, IMVFormula ENGINEmanif, IEntitlement ENGINEwsent,
       bool singlesheet, int idWS, string strRoleList)
    {
      Workbook book = new Workbook();

      Worksheet sheet = null;

      Worksheet sheetRoleMetadata = null;

      Worksheet sheetEASetMetadata = null;

      int IDsubprocess = -1;



      int colnumFormula = -1;
      int colnumStatus = -1;
      int colnumBusRole = -1;
      int colnumIdSubprocess = -1;

      string currolename = null;

      WorksheetRow row;

      sheetEASetMetadata = book.Worksheets.Add("Context");

      // Create an annotation row with info about the snapshot context.
      RoleMetadata mdat = this.GenerateRoleMetadata(null, idWS);
      bool boolMetadataHasBeenEmitted = false;


      sheetRoleMetadata = book.Worksheets.Add("Role Metadata");


      if (singlesheet)
        {
          sheet = book.Worksheets.Add("Technical Framework");

         

          row = sheet.Table.Rows.Add();

          for (int i = 0; i < dr.VisibleFieldCount; i++)
            {
              switch (dr.GetName(i) as string)
                {
                case "Formula":
                  colnumFormula = i;
                  break;
                case "Mod":
                  colnumStatus = i;
                  row.Cells.Add(dr.GetName(i));
                  break;
                  /*
                    case "EntID":
                    break;
                  */
                case "BusRole":
                  colnumBusRole = i;
                  row.Cells.Add(dr.GetName(i));
                  break;
                case "IDSubProcess":
                  colnumIdSubprocess = i;
                  break;
                default:
                  row.Cells.Add(dr.GetName(i));
                  break;
                }
            }
          row.Cells.Add("Manifest");
          row.Cells.Add("AutoCorrectedNulls");
        }





      if (colnumBusRole < 0)
        {
          // We haven't even gone through the SQL result's columns to determine
          // key column numbers yet!  Special case donehere.
          for (int i = 0; i < dr.VisibleFieldCount; i++)
            {

              switch (dr.GetName(i) as string)
                {
                case "Formula":
                  colnumFormula = i;
                  break;
                case "Mod":
                  colnumStatus = i;
                  break;
                  /*
                    case "EntID":
                    break;
                    * */
                case "Business_Role_Name":
                  colnumBusRole = i;
                  break;
                case "IDSubProcess":
                  colnumIdSubprocess = i;
                  break;
                default:
                  break;
                }
            }
        }


      // FOR EACH ROW

      while (dr.Read())
        {
          try
            {

              if (!boolMetadataHasBeenEmitted)
                {
                  EmitMetadata(sheetEASetMetadata, ref mdat, dr, colnumFormula);
                  boolMetadataHasBeenEmitted = true;
                }


              string newrolename = dr.GetValue(colnumBusRole) as string;

              IDsubprocess = (int) dr.GetValue(colnumIdSubprocess);

              if (singlesheet)
                {
                  bool isBabySheet = false;
                  if (currolename == null)
                    {
                      currolename = newrolename;
                      isBabySheet = true;
                    }
                  else if (currolename != newrolename)
                    {
                      currolename = newrolename;
                      isBabySheet = true;
                    }

                  if (isBabySheet) {

                    WorksheetRow row2 = null;
                      
                    if (sheetRoleMetadata.Table.Rows.Count < 3)
                      {
                        row2 = sheetRoleMetadata.Table.Rows.Add();
                        row2.Cells.Add("ROLE OWNERS/APPROVERS");
                        row2 = sheetRoleMetadata.Table.Rows.Add();
                        row2 = sheetRoleMetadata.Table.Rows.Add();
                        row2.Cells.Add("Role Name");
                        row2.Cells.Add("Type");
                        row2.Cells.Add("EID");
                        row2.Cells.Add("Name");
                        row2.Cells.Add("Geography");
                      }
                    
                    mdat = this.GenerateRoleMetadata(newrolename, idWS);
                    foreach (RoleMetadataOwner x in mdat.ROLEownersident)
                      {
                        row2 = sheetRoleMetadata.Table.Rows.Add();
                        row2.Cells.Add(newrolename);
                        row2.Cells.Add(x.rank);
                        row2.Cells.Add(x.EID);
                        row2.Cells.Add(x.name);
                        row2.Cells.Add(x.geography);
                      }
                  }
                  //                    GenMetadataSpreadsheetRow(sheetRoleMetadata, "--------", "-----------------------");                      
                }

                

              if ( ! singlesheet) {
                bool isBabySheet = false;

                //Note: Excel cannot handle worksheet names longer than 31 chars
                string truncsheetname = newrolename;
                if (truncsheetname.Length > 31) {
                  truncsheetname = truncsheetname.Substring(truncsheetname.Length - 31);
                }
                
                if (currolename == null) {
                  sheet = book.Worksheets.Add(truncsheetname);
                  currolename = newrolename;
                  isBabySheet = true;
                }
                else if (currolename != newrolename) {
                  sheet = book.Worksheets.Add(truncsheetname);
                  isBabySheet = true;
                }


                if (isBabySheet)
                  {
                    // Create an annotation row with info about the snapshot context.
                    mdat = this.GenerateRoleMetadata(newrolename, idWS);

                    GenMetadataSpreadsheetRow(sheet, "Role name:", mdat.rolename);
                    if (mdat.ROLEownersident.Count == 0)
                      {
                        GenMetadataSpreadsheetRow(sheet, "Role owners:", "NOT YET ENTERED");
                      }
                    else
                      {
                        foreach (object x in mdat.ROLEownersident)
                          {
                            GenMetadataSpreadsheetRow(sheet, "Principal: ", x.ToString());
                          }
                      }
                    GenMetadataSpreadsheetRow(sheet, "----------", "---------------------");


                    /*

                      row = sheet.Table.Rows.Add();
                      row.Cells.Add(annotation);
                    */


                    currolename = newrolename;
                    row = sheet.Table.Rows.Add();

                    for (int i = 0; i < dr.VisibleFieldCount; i++)
                      {

                        switch (dr.GetName(i) as string)
                          {
                          case "Formula":
                            colnumFormula = i;
                            break;
                          case "Mod":
                            colnumStatus = i;
                            row.Cells.Add(dr.GetName(i));
                            break;
                            /*
                              case "EntID":
                              break;
                              * */
                          case "BusRole":
                            colnumBusRole = i;
                            row.Cells.Add(dr.GetName(i));
                            break;
			  case "IDSubProcess":
			    colnumIdSubprocess = i;
			    break;
                          default:
                            row.Cells.Add(dr.GetName(i));
                            break;
                          }
                      }
                    row.Cells.Add("Manifest");
                  }
              }


              row = sheet.Table.Rows.Add();

              int IDwsentrow = (int)(dr.GetValue(0));

              returnGetEntitlement OBJwsent =
                ENGINEwsent.GetEntitlement(IDwsentrow);

              string appname = OBJwsent.Application;

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
                  //context.Response.Write("Error: the manifest formula for this application has NOT been specified.");
                  //return;
                }

              // We have the formula; now we can evaluate.
              Evaluator ev = new Evaluator(Eval3.eParserSyntax.cSharp, false);
              ev.AddEnvironmentFunctions(this);
              ev.AddEnvironmentFunctions(new ManifestFormulaEvaluatorFunctions(OBJwsent));

              opCode lCode;

              bool doMakeNullRepair2ndTry = false;

              try
                {
                  lCode = ev.Parse(STRformula);
                }
              catch (Exception e)
                {
                  row.Cells.Add("The formula for " + appname + " has parse errors: " + e.ToString());
                  return book;
                }

              object RESLT = null;
              try
                {
                  RESLT = lCode.value;
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
                  row.Cells.Add("Interpreting the formula for this application resulted in this exception: " + e.ToString());
                  RESLT = "[ERROR: see line above for details]";
                  row = sheet.Table.Rows.Add();
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
                  ev.AddEnvironmentFunctions(this);
                  ev.AddEnvironmentFunctions(new ManifestFormulaEvaluatorFunctions(OBJwsent));
                  lCode2 = ev.Parse(STRformula);

                  try
                    {
                      RESLT = lCode2.value;
                    }
                  catch (Exception e)
                    {
                      row.Cells.Add("2nd try: Interpreting the formula for this application resulted in this exception: " + e.ToString());
                      RESLT = "[ERROR: see line above for details]";
                      row = sheet.Table.Rows.Add();
                    }

                }


              // NOW READY TO EMIT THE COLUMNS

              for (int i = 0; i < dr.VisibleFieldCount; i++)
                {
                  if (i != -342350)  /*used to be != 0 */
                    {
                      if ( (i != colnumFormula) && (i != colnumIdSubprocess) )
                        {
                          if (i == colnumStatus) {
                            string statSemantics = "ERR";
                            switch (dr.GetValue(i) as string) {
                            case "N":
                              statSemantics = "New"; break;
                            case "P":
                              statSemantics = "New"; break;
                            case "X":
                              statSemantics = "Deleted"; break;
                            case "A":
                              statSemantics = ""; break;
                            }
                            row.Cells.Add(statSemantics);
                          }else{   
                            row.Cells.Add(dr.GetValue(i).ToString());
                          }
                        }
                    }
                }

              row.Cells.Add(RESLT as string);
              row.Cells.Add(doMakeNullRepair2ndTry ? "Y" : "");      
            }
          catch (Exception e)
            {
              row = sheet.Table.Rows.Add();
              row.Cells.Add(e.ToString() + e.StackTrace.ToString());
            }

        }



      EmitRoleMetadata_AdditionalRoles
        (sheetRoleMetadata, strRoleList, idWS, IDsubprocess);
      EmitRoleMetadata_FuncAppEntsByRole
        (sheetRoleMetadata, strRoleList, idWS, IDsubprocess);
      EmitRoleMetadata_SubProcessActivities
        (sheetRoleMetadata, strRoleList, idWS, IDsubprocess);



      return book;

    }




    private void EmitMetadata(Worksheet sheetEASetMetadata, ref RoleMetadata mdat, OdbcDataReader dr,
                              int colnumFormula)
    {
      WorksheetRow row;
      GenMetadataSpreadsheetRow(sheetEASetMetadata, "Process:", dr.GetValue(colnumFormula+2).ToString());
      GenMetadataSpreadsheetRow(sheetEASetMetadata, "SubProcess:", dr.GetValue(colnumFormula+1).ToString());
      GenMetadataSpreadsheetRow(sheetEASetMetadata, "----------", "---------");
      GenMetadataSpreadsheetRow(sheetEASetMetadata, "EntSet ID:", mdat.WSid.ToString());
      GenMetadataSpreadsheetRow(sheetEASetMetadata, "EntSet Status:", mdat.WSstatus);
      GenMetadataSpreadsheetRow(sheetEASetMetadata, "EntSet Owner:", mdat.WSownerident);
      if (mdat.WSstatus == "WORKSPACE")
        {
          GenMetadataSpreadsheetRow(sheetEASetMetadata, "EntSet start time:", mdat.WSdateOfImport);
        }
      else
        {
          GenMetadataSpreadsheetRow(sheetEASetMetadata, "EntSet lock time:", mdat.WSdateOfImport);
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

    private void GenMetadataSpreadsheetRow
      (Worksheet sheet, string legend, string value)
    {
      WorksheetRow row = sheet.Table.Rows.Add();
      row.Cells.Add(legend);
      row.Cells.Add(value);
    }


    struct RoleMetadataOwner
    {
      public string EID;
      public string name;
      public string geography;
      public string rank;
    }
    struct RoleMetadata
    {
      public int WSid;
      public string rolename;
      public string WSstatus;
      public string WSdateOfImport;
      public string EID;
      public string WSownerident;  // Each is a string showing EID and name
      public Queue<RoleMetadataOwner> ROLEownersident;  // For each owner: Ownertype, EID, name, geography
    };



    // Send either the numeric UUID or the employee's EID string
    private string UserIdentificationString(int userID, OdbcConnection conn)
    {
      IUser engine = new IUser(conn);
      returnGetUser user = engine.GetUser(userID);
      return user.EID + " " + user.NameSurname + ", " + user.NameFirst;
    }


    // Send either the numeric UUID or the employee's EID string
    private string UserIdentificationString (string eid, OdbcConnection conn)
    {
      IUser engine = new IUser(conn);
      returnListUser[] user = engine.ListUser(null, "\"EID\" like ?",
                                              new string[] { eid }, "");

      return user[0].EID + " " + user[0].NameSurname + ", " + user[0].NameFirst;
    }

    private string UserFriendlyName(string eid, OdbcConnection conn)
    {
      IUser engine = new IUser(conn);
      returnListUser[] user = engine.ListUser(null, "\"EID\" like ?",
                                              new string[] { eid }, "");

      return user[0].NameFirst + " " + user[0].NameSurname;
    }





    private void EmitRoleMetadata_AdditionalRoles(Worksheet sheet, string localbroles, int idEASet, int idSubProcess)
    {
      // Param localbroles is a list of all broles that have been subjected to this
      // report generation, the ones whose additroles are sought.


      // Careful: this SQL could return a particular additrole more than once, if
      // that role happens to have two primary owners.  Take only the first line
      // and ignore any multiple references to a particular additrole.
      string sql =
        @"
SELECT
ORIGBR.c_u_Name as NAME_OrigBusRole,
AVAILBR.c_u_Name AS NAME_AdditionalBusRole, 
(USERR.c_u_NameFirst + ' ' + USERR.c_u_NameSurname) as OWNER_AdditionalBusRole,
ABRLINK.c_u_Comment as Comment,
ABRLINK.c_u_ExpirationDate as ExpDate,
ABRLINK.c_u_RecertificationStartDate as RecertStart,
ABRLINK.c_u_RecertificationInterval as RecertInterval

FROM t_RBSR_AUFW_u_AdditionalBusRole ABRLINK 

LEFT OUTER JOIN t_RBSR_AUFW_u_BusRole AVAILBR ON AVAILBR.c_id=ABRLINK.c_u_idAdditionalBusRole  
LEFT OUTER JOIN t_RBSR_AUFW_u_BusRole ORIGBR ON ORIGBR.c_id=ABRLINK.c_r_BusRole
LEFT OUTER JOIN t_RBSR_AUFW_u_BusRoleOwner AVAILBROWNER 
   ON AVAILBROWNER.c_r_BusRole = AVAILBR.c_id AND AVAILBROWNER.c_u_Rank='OWNprim'
LEFT OUTER JOIN t_RBSR_AUFW_u_User USERR ON USERR.c_u_EID=AVAILBROWNER.c_u_EID

WHERE ";

      sql += " ( ORIGBR.c_r_SubProcess = " + idSubProcess + " ) ";

      if (localbroles != null)
        {
          if (localbroles.Length > 0)
            {
              sql += " AND  ABRLINK.c_r_BusRole IN (" + localbroles + ") ";
            }
        }

      sql += ";";



      WorksheetRow row;
      row = sheet.Table.Rows.Add();
      row = sheet.Table.Rows.Add();
      row = sheet.Table.Rows.Add();
      row = sheet.Table.Rows.Add();
      row = sheet.Table.Rows.Add();
      row.Cells.Add("ADDITIONAL ROLES ALLOWED");
      row = sheet.Table.Rows.Add();
      row = sheet.Table.Rows.Add();
      row.Cells.Add("Role Name assigned to user");
      row.Cells.Add("Available Role Name");
      row.Cells.Add("Role Owner or Approver");
      row.Cells.Add("Comments");
      row.Cells.Add("Expiration");
      row.Cells.Add("Recertification");

      OdbcDataReader DR = HELPERS.RunSqlSelect(sql);

      // Key is an Additional BRole's name
      Dictionary<string,int> DICTseenbefore =  new Dictionary<string,int>();
      while (DR.Read())
        {
          string origbr = DR.GetValue(0) as string;
          string additbr = DR.GetValue(1) as string;
          string additbrowner = DR.GetValue(2) as string;
          string comment = DR.GetValue(3) as string;
          string strExpirDate = SafeDatePrinter(DR.GetValue(4));
          string strRecertStartDate = SafeDatePrinter(DR.GetValue(5));
          string strRecertInterval = DR.GetValue(6) as string;

          if ( ! DICTseenbefore.ContainsKey(additbr)) {
            DICTseenbefore.Add(additbr,1);
            row = sheet.Table.Rows.Add();
            row.Cells.Add(origbr);
            row.Cells.Add(additbr);
            row.Cells.Add(additbrowner);
            row.Cells.Add(comment);
            row.Cells.Add(strExpirDate);
            row.Cells.Add(strRecertInterval +
                          ((strRecertStartDate.Length > 1) ? (" starting " + strRecertStartDate) : ""));
          }
        }
        
    }


  private string SafeDatePrinter(Object val)
  {
    string strExpdate = "";
    try
      {
        strExpdate = ((DateTime)val).ToString("R").Substring(5).Substring(0, 11);
      }
    catch (Exception ignore1) { }
    return strExpdate;
  }



  private void EmitRoleMetadata_FuncAppEntsByRole
    (Worksheet sheet, string localbroles, int idEASet, int idSubProcess)
  {

    // Param localbroles is a list of all broles that have been subjected to this
    // report generation, the ones whose additroles are sought.


    string sql =
      @"
SELECT
ORIGBR.c_u_Name as NAME_BROLE,
ORIGBR.c_u_Description as DESCR_BROLE,
APP.c_u_Name as NAME_APP,
FANOTES.c_u_Comment as COMMENT

FROM t_RBSR_AUFW_u_FuncApplNotes FANOTES

LEFT OUTER JOIN t_RBSR_AUFW_u_BusRole ORIGBR ON ORIGBR.c_id=FANOTES.c_r_BusRole
LEFT OUTER JOIN t_RBSR_AUFW_u_Application APP ON APP.c_id=FANOTES.c_u_REFapplication

WHERE ";

    sql += " ( ORIGBR.c_r_SubProcess = " + idSubProcess + " ) ";

    if (localbroles != null)
      {
        if (localbroles.Length > 0)
          {
            sql += "   AND   FANOTES.c_r_BusRole IN (" + localbroles + ") ";
          }
      }

    sql += " ORDER BY ORIGBR.c_u_Name;";



    WorksheetRow row;
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row.Cells.Add("FUNCTIONAL APPLICATION ENTITLEMENTS BY ROLE");
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row.Cells.Add("Role Name");
    row.Cells.Add("Role Description");
    row.Cells.Add("Application");
    row.Cells.Add("Functional Entitlements");

    OdbcDataReader DR = HELPERS.RunSqlSelect(sql);

    while (DR.Read())
      {
        string origbr = DR.GetValue(0) as string;
        string additbr = DR.GetValue(1) as string;
        string additbrowner = DR.GetValue(2) as string;
        string comment = DR.GetValue(3) as string;

        {

          row = sheet.Table.Rows.Add();
          row.Cells.Add(origbr);
          row.Cells.Add(additbr);
          row.Cells.Add(additbrowner);
          row.Cells.Add(comment);
        }
      }

  }







  private void EmitRoleMetadata_SubProcessActivities
    (Worksheet sheet, string localbroles, int idEASet, int idSubProcess)
  {


    string sql =
      @"
SELECT
ACTIVS.c_u_NodeType,
ACTIVS.c_u_BOOLisKeyPoint,
ACTIVS.c_u_Text,
ACTIVS.c_u_ListIdsBusRoles,
ACTIVS.c_u_ListIdsApps

FROM t_RBSR_AUFW_u_METADATA_SubprToActivityList ACTIVS

WHERE ";

    sql += " ( ACTIVS.c_r_SubProcess = " + idSubProcess + " ) ";

    sql += " ORDER BY ACTIVS.c_u_Sequence;";
          


    WorksheetRow row;
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row.Cells.Add("BUSINESS PROCESS ACTIVITIES");
    row = sheet.Table.Rows.Add();
    row = sheet.Table.Rows.Add();
    row.Cells.Add("Activities");
    row.Cells.Add("Performed by");
    row.Cells.Add("System Mapping");

    OdbcDataReader DR = HELPERS.RunSqlSelect(sql);

    while (DR.Read())
      {
        string nodetype = DR.GetValue(0) as string;
        bool isKeyPoint = (bool) (DR.GetValue(1));
        string comment = DR.GetValue(2) as string;
        string listIdBusRoles = HELPERS.HumanReadableRoleList(DR.GetValue(3) as string, "", "; ");
        string listIdApps = HELPERS.HumanReadableAppList(DR.GetValue(4) as string, "", "; ");

        row = sheet.Table.Rows.Add();
        row.Cells.Add(
                      ( (isKeyPoint) ? "***KEY*** " : "")  + comment);
        if (nodetype == "ACT")
          {
            row.Cells.Add(listIdBusRoles);
            row.Cells.Add(listIdApps);
          }
      }

  }







  /* Passing rolename as null is OK: generates just metadata about the entset context */
  private RoleMetadata GenerateRoleMetadata(string rolename, int wsID)
  {
    OdbcConnection tempconn = HELPERS.NewOdbcConn();

    RoleMetadata newbee = new RoleMetadata();

    IEntAssignmentSet engineEASet = new IEntAssignmentSet(tempconn);

    returnGetEntAssignmentSet eas = engineEASet.GetEntAssignmentSet(wsID);

    newbee.WSid = wsID;
    newbee.WSstatus = eas.Status;
    newbee.WSownerident =
      UserIdentificationString(eas.UserID, tempconn);
    newbee.WSdateOfImport =
      (eas.Status == "WORKSPACE") ? eas.DATETIMEbirth.ToString() :
      eas.DATETIMElock.ToString();
        
        
    newbee.ROLEownersident = new Queue<RoleMetadataOwner>();

    if (rolename != null)
      {
        int roleID;


        IBusRole engineBR = new IBusRole(tempconn);
        returnListBusRole[] retFindBRole
          = engineBR.ListBusRole(null, "\"Name\" = ?", new string[] { rolename }, "");

        if (retFindBRole.Length < 1)
        {
            // If we get here, it's probably the case that rolename ends in "//DEL",
            // i.e. representing a deleted role.
            // throw new Exception("Rolename " + rolename + " not found");
            roleID = -1;
            // By setting roleID to this, we ensure that the list of role owners
            // (computed by the logic immediately below)
            // will just create an empty list.
        }
        else
        {
            roleID = retFindBRole[0].ID;
        }

        newbee.rolename = rolename;

        IBusRoleOwner engineBRole = new IBusRoleOwner(tempconn);
        returnListBusRoleOwnerByBusRole[] roleowners
          = engineBRole.ListBusRoleOwnerByBusRole(null, roleID);

        if (roleowners.Length < 1)
        {
            RoleMetadataOwner baby = new RoleMetadataOwner();
            baby.EID = "";
            baby.geography = "";
            baby.rank = "";
            baby.name = "";
            newbee.ROLEownersident.Enqueue(baby);
        }
        else
        {
            foreach (returnListBusRoleOwnerByBusRole roleowner in roleowners)
            {
                RoleMetadataOwner baby = new RoleMetadataOwner();
                baby.EID = roleowner.EID;
                baby.geography = roleowner.Geography;
                baby.rank = roleowner.RankFriendly;
                baby.name = UserFriendlyName(roleowner.EID, tempconn);
                newbee.ROLEownersident.Enqueue(baby);
            }
        }
      }

    return newbee;
  }




  private void IssueSpreadsheetRowsToShowRoleMetadata(RoleMetadata thedata) 
  {
    // Workspace name:_____
    // Workspace status:  either WORK-IN-PROGRESS or PUBLISHED
    // Workspace owner: ______
    // Workspace creation date:  OR  Workspace publish date:
    // Role name: _______
    // THEN FOR EACH OWNER/APPROVER:
    //     Role XXXXXXownerXXXXX: EID and name and geography
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
