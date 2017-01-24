using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;
using _6MAR_WebApplication;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.241 (#282)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IAdditionalBusRole
{
	/// <summary>
	/// Return value from method GetAdditionalBusRole
	/// </summary>
	public struct returnGetAdditionalBusRole
	{
		public int ID;
		public int idAdditionalBusRole;
		public int BusRoleID;
		public string Comment;
		public string RecertificationInterval;
		public DateTime? RecertificationStartDate;
		public DateTime? ExpirationDate;
	}
	/// <summary>
	/// Return value from method ListAdditionalBusRole
	/// </summary>
	public struct returnListAdditionalBusRole
	{
		public int ID;
		public int idAdditionalBusRole;
		public int BusRoleID;
		public string Comment;
		public string RecertificationInterval;
		public DateTime? RecertificationStartDate;
		public DateTime? ExpirationDate;
	}
	/// <summary>
	/// Return value from method ListAdditionalBusRoleByBusRole
	/// </summary>
	public struct returnListAdditionalBusRoleByBusRole
	{
		public int ID;
		public int idAdditionalBusRole;
		public int BusRoleID;
		public string Comment;
		public string RecertificationInterval;
		public DateTime? RecertificationStartDate;
		public DateTime? ExpirationDate;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_AdditionalBusRole
	/// </summary>
	public class IAdditionalBusRole
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
		public IAdditionalBusRole() : this((OdbcConnection)null) { }
		public IAdditionalBusRole(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IAdditionalBusRole(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_AdditionalBusRole.
		/// </summary>
		/// <param name="idAdditionalBusRole"></param>
		/// <param name="BusRoleID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewAdditionalBusRole(int idAdditionalBusRole, int BusRoleID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_AdditionalBusRole\"(\"c_u_idAdditionalBusRole\",\"c_r_BusRole\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_u_idAdditionalBusRole", OdbcType.Int);
			cmd.Parameters["c_u_idAdditionalBusRole"].Value = (object)idAdditionalBusRole;
			cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
			cmd.Parameters["c_r_BusRole"].Value = (object)BusRoleID;
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
		/// delete a row from table t_RBSR_AUFW_u_AdditionalBusRole.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteAdditionalBusRole(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_AdditionalBusRole\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_AdditionalBusRole.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetAdditionalBusRole</returns>
		public returnGetAdditionalBusRole GetAdditionalBusRole(int ID)
		{
			returnGetAdditionalBusRole rv = new returnGetAdditionalBusRole();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_idAdditionalBusRole\",\"c_r_BusRole\",\"c_u_Comment\",\"c_u_RecertificationInterval\",\"c_u_RecertificationStartDate\",\"c_u_ExpirationDate\" from \"t_RBSR_AUFW_u_AdditionalBusRole\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'idAdditionalBusRole'"); } 
				else
					rv.idAdditionalBusRole = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					rv.BusRoleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					rv.Comment = null;
				else
					rv.Comment = dr.GetString(3);
				if (dr.IsDBNull(4))
					rv.RecertificationInterval = null;
				else
					rv.RecertificationInterval = dr.GetString(4);
				if (dr.IsDBNull(5))
					rv.RecertificationStartDate = null;
				else
					rv.RecertificationStartDate = dr.GetDateTime(5);
				if (dr.IsDBNull(6))
					rv.ExpirationDate = null;
				else
					rv.ExpirationDate = dr.GetDateTime(6);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_AdditionalBusRole.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="idAdditionalBusRole"></param>
		/// <param name="BusRoleID"></param>
		/// <param name="Comment"></param>
		/// <param name="RecertificationInterval"></param>
		/// <param name="RecertificationStartDate"></param>
		/// <param name="ExpirationDate"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetAdditionalBusRole(int ID, int idAdditionalBusRole, int BusRoleID, string Comment, string RecertificationInterval, DateTime? RecertificationStartDate, DateTime? ExpirationDate)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_AdditionalBusRole\" set \"c_u_idAdditionalBusRole\"=?,\"c_r_BusRole\"=?,\"c_u_Comment\"=?,\"c_u_RecertificationInterval\"=?,\"c_u_RecertificationStartDate\"=?,\"c_u_ExpirationDate\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_u_idAdditionalBusRole", OdbcType.Int);
			cmd.Parameters["c_u_idAdditionalBusRole"].Value = (object)idAdditionalBusRole;
			cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
			cmd.Parameters["c_r_BusRole"].Value = (object)BusRoleID;
			cmd.Parameters.Add("c_u_Comment", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_Comment"].Value = (Comment != null ? (object)Comment : DBNull.Value);
			cmd.Parameters.Add("c_u_RecertificationInterval", OdbcType.NVarChar, 15);
			cmd.Parameters["c_u_RecertificationInterval"].Value = (RecertificationInterval != null ? (object)RecertificationInterval : DBNull.Value);
			cmd.Parameters.Add("c_u_RecertificationStartDate", OdbcType.DateTime);
			cmd.Parameters["c_u_RecertificationStartDate"].Value = ((RecertificationStartDate.HasValue) ? HELPERS.SetSafeDBDate(RecertificationStartDate.Value) : DBNull.Value);
			cmd.Parameters.Add("c_u_ExpirationDate", OdbcType.DateTime);
			cmd.Parameters["c_u_ExpirationDate"].Value = (ExpirationDate.HasValue ? HELPERS.SetSafeDBDate(ExpirationDate.Value) : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_AdditionalBusRole.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListAdditionalBusRole[]</returns>
		public returnListAdditionalBusRole[] ListAdditionalBusRole(int? maxRowsToReturn)
		{
			returnListAdditionalBusRole[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_idAdditionalBusRole\", \"c_r_BusRole\", \"c_u_Comment\", \"c_u_RecertificationInterval\", \"c_u_RecertificationStartDate\", \"c_u_ExpirationDate\" FROM \"t_RBSR_AUFW_u_AdditionalBusRole\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_idAdditionalBusRole\", \"c_r_BusRole\", \"c_u_Comment\", \"c_u_RecertificationInterval\", \"c_u_RecertificationStartDate\", \"c_u_ExpirationDate\" FROM \"t_RBSR_AUFW_u_AdditionalBusRole\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_idAdditionalBusRole\", \"c_r_BusRole\", \"c_u_Comment\", \"c_u_RecertificationInterval\", \"c_u_RecertificationStartDate\", \"c_u_ExpirationDate\" FROM \"t_RBSR_AUFW_u_AdditionalBusRole\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListAdditionalBusRole> rvl = new List<returnListAdditionalBusRole>();
			while (dr.Read())
			{
				returnListAdditionalBusRole cr = new returnListAdditionalBusRole();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'idAdditionalBusRole'"); } 
				else
					cr.idAdditionalBusRole = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					cr.BusRoleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					cr.Comment = null;
				else
					cr.Comment = dr.GetString(3);
				if (dr.IsDBNull(4))
					cr.RecertificationInterval = null;
				else
					cr.RecertificationInterval = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.RecertificationStartDate = null;
				else
					cr.RecertificationStartDate = dr.GetDateTime(5);
				if (dr.IsDBNull(6))
					cr.ExpirationDate = null;
				else
					cr.ExpirationDate = dr.GetDateTime(6);
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
		/// select a set of rows from table t_RBSR_AUFW_u_AdditionalBusRole.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="BusRoleID"></param>
		/// <returns>returnListAdditionalBusRoleByBusRole[]</returns>
		public returnListAdditionalBusRoleByBusRole[] ListAdditionalBusRoleByBusRole(int? maxRowsToReturn, int BusRoleID)
		{
			returnListAdditionalBusRoleByBusRole[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_idAdditionalBusRole\", \"c_r_BusRole\", \"c_u_Comment\", \"c_u_RecertificationInterval\", \"c_u_RecertificationStartDate\", \"c_u_ExpirationDate\" FROM \"t_RBSR_AUFW_u_AdditionalBusRole\" WHERE \"c_r_BusRole\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_idAdditionalBusRole\", \"c_r_BusRole\", \"c_u_Comment\", \"c_u_RecertificationInterval\", \"c_u_RecertificationStartDate\", \"c_u_ExpirationDate\" FROM \"t_RBSR_AUFW_u_AdditionalBusRole\" WHERE \"c_r_BusRole\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_idAdditionalBusRole\", \"c_r_BusRole\", \"c_u_Comment\", \"c_u_RecertificationInterval\", \"c_u_RecertificationStartDate\", \"c_u_ExpirationDate\" FROM \"t_RBSR_AUFW_u_AdditionalBusRole\" WHERE \"c_r_BusRole\"=?";
			cmd.Parameters.Add("1_BusRoleID", OdbcType.Int);
			cmd.Parameters["1_BusRoleID"].Value = (object)BusRoleID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListAdditionalBusRoleByBusRole> rvl = new List<returnListAdditionalBusRoleByBusRole>();
			while (dr.Read())
			{
				returnListAdditionalBusRoleByBusRole cr = new returnListAdditionalBusRoleByBusRole();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'idAdditionalBusRole'"); } 
				else
					cr.idAdditionalBusRole = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					cr.BusRoleID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					cr.Comment = null;
				else
					cr.Comment = dr.GetString(3);
				if (dr.IsDBNull(4))
					cr.RecertificationInterval = null;
				else
					cr.RecertificationInterval = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.RecertificationStartDate = null;
				else
					cr.RecertificationStartDate = dr.GetDateTime(5);
				if (dr.IsDBNull(6))
					cr.ExpirationDate = null;
				else
					cr.ExpirationDate = dr.GetDateTime(6);
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
