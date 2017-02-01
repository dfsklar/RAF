using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.465 (#506)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IFuncApplNotes
{
	/// <summary>
	/// Return value from method GetFuncApplNotes
	/// </summary>
	public struct returnGetFuncApplNotes
	{
		public int ID;
		public int REFapplication;
		public string Comment;
		public int BusRoleID;
	}
	/// <summary>
	/// Return value from method ListFuncApplNotes
	/// </summary>
	public struct returnListFuncApplNotes
	{
		public int ID;
		public int REFapplication;
		public string Comment;
		public int BusRoleID;
	}
	/// <summary>
	/// Return value from method ListFuncAppl_NotesByBusRole
	/// </summary>
	public struct returnListFuncAppl_NotesByBusRole
	{
		public int ID;
		public int REFapplication;
		public string Comment;
		public int BusRoleID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_FuncApplNotes
	/// </summary>
    public class IFuncApplNotes : _6MAR_WebApplication.RISEBASE
	{
		public IFuncApplNotes() : this((OdbcConnection)null) { }
		public IFuncApplNotes(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IFuncApplNotes(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}
		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_FuncApplNotes.
		/// </summary>
		/// <param name="BusRoleID"></param>
		/// <param name="REFapplication"></param>
		/// <param name="Comment"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewFuncApplNotes(int BusRoleID, int REFapplication, string Comment)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_FuncApplNotes\"(\"c_r_BusRole\",\"c_u_REFapplication\",\"c_u_Comment\") VALUES(?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
			cmd.Parameters["c_r_BusRole"].Value = (object)BusRoleID;
			cmd.Parameters.Add("c_u_REFapplication", OdbcType.Int);
			cmd.Parameters["c_u_REFapplication"].Value = (object)REFapplication;
			cmd.Parameters.Add("c_u_Comment", OdbcType.NText);
			cmd.Parameters["c_u_Comment"].Value = (object)Comment;
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
		/// delete a row from table t_RBSR_AUFW_u_FuncApplNotes.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteFuncApplNotes(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_FuncApplNotes\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_FuncApplNotes.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetFuncApplNotes</returns>
		public returnGetFuncApplNotes GetFuncApplNotes(int ID)
		{
			returnGetFuncApplNotes rv = new returnGetFuncApplNotes();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_REFapplication\",\"c_u_Comment\",\"c_r_BusRole\" from \"t_RBSR_AUFW_u_FuncApplNotes\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'REFapplication'");
				else
					rv.REFapplication = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'Comment'");
				else
					rv.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'BusRoleID'");
				else
					rv.BusRoleID = dr.GetInt32(3);
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
		/// update a row in table t_RBSR_AUFW_u_FuncApplNotes.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="REFapplication"></param>
		/// <param name="Comment"></param>
		/// <param name="BusRoleID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetFuncApplNotes(int ID, int REFapplication, string Comment, int BusRoleID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_FuncApplNotes\" set \"c_u_REFapplication\"=?,\"c_u_Comment\"=?,\"c_r_BusRole\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_u_REFapplication", OdbcType.Int);
			cmd.Parameters["c_u_REFapplication"].Value = (object)REFapplication;
			cmd.Parameters.Add("c_u_Comment", OdbcType.NText);
			cmd.Parameters["c_u_Comment"].Value = (object)Comment;
			cmd.Parameters.Add("c_r_BusRole", OdbcType.Int);
			cmd.Parameters["c_r_BusRole"].Value = (object)BusRoleID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_FuncApplNotes.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListFuncApplNotes[]</returns>
		public returnListFuncApplNotes[] ListFuncApplNotes(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListFuncApplNotes[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"refapplication\"", "\"c_u_REFapplication\"").Replace("\"comment\"", "\"c_u_Comment\"").Replace("\"busrole\"", "\"c_r_BusRole\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"refapplication\"", "\"c_u_REFapplication\"").Replace("\"comment\"", "\"c_u_Comment\"").Replace("\"busrole\"", "\"c_r_BusRole\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_REFapplication\", \"c_u_Comment\", \"c_r_BusRole\" FROM \"t_RBSR_AUFW_u_FuncApplNotes\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_REFapplication\", \"c_u_Comment\", \"c_r_BusRole\" FROM \"t_RBSR_AUFW_u_FuncApplNotes\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_REFapplication\", \"c_u_Comment\", \"c_r_BusRole\" FROM \"t_RBSR_AUFW_u_FuncApplNotes\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListFuncApplNotes> rvl = new List<returnListFuncApplNotes>();
			while (dr.Read())
			{
				returnListFuncApplNotes cr = new returnListFuncApplNotes();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'REFapplication'");
				else
					cr.REFapplication = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'Comment'");
				else
					cr.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'BusRoleID'");
				else
					cr.BusRoleID = dr.GetInt32(3);
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
		/// select a set of rows from table t_RBSR_AUFW_u_FuncApplNotes.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="BusRoleID"></param>
		/// <returns>returnListFuncAppl_NotesByBusRole[]</returns>
		public returnListFuncAppl_NotesByBusRole[] ListFuncAppl_NotesByBusRole(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int BusRoleID)
		{
			returnListFuncAppl_NotesByBusRole[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"refapplication\"", "\"c_u_REFapplication\"").Replace("\"comment\"", "\"c_u_Comment\"").Replace("\"busrole\"", "\"c_r_BusRole\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"refapplication\"", "\"c_u_REFapplication\"").Replace("\"comment\"", "\"c_u_Comment\"").Replace("\"busrole\"", "\"c_r_BusRole\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_REFapplication\", \"c_u_Comment\", \"c_r_BusRole\" FROM \"t_RBSR_AUFW_u_FuncApplNotes\" WHERE \"FuncApplNotes\".\"@id\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_REFapplication\", \"c_u_Comment\", \"c_r_BusRole\" FROM \"t_RBSR_AUFW_u_FuncApplNotes\" WHERE \"FuncApplNotes\".\"@id\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_REFapplication\", \"c_u_Comment\", \"c_r_BusRole\" FROM \"t_RBSR_AUFW_u_FuncApplNotes\" WHERE \"FuncApplNotes\".\"@id\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_BusRoleID", OdbcType.Int);
			cmd.Parameters["1_BusRoleID"].Value = (object)BusRoleID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListFuncAppl_NotesByBusRole> rvl = new List<returnListFuncAppl_NotesByBusRole>();
			while (dr.Read())
			{
				returnListFuncAppl_NotesByBusRole cr = new returnListFuncAppl_NotesByBusRole();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'REFapplication'");
				else
					cr.REFapplication = dr.GetInt32(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'Comment'");
				else
					cr.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'BusRoleID'");
				else
					cr.BusRoleID = dr.GetInt32(3);
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
