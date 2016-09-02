using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.363 (#404)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IReconcReport
{
	/// <summary>
	/// Return value from method GetReconcReport
	/// </summary>
	public struct returnGetReconcReport
	{
		public int ID;
		public DateTime CreationTime;
		public string Comment;
		public int UserID;
		public string Domain;
	}
	/// <summary>
	/// Return value from method ListReconcReport
	/// </summary>
	public struct returnListReconcReport
	{
		public int ID;
		public DateTime CreationTime;
		public string Comment;
		public int UserID;
		public string Domain;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_ReconcReport
	/// </summary>
	public class IReconcReport
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
		public IReconcReport() : this((OdbcConnection)null) { }
		public IReconcReport(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IReconcReport(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_ReconcReport.
		/// </summary>
		/// <param name="CreationTime"></param>
		/// <param name="UserID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewReconcReport(DateTime CreationTime, int UserID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_ReconcReport\"(\"c_u_CreationTime\",\"c_r_User\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_u_CreationTime", OdbcType.DateTime);
			cmd.Parameters["c_u_CreationTime"].Value = (object)CreationTime;
			cmd.Parameters.Add("c_r_User", OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
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
		/// delete a row from table t_RBSR_AUFW_u_ReconcReport.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteReconcReport(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_ReconcReport\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_ReconcReport.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetReconcReport</returns>
		public returnGetReconcReport GetReconcReport(int ID)
		{
			returnGetReconcReport rv = new returnGetReconcReport();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_CreationTime\",\"c_u_Comment\",\"c_r_User\",\"c_u_Domain\" from \"t_RBSR_AUFW_u_ReconcReport\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'CreationTime'");
				else
					rv.CreationTime = dr.GetDateTime(1);
				if (dr.IsDBNull(2))
					rv.Comment = null;
				else
					rv.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'UserID'");
				else
					rv.UserID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'Domain'");
				else
					rv.Domain = dr.GetString(4);
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
		/// update a row in table t_RBSR_AUFW_u_ReconcReport.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="CreationTime"></param>
		/// <param name="Comment"></param>
		/// <param name="UserID"></param>
		/// <param name="Domain"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetReconcReport(int ID, DateTime CreationTime, string Comment, int UserID, string Domain)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_ReconcReport\" set \"c_u_CreationTime\"=?,\"c_u_Comment\"=?,\"c_r_User\"=?,\"c_u_Domain\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_u_CreationTime", OdbcType.DateTime);
			cmd.Parameters["c_u_CreationTime"].Value = (object)CreationTime;
			cmd.Parameters.Add("c_u_Comment", OdbcType.NText);
			cmd.Parameters["c_u_Comment"].Value = (Comment != null ? (object)Comment : DBNull.Value);
			cmd.Parameters.Add("c_r_User", OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			if (Domain == null) throw new Exception("Domain must not be null!");
			cmd.Parameters.Add("c_u_Domain", OdbcType.NVarChar, 3);
			cmd.Parameters["c_u_Domain"].Value = (Domain != null ? (object)Domain : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_ReconcReport.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListReconcReport[]</returns>
		public returnListReconcReport[] ListReconcReport(int? maxRowsToReturn)
		{
			returnListReconcReport[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_CreationTime\", \"c_u_Comment\", \"c_r_User\", \"c_u_Domain\" FROM \"t_RBSR_AUFW_u_ReconcReport\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_CreationTime\", \"c_u_Comment\", \"c_r_User\", \"c_u_Domain\" FROM \"t_RBSR_AUFW_u_ReconcReport\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_CreationTime\", \"c_u_Comment\", \"c_r_User\", \"c_u_Domain\" FROM \"t_RBSR_AUFW_u_ReconcReport\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListReconcReport> rvl = new List<returnListReconcReport>();
			while (dr.Read())
			{
				returnListReconcReport cr = new returnListReconcReport();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'CreationTime'");
				else
					cr.CreationTime = dr.GetDateTime(1);
				if (dr.IsDBNull(2))
					cr.Comment = null;
				else
					cr.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'UserID'");
				else
					cr.UserID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'Domain'");
				else
					cr.Domain = dr.GetString(4);
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
