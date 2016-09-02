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

namespace RBSR_AUFW.DB.IMVFormula
{
	/// <summary>
	/// Return value from method GetMVFormula
	/// </summary>
	public struct returnGetMVFormula
	{
		public int ID;
		public string KEYapplication;
		public string MATCHentVal;
		public string MATCHauthObj;
		public string MATCHfieldSecName;
		public string Formula;
	}
	/// <summary>
	/// Return value from method ListMVFormula
	/// </summary>
	public struct returnListMVFormula
	{
		public int ID;
		public string KEYapplication;
		public string MATCHentVal;
		public string MATCHauthObj;
		public string MATCHfieldSecName;
		public string Formula;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_MVFormula
	/// </summary>
	public class IMVFormula
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
		public IMVFormula() : this((OdbcConnection)null) { }
		public IMVFormula(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IMVFormula(OdbcConnection dbConnection)
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
		/// insert a row in table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="KEYapplication"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewMVFormula(string KEYapplication)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_MVFormula\"(\"c_u_KEYapplication\") VALUES(?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (KEYapplication == null)  { cmd.Dispose(); DBClose(); throw new Exception("KEYapplication must not be null!"); } 
			cmd.Parameters.Add("c_u_KEYapplication", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_KEYapplication"].Value = (KEYapplication != null ? (object)KEYapplication : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteMVFormula(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_MVFormula\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetMVFormula</returns>
		public returnGetMVFormula GetMVFormula(int ID)
		{
			returnGetMVFormula rv = new returnGetMVFormula();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_KEYapplication\",\"c_u_MATCHentVal\",\"c_u_MATCHauthObj\",\"c_u_MATCHfieldSecName\",\"c_u_Formula\" from \"t_RBSR_AUFW_u_MVFormula\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'KEYapplication'"); } 
				else
					rv.KEYapplication = dr.GetString(1);
				if (dr.IsDBNull(2))
					rv.MATCHentVal = null;
				else
					rv.MATCHentVal = dr.GetString(2);
				if (dr.IsDBNull(3))
					rv.MATCHauthObj = null;
				else
					rv.MATCHauthObj = dr.GetString(3);
				if (dr.IsDBNull(4))
					rv.MATCHfieldSecName = null;
				else
					rv.MATCHfieldSecName = dr.GetString(4);
				if (dr.IsDBNull(5))
					rv.Formula = null;
				else
					rv.Formula = dr.GetString(5);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="KEYapplication"></param>
		/// <param name="MATCHentVal"></param>
		/// <param name="MATCHauthObj"></param>
		/// <param name="MATCHfieldSecName"></param>
		/// <param name="Formula"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetMVFormula(int ID, string KEYapplication, string MATCHentVal, string MATCHauthObj, string MATCHfieldSecName, string Formula)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_MVFormula\" set \"c_u_KEYapplication\"=?,\"c_u_MATCHentVal\"=?,\"c_u_MATCHauthObj\"=?,\"c_u_MATCHfieldSecName\"=?,\"c_u_Formula\"=? where \"c_id\" = ?";
			if (KEYapplication == null)  { cmd.Dispose(); DBClose(); throw new Exception("KEYapplication must not be null!"); } 
			cmd.Parameters.Add("c_u_KEYapplication", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_KEYapplication"].Value = (KEYapplication != null ? (object)KEYapplication : DBNull.Value);
			cmd.Parameters.Add("c_u_MATCHentVal", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_MATCHentVal"].Value = (MATCHentVal != null ? (object)MATCHentVal : DBNull.Value);
			cmd.Parameters.Add("c_u_MATCHauthObj", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_MATCHauthObj"].Value = (MATCHauthObj != null ? (object)MATCHauthObj : DBNull.Value);
			cmd.Parameters.Add("c_u_MATCHfieldSecName", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_MATCHfieldSecName"].Value = (MATCHfieldSecName != null ? (object)MATCHfieldSecName : DBNull.Value);
			cmd.Parameters.Add("c_u_Formula", OdbcType.NVarChar, 500);
			cmd.Parameters["c_u_Formula"].Value = (Formula != null ? (object)Formula : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListMVFormula[]</returns>
		public returnListMVFormula[] ListMVFormula(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListMVFormula[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"keyapplication\"", "\"c_u_KEYapplication\"").Replace("\"matchentval\"", "\"c_u_MATCHentVal\"").Replace("\"matchauthobj\"", "\"c_u_MATCHauthObj\"").Replace("\"matchfieldsecname\"", "\"c_u_MATCHfieldSecName\"").Replace("\"formula\"", "\"c_u_Formula\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"keyapplication\"", "\"c_u_KEYapplication\"").Replace("\"matchentval\"", "\"c_u_MATCHentVal\"").Replace("\"matchauthobj\"", "\"c_u_MATCHauthObj\"").Replace("\"matchfieldsecname\"", "\"c_u_MATCHfieldSecName\"").Replace("\"formula\"", "\"c_u_Formula\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_KEYapplication\", \"c_u_MATCHentVal\", \"c_u_MATCHauthObj\", \"c_u_MATCHfieldSecName\", \"c_u_Formula\" FROM \"t_RBSR_AUFW_u_MVFormula\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_KEYapplication\", \"c_u_MATCHentVal\", \"c_u_MATCHauthObj\", \"c_u_MATCHfieldSecName\", \"c_u_Formula\" FROM \"t_RBSR_AUFW_u_MVFormula\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_KEYapplication\", \"c_u_MATCHentVal\", \"c_u_MATCHauthObj\", \"c_u_MATCHfieldSecName\", \"c_u_Formula\" FROM \"t_RBSR_AUFW_u_MVFormula\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListMVFormula> rvl = new List<returnListMVFormula>();
			while (dr.Read())
			{
				returnListMVFormula cr = new returnListMVFormula();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'KEYapplication'"); } 
				else
					cr.KEYapplication = dr.GetString(1);
				if (dr.IsDBNull(2))
					cr.MATCHentVal = null;
				else
					cr.MATCHentVal = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.MATCHauthObj = null;
				else
					cr.MATCHauthObj = dr.GetString(3);
				if (dr.IsDBNull(4))
					cr.MATCHfieldSecName = null;
				else
					cr.MATCHfieldSecName = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.Formula = null;
				else
					cr.Formula = dr.GetString(5);
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
