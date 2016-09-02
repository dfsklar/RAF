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
using RBSR_AUFW.DB.ITcodeEntitlement;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.ITcodeEntitlement
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.ITcodeEntitlement/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ITcodeEntitlement : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement.NewTcodeEntitlement to insert a row in table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="TCode"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewTcodeEntitlement(string TCode)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement obj = new RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement(dbconn);
			return obj.NewTcodeEntitlement(TCode);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement.DeleteTcodeEntitlement to delete a row from table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteTcodeEntitlement(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement obj = new RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement(dbconn);
			return obj.DeleteTcodeEntitlement(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement.GetTcodeEntitlement to select a row from table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetTcodeEntitlement</returns>
		[WebMethod]
		public returnGetTcodeEntitlement GetTcodeEntitlement(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement obj = new RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement(dbconn);
			return obj.GetTcodeEntitlement(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement.SetTcodeEntitlement to update a row in table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Activity"></param>
		/// <param name="AuthObj"></param>
		/// <param name="OrgAxisList"></param>
		/// <param name="OrgValue"></param>
		/// <param name="Commentary"></param>
		/// <param name="TCode"></param>
		/// <param name="StandardActivity"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetTcodeEntitlement(int ID, string Activity, string AuthObj, string OrgAxisList, string OrgValue, string Commentary, string TCode, string StandardActivity)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement obj = new RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement(dbconn);
			return obj.SetTcodeEntitlement(ID, Activity, AuthObj, OrgAxisList, OrgValue, Commentary, TCode, StandardActivity);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement.ListTcodeEntitlement to select a set of rows from table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListTcodeEntitlement[]</returns>
		[WebMethod]
		public returnListTcodeEntitlement[] ListTcodeEntitlement(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement obj = new RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement(dbconn);
			return obj.ListTcodeEntitlement(maxRowsToReturn);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement.ListTcodeEntitlementByTcodeDictionary to select a set of rows from table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="TcodeDictionaryID"></param>
		/// <returns>returnListTcodeEntitlementByTcodeDictionary[]</returns>
		[WebMethod]
		public returnListTcodeEntitlementByTcodeDictionary[] ListTcodeEntitlementByTcodeDictionary(int? maxRowsToReturn, int TcodeDictionaryID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement obj = new RBSR_AUFW.DB.ITcodeEntitlement.ITcodeEntitlement(dbconn);
			return obj.ListTcodeEntitlementByTcodeDictionary(maxRowsToReturn, TcodeDictionaryID);
		}
	}
}
