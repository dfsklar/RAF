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
using RBSR_AUFW.DB.IUser1;

//c5239606-e81b-4173-a133-8a3bf82326d1
//
// New Model (RBSR_AUFW)
// Version 1.374 (#382)
//
// 
// 
//
//c5239606-e81b-4173-a133-8a3bf82326d1

namespace RBSR_AUFW.WS.IUser1
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IUser1.IUser1
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IUser1/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IUser1 : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IUser1.IUser1 obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.NewUser to insert a row in table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="EID"></param>
		/// <param name="Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewUser(string EID, string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.NewUser(EID, Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.DeleteUser to delete a row from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteUser(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.DeleteUser(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.GetUser to select a row from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUser</returns>
		[WebMethod]
		public returnGetUser GetUser(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.GetUser(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.SetUser to update a row in table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EID"></param>
		/// <param name="Name"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetUser(int ID, string EID, string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.SetUser(ID, EID, Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.ListUser to select a set of rows from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListUser[]</returns>
		[WebMethod]
		public returnListUser[] ListUser(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.ListUser(maxRowsToReturn);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.NewUserEditingWorkspace to insert a row in table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="EditingWorkspaceID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewUserEditingWorkspace(int UserID, int EditingWorkspaceID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.NewUserEditingWorkspace(UserID, EditingWorkspaceID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.DeleteUserEditingWorkspace to delete a row from table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteUserEditingWorkspace(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.DeleteUserEditingWorkspace(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.GetUserEditingWorkspace to select a row from table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUserEditingWorkspace</returns>
		[WebMethod]
		public returnGetUserEditingWorkspace GetUserEditingWorkspace(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.GetUserEditingWorkspace(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.SetUserEditingWorkspace to update a row in table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="UserID"></param>
		/// <param name="EditingWorkspaceID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetUserEditingWorkspace(int ID, int UserID, int EditingWorkspaceID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.SetUserEditingWorkspace(ID, UserID, EditingWorkspaceID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.ListUserEditingWorkspace to select a set of rows from table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListUserEditingWorkspace[]</returns>
		[WebMethod]
		public returnListUserEditingWorkspace[] ListUserEditingWorkspace(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.ListUserEditingWorkspace(maxRowsToReturn);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.NewUserEntAssignmentSet to insert a row in table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewUserEntAssignmentSet(int UserID, int EntAssignmentSetID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.NewUserEntAssignmentSet(UserID, EntAssignmentSetID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.DeleteUserEntAssignmentSet to delete a row from table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteUserEntAssignmentSet(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.DeleteUserEntAssignmentSet(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.GetUserEntAssignmentSet to select a row from table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUserEntAssignmentSet</returns>
		[WebMethod]
		public returnGetUserEntAssignmentSet GetUserEntAssignmentSet(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.GetUserEntAssignmentSet(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.SetUserEntAssignmentSet to update a row in table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="UserID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetUserEntAssignmentSet(int ID, int UserID, int EntAssignmentSetID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.SetUserEntAssignmentSet(ID, UserID, EntAssignmentSetID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.ListUserEntAssignmentSet to select a set of rows from table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListUserEntAssignmentSet[]</returns>
		[WebMethod]
		public returnListUserEntAssignmentSet[] ListUserEntAssignmentSet(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.ListUserEntAssignmentSet(maxRowsToReturn);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.NewUserTcodeAssignmentSet to insert a row in table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewUserTcodeAssignmentSet(int UserID, int TcodeAssignmentSetID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.NewUserTcodeAssignmentSet(UserID, TcodeAssignmentSetID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.DeleteUserTcodeAssignmentSet to delete a row from table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteUserTcodeAssignmentSet(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.DeleteUserTcodeAssignmentSet(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.GetUserTcodeAssignmentSet to select a row from table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUserTcodeAssignmentSet</returns>
		[WebMethod]
		public returnGetUserTcodeAssignmentSet GetUserTcodeAssignmentSet(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.GetUserTcodeAssignmentSet(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.SetUserTcodeAssignmentSet to update a row in table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="UserID"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetUserTcodeAssignmentSet(int ID, int UserID, int TcodeAssignmentSetID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.SetUserTcodeAssignmentSet(ID, UserID, TcodeAssignmentSetID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser1.IUser1.ListUserTcodeAssignmentSet to select a set of rows from table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListUserTcodeAssignmentSet[]</returns>
		[WebMethod]
		public returnListUserTcodeAssignmentSet[] ListUserTcodeAssignmentSet(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser1.IUser1 obj = new RBSR_AUFW.DB.IUser1.IUser1(dbconn);
			return obj.ListUserTcodeAssignmentSet(maxRowsToReturn);
		}
	}
}
