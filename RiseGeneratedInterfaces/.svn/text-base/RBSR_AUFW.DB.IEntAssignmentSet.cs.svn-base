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

namespace RBSR_AUFW.DB.IEntAssignmentSet
{
	/// <summary>
	/// Return value from method GetEntAssignmentSet
	/// </summary>
	public struct returnGetEntAssignmentSet
	{
		public int ID;
		public string Status;
		public DateTime? DATETIMElock;
		public string Commentary;
		public int SubProcessID;
		public int UserID;
		public DateTime? DATETIMEbirth;
	}
	/// <summary>
	/// Return value from method ListEntAssignmentSet
	/// </summary>
	public struct returnListEntAssignmentSet
	{
		public int ID;
		public string Status;
		public DateTime? DATETIMElock;
		public string Commentary;
		public int SubProcessID;
		public int UserID;
		public DateTime? DATETIMEbirth;
	}
	/// <summary>
	/// Return value from method ListEntAssignmentSetBySubProcess
	/// </summary>
	public struct returnListEntAssignmentSetBySubProcess
	{
		public int ID;
		public string Status;
		public DateTime? DATETIMElock;
		public string Commentary;
		public int SubProcessID;
		public int UserID;
        public string UserLoginName;
		public DateTime? DATETIMEbirth;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_EntAssignmentSet
	/// </summary>
	public class IEntAssignmentSet
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
		public IEntAssignmentSet() : this((OdbcConnection)null) { }
		public IEntAssignmentSet(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IEntAssignmentSet(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewEntAssignmentSet(int SubProcessID, int UserID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_EntAssignmentSet\"(\"c_r_SubProcess\",\"c_r_User\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			cmd.Parameters.Add("c_r_User", OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
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
		/// delete a row from table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteEntAssignmentSet(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_EntAssignmentSet\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEntAssignmentSet</returns>
		public returnGetEntAssignmentSet GetEntAssignmentSet(int ID)
		{
			returnGetEntAssignmentSet rv = new returnGetEntAssignmentSet();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_Status\",\"c_u_DATETIMElock\",\"c_u_Commentary\",\"c_r_SubProcess\",\"c_r_User\",\"c_u_DATETIMEbirth\" from \"t_RBSR_AUFW_u_EntAssignmentSet\" where \"c_id\"= ?";
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
					rv.Status = null;
				else
					rv.Status = dr.GetString(1);
				if (dr.IsDBNull(2))
					rv.DATETIMElock = null;
				else
					rv.DATETIMElock = dr.GetDateTime(2);
				if (dr.IsDBNull(3))
					rv.Commentary = null;
				else
					rv.Commentary = dr.GetString(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					rv.SubProcessID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				else
					rv.UserID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					rv.DATETIMEbirth = null;
				else
					rv.DATETIMEbirth = dr.GetDateTime(6);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Status"></param>
		/// <param name="DATETIMElock"></param>
		/// <param name="Commentary"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="DATETIMEbirth"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetEntAssignmentSet(int ID, string Status, DateTime? DATETIMElock, string Commentary, int SubProcessID, int UserID, DateTime? DATETIMEbirth)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_EntAssignmentSet\" set \"c_u_Status\"=?,\"c_u_DATETIMElock\"=?,\"c_u_Commentary\"=?,\"c_r_SubProcess\"=?,\"c_r_User\"=?,\"c_u_DATETIMEbirth\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_u_Status", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Status"].Value = (Status != null ? (object)Status : DBNull.Value);
			cmd.Parameters.Add("c_u_DATETIMElock", OdbcType.DateTime);
			cmd.Parameters["c_u_DATETIMElock"].Value = (DATETIMElock != null ? (object)DATETIMElock : DBNull.Value);
			cmd.Parameters.Add("c_u_Commentary", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_Commentary"].Value = (Commentary != null ? (object)Commentary : DBNull.Value);
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			cmd.Parameters.Add("c_r_User", OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			cmd.Parameters.Add("c_u_DATETIMEbirth", OdbcType.DateTime);
            if (DATETIMEbirth != null)
            {
                DATETIMEbirth = DATETIMEbirth.Value.AddMilliseconds(0 - DATETIMEbirth.Value.Millisecond);
            }

			cmd.Parameters["c_u_DATETIMEbirth"].Value = (DATETIMEbirth != null ? (object)DATETIMEbirth : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListEntAssignmentSet[]</returns>
		public returnListEntAssignmentSet[] ListEntAssignmentSet(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListEntAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"datetimelock\"", "\"c_u_DATETIMElock\"").Replace("\"datetimebirth\"", "\"c_u_DATETIMEbirth\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"").Replace("\"user\"", "\"c_r_User\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"datetimelock\"", "\"c_u_DATETIMElock\"").Replace("\"datetimebirth\"", "\"c_u_DATETIMEbirth\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"").Replace("\"user\"", "\"c_r_User\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Status\", \"c_u_DATETIMElock\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_DATETIMEbirth\" FROM \"t_RBSR_AUFW_u_EntAssignmentSet\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Status\", \"c_u_DATETIMElock\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_DATETIMEbirth\" FROM \"t_RBSR_AUFW_u_EntAssignmentSet\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Status\", \"c_u_DATETIMElock\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_DATETIMEbirth\" FROM \"t_RBSR_AUFW_u_EntAssignmentSet\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEntAssignmentSet> rvl = new List<returnListEntAssignmentSet>();
			while (dr.Read())
			{
				returnListEntAssignmentSet cr = new returnListEntAssignmentSet();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					cr.Status = null;
				else
					cr.Status = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.DATETIMElock = null;
				else
					cr.DATETIMElock = dr.GetDateTime(2);
				if (dr.IsDBNull(3))
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				else
					cr.UserID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					cr.DATETIMEbirth = null;
				else
					cr.DATETIMEbirth = dr.GetDateTime(6);
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
		/// select a set of rows from table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="SubProcessID"></param>
		/// <returns>returnListEntAssignmentSetBySubProcess[]</returns>
		public returnListEntAssignmentSetBySubProcess[] ListEntAssignmentSetBySubProcess(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int SubProcessID)
		{
			returnListEntAssignmentSetBySubProcess[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"datetimelock\"", "\"c_u_DATETIMElock\"").Replace("\"datetimebirth\"", "\"c_u_DATETIMEbirth\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"").Replace("\"user\"", "\"c_r_User\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"datetimelock\"", "\"c_u_DATETIMElock\"").Replace("\"datetimebirth\"", "\"c_u_DATETIMEbirth\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"").Replace("\"user\"", "\"c_r_User\"");
            string join = " LEFT OUTER JOIN t_RBSR_AUFW_u_User TUSER ON TEASET.c_r_User=TUSER.c_id "; 

            if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Status\", \"c_u_DATETIMElock\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_DATETIMEbirth\", TUSER.c_u_Name as UserLoginName FROM \"t_RBSR_AUFW_u_EntAssignmentSet\" TEASET " 
                        + " LEFT OUTER JOIN t_RBSR_AUFW_u_User TUSER ON TEASET.c_r_User=TUSER.c_id " 
                        + " WHERE \"c_r_SubProcess\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") +
                        (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Status\", \"c_u_DATETIMElock\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_DATETIMEbirth\" FROM \"t_RBSR_AUFW_u_EntAssignmentSet\" WHERE \"c_r_SubProcess\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
                cmd.CommandText = "SELECT TEASET.c_id, \"c_u_Status\", \"c_u_DATETIMElock\", \"c_u_Commentary\", \"c_r_SubProcess\", \"c_r_User\", \"c_u_DATETIMEbirth\", TUSER.c_u_Name as UserLoginName FROM \"t_RBSR_AUFW_u_EntAssignmentSet\" TEASET " + join 
                    + " WHERE \"c_r_SubProcess\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_SubProcessID", OdbcType.Int);
			cmd.Parameters["1_SubProcessID"].Value = (object)SubProcessID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEntAssignmentSetBySubProcess> rvl = new List<returnListEntAssignmentSetBySubProcess>();
			while (dr.Read())
			{
				returnListEntAssignmentSetBySubProcess cr = new returnListEntAssignmentSetBySubProcess();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					cr.Status = null;
				else
					cr.Status = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.DATETIMElock = null;
				else
					cr.DATETIMElock = dr.GetDateTime(2);
				if (dr.IsDBNull(3))
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(4);
                if (dr.IsDBNull(5))
                     { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
                else
                {
                    cr.UserID = dr.GetInt32(5);
                    cr.UserLoginName = dr.GetString(7);
                }
				if (dr.IsDBNull(6))
					cr.DATETIMEbirth = null;
				else
					cr.DATETIMEbirth = dr.GetDateTime(6);
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
