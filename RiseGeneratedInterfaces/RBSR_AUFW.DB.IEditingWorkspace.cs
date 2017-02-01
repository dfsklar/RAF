using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;
using _6MAR_WebApplication;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IEditingWorkspace
{
	/// <summary>
	/// Return value from method GetEditingWorkspace
	/// </summary>
	public struct returnGetEditingWorkspace
	{
		public int ID;
		public string Commentary;
		public DateTime TimeOfBirth;
		public bool HasUnsavedChanges;
		public int SubProcessID;
		public int UserID;
		public int EntAssignmentSetID;
	}
	/// <summary>
	/// Return value from method ListEditingWorkspace
	/// </summary>
	public struct returnListEditingWorkspace
	{
		public int ID;
		public string Commentary;
		public DateTime TimeOfBirth;
		public bool HasUnsavedChanges;
		public int SubProcessID;
		public int UserID;
		public int EntAssignmentSetID;
	}
	/// <summary>
	/// Return value from method ListEditingWorkspaceBySubProcess
	/// </summary>
	public struct returnListEditingWorkspaceBySubProcess
	{
		public int ID;
		public string Commentary;
		public DateTime TimeOfBirth;
		public bool HasUnsavedChanges;
		public int SubProcessID;
		public int UserID;
		public int EntAssignmentSetID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_EditingWorkspace
	/// </summary>
	public class IEditingWorkspace : _6MAR_WebApplication.RISEBASE
	{
		public IEditingWorkspace() : this((OdbcConnection)null) { }
		public IEditingWorkspace(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IEditingWorkspace(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="Commentary"></param>
		/// <param name="TimeOfBirth"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewEditingWorkspace(string Commentary, DateTime TimeOfBirth, int SubProcessID, int UserID, int EntAssignmentSetID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_EditingWorkspace\"(\"c_u_Commentary\",\"c_u_TimeOfBirth\",\"c_r_SubProcess\",\"c_r_User\",\"c_r_EntAssignmentSet\") VALUES(?,?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (Commentary == null)  { cmd.Dispose(); DBClose(); throw new Exception("Commentary must not be null!"); } 
			cmd.Parameters.Add("c_u_Commentary", OdbcType.NVarChar, 2048);
			cmd.Parameters["c_u_Commentary"].Value = (Commentary != null ? (object)Commentary : DBNull.Value);
			cmd.Parameters.Add("c_u_TimeOfBirth", OdbcType.DateTime);
			cmd.Parameters["c_u_TimeOfBirth"].Value = HELPERS.SetSafeDBDate(TimeOfBirth);
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			cmd.Parameters.Add("c_r_User", OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			cmd.Parameters.Add("c_r_EntAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_EntAssignmentSet"].Value = (object)EntAssignmentSetID;
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
		/// delete a row from table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteEditingWorkspace(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_EditingWorkspace\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEditingWorkspace</returns>
		public returnGetEditingWorkspace GetEditingWorkspace(int ID)
		{
			returnGetEditingWorkspace rv = new returnGetEditingWorkspace();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_Commentary\",\"c_u_TimeOfBirth\",\"c_u_HasUnsavedChanges\",\"c_r_SubProcess\",\"c_r_User\",\"c_r_EntAssignmentSet\" from \"t_RBSR_AUFW_u_EditingWorkspace\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Commentary'"); } 
				else
					rv.Commentary = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TimeOfBirth'"); } 
				else
					rv.TimeOfBirth = dr.GetDateTime(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'HasUnsavedChanges'"); } 
				else
					rv.HasUnsavedChanges = typeof(short).IsAssignableFrom(dr.GetFieldType(3))? (dr.GetInt16(3) != 0): dr.GetBoolean(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					rv.SubProcessID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				else
					rv.UserID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					rv.EntAssignmentSetID = dr.GetInt32(6);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Commentary"></param>
		/// <param name="TimeOfBirth"></param>
		/// <param name="HasUnsavedChanges"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetEditingWorkspace(int ID, string Commentary, DateTime TimeOfBirth, bool HasUnsavedChanges, int SubProcessID, int UserID, int EntAssignmentSetID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_EditingWorkspace\" set \"c_u_Commentary\"=?,\"c_u_TimeOfBirth\"=?,\"c_u_HasUnsavedChanges\"=?,\"c_r_SubProcess\"=?,\"c_r_User\"=?,\"c_r_EntAssignmentSet\"=? where \"c_id\" = ?";
			if (Commentary == null)  { cmd.Dispose(); DBClose(); throw new Exception("Commentary must not be null!"); } 
			cmd.Parameters.Add("c_u_Commentary", OdbcType.NVarChar, 2048);
			cmd.Parameters["c_u_Commentary"].Value = (Commentary != null ? (object)Commentary : DBNull.Value);
			cmd.Parameters.Add("c_u_TimeOfBirth", OdbcType.DateTime);
            cmd.Parameters["c_u_TimeOfBirth"].Value = HELPERS.SetSafeDBDate(TimeOfBirth);
			cmd.Parameters.Add("c_u_HasUnsavedChanges", OdbcType.Bit);
			cmd.Parameters["c_u_HasUnsavedChanges"].Value = (object)HasUnsavedChanges;
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			cmd.Parameters.Add("c_r_User", OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			cmd.Parameters.Add("c_r_EntAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_EntAssignmentSet"].Value = (object)EntAssignmentSetID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListEditingWorkspace[]</returns>
		public returnListEditingWorkspace[] ListEditingWorkspace(int? maxRowsToReturn)
		{
			returnListEditingWorkspace[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Commentary\", \"c_u_TimeOfBirth\", \"c_u_HasUnsavedChanges\", \"c_r_SubProcess\", \"c_r_User\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EditingWorkspace\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Commentary\", \"c_u_TimeOfBirth\", \"c_u_HasUnsavedChanges\", \"c_r_SubProcess\", \"c_r_User\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EditingWorkspace\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Commentary\", \"c_u_TimeOfBirth\", \"c_u_HasUnsavedChanges\", \"c_r_SubProcess\", \"c_r_User\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EditingWorkspace\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEditingWorkspace> rvl = new List<returnListEditingWorkspace>();
			while (dr.Read())
			{
				returnListEditingWorkspace cr = new returnListEditingWorkspace();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Commentary'"); } 
				else
					cr.Commentary = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TimeOfBirth'"); } 
				else
					cr.TimeOfBirth = dr.GetDateTime(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'HasUnsavedChanges'"); } 
				else
					cr.HasUnsavedChanges = typeof(short).IsAssignableFrom(dr.GetFieldType(3))? (dr.GetInt16(3) != 0): dr.GetBoolean(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				else
					cr.UserID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					cr.EntAssignmentSetID = dr.GetInt32(6);
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
		/// select a set of rows from table t_RBSR_AUFW_u_EditingWorkspace.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SubProcessID"></param>
		/// <returns>returnListEditingWorkspaceBySubProcess[]</returns>
		public returnListEditingWorkspaceBySubProcess[] ListEditingWorkspaceBySubProcess(int? maxRowsToReturn, int SubProcessID)
		{
			returnListEditingWorkspaceBySubProcess[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Commentary\", \"c_u_TimeOfBirth\", \"c_u_HasUnsavedChanges\", \"c_r_SubProcess\", \"c_r_User\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EditingWorkspace\" WHERE \"c_r_SubProcess\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Commentary\", \"c_u_TimeOfBirth\", \"c_u_HasUnsavedChanges\", \"c_r_SubProcess\", \"c_r_User\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EditingWorkspace\" WHERE \"c_r_SubProcess\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Commentary\", \"c_u_TimeOfBirth\", \"c_u_HasUnsavedChanges\", \"c_r_SubProcess\", \"c_r_User\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EditingWorkspace\" WHERE \"c_r_SubProcess\"=?";
			cmd.Parameters.Add("1_SubProcessID", OdbcType.Int);
			cmd.Parameters["1_SubProcessID"].Value = (object)SubProcessID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEditingWorkspaceBySubProcess> rvl = new List<returnListEditingWorkspaceBySubProcess>();
			while (dr.Read())
			{
				returnListEditingWorkspaceBySubProcess cr = new returnListEditingWorkspaceBySubProcess();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Commentary'"); } 
				else
					cr.Commentary = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TimeOfBirth'"); } 
				else
					cr.TimeOfBirth = dr.GetDateTime(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'HasUnsavedChanges'"); } 
				else
					cr.HasUnsavedChanges = typeof(short).IsAssignableFrom(dr.GetFieldType(3))? (dr.GetInt16(3) != 0): dr.GetBoolean(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				else
					cr.UserID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					cr.EntAssignmentSetID = dr.GetInt32(6);
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
