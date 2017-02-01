using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.ISAPsecurityOrgValue
{
	/// <summary>
	/// Return value from method GetSAPsecurityOrgValue
	/// </summary>
	public struct returnGetSAPsecurityOrgValue
	{
		public int ID;
		public string ValueString;
		public int SAPsecurityOrgAxisID;
		public int SAPsecurityOrgID;
	}
	/// <summary>
	/// Return value from method ListSAPsecurityOrgValue
	/// </summary>
	public struct returnListSAPsecurityOrgValue
	{
		public int ID;
		public string ValueString;
		public int SAPsecurityOrgAxisID;
		public int SAPsecurityOrgID;
	}
	/// <summary>
	/// Return value from method ListSAPsecurityOrgValueBySAPsecurityOrgAxis
	/// </summary>
	public struct returnListSAPsecurityOrgValueBySAPsecurityOrgAxis
	{
		public int ID;
		public string ValueString;
		public int SAPsecurityOrgAxisID;
		public int SAPsecurityOrgID;
	}
	/// <summary>
	/// Return value from method ListSAPsecurityOrgValueBySAPsecurityOrg
	/// </summary>
	public struct returnListSAPsecurityOrgValueBySAPsecurityOrg
	{
		public int ID;
		public string ValueString;
		public int SAPsecurityOrgAxisID;
		public int SAPsecurityOrgID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_SAPsecurityOrgValue
	/// </summary>
	public class ISAPsecurityOrgValue : _6MAR_WebApplication.RISEBASE
	{
		public ISAPsecurityOrgValue() : this((OdbcConnection)null) { }
		public ISAPsecurityOrgValue(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ISAPsecurityOrgValue(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="ValueString"></param>
		/// <param name="SAPsecurityOrgAxisID"></param>
		/// <param name="SAPsecurityOrgID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewSAPsecurityOrgValue(string ValueString, int SAPsecurityOrgAxisID, int SAPsecurityOrgID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_SAPsecurityOrgValue\"(\"c_u_ValueString\",\"c_r_SAPsecurityOrgAxis\",\"c_r_SAPsecurityOrg\") VALUES(?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (ValueString == null)  { cmd.Dispose(); DBClose(); throw new Exception("ValueString must not be null!"); } 
			cmd.Parameters.Add("c_u_ValueString", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_ValueString"].Value = (ValueString != null ? (object)ValueString : DBNull.Value);
			cmd.Parameters.Add("c_r_SAPsecurityOrgAxis", OdbcType.Int);
			cmd.Parameters["c_r_SAPsecurityOrgAxis"].Value = (object)SAPsecurityOrgAxisID;
			cmd.Parameters.Add("c_r_SAPsecurityOrg", OdbcType.Int);
			cmd.Parameters["c_r_SAPsecurityOrg"].Value = (object)SAPsecurityOrgID;
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
		/// delete a row from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteSAPsecurityOrgValue(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_SAPsecurityOrgValue\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPsecurityOrgValue</returns>
		public returnGetSAPsecurityOrgValue GetSAPsecurityOrgValue(int ID)
		{
			returnGetSAPsecurityOrgValue rv = new returnGetSAPsecurityOrgValue();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_ValueString\",\"c_r_SAPsecurityOrgAxis\",\"c_r_SAPsecurityOrg\" from \"t_RBSR_AUFW_u_SAPsecurityOrgValue\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ValueString'"); } 
				else
					rv.ValueString = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgAxisID'"); } 
				else
					rv.SAPsecurityOrgAxisID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgID'"); } 
				else
					rv.SAPsecurityOrgID = dr.GetInt32(3);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="ValueString"></param>
		/// <param name="SAPsecurityOrgAxisID"></param>
		/// <param name="SAPsecurityOrgID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetSAPsecurityOrgValue(int ID, string ValueString, int SAPsecurityOrgAxisID, int SAPsecurityOrgID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_SAPsecurityOrgValue\" set \"c_u_ValueString\"=?,\"c_r_SAPsecurityOrgAxis\"=?,\"c_r_SAPsecurityOrg\"=? where \"c_id\" = ?";
			if (ValueString == null)  { cmd.Dispose(); DBClose(); throw new Exception("ValueString must not be null!"); } 
			cmd.Parameters.Add("c_u_ValueString", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_ValueString"].Value = (ValueString != null ? (object)ValueString : DBNull.Value);
			cmd.Parameters.Add("c_r_SAPsecurityOrgAxis", OdbcType.Int);
			cmd.Parameters["c_r_SAPsecurityOrgAxis"].Value = (object)SAPsecurityOrgAxisID;
			cmd.Parameters.Add("c_r_SAPsecurityOrg", OdbcType.Int);
			cmd.Parameters["c_r_SAPsecurityOrg"].Value = (object)SAPsecurityOrgID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListSAPsecurityOrgValue[]</returns>
		public returnListSAPsecurityOrgValue[] ListSAPsecurityOrgValue(int? maxRowsToReturn)
		{
			returnListSAPsecurityOrgValue[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_ValueString\", \"c_r_SAPsecurityOrgAxis\", \"c_r_SAPsecurityOrg\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgValue\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_ValueString\", \"c_r_SAPsecurityOrgAxis\", \"c_r_SAPsecurityOrg\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgValue\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_ValueString\", \"c_r_SAPsecurityOrgAxis\", \"c_r_SAPsecurityOrg\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgValue\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPsecurityOrgValue> rvl = new List<returnListSAPsecurityOrgValue>();
			while (dr.Read())
			{
				returnListSAPsecurityOrgValue cr = new returnListSAPsecurityOrgValue();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ValueString'"); } 
				else
					cr.ValueString = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgAxisID'"); } 
				else
					cr.SAPsecurityOrgAxisID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgID'"); } 
				else
					cr.SAPsecurityOrgID = dr.GetInt32(3);
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SAPsecurityOrgAxisID"></param>
		/// <returns>returnListSAPsecurityOrgValueBySAPsecurityOrgAxis[]</returns>
		public returnListSAPsecurityOrgValueBySAPsecurityOrgAxis[] ListSAPsecurityOrgValueBySAPsecurityOrgAxis(int? maxRowsToReturn, int SAPsecurityOrgAxisID)
		{
			returnListSAPsecurityOrgValueBySAPsecurityOrgAxis[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_ValueString\", \"c_r_SAPsecurityOrgAxis\", \"c_r_SAPsecurityOrg\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgValue\" WHERE \"c_r_SAPsecurityOrgAxis\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_ValueString\", \"c_r_SAPsecurityOrgAxis\", \"c_r_SAPsecurityOrg\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgValue\" WHERE \"c_r_SAPsecurityOrgAxis\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_ValueString\", \"c_r_SAPsecurityOrgAxis\", \"c_r_SAPsecurityOrg\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgValue\" WHERE \"c_r_SAPsecurityOrgAxis\"=?";
			cmd.Parameters.Add("1_SAPsecurityOrgAxisID", OdbcType.Int);
			cmd.Parameters["1_SAPsecurityOrgAxisID"].Value = (object)SAPsecurityOrgAxisID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPsecurityOrgValueBySAPsecurityOrgAxis> rvl = new List<returnListSAPsecurityOrgValueBySAPsecurityOrgAxis>();
			while (dr.Read())
			{
				returnListSAPsecurityOrgValueBySAPsecurityOrgAxis cr = new returnListSAPsecurityOrgValueBySAPsecurityOrgAxis();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ValueString'"); } 
				else
					cr.ValueString = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgAxisID'"); } 
				else
					cr.SAPsecurityOrgAxisID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgID'"); } 
				else
					cr.SAPsecurityOrgID = dr.GetInt32(3);
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrgValue.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SAPsecurityOrgID"></param>
		/// <returns>returnListSAPsecurityOrgValueBySAPsecurityOrg[]</returns>
		public returnListSAPsecurityOrgValueBySAPsecurityOrg[] ListSAPsecurityOrgValueBySAPsecurityOrg(int? maxRowsToReturn, int SAPsecurityOrgID)
		{
			returnListSAPsecurityOrgValueBySAPsecurityOrg[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_ValueString\", \"c_r_SAPsecurityOrgAxis\", \"c_r_SAPsecurityOrg\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgValue\" WHERE \"c_r_SAPsecurityOrg\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_ValueString\", \"c_r_SAPsecurityOrgAxis\", \"c_r_SAPsecurityOrg\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgValue\" WHERE \"c_r_SAPsecurityOrg\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_ValueString\", \"c_r_SAPsecurityOrgAxis\", \"c_r_SAPsecurityOrg\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgValue\" WHERE \"c_r_SAPsecurityOrg\"=?";
			cmd.Parameters.Add("1_SAPsecurityOrgID", OdbcType.Int);
			cmd.Parameters["1_SAPsecurityOrgID"].Value = (object)SAPsecurityOrgID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPsecurityOrgValueBySAPsecurityOrg> rvl = new List<returnListSAPsecurityOrgValueBySAPsecurityOrg>();
			while (dr.Read())
			{
				returnListSAPsecurityOrgValueBySAPsecurityOrg cr = new returnListSAPsecurityOrgValueBySAPsecurityOrg();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ValueString'"); } 
				else
					cr.ValueString = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgAxisID'"); } 
				else
					cr.SAPsecurityOrgAxisID = dr.GetInt32(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SAPsecurityOrgID'"); } 
				else
					cr.SAPsecurityOrgID = dr.GetInt32(3);
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
