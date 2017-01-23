using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.184 (#225)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IUser
{
	/// <summary>
	/// Return value from method GetUser
	/// </summary>
	public struct returnGetUser
	{
		public int ID;
		public string EID;
		public string Name;
		public string PrivilegeLevel;
		public string NameSurname;
		public string NameFirst;
	}
	/// <summary>
	/// Return value from method ListUser
	/// </summary>
	public struct returnListUser
	{
		public int ID;
		public string EID;
		public string Name;
		public string PrivilegeLevel;
		public string NameSurname;
		public string NameFirst;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_User
	/// </summary>
	public class IUser
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
		public IUser() : this((OdbcConnection)null) { }
		public IUser(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IUser(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="EID"></param>
		/// <param name="Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewUser(string EID, string Name)
		{

            // DFSklar added on 02-Oct-2010:
            EID = EID.ToUpper().Trim();

			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_User\"(\"c_u_EID\",\"c_u_Name\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (EID == null)  { cmd.Dispose(); DBClose(); throw new Exception("EID must not be null!"); } 
			cmd.Parameters.Add("c_u_EID", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_EID"].Value = (EID != null ? (object)EID : DBNull.Value);
			if (Name == null)  { cmd.Dispose(); DBClose(); throw new Exception("Name must not be null!"); } 
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteUser(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_User\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUser</returns>
		public returnGetUser GetUser(int ID)
		{
			returnGetUser rv = new returnGetUser();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_EID\",\"c_u_Name\",\"c_u_PrivilegeLevel\",\"c_u_NameSurname\",\"c_u_NameFirst\" from \"t_RBSR_AUFW_u_User\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EID'"); } 
				else
					rv.EID = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Name'"); } 
				else
					rv.Name = dr.GetString(2);
				if (dr.IsDBNull(3))
					rv.PrivilegeLevel = null;
				else
					rv.PrivilegeLevel = dr.GetString(3);
				if (dr.IsDBNull(4))
					rv.NameSurname = null;
				else
					rv.NameSurname = dr.GetString(4);
				if (dr.IsDBNull(5))
					rv.NameFirst = null;
				else
					rv.NameFirst = dr.GetString(5);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EID"></param>
		/// <param name="Name"></param>
		/// <param name="PrivilegeLevel"></param>
		/// <param name="NameSurname"></param>
		/// <param name="NameFirst"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetUser(int ID, string EID, string Name, string PrivilegeLevel, string NameSurname, string NameFirst)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_User\" set \"c_u_EID\"=?,\"c_u_Name\"=?,\"c_u_PrivilegeLevel\"=?,\"c_u_NameSurname\"=?,\"c_u_NameFirst\"=? where \"c_id\" = ?";
			if (EID == null)  { cmd.Dispose(); DBClose(); throw new Exception("EID must not be null!"); } 
			cmd.Parameters.Add("c_u_EID", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_EID"].Value = (EID != null ? (object)EID : DBNull.Value);
			if (Name == null)  { cmd.Dispose(); DBClose(); throw new Exception("Name must not be null!"); } 
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			cmd.Parameters.Add("c_u_PrivilegeLevel", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_PrivilegeLevel"].Value = (PrivilegeLevel != null ? (object)PrivilegeLevel : DBNull.Value);
			cmd.Parameters.Add("c_u_NameSurname", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_NameSurname"].Value = (NameSurname != null ? (object)NameSurname : DBNull.Value);
			cmd.Parameters.Add("c_u_NameFirst", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_NameFirst"].Value = (NameFirst != null ? (object)NameFirst : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListUser[]</returns>
		public returnListUser[] ListUser(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListUser[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"eid\"", "\"c_u_EID\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"privilegelevel\"", "\"c_u_PrivilegeLevel\"").Replace("\"namesurname\"", "\"c_u_NameSurname\"").Replace("\"namefirst\"", "\"c_u_NameFirst\"").Replace("\"password\"", "\"c_u_Password\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"eid\"", "\"c_u_EID\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"privilegelevel\"", "\"c_u_PrivilegeLevel\"").Replace("\"namesurname\"", "\"c_u_NameSurname\"").Replace("\"namefirst\"", "\"c_u_NameFirst\"").Replace("\"password\"", "\"c_u_Password\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_EID\", \"c_u_Name\", \"c_u_PrivilegeLevel\", \"c_u_NameSurname\", \"c_u_NameFirst\" FROM \"t_RBSR_AUFW_u_User\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Name\", \"c_u_PrivilegeLevel\", \"c_u_NameSurname\", \"c_u_NameFirst\" FROM \"t_RBSR_AUFW_u_User\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Name\", \"c_u_PrivilegeLevel\", \"c_u_NameSurname\", \"c_u_NameFirst\" FROM \"t_RBSR_AUFW_u_User\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListUser> rvl = new List<returnListUser>();
			while (dr.Read())
			{
				returnListUser cr = new returnListUser();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EID'"); } 
				else
					cr.EID = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Name'"); } 
				else
					cr.Name = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.PrivilegeLevel = null;
				else
					cr.PrivilegeLevel = dr.GetString(3);
				if (dr.IsDBNull(4))
					cr.NameSurname = null;
				else
					cr.NameSurname = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.NameFirst = null;
				else
					cr.NameFirst = dr.GetString(5);
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
