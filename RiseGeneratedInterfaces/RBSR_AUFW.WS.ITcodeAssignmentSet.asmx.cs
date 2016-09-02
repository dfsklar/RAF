using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.Odbc;
using System.IO;
using RBSR_AUFW.DB.ITcodeAssignmentSet;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.ITcodeAssignmentSet
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.ITcodeAssignmentSet/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ITcodeAssignmentSet : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet.NewTcodeAssignmentSet to insert a row in table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="tstamp"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="Status"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewTcodeAssignmentSet(DateTime tstamp, int SubProcessID, int UserID, string Status)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet obj = new RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet(dbconn);
			return obj.NewTcodeAssignmentSet(tstamp, SubProcessID, UserID, Status);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet.DeleteTcodeAssignmentSet to delete a row from table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteTcodeAssignmentSet(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet obj = new RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet(dbconn);
			return obj.DeleteTcodeAssignmentSet(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet.GetTcodeAssignmentSet to select a row from table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetTcodeAssignmentSet</returns>
		[WebMethod]
		public returnGetTcodeAssignmentSet GetTcodeAssignmentSet(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet obj = new RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet(dbconn);
			return obj.GetTcodeAssignmentSet(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet.SetTcodeAssignmentSet to update a row in table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="tstamp"></param>
		/// <param name="Commentary"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="Status"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetTcodeAssignmentSet(int ID, DateTime tstamp, string Commentary, int SubProcessID, int UserID, string Status)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet obj = new RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet(dbconn);
			return obj.SetTcodeAssignmentSet(ID, tstamp, Commentary, SubProcessID, UserID, Status);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet.ListTcodeAssignmentSet to select a set of rows from table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListTcodeAssignmentSet[]</returns>
		[WebMethod]
		public returnListTcodeAssignmentSet[] ListTcodeAssignmentSet(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet obj = new RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet(dbconn);
			return obj.ListTcodeAssignmentSet(maxRowsToReturn);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet.ListTcodeAssignmentSetBySubProcess to select a set of rows from table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="SubProcessID"></param>
		/// <returns>returnListTcodeAssignmentSetBySubProcess[]</returns>
		[WebMethod]
		public returnListTcodeAssignmentSetBySubProcess[] ListTcodeAssignmentSetBySubProcess(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int SubProcessID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet obj = new RBSR_AUFW.DB.ITcodeAssignmentSet.ITcodeAssignmentSet(dbconn);
			return obj.ListTcodeAssignmentSetBySubProcess(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder, SubProcessID);
		}
	}
}
