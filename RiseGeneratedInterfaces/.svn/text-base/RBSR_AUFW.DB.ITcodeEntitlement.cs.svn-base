using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.414 (#455)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.ITcodeEntitlement
{
	/// <summary>
	/// Return value from method GetTcodeEntitlement
	/// </summary>
	public struct returnGetTcodeEntitlement
	{
		public int ID;
		public string TCode;
		public string CHECKSUM;
		public string ActivityFolder;
		public string Type;
		public string AccessLevel;
		public string Comment;
	}
	/// <summary>
	/// Return value from method ListTcodeEntitlement
	/// </summary>
	public struct returnListTcodeEntitlement
	{
		public int ID;
		public string TCode;
		public string CHECKSUM;
		public string ActivityFolder;
		public string Type;
		public string AccessLevel;
		public string Comment;
	}
	/// <summary>
	/// Return value from method ListTcodeEntitlementByTcodeDictionary
	/// </summary>
	public struct returnListTcodeEntitlementByTcodeDictionary
	{
		public int ID;
		public string TCode;
		public string CHECKSUM;
		public string ActivityFolder;
		public string Type;
		public string AccessLevel;
		public string Comment;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_TcodeEntitlement
	/// </summary>
	public class ITcodeEntitlement
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
		public ITcodeEntitlement() : this((OdbcConnection)null) { }
		public ITcodeEntitlement(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ITcodeEntitlement(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="TCode"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewTcodeEntitlement(string TCode)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_TcodeEntitlement\"(\"c_u_TCode\") VALUES(?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (TCode == null) throw new Exception("TCode must not be null!");
			cmd.Parameters.Add("c_u_TCode", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_TCode"].Value = (TCode != null ? (object)TCode : DBNull.Value);
			OdbcDataReader dri = cmd.ExecuteReader();
			if (_dbConnection.Driver.ToLower().StartsWith("myodbc"))
			{
				cmd = _dbConnection.CreateCommand();
				cmd.CommandText = "SELECT LAST_INSERT_ID()";
				dri = cmd.ExecuteReader();
			}
			dri.Read();
			rv = (dri.IsDBNull(0) ? 0 : (typeof(long).Equals(dri.GetFieldType(0)) ? (int)dri.GetInt64(0) : (int)dri.GetInt32(0)));
			dri.Close();
			if (rv == 0) throw new Exception("Insert operation failed!");
			dri.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// delete a row from table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteTcodeEntitlement(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_TcodeEntitlement\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetTcodeEntitlement</returns>
		public returnGetTcodeEntitlement GetTcodeEntitlement(int ID)
		{
			returnGetTcodeEntitlement rv = new returnGetTcodeEntitlement();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_TCode\",\"c_u_CHECKSUM\",\"c_u_ActivityFolder\",\"c_u_Type\",\"c_u_AccessLevel\",\"c_u_Comment\" from \"t_RBSR_AUFW_u_TcodeEntitlement\" where \"c_id\"= ?";
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			int drctr = 0;
			while (dr.Read())
			{
				drctr++;
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					rv.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'TCode'");
				else
					rv.TCode = dr.GetString(1);
				if (dr.IsDBNull(2))
					rv.CHECKSUM = null;
				else
					rv.CHECKSUM = dr.GetString(2);
				if (dr.IsDBNull(3))
					rv.ActivityFolder = null;
				else
					rv.ActivityFolder = dr.GetString(3);
				if (dr.IsDBNull(4))
					rv.Type = null;
				else
					rv.Type = dr.GetString(4);
				if (dr.IsDBNull(5))
					rv.AccessLevel = null;
				else
					rv.AccessLevel = dr.GetString(5);
				if (dr.IsDBNull(6))
					rv.Comment = null;
				else
					rv.Comment = dr.GetString(6);
			}
			dr.Close();
			dr.Dispose();
			if (drctr != 1) throw new Exception("Operation selected no rows!");
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="TCode"></param>
		/// <param name="CHECKSUM"></param>
		/// <param name="ActivityFolder"></param>
		/// <param name="Type"></param>
		/// <param name="AccessLevel"></param>
		/// <param name="Comment"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetTcodeEntitlement(int ID, string TCode, string CHECKSUM, string ActivityFolder, string Type, string AccessLevel, string Comment)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_TcodeEntitlement\" set \"c_u_TCode\"=?,\"c_u_CHECKSUM\"=?,\"c_u_ActivityFolder\"=?,\"c_u_Type\"=?,\"c_u_AccessLevel\"=?,\"c_u_Comment\"=? where \"c_id\" = ?";
			if (TCode == null) throw new Exception("TCode must not be null!");
			cmd.Parameters.Add("c_u_TCode", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_TCode"].Value = (TCode != null ? (object)TCode : DBNull.Value);
			cmd.Parameters.Add("c_u_CHECKSUM", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_CHECKSUM"].Value = (CHECKSUM != null ? (object)CHECKSUM : DBNull.Value);
			cmd.Parameters.Add("c_u_ActivityFolder", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_ActivityFolder"].Value = (ActivityFolder != null ? (object)ActivityFolder : DBNull.Value);
			cmd.Parameters.Add("c_u_Type", OdbcType.NVarChar, 1);
			cmd.Parameters["c_u_Type"].Value = (Type != null ? (object)Type : DBNull.Value);
			cmd.Parameters.Add("c_u_AccessLevel", OdbcType.NVarChar, 1);
			cmd.Parameters["c_u_AccessLevel"].Value = (AccessLevel != null ? (object)AccessLevel : DBNull.Value);
			cmd.Parameters.Add("c_u_Comment", OdbcType.NText);
			cmd.Parameters["c_u_Comment"].Value = (Comment != null ? (object)Comment : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListTcodeEntitlement[]</returns>
		public returnListTcodeEntitlement[] ListTcodeEntitlement(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListTcodeEntitlement[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"tcode\"", "\"c_u_TCode\"").Replace("\"checksum\"", "\"c_u_CHECKSUM\"").Replace("\"activityfolder\"", "\"c_u_ActivityFolder\"").Replace("\"type\"", "\"c_u_Type\"").Replace("\"accesslevel\"", "\"c_u_AccessLevel\"").Replace("\"comment\"", "\"c_u_Comment\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"tcode\"", "\"c_u_TCode\"").Replace("\"checksum\"", "\"c_u_CHECKSUM\"").Replace("\"activityfolder\"", "\"c_u_ActivityFolder\"").Replace("\"type\"", "\"c_u_Type\"").Replace("\"accesslevel\"", "\"c_u_AccessLevel\"").Replace("\"comment\"", "\"c_u_Comment\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_TCode\", \"c_u_CHECKSUM\", \"c_u_ActivityFolder\", \"c_u_Type\", \"c_u_AccessLevel\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_TcodeEntitlement\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_TCode\", \"c_u_CHECKSUM\", \"c_u_ActivityFolder\", \"c_u_Type\", \"c_u_AccessLevel\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_TcodeEntitlement\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_TCode\", \"c_u_CHECKSUM\", \"c_u_ActivityFolder\", \"c_u_Type\", \"c_u_AccessLevel\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_TcodeEntitlement\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListTcodeEntitlement> rvl = new List<returnListTcodeEntitlement>();
			while (dr.Read())
			{
				returnListTcodeEntitlement cr = new returnListTcodeEntitlement();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'TCode'");
				else
					cr.TCode = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.CHECKSUM = null;
				else
					cr.CHECKSUM = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.ActivityFolder = null;
				else
					cr.ActivityFolder = dr.GetString(3);
				if (dr.IsDBNull(4))
					cr.Type = null;
				else
					cr.Type = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.AccessLevel = null;
				else
					cr.AccessLevel = dr.GetString(5);
				if (dr.IsDBNull(6))
					cr.Comment = null;
				else
					cr.Comment = dr.GetString(6);
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
		/// select a set of rows from table t_RBSR_AUFW_u_TcodeEntitlement.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="TcodeDictionaryID"></param>
		/// <returns>returnListTcodeEntitlementByTcodeDictionary[]</returns>
		public returnListTcodeEntitlementByTcodeDictionary[] ListTcodeEntitlementByTcodeDictionary(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int TcodeDictionaryID)
		{
			returnListTcodeEntitlementByTcodeDictionary[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"tcode\"", "\"c_u_TCode\"").Replace("\"checksum\"", "\"c_u_CHECKSUM\"").Replace("\"activityfolder\"", "\"c_u_ActivityFolder\"").Replace("\"type\"", "\"c_u_Type\"").Replace("\"accesslevel\"", "\"c_u_AccessLevel\"").Replace("\"comment\"", "\"c_u_Comment\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"tcode\"", "\"c_u_TCode\"").Replace("\"checksum\"", "\"c_u_CHECKSUM\"").Replace("\"activityfolder\"", "\"c_u_ActivityFolder\"").Replace("\"type\"", "\"c_u_Type\"").Replace("\"accesslevel\"", "\"c_u_AccessLevel\"").Replace("\"comment\"", "\"c_u_Comment\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_TCode\", \"c_u_CHECKSUM\", \"c_u_ActivityFolder\", \"c_u_Type\", \"c_u_AccessLevel\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_TcodeEntitlement\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_TCode\", \"c_u_CHECKSUM\", \"c_u_ActivityFolder\", \"c_u_Type\", \"c_u_AccessLevel\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_TcodeEntitlement\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_TCode\", \"c_u_CHECKSUM\", \"c_u_ActivityFolder\", \"c_u_Type\", \"c_u_AccessLevel\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_TcodeEntitlement\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListTcodeEntitlementByTcodeDictionary> rvl = new List<returnListTcodeEntitlementByTcodeDictionary>();
			while (dr.Read())
			{
				returnListTcodeEntitlementByTcodeDictionary cr = new returnListTcodeEntitlementByTcodeDictionary();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'TCode'");
				else
					cr.TCode = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.CHECKSUM = null;
				else
					cr.CHECKSUM = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.ActivityFolder = null;
				else
					cr.ActivityFolder = dr.GetString(3);
				if (dr.IsDBNull(4))
					cr.Type = null;
				else
					cr.Type = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.AccessLevel = null;
				else
					cr.AccessLevel = dr.GetString(5);
				if (dr.IsDBNull(6))
					cr.Comment = null;
				else
					cr.Comment = dr.GetString(6);
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
