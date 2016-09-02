using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//c5239606-e81b-4173-a133-8a3bf82326d1
//
// New Model (RBSR_AUFW)
// Version 1.369 (#377)
//
// 
// 
//
//c5239606-e81b-4173-a133-8a3bf82326d1

namespace RBSR_AUFW.DB.IProcess
{
	/// <summary>
	/// Return value from method GetProcess
	/// </summary>
	public struct returnGetProcess
	{
		public int ID;
		public string Name;
	}
	/// <summary>
	/// Return value from method ListProcess
	/// </summary>
	public struct returnListProcess
	{
		public int ID;
		public string Name;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_Process
	/// </summary>
	public class IProcess
	{
		private string _tempDir = ".";
		private bool _alreadyOpen = false;
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
		public IProcess() {}
		public IProcess(string connectionString)
		{
			_dbConnection = new OdbcConnection(connectionString);
		}
		public IProcess(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}
		protected string SpecSQL(string spec)
		{
			string driver = _dbConnection.Driver.ToLower();
			if (driver.StartsWith("myodbc"))
			{
				if (spec.ToLower()=="get id") return "select last_insert_id()";
				if (spec.ToLower()=="on connect") return "set sql_mode ='ANSI_QUOTES'";
			}
			else if (driver.StartsWith("sqlsrv"))
			{
				if (spec.ToLower()=="get id") return ";select convert(int,SCOPE_IDENTITY())";
			}
			return string.Empty;
		}
		protected void DBConnect()
		{
			_alreadyOpen = (_dbConnection.State == ConnectionState.Open ? true : false);
			if (!_alreadyOpen) _dbConnection.Open();
			string postConnect = SpecSQL("on connect");
			if (postConnect != string.Empty)
			{
				OdbcCommand cmd = new OdbcCommand();
				cmd.CommandText = postConnect;
				cmd.Connection = _dbConnection;
				cmd.ExecuteNonQuery();
			}
		}
		protected void DBClose()
		{
			if (!_alreadyOpen)
			{
				_dbConnection.Close();
			}
		}
		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewProcess(string Name)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "insert into \"t_RBSR_AUFW_u_Process\" (\"c_u_Name\") values(?) " + SpecSQL("get id");
			if(Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name",OdbcType.NVarChar,50);
			cmd.Parameters["c_u_Name"].Value = (Name!= null ? (object)Name : DBNull.Value);
			cmd.Connection = _dbConnection;
			OdbcDataReader dri = cmd.ExecuteReader();
			dri.Read();
			rv = (dri.IsDBNull(0) ? 0 : dri.GetInt32(0));
			dri.Close();
			dri.Dispose();
			if (rv == 0) throw new Exception("Insert operation failed!");
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// delete a row from table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteProcess(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_Process\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetProcess</returns>
		public returnGetProcess GetProcess(int ID)
		{
			returnGetProcess rv = new returnGetProcess();
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_Name\" from \"t_RBSR_AUFW_u_Process\" where \"c_id\"= ?";
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				if(!dr.IsDBNull(0))
				{
					rv.ID = dr.GetInt32(0);
				}
				else
				{
					throw new Exception("Value 'null' is not allowed for 'ID'");
				}
				if(!dr.IsDBNull(1))
				{
					rv.Name = dr.GetString(1);
				}
				else
				{
					throw new Exception("Value 'null' is not allowed for 'Name'");
				}
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetProcess(int ID, string Name)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_Process\" set \"c_u_Name\"=? where \"c_id\" = ?";
			if(Name == null) throw new Exception("Name must not be null!");
			cmd.Parameters.Add("c_u_Name",OdbcType.NVarChar,50);
			cmd.Parameters["c_u_Name"].Value = (Name!= null ? (object)Name : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_Process.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListProcess[]</returns>
		public returnListProcess[] ListProcess(int? maxRowsToReturn)
		{
			returnListProcess[] rv = null;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select" + (maxRowsToReturn.HasValue && maxRowsToReturn.Value!=0 ? " top " + maxRowsToReturn.Value.ToString() : "") + " " + "\"c_id\",\"c_u_Name\" from \"t_RBSR_AUFW_u_Process\"";
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListProcess> rvl = new List<returnListProcess>();
			while(dr.Read())
			{
				returnListProcess cr = new returnListProcess();
				if(!dr.IsDBNull(0))
				{
					cr.ID = dr.GetInt32(0);
				}
				else
				{
					throw new Exception("Value 'null' is not allowed for 'ID'");
				}
				if(!dr.IsDBNull(1))
				{
					cr.Name = dr.GetString(1);
				}
				else
				{
					throw new Exception("Value 'null' is not allowed for 'Name'");
				}
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
