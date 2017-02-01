using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.315 (#356)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.ISAPsecurityOrgAxis
{
	/// <summary>
	/// Return value from method GetSAPsecurityOrgAxis
	/// </summary>
	public struct returnGetSAPsecurityOrgAxis
	{
		public int ID;
		public string English_Name;
		public string SAP_Name;
		public string LegalValues;
	}
	/// <summary>
	/// Return value from method ListSAPsecurityOrgAxis
	/// </summary>
	public struct returnListSAPsecurityOrgAxis
	{
		public int ID;
		public string English_Name;
		public string SAP_Name;
		public string LegalValues;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_SAPsecurityOrgAxis
	/// </summary>
	public class ISAPsecurityOrgAxis : _6MAR_WebApplication.RISEBASE
	{
		public ISAPsecurityOrgAxis() : this((OdbcConnection)null) { }
		public ISAPsecurityOrgAxis(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public ISAPsecurityOrgAxis(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="English_Name"></param>
		/// <param name="SAP_Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewSAPsecurityOrgAxis(string English_Name, string SAP_Name)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_SAPsecurityOrgAxis\"(\"c_u_English_Name\",\"c_u_SAP_Name\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (English_Name == null) throw new Exception("English_Name must not be null!");
			cmd.Parameters.Add("c_u_English_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_English_Name"].Value = (English_Name != null ? (object)English_Name : DBNull.Value);
			if (SAP_Name == null) throw new Exception("SAP_Name must not be null!");
			cmd.Parameters.Add("c_u_SAP_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_SAP_Name"].Value = (SAP_Name != null ? (object)SAP_Name : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteSAPsecurityOrgAxis(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_SAPsecurityOrgAxis\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetSAPsecurityOrgAxis</returns>
		public returnGetSAPsecurityOrgAxis GetSAPsecurityOrgAxis(int ID)
		{
			returnGetSAPsecurityOrgAxis rv = new returnGetSAPsecurityOrgAxis();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_English_Name\",\"c_u_SAP_Name\",\"c_u_LegalValues\" from \"t_RBSR_AUFW_u_SAPsecurityOrgAxis\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'English_Name'");
				else
					rv.English_Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'SAP_Name'");
				else
					rv.SAP_Name = dr.GetString(2);
				if (dr.IsDBNull(3))
					rv.LegalValues = null;
				else
					rv.LegalValues = dr.GetString(3);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="English_Name"></param>
		/// <param name="SAP_Name"></param>
		/// <param name="LegalValues"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetSAPsecurityOrgAxis(int ID, string English_Name, string SAP_Name, string LegalValues)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_SAPsecurityOrgAxis\" set \"c_u_English_Name\"=?,\"c_u_SAP_Name\"=?,\"c_u_LegalValues\"=? where \"c_id\" = ?";
			if (English_Name == null) throw new Exception("English_Name must not be null!");
			cmd.Parameters.Add("c_u_English_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_English_Name"].Value = (English_Name != null ? (object)English_Name : DBNull.Value);
			if (SAP_Name == null) throw new Exception("SAP_Name must not be null!");
			cmd.Parameters.Add("c_u_SAP_Name", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_SAP_Name"].Value = (SAP_Name != null ? (object)SAP_Name : DBNull.Value);
			cmd.Parameters.Add("c_u_LegalValues", OdbcType.NVarChar, 2048);
			cmd.Parameters["c_u_LegalValues"].Value = (LegalValues != null ? (object)LegalValues : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_SAPsecurityOrgAxis.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListSAPsecurityOrgAxis[]</returns>
		public returnListSAPsecurityOrgAxis[] ListSAPsecurityOrgAxis(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListSAPsecurityOrgAxis[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"english_name\"", "\"c_u_English_Name\"").Replace("\"sap_name\"", "\"c_u_SAP_Name\"").Replace("\"legalvalues\"", "\"c_u_LegalValues\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"english_name\"", "\"c_u_English_Name\"").Replace("\"sap_name\"", "\"c_u_SAP_Name\"").Replace("\"legalvalues\"", "\"c_u_LegalValues\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_English_Name\", \"c_u_SAP_Name\", \"c_u_LegalValues\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgAxis\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_English_Name\", \"c_u_SAP_Name\", \"c_u_LegalValues\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgAxis\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_English_Name\", \"c_u_SAP_Name\", \"c_u_LegalValues\" FROM \"t_RBSR_AUFW_u_SAPsecurityOrgAxis\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListSAPsecurityOrgAxis> rvl = new List<returnListSAPsecurityOrgAxis>();
			while (dr.Read())
			{
				returnListSAPsecurityOrgAxis cr = new returnListSAPsecurityOrgAxis();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'English_Name'");
				else
					cr.English_Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'SAP_Name'");
				else
					cr.SAP_Name = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.LegalValues = null;
				else
					cr.LegalValues = dr.GetString(3);
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
