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
using RBSR_AUFW.DB.IBusRole;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IBusRole
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IBusRole.IBusRole
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IBusRole/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IBusRole : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IBusRole.IBusRole obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IBusRole.IBusRole.NewBusRole to insert a row in table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="SubProcessID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewBusRole(string Name, string Description, int SubProcessID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IBusRole.IBusRole obj = new RBSR_AUFW.DB.IBusRole.IBusRole(dbconn);
			return obj.NewBusRole(Name, Description, SubProcessID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IBusRole.IBusRole.DeleteBusRole to delete a row from table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteBusRole(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IBusRole.IBusRole obj = new RBSR_AUFW.DB.IBusRole.IBusRole(dbconn);
			return obj.DeleteBusRole(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IBusRole.IBusRole.GetBusRole to select a row from table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetBusRole</returns>
		[WebMethod]
		public returnGetBusRole GetBusRole(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IBusRole.IBusRole obj = new RBSR_AUFW.DB.IBusRole.IBusRole(dbconn);
			return obj.GetBusRole(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IBusRole.IBusRole.SetBusRole to update a row in table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="Abbrev"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetBusRole(int ID, string Name, string Description, int SubProcessID, string Abbrev)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IBusRole.IBusRole obj = new RBSR_AUFW.DB.IBusRole.IBusRole(dbconn);
			return obj.SetBusRole(ID, Name, Description, SubProcessID, Abbrev);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IBusRole.IBusRole.ListBusRole to select a set of rows from table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListBusRole[]</returns>
		[WebMethod]
		public returnListBusRole[] ListBusRole(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IBusRole.IBusRole obj = new RBSR_AUFW.DB.IBusRole.IBusRole(dbconn);
			return obj.ListBusRole(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IBusRole.IBusRole.ListBusRoleBySubProcess to select a set of rows from table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="SubProcessID"></param>
		/// <returns>returnListBusRoleBySubProcess[]</returns>
		[WebMethod]
		public returnListBusRoleBySubProcess[] ListBusRoleBySubProcess(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int SubProcessID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IBusRole.IBusRole obj = new RBSR_AUFW.DB.IBusRole.IBusRole(dbconn);
			return obj.ListBusRoleBySubProcess(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder, SubProcessID);
		}
	}
}
