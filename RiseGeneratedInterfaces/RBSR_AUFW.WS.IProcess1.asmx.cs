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
using RBSR_AUFW.DB.IProcess1;

//c5239606-e81b-4173-a133-8a3bf82326d1
//
// New Model (RBSR_AUFW)
// Version 1.374 (#382)
//
// 
// 
//
//c5239606-e81b-4173-a133-8a3bf82326d1

namespace RBSR_AUFW.WS.IProcess1
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IProcess1.IProcess1
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IProcess1/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IProcess1 : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IProcess1.IProcess1 obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess1.IProcess1.NewProcess to insert a row in table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewProcess(string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess1.IProcess1 obj = new RBSR_AUFW.DB.IProcess1.IProcess1(dbconn);
			return obj.NewProcess(Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess1.IProcess1.DeleteProcess to delete a row from table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteProcess(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess1.IProcess1 obj = new RBSR_AUFW.DB.IProcess1.IProcess1(dbconn);
			return obj.DeleteProcess(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess1.IProcess1.GetProcess to select a row from table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetProcess</returns>
		[WebMethod]
		public returnGetProcess GetProcess(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess1.IProcess1 obj = new RBSR_AUFW.DB.IProcess1.IProcess1(dbconn);
			return obj.GetProcess(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess1.IProcess1.SetProcess to update a row in table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetProcess(int ID, string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess1.IProcess1 obj = new RBSR_AUFW.DB.IProcess1.IProcess1(dbconn);
			return obj.SetProcess(ID, Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IProcess1.IProcess1.ListProcess to select a set of rows from table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListProcess[]</returns>
		[WebMethod]
		public returnListProcess[] ListProcess(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IProcess1.IProcess1 obj = new RBSR_AUFW.DB.IProcess1.IProcess1(dbconn);
			return obj.ListProcess(maxRowsToReturn);
		}
	}
}
