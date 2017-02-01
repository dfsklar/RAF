using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.313 (#354)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IEventLog
{
	/// <summary>
	/// Return value from method GetEventLog
	/// </summary>
	public struct returnGetEventLog
	{
		public int ID;
		public DateTime TStamp;
		public int IDuser;
		public string IPaddr;
		public string Action;
		public int? IDobject;
		public string Detail1;
		public string Detail2;
		public string ObjType;
	}
	/// <summary>
	/// Return value from method ListEventLog
	/// </summary>
	public struct returnListEventLog
	{
		public int ID;
		public DateTime TStamp;
		public int IDuser;
		public string IPaddr;
		public string Action;
		public int? IDobject;
		public string Detail1;
		public string Detail2;
		public string ObjType;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_EventLog
	/// </summary>
	public class IEventLog : _6MAR_WebApplication.RISEBASE
	{
		public IEventLog() : this((OdbcConnection)null) { }
		public IEventLog(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IEventLog(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="TStamp"></param>
		/// <param name="IDuser"></param>
		/// <param name="IPaddr"></param>
		/// <param name="Action"></param>
		/// <param name="ObjType"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewEventLog(DateTime TStamp, int IDuser, string IPaddr, string Action, string ObjType)
		{

            // Work around bug in the new SQL Server 2008 ODBC driver
            TStamp = TStamp.AddMilliseconds(0 - TStamp.Millisecond);

			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_EventLog\"(\"c_u_TStamp\",\"c_u_IDuser\",\"c_u_IPaddr\",\"c_u_Action\",\"c_u_ObjType\") VALUES(?,?,?,?,?)";
            if (_dbConnection.Driver.ToLower().StartsWith("sql"))
                cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
            if (_dbConnection.Driver.ToLower().StartsWith("sqlncli"))
                cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
            cmd.Parameters.Add("c_u_TStamp", OdbcType.DateTime);
			cmd.Parameters["c_u_TStamp"].Value = (object)TStamp;
			cmd.Parameters.Add("c_u_IDuser", OdbcType.Int);
			cmd.Parameters["c_u_IDuser"].Value = (object)IDuser;
			if (IPaddr == null) throw new Exception("IPaddr must not be null!");
			cmd.Parameters.Add("c_u_IPaddr", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_IPaddr"].Value = (IPaddr != null ? (object)IPaddr : DBNull.Value);
			if (Action == null) throw new Exception("Action must not be null!");
			cmd.Parameters.Add("c_u_Action", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Action"].Value = (Action != null ? (object)Action : DBNull.Value);
			if (ObjType == null) throw new Exception("ObjType must not be null!");
			cmd.Parameters.Add("c_u_ObjType", OdbcType.NVarChar, 3);
			cmd.Parameters["c_u_ObjType"].Value = (ObjType != null ? (object)ObjType : DBNull.Value);
			OdbcDataReader dri = cmd.ExecuteReader();
			if (_dbConnection.Driver.ToLower().StartsWith("myodbc"))
			{
				cmd = _dbConnection.CreateCommand();
				cmd.CommandText = "SELECT LAST_INSERT_ID()";
				dri = cmd.ExecuteReader();
			}
			dri.Read();
			rv = (dri.IsDBNull(0) ? 0 : (typeof(long).Equals(dri.GetFieldType(0)) ? (int)dri.GetInt64(0) : dri.GetInt32(0)));
			dri.Close();
			if (rv == 0) throw new Exception("Insert operation failed!");
			dri.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}







		/// <summary>
		/// 
		/// delete a row from table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteEventLog(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_EventLog\" where \"c_id\" = ?";
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();
			if (rv != 1) throw new Exception("Delete resulted in " + rv.ToString() + " objects being deleted!");
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// select a row from table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEventLog</returns>
		public returnGetEventLog GetEventLog(int ID)
		{
			returnGetEventLog rv = new returnGetEventLog();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_TStamp\",\"c_u_IDuser\",\"c_u_IPaddr\",\"c_u_Action\",\"c_u_IDobject\",\"c_u_Detail1\",\"c_u_Detail2\",\"c_u_ObjType\" from \"t_RBSR_AUFW_u_EventLog\" where \"c_id\"= ?";
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					rv.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'TStamp'");
				else
					rv.TStamp = dr.GetDateTime(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'IDuser'");
				else
					rv.IDuser = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'IPaddr'");
				else
					rv.IPaddr = dr.GetString(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'Action'");
				else
					rv.Action = dr.GetString(4);
				if (dr.IsDBNull(5))
					rv.IDobject = null;
				else
					rv.IDobject = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					rv.Detail1 = null;
				else
					rv.Detail1 = dr.GetString(6);
				if (dr.IsDBNull(7))
					rv.Detail2 = null;
				else
					rv.Detail2 = dr.GetString(7);
				if (dr.IsDBNull(8))
					throw new Exception("Value 'null' is not allowed for 'ObjType'");
				else
					rv.ObjType = dr.GetString(8);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="TStamp"></param>
		/// <param name="IDuser"></param>
		/// <param name="IPaddr"></param>
		/// <param name="Action"></param>
		/// <param name="IDobject"></param>
		/// <param name="Detail1"></param>
		/// <param name="Detail2"></param>
		/// <param name="ObjType"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetEventLog(int ID, DateTime TStamp, int IDuser, string IPaddr, string Action, int? IDobject, string Detail1, string Detail2, string ObjType)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_EventLog\" set \"c_u_TStamp\"=?,\"c_u_IDuser\"=?,\"c_u_IPaddr\"=?,\"c_u_Action\"=?,\"c_u_IDobject\"=?,\"c_u_Detail1\"=?,\"c_u_Detail2\"=?,\"c_u_ObjType\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_u_TStamp", OdbcType.DateTime);
			cmd.Parameters["c_u_TStamp"].Value = (object)TStamp;
			cmd.Parameters.Add("c_u_IDuser", OdbcType.Int);
			cmd.Parameters["c_u_IDuser"].Value = (object)IDuser;
			if (IPaddr == null) throw new Exception("IPaddr must not be null!");
			cmd.Parameters.Add("c_u_IPaddr", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_IPaddr"].Value = (IPaddr != null ? (object)IPaddr : DBNull.Value);
			if (Action == null) throw new Exception("Action must not be null!");
			cmd.Parameters.Add("c_u_Action", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Action"].Value = (Action != null ? (object)Action : DBNull.Value);
			cmd.Parameters.Add("c_u_IDobject", OdbcType.Int);
			cmd.Parameters["c_u_IDobject"].Value = (IDobject != null ? (object)IDobject : DBNull.Value);
			cmd.Parameters.Add("c_u_Detail1", OdbcType.NVarChar, 4000);
			cmd.Parameters["c_u_Detail1"].Value = (Detail1 != null ? (object)Detail1 : DBNull.Value);
			cmd.Parameters.Add("c_u_Detail2", OdbcType.NVarChar, 4000);
			cmd.Parameters["c_u_Detail2"].Value = (Detail2 != null ? (object)Detail2 : DBNull.Value);
			if (ObjType == null) throw new Exception("ObjType must not be null!");
			cmd.Parameters.Add("c_u_ObjType", OdbcType.NVarChar, 3);
			cmd.Parameters["c_u_ObjType"].Value = (ObjType != null ? (object)ObjType : DBNull.Value);
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();
			if (rv != 1) throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!");
			cmd.Dispose();
			DBClose();
			return rv;
		}





		/// <summary>
		/// 
		/// select a set of rows from table t_RBSR_AUFW_u_EventLog.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListEventLog[]</returns>
		public returnListEventLog[] ListEventLog_ForSpecificTimestamp(DateTime tstamp, int? maxRowsToReturn)
		{
			returnListEventLog[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();

            // For comparing datetime values, I allow up to a full second's worth of mismatch.
            string extracond = " WHERE ( ABS(dbo.DateTimeToTicks(c_u_TStamp) - " + tstamp.Ticks.ToString() + ") < 10000000 ) ";


			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_TStamp\", \"c_u_IDuser\", \"c_u_IPaddr\", \"c_u_Action\", \"c_u_IDobject\", \"c_u_Detail1\", \"c_u_Detail2\", \"c_u_ObjType\" FROM \"t_RBSR_AUFW_u_EventLog\"" + extracond;
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_TStamp\", \"c_u_IDuser\", \"c_u_IPaddr\", \"c_u_Action\", \"c_u_IDobject\", \"c_u_Detail1\", \"c_u_Detail2\", \"c_u_ObjType\" FROM \"t_RBSR_AUFW_u_EventLog\"" + extracond + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_TStamp\", \"c_u_IDuser\", \"c_u_IPaddr\", \"c_u_Action\", \"c_u_IDobject\", \"c_u_Detail1\", \"c_u_Detail2\", \"c_u_ObjType\" FROM \"t_RBSR_AUFW_u_EventLog\"" + extracond;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEventLog> rvl = new List<returnListEventLog>();
			while (dr.Read())
			{
				returnListEventLog cr = new returnListEventLog();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'TStamp'");
				else
					cr.TStamp = dr.GetDateTime(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'IDuser'");
				else
					cr.IDuser = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'IPaddr'");
				else
					cr.IPaddr = dr.GetString(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'Action'");
				else
					cr.Action = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.IDobject = null;
				else
					cr.IDobject = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					cr.Detail1 = null;
				else
					cr.Detail1 = dr.GetString(6);
				if (dr.IsDBNull(7))
					cr.Detail2 = null;
				else
					cr.Detail2 = dr.GetString(7);
				if (dr.IsDBNull(8))
					throw new Exception("Value 'null' is not allowed for 'ObjType'");
				else
					cr.ObjType = dr.GetString(8);
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
