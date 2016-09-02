using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.151 (#192)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IEntAssignment
{
	/// <summary>
	/// Return value from method GetEntAssignment
	/// </summary>
	public struct returnGetEntAssignment
	{
		public int ID;
		public int EntAssignmentSetID;
		public int BusRoleID;
		public int EntitlementID;
		public string Status;
	}
	/// <summary>
	/// Return value from method ListEntAssignment
	/// </summary>
	public struct returnListEntAssignment
	{
		public int ID;
		public int EntAssignmentSetID;
		public int BusRoleID;
		public int EntitlementID;
		public string Status;
	}
	/// <summary>
	/// Return value from method ListEntAssignmentByEntAssignmentSet
	/// </summary>
	public struct returnListEntAssignmentByEntAssignmentSet
	{
		public int ID;
		public int EntAssignmentSetID;
		public int BusRoleID;
		public int EntitlementID;
		public string Status;
	}
	/// <summary>
	/// Return value from method ListEntAssignmentByBusRole
	/// </summary>
	public struct returnListEntAssignmentByBusRole
	{
		public int ID;
		public int EntAssignmentSetID;
		public int BusRoleID;
		public int EntitlementID;
		public string Status;
	}
	/// <summary>
	/// Return value from method GetRoleEntAssignment
	/// </summary>
	public struct returnGetRoleEntAssignment
	{
		public int ID;
		public int RoleID;
		public int EntAssignmentID;
	}
	/// <summary>
	/// Return value from method ListRoleEntAssignment
	/// </summary>
	public struct returnListRoleEntAssignment
	{
		public int ID;
		public int RoleID;
		public int EntAssignmentID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_EntAssignment
	///     Table t_RBSR_AUFW_r_RoleEntAssignment
	/// </summary>
	public class IEntAssignment
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
		public IEntAssignment() : this((OdbcConnection)null) { }
		public IEntAssignment(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IEntAssignment(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="EntAssignmentSetID"></param>
		/// <param name="BusRoleID"></param>
		/// <param name="EntitlementID"></param>
		/// <param name="Status"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewEntAssignment(int EntAssignmentSetID, int BusRoleID, int EntitlementID, string Status)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_EntAssignment\"(\"c_r_EntAssignmentSet\",\"c_r_BusRole\",\"c_r_Entitlement\",\"c_u_Status\") VALUES(?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_r_EntAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_EntAssignmentSet"].Value = (object)EntAssignmentSetID;
			cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
			cmd.Parameters["c_r_BusRole"].Value = (object)BusRoleID;
			cmd.Parameters.Add("c_r_Entitlement", OdbcType.Int);
			cmd.Parameters["c_r_Entitlement"].Value = (object)EntitlementID;
			if (Status == null)  { cmd.Dispose(); DBClose(); throw new Exception("Status must not be null!"); } 
			cmd.Parameters.Add("c_u_Status", OdbcType.NVarChar, 1);
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
		/// delete a row from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteEntAssignment(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_EntAssignment\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEntAssignment</returns>
		public returnGetEntAssignment GetEntAssignment(int ID)
		{
			returnGetEntAssignment rv = new returnGetEntAssignment();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_r_EntAssignmentSet\",\"c_r_BusRole\",\"c_r_Entitlement\",\"c_u_Status\" from \"t_RBSR_AUFW_u_EntAssignment\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					rv.EntAssignmentSetID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					rv.BusRoleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntitlementID'"); } 
				else
					rv.EntitlementID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Status'"); } 
				else
					rv.Status = dr.GetString(4);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <param name="BusRoleID"></param>
		/// <param name="EntitlementID"></param>
		/// <param name="Status"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetEntAssignment(int ID, int EntAssignmentSetID, int BusRoleID, int EntitlementID, string Status)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_EntAssignment\" set \"c_r_EntAssignmentSet\"=?,\"c_r_BusRole\"=?,\"c_r_Entitlement\"=?,\"c_u_Status\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_r_EntAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_EntAssignmentSet"].Value = (object)EntAssignmentSetID;
			cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
			cmd.Parameters["c_r_BusRole"].Value = (object)BusRoleID;
			cmd.Parameters.Add("c_r_Entitlement", OdbcType.Int);
			cmd.Parameters["c_r_Entitlement"].Value = (object)EntitlementID;
			if (Status == null)  { cmd.Dispose(); DBClose(); throw new Exception("Status must not be null!"); } 
			cmd.Parameters.Add("c_u_Status", OdbcType.NVarChar, 1);
			cmd.Parameters["c_u_Status"].Value = (Status != null ? (object)Status : DBNull.Value);
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();

            // DFSKLAR ELIMINATED THIS NEXT LINE:
	//		if (rv != 1)  { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// select a set of rows from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListEntAssignment[]</returns>
		public returnListEntAssignment[] ListEntAssignment(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListEntAssignment[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"entassignmentset\"", "\"c_r_EntAssignmentSet\"").Replace("\"busrole\"", "\"c_r_BusRole\"").Replace("\"entitlement\"", "\"c_r_Entitlement\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"entassignmentset\"", "\"c_r_EntAssignmentSet\"").Replace("\"busrole\"", "\"c_r_BusRole\"").Replace("\"entitlement\"", "\"c_r_Entitlement\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_r_EntAssignmentSet\", \"c_r_BusRole\", \"c_r_Entitlement\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_EntAssignment\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_r_EntAssignmentSet\", \"c_r_BusRole\", \"c_r_Entitlement\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_EntAssignment\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_r_EntAssignmentSet\", \"c_r_BusRole\", \"c_r_Entitlement\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_EntAssignment\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEntAssignment> rvl = new List<returnListEntAssignment>();
			while (dr.Read())
			{
				returnListEntAssignment cr = new returnListEntAssignment();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					cr.EntAssignmentSetID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					cr.BusRoleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntitlementID'"); } 
				else
					cr.EntitlementID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Status'"); } 
				else
					cr.Status = dr.GetString(4);
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
		/// select a set of rows from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>returnListEntAssignmentByEntAssignmentSet[]</returns>
		public returnListEntAssignmentByEntAssignmentSet[] ListEntAssignmentByEntAssignmentSet(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int EntAssignmentSetID)
		{
			returnListEntAssignmentByEntAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"entassignmentset\"", "\"c_r_EntAssignmentSet\"").Replace("\"busrole\"", "\"c_r_BusRole\"").Replace("\"entitlement\"", "\"c_r_Entitlement\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"entassignmentset\"", "\"c_r_EntAssignmentSet\"").Replace("\"busrole\"", "\"c_r_BusRole\"").Replace("\"entitlement\"", "\"c_r_Entitlement\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_r_EntAssignmentSet\", \"c_r_BusRole\", \"c_r_Entitlement\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_EntAssignment\" WHERE \"c_r_EntAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_r_EntAssignmentSet\", \"c_r_BusRole\", \"c_r_Entitlement\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_EntAssignment\" WHERE \"c_r_EntAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_r_EntAssignmentSet\", \"c_r_BusRole\", \"c_r_Entitlement\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_EntAssignment\" WHERE \"c_r_EntAssignmentSet\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_EntAssignmentSetID", OdbcType.Int);
			cmd.Parameters["1_EntAssignmentSetID"].Value = (object)EntAssignmentSetID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEntAssignmentByEntAssignmentSet> rvl = new List<returnListEntAssignmentByEntAssignmentSet>();
			while (dr.Read())
			{
				returnListEntAssignmentByEntAssignmentSet cr = new returnListEntAssignmentByEntAssignmentSet();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					cr.EntAssignmentSetID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					cr.BusRoleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntitlementID'"); } 
				else
					cr.EntitlementID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Status'"); } 
				else
					cr.Status = dr.GetString(4);
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
		/// select a set of rows from table t_RBSR_AUFW_u_EntAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="BusRoleID"></param>
		/// <returns>returnListEntAssignmentByBusRole[]</returns>
		public returnListEntAssignmentByBusRole[] ListEntAssignmentByBusRole(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int BusRoleID)
		{
			returnListEntAssignmentByBusRole[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"entassignmentset\"", "\"c_r_EntAssignmentSet\"").Replace("\"busrole\"", "\"c_r_BusRole\"").Replace("\"entitlement\"", "\"c_r_Entitlement\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"entassignmentset\"", "\"c_r_EntAssignmentSet\"").Replace("\"busrole\"", "\"c_r_BusRole\"").Replace("\"entitlement\"", "\"c_r_Entitlement\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_r_EntAssignmentSet\", \"c_r_BusRole\", \"c_r_Entitlement\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_EntAssignment\" WHERE \"c_r_BusRole\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_r_EntAssignmentSet\", \"c_r_BusRole\", \"c_r_Entitlement\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_EntAssignment\" WHERE \"c_r_BusRole\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_r_EntAssignmentSet\", \"c_r_BusRole\", \"c_r_Entitlement\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_EntAssignment\" WHERE \"c_r_BusRole\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_BusRoleID", OdbcType.Int);
			cmd.Parameters["1_BusRoleID"].Value = (object)BusRoleID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEntAssignmentByBusRole> rvl = new List<returnListEntAssignmentByBusRole>();
			while (dr.Read())
			{
				returnListEntAssignmentByBusRole cr = new returnListEntAssignmentByBusRole();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
				else
					cr.EntAssignmentSetID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					cr.BusRoleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntitlementID'"); } 
				else
					cr.EntitlementID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Status'"); } 
				else
					cr.Status = dr.GetString(4);
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
		/// insert a row in table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="RoleID"></param>
		/// <param name="EntAssignmentID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewRoleEntAssignment(int RoleID, int EntAssignmentID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_r_RoleEntAssignment\"(\"c_r_Role\",\"c_r_EntAssignment\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_r_Role", OdbcType.Int);
			cmd.Parameters["c_r_Role"].Value = (object)RoleID;
			cmd.Parameters.Add("c_r_EntAssignment", OdbcType.Int);
			cmd.Parameters["c_r_EntAssignment"].Value = (object)EntAssignmentID;
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
		/// delete a row from table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteRoleEntAssignment(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_r_RoleEntAssignment\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetRoleEntAssignment</returns>
		public returnGetRoleEntAssignment GetRoleEntAssignment(int ID)
		{
			returnGetRoleEntAssignment rv = new returnGetRoleEntAssignment();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_r_Role\",\"c_r_EntAssignment\" from \"t_RBSR_AUFW_r_RoleEntAssignment\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentID'"); } 
				else
					rv.EntAssignmentID = dr.GetInt32(2);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="RoleID"></param>
		/// <param name="EntAssignmentID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetRoleEntAssignment(int ID, int RoleID, int EntAssignmentID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_r_RoleEntAssignment\" set \"c_r_Role\"=?,\"c_r_EntAssignment\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_r_Role", OdbcType.Int);
			cmd.Parameters["c_r_Role"].Value = (object)RoleID;
			cmd.Parameters.Add("c_r_EntAssignment", OdbcType.Int);
			cmd.Parameters["c_r_EntAssignment"].Value = (object)EntAssignmentID;
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
		/// select a set of rows from table t_RBSR_AUFW_r_RoleEntAssignment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListRoleEntAssignment[]</returns>
		public returnListRoleEntAssignment[] ListRoleEntAssignment(int? maxRowsToReturn)
		{
			returnListRoleEntAssignment[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_r_Role\", \"c_r_EntAssignment\" FROM \"t_RBSR_AUFW_r_RoleEntAssignment\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_r_Role\", \"c_r_EntAssignment\" FROM \"t_RBSR_AUFW_r_RoleEntAssignment\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_r_Role\", \"c_r_EntAssignment\" FROM \"t_RBSR_AUFW_r_RoleEntAssignment\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListRoleEntAssignment> rvl = new List<returnListRoleEntAssignment>();
			while (dr.Read())
			{
				returnListRoleEntAssignment cr = new returnListRoleEntAssignment();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'RoleID'"); } 
				else
					cr.RoleID = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentID'"); } 
				else
					cr.EntAssignmentID = dr.GetInt32(2);
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
