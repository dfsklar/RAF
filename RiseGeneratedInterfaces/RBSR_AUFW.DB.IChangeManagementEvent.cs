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
// Version 1.235 (#276)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IChangeManagementEvent
{
	/// <summary>
	/// Return value from method GetChangeManagementEvent
	/// </summary>
	public struct returnGetChangeManagementEvent
	{
		public int ID;
		public string EventType;
		public string Who;
		public DateTime? TimeStamp;
		public int EntAssignmentSetID;
		public string Commentary;
	}
	/// <summary>
	/// Return value from method ListChangeManagementEvent
	/// </summary>
	public struct returnListChangeManagementEvent
	{
		public int ID;
		public string EventType;
		public string Who;
		public DateTime? TimeStamp;
		public int EntAssignmentSetID;
		public string Commentary;
	}
	/// <summary>
	/// Return value from method ListChangeManagementEventByEntAssignmentSet
	/// </summary>
	public struct returnListChangeManagementEventByEntAssignmentSet
	{
		public int ID;
		public string EventType;
		public string Who;
		public DateTime? TimeStamp;
		public int EntAssignmentSetID;
		public string Commentary;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_ChangeManagementEvent
	/// </summary>
	public class IChangeManagementEvent
	{
		private string _tempDir = ".";
		private bool _odbcCloseAfterUse;
		private OdbcConnection _dbConnection = null;
		public string TempDir
		{
			get { return _tempDir; }
			set { _tempDir = value; }
		}
		public OdbcConnection DbConnection
		{
			get { return _dbConnection; }
			set { _dbConnection = value; }
		}
		public IChangeManagementEvent() : this((OdbcConnection)null) { }
		public IChangeManagementEvent(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IChangeManagementEvent(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}
		protected void DBConnect()
		{
			if (_odbcCloseAfterUse = (_dbConnection.State != ConnectionState.Open))
			{
				_dbConnection.Open();
				if (_dbConnection.Driver.ToLower().StartsWith("myodbc"))
				{
					OdbcCommand cmd = _dbConnection.CreateCommand();
					cmd.CommandText = "SET sql_mode = 'ANSI'";
					cmd.ExecuteNonQuery();
				}
			}
		}
		protected void DBClose() { if (_odbcCloseAfterUse) _dbConnection.Close(); }
		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_ChangeManagementEvent.
		/// </summary>
		/// <param name="EventType"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewChangeManagementEvent(string EventType, int EntAssignmentSetID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_ChangeManagementEvent\"(\"c_u_EventType\",\"c_r_EntAssignmentSet\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (EventType == null)  { cmd.Dispose(); DBClose(); throw new Exception("EventType must not be null!"); } 
			cmd.Parameters.Add("c_u_EventType", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_EventType"].Value = (EventType != null ? (object)EventType : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_ChangeManagementEvent.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteChangeManagementEvent(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_ChangeManagementEvent\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_ChangeManagementEvent.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetChangeManagementEvent</returns>
		public returnGetChangeManagementEvent GetChangeManagementEvent(int ID)
		{
			returnGetChangeManagementEvent rv = new returnGetChangeManagementEvent();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_EventType\",\"c_u_Who\",\"c_u_TimeStamp\",\"c_r_EntAssignmentSet\",\"c_u_Commentary\" from \"t_RBSR_AUFW_u_ChangeManagementEvent\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EventType'"); } 
				else
					rv.EventType = dr.GetString(1);
				if (dr.IsDBNull(2))
					rv.Who = null;
				else
					rv.Who = dr.GetString(2);
				if (dr.IsDBNull(3))
					rv.TimeStamp = null;
				else
					rv.TimeStamp = dr.GetDateTime(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					rv.EntAssignmentSetID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					rv.Commentary = null;
				else
					rv.Commentary = dr.GetString(5);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_ChangeManagementEvent.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EventType"></param>
		/// <param name="Who"></param>
		/// <param name="TimeStamp"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <param name="Commentary"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetChangeManagementEvent(int ID, string EventType, string Who, DateTime? TimeStamp, int EntAssignmentSetID, string Commentary)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_ChangeManagementEvent\" set \"c_u_EventType\"=?,\"c_u_Who\"=?,\"c_u_TimeStamp\"=?,\"c_r_EntAssignmentSet\"=?,\"c_u_Commentary\"=? where \"c_id\" = ?";
			if (EventType == null)  { cmd.Dispose(); DBClose(); throw new Exception("EventType must not be null!"); } 
			cmd.Parameters.Add("c_u_EventType", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_EventType"].Value = (EventType != null ? (object)EventType : DBNull.Value);
			cmd.Parameters.Add("c_u_Who", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Who"].Value = (Who != null ? (object)Who : DBNull.Value);
			cmd.Parameters.Add("c_u_TimeStamp", OdbcType.DateTime);
			cmd.Parameters["c_u_TimeStamp"].Value = (TimeStamp.HasValue ? HELPERS.SetSafeDBDate(TimeStamp.Value) : DBNull.Value);
			cmd.Parameters.Add("c_r_EntAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_EntAssignmentSet"].Value = (object)EntAssignmentSetID;
			cmd.Parameters.Add("c_u_Commentary", OdbcType.NText);
			cmd.Parameters["c_u_Commentary"].Value = (Commentary != null ? (object)Commentary : DBNull.Value);
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();
			if (rv != 1)  { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}



        public int SetChangeManagementEvent(int ID, string Who, DateTime? TimeStamp, string Commentary)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "update \"t_RBSR_AUFW_u_ChangeManagementEvent\" set \"c_u_Who\"=?,\"c_u_TimeStamp\"=?,\"c_u_Commentary\"=? where \"c_id\" = ?";
            cmd.Parameters.Add("c_u_Who", OdbcType.NVarChar, 50);
            cmd.Parameters["c_u_Who"].Value = (Who != null ? (object)Who : DBNull.Value);
            cmd.Parameters.Add("c_u_TimeStamp", OdbcType.DateTime);
            cmd.Parameters["c_u_TimeStamp"].Value = (TimeStamp.HasValue ? HELPERS.SetSafeDBDate(TimeStamp.Value) : DBNull.Value);
            cmd.Parameters.Add("c_u_Commentary", OdbcType.NText);
            cmd.Parameters["c_u_Commentary"].Value = (Commentary != null ? (object)Commentary : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_ChangeManagementEvent.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListChangeManagementEvent[]</returns>
		public returnListChangeManagementEvent[] ListChangeManagementEvent(int? maxRowsToReturn)
		{
			returnListChangeManagementEvent[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_r_EntAssignmentSet\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_ChangeManagementEvent\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_r_EntAssignmentSet\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_ChangeManagementEvent\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_r_EntAssignmentSet\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_ChangeManagementEvent\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListChangeManagementEvent> rvl = new List<returnListChangeManagementEvent>();
			while (dr.Read())
			{
				returnListChangeManagementEvent cr = new returnListChangeManagementEvent();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EventType'"); } 
				else
					cr.EventType = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.Who = null;
				else
					cr.Who = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.TimeStamp = null;
				else
					cr.TimeStamp = dr.GetDateTime(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					cr.EntAssignmentSetID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(5);
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
		/// select a set of rows from table t_RBSR_AUFW_u_ChangeManagementEvent.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>returnListChangeManagementEventByEntAssignmentSet[]</returns>
		public returnListChangeManagementEventByEntAssignmentSet[] ListChangeManagementEventByEntAssignmentSet(int? maxRowsToReturn, int EntAssignmentSetID)
		{
			returnListChangeManagementEventByEntAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_r_EntAssignmentSet\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_ChangeManagementEvent\" WHERE \"c_r_EntAssignmentSet\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_r_EntAssignmentSet\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_ChangeManagementEvent\" WHERE \"c_r_EntAssignmentSet\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_r_EntAssignmentSet\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_ChangeManagementEvent\" WHERE \"c_r_EntAssignmentSet\"=?";
			cmd.Parameters.Add("1_EntAssignmentSetID", OdbcType.Int);
			cmd.Parameters["1_EntAssignmentSetID"].Value = (object)EntAssignmentSetID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListChangeManagementEventByEntAssignmentSet> rvl = new List<returnListChangeManagementEventByEntAssignmentSet>();
			while (dr.Read())
			{
				returnListChangeManagementEventByEntAssignmentSet cr = new returnListChangeManagementEventByEntAssignmentSet();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EventType'"); } 
				else
					cr.EventType = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.Who = null;
				else
					cr.Who = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.TimeStamp = null;
				else
					cr.TimeStamp = dr.GetDateTime(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					cr.EntAssignmentSetID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(5);
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
