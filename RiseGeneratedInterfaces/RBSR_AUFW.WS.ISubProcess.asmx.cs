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
using RBSR_AUFW.DB.ISubProcess;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.ISubProcess
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.ISubProcess.ISubProcess
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.ISubProcess/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ISubProcess : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.ISubProcess.ISubProcess obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISubProcess.ISubProcess.NewSubProcess to insert a row in table t_RBSR_AUFW_u_SubProcess.
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="ProcessID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewSubProcess(string Name, int ProcessID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISubProcess.ISubProcess obj = new RBSR_AUFW.DB.ISubProcess.ISubProcess(dbconn);
			return obj.NewSubProcess(Name, ProcessID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISubProcess.ISubProcess.DeleteSubProcess to delete a row from table t_RBSR_AUFW_u_SubProcess.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteSubProcess(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISubProcess.ISubProcess obj = new RBSR_AUFW.DB.ISubProcess.ISubProcess(dbconn);
			return obj.DeleteSubProcess(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISubProcess.ISubProcess.GetSubProcess to select a row from table t_RBSR_AUFW_u_SubProcess.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSubProcess</returns>
		[WebMethod]
		public returnGetSubProcess GetSubProcess(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISubProcess.ISubProcess obj = new RBSR_AUFW.DB.ISubProcess.ISubProcess(dbconn);
			return obj.GetSubProcess(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISubProcess.ISubProcess.SetSubProcess to update a row in table t_RBSR_AUFW_u_SubProcess.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <param name="ProcessID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetSubProcess(int ID, string Name, int ProcessID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISubProcess.ISubProcess obj = new RBSR_AUFW.DB.ISubProcess.ISubProcess(dbconn);
			return obj.SetSubProcess(ID, Name, ProcessID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISubProcess.ISubProcess.ListSubProcess to select a set of rows from table t_RBSR_AUFW_u_SubProcess.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListSubProcess[]</returns>
		[WebMethod]
		public returnListSubProcess[] ListSubProcess(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISubProcess.ISubProcess obj = new RBSR_AUFW.DB.ISubProcess.ISubProcess(dbconn);
			return obj.ListSubProcess(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISubProcess.ISubProcess.ListSubProcessByProcess to select a set of rows from table t_RBSR_AUFW_u_SubProcess.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="ProcessID"></param>
		/// <returns>returnListSubProcessByProcess[]</returns>
		[WebMethod]
		public returnListSubProcessByProcess[] ListSubProcessByProcess(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int ProcessID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISubProcess.ISubProcess obj = new RBSR_AUFW.DB.ISubProcess.ISubProcess(dbconn);
			return obj.ListSubProcessByProcess(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder, ProcessID);
		}
	}
}
