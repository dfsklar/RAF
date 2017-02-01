using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.285 (#326)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.ISAPRoleOwner
{
	/// <summary>
	/// Return value from method GetSAPRoleOwner
	/// </summary>
	public struct returnGetSAPRoleOwner
	{
		public int ID;
		public string EID;
		public string Geography;
		public string Rank;
		public int SAProleID;
	}
	/// <summary>
	/// Return value from method ListSAPRoleOwner
	/// </summary>
	public struct returnListSAPRoleOwner
	{
		public int ID;
		public string EID;
		public string Geography;
		public string Rank;
		public int SAProleID;
	}
	/// <summary>
	/// Return value from method ListSAPRoleOwnerBySAProle
	/// </summary>
	public struct returnListSAPRoleOwnerBySAProle
	{
		public int ID;
		public string EID;
		public string Geography;
		public string Rank;
		public int SAProleID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_SAPRoleOwner
	/// </summary>
	public class ISAPRoleOwner : _6MAR_WebApplication.RISEBASE
	{
		public ISAPRoleOwner() : this((OdbcConnection)null) { }
		public ISAPRoleOwner(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ISAPRoleOwner(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_SAPRoleOwner.
		/// </summary>
		/// <param name="EID"></param>
		/// <param name="Geography"></param>
		/// <param name="Rank"></param>
		/// <param name="SAProleID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewSAPRoleOwner(string EID, string Geography, string Rank, int SAProleID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_SAPRoleOwner\"(\"c_u_EID\",\"c_u_Geography\",\"c_u_Rank\",\"c_r_SAProle\") VALUES(?,?,?,?)";
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
			cmd.Parameters.Add("c_r_SAProle", OdbcType.Int);
			cmd.Parameters["c_r_SAProle"].Value = (object)SAProleID;
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
		/// delete a row from table t_RBSR_AUFW_u_SAPRoleOwner.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteSAPRoleOwner(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_SAPRoleOwner\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_SAPRoleOwner.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPRoleOwner</returns>
		public returnGetSAPRoleOwner GetSAPRoleOwner(int ID)
		{
			returnGetSAPRoleOwner rv = new returnGetSAPRoleOwner();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_EID\",\"c_u_Geography\",\"c_u_Rank\",\"c_r_SAProle\" from \"t_RBSR_AUFW_u_SAPRoleOwner\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAProleID'"); } 
				else
					rv.SAProleID = dr.GetInt32(4);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_SAPRoleOwner.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EID"></param>
		/// <param name="Geography"></param>
		/// <param name="Rank"></param>
		/// <param name="SAProleID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetSAPRoleOwner(int ID, string EID, string Geography, string Rank, int SAProleID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_SAPRoleOwner\" set \"c_u_EID\"=?,\"c_u_Geography\"=?,\"c_u_Rank\"=?,\"c_r_SAProle\"=? where \"c_id\" = ?";
			if (EID == null)  { cmd.Dispose(); DBClose(); throw new Exception("EID must not be null!"); } 
			cmd.Parameters.Add("c_u_EID", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_EID"].Value = (EID != null ? (object)EID : DBNull.Value);
			if (Geography == null)  { cmd.Dispose(); DBClose(); throw new Exception("Geography must not be null!"); } 
			cmd.Parameters.Add("c_u_Geography", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Geography"].Value = (Geography != null ? (object)Geography : DBNull.Value);
			if (Rank == null)  { cmd.Dispose(); DBClose(); throw new Exception("Rank must not be null!"); } 
			cmd.Parameters.Add("c_u_Rank", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Rank"].Value = (Rank != null ? (object)Rank : DBNull.Value);
			cmd.Parameters.Add("c_r_SAProle", OdbcType.Int);
			cmd.Parameters["c_r_SAProle"].Value = (object)SAProleID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPRoleOwner.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListSAPRoleOwner[]</returns>
		public returnListSAPRoleOwner[] ListSAPRoleOwner(int? maxRowsToReturn)
		{
			returnListSAPRoleOwner[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_SAProle\" FROM \"t_RBSR_AUFW_u_SAPRoleOwner\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_SAProle\" FROM \"t_RBSR_AUFW_u_SAPRoleOwner\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_SAProle\" FROM \"t_RBSR_AUFW_u_SAPRoleOwner\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPRoleOwner> rvl = new List<returnListSAPRoleOwner>();
			while (dr.Read())
			{
				returnListSAPRoleOwner cr = new returnListSAPRoleOwner();
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAProleID'"); } 
				else
					cr.SAProleID = dr.GetInt32(4);
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPRoleOwner.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SAProleID"></param>
		/// <returns>returnListSAPRoleOwnerBySAProle[]</returns>
		public returnListSAPRoleOwnerBySAProle[] ListSAPRoleOwnerBySAProle(int? maxRowsToReturn, int SAProleID)
		{
			returnListSAPRoleOwnerBySAProle[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_SAProle\" FROM \"t_RBSR_AUFW_u_SAPRoleOwner\" WHERE \"c_r_SAProle\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_SAProle\" FROM \"t_RBSR_AUFW_u_SAPRoleOwner\" WHERE \"c_r_SAProle\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_EID\", \"c_u_Geography\", \"c_u_Rank\", \"c_r_SAProle\" FROM \"t_RBSR_AUFW_u_SAPRoleOwner\" WHERE \"c_r_SAProle\"=?";
			cmd.Parameters.Add("1_SAProleID", OdbcType.Int);
			cmd.Parameters["1_SAProleID"].Value = (object)SAProleID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPRoleOwnerBySAProle> rvl = new List<returnListSAPRoleOwnerBySAProle>();
			while (dr.Read())
			{
				returnListSAPRoleOwnerBySAProle cr = new returnListSAPRoleOwnerBySAProle();
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAProleID'"); } 
				else
					cr.SAProleID = dr.GetInt32(4);
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
