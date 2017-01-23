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

namespace RBSR_AUFW.DB.ITcodeAssignmentSet
{
	/// <summary>
	/// Return value from method GetTcodeAssignmentSet
	/// </summary>
	public struct returnGetTcodeAssignmentSet
	{
		public int ID;
		public DateTime tstamp;
		public string Commentary;
		public int SubProcessID;
		public int UserID;
		public string Status;
	}
	/// <summary>
	/// Return value from method ListTcodeAssignmentSet
	/// </summary>
	public struct returnListTcodeAssignmentSet
	{
		public int ID;
		public DateTime tstamp;
		public string Commentary;
		public int SubProcessID;
		public int UserID;
		public string Status;
	}
	/// <summary>
	/// Return value from method ListTcodeAssignmentSetBySubProcess
	/// </summary>
	public struct returnListTcodeAssignmentSetBySubProcess
	{
		public int ID;
		public DateTime tstamp;
		public string Commentary;
		public int SubProcessID;
		public int UserID;
		public string Status;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_TcodeAssignmentSet
	/// </summary>
	public class ITcodeAssignmentSet
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
		public ITcodeAssignmentSet() : this((OdbcConnection)null) { }
		public ITcodeAssignmentSet(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ITcodeAssignmentSet(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="tstamp"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="Status"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewTcodeAssignmentSet(DateTime tstamp, int SubProcessID, int UserID, string Status)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_TcodeAssignmentSet\"(\"c_u_tstamp\",\"c_r_SubProcess\",\"c_r_User\",\"c_u_Status\") VALUES(?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_u_tstamp", OdbcType.DateTime);
            cmd.Parameters["c_u_tstamp"].Value = SetSafeDBDate(tstamp);// (object)tstamp;
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			cmd.Parameters.Add("c_r_User", OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			if (Status == null)  { cmd.Dispose(); DBClose(); throw new Exception("Status must not be null!"); } 
			cmd.Parameters.Add("c_u_Status", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Status"].Value = (Status != null ? (object)Status : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteTcodeAssignmentSet(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_TcodeAssignmentSet\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetTcodeAssignmentSet</returns>
		public returnGetTcodeAssignmentSet GetTcodeAssignmentSet(int ID)
		{
			returnGetTcodeAssignmentSet rv = new returnGetTcodeAssignmentSet();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_tstamp\",\"c_u_Commentary\",\"c_r_SubProcess\",\"c_r_User\",\"c_u_Status\" from \"t_RBSR_AUFW_u_TcodeAssignmentSet\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'tstamp'"); } 
				else
					rv.tstamp = dr.GetDateTime(1);
				if (dr.IsDBNull(2))
					rv.Commentary = null;
				else
					rv.Commentary = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					rv.SubProcessID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				else
					rv.UserID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Status'"); } 
				else
					rv.Status = dr.GetString(5);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}


        public static object SetSafeDBDate(System.DateTime dtIn)
        {
            if (dtIn == new DateTime(0))
                return System.DBNull.Value;
            else
                return new DateTime(dtIn.Year, dtIn.Month, dtIn.Day, dtIn.Hour, dtIn.Minute, dtIn.Second);
        }


		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="tstamp"></param>
		/// <param name="Commentary"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="Status"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetTcodeAssignmentSet(int ID, DateTime tstamp, string Commentary, int SubProcessID, int UserID, string Status)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_TcodeAssignmentSet\" set \"c_u_tstamp\"=?,\"c_u_Commentary\"=?,\"c_r_SubProcess\"=?,\"c_r_User\"=?,\"c_u_Status\"=? where \"c_id\" = ?";

            cmd.Parameters.Add("c_u_tstamp", OdbcType.DateTime);
            cmd.Parameters["c_u_tstamp"].Value = SetSafeDBDate(tstamp);
			cmd.Parameters.Add("c_u_Commentary", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_Commentary"].Value = (Commentary != null ? (object)Commentary : DBNull.Value);
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			cmd.Parameters.Add("c_r_User", OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			if (Status == null)  { cmd.Dispose(); DBClose(); throw new Exception("Status must not be null!"); } 
			cmd.Parameters.Add("c_u_Status", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Status"].Value = (Status != null ? (object)Status : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListTcodeAssignmentSet[]</returns>
		public returnListTcodeAssignmentSet[] ListTcodeAssignmentSet(int? maxRowsToReturn)
		{
			returnListTcodeAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_tstamp\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_TcodeAssignmentSet\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_tstamp\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_TcodeAssignmentSet\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_tstamp\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_TcodeAssignmentSet\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListTcodeAssignmentSet> rvl = new List<returnListTcodeAssignmentSet>();
			while (dr.Read())
			{
				returnListTcodeAssignmentSet cr = new returnListTcodeAssignmentSet();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'tstamp'"); } 
				else
					cr.tstamp = dr.GetDateTime(1);
				if (dr.IsDBNull(2))
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				else
					cr.UserID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Status'"); } 
				else
					cr.Status = dr.GetString(5);
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
		/// select a set of rows from table t_RBSR_AUFW_u_TcodeAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="SubProcessID"></param>
		/// <returns>returnListTcodeAssignmentSetBySubProcess[]</returns>
		public returnListTcodeAssignmentSetBySubProcess[] ListTcodeAssignmentSetBySubProcess(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int SubProcessID)
		{
			returnListTcodeAssignmentSetBySubProcess[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"tstamp\"", "\"c_u_tstamp\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"").Replace("\"user\"", "\"c_r_User\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"tstamp\"", "\"c_u_tstamp\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"").Replace("\"user\"", "\"c_r_User\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_tstamp\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_TcodeAssignmentSet\" WHERE \"c_r_SubProcess\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_tstamp\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_TcodeAssignmentSet\" WHERE \"c_r_SubProcess\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_tstamp\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_TcodeAssignmentSet\" WHERE \"c_r_SubProcess\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_SubProcessID", OdbcType.Int);
			cmd.Parameters["1_SubProcessID"].Value = (object)SubProcessID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListTcodeAssignmentSetBySubProcess> rvl = new List<returnListTcodeAssignmentSetBySubProcess>();
			while (dr.Read())
			{
				returnListTcodeAssignmentSetBySubProcess cr = new returnListTcodeAssignmentSetBySubProcess();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'tstamp'"); } 
				else
					cr.tstamp = dr.GetDateTime(1);
				if (dr.IsDBNull(2))
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				else
					cr.UserID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Status'"); } 
				else
					cr.Status = dr.GetString(5);
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
