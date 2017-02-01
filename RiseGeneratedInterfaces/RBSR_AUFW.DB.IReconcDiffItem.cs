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

namespace RBSR_AUFW.DB.IReconcDiffItem
{
	/// <summary>
	/// Return value from method GetReconcDiffItem
	/// </summary>
	public struct returnGetReconcDiffItem
	{
		public int ID;
		public string DiffType;
		public string Comment;
		public string Status;
		public string AssignedUser;
		public int ReconcReportID;
		public string DiffObject;
		public string RoleName;
		public string Detail;
		public string Platform;
	}
	/// <summary>
	/// Return value from method ListReconcDiffItem
	/// </summary>
	public struct returnListReconcDiffItem
	{
		public int ID;
		public string DiffType;
		public string Comment;
		public string Status;
		public string AssignedUser;
		public int ReconcReportID;
		public string DiffObject;
		public string RoleName;
		public string Detail;
		public string Platform;
	}
	/// <summary>
	/// Return value from method ListReconcDiffItemByReconcReport
	/// </summary>
	public struct returnListReconcDiffItemByReconcReport
	{
		public int ID;
		public string DiffType;
		public string Comment;
		public string Status;
		public string AssignedUser;
		public int ReconcReportID;
		public string DiffObject;
		public string RoleName;
		public string Detail;
		public string Platform;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_ReconcDiffItem
	/// </summary>
	public class IReconcDiffItem : _6MAR_WebApplication.RISEBASE
	{
		public IReconcDiffItem() : this((OdbcConnection)null) { }
		public IReconcDiffItem(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IReconcDiffItem(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_ReconcDiffItem.
		/// </summary>
		/// <param name="DiffType"></param>
		/// <param name="ReconcReportID"></param>
		/// <param name="DiffObject"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewReconcDiffItem(string DiffType, int ReconcReportID, string DiffObject)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_ReconcDiffItem\"(\"c_u_DiffType\",\"c_r_ReconcReport\",\"c_u_DiffObject\") VALUES(?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (DiffType == null) throw new Exception("DiffType must not be null!");
			cmd.Parameters.Add("c_u_DiffType", OdbcType.NVarChar, 3);
			cmd.Parameters["c_u_DiffType"].Value = (DiffType != null ? (object)DiffType : DBNull.Value);
			cmd.Parameters.Add("c_r_ReconcReport", OdbcType.Int);
			cmd.Parameters["c_r_ReconcReport"].Value = (object)ReconcReportID;
			if (DiffObject == null) throw new Exception("DiffObject must not be null!");
			cmd.Parameters.Add("c_u_DiffObject", OdbcType.NVarChar, 4);
			cmd.Parameters["c_u_DiffObject"].Value = (DiffObject != null ? (object)DiffObject : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_ReconcDiffItem.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteReconcDiffItem(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_ReconcDiffItem\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_ReconcDiffItem.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetReconcDiffItem</returns>
		public returnGetReconcDiffItem GetReconcDiffItem(int ID)
		{
			returnGetReconcDiffItem rv = new returnGetReconcDiffItem();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_DiffType\",\"c_u_Comment\",\"c_u_Status\",\"c_u_AssignedUser\",\"c_r_ReconcReport\",\"c_u_DiffObject\",\"c_u_RoleName\",\"c_u_Detail\",\"c_u_Platform\" from \"t_RBSR_AUFW_u_ReconcDiffItem\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'DiffType'");
				else
					rv.DiffType = dr.GetString(1);
				if (dr.IsDBNull(2))
					rv.Comment = null;
				else
					rv.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					rv.Status = null;
				else
					rv.Status = dr.GetString(3);
				if (dr.IsDBNull(4))
					rv.AssignedUser = null;
				else
					rv.AssignedUser = dr.GetString(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'ReconcReportID'");
				else
					rv.ReconcReportID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'DiffObject'");
				else
					rv.DiffObject = dr.GetString(6);
				if (dr.IsDBNull(7))
					rv.RoleName = null;
				else
					rv.RoleName = dr.GetString(7);
				if (dr.IsDBNull(8))
					rv.Detail = null;
				else
					rv.Detail = dr.GetString(8);
				if (dr.IsDBNull(9))
					rv.Platform = null;
				else
					rv.Platform = dr.GetString(9);
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
		/// update a row in table t_RBSR_AUFW_u_ReconcDiffItem.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="DiffType"></param>
		/// <param name="Comment"></param>
		/// <param name="Status"></param>
		/// <param name="AssignedUser"></param>
		/// <param name="ReconcReportID"></param>
		/// <param name="DiffObject"></param>
		/// <param name="RoleName"></param>
		/// <param name="Detail"></param>
		/// <param name="Platform"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetReconcDiffItem(int ID, string DiffType, string Comment, string Status, string AssignedUser, int ReconcReportID, string DiffObject, string RoleName, string Detail, string Platform)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_ReconcDiffItem\" set \"c_u_DiffType\"=?,\"c_u_Comment\"=?,\"c_u_Status\"=?,\"c_u_AssignedUser\"=?,\"c_r_ReconcReport\"=?,\"c_u_DiffObject\"=?,\"c_u_RoleName\"=?,\"c_u_Detail\"=?,\"c_u_Platform\"=? where \"c_id\" = ?";
			if (DiffType == null) throw new Exception("DiffType must not be null!");
			cmd.Parameters.Add("c_u_DiffType", OdbcType.NVarChar, 3);
			cmd.Parameters["c_u_DiffType"].Value = (DiffType != null ? (object)DiffType : DBNull.Value);
			cmd.Parameters.Add("c_u_Comment", OdbcType.NText);
			cmd.Parameters["c_u_Comment"].Value = (Comment != null ? (object)Comment : DBNull.Value);
			cmd.Parameters.Add("c_u_Status", OdbcType.NVarChar, 1);
			cmd.Parameters["c_u_Status"].Value = (Status != null ? (object)Status : DBNull.Value);
			cmd.Parameters.Add("c_u_AssignedUser", OdbcType.NVarChar, 6);
			cmd.Parameters["c_u_AssignedUser"].Value = (AssignedUser != null ? (object)AssignedUser : DBNull.Value);
			cmd.Parameters.Add("c_r_ReconcReport", OdbcType.Int);
			cmd.Parameters["c_r_ReconcReport"].Value = (object)ReconcReportID;
			if (DiffObject == null) throw new Exception("DiffObject must not be null!");
			cmd.Parameters.Add("c_u_DiffObject", OdbcType.NVarChar, 4);
			cmd.Parameters["c_u_DiffObject"].Value = (DiffObject != null ? (object)DiffObject : DBNull.Value);
			cmd.Parameters.Add("c_u_RoleName", OdbcType.NVarChar, 255);
			cmd.Parameters["c_u_RoleName"].Value = (RoleName != null ? (object)RoleName : DBNull.Value);
			cmd.Parameters.Add("c_u_Detail", OdbcType.NVarChar, 2000);
			cmd.Parameters["c_u_Detail"].Value = (Detail != null ? (object)Detail : DBNull.Value);
			cmd.Parameters.Add("c_u_Platform", OdbcType.NVarChar, 12);
			cmd.Parameters["c_u_Platform"].Value = (Platform != null ? (object)Platform : DBNull.Value);
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();
			if (rv != 1) throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!");
			cmd.Dispose();
			DBClose();
			return rv;
		}




        public int SetReconcDiffItem(int ID, string Status)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "update \"t_RBSR_AUFW_u_ReconcDiffItem\" set \"c_u_Status\"=? where \"c_id\" = ?";

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
		/// select a set of rows from table t_RBSR_AUFW_u_ReconcDiffItem.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListReconcDiffItem[]</returns>
		public returnListReconcDiffItem[] ListReconcDiffItem(int? maxRowsToReturn)
		{
			returnListReconcDiffItem[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_DiffType\", \"c_u_Comment\", \"c_u_Status\", \"c_u_AssignedUser\", \"c_r_ReconcReport\", \"c_u_DiffObject\", \"c_u_RoleName\", \"c_u_Detail\", \"c_u_Platform\" FROM \"t_RBSR_AUFW_u_ReconcDiffItem\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_DiffType\", \"c_u_Comment\", \"c_u_Status\", \"c_u_AssignedUser\", \"c_r_ReconcReport\", \"c_u_DiffObject\", \"c_u_RoleName\", \"c_u_Detail\", \"c_u_Platform\" FROM \"t_RBSR_AUFW_u_ReconcDiffItem\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_DiffType\", \"c_u_Comment\", \"c_u_Status\", \"c_u_AssignedUser\", \"c_r_ReconcReport\", \"c_u_DiffObject\", \"c_u_RoleName\", \"c_u_Detail\", \"c_u_Platform\" FROM \"t_RBSR_AUFW_u_ReconcDiffItem\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListReconcDiffItem> rvl = new List<returnListReconcDiffItem>();
			while (dr.Read())
			{
				returnListReconcDiffItem cr = new returnListReconcDiffItem();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'DiffType'");
				else
					cr.DiffType = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.Comment = null;
				else
					cr.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.Status = null;
				else
					cr.Status = dr.GetString(3);
				if (dr.IsDBNull(4))
					cr.AssignedUser = null;
				else
					cr.AssignedUser = dr.GetString(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'ReconcReportID'");
				else
					cr.ReconcReportID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'DiffObject'");
				else
					cr.DiffObject = dr.GetString(6);
				if (dr.IsDBNull(7))
					cr.RoleName = null;
				else
					cr.RoleName = dr.GetString(7);
				if (dr.IsDBNull(8))
					cr.Detail = null;
				else
					cr.Detail = dr.GetString(8);
				if (dr.IsDBNull(9))
					cr.Platform = null;
				else
					cr.Platform = dr.GetString(9);
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
		/// select a set of rows from table t_RBSR_AUFW_u_ReconcDiffItem.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="ReconcReportID"></param>
		/// <returns>returnListReconcDiffItemByReconcReport[]</returns>
		public returnListReconcDiffItemByReconcReport[] ListReconcDiffItemByReconcReport(int? maxRowsToReturn, int ReconcReportID)
		{
			returnListReconcDiffItemByReconcReport[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_DiffType\", \"c_u_Comment\", \"c_u_Status\", \"c_u_AssignedUser\", \"c_r_ReconcReport\", \"c_u_DiffObject\", \"c_u_RoleName\", \"c_u_Detail\", \"c_u_Platform\" FROM \"t_RBSR_AUFW_u_ReconcDiffItem\" WHERE \"c_r_ReconcReport\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_DiffType\", \"c_u_Comment\", \"c_u_Status\", \"c_u_AssignedUser\", \"c_r_ReconcReport\", \"c_u_DiffObject\", \"c_u_RoleName\", \"c_u_Detail\", \"c_u_Platform\" FROM \"t_RBSR_AUFW_u_ReconcDiffItem\" WHERE \"c_r_ReconcReport\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_DiffType\", \"c_u_Comment\", \"c_u_Status\", \"c_u_AssignedUser\", \"c_r_ReconcReport\", \"c_u_DiffObject\", \"c_u_RoleName\", \"c_u_Detail\", \"c_u_Platform\" FROM \"t_RBSR_AUFW_u_ReconcDiffItem\" WHERE \"c_r_ReconcReport\"=?";
			cmd.Parameters.Add("1_ReconcReportID", OdbcType.Int);
			cmd.Parameters["1_ReconcReportID"].Value = (object)ReconcReportID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListReconcDiffItemByReconcReport> rvl = new List<returnListReconcDiffItemByReconcReport>();
			while (dr.Read())
			{
				returnListReconcDiffItemByReconcReport cr = new returnListReconcDiffItemByReconcReport();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'DiffType'");
				else
					cr.DiffType = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.Comment = null;
				else
					cr.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.Status = null;
				else
					cr.Status = dr.GetString(3);
				if (dr.IsDBNull(4))
					cr.AssignedUser = null;
				else
					cr.AssignedUser = dr.GetString(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'ReconcReportID'");
				else
					cr.ReconcReportID = dr.GetInt32(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'DiffObject'");
				else
					cr.DiffObject = dr.GetString(6);
				if (dr.IsDBNull(7))
					cr.RoleName = null;
				else
					cr.RoleName = dr.GetString(7);
				if (dr.IsDBNull(8))
					cr.Detail = null;
				else
					cr.Detail = dr.GetString(8);
				if (dr.IsDBNull(9))
					cr.Platform = null;
				else
					cr.Platform = dr.GetString(9);
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
