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
using RBSR_AUFW.DB.ISAProle;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.ISAProle
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.ISAProle.ISAProle
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.ISAProle/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ISAProle : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.ISAProle.ISAProle obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAProle.ISAProle.NewSAProle to insert a row in table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="SubProcessID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewSAProle(string Name, int SubProcessID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAProle.ISAProle obj = new RBSR_AUFW.DB.ISAProle.ISAProle(dbconn);
			return obj.NewSAProle(Name, SubProcessID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAProle.ISAProle.DeleteSAProle to delete a row from table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteSAProle(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAProle.ISAProle obj = new RBSR_AUFW.DB.ISAProle.ISAProle(dbconn);
			return obj.DeleteSAProle(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAProle.ISAProle.GetSAProle to select a row from table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAProle</returns>
		[WebMethod]
		public returnGetSAProle GetSAProle(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAProle.ISAProle obj = new RBSR_AUFW.DB.ISAProle.ISAProle(dbconn);
			return obj.GetSAProle(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAProle.ISAProle.SetSAProle to update a row in table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="System"></param>
		/// <param name="Platform"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetSAProle(int ID, string Name, string Description, int SubProcessID, string System, string Platform)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAProle.ISAProle obj = new RBSR_AUFW.DB.ISAProle.ISAProle(dbconn);
			return obj.SetSAProle(ID, Name, Description, SubProcessID, System, Platform);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAProle.ISAProle.ListSAProle to select a set of rows from table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListSAProle[]</returns>
		[WebMethod]
		public returnListSAProle[] ListSAProle(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAProle.ISAProle obj = new RBSR_AUFW.DB.ISAProle.ISAProle(dbconn);
			return obj.ListSAProle(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ISAProle.ISAProle.ListSAProleByBusRole to select a set of rows from table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="BusRoleID"></param>
		/// <returns>returnListSAProleByBusRole[]</returns>
		[WebMethod]
		public returnListSAProleByBusRole[] ListSAProleByBusRole(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int BusRoleID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ISAProle.ISAProle obj = new RBSR_AUFW.DB.ISAProle.ISAProle(dbconn);
			return obj.ListSAProleByBusRole(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder, BusRoleID);
		}
	}
}
