using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.412 (#453)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.ISAPauthObj
{
	/// <summary>
	/// Return value from method GetSAPauthObj
	/// </summary>
	public struct returnGetSAPauthObj
	{
		public int ID;
		public string Name;
		public string Description;
		public int SAPauthClassID;
		public string Status;
	}
	/// <summary>
	/// Return value from method ListSAPauthObj
	/// </summary>
	public struct returnListSAPauthObj
	{
		public int ID;
		public string Name;
		public string Description;
		public int SAPauthClassID;
		public string Status;
	}
	/// <summary>
	/// Return value from method ListSAPauthObjDictionaryBySAPauthClassDictionary
	/// </summary>
	public struct returnListSAPauthObjDictionaryBySAPauthClassDictionary
	{
		public int ID;
		public string Name;
		public string Description;
		public int SAPauthClassID;
		public string Status;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_SAPauthObj
	/// </summary>
	public class ISAPauthObj
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
		public ISAPauthObj() : this((OdbcConnection)null) { }
		public ISAPauthObj(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ISAPauthObj(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_SAPauthObj.
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="SAPauthClassID"></param>
		/// <param name="Status"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewSAPauthObj(string Name, string Description, int SAPauthClassID, string Status)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_SAPauthObj\"(\"c_u_Name\",\"c_u_Description\",\"c_r_SAPauthClass\",\"c_u_Status\") VALUES(?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 30);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			cmd.Parameters.Add("c_u_Description", OdbcType.NText);
			cmd.Parameters["c_u_Description"].Value = (object)Description;
			cmd.Parameters.Add("c_r_SAPauthClass", OdbcType.Int);
			cmd.Parameters["c_r_SAPauthClass"].Value = (object)SAPauthClassID;
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
		/// delete a row from table t_RBSR_AUFW_u_SAPauthObj.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteSAPauthObj(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_SAPauthObj\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_SAPauthObj.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPauthObj</returns>
		public returnGetSAPauthObj GetSAPauthObj(int ID)
		{
			returnGetSAPauthObj rv = new returnGetSAPauthObj();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_Name\",\"c_u_Description\",\"c_r_SAPauthClass\",\"c_u_Status\" from \"t_RBSR_AUFW_u_SAPauthObj\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'Description'");
				else
					rv.Description = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SAPauthClassID'");
				else
					rv.SAPauthClassID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'Status'");
				else
					rv.Status = dr.GetString(4);
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
		/// update a row in table t_RBSR_AUFW_u_SAPauthObj.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="SAPauthClassID"></param>
		/// <param name="Status"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetSAPauthObj(int ID, string Name, string Description, int SAPauthClassID, string Status)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_SAPauthObj\" set \"c_u_Name\"=?,\"c_u_Description\"=?,\"c_r_SAPauthClass\"=?,\"c_u_Status\"=? where \"c_id\" = ?";
			if (Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 30);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			cmd.Parameters.Add("c_u_Description", OdbcType.NText);
			cmd.Parameters["c_u_Description"].Value = (object)Description;
			cmd.Parameters.Add("c_r_SAPauthClass", OdbcType.Int);
			cmd.Parameters["c_r_SAPauthClass"].Value = (object)SAPauthClassID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPauthObj.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListSAPauthObj[]</returns>
		public returnListSAPauthObj[] ListSAPauthObj(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListSAPauthObj[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"sapauthclass\"", "\"c_r_SAPauthClass\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"status\"", "\"c_u_Status\"").Replace("\"sapauthclass\"", "\"c_r_SAPauthClass\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SAPauthClass\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_SAPauthObj\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SAPauthClass\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_SAPauthObj\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SAPauthClass\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_SAPauthObj\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPauthObj> rvl = new List<returnListSAPauthObj>();
			while (dr.Read())
			{
				returnListSAPauthObj cr = new returnListSAPauthObj();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'Name'");
				else
					cr.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'Description'");
				else
					cr.Description = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SAPauthClassID'");
				else
					cr.SAPauthClassID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'Status'");
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPauthObj.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SAPauthClassID"></param>
		/// <returns>returnListSAPauthObjDictionaryBySAPauthClassDictionary[]</returns>
		public returnListSAPauthObjDictionaryBySAPauthClassDictionary[] ListSAPauthObjDictionaryBySAPauthClassDictionary(int? maxRowsToReturn, int SAPauthClassID)
		{
			returnListSAPauthObjDictionaryBySAPauthClassDictionary[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SAPauthClass\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_SAPauthObj\" WHERE \"SAPauthObj\".\"@id\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SAPauthClass\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_SAPauthObj\" WHERE \"SAPauthObj\".\"@id\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SAPauthClass\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_SAPauthObj\" WHERE \"SAPauthObj\".\"@id\"=?";
			cmd.Parameters.Add("1_SAPauthClassID", OdbcType.Int);
			cmd.Parameters["1_SAPauthClassID"].Value = (object)SAPauthClassID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPauthObjDictionaryBySAPauthClassDictionary> rvl = new List<returnListSAPauthObjDictionaryBySAPauthClassDictionary>();
			while (dr.Read())
			{
				returnListSAPauthObjDictionaryBySAPauthClassDictionary cr = new returnListSAPauthObjDictionaryBySAPauthClassDictionary();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'Name'");
				else
					cr.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'Description'");
				else
					cr.Description = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'SAPauthClassID'");
				else
					cr.SAPauthClassID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'Status'");
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
	}
}
