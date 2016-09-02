using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.253 (#294)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IBusRoleOwner
{
	/// <summary>
	/// Return value from method GetBusRoleOwner
	/// </summary>
	public struct returnGetBusRoleOwner
	{
		public int ID;
		public string EID;
		public string Geography;
		public string Rank;
        public string RankFriendly;
        public int BusRoleID;
	}
	/// <summary>
	/// Return value from method ListBusRoleOwner
	/// </summary>
	public struct returnListBusRoleOwner
	{
		public int ID;
		public string EID;
		public string Geography;
		public string Rank;
        public string RankFriendly;
		public int BusRoleID;
	}
	/// <summary>
	/// Return value from method ListBusRoleOwnerByBusRole
	/// </summary>
	public struct returnListBusRoleOwnerByBusRole
	{
		public int ID;
		public string EID;
		public string Geography;
		public string Rank;
        public string RankFriendly;
        public int BusRoleID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_BusRoleOwner
	/// </summary>
	public class IBusRoleOwner
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
		public IBusRoleOwner() : this((OdbcConnection)null) { }
		public IBusRoleOwner(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IBusRoleOwner(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_BusRoleOwner.
		/// </summary>
		/// <param name="EID"></param>
		/// <param name="Geography"></param>
		/// <param name="Rank"></param>
		/// <param name="BusRoleID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewBusRoleOwner(string EID, string Geography, string Rank, int BusRoleID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_BusRoleOwner\"(\"c_u_EID\",\"c_u_Geography\",\"c_u_Rank\",\"c_r_BusRole\") VALUES(?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (EID == null)  { cmd.Dispose(); DBClose(); throw new Exception("EID must not be null!"); } 
			cmd.Parameters.Add("c_u_EID", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_EID"].Value = (EID != null ? (object)EID : DBNull.Value);
			if (Geography == null)  { cmd.Dispose(); DBClose(); throw new Exception("Geography must not be null!"); } 
			cmd.Parameters.Add("c_u_Geography", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Geography"].Value = (Geography != null ? (object)Geography : DBNull.Value);
			if (Rank == null)  { cmd.Dispose(); DBClose(); throw new Exception("Rank must not be null!"); } 
			cmd.Parameters.Add("c_u_Rank", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Rank"].Value = (Rank != null ? (object)Rank : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_BusRoleOwner.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteBusRoleOwner(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_BusRoleOwner\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_BusRoleOwner.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetBusRoleOwner</returns>
		public returnGetBusRoleOwner GetBusRoleOwner(int ID)
		{
			returnGetBusRoleOwner rv = new returnGetBusRoleOwner();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_EID\",\"c_u_Geography\",\"c_u_Rank\",\"c_r_BusRole\", (SELECT ForDisplay FROM DICT_RoleOwnerType where Abbrev=c_u_Rank) as Friendly from \"t_RBSR_AUFW_u_BusRoleOwner\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Geography'"); } 
				else
					rv.Geography = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Rank'"); } 
				else
					rv.Rank = dr.GetString(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					rv.BusRoleID = dr.GetInt32(4);
                rv.RankFriendly = dr.GetString(5);
            }
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_BusRoleOwner.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EID"></param>
		/// <param name="Geography"></param>
		/// <param name="Rank"></param>
		/// <param name="BusRoleID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetBusRoleOwner(int ID, string EID, string Geography, string Rank, int BusRoleID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_BusRoleOwner\" set \"c_u_EID\"=?,\"c_u_Geography\"=?,\"c_u_Rank\"=?,\"c_r_BusRole\"=? where \"c_id\" = ?";
			if (EID == null)  { cmd.Dispose(); DBClose(); throw new Exception("EID must not be null!"); } 
			cmd.Parameters.Add("c_u_EID", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_EID"].Value = (EID != null ? (object)EID : DBNull.Value);
			if (Geography == null)  { cmd.Dispose(); DBClose(); throw new Exception("Geography must not be null!"); } 
			cmd.Parameters.Add("c_u_Geography", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Geography"].Value = (Geography != null ? (object)Geography : DBNull.Value);
			if (Rank == null)  { cmd.Dispose(); DBClose(); throw new Exception("Rank must not be null!"); } 
			cmd.Parameters.Add("c_u_Rank", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Rank"].Value = (Rank != null ? (object)Rank : DBNull.Value);
			cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
			cmd.Parameters["c_r_BusRole"].Value = (object)BusRoleID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_BusRoleOwner.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListBusRoleOwner[]</returns>
		public returnListBusRoleOwner[] ListBusRoleOwner(int? maxRowsToReturn)
		{
			returnListBusRoleOwner[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
                    cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_BusRole\",  (SELECT ForDisplay FROM DICT_RoleOwnerType where Abbrev=c_u_Rank) as Friendly FROM \"t_RBSR_AUFW_u_BusRoleOwner\"";
				else
                    cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_BusRole\",  (SELECT ForDisplay FROM DICT_RoleOwnerType where Abbrev=c_u_Rank) as Friendly FROM \"t_RBSR_AUFW_u_BusRoleOwner\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
                cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_BusRole\",  (SELECT ForDisplay FROM DICT_RoleOwnerType where Abbrev=c_u_Rank) as Friendly FROM \"t_RBSR_AUFW_u_BusRoleOwner\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListBusRoleOwner> rvl = new List<returnListBusRoleOwner>();
			while (dr.Read())
			{
				returnListBusRoleOwner cr = new returnListBusRoleOwner();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EID'"); } 
				else
					cr.EID = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Geography'"); } 
				else
					cr.Geography = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Rank'"); } 
				else
					cr.Rank = dr.GetString(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					cr.BusRoleID = dr.GetInt32(4);
                cr.RankFriendly = dr.GetString(5);
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
		/// select a set of rows from table t_RBSR_AUFW_u_BusRoleOwner.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="BusRoleID"></param>
		/// <returns>returnListBusRoleOwnerByBusRole[]</returns>
		public returnListBusRoleOwnerByBusRole[] ListBusRoleOwnerByBusRole(int? maxRowsToReturn, int BusRoleID)
		{
			returnListBusRoleOwnerByBusRole[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
                    cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_BusRole\",  (SELECT ForDisplay FROM DICT_RoleOwnerType where Abbrev=c_u_Rank) as Friendly, (SELECT Sequence FROM DICT_RoleOwnerType where Abbrev=c_u_Rank) as Sequence FROM \"t_RBSR_AUFW_u_BusRoleOwner\" WHERE \"c_r_BusRole\"=? ORDER BY Sequence";
				else
                    cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_BusRole\",  (SELECT ForDisplay FROM DICT_RoleOwnerType where Abbrev=c_u_Rank) as Friendly, (SELECT Sequence FROM DICT_RoleOwnerType where Abbrev=c_u_Rank) as Sequence FROM \"t_RBSR_AUFW_u_BusRoleOwner\" WHERE \"c_r_BusRole\"=?" + " ORDER BY Sequence LIMIT " + maxRowsToReturn.Value;
			}
			else
                cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_BusRole\",  (SELECT ForDisplay FROM DICT_RoleOwnerType where Abbrev=c_u_Rank) as Friendly FROM \"t_RBSR_AUFW_u_BusRoleOwner\" WHERE \"c_r_BusRole\"=?";
			cmd.Parameters.Add("1_BusRoleID", OdbcType.Int);
			cmd.Parameters["1_BusRoleID"].Value = (object)BusRoleID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListBusRoleOwnerByBusRole> rvl = new List<returnListBusRoleOwnerByBusRole>();
			while (dr.Read())
			{
				returnListBusRoleOwnerByBusRole cr = new returnListBusRoleOwnerByBusRole();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EID'"); } 
				else
					cr.EID = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Geography'"); } 
				else
					cr.Geography = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Rank'"); } 
				else
					cr.Rank = dr.GetString(3);
				if (dr.IsDBNull(4))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'BusRoleID'"); } 
				else
					cr.BusRoleID = dr.GetInt32(4);
                cr.RankFriendly = dr.GetString(5);
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
