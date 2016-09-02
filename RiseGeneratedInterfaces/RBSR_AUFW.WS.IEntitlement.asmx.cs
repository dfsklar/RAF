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
using RBSR_AUFW.DB.IEntitlement;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IEntitlement
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IEntitlement.IEntitlement
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IEntitlement/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IEntitlement : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IEntitlement.IEntitlement obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntitlement.IEntitlement.NewEntitlement to insert a row in table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="StandardActivity"></param>
		/// <param name="RoleType"></param>
		/// <param name="System"></param>
		/// <param name="Platform"></param>
		/// <param name="EntitlementName"></param>
		/// <param name="EntitlementValue"></param>
		/// <param name="Application"></param>
		/// <param name="CHECKSUM"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewEntitlement(string StandardActivity, string RoleType, string System, string Platform, string EntitlementName, string EntitlementValue, string Application, string CHECKSUM)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntitlement.IEntitlement obj = new RBSR_AUFW.DB.IEntitlement.IEntitlement(dbconn);
			return obj.NewEntitlement(StandardActivity, RoleType, System, Platform, EntitlementName, EntitlementValue, Application, CHECKSUM);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntitlement.IEntitlement.DeleteEntitlement to delete a row from table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteEntitlement(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntitlement.IEntitlement obj = new RBSR_AUFW.DB.IEntitlement.IEntitlement(dbconn);
			return obj.DeleteEntitlement(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntitlement.IEntitlement.GetEntitlement to select a row from table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEntitlement</returns>
		[WebMethod]
		public returnGetEntitlement GetEntitlement(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntitlement.IEntitlement obj = new RBSR_AUFW.DB.IEntitlement.IEntitlement(dbconn);
			return obj.GetEntitlement(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntitlement.IEntitlement.SetEntitlement to update a row in table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="StandardActivity"></param>
		/// <param name="RoleType"></param>
		/// <param name="System"></param>
		/// <param name="Platform"></param>
		/// <param name="EntitlementName"></param>
		/// <param name="EntitlementValue"></param>
		/// <param name="AuthObjName"></param>
		/// <param name="AuthObjValue"></param>
		/// <param name="FieldSecName"></param>
		/// <param name="FieldSecValue"></param>
		/// <param name="Level4SecName"></param>
		/// <param name="Level4SecValue"></param>
		/// <param name="Commentary"></param>
		/// <param name="GENmanifestValue"></param>
		/// <param name="Application"></param>
		/// <param name="CHECKSUM"></param>
		/// <param name="Status"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetEntitlement(int ID, string StandardActivity, string RoleType, string System, string Platform, string EntitlementName, string EntitlementValue, string AuthObjName, string AuthObjValue, string FieldSecName, string FieldSecValue, string Level4SecName, string Level4SecValue, string Commentary, string GENmanifestValue, string Application, string CHECKSUM, string Status)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntitlement.IEntitlement obj = new RBSR_AUFW.DB.IEntitlement.IEntitlement(dbconn);
			return obj.SetEntitlement(ID, StandardActivity, RoleType, System, Platform, EntitlementName, EntitlementValue, AuthObjName, AuthObjValue, FieldSecName, FieldSecValue, Level4SecName, Level4SecValue, Commentary, GENmanifestValue, Application, CHECKSUM, Status);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntitlement.IEntitlement.ListEntitlement to select a set of rows from table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListEntitlement[]</returns>
		[WebMethod]
		public returnListEntitlement[] ListEntitlement(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntitlement.IEntitlement obj = new RBSR_AUFW.DB.IEntitlement.IEntitlement(dbconn);
			return obj.ListEntitlement(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
	}
}
