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
using RBSR_AUFW.DB.IApplication;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IApplication
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IApplication.IApplication
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IApplication/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IApplication : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IApplication.IApplication obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IApplication.IApplication.NewApplication to insert a row in table t_RBSR_AUFW_u_Application.
		/// </summary>
		/// <param name="Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewApplication(string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IApplication.IApplication obj = new RBSR_AUFW.DB.IApplication.IApplication(dbconn);
			return obj.NewApplication(Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IApplication.IApplication.DeleteApplication to delete a row from table t_RBSR_AUFW_u_Application.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteApplication(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IApplication.IApplication obj = new RBSR_AUFW.DB.IApplication.IApplication(dbconn);
			return obj.DeleteApplication(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IApplication.IApplication.GetApplication to select a row from table t_RBSR_AUFW_u_Application.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetApplication</returns>
		[WebMethod]
		public returnGetApplication GetApplication(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IApplication.IApplication obj = new RBSR_AUFW.DB.IApplication.IApplication(dbconn);
			return obj.GetApplication(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IApplication.IApplication.SetApplication to update a row in table t_RBSR_AUFW_u_Application.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetApplication(int ID, string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IApplication.IApplication obj = new RBSR_AUFW.DB.IApplication.IApplication(dbconn);
			return obj.SetApplication(ID, Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IApplication.IApplication.ListApplication to select a set of rows from table t_RBSR_AUFW_u_Application.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListApplication[]</returns>
		[WebMethod]
		public returnListApplication[] ListApplication(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IApplication.IApplication obj = new RBSR_AUFW.DB.IApplication.IApplication(dbconn);
			return obj.ListApplication(maxRowsToReturn);
		}
	}
}
