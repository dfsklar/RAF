using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.445 (#486)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.ISAProle
{
	/// <summary>
	/// Return value from method GetSAProle
	/// </summary>
	public struct returnGetSAProle
	{
		public int ID;
		public string Name;
		public string Description;
		public int SubProcessID;
		public string System;
		public string Platform;
		public string RoleActivity;
		public string RoleType;
		public string Comment;
	}
	/// <summary>
	/// Return value from method ListSAProle
	/// </summary>
	public struct returnListSAProle
	{
		public int ID;
		public string Name;
		public string Description;
		public int SubProcessID;
		public string System;
		public string Platform;
		public string RoleActivity;
		public string RoleType;
		public string Comment;
	}
	/// <summary>
	/// Return value from method ListSAProleByBusRole
	/// </summary>
	public struct returnListSAProleByBusRole
	{
		public int ID;
		public string Name;
		public string Description;
		public int SubProcessID;
		public string System;
		public string Platform;
		public string RoleActivity;
		public string RoleType;
		public string Comment;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_SAProle
	/// </summary>
	public class ISAProle : _6MAR_WebApplication.RISEBASE
	{
		public ISAProle() : this((OdbcConnection)null) { }
		public ISAProle(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ISAProle(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="System"></param>
		/// <param name="Platform"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewSAProle(string Name, int SubProcessID, string System, string Platform)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_SAProle\"(\"c_u_Name\",\"c_r_SubProcess\",\"c_u_System\",\"c_u_Platform\") VALUES(?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			if (System == null) throw new Exception("System must not be null!");
			cmd.Parameters.Add("c_u_System", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_System"].Value = (System != null ? (object)System : DBNull.Value);
			if (Platform == null) throw new Exception("Platform must not be null!");
			cmd.Parameters.Add("c_u_Platform", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Platform"].Value = (Platform != null ? (object)Platform : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteSAProle(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_SAProle\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAProle</returns>
		public returnGetSAProle GetSAProle(int ID)
		{
			returnGetSAProle rv = new returnGetSAProle();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_Name\",\"c_u_Description\",\"c_r_SubProcess\",\"c_u_System\",\"c_u_Platform\",\"c_u_RoleActivity\",\"c_u_RoleType\",\"c_u_Comment\" from \"t_RBSR_AUFW_u_SAProle\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'Name'");
				else
					rv.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					rv.Description = null;
				else
					rv.Description = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SubProcessID'");
				else
					rv.SubProcessID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'System'");
				else
					rv.System = dr.GetString(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'Platform'");
				else
					rv.Platform = dr.GetString(5);
				if (dr.IsDBNull(6))
					rv.RoleActivity = null;
				else
					rv.RoleActivity = dr.GetString(6);
				if (dr.IsDBNull(7))
					rv.RoleType = null;
				else
					rv.RoleType = dr.GetString(7);
				if (dr.IsDBNull(8))
					rv.Comment = null;
				else
					rv.Comment = dr.GetString(8);
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
		/// update a row in table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="System"></param>
		/// <param name="Platform"></param>
		/// <param name="RoleActivity"></param>
		/// <param name="RoleType"></param>
		/// <param name="Comment"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetSAProle(int ID, string Name, string Description, int SubProcessID, string System, string Platform, string RoleActivity, string RoleType, string Comment)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_SAProle\" set \"c_u_Name\"=?,\"c_u_Description\"=?,\"c_r_SubProcess\"=?,\"c_u_System\"=?,\"c_u_Platform\"=?,\"c_u_RoleActivity\"=?,\"c_u_RoleType\"=?,\"c_u_Comment\"=? where \"c_id\" = ?";
			if (Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			cmd.Parameters.Add("c_u_Description", OdbcType.NVarChar, 512);
			cmd.Parameters["c_u_Description"].Value = (Description != null ? (object)Description : DBNull.Value);
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			if (System == null) throw new Exception("System must not be null!");
			cmd.Parameters.Add("c_u_System", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_System"].Value = (System != null ? (object)System : DBNull.Value);
			if (Platform == null) throw new Exception("Platform must not be null!");
			cmd.Parameters.Add("c_u_Platform", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Platform"].Value = (Platform != null ? (object)Platform : DBNull.Value);
			cmd.Parameters.Add("c_u_RoleActivity", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_RoleActivity"].Value = (RoleActivity != null ? (object)RoleActivity : DBNull.Value);
			cmd.Parameters.Add("c_u_RoleType", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_RoleType"].Value = (RoleType != null ? (object)RoleType : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListSAProle[]</returns>
		public returnListSAProle[] ListSAProle(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListSAProle[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"system\"", "\"c_u_System\"").Replace("\"platform\"", "\"c_u_Platform\"").Replace("\"roleactivity\"", "\"c_u_RoleActivity\"").Replace("\"roletype\"", "\"c_u_RoleType\"").Replace("\"comment\"", "\"c_u_Comment\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"system\"", "\"c_u_System\"").Replace("\"platform\"", "\"c_u_Platform\"").Replace("\"roleactivity\"", "\"c_u_RoleActivity\"").Replace("\"roletype\"", "\"c_u_RoleType\"").Replace("\"comment\"", "\"c_u_Comment\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_System\", \"c_u_Platform\", \"c_u_RoleActivity\", \"c_u_RoleType\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_SAProle\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_System\", \"c_u_Platform\", \"c_u_RoleActivity\", \"c_u_RoleType\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_SAProle\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_System\", \"c_u_Platform\", \"c_u_RoleActivity\", \"c_u_RoleType\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_SAProle\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAProle> rvl = new List<returnListSAProle>();
			while (dr.Read())
			{
				returnListSAProle cr = new returnListSAProle();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'Name'");
				else
					cr.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.Description = null;
				else
					cr.Description = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SubProcessID'");
				else
					cr.SubProcessID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'System'");
				else
					cr.System = dr.GetString(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'Platform'");
				else
					cr.Platform = dr.GetString(5);
				if (dr.IsDBNull(6))
					cr.RoleActivity = null;
				else
					cr.RoleActivity = dr.GetString(6);
				if (dr.IsDBNull(7))
					cr.RoleType = null;
				else
					cr.RoleType = dr.GetString(7);
				if (dr.IsDBNull(8))
					cr.Comment = null;
				else
					cr.Comment = dr.GetString(8);
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAProle.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="BusRoleID"></param>
		/// <returns>returnListSAProleByBusRole[]</returns>
		public returnListSAProleByBusRole[] ListSAProleByBusRole(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int BusRoleID)
		{
			returnListSAProleByBusRole[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"system\"", "\"c_u_System\"").Replace("\"platform\"", "\"c_u_Platform\"").Replace("\"roleactivity\"", "\"c_u_RoleActivity\"").Replace("\"roletype\"", "\"c_u_RoleType\"").Replace("\"comment\"", "\"c_u_Comment\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"system\"", "\"c_u_System\"").Replace("\"platform\"", "\"c_u_Platform\"").Replace("\"roleactivity\"", "\"c_u_RoleActivity\"").Replace("\"roletype\"", "\"c_u_RoleType\"").Replace("\"comment\"", "\"c_u_Comment\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_System\", \"c_u_Platform\", \"c_u_RoleActivity\", \"c_u_RoleType\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_SAProle\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_System\", \"c_u_Platform\", \"c_u_RoleActivity\", \"c_u_RoleType\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_SAProle\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_System\", \"c_u_Platform\", \"c_u_RoleActivity\", \"c_u_RoleType\", \"c_u_Comment\" FROM \"t_RBSR_AUFW_u_SAProle\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAProleByBusRole> rvl = new List<returnListSAProleByBusRole>();
			while (dr.Read())
			{
				returnListSAProleByBusRole cr = new returnListSAProleByBusRole();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'Name'");
				else
					cr.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.Description = null;
				else
					cr.Description = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SubProcessID'");
				else
					cr.SubProcessID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'System'");
				else
					cr.System = dr.GetString(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'Platform'");
				else
					cr.Platform = dr.GetString(5);
				if (dr.IsDBNull(6))
					cr.RoleActivity = null;
				else
					cr.RoleActivity = dr.GetString(6);
				if (dr.IsDBNull(7))
					cr.RoleType = null;
				else
					cr.RoleType = dr.GetString(7);
				if (dr.IsDBNull(8))
					cr.Comment = null;
				else
					cr.Comment = dr.GetString(8);
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
