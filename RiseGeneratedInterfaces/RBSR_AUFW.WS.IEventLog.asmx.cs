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
using RBSR_AUFW.DB.IEventLog;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IEventLog
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IEventLog.IEventLog
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IEventLog/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IEventLog : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IEventLog.IEventLog obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEventLog.IEventLog.NewEventLog to insert a row in table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="TStamp"></param>
		/// <param name="IDuser"></param>
		/// <param name="IPaddr"></param>
		/// <param name="Action"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewEventLog(DateTime TStamp, int IDuser, string IPaddr, string Action)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEventLog.IEventLog obj = new RBSR_AUFW.DB.IEventLog.IEventLog(dbconn);
			return obj.NewEventLog(TStamp, IDuser, IPaddr, Action);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEventLog.IEventLog.DeleteEventLog to delete a row from table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteEventLog(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEventLog.IEventLog obj = new RBSR_AUFW.DB.IEventLog.IEventLog(dbconn);
			return obj.DeleteEventLog(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEventLog.IEventLog.GetEventLog to select a row from table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEventLog</returns>
		[WebMethod]
		public returnGetEventLog GetEventLog(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEventLog.IEventLog obj = new RBSR_AUFW.DB.IEventLog.IEventLog(dbconn);
			return obj.GetEventLog(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEventLog.IEventLog.SetEventLog to update a row in table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="TStamp"></param>
		/// <param name="IDuser"></param>
		/// <param name="IPaddr"></param>
		/// <param name="Action"></param>
		/// <param name="IDobject"></param>
		/// <param name="Detail"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetEventLog(int ID, DateTime TStamp, int IDuser, string IPaddr, string Action, int? IDobject, string Detail)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEventLog.IEventLog obj = new RBSR_AUFW.DB.IEventLog.IEventLog(dbconn);
			return obj.SetEventLog(ID, TStamp, IDuser, IPaddr, Action, IDobject, Detail);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEventLog.IEventLog.ListEventLog to select a set of rows from table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListEventLog[]</returns>
		[WebMethod]
		public returnListEventLog[] ListEventLog(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEventLog.IEventLog obj = new RBSR_AUFW.DB.IEventLog.IEventLog(dbconn);
			return obj.ListEventLog(maxRowsToReturn);
		}
	}
}
