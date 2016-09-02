/*
 * DFSklar edited NewTcodeAssignment to init the Editstatus to "2" 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.159 (#200)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.ITcodeAssignment
{
	/// <summary>
	/// Return value from method GetTcodeAssignment
	/// </summary>
	public struct returnGetTcodeAssignment
	{
		public int ID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int TcodeEntitlementID;
		public int EditStatus;
		public string Commentary;
	}
	/// <summary>
	/// Return value from method ListTcodeAssignment
	/// </summary>
	public struct returnListTcodeAssignment
	{
		public int ID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int TcodeEntitlementID;
		public int EditStatus;
		public string Commentary;
	}
	/// <summary>
	/// Return value from method ListTcodeAssignmentByTcodeAssignmentSet
	/// </summary>
	public struct returnListTcodeAssignmentByTcodeAssignmentSet
	{
		public int ID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int TcodeEntitlementID;
		public int EditStatus;
		public string Commentary;
	}
	/// <summary>
	/// Return value from method ListTcodeAssignmentBySAProle
	/// </summary>
	public struct returnListTcodeAssignmentBySAProle
	{
		public int ID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int TcodeEntitlementID;
		public int EditStatus;
		public string Commentary;
	}
	/// <summary>
	/// Return value from method ListTcodeAssignmentByTcodeEntitlement
	/// </summary>
	public struct returnListTcodeAssignmentByTcodeEntitlement
	{
		public int ID;
		public int TcodeAssignmentSetID;
		public int SAProleID;
		public int TcodeEntitlementID;
		public int EditStatus;
		public string Commentary;
	}
	/// <summary>
	/// Return value from method GetRoleTcodeAssignment
	/// </summary>
	public struct returnGetRoleTcodeAssignment
	{
		public int ID;
		public int RoleID;
		public int TcodeAssignmentID;
	}
	/// <summary>
	/// Return value from method ListRoleTcodeAssignment
	/// </summary>
	public struct returnListRoleTcodeAssignment
	{
		public int ID;
		public int RoleID;
		public int TcodeAssignmentID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_TcodeAssignment
	///     Table t_RBSR_AUFW_r_RoleTcodeAssignment
	/// </summary>
	public class ITcodeAssignment
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
		public ITcodeAssignment() : this((OdbcConnection)null) { }
		public ITcodeAssignment(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ITcodeAssignment(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <param name="SAProleID"></param>
		/// <param name="TcodeEntitlementID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewTcodeAssignment(int TcodeAssignmentSetID, int SAProleID, int TcodeEntitlementID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_TcodeAssignment\"(\"c_r_TcodeAssignmentSet\",\"c_r_SAProle\",\"c_r_TcodeEntitlement\",\"c_u_EditStatus\") VALUES(?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_r_TcodeAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_TcodeAssignmentSet"].Value = (object)TcodeAssignmentSetID;
			cmd.Parameters.Add("c_r_SAProle", OdbcType.Int);
			cmd.Parameters["c_r_SAProle"].Value = (object)SAProleID;

            cmd.Parameters.Add("c_r_TcodeEntitlement", OdbcType.Int);
			cmd.Parameters["c_r_TcodeEntitlement"].Value = (object)TcodeEntitlementID;

            cmd.Parameters.Add("c_u_EditStatus", OdbcType.Int);
            cmd.Parameters["c_u_EditStatus"].Value = (object)2;
            
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
		/// delete a row from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteTcodeAssignment(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_TcodeAssignment\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetTcodeAssignment</returns>
		public returnGetTcodeAssignment GetTcodeAssignment(int ID)
		{
			returnGetTcodeAssignment rv = new returnGetTcodeAssignment();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_r_TcodeAssignmentSet\",\"c_r_SAProle\",\"c_r_TcodeEntitlement\",\"c_u_EditStatus\",\"c_u_Commentary\" from \"t_RBSR_AUFW_u_TcodeAssignment\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
				else
					rv.TcodeAssignmentSetID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAProleID'"); } 
				else
					rv.SAProleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeEntitlementID'"); } 
				else
					rv.TcodeEntitlementID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditStatus'"); } 
				else
					rv.EditStatus = dr.GetInt32(4);
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
		/// update a row in table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <param name="SAProleID"></param>
		/// <param name="TcodeEntitlementID"></param>
		/// <param name="EditStatus"></param>
		/// <param name="Commentary"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetTcodeAssignment(int ID, int TcodeAssignmentSetID, int SAProleID, int TcodeEntitlementID, int EditStatus, string Commentary)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_TcodeAssignment\" set \"c_r_TcodeAssignmentSet\"=?,\"c_r_SAProle\"=?,\"c_r_TcodeEntitlement\"=?,\"c_u_EditStatus\"=?,\"c_u_Commentary\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_r_TcodeAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_TcodeAssignmentSet"].Value = (object)TcodeAssignmentSetID;
			cmd.Parameters.Add("c_r_SAProle", OdbcType.Int);
			cmd.Parameters["c_r_SAProle"].Value = (object)SAProleID;
			cmd.Parameters.Add("c_r_TcodeEntitlement", OdbcType.Int);
			cmd.Parameters["c_r_TcodeEntitlement"].Value = (object)TcodeEntitlementID;
			cmd.Parameters.Add("c_u_EditStatus", OdbcType.Int);
			cmd.Parameters["c_u_EditStatus"].Value = (object)EditStatus;
			cmd.Parameters.Add("c_u_Commentary", OdbcType.NVarChar, 1024);
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
		/// select a set of rows from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListTcodeAssignment[]</returns>
		public returnListTcodeAssignment[] ListTcodeAssignment(int? maxRowsToReturn)
		{
			returnListTcodeAssignment[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListTcodeAssignment> rvl = new List<returnListTcodeAssignment>();
			while (dr.Read())
			{
				returnListTcodeAssignment cr = new returnListTcodeAssignment();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAProleID'"); } 
				else
					cr.SAProleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeEntitlementID'"); } 
				else
					cr.TcodeEntitlementID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditStatus'"); } 
				else
					cr.EditStatus = dr.GetInt32(4);
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
		/// select a set of rows from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>returnListTcodeAssignmentByTcodeAssignmentSet[]</returns>
		public returnListTcodeAssignmentByTcodeAssignmentSet[] ListTcodeAssignmentByTcodeAssignmentSet(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int TcodeAssignmentSetID)
		{
			returnListTcodeAssignmentByTcodeAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"").Replace("\"tcodeentitlement\"", "\"c_r_TcodeEntitlement\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"editstatus\"", "\"c_u_EditStatus\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"tcodeassignmentset\"", "\"c_r_TcodeAssignmentSet\"").Replace("\"saprole\"", "\"c_r_SAProle\"").Replace("\"tcodeentitlement\"", "\"c_r_TcodeEntitlement\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\" WHERE \"c_r_TcodeAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\" WHERE \"c_r_TcodeAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\" WHERE \"c_r_TcodeAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_TcodeAssignmentSetID", OdbcType.Int);
			cmd.Parameters["1_TcodeAssignmentSetID"].Value = (object)TcodeAssignmentSetID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListTcodeAssignmentByTcodeAssignmentSet> rvl = new List<returnListTcodeAssignmentByTcodeAssignmentSet>();
			while (dr.Read())
			{
				returnListTcodeAssignmentByTcodeAssignmentSet cr = new returnListTcodeAssignmentByTcodeAssignmentSet();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAProleID'"); } 
				else
					cr.SAProleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeEntitlementID'"); } 
				else
					cr.TcodeEntitlementID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditStatus'"); } 
				else
					cr.EditStatus = dr.GetInt32(4);
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
		/// select a set of rows from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SAProleID"></param>
		/// <returns>returnListTcodeAssignmentBySAProle[]</returns>
		public returnListTcodeAssignmentBySAProle[] ListTcodeAssignmentBySAProle(int? maxRowsToReturn, int SAProleID)
		{
			returnListTcodeAssignmentBySAProle[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\" WHERE \"c_r_SAProle\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\" WHERE \"c_r_SAProle\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\" WHERE \"c_r_SAProle\"=?";
			cmd.Parameters.Add("1_SAProleID", OdbcType.Int);
			cmd.Parameters["1_SAProleID"].Value = (object)SAProleID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListTcodeAssignmentBySAProle> rvl = new List<returnListTcodeAssignmentBySAProle>();
			while (dr.Read())
			{
				returnListTcodeAssignmentBySAProle cr = new returnListTcodeAssignmentBySAProle();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAProleID'"); } 
				else
					cr.SAProleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeEntitlementID'"); } 
				else
					cr.TcodeEntitlementID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditStatus'"); } 
				else
					cr.EditStatus = dr.GetInt32(4);
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
		/// select a set of rows from table t_RBSR_AUFW_u_TcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="TcodeEntitlementID"></param>
		/// <returns>returnListTcodeAssignmentByTcodeEntitlement[]</returns>
		public returnListTcodeAssignmentByTcodeEntitlement[] ListTcodeAssignmentByTcodeEntitlement(int? maxRowsToReturn, int TcodeEntitlementID)
		{
			returnListTcodeAssignmentByTcodeEntitlement[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\" WHERE \"c_r_TcodeEntitlement\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\" WHERE \"c_r_TcodeEntitlement\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_r_TcodeAssignmentSet\", \"c_r_SAProle\", \"c_r_TcodeEntitlement\", \"c_u_EditStatus\", \"c_u_Commentary\" FROM \"t_RBSR_AUFW_u_TcodeAssignment\" WHERE \"c_r_TcodeEntitlement\"=?";
			cmd.Parameters.Add("1_TcodeEntitlementID", OdbcType.Int);
			cmd.Parameters["1_TcodeEntitlementID"].Value = (object)TcodeEntitlementID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListTcodeAssignmentByTcodeEntitlement> rvl = new List<returnListTcodeAssignmentByTcodeEntitlement>();
			while (dr.Read())
			{
				returnListTcodeAssignmentByTcodeEntitlement cr = new returnListTcodeAssignmentByTcodeEntitlement();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
				else
					cr.TcodeAssignmentSetID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAProleID'"); } 
				else
					cr.SAProleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeEntitlementID'"); } 
				else
					cr.TcodeEntitlementID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditStatus'"); } 
				else
					cr.EditStatus = dr.GetInt32(4);
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
		/// insert a row in table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="RoleID"></param>
		/// <param name="TcodeAssignmentID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewRoleTcodeAssignment(int RoleID, int TcodeAssignmentID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_r_RoleTcodeAssignment\"(\"c_r_Role\",\"c_r_TcodeAssignment\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_r_Role", OdbcType.Int);
			cmd.Parameters["c_r_Role"].Value = (object)RoleID;
			cmd.Parameters.Add("c_r_TcodeAssignment", OdbcType.Int);
			cmd.Parameters["c_r_TcodeAssignment"].Value = (object)TcodeAssignmentID;
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
		/// delete a row from table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteRoleTcodeAssignment(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_r_RoleTcodeAssignment\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetRoleTcodeAssignment</returns>
		public returnGetRoleTcodeAssignment GetRoleTcodeAssignment(int ID)
		{
			returnGetRoleTcodeAssignment rv = new returnGetRoleTcodeAssignment();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_r_Role\",\"c_r_TcodeAssignment\" from \"t_RBSR_AUFW_r_RoleTcodeAssignment\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'RoleID'"); } 
				else
					rv.RoleID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentID'"); } 
				else
					rv.TcodeAssignmentID = dr.GetInt32(2);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="RoleID"></param>
		/// <param name="TcodeAssignmentID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetRoleTcodeAssignment(int ID, int RoleID, int TcodeAssignmentID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_r_RoleTcodeAssignment\" set \"c_r_Role\"=?,\"c_r_TcodeAssignment\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_r_Role", OdbcType.Int);
			cmd.Parameters["c_r_Role"].Value = (object)RoleID;
			cmd.Parameters.Add("c_r_TcodeAssignment", OdbcType.Int);
			cmd.Parameters["c_r_TcodeAssignment"].Value = (object)TcodeAssignmentID;
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
		/// select a set of rows from table t_RBSR_AUFW_r_RoleTcodeAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListRoleTcodeAssignment[]</returns>
		public returnListRoleTcodeAssignment[] ListRoleTcodeAssignment(int? maxRowsToReturn)
		{
			returnListRoleTcodeAssignment[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_r_Role\", \"c_r_TcodeAssignment\" FROM \"t_RBSR_AUFW_r_RoleTcodeAssignment\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_r_Role\", \"c_r_TcodeAssignment\" FROM \"t_RBSR_AUFW_r_RoleTcodeAssignment\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_r_Role\", \"c_r_TcodeAssignment\" FROM \"t_RBSR_AUFW_r_RoleTcodeAssignment\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListRoleTcodeAssignment> rvl = new List<returnListRoleTcodeAssignment>();
			while (dr.Read())
			{
				returnListRoleTcodeAssignment cr = new returnListRoleTcodeAssignment();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'RoleID'"); } 
				else
					cr.RoleID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentID'"); } 
				else
					cr.TcodeAssignmentID = dr.GetInt32(2);
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
