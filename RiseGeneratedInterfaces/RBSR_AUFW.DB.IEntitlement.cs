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

namespace RBSR_AUFW.DB.IEntitlement
{
	/// <summary>
	/// Return value from method GetEntitlement
	/// </summary>
	public struct returnGetEntitlement
	{
		public int ID;
		public string StandardActivity;
		public string RoleType;
		public string System;
		public string Platform;
		public string EntitlementName;
		public string EntitlementValue;
		public string AuthObjName;
		public string AuthObjValue;
		public string FieldSecName;
		public string FieldSecValue;
		public string Level4SecName;
		public string Level4SecValue;
		public string Commentary;
		public string GENmanifestValue;
		public string Application;
		public string CHECKSUM;
		public string Status;
	}
	/// <summary>
	/// Return value from method ListEntitlement
	/// </summary>
	public struct returnListEntitlement
	{
		public int ID;
		public string StandardActivity;
		public string RoleType;
		public string System;
		public string Platform;
		public string EntitlementName;
		public string EntitlementValue;
		public string AuthObjName;
		public string AuthObjValue;
		public string FieldSecName;
		public string FieldSecValue;
		public string Level4SecName;
		public string Level4SecValue;
		public string Commentary;
		public string GENmanifestValue;
		public string Application;
		public string CHECKSUM;
		public string Status;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_Entitlement
	/// </summary>
	public class IEntitlement : _6MAR_WebApplication.RISEBASE
	{
		public IEntitlement() : this((OdbcConnection)null) { }
		public IEntitlement(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IEntitlement(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="StandardActivity"></param>
		/// <param name="RoleType"></param>
		/// <param name="System"></param>
		/// <param name="Platform"></param>
		/// <param name="EntitlementName"></param>
		/// <param name="EntitlementValue"></param>
		/// <param name="Application"></param>
		/// <param name="CHECKSUM"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewEntitlement(string StandardActivity, string RoleType, string System, string Platform, string EntitlementName, string EntitlementValue, string Application, string CHECKSUM)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_Entitlement\"(\"c_u_StandardActivity\",\"c_u_RoleType\",\"c_u_System\",\"c_u_Platform\",\"c_u_EntitlementName\",\"c_u_EntitlementValue\",\"c_u_Application\",\"c_u_CHECKSUM\") VALUES(?,?,?,?,?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (StandardActivity == null) throw new Exception("StandardActivity must not be null!");
			cmd.Parameters.Add("c_u_StandardActivity", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_StandardActivity"].Value = (StandardActivity != null ? (object)StandardActivity : DBNull.Value);
			if (RoleType == null) throw new Exception("RoleType must not be null!");
			cmd.Parameters.Add("c_u_RoleType", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_RoleType"].Value = (RoleType != null ? (object)RoleType : DBNull.Value);
			if (System == null) throw new Exception("System must not be null!");
			cmd.Parameters.Add("c_u_System", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_System"].Value = (System != null ? (object)System : DBNull.Value);
			if (Platform == null) throw new Exception("Platform must not be null!");
			cmd.Parameters.Add("c_u_Platform", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Platform"].Value = (Platform != null ? (object)Platform : DBNull.Value);
			if (EntitlementName == null) throw new Exception("EntitlementName must not be null!");
			cmd.Parameters.Add("c_u_EntitlementName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_EntitlementName"].Value = (EntitlementName != null ? (object)EntitlementName : DBNull.Value);
			if (EntitlementValue == null) throw new Exception("EntitlementValue must not be null!");
			cmd.Parameters.Add("c_u_EntitlementValue", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_EntitlementValue"].Value = (EntitlementValue != null ? (object)EntitlementValue : DBNull.Value);
			if (Application == null) throw new Exception("Application must not be null!");
			cmd.Parameters.Add("c_u_Application", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Application"].Value = (Application != null ? (object)Application : DBNull.Value);
			if (CHECKSUM == null) throw new Exception("CHECKSUM must not be null!");
			cmd.Parameters.Add("c_u_CHECKSUM", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_CHECKSUM"].Value = (CHECKSUM != null ? (object)CHECKSUM : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteEntitlement(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_Entitlement\" where \"c_id\" = ?";
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();
			if (rv != 1) throw new Exception("Delete resulted in " + rv.ToString() + " objects being deleted!");
			cmd.Dispose();
			DBClose();
			return rv;
		}





        public int SetEntitlementStatus(int ID, char newstat)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "UPDATE \"t_RBSR_AUFW_u_Entitlement\" SET c_u_Status = ? where \"c_id\" = ?";
            cmd.Parameters.Add("c_u_status", OdbcType.Char);
            cmd.Parameters["c_u_status"].Value = (object)newstat;
            cmd.Parameters.Add("c_id", OdbcType.Int);
            cmd.Parameters["c_id"].Value = (object)ID;
            cmd.Connection = _dbConnection;
            rv = cmd.ExecuteNonQuery();
            if (rv != 1) throw new Exception("Update status resulted in " + rv.ToString() + " objects being updated!");
            cmd.Dispose();
            DBClose();
            return rv;
        }

        public int SetEntitlementPrivstring(int ID, string privstring)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "UPDATE \"t_RBSR_AUFW_u_Entitlement\" SET c_u_GENmanifestValue = ? where \"c_id\" = ?";
            cmd.Parameters.Add("c_u_status", OdbcType.Char);
            cmd.Parameters["c_u_status"].Value = (object)privstring;
            cmd.Parameters.Add("c_id", OdbcType.Int);
            cmd.Parameters["c_id"].Value = (object)ID;
            cmd.Connection = _dbConnection;
            rv = cmd.ExecuteNonQuery();
            if (rv != 1) throw new Exception("Update GENmanfistValue resulted in " + rv.ToString() + " objects being updated!");
            cmd.Dispose();
            DBClose();
            return rv;
        }

        
        
        
        
        
        /// <summary>
		/// 
		/// select a row from table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEntitlement</returns>
		public returnGetEntitlement GetEntitlement(int ID)
		{
			returnGetEntitlement rv = new returnGetEntitlement();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_StandardActivity\",\"c_u_RoleType\",\"c_u_System\",\"c_u_Platform\",\"c_u_EntitlementName\",\"c_u_EntitlementValue\",\"c_u_AuthObjName\",\"c_u_AuthObjValue\",\"c_u_FieldSecName\",\"c_u_FieldSecValue\",\"c_u_Level4SecName\",\"c_u_Level4SecValue\",\"c_u_Commentary\",\"c_u_GENmanifestValue\",\"c_u_Application\",\"c_u_CHECKSUM\",\"c_u_Status\" from \"t_RBSR_AUFW_u_Entitlement\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'StandardActivity'");
				else
					rv.StandardActivity = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'RoleType'");
				else
					rv.RoleType = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'System'");
				else
					rv.System = dr.GetString(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'Platform'");
				else
					rv.Platform = dr.GetString(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'EntitlementName'");
				else
					rv.EntitlementName = dr.GetString(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'EntitlementValue'");
				else
					rv.EntitlementValue = dr.GetString(6);
				if (dr.IsDBNull(7))
					rv.AuthObjName = null;
				else
					rv.AuthObjName = dr.GetString(7);
				if (dr.IsDBNull(8))
					rv.AuthObjValue = null;
				else
					rv.AuthObjValue = dr.GetString(8);
				if (dr.IsDBNull(9))
					rv.FieldSecName = null;
				else
					rv.FieldSecName = dr.GetString(9);
				if (dr.IsDBNull(10))
					rv.FieldSecValue = null;
				else
					rv.FieldSecValue = dr.GetString(10);
				if (dr.IsDBNull(11))
					rv.Level4SecName = null;
				else
					rv.Level4SecName = dr.GetString(11);
				if (dr.IsDBNull(12))
					rv.Level4SecValue = null;
				else
					rv.Level4SecValue = dr.GetString(12);
				if (dr.IsDBNull(13))
					rv.Commentary = null;
				else
					rv.Commentary = dr.GetString(13);
				if (dr.IsDBNull(14))
					rv.GENmanifestValue = null;
				else
					rv.GENmanifestValue = dr.GetString(14);
				if (dr.IsDBNull(15))
					throw new Exception("Value 'null' is not allowed for 'Application'");
				else
					rv.Application = dr.GetString(15);
				if (dr.IsDBNull(16))
					throw new Exception("Value 'null' is not allowed for 'CHECKSUM'");
				else
					rv.CHECKSUM = dr.GetString(16);
				if (dr.IsDBNull(17))
					rv.Status = null;
				else
					rv.Status = dr.GetString(17);
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
		/// update a row in table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="StandardActivity"></param>
		/// <param name="RoleType"></param>
		/// <param name="System"></param>
		/// <param name="Platform"></param>
		/// <param name="EntitlementName"></param>
		/// <param name="EntitlementValue"></param>
		/// <param name="AuthObjName"></param>
		/// <param name="AuthObjValue"></param>
		/// <param name="FieldSecName"></param>
		/// <param name="FieldSecValue"></param>
		/// <param name="Level4SecName"></param>
		/// <param name="Level4SecValue"></param>
		/// <param name="Commentary"></param>
		/// <param name="GENmanifestValue"></param>
		/// <param name="Application"></param>
		/// <param name="CHECKSUM"></param>
		/// <param name="Status"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetEntitlement(int ID, string StandardActivity, string RoleType, string System, string Platform, string EntitlementName, string EntitlementValue, string AuthObjName, string AuthObjValue, string FieldSecName, string FieldSecValue, string Level4SecName, string Level4SecValue, string Commentary, string GENmanifestValue, string Application, string CHECKSUM, string Status)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_Entitlement\" set \"c_u_StandardActivity\"=?,\"c_u_RoleType\"=?,\"c_u_System\"=?,\"c_u_Platform\"=?,\"c_u_EntitlementName\"=?,\"c_u_EntitlementValue\"=?,\"c_u_AuthObjName\"=?,\"c_u_AuthObjValue\"=?,\"c_u_FieldSecName\"=?,\"c_u_FieldSecValue\"=?,\"c_u_Level4SecName\"=?,\"c_u_Level4SecValue\"=?,\"c_u_Commentary\"=?,\"c_u_GENmanifestValue\"=?,\"c_u_Application\"=?,\"c_u_CHECKSUM\"=?,\"c_u_Status\"=? where \"c_id\" = ?";
			if (StandardActivity == null) throw new Exception("StandardActivity must not be null!");
			cmd.Parameters.Add("c_u_StandardActivity", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_StandardActivity"].Value = (StandardActivity != null ? (object)StandardActivity : DBNull.Value);
			if (RoleType == null) throw new Exception("RoleType must not be null!");
			cmd.Parameters.Add("c_u_RoleType", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_RoleType"].Value = (RoleType != null ? (object)RoleType : DBNull.Value);
			if (System == null) throw new Exception("System must not be null!");
			cmd.Parameters.Add("c_u_System", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_System"].Value = (System != null ? (object)System : DBNull.Value);
			if (Platform == null) throw new Exception("Platform must not be null!");
			cmd.Parameters.Add("c_u_Platform", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Platform"].Value = (Platform != null ? (object)Platform : DBNull.Value);
			if (EntitlementName == null) throw new Exception("EntitlementName must not be null!");
			cmd.Parameters.Add("c_u_EntitlementName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_EntitlementName"].Value = (EntitlementName != null ? (object)EntitlementName : DBNull.Value);
			if (EntitlementValue == null) throw new Exception("EntitlementValue must not be null!");
			cmd.Parameters.Add("c_u_EntitlementValue", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_EntitlementValue"].Value = (EntitlementValue != null ? (object)EntitlementValue : DBNull.Value);
			cmd.Parameters.Add("c_u_AuthObjName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_AuthObjName"].Value = (AuthObjName != null ? (object)AuthObjName : DBNull.Value);
			cmd.Parameters.Add("c_u_AuthObjValue", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_AuthObjValue"].Value = (AuthObjValue != null ? (object)AuthObjValue : DBNull.Value);
			cmd.Parameters.Add("c_u_FieldSecName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_FieldSecName"].Value = (FieldSecName != null ? (object)FieldSecName : DBNull.Value);
			cmd.Parameters.Add("c_u_FieldSecValue", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_FieldSecValue"].Value = (FieldSecValue != null ? (object)FieldSecValue : DBNull.Value);
			cmd.Parameters.Add("c_u_Level4SecName", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_Level4SecName"].Value = (Level4SecName != null ? (object)Level4SecName : DBNull.Value);
			cmd.Parameters.Add("c_u_Level4SecValue", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_Level4SecValue"].Value = (Level4SecValue != null ? (object)Level4SecValue : DBNull.Value);
			cmd.Parameters.Add("c_u_Commentary", OdbcType.NVarChar, 4000);
			cmd.Parameters["c_u_Commentary"].Value = (Commentary != null ? (object)Commentary : DBNull.Value);
			cmd.Parameters.Add("c_u_GENmanifestValue", OdbcType.NVarChar, 4000);
			cmd.Parameters["c_u_GENmanifestValue"].Value = (GENmanifestValue != null ? (object)GENmanifestValue : DBNull.Value);
			if (Application == null) throw new Exception("Application must not be null!");
			cmd.Parameters.Add("c_u_Application", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_Application"].Value = (Application != null ? (object)Application : DBNull.Value);
			if (CHECKSUM == null) throw new Exception("CHECKSUM must not be null!");
			cmd.Parameters.Add("c_u_CHECKSUM", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_CHECKSUM"].Value = (CHECKSUM != null ? (object)CHECKSUM : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_Entitlement.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListEntitlement[]</returns>
		public returnListEntitlement[] ListEntitlement(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListEntitlement[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			string runTimeCriteria = (extendedCriteria != null ? extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"standardactivity\"", "\"c_u_StandardActivity\"").Replace("\"roletype\"", "\"c_u_RoleType\"").Replace("\"application\"", "\"c_u_Application\"").Replace("\"system\"", "\"c_u_System\"").Replace("\"platform\"", "\"c_u_Platform\"").Replace("\"entitlementname\"", "\"c_u_EntitlementName\"").Replace("\"entitlementvalue\"", "\"c_u_EntitlementValue\"").Replace("\"authobjname\"", "\"c_u_AuthObjName\"").Replace("\"authobjvalue\"", "\"c_u_AuthObjValue\"").Replace("\"fieldsecname\"", "\"c_u_FieldSecName\"").Replace("\"fieldsecvalue\"", "\"c_u_FieldSecValue\"").Replace("\"level4secname\"", "\"c_u_Level4SecName\"").Replace("\"level4secvalue\"", "\"c_u_Level4SecValue\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"genmanifestvalue\"", "\"c_u_GENmanifestValue\"").Replace("\"checksum\"", "\"c_u_CHECKSUM\"").Replace("\"status\"", "\"c_u_Status\"");
			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"standardactivity\"", "\"c_u_StandardActivity\"").Replace("\"roletype\"", "\"c_u_RoleType\"").Replace("\"application\"", "\"c_u_Application\"").Replace("\"system\"", "\"c_u_System\"").Replace("\"platform\"", "\"c_u_Platform\"").Replace("\"entitlementname\"", "\"c_u_EntitlementName\"").Replace("\"entitlementvalue\"", "\"c_u_EntitlementValue\"").Replace("\"authobjname\"", "\"c_u_AuthObjName\"").Replace("\"authobjvalue\"", "\"c_u_AuthObjValue\"").Replace("\"fieldsecname\"", "\"c_u_FieldSecName\"").Replace("\"fieldsecvalue\"", "\"c_u_FieldSecValue\"").Replace("\"level4secname\"", "\"c_u_Level4SecName\"").Replace("\"level4secvalue\"", "\"c_u_Level4SecValue\"").Replace("\"commentary\"", "\"c_u_Commentary\"").Replace("\"genmanifestvalue\"", "\"c_u_GENmanifestValue\"").Replace("\"checksum\"", "\"c_u_CHECKSUM\"").Replace("\"status\"", "\"c_u_Status\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_StandardActivity\", \"c_u_RoleType\", \"c_u_System\", \"c_u_Platform\", \"c_u_EntitlementName\", \"c_u_EntitlementValue\", \"c_u_AuthObjName\", \"c_u_AuthObjValue\", \"c_u_FieldSecName\", \"c_u_FieldSecValue\", \"c_u_Level4SecName\", \"c_u_Level4SecValue\", \"c_u_Commentary\", \"c_u_GENmanifestValue\", \"c_u_Application\", \"c_u_CHECKSUM\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_Entitlement\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_StandardActivity\", \"c_u_RoleType\", \"c_u_System\", \"c_u_Platform\", \"c_u_EntitlementName\", \"c_u_EntitlementValue\", \"c_u_AuthObjName\", \"c_u_AuthObjValue\", \"c_u_FieldSecName\", \"c_u_FieldSecValue\", \"c_u_Level4SecName\", \"c_u_Level4SecValue\", \"c_u_Commentary\", \"c_u_GENmanifestValue\", \"c_u_Application\", \"c_u_CHECKSUM\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_Entitlement\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_StandardActivity\", \"c_u_RoleType\", \"c_u_System\", \"c_u_Platform\", \"c_u_EntitlementName\", \"c_u_EntitlementValue\", \"c_u_AuthObjName\", \"c_u_AuthObjValue\", \"c_u_FieldSecName\", \"c_u_FieldSecValue\", \"c_u_Level4SecName\", \"c_u_Level4SecValue\", \"c_u_Commentary\", \"c_u_GENmanifestValue\", \"c_u_Application\", \"c_u_CHECKSUM\", \"c_u_Status\" FROM \"t_RBSR_AUFW_u_Entitlement\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEntitlement> rvl = new List<returnListEntitlement>();
			while (dr.Read())
			{
				returnListEntitlement cr = new returnListEntitlement();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'StandardActivity'");
				else
					cr.StandardActivity = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'RoleType'");
				else
					cr.RoleType = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'System'");
				else
					cr.System = dr.GetString(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'Platform'");
				else
					cr.Platform = dr.GetString(4);
				if (dr.IsDBNull(5))
					throw new Exception("Value 'null' is not allowed for 'EntitlementName'");
				else
					cr.EntitlementName = dr.GetString(5);
				if (dr.IsDBNull(6))
					throw new Exception("Value 'null' is not allowed for 'EntitlementValue'");
				else
					cr.EntitlementValue = dr.GetString(6);
				if (dr.IsDBNull(7))
					cr.AuthObjName = null;
				else
					cr.AuthObjName = dr.GetString(7);
				if (dr.IsDBNull(8))
					cr.AuthObjValue = null;
				else
					cr.AuthObjValue = dr.GetString(8);
				if (dr.IsDBNull(9))
					cr.FieldSecName = null;
				else
					cr.FieldSecName = dr.GetString(9);
				if (dr.IsDBNull(10))
					cr.FieldSecValue = null;
				else
					cr.FieldSecValue = dr.GetString(10);
				if (dr.IsDBNull(11))
					cr.Level4SecName = null;
				else
					cr.Level4SecName = dr.GetString(11);
				if (dr.IsDBNull(12))
					cr.Level4SecValue = null;
				else
					cr.Level4SecValue = dr.GetString(12);
				if (dr.IsDBNull(13))
					cr.Commentary = null;
				else
					cr.Commentary = dr.GetString(13);
				if (dr.IsDBNull(14))
					cr.GENmanifestValue = null;
				else
					cr.GENmanifestValue = dr.GetString(14);
				if (dr.IsDBNull(15))
					throw new Exception("Value 'null' is not allowed for 'Application'");
				else
					cr.Application = dr.GetString(15);
				if (dr.IsDBNull(16))
					throw new Exception("Value 'null' is not allowed for 'CHECKSUM'");
				else
					cr.CHECKSUM = dr.GetString(16);
				if (dr.IsDBNull(17))
					cr.Status = null;
				else
					cr.Status = dr.GetString(17);
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
