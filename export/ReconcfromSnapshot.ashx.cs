using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using CarlosAg.ExcelXmlWriter;
using System.Web.SessionState;
using System.Data.Odbc;
using System.Collections.Generic;
using RBSR_AUFW.DB.IReconcDiffItem;


namespace _6MAR_WebApplication.export
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class ReconcfromSnapshot : IHttpHandler, IRequiresSessionState
  {

    public void ProcessRequest(HttpContext context)
    {
      int idSnapshot = int.Parse(context.Request.Params["id"]);

      string mode = context.Request.Params["mode"];

      switch (mode) {
      case "exec":
        exportForExecution(idSnapshot, context);
        break;
      case "markdone":
        markdone(idSnapshot, context);
        break;
      }
    }









    public void markdone(int idSnapshot, HttpContext context)
    {
      Queue<int> rowIDsToBeMarkedAsDone =
        context.Session["ReconcfromSnapshot_RowIDs"] as Queue<int>;

      Dictionary<int,string> mapper = 
        context.Session["ReconcfromSnapshot_MapIdToStatus"] as 
        Dictionary<int,string>;

      IReconcDiffItem engine = new IReconcDiffItem(HELPERS.NewOdbcConn());

      foreach (int i in rowIDsToBeMarkedAsDone)
        {
          engine.SetReconcDiffItem(i, mapper[i]);
        }
    }









    public void exportForExecution(int idSnapshot, HttpContext context)
    {
      Workbook book = new Workbook();

      context.Response.ContentType = "text/xml";
      context.Response.AddHeader("Content-Disposition",
                                 "filename=RAFexportReconcileAnalysis.xls;attachment");

      Worksheet sheet4IDM = book.Worksheets.Add("OK (change in IDM)");
      Worksheet sheet4RAF = book.Worksheets.Add("REJECTED (error in R-AF)");

      OdbcCommand cmd = new OdbcCommand();
      cmd.Connection = HELPERS.NewOdbcConn();
      cmd.CommandText = @"
SELECT 
ITEM.c_u_Status,
ITEM.c_u_RoleName,
ITEM.c_u_DiffType,
ITEM.c_u_DiffObject,
ITEM.c_u_Detail,
ITEM.c_id,
ITEM.c_u_Comment,
USSR.c_u_NameSurname, USSR.c_u_NameFirst, USSR.c_u_EID
FROM t_RBSR_AUFW_u_ReconcDiffItem ITEM
LEFT OUTER JOIN t_RBSR_AUFW_u_User USSR ON USSR.c_u_EID = ITEM.c_u_AssignedUser
WHERE 
ITEM.c_u_Status IN ('i','r') AND
ITEM.c_r_ReconcReport = " + idSnapshot + @"
ORDER BY ITEM.c_id
";
      OdbcDataReader dr = cmd.ExecuteReader();

      context.Session["ReconcfromSnapshot_SnapshotID"] = idSnapshot;

      Queue<int> rowIDsToBeMarkedAsDone = new Queue<int>();
      context.Session["ReconcfromSnapshot_RowIDs"] = rowIDsToBeMarkedAsDone;

      Dictionary<int,string> statusToBeMarkedAsDone = new Dictionary<int,string>();
      context.Session["ReconcfromSnapshot_MapIdToStatus"] = statusToBeMarkedAsDone;


      WorksheetRow row = null;

      while (dr.Read()) 
        {
          int rowID = int.Parse(dr.GetValue(5).ToString());
          string charNewStatus = null;
          row = null;
          switch (dr.GetValue(0).ToString())
            {
            case "i":
              row = sheet4IDM.Table.Rows.Add();
              charNewStatus =("I");
              break;
            case "r":
              row = sheet4RAF.Table.Rows.Add();
              charNewStatus =("R");
              break;
            }
            if (row != null)
            {
                row.Cells.Add(dr.GetValue(1).ToString());
                row.Cells.Add(dr.GetValue(2).ToString());
                row.Cells.Add(dr.GetValue(3).ToString());
                row.Cells.Add(dr.GetValue(4).ToString());
                rowIDsToBeMarkedAsDone.Enqueue(rowID);
                if (charNewStatus == null)
                    throw new Exception();
                statusToBeMarkedAsDone.Add(rowID, charNewStatus);
            }
        }

      book.Save(context.Response.OutputStream);
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
