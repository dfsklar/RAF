using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.442 (#483)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IAuthRow1251
{
	/// <summary>
	/// Return value from method GetAuthRow1251
	/// </summary>
	public struct returnGetAuthRow1251
	{
		public int ID;
		public string RangeLow;
		public string RangeHigh;
		public int SAPauthObjID;
		public int SAPauthFieldID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int EditStatus;
	}
	/// <summary>
	/// Return value from method ListAuthRow1251
	/// </summary>
	public struct returnListAuthRow1251
	{
		public int ID;
		public string RangeLow;
		public string RangeHigh;
		public int SAPauthObjID;
		public int SAPauthFieldID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int EditStatus;
	}
	/// <summary>
	/// Return value from method ListAuthRow1251BySAPauthObj
	/// </summary>
	public struct returnListAuthRow1251BySAPauthObj
	{
		public int ID;
		public string RangeLow;
		public string RangeHigh;
		public int SAPauthObjID;
		public int SAPauthFieldID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int EditStatus;
	}
	/// <summary>
	/// Return value from method ListAuthRow1251BySAPauthField
	/// </summary>
	public struct returnListAuthRow1251BySAPauthField
	{
		public int ID;
		public string RangeLow;
		public string RangeHigh;
		public int SAPauthObjID;
		public int SAPauthFieldID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int EditStatus;
	}
	/// <summary>
	/// Return value from method ListAuthRow1251ByTcodeAssignmentSet
	/// </summary>
	public struct returnListAuthRow1251ByTcodeAssignmentSet
	{
		public int ID;
		public string RangeLow;
		public string RangeHigh;
		public int SAPauthObjID;
		public int SAPauthFieldID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int EditStatus;
	}
	/// <summary>
	/// Return value from method ListAuthRow1251BySAProle
	/// </summary>
	public struct returnListAuthRow1251BySAProle
	{
		public int ID;
		public string RangeLow;
		public string RangeHigh;
		public int SAPauthObjID;
		public int SAPauthFieldID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int EditStatus;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_AuthRow1251
	/// </summary>
	public class IAuthRow1251 : _6MAR_WebApplication.RISEBASE
	{
		public IAuthRow1251() : this((OdbcConnection)null) { }
		public IAuthRow1251(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IAuthRow1251(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}
		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_AuthRow1251.
		/// </summary>
		/// <param name="SAPauthObjID"></param>
		/// <param name="SAPauthFieldID"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <param name="SAProleID"></param>
		/// <param name="EditStatus"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewAuthRow1251(int SAPauthObjID, int SAPauthFieldID, int TcodeAssignmentSetID, int SAProleID, int EditStatus)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_AuthRow1251\"(\"c_r_SAPauthObj\",\"c_r_SAPauthField\",\"c_r_TcodeAssignmentSet\",\"c_r_SAProle\",\"c_u_EditStatus\") VALUES(?,?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_r_SAPauthObj", OdbcType.Int);
			cmd.Parameters["c_r_SAPauthObj"].Value = (object)SAPauthObjID;
			cmd.Parameters.Add("c_r_SAPauthField", OdbcType.Int);
			cmd.Parameters["c_r_SAPauthField"].Value = (object)SAPauthFieldID;
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
		/// delete a row from table t_RBSR_AUFW_u_AuthRow1251.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteAuthRow1251(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_AuthRow1251\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_AuthRow1251.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetAuthRow1251</returns>
		public returnGetAuthRow1251 GetAuthRow1251(int ID)
		{
			returnGetAuthRow1251 rv = new returnGetAuthRow1251();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_RangeLow\",\"c_u_RangeHigh\",\"c_r_SAPauthObj\",\"c_r_SAPauthField\",\"c_r_TcodeAssignmentSet\",\"c_r_SAProle\",\"c_u_EditStatus\" from \"t_RBSR_AUFW_u_AuthRow1251\" where \"c_id\"= ?";
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
					rv.RangeLow = null;
				else
					rv.RangeLow = dr.GetString(1);
				if (dr.IsDBNull(2))
					rv.RangeHigh = null;
				else
					rv.RangeHigh = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SAPauthObjID'");
				else
					rv.SAPauthObjID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'SAPauthFieldID'");
				else
					rv.SAPauthFieldID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'");
				else
					rv.TcodeAssignmentSetID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'SAProleID'");
				else
					rv.SAProleID = dr.GetInt32(6);
				if (dr.IsDBNull(7))
					throw new Exception("Value 'null' is not allowed for 'EditStatus'");
				else
					rv.EditStatus = dr.GetInt32(7);
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
		/// update a row in table t_RBSR_AUFW_u_AuthRow1251.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="RangeLow"></param>
		/// <param name="RangeHigh"></param>
		/// <param name="SAPauthObjID"></param>
		/// <param name="SAPauthFieldID"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <param name="SAProleID"></param>
		/// <param name="EditStatus"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetAuthRow1251(int ID, string RangeLow, string RangeHigh, int SAPauthObjID, int SAPauthFieldID, int TcodeAssignmentSetID, int SAProleID, int EditStatus)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_AuthRow1251\" set \"c_u_RangeLow\"=?,\"c_u_RangeHigh\"=?,\"c_r_SAPauthObj\"=?,\"c_r_SAPauthField\"=?,\"c_r_TcodeAssignmentSet\"=?,\"c_r_SAProle\"=?,\"c_u_EditStatus\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_u_RangeLow", OdbcType.NVarChar, 30);
			cmd.Parameters["c_u_RangeLow"].Value = (RangeLow != null ? (object)RangeLow : DBNull.Value);
			cmd.Parameters.Add("c_u_RangeHigh", OdbcType.NVarChar, 30);
			cmd.Parameters["c_u_RangeHigh"].Value = (RangeHigh != null ? (object)RangeHigh : DBNull.Value);
			cmd.Parameters.Add("c_r_SAPauthObj", OdbcType.Int);
			cmd.Parameters["c_r_SAPauthObj"].Value = (object)SAPauthObjID;
			cmd.Parameters.Add("c_r_SAPauthField", OdbcType.Int);
			cmd.Parameters["c_r_SAPauthField"].Value = (object)SAPauthFieldID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_AuthRow1251.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListAuthRow1251[]</returns>
		public returnListAuthRow1251[] ListAuthRow1251(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListAuthRow1251[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"sapauthobj\"", "\"c_r_SAPauthObj\"").Replace("\"sapauthfield\"", "\"c_r_SAPauthField\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"sapauthobj\"", "\"c_r_SAPauthObj\"").Replace("\"sapauthfield\"", "\"c_r_SAPauthField\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListAuthRow1251> rvl = new List<returnListAuthRow1251>();
			while (dr.Read())
			{
				returnListAuthRow1251 cr = new returnListAuthRow1251();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					cr.RangeLow = null;
				else
					cr.RangeLow = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.RangeHigh = null;
				else
					cr.RangeHigh = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SAPauthObjID'");
				else
					cr.SAPauthObjID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'SAPauthFieldID'");
				else
					cr.SAPauthFieldID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'");
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'SAProleID'");
				else
					cr.SAProleID = dr.GetInt32(6);
				if (dr.IsDBNull(7))
					throw new Exception("Value 'null' is not allowed for 'EditStatus'");
				else
					cr.EditStatus = dr.GetInt32(7);
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
		/// select a set of rows from table t_RBSR_AUFW_u_AuthRow1251.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SAPauthObjID"></param>
		/// <returns>returnListAuthRow1251BySAPauthObj[]</returns>
		public returnListAuthRow1251BySAPauthObj[] ListAuthRow1251BySAPauthObj(int? maxRowsToReturn, int SAPauthObjID)
		{
			returnListAuthRow1251BySAPauthObj[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_SAPauthObj\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_SAPauthObj\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_SAPauthObj\"=?";
			cmd.Parameters.Add("1_SAPauthObjID", OdbcType.Int);
			cmd.Parameters["1_SAPauthObjID"].Value = (object)SAPauthObjID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListAuthRow1251BySAPauthObj> rvl = new List<returnListAuthRow1251BySAPauthObj>();
			while (dr.Read())
			{
				returnListAuthRow1251BySAPauthObj cr = new returnListAuthRow1251BySAPauthObj();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					cr.RangeLow = null;
				else
					cr.RangeLow = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.RangeHigh = null;
				else
					cr.RangeHigh = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SAPauthObjID'");
				else
					cr.SAPauthObjID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'SAPauthFieldID'");
				else
					cr.SAPauthFieldID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'");
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'SAProleID'");
				else
					cr.SAProleID = dr.GetInt32(6);
				if (dr.IsDBNull(7))
					throw new Exception("Value 'null' is not allowed for 'EditStatus'");
				else
					cr.EditStatus = dr.GetInt32(7);
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
		/// select a set of rows from table t_RBSR_AUFW_u_AuthRow1251.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="SAPauthFieldID"></param>
		/// <returns>returnListAuthRow1251BySAPauthField[]</returns>
		public returnListAuthRow1251BySAPauthField[] ListAuthRow1251BySAPauthField(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int SAPauthFieldID)
		{
			returnListAuthRow1251BySAPauthField[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"sapauthobj\"", "\"c_r_SAPauthObj\"").Replace("\"sapauthfield\"", "\"c_r_SAPauthField\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"sapauthobj\"", "\"c_r_SAPauthObj\"").Replace("\"sapauthfield\"", "\"c_r_SAPauthField\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_SAPauthField\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_SAPauthField\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_SAPauthField\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_SAPauthFieldID", OdbcType.Int);
			cmd.Parameters["1_SAPauthFieldID"].Value = (object)SAPauthFieldID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListAuthRow1251BySAPauthField> rvl = new List<returnListAuthRow1251BySAPauthField>();
			while (dr.Read())
			{
				returnListAuthRow1251BySAPauthField cr = new returnListAuthRow1251BySAPauthField();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					cr.RangeLow = null;
				else
					cr.RangeLow = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.RangeHigh = null;
				else
					cr.RangeHigh = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SAPauthObjID'");
				else
					cr.SAPauthObjID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'SAPauthFieldID'");
				else
					cr.SAPauthFieldID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'");
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'SAProleID'");
				else
					cr.SAProleID = dr.GetInt32(6);
				if (dr.IsDBNull(7))
					throw new Exception("Value 'null' is not allowed for 'EditStatus'");
				else
					cr.EditStatus = dr.GetInt32(7);
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
		/// select a set of rows from table t_RBSR_AUFW_u_AuthRow1251.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>returnListAuthRow1251ByTcodeAssignmentSet[]</returns>
		public returnListAuthRow1251ByTcodeAssignmentSet[] ListAuthRow1251ByTcodeAssignmentSet(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int TcodeAssignmentSetID)
		{
			returnListAuthRow1251ByTcodeAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"sapauthobj\"", "\"c_r_SAPauthObj\"").Replace("\"sapauthfield\"", "\"c_r_SAPauthField\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"sapauthobj\"", "\"c_r_SAPauthObj\"").Replace("\"sapauthfield\"", "\"c_r_SAPauthField\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_TcodeAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_TcodeAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_TcodeAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_TcodeAssignmentSetID", OdbcType.Int);
			cmd.Parameters["1_TcodeAssignmentSetID"].Value = (object)TcodeAssignmentSetID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListAuthRow1251ByTcodeAssignmentSet> rvl = new List<returnListAuthRow1251ByTcodeAssignmentSet>();
			while (dr.Read())
			{
				returnListAuthRow1251ByTcodeAssignmentSet cr = new returnListAuthRow1251ByTcodeAssignmentSet();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					cr.RangeLow = null;
				else
					cr.RangeLow = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.RangeHigh = null;
				else
					cr.RangeHigh = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SAPauthObjID'");
				else
					cr.SAPauthObjID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'SAPauthFieldID'");
				else
					cr.SAPauthFieldID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'");
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'SAProleID'");
				else
					cr.SAProleID = dr.GetInt32(6);
				if (dr.IsDBNull(7))
					throw new Exception("Value 'null' is not allowed for 'EditStatus'");
				else
					cr.EditStatus = dr.GetInt32(7);
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
		/// select a set of rows from table t_RBSR_AUFW_u_AuthRow1251.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="SAProleID"></param>
		/// <returns>returnListAuthRow1251BySAProle[]</returns>
		public returnListAuthRow1251BySAProle[] ListAuthRow1251BySAProle(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int SAProleID)
		{
			returnListAuthRow1251BySAProle[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"sapauthobj\"", "\"c_r_SAPauthObj\"").Replace("\"sapauthfield\"", "\"c_r_SAPauthField\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"rangelow\"", "\"c_u_RangeLow\"").Replace("\"rangehigh\"", "\"c_u_RangeHigh\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"sapauthobj\"", "\"c_r_SAPauthObj\"").Replace("\"sapauthfield\"", "\"c_r_SAPauthField\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_SAProle\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_SAProle\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_RangeLow\", \"c_u_RangeHigh\", \"c_r_SAPauthObj\", \"c_r_SAPauthField\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_u_EditStatus\" FROM \"t_RBSR_AUFW_u_AuthRow1251\" WHERE \"c_r_SAProle\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_SAProleID", OdbcType.Int);
			cmd.Parameters["1_SAProleID"].Value = (object)SAProleID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListAuthRow1251BySAProle> rvl = new List<returnListAuthRow1251BySAProle>();
			while (dr.Read())
			{
				returnListAuthRow1251BySAProle cr = new returnListAuthRow1251BySAProle();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					cr.RangeLow = null;
				else
					cr.RangeLow = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.RangeHigh = null;
				else
					cr.RangeHigh = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SAPauthObjID'");
				else
					cr.SAPauthObjID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'SAPauthFieldID'");
				else
					cr.SAPauthFieldID = dr.GetInt32(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'");
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'SAProleID'");
				else
					cr.SAProleID = dr.GetInt32(6);
				if (dr.IsDBNull(7))
					throw new Exception("Value 'null' is not allowed for 'EditStatus'");
				else
					cr.EditStatus = dr.GetInt32(7);
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
