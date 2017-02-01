using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IWorkspaceTcode
{
	/// <summary>
	/// Return value from method GetWorkspaceTcode
	/// </summary>
	public struct returnGetWorkspaceTcode
	{
		public int ID;
		public string SAPRoleName;
		public string StandardActivity;
		public string RoleType;
		public string System;
		public string Platform;
		public string TcodeName;
		public string TcodeValue;
		public string AuthObjName;
		public string AuthObjValue;
		public string FieldSecName;
		public string FieldSecValue;
		public string Commentary;
		public int EditingWorkspaceID;
	}
	/// <summary>
	/// Return value from method ListWorkspaceTcode
	/// </summary>
	public struct returnListWorkspaceTcode
	{
		public int ID;
		public string SAPRoleName;
		public string StandardActivity;
		public string RoleType;
		public string System;
		public string Platform;
		public string TcodeName;
		public string TcodeValue;
		public string AuthObjName;
		public string AuthObjValue;
		public string FieldSecName;
		public string FieldSecValue;
		public string Commentary;
		public int EditingWorkspaceID;
	}
	/// <summary>
	/// Return value from method ListWorkspaceTcodeByEditingWorkspace
	/// </summary>
	public struct returnListWorkspaceTcodeByEditingWorkspace
	{
		public int ID;
		public string SAPRoleName;
		public string StandardActivity;
		public string RoleType;
		public string System;
		public string Platform;
		public string TcodeName;
		public string TcodeValue;
		public string AuthObjName;
		public string AuthObjValue;
		public string FieldSecName;
		public string FieldSecValue;
		public string Commentary;
		public int EditingWorkspaceID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_WorkspaceTcode
	/// </summary>
	public class IWorkspaceTcode : _6MAR_WebApplication.RISEBASE
	{
		public IWorkspaceTcode() : this((OdbcConnection)null) { }
		public IWorkspaceTcode(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IWorkspaceTcode(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_WorkspaceTcode.
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
		public int NewWorkspaceTcode(string SAPRoleName, string StandardActivity, string RoleType, string System, string Platform, string TcodeName, string TcodeValue, int EditingWorkspaceID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_WorkspaceTcode\"(\"c_u_SAPRoleName\",\"c_u_StandardActivity\",\"c_u_RoleType\",\"c_u_System\",\"c_u_Platform\",\"c_u_TcodeName\",\"c_u_TcodeValue\",\"c_r_EditingWorkspace\") VALUES(?,?,?,?,?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (SAPRoleName == null)  { cmd.Dispose(); DBClose(); throw new Exception("SAPRoleName must not be null!"); } 
			cmd.Parameters.Add("c_u_SAPRoleName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_SAPRoleName"].Value = (SAPRoleName != null ? (object)SAPRoleName : DBNull.Value);
			if (StandardActivity == null)  { cmd.Dispose(); DBClose(); throw new Exception("StandardActivity must not be null!"); } 
			cmd.Parameters.Add("c_u_StandardActivity", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_StandardActivity"].Value = (StandardActivity != null ? (object)StandardActivity : DBNull.Value);
			if (RoleType == null)  { cmd.Dispose(); DBClose(); throw new Exception("RoleType must not be null!"); } 
			cmd.Parameters.Add("c_u_RoleType", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_RoleType"].Value = (RoleType != null ? (object)RoleType : DBNull.Value);
			if (System == null)  { cmd.Dispose(); DBClose(); throw new Exception("System must not be null!"); } 
			cmd.Parameters.Add("c_u_System", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_System"].Value = (System != null ? (object)System : DBNull.Value);
			if (Platform == null)  { cmd.Dispose(); DBClose(); throw new Exception("Platform must not be null!"); } 
			cmd.Parameters.Add("c_u_Platform", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Platform"].Value = (Platform != null ? (object)Platform : DBNull.Value);
			if (TcodeName == null)  { cmd.Dispose(); DBClose(); throw new Exception("TcodeName must not be null!"); } 
			cmd.Parameters.Add("c_u_TcodeName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_TcodeName"].Value = (TcodeName != null ? (object)TcodeName : DBNull.Value);
			if (TcodeValue == null)  { cmd.Dispose(); DBClose(); throw new Exception("TcodeValue must not be null!"); } 
			cmd.Parameters.Add("c_u_TcodeValue", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_TcodeValue"].Value = (TcodeValue != null ? (object)TcodeValue : DBNull.Value);
			cmd.Parameters.Add("c_r_EditingWorkspace", OdbcType.Int);
			cmd.Parameters["c_r_EditingWorkspace"].Value = (object)EditingWorkspaceID;
OdbcDataReader dri=null; try{dri=cmd.ExecuteReader();}catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
			if (_dbConnection.Driver.ToLower().StartsWith("myodbc"))
			{
				cmd = _dbConnection.CreateCommand();
				cmd.CommandText = "SELECT LAST_INSERT_ID()";
				dri = cmd.ExecuteReader();
			}
try{dri.Read();} catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
			rv = (dri.IsDBNull(0) ? 0 : (typeof(long).Equals(dri.GetFieldType(0)) ? (int)dri.GetInt64(0) : dri.GetInt32(0)));
			dri.Close();
			if (rv == 0)  { cmd.Dispose(); DBClose(); throw new Exception("Insert operation failed!"); } 
			dri.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// delete a row from table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteWorkspaceTcode(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_WorkspaceTcode\" where \"c_id\" = ?";
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();
			if (rv != 1)  { cmd.Dispose(); DBClose(); throw new Exception("Delete resulted in " + rv.ToString() + " objects being deleted!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// select a row from table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetWorkspaceTcode</returns>
		public returnGetWorkspaceTcode GetWorkspaceTcode(int ID)
		{
			returnGetWorkspaceTcode rv = new returnGetWorkspaceTcode();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_SAPRoleName\",\"c_u_StandardActivity\",\"c_u_RoleType\",\"c_u_System\",\"c_u_Platform\",\"c_u_TcodeName\",\"c_u_TcodeValue\",\"c_u_AuthObjName\",\"c_u_AuthObjValue\",\"c_u_FieldSecName\",\"c_u_FieldSecValue\",\"c_u_Commentary\",\"c_r_EditingWorkspace\" from \"t_RBSR_AUFW_u_WorkspaceTcode\" where \"c_id\"= ?";
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					rv.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPRoleName'"); } 
				else
					rv.SAPRoleName = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'StandardActivity'"); } 
				else
					rv.StandardActivity = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'RoleType'"); } 
				else
					rv.RoleType = dr.GetString(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'System'"); } 
				else
					rv.System = dr.GetString(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Platform'"); } 
				else
					rv.Platform = dr.GetString(5);
				if (dr.IsDBNull(6))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeName'"); } 
				else
					rv.TcodeName = dr.GetString(6);
				if (dr.IsDBNull(7))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeValue'"); } 
				else
					rv.TcodeValue = dr.GetString(7);
				if (dr.IsDBNull(8))
					rv.AuthObjName = null;
				else
					rv.AuthObjName = dr.GetString(8);
				if (dr.IsDBNull(9))
					rv.AuthObjValue = null;
				else
					rv.AuthObjValue = dr.GetString(9);
				if (dr.IsDBNull(10))
					rv.FieldSecName = null;
				else
					rv.FieldSecName = dr.GetString(10);
				if (dr.IsDBNull(11))
					rv.FieldSecValue = null;
				else
					rv.FieldSecValue = dr.GetString(11);
				if (dr.IsDBNull(12))
					rv.Commentary = null;
				else
					rv.Commentary = dr.GetString(12);
				if (dr.IsDBNull(13))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditingWorkspaceID'"); } 
				else
					rv.EditingWorkspaceID = dr.GetInt32(13);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_WorkspaceTcode.
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
		public int SetWorkspaceTcode(int ID, string SAPRoleName, string StandardActivity, string RoleType, string System, string Platform, string TcodeName, string TcodeValue, string AuthObjName, string AuthObjValue, string FieldSecName, string FieldSecValue, string Commentary, int EditingWorkspaceID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_WorkspaceTcode\" set \"c_u_SAPRoleName\"=?,\"c_u_StandardActivity\"=?,\"c_u_RoleType\"=?,\"c_u_System\"=?,\"c_u_Platform\"=?,\"c_u_TcodeName\"=?,\"c_u_TcodeValue\"=?,\"c_u_AuthObjName\"=?,\"c_u_AuthObjValue\"=?,\"c_u_FieldSecName\"=?,\"c_u_FieldSecValue\"=?,\"c_u_Commentary\"=?,\"c_r_EditingWorkspace\"=? where \"c_id\" = ?";
			if (SAPRoleName == null)  { cmd.Dispose(); DBClose(); throw new Exception("SAPRoleName must not be null!"); } 
			cmd.Parameters.Add("c_u_SAPRoleName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_SAPRoleName"].Value = (SAPRoleName != null ? (object)SAPRoleName : DBNull.Value);
			if (StandardActivity == null)  { cmd.Dispose(); DBClose(); throw new Exception("StandardActivity must not be null!"); } 
			cmd.Parameters.Add("c_u_StandardActivity", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_StandardActivity"].Value = (StandardActivity != null ? (object)StandardActivity : DBNull.Value);
			if (RoleType == null)  { cmd.Dispose(); DBClose(); throw new Exception("RoleType must not be null!"); } 
			cmd.Parameters.Add("c_u_RoleType", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_RoleType"].Value = (RoleType != null ? (object)RoleType : DBNull.Value);
			if (System == null)  { cmd.Dispose(); DBClose(); throw new Exception("System must not be null!"); } 
			cmd.Parameters.Add("c_u_System", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_System"].Value = (System != null ? (object)System : DBNull.Value);
			if (Platform == null)  { cmd.Dispose(); DBClose(); throw new Exception("Platform must not be null!"); } 
			cmd.Parameters.Add("c_u_Platform", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Platform"].Value = (Platform != null ? (object)Platform : DBNull.Value);
			if (TcodeName == null)  { cmd.Dispose(); DBClose(); throw new Exception("TcodeName must not be null!"); } 
			cmd.Parameters.Add("c_u_TcodeName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_TcodeName"].Value = (TcodeName != null ? (object)TcodeName : DBNull.Value);
			if (TcodeValue == null)  { cmd.Dispose(); DBClose(); throw new Exception("TcodeValue must not be null!"); } 
			cmd.Parameters.Add("c_u_TcodeValue", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_TcodeValue"].Value = (TcodeValue != null ? (object)TcodeValue : DBNull.Value);
			cmd.Parameters.Add("c_u_AuthObjName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_AuthObjName"].Value = (AuthObjName != null ? (object)AuthObjName : DBNull.Value);
			cmd.Parameters.Add("c_u_AuthObjValue", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_AuthObjValue"].Value = (AuthObjValue != null ? (object)AuthObjValue : DBNull.Value);
			cmd.Parameters.Add("c_u_FieldSecName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_FieldSecName"].Value = (FieldSecName != null ? (object)FieldSecName : DBNull.Value);
			cmd.Parameters.Add("c_u_FieldSecValue", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_FieldSecValue"].Value = (FieldSecValue != null ? (object)FieldSecValue : DBNull.Value);
			cmd.Parameters.Add("c_u_Commentary", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_Commentary"].Value = (Commentary != null ? (object)Commentary : DBNull.Value);
			cmd.Parameters.Add("c_r_EditingWorkspace", OdbcType.Int);
			cmd.Parameters["c_r_EditingWorkspace"].Value = (object)EditingWorkspaceID;
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();
			if (rv != 1)  { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// select a set of rows from table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListWorkspaceTcode[]</returns>
		public returnListWorkspaceTcode[] ListWorkspaceTcode(int? maxRowsToReturn)
		{
			returnListWorkspaceTcode[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_SAPRoleName\", \"c_u_StandardActivity\", \"c_u_RoleType\", \"c_u_System\", \"c_u_Platform\", \"c_u_TcodeName\", \"c_u_TcodeValue\", \"c_u_AuthObjName\", \"c_u_AuthObjValue\", \"c_u_FieldSecName\", \"c_u_FieldSecValue\", \"c_u_Commentary\", \"c_r_EditingWorkspace\" FROM \"t_RBSR_AUFW_u_WorkspaceTcode\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_SAPRoleName\", \"c_u_StandardActivity\", \"c_u_RoleType\", \"c_u_System\", \"c_u_Platform\", \"c_u_TcodeName\", \"c_u_TcodeValue\", \"c_u_AuthObjName\", \"c_u_AuthObjValue\", \"c_u_FieldSecName\", \"c_u_FieldSecValue\", \"c_u_Commentary\", \"c_r_EditingWorkspace\" FROM \"t_RBSR_AUFW_u_WorkspaceTcode\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_SAPRoleName\", \"c_u_StandardActivity\", \"c_u_RoleType\", \"c_u_System\", \"c_u_Platform\", \"c_u_TcodeName\", \"c_u_TcodeValue\", \"c_u_AuthObjName\", \"c_u_AuthObjValue\", \"c_u_FieldSecName\", \"c_u_FieldSecValue\", \"c_u_Commentary\", \"c_r_EditingWorkspace\" FROM \"t_RBSR_AUFW_u_WorkspaceTcode\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListWorkspaceTcode> rvl = new List<returnListWorkspaceTcode>();
			while (dr.Read())
			{
				returnListWorkspaceTcode cr = new returnListWorkspaceTcode();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPRoleName'"); } 
				else
					cr.SAPRoleName = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'StandardActivity'"); } 
				else
					cr.StandardActivity = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'RoleType'"); } 
				else
					cr.RoleType = dr.GetString(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'System'"); } 
				else
					cr.System = dr.GetString(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Platform'"); } 
				else
					cr.Platform = dr.GetString(5);
				if (dr.IsDBNull(6))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeName'"); } 
				else
					cr.TcodeName = dr.GetString(6);
				if (dr.IsDBNull(7))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeValue'"); } 
				else
					cr.TcodeValue = dr.GetString(7);
				if (dr.IsDBNull(8))
					cr.AuthObjName = null;
				else
					cr.AuthObjName = dr.GetString(8);
				if (dr.IsDBNull(9))
					cr.AuthObjValue = null;
				else
					cr.AuthObjValue = dr.GetString(9);
				if (dr.IsDBNull(10))
					cr.FieldSecName = null;
				else
					cr.FieldSecName = dr.GetString(10);
				if (dr.IsDBNull(11))
					cr.FieldSecValue = null;
				else
					cr.FieldSecValue = dr.GetString(11);
				if (dr.IsDBNull(12))
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(12);
				if (dr.IsDBNull(13))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditingWorkspaceID'"); } 
				else
					cr.EditingWorkspaceID = dr.GetInt32(13);
				rvl.Add(cr);
			}
			dr.Close();
			dr.Dispose();
			rv = rvl.ToArray();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// select a set of rows from table t_RBSR_AUFW_u_WorkspaceTcode.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="EditingWorkspaceID"></param>
		/// <returns>returnListWorkspaceTcodeByEditingWorkspace[]</returns>
		public returnListWorkspaceTcodeByEditingWorkspace[] ListWorkspaceTcodeByEditingWorkspace(int? maxRowsToReturn, int EditingWorkspaceID)
		{
			returnListWorkspaceTcodeByEditingWorkspace[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_SAPRoleName\", \"c_u_StandardActivity\", \"c_u_RoleType\", \"c_u_System\", \"c_u_Platform\", \"c_u_TcodeName\", \"c_u_TcodeValue\", \"c_u_AuthObjName\", \"c_u_AuthObjValue\", \"c_u_FieldSecName\", \"c_u_FieldSecValue\", \"c_u_Commentary\", \"c_r_EditingWorkspace\" FROM \"t_RBSR_AUFW_u_WorkspaceTcode\" WHERE \"c_r_EditingWorkspace\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_SAPRoleName\", \"c_u_StandardActivity\", \"c_u_RoleType\", \"c_u_System\", \"c_u_Platform\", \"c_u_TcodeName\", \"c_u_TcodeValue\", \"c_u_AuthObjName\", \"c_u_AuthObjValue\", \"c_u_FieldSecName\", \"c_u_FieldSecValue\", \"c_u_Commentary\", \"c_r_EditingWorkspace\" FROM \"t_RBSR_AUFW_u_WorkspaceTcode\" WHERE \"c_r_EditingWorkspace\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_SAPRoleName\", \"c_u_StandardActivity\", \"c_u_RoleType\", \"c_u_System\", \"c_u_Platform\", \"c_u_TcodeName\", \"c_u_TcodeValue\", \"c_u_AuthObjName\", \"c_u_AuthObjValue\", \"c_u_FieldSecName\", \"c_u_FieldSecValue\", \"c_u_Commentary\", \"c_r_EditingWorkspace\" FROM \"t_RBSR_AUFW_u_WorkspaceTcode\" WHERE \"c_r_EditingWorkspace\"=?";
			cmd.Parameters.Add("1_EditingWorkspaceID", OdbcType.Int);
			cmd.Parameters["1_EditingWorkspaceID"].Value = (object)EditingWorkspaceID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListWorkspaceTcodeByEditingWorkspace> rvl = new List<returnListWorkspaceTcodeByEditingWorkspace>();
			while (dr.Read())
			{
				returnListWorkspaceTcodeByEditingWorkspace cr = new returnListWorkspaceTcodeByEditingWorkspace();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPRoleName'"); } 
				else
					cr.SAPRoleName = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'StandardActivity'"); } 
				else
					cr.StandardActivity = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'RoleType'"); } 
				else
					cr.RoleType = dr.GetString(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'System'"); } 
				else
					cr.System = dr.GetString(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Platform'"); } 
				else
					cr.Platform = dr.GetString(5);
				if (dr.IsDBNull(6))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeName'"); } 
				else
					cr.TcodeName = dr.GetString(6);
				if (dr.IsDBNull(7))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeValue'"); } 
				else
					cr.TcodeValue = dr.GetString(7);
				if (dr.IsDBNull(8))
					cr.AuthObjName = null;
				else
					cr.AuthObjName = dr.GetString(8);
				if (dr.IsDBNull(9))
					cr.AuthObjValue = null;
				else
					cr.AuthObjValue = dr.GetString(9);
				if (dr.IsDBNull(10))
					cr.FieldSecName = null;
				else
					cr.FieldSecName = dr.GetString(10);
				if (dr.IsDBNull(11))
					cr.FieldSecValue = null;
				else
					cr.FieldSecValue = dr.GetString(11);
				if (dr.IsDBNull(12))
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(12);
				if (dr.IsDBNull(13))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditingWorkspaceID'"); } 
				else
					cr.EditingWorkspaceID = dr.GetInt32(13);
				rvl.Add(cr);
			}
			dr.Close();
			dr.Dispose();
			rv = rvl.ToArray();
			cmd.Dispose();
			DBClose();
			return rv;
		}
	}
}
