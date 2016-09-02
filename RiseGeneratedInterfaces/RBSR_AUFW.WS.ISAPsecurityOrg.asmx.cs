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
using RBSR_AUFW.DB.ISAPsecurityOrg;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.ISAPsecurityOrg
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.ISAPsecurityOrg/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ISAPsecurityOrg : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg.NewSAPsecurityOrg to insert a row in table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewSAPsecurityOrg(string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg obj = new RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg(dbconn);
			return obj.NewSAPsecurityOrg(Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg.DeleteSAPsecurityOrg to delete a row from table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteSAPsecurityOrg(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg obj = new RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg(dbconn);
			return obj.DeleteSAPsecurityOrg(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg.GetSAPsecurityOrg to select a row from table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPsecurityOrg</returns>
		[WebMethod]
		public returnGetSAPsecurityOrg GetSAPsecurityOrg(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg obj = new RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg(dbconn);
			return obj.GetSAPsecurityOrg(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg.SetSAPsecurityOrg to update a row in table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetSAPsecurityOrg(int ID, string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg obj = new RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg(dbconn);
			return obj.SetSAPsecurityOrg(ID, Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg.ListSAPsecurityOrg to select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListSAPsecurityOrg[]</returns>
		[WebMethod]
		public returnListSAPsecurityOrg[] ListSAPsecurityOrg(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg obj = new RBSR_AUFW.DB.ISAPsecurityOrg.ISAPsecurityOrg(dbconn);
			return obj.ListSAPsecurityOrg(maxRowsToReturn);
		}
	}
}
