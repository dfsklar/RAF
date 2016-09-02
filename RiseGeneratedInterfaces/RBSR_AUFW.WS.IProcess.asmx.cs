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
using RBSR_AUFW.DB.IProcess;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IProcess
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IProcess.IProcess
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IProcess/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IProcess : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IProcess.IProcess obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess.IProcess.NewProcess to insert a row in table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewProcess(string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess.IProcess obj = new RBSR_AUFW.DB.IProcess.IProcess(dbconn);
			return obj.NewProcess(Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess.IProcess.DeleteProcess to delete a row from table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteProcess(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess.IProcess obj = new RBSR_AUFW.DB.IProcess.IProcess(dbconn);
			return obj.DeleteProcess(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess.IProcess.GetProcess to select a row from table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetProcess</returns>
		[WebMethod]
		public returnGetProcess GetProcess(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess.IProcess obj = new RBSR_AUFW.DB.IProcess.IProcess(dbconn);
			return obj.GetProcess(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess.IProcess.SetProcess to update a row in table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetProcess(int ID, string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess.IProcess obj = new RBSR_AUFW.DB.IProcess.IProcess(dbconn);
			return obj.SetProcess(ID, Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess.IProcess.ListProcess to select a set of rows from table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListProcess[]</returns>
		[WebMethod]
		public returnListProcess[] ListProcess(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess.IProcess obj = new RBSR_AUFW.DB.IProcess.IProcess(dbconn);
			return obj.ListProcess(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
	}
}
