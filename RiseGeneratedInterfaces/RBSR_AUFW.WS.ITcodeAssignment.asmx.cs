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
using RBSR_AUFW.DB.ITcodeAssignment;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.ITcodeAssignment
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.ITcodeAssignment/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ITcodeAssignment : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.NewTcodeAssignment to insert a row in table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <param name="SAProleID"></param>
		/// <param name="TcodeEntitlementID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewTcodeAssignment(int TcodeAssignmentSetID, int SAProleID, int TcodeEntitlementID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.NewTcodeAssignment(TcodeAssignmentSetID, SAProleID, TcodeEntitlementID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.DeleteTcodeAssignment to delete a row from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteTcodeAssignment(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.DeleteTcodeAssignment(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.GetTcodeAssignment to select a row from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetTcodeAssignment</returns>
		[WebMethod]
		public returnGetTcodeAssignment GetTcodeAssignment(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.GetTcodeAssignment(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.SetTcodeAssignment to update a row in table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <param name="SAProleID"></param>
		/// <param name="TcodeEntitlementID"></param>
		/// <param name="EditStatus"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetTcodeAssignment(int ID, int TcodeAssignmentSetID, int SAProleID, int TcodeEntitlementID, int EditStatus)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.SetTcodeAssignment(ID, TcodeAssignmentSetID, SAProleID, TcodeEntitlementID, EditStatus);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.ListTcodeAssignment to select a set of rows from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListTcodeAssignment[]</returns>
		[WebMethod]
		public returnListTcodeAssignment[] ListTcodeAssignment(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.ListTcodeAssignment(maxRowsToReturn);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.ListTcodeAssignmentByTcodeAssignmentSet to select a set of rows from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>returnListTcodeAssignmentByTcodeAssignmentSet[]</returns>
		[WebMethod]
		public returnListTcodeAssignmentByTcodeAssignmentSet[] ListTcodeAssignmentByTcodeAssignmentSet(int? maxRowsToReturn, int TcodeAssignmentSetID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.ListTcodeAssignmentByTcodeAssignmentSet(maxRowsToReturn, TcodeAssignmentSetID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.ListTcodeAssignmentBySAProle to select a set of rows from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SAProleID"></param>
		/// <returns>returnListTcodeAssignmentBySAProle[]</returns>
		[WebMethod]
		public returnListTcodeAssignmentBySAProle[] ListTcodeAssignmentBySAProle(int? maxRowsToReturn, int SAProleID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.ListTcodeAssignmentBySAProle(maxRowsToReturn, SAProleID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.ListTcodeAssignmentByTcodeEntitlement to select a set of rows from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="TcodeEntitlementID"></param>
		/// <returns>returnListTcodeAssignmentByTcodeEntitlement[]</returns>
		[WebMethod]
		public returnListTcodeAssignmentByTcodeEntitlement[] ListTcodeAssignmentByTcodeEntitlement(int? maxRowsToReturn, int TcodeEntitlementID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.ListTcodeAssignmentByTcodeEntitlement(maxRowsToReturn, TcodeEntitlementID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.NewRoleTcodeAssignment to insert a row in table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="RoleID"></param>
		/// <param name="TcodeAssignmentID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewRoleTcodeAssignment(int RoleID, int TcodeAssignmentID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.NewRoleTcodeAssignment(RoleID, TcodeAssignmentID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.DeleteRoleTcodeAssignment to delete a row from table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteRoleTcodeAssignment(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.DeleteRoleTcodeAssignment(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.GetRoleTcodeAssignment to select a row from table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetRoleTcodeAssignment</returns>
		[WebMethod]
		public returnGetRoleTcodeAssignment GetRoleTcodeAssignment(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.GetRoleTcodeAssignment(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.SetRoleTcodeAssignment to update a row in table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="RoleID"></param>
		/// <param name="TcodeAssignmentID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetRoleTcodeAssignment(int ID, int RoleID, int TcodeAssignmentID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.SetRoleTcodeAssignment(ID, RoleID, TcodeAssignmentID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment.ListRoleTcodeAssignment to select a set of rows from table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListRoleTcodeAssignment[]</returns>
		[WebMethod]
		public returnListRoleTcodeAssignment[] ListRoleTcodeAssignment(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment obj = new RBSR_AUFW.DB.ITcodeAssignment.ITcodeAssignment(dbconn);
			return obj.ListRoleTcodeAssignment(maxRowsToReturn);
		}
	}
}
