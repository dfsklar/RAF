using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.287 (#328)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.ISAPsecurityOrg
{
	/// <summary>
	/// Return value from method GetSAPsecurityOrg
	/// </summary>
	public struct returnGetSAPsecurityOrg
	{
		public int ID;
		public string Name;
		public int SAPsecurityOrgAxisID;
	}
	/// <summary>
	/// Return value from method ListSAPsecurityOrg
	/// </summary>
	public struct returnListSAPsecurityOrg
	{
		public int ID;
		public string Name;
		public int SAPsecurityOrgAxisID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_SAPsecurityOrg
	/// </summary>
	public class ISAPsecurityOrg : _6MAR_WebApplication.RISEBASE
	{
		public ISAPsecurityOrg() : this((OdbcConnection)null) { }
		public ISAPsecurityOrg(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ISAPsecurityOrg(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="SAPsecurityOrgAxisID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewSAPsecurityOrg(string Name, int SAPsecurityOrgAxisID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_SAPsecurityOrg\"(\"c_u_Name\",\"c_r_SAPsecurityOrgAxis\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			cmd.Parameters.Add("c_r_SAPsecurityOrgAxis", OdbcType.Int);
			cmd.Parameters["c_r_SAPsecurityOrgAxis"].Value = (object)SAPsecurityOrgAxisID;
			OdbcDataReader dri = cmd.ExecuteReader();
			if (_dbConnection.Driver.ToLower().StartsWith("myodbc"))
			{
				cmd = _dbConnection.CreateCommand();
				cmd.CommandText = "SELECT LAST_INSERT_ID()";
				dri = cmd.ExecuteReader();
			}
			dri.Read();
			rv = (dri.IsDBNull(0) ? 0 : (typeof(long).Equals(dri.GetFieldType(0)) ? (int)dri.GetInt64(0) : dri.GetInt32(0)));
			dri.Close();
			if (rv == 0) throw new Exception("Insert operation failed!");
			dri.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// delete a row from table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteSAPsecurityOrg(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_SAPsecurityOrg\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPsecurityOrg</returns>
		public returnGetSAPsecurityOrg GetSAPsecurityOrg(int ID)
		{
			returnGetSAPsecurityOrg rv = new returnGetSAPsecurityOrg();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_Name\",\"c_r_SAPsecurityOrgAxis\" from \"t_RBSR_AUFW_u_SAPsecurityOrg\" where \"c_id\"= ?";
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					rv.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'Name'");
				else
					rv.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgAxisID'");
				else
					rv.SAPsecurityOrgAxisID = dr.GetInt32(2);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <param name="SAPsecurityOrgAxisID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetSAPsecurityOrg(int ID, string Name, int SAPsecurityOrgAxisID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_SAPsecurityOrg\" set \"c_u_Name\"=?,\"c_r_SAPsecurityOrgAxis\"=? where \"c_id\" = ?";
			if (Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			cmd.Parameters.Add("c_r_SAPsecurityOrgAxis", OdbcType.Int);
			cmd.Parameters["c_r_SAPsecurityOrgAxis"].Value = (object)SAPsecurityOrgAxisID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrg.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListSAPsecurityOrg[]</returns>
		public returnListSAPsecurityOrg[] ListSAPsecurityOrg(int? maxRowsToReturn)
		{
			returnListSAPsecurityOrg[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Name\", \"c_r_SAPsecurityOrgAxis\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrg\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_r_SAPsecurityOrgAxis\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrg\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_r_SAPsecurityOrgAxis\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrg\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPsecurityOrg> rvl = new List<returnListSAPsecurityOrg>();
			while (dr.Read())
			{
				returnListSAPsecurityOrg cr = new returnListSAPsecurityOrg();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'Name'");
				else
					cr.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgAxisID'");
				else
					cr.SAPsecurityOrgAxisID = dr.GetInt32(2);
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
