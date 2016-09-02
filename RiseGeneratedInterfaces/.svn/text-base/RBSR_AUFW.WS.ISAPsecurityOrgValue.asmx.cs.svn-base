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
using RBSR_AUFW.DB.ISAPsecurityOrgValue;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.ISAPsecurityOrgValue
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.ISAPsecurityOrgValue/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ISAPsecurityOrgValue : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue.NewSAPsecurityOrgValue to insert a row in table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="ValueString"></param>
		/// <param name="SAPsecurityOrgAxisID"></param>
		/// <param name="SAPsecurityOrgID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewSAPsecurityOrgValue(string ValueString, int SAPsecurityOrgAxisID, int SAPsecurityOrgID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue obj = new RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue(dbconn);
			return obj.NewSAPsecurityOrgValue(ValueString, SAPsecurityOrgAxisID, SAPsecurityOrgID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue.DeleteSAPsecurityOrgValue to delete a row from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteSAPsecurityOrgValue(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue obj = new RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue(dbconn);
			return obj.DeleteSAPsecurityOrgValue(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue.GetSAPsecurityOrgValue to select a row from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPsecurityOrgValue</returns>
		[WebMethod]
		public returnGetSAPsecurityOrgValue GetSAPsecurityOrgValue(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue obj = new RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue(dbconn);
			return obj.GetSAPsecurityOrgValue(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue.SetSAPsecurityOrgValue to update a row in table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="ValueString"></param>
		/// <param name="SAPsecurityOrgAxisID"></param>
		/// <param name="SAPsecurityOrgID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetSAPsecurityOrgValue(int ID, string ValueString, int SAPsecurityOrgAxisID, int SAPsecurityOrgID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue obj = new RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue(dbconn);
			return obj.SetSAPsecurityOrgValue(ID, ValueString, SAPsecurityOrgAxisID, SAPsecurityOrgID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue.ListSAPsecurityOrgValue to select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListSAPsecurityOrgValue[]</returns>
		[WebMethod]
		public returnListSAPsecurityOrgValue[] ListSAPsecurityOrgValue(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue obj = new RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue(dbconn);
			return obj.ListSAPsecurityOrgValue(maxRowsToReturn);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue.ListSAPsecurityOrgValueBySAPsecurityOrgAxis to select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SAPsecurityOrgAxisID"></param>
		/// <returns>returnListSAPsecurityOrgValueBySAPsecurityOrgAxis[]</returns>
		[WebMethod]
		public returnListSAPsecurityOrgValueBySAPsecurityOrgAxis[] ListSAPsecurityOrgValueBySAPsecurityOrgAxis(int? maxRowsToReturn, int SAPsecurityOrgAxisID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue obj = new RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue(dbconn);
			return obj.ListSAPsecurityOrgValueBySAPsecurityOrgAxis(maxRowsToReturn, SAPsecurityOrgAxisID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue.ListSAPsecurityOrgValueBySAPsecurityOrg to select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SAPsecurityOrgID"></param>
		/// <returns>returnListSAPsecurityOrgValueBySAPsecurityOrg[]</returns>
		[WebMethod]
		public returnListSAPsecurityOrgValueBySAPsecurityOrg[] ListSAPsecurityOrgValueBySAPsecurityOrg(int? maxRowsToReturn, int SAPsecurityOrgID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue obj = new RBSR_AUFW.DB.ISAPsecurityOrgValue.ISAPsecurityOrgValue(dbconn);
			return obj.ListSAPsecurityOrgValueBySAPsecurityOrg(maxRowsToReturn, SAPsecurityOrgID);
		}
	}
}
