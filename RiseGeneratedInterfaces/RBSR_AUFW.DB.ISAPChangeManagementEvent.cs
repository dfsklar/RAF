﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.286 (#327)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.ISAPChangeManagementEvent
{
	/// <summary>
	/// Return value from method GetSAPChangeManagementEvent
	/// </summary>
	public struct returnGetSAPChangeManagementEvent
	{
		public int ID;
		public string EventType;
		public string Who;
		public DateTime? TimeStamp;
		public string Commentary;
		public int TcodeAssignmentSetID;
	}
	/// <summary>
	/// Return value from method ListSAPChangeManagementEvent
	/// </summary>
	public struct returnListSAPChangeManagementEvent
	{
		public int ID;
		public string EventType;
		public string Who;
		public DateTime? TimeStamp;
		public string Commentary;
		public int TcodeAssignmentSetID;
	}
	/// <summary>
	/// Return value from method ListSAPChangeManagementEventByTcodeAssignmentSet
	/// </summary>
	public struct returnListSAPChangeManagementEventByTcodeAssignmentSet
	{
		public int ID;
		public string EventType;
		public string Who;
		public DateTime? TimeStamp;
		public string Commentary;
		public int TcodeAssignmentSetID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_SAPChangeManagementEvent
	/// </summary>
	public class ISAPChangeManagementEvent : _6MAR_WebApplication.RISEBASE
	{
		public ISAPChangeManagementEvent() : this((OdbcConnection)null) { }
		public ISAPChangeManagementEvent(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ISAPChangeManagementEvent(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_SAPChangeManagementEvent.
		/// </summary>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <param name="EventType"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewSAPChangeManagementEvent(int TcodeAssignmentSetID, string EventType)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_SAPChangeManagementEvent\"(\"c_r_TcodeAssignmentSet\",\"c_u_EventType\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_r_TcodeAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_TcodeAssignmentSet"].Value = (object)TcodeAssignmentSetID;
			if (EventType == null)  { cmd.Dispose(); DBClose(); throw new Exception("EventType must not be null!"); } 
			cmd.Parameters.Add("c_u_EventType", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_EventType"].Value = (EventType != null ? (object)EventType : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_SAPChangeManagementEvent.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteSAPChangeManagementEvent(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_SAPChangeManagementEvent\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_SAPChangeManagementEvent.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPChangeManagementEvent</returns>
		public returnGetSAPChangeManagementEvent GetSAPChangeManagementEvent(int ID)
		{
			returnGetSAPChangeManagementEvent rv = new returnGetSAPChangeManagementEvent();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_EventType\",\"c_u_Who\",\"c_u_TimeStamp\",\"c_u_Commentary\",\"c_r_TcodeAssignmentSet\" from \"t_RBSR_AUFW_u_SAPChangeManagementEvent\" where \"c_id\"= ?";
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
					rv.Commentary = null;
				else
					rv.Commentary = dr.GetString(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
				else
					rv.TcodeAssignmentSetID = dr.GetInt32(5);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}





	  // DFSKLAR CHANGED THIS:
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_SAPChangeManagementEvent.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EventType"></param>
		/// <param name="Who"></param>
		/// <param name="TimeStamp"></param>
		/// <param name="Commentary"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetSAPChangeManagementEvent(int ID, string Who, DateTime? TimeStamp, string Commentary)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_SAPChangeManagementEvent\" set \"c_u_Who\"=?,\"c_u_TimeStamp\"=?,\"c_u_Commentary\"=?  where \"c_id\" = ?";
			cmd.Parameters.Add("c_u_Who", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Who"].Value = (Who != null ? (object)Who : DBNull.Value);
			cmd.Parameters.Add("c_u_TimeStamp", OdbcType.DateTime);
			cmd.Parameters["c_u_TimeStamp"].Value = (TimeStamp != null ? (object)TimeStamp : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPChangeManagementEvent.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListSAPChangeManagementEvent[]</returns>
		public returnListSAPChangeManagementEvent[] ListSAPChangeManagementEvent(int? maxRowsToReturn)
		{
			returnListSAPChangeManagementEvent[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_u_Commentary\", \"c_r_TcodeAssignmentSet\" FROM \"t_RBSR_AUFW_u_SAPChangeManagementEvent\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_u_Commentary\", \"c_r_TcodeAssignmentSet\" FROM \"t_RBSR_AUFW_u_SAPChangeManagementEvent\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_u_Commentary\", \"c_r_TcodeAssignmentSet\" FROM \"t_RBSR_AUFW_u_SAPChangeManagementEvent\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPChangeManagementEvent> rvl = new List<returnListSAPChangeManagementEvent>();
			while (dr.Read())
			{
				returnListSAPChangeManagementEvent cr = new returnListSAPChangeManagementEvent();
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
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(5);
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPChangeManagementEvent.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>returnListSAPChangeManagementEventByTcodeAssignmentSet[]</returns>
		public returnListSAPChangeManagementEventByTcodeAssignmentSet[] ListSAPChangeManagementEventByTcodeAssignmentSet(int? maxRowsToReturn, int TcodeAssignmentSetID)
		{
			returnListSAPChangeManagementEventByTcodeAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_u_Commentary\", \"c_r_TcodeAssignmentSet\" FROM \"t_RBSR_AUFW_u_SAPChangeManagementEvent\" WHERE \"c_r_TcodeAssignmentSet\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_u_Commentary\", \"c_r_TcodeAssignmentSet\" FROM \"t_RBSR_AUFW_u_SAPChangeManagementEvent\" WHERE \"c_r_TcodeAssignmentSet\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_EventType\", \"c_u_Who\", \"c_u_TimeStamp\", \"c_u_Commentary\", \"c_r_TcodeAssignmentSet\" FROM \"t_RBSR_AUFW_u_SAPChangeManagementEvent\" WHERE \"c_r_TcodeAssignmentSet\"=?";
			cmd.Parameters.Add("1_TcodeAssignmentSetID", OdbcType.Int);
			cmd.Parameters["1_TcodeAssignmentSetID"].Value = (object)TcodeAssignmentSetID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPChangeManagementEventByTcodeAssignmentSet> rvl = new List<returnListSAPChangeManagementEventByTcodeAssignmentSet>();
			while (dr.Read())
			{
				returnListSAPChangeManagementEventByTcodeAssignmentSet cr = new returnListSAPChangeManagementEventByTcodeAssignmentSet();
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
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(5);
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
