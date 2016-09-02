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
using RBSR_AUFW.DB.IWorkspaceTcode;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IWorkspaceTcode
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IWorkspaceTcode/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IWorkspaceTcode : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode.NewWorkspaceTcode to insert a row in table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="SAPRoleName"></param>
		/// <param name="StandardActivity"></param>
		/// <param name="RoleType"></param>
		/// <param name="System"></param>
		/// <param name="Platform"></param>
		/// <param name="TcodeName"></param>
		/// <param name="TcodeValue"></param>
		/// <param name="EditingWorkspaceID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewWorkspaceTcode(string SAPRoleName, string StandardActivity, string RoleType, string System, string Platform, string TcodeName, string TcodeValue, int EditingWorkspaceID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode obj = new RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode(dbconn);
			return obj.NewWorkspaceTcode(SAPRoleName, StandardActivity, RoleType, System, Platform, TcodeName, TcodeValue, EditingWorkspaceID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode.DeleteWorkspaceTcode to delete a row from table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteWorkspaceTcode(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode obj = new RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode(dbconn);
			return obj.DeleteWorkspaceTcode(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode.GetWorkspaceTcode to select a row from table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetWorkspaceTcode</returns>
		[WebMethod]
		public returnGetWorkspaceTcode GetWorkspaceTcode(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode obj = new RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode(dbconn);
			return obj.GetWorkspaceTcode(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode.SetWorkspaceTcode to update a row in table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="SAPRoleName"></param>
		/// <param name="StandardActivity"></param>
		/// <param name="RoleType"></param>
		/// <param name="System"></param>
		/// <param name="Platform"></param>
		/// <param name="TcodeName"></param>
		/// <param name="TcodeValue"></param>
		/// <param name="AuthObjName"></param>
		/// <param name="AuthObjValue"></param>
		/// <param name="FieldSecName"></param>
		/// <param name="FieldSecValue"></param>
		/// <param name="Commentary"></param>
		/// <param name="EditingWorkspaceID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetWorkspaceTcode(int ID, string SAPRoleName, string StandardActivity, string RoleType, string System, string Platform, string TcodeName, string TcodeValue, string AuthObjName, string AuthObjValue, string FieldSecName, string FieldSecValue, string Commentary, int EditingWorkspaceID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode obj = new RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode(dbconn);
			return obj.SetWorkspaceTcode(ID, SAPRoleName, StandardActivity, RoleType, System, Platform, TcodeName, TcodeValue, AuthObjName, AuthObjValue, FieldSecName, FieldSecValue, Commentary, EditingWorkspaceID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode.ListWorkspaceTcode to select a set of rows from table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListWorkspaceTcode[]</returns>
		[WebMethod]
		public returnListWorkspaceTcode[] ListWorkspaceTcode(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode obj = new RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode(dbconn);
			return obj.ListWorkspaceTcode(maxRowsToReturn);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode.ListWorkspaceTcodeByEditingWorkspace to select a set of rows from table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="EditingWorkspaceID"></param>
		/// <returns>returnListWorkspaceTcodeByEditingWorkspace[]</returns>
		[WebMethod]
		public returnListWorkspaceTcodeByEditingWorkspace[] ListWorkspaceTcodeByEditingWorkspace(int? maxRowsToReturn, int EditingWorkspaceID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode obj = new RBSR_AUFW.DB.IWorkspaceTcode.IWorkspaceTcode(dbconn);
			return obj.ListWorkspaceTcodeByEditingWorkspace(maxRowsToReturn, EditingWorkspaceID);
		}
	}
}
