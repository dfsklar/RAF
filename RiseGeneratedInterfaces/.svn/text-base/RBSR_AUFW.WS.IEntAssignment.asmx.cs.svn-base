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
using RBSR_AUFW.DB.IEntAssignment;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IEntAssignment
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IEntAssignment.IEntAssignment
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IEntAssignment/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IEntAssignment : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.NewEntAssignment to insert a row in table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="EntAssignmentSetID"></param>
		/// <param name="BusRoleID"></param>
		/// <param name="EntitlementID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewEntAssignment(int EntAssignmentSetID, int BusRoleID, int EntitlementID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.NewEntAssignment(EntAssignmentSetID, BusRoleID, EntitlementID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.DeleteEntAssignment to delete a row from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteEntAssignment(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.DeleteEntAssignment(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.GetEntAssignment to select a row from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEntAssignment</returns>
		[WebMethod]
		public returnGetEntAssignment GetEntAssignment(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.GetEntAssignment(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.SetEntAssignment to update a row in table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <param name="BusRoleID"></param>
		/// <param name="EntitlementID"></param>
		/// <param name="Status"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetEntAssignment(int ID, int EntAssignmentSetID, int BusRoleID, int EntitlementID, string Status)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.SetEntAssignment(ID, EntAssignmentSetID, BusRoleID, EntitlementID, Status);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.ListEntAssignment to select a set of rows from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListEntAssignment[]</returns>
		[WebMethod]
		public returnListEntAssignment[] ListEntAssignment(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.ListEntAssignment(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.ListEntAssignmentByEntAssignmentSet to select a set of rows from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>returnListEntAssignmentByEntAssignmentSet[]</returns>
		[WebMethod]
		public returnListEntAssignmentByEntAssignmentSet[] ListEntAssignmentByEntAssignmentSet(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int EntAssignmentSetID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.ListEntAssignmentByEntAssignmentSet(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder, EntAssignmentSetID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.ListEntAssignmentByBusRole to select a set of rows from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="BusRoleID"></param>
		/// <returns>returnListEntAssignmentByBusRole[]</returns>
		[WebMethod]
		public returnListEntAssignmentByBusRole[] ListEntAssignmentByBusRole(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int BusRoleID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.ListEntAssignmentByBusRole(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder, BusRoleID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.NewRoleEntAssignment to insert a row in table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="RoleID"></param>
		/// <param name="EntAssignmentID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewRoleEntAssignment(int RoleID, int EntAssignmentID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.NewRoleEntAssignment(RoleID, EntAssignmentID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.DeleteRoleEntAssignment to delete a row from table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteRoleEntAssignment(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.DeleteRoleEntAssignment(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.GetRoleEntAssignment to select a row from table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetRoleEntAssignment</returns>
		[WebMethod]
		public returnGetRoleEntAssignment GetRoleEntAssignment(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.GetRoleEntAssignment(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.SetRoleEntAssignment to update a row in table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="RoleID"></param>
		/// <param name="EntAssignmentID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetRoleEntAssignment(int ID, int RoleID, int EntAssignmentID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.SetRoleEntAssignment(ID, RoleID, EntAssignmentID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignment.IEntAssignment.ListRoleEntAssignment to select a set of rows from table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListRoleEntAssignment[]</returns>
		[WebMethod]
		public returnListRoleEntAssignment[] ListRoleEntAssignment(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignment.IEntAssignment obj = new RBSR_AUFW.DB.IEntAssignment.IEntAssignment(dbconn);
			return obj.ListRoleEntAssignment(maxRowsToReturn);
		}
	}
}
