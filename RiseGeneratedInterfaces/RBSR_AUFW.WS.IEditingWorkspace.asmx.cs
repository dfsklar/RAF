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
using RBSR_AUFW.DB.IEditingWorkspace;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IEditingWorkspace
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IEditingWorkspace/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IEditingWorkspace : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace.NewEditingWorkspace to insert a row in table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="Commentary"></param>
		/// <param name="TimeOfBirth"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewEditingWorkspace(string Commentary, DateTime TimeOfBirth, int SubProcessID, int UserID, int EntAssignmentSetID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace obj = new RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace(dbconn);
			return obj.NewEditingWorkspace(Commentary, TimeOfBirth, SubProcessID, UserID, EntAssignmentSetID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace.DeleteEditingWorkspace to delete a row from table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteEditingWorkspace(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace obj = new RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace(dbconn);
			return obj.DeleteEditingWorkspace(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace.GetEditingWorkspace to select a row from table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEditingWorkspace</returns>
		[WebMethod]
		public returnGetEditingWorkspace GetEditingWorkspace(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace obj = new RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace(dbconn);
			return obj.GetEditingWorkspace(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace.SetEditingWorkspace to update a row in table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Commentary"></param>
		/// <param name="TimeOfBirth"></param>
		/// <param name="HasUnsavedChanges"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetEditingWorkspace(int ID, string Commentary, DateTime TimeOfBirth, bool HasUnsavedChanges, int SubProcessID, int UserID, int EntAssignmentSetID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace obj = new RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace(dbconn);
			return obj.SetEditingWorkspace(ID, Commentary, TimeOfBirth, HasUnsavedChanges, SubProcessID, UserID, EntAssignmentSetID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace.ListEditingWorkspace to select a set of rows from table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListEditingWorkspace[]</returns>
		[WebMethod]
		public returnListEditingWorkspace[] ListEditingWorkspace(int? maxRowsToReturn)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace obj = new RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace(dbconn);
			return obj.ListEditingWorkspace(maxRowsToReturn);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace.ListEditingWorkspaceBySubProcess to select a set of rows from table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SubProcessID"></param>
		/// <returns>returnListEditingWorkspaceBySubProcess[]</returns>
		[WebMethod]
		public returnListEditingWorkspaceBySubProcess[] ListEditingWorkspaceBySubProcess(int? maxRowsToReturn, int SubProcessID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace obj = new RBSR_AUFW.DB.IEditingWorkspace.IEditingWorkspace(dbconn);
			return obj.ListEditingWorkspaceBySubProcess(maxRowsToReturn, SubProcessID);
		}
	}
}
