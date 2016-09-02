using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.428 (#469)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IOrgValue1252
{
	/// <summary>
	/// Return value from method GetOrgValue1252
	/// </summary>
	public struct returnGetOrgValue1252
	{
		public int ID;
		public string FieldName;
		public string RangeLow;
		public string RangeHigh;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int EditStatus;
	}
	/// <summary>
	/// Return value from method ListOrgValue1252
	/// </summary>
	public struct returnListOrgValue1252
	{
		public int ID;
		public string FieldName;
		public string RangeLow;
		public string RangeHigh;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int EditStatus;
	}
	/// <summary>
	/// Return value from method ListOrgValue1252ByTcodeAssignmentSet
	/// </summary>
	public struct returnListOrgValue1252ByTcodeAssignmentSet
	{
		public int ID;
		public string FieldName;
		public string RangeLow;
		public string RangeHigh;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int EditStatus;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_OrgValue1252
	/// </summary>
	public class IOrgValue1252
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
		public IOrgValue1252() : this((OdbcConnection)null) { }
		public IOrgValue1252(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IOrgValue1252(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_OrgValue1252.
		/// </summary>
		/// <param name="FieldName"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <param name="SAProleID"></param>
		/// <param name="EditStatus"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewOrgValue1252(string FieldName, int TcodeAssignmentSetID, int SAProleID, int EditStatus)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_OrgValue1252\"(\"c_u_FieldName\",\"c_r_TcodeAssignmentSet\",\"c_r_SAProle\",\"c_u_EditStatus\") VALUES(?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (FieldName == null) throw new Exception("FieldName must not be null!");
			cmd.Parameters.Add("c_u_FieldName", OdbcType.NVarChar, 8);
			cmd.Parameters["c_u_FieldName"].Value = (FieldName != null ? (object)FieldName : DBNull.Value);
			cmd.Parameters.Add("c_r_TcodeAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_TcodeAssignmentSet"].Value = (object)TcodeAssignmentSetID;
			cmd.Parameters.Add("c_r_SAProle", OdbcType.Int);
			cmd.Parameters["c_r_SAProle"].Value = (object)SAProleID;
			cmd.Parameters.Add("c_u_EditStatus", OdbcType.Int);
			cmd.Parameters["c_u_EditStatus"].Value = (object)EditStatus;
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
		/// delete a row from table t_RBSR_AUFW_u_OrgValue1252.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteOrgValue1252(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_OrgValue1252\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_OrgValue1252.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetOrgValue1252</returns>
		public returnGetOrgValue1252 GetOrgValue1252(int ID)
		{
			returnGetOrgValue1252 rv = new returnGetOrgValue1252();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_FieldName\",\"c_u_RangeLow\",\"c_u_RangeHigh\",\"c_r_TcodeAssignmentSet\",\"c_r_SAProle\",\"c_u_EditStatus\" from \"t_RBSR_AUFW_u_OrgValue1252\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'FieldName'");
				else
					rv.FieldName = dr.GetString(1);
				if (dr.IsDBNull(2))
					rv.RangeLow = null;
				else
					rv.RangeLow = dr.GetString(2);
				if (dr.IsDBNull(3))
					rv.RangeHigh = null;
				else
					rv.RangeHigh = dr.GetString(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'");
				else
					rv.TcodeAssignmentSetID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'SAProleID'");
				else
					rv.SAProleID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'EditStatus'");
				else
					rv.EditStatus = dr.GetInt32(6);
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
		/// update a row in table t_RBSR_AUFW_u_OrgValue1252.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="FieldName"></param>
		/// <param name="RangeLow"></param>
		/// <param name="RangeHigh"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <param name="SAProleID"></param>
		/// <param name="EditStatus"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetOrgValue1252(int ID, string FieldName, string RangeLow, string RangeHigh, int TcodeAssignmentSetID, int SAProleID, int EditStatus)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_OrgValue1252\" set \"c_u_FieldName\"=?,\"c_u_RangeLow\"=?,\"c_u_RangeHigh\"=?,\"c_r_TcodeAssignmentSet\"=?,\"c_r_SAProle\"=?,\"c_u_EditStatus\"=? where \"c_id\" = ?";
			if (FieldName == null) throw new Exception("FieldName must not be null!");
			cmd.Parameters.Add("c_u_FieldName", OdbcType.NVarChar, 8);
			cmd.Parameters["c_u_FieldName"].Value = (FieldName != null ? (object)FieldName : DBNull.Value);
			cmd.Parameters.Add("c_u_RangeLow", OdbcType.NVarChar, 20);
			cmd.Parameters["c_u_RangeLow"].Value = (RangeLow != null ? (object)RangeLow : DBNull.Value);
			cmd.Parameters.Add("c_u_RangeHigh", OdbcType.NVarChar, 20);
			cmd.Parameters["c_u_RangeHigh"].Value = (RangeHigh != null ? (object)RangeHigh : DBNull.Value);
			cmd.Parameters.Add("c_r_TcodeAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_TcodeAssignmentSet"].Value = (object)TcodeAssignmentSetID;
			cmd.Parameters.Add("c_r_SAProle", OdbcType.Int);
			cmd.Parameters["c_r_SAProle"].Value = (object)SAProleID;
			cmd.Parameters.Add("c_u_EditStatus", OdbcType.Int);
			cmd.Parameters["c_u_EditStatus"].Value = (object)EditStatus;
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
		/// select a set of rows from table t_RBSR_AUFW_u_OrgValue1252.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListOrgValue1252[]</returns>
		public returnListOrgValue1252[] ListOrgValue1252(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListOrgValue1252[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"fieldname\"", "\"c_u_FieldName\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"fieldname\"", "\"c_u_FieldName\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_FieldName\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_OrgValue1252\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_FieldName\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_OrgValue1252\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_FieldName\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_OrgValue1252\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListOrgValue1252> rvl = new List<returnListOrgValue1252>();
			while (dr.Read())
			{
				returnListOrgValue1252 cr = new returnListOrgValue1252();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'FieldName'");
				else
					cr.FieldName = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.RangeLow = null;
				else
					cr.RangeLow = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.RangeHigh = null;
				else
					cr.RangeHigh = dr.GetString(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'");
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'SAProleID'");
				else
					cr.SAProleID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'EditStatus'");
				else
					cr.EditStatus = dr.GetInt32(6);
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
		/// select a set of rows from table t_RBSR_AUFW_u_OrgValue1252.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>returnListOrgValue1252ByTcodeAssignmentSet[]</returns>
		public returnListOrgValue1252ByTcodeAssignmentSet[] ListOrgValue1252ByTcodeAssignmentSet(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int TcodeAssignmentSetID)
		{
			returnListOrgValue1252ByTcodeAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"fieldname\"", "\"c_u_FieldName\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"fieldname\"", "\"c_u_FieldName\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_FieldName\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_OrgValue1252\" WHERE \"c_r_TcodeAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_FieldName\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_OrgValue1252\" WHERE \"c_r_TcodeAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_FieldName\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_OrgValue1252\" WHERE \"c_r_TcodeAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_TcodeAssignmentSetID", OdbcType.Int);
			cmd.Parameters["1_TcodeAssignmentSetID"].Value = (object)TcodeAssignmentSetID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListOrgValue1252ByTcodeAssignmentSet> rvl = new List<returnListOrgValue1252ByTcodeAssignmentSet>();
			while (dr.Read())
			{
				returnListOrgValue1252ByTcodeAssignmentSet cr = new returnListOrgValue1252ByTcodeAssignmentSet();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'FieldName'");
				else
					cr.FieldName = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.RangeLow = null;
				else
					cr.RangeLow = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.RangeHigh = null;
				else
					cr.RangeHigh = dr.GetString(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'");
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'SAProleID'");
				else
					cr.SAProleID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'EditStatus'");
				else
					cr.EditStatus = dr.GetInt32(6);
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
