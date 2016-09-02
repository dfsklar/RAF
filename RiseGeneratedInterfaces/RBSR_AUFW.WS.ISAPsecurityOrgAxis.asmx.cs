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
using RBSR_AUFW.DB.ISAPsecurityOrgAxis;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.ISAPsecurityOrgAxis
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.ISAPsecurityOrgAxis/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ISAPsecurityOrgAxis : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis.NewSAPsecurityOrgAxis to insert a row in table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="English_Name"></param>
		/// <param name="SAP_Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewSAPsecurityOrgAxis(string English_Name, string SAP_Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis obj = new RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis(dbconn);
			return obj.NewSAPsecurityOrgAxis(English_Name, SAP_Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis.DeleteSAPsecurityOrgAxis to delete a row from table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteSAPsecurityOrgAxis(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis obj = new RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis(dbconn);
			return obj.DeleteSAPsecurityOrgAxis(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis.GetSAPsecurityOrgAxis to select a row from table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPsecurityOrgAxis</returns>
		[WebMethod]
		public returnGetSAPsecurityOrgAxis GetSAPsecurityOrgAxis(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis obj = new RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis(dbconn);
			return obj.GetSAPsecurityOrgAxis(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis.SetSAPsecurityOrgAxis to update a row in table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="English_Name"></param>
		/// <param name="SAP_Name"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetSAPsecurityOrgAxis(int ID, string English_Name, string SAP_Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis obj = new RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis(dbconn);
			return obj.SetSAPsecurityOrgAxis(ID, English_Name, SAP_Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis.ListSAPsecurityOrgAxis to select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListSAPsecurityOrgAxis[]</returns>
		[WebMethod]
		public returnListSAPsecurityOrgAxis[] ListSAPsecurityOrgAxis(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis obj = new RBSR_AUFW.DB.ISAPsecurityOrgAxis.ISAPsecurityOrgAxis(dbconn);
			return obj.ListSAPsecurityOrgAxis(maxRowsToReturn);
		}
	}
}
