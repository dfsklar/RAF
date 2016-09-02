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

namespace RBSR_AUFW.DB.ISAPauthField
{
	/// <summary>
	/// Return value from method GetSAPauthField
	/// </summary>
	public struct returnGetSAPauthField
	{
		public int ID;
		public string Name;
		public string Description;
		public string Status;
	}
	/// <summary>
	/// Return value from method ListSAPauthField
	/// </summary>
	public struct returnListSAPauthField
	{
		public int ID;
		public string Name;
		public string Description;
		public string Status;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_SAPauthField
	/// </summary>
	public class ISAPauthField
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
		public ISAPauthField() : this((OdbcConnection)null) { }
		public ISAPauthField(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ISAPauthField(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_SAPauthField.
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="Status"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewSAPauthField(string Name, string Status)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_SAPauthField\"(\"c_u_Name\",\"c_u_Status\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			if (Status == null) throw new Exception("Status must not be null!");
			cmd.Parameters.Add("c_u_Status", OdbcType.NVarChar, 1);
			cmd.Parameters["c_u_Status"].Value = (Status != null ? (object)Status : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_SAPauthField.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteSAPauthField(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_SAPauthField\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_SAPauthField.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPauthField</returns>
		public returnGetSAPauthField GetSAPauthField(int ID)
		{
			returnGetSAPauthField rv = new returnGetSAPauthField();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_Name\",\"c_u_Description\",\"c_u_Status\" from \"t_RBSR_AUFW_u_SAPauthField\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'Status'");
				else
					rv.Status = dr.GetString(3);
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
		/// update a row in table t_RBSR_AUFW_u_SAPauthField.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="Status"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetSAPauthField(int ID, string Name, string Description, string Status)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_SAPauthField\" set \"c_u_Name\"=?,\"c_u_Description\"=?,\"c_u_Status\"=? where \"c_id\" = ?";
			if (Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			cmd.Parameters.Add("c_u_Description", OdbcType.NText);
			cmd.Parameters["c_u_Description"].Value = (Description != null ? (object)Description : DBNull.Value);
			if (Status == null) throw new Exception("Status must not be null!");
			cmd.Parameters.Add("c_u_Status", OdbcType.NVarChar, 1);
			cmd.Parameters["c_u_Status"].Value = (Status != null ? (object)Status : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPauthField.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListSAPauthField[]</returns>
		public returnListSAPauthField[] ListSAPauthField(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListSAPauthField[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"status\"", "\"c_u_Status\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"status\"", "\"c_u_Status\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_SAPauthField\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_SAPauthField\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_SAPauthField\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPauthField> rvl = new List<returnListSAPauthField>();
			while (dr.Read())
			{
				returnListSAPauthField cr = new returnListSAPauthField();
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
					throw new Exception("Value 'null' is not allowed for 'Status'");
				else
					cr.Status = dr.GetString(3);
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
