/*
Major DFSklar changes here.
The listing of bus roles now automatically hides any
busrole whose name includes the "//DEL" sentinel for deleted roles.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.186 (#227)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IBusRole
{
	/// <summary>
	/// Return value from method GetBusRole
	/// </summary>
	public struct returnGetBusRole
	{
		public int ID;
		public string Name;
		public string Description;
		public int SubProcessID;
		public string Abbrev;
		public string OwnerPrimaryEID;
		public string OwnerSecondaryEID;
		public string DesignDetails;
        public string RoleType_Abbrev;
        public string RoleType_Displayable;
	}
	/// <summary>
	/// Return value from method ListBusRole
	/// </summary>
	public struct returnListBusRole
	{
		public int ID;
		public string Name;
		public string Description;
		public int SubProcessID;
		public string Abbrev;
		public string OwnerPrimaryEID;
		public string OwnerSecondaryEID;
		public string DesignDetails;
        public string RoleType_Abbrev;
        public string RoleType_Displayable;
	}
	/// <summary>
	/// Return value from method ListBusRoleBySubProcess
	/// </summary>
	public struct returnListBusRoleBySubProcess
	{
		public int ID;
		public string Name;
		public string Description;
		public int SubProcessID;
		public string Abbrev;
		public string OwnerPrimaryEID;
		public string OwnerSecondaryEID;
		public string DesignDetails;
        public string RoleType_Abbrev;
        public string RoleType_Displayable;
    }
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_BusRole
	/// </summary>
	public class IBusRole
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
		public IBusRole() : this((OdbcConnection)null) { }
		public IBusRole(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IBusRole(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
            FEATUREroletype =
    ", \"c_u_RoleType\" as RoleTypeAbbrev, (select Displayable from DICT_BusRoleType where Abbrev=c_u_RoleType) as RoleTypeDisplayable ";

		}

        public string FEATUREroletype;
        
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
		/// insert a row in table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="SubProcessID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewBusRole(string Name, string Description, int SubProcessID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_BusRole\"(\"c_u_Name\",\"c_u_Description\",\"c_r_SubProcess\") VALUES(?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (Name == null)  { cmd.Dispose(); DBClose(); throw new Exception("Name must not be null!"); } 
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			if (Description == null)  { cmd.Dispose(); DBClose(); throw new Exception("Description must not be null!"); } 
			cmd.Parameters.Add("c_u_Description", OdbcType.NVarChar, 250);
			cmd.Parameters["c_u_Description"].Value = (Description != null ? (object)Description : DBNull.Value);
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
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
		/// delete a row from table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteBusRole(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_BusRole\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetBusRole</returns>
		public returnGetBusRole GetBusRole(int ID)
		{
			returnGetBusRole rv = new returnGetBusRole();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "select \"c_id\",\"c_u_Name\",\"c_u_Description\",\"c_r_SubProcess\",\"c_u_Abbrev\",\"c_u_OwnerPrimaryEID\",\"c_u_OwnerSecondaryEID\",\"c_u_DesignDetails\"" + FEATUREroletype + " from \"t_RBSR_AUFW_u_BusRole\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Name'"); } 
				else
					rv.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Description'"); } 
				else
					rv.Description = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					rv.SubProcessID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					rv.Abbrev = null;
				else
					rv.Abbrev = dr.GetString(4);
				if (dr.IsDBNull(5))
					rv.OwnerPrimaryEID = null;
				else
					rv.OwnerPrimaryEID = dr.GetString(5);
				if (dr.IsDBNull(6))
					rv.OwnerSecondaryEID = null;
				else
					rv.OwnerSecondaryEID = dr.GetString(6);
				if (dr.IsDBNull(7))
					rv.DesignDetails = null;
				else
					rv.DesignDetails = dr.GetString(7);

                if (dr.IsDBNull(8))
                    rv.RoleType_Abbrev = null;
                else
                    rv.RoleType_Abbrev = dr.GetString(8);
                if (dr.IsDBNull(9))
                    rv.RoleType_Displayable = null;
                else
                    rv.RoleType_Displayable = dr.GetString(9);


			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Name"></param>
		/// <param name="Description"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="Abbrev"></param>
		/// <param name="OwnerPrimaryEID"></param>
		/// <param name="OwnerSecondaryEID"></param>
		/// <param name="DesignDetails"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetBusRole(int ID, string Name, string Description, int SubProcessID, string Abbrev, string OwnerPrimaryEID, string OwnerSecondaryEID, string DesignDetails)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_BusRole\" set \"c_u_Name\"=?,\"c_u_Description\"=?,\"c_r_SubProcess\"=?,\"c_u_Abbrev\"=?,\"c_u_OwnerPrimaryEID\"=?,\"c_u_OwnerSecondaryEID\"=?,\"c_u_DesignDetails\"=? where \"c_id\" = ?";
			if (Name == null)  { cmd.Dispose(); DBClose(); throw new Exception("Name must not be null!"); } 
			cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 100);
			cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
			if (Description == null)  { cmd.Dispose(); DBClose(); throw new Exception("Description must not be null!"); } 
			cmd.Parameters.Add("c_u_Description", OdbcType.NVarChar, 250);
			cmd.Parameters["c_u_Description"].Value = (Description != null ? (object)Description : DBNull.Value);
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			cmd.Parameters.Add("c_u_Abbrev", OdbcType.NVarChar, 10);
			cmd.Parameters["c_u_Abbrev"].Value = (Abbrev != null ? (object)Abbrev : DBNull.Value);
			cmd.Parameters.Add("c_u_OwnerPrimaryEID", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_OwnerPrimaryEID"].Value = (OwnerPrimaryEID != null ? (object)OwnerPrimaryEID : DBNull.Value);
			cmd.Parameters.Add("c_u_OwnerSecondaryEID", OdbcType.NVarChar, 50);
			cmd.Parameters["c_u_OwnerSecondaryEID"].Value = (OwnerSecondaryEID != null ? (object)OwnerSecondaryEID : DBNull.Value);
			cmd.Parameters.Add("c_u_DesignDetails", OdbcType.NText);
			cmd.Parameters["c_u_DesignDetails"].Value = (DesignDetails != null ? (object)DesignDetails : DBNull.Value);
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();
			if (rv != 1)  { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}





	  // DFSklar CREATED THIS FUNCTION MANUALLY:
	  // DFSklar CREATED THIS FUNCTION MANUALLY:
	  // DFSklar CREATED THIS FUNCTION MANUALLY:
	  // DFSklar CREATED THIS FUNCTION MANUALLY:
	  public int SetBusRole(int ID, string Name, string Description, string OwnerPrimaryEID, string OwnerSecondaryEID, string DesignDetails)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "update \"t_RBSR_AUFW_u_BusRole\" set \"c_u_Name\"=?,\"c_u_Description\"=?,\"c_u_OwnerPrimaryEID\"=?,\"c_u_OwnerSecondaryEID\"=?,\"c_u_DesignDetails\"=? where \"c_id\" = ?";
            if (Name == null)  { cmd.Dispose(); DBClose(); throw new Exception("Name must not be null!"); } 
            cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 100);
            cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
            if (Description == null)  { cmd.Dispose(); DBClose(); throw new Exception("Description must not be null!"); } 
            cmd.Parameters.Add("c_u_Description", OdbcType.NVarChar, 250);
            cmd.Parameters["c_u_Description"].Value = (Description != null ? (object)Description : DBNull.Value);
            cmd.Parameters.Add("c_u_OwnerPrimaryEID", OdbcType.NVarChar, 50);
            cmd.Parameters["c_u_OwnerPrimaryEID"].Value = (OwnerPrimaryEID != null ? (object)OwnerPrimaryEID : DBNull.Value);
            cmd.Parameters.Add("c_u_OwnerSecondaryEID", OdbcType.NVarChar, 50);
            cmd.Parameters["c_u_OwnerSecondaryEID"].Value = (OwnerSecondaryEID != null ? (object)OwnerSecondaryEID : DBNull.Value);
	    cmd.Parameters.Add("c_u_DesignDetails", OdbcType.NText);
	    cmd.Parameters["c_u_DesignDetails"].Value = (DesignDetails != null ? (object)DesignDetails : DBNull.Value);
            cmd.Parameters.Add("c_id", OdbcType.Int);
            cmd.Parameters["c_id"].Value = (object)ID;
            cmd.Connection = _dbConnection;
            rv = cmd.ExecuteNonQuery();
            if (rv != 1)  { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); } 
            cmd.Dispose();
            DBClose();
            return rv;
        }
	  // END OF DFSKLAR MANUAL ADDITION.
	  // END OF DFSKLAR MANUAL ADDITION.












        public int SetBusRole(int ID, string Name, string Description, int SubProcessID, string Abbrev)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "update \"t_RBSR_AUFW_u_BusRole\" set \"c_u_Name\"=?,\"c_u_Description\"=?,\"c_r_SubProcess\"=?,\"c_u_Abbrev\"=? where \"c_id\" = ?";
            if (Name == null)  { cmd.Dispose(); DBClose(); throw new Exception("Name must not be null!"); } 
            cmd.Parameters.Add("c_u_Name", OdbcType.NVarChar, 100);
            cmd.Parameters["c_u_Name"].Value = (Name != null ? (object)Name : DBNull.Value);
            if (Description == null)  { cmd.Dispose(); DBClose(); throw new Exception("Description must not be null!"); } 
            cmd.Parameters.Add("c_u_Description", OdbcType.NVarChar, 250);
            cmd.Parameters["c_u_Description"].Value = (Description != null ? (object)Description : DBNull.Value);
            cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
            cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
            cmd.Parameters.Add("c_u_Abbrev", OdbcType.NVarChar, 10);
            cmd.Parameters["c_u_Abbrev"].Value = (Abbrev != null ? (object)Abbrev : DBNull.Value);
            cmd.Parameters.Add("c_id", OdbcType.Int);
            cmd.Parameters["c_id"].Value = (object)ID;
            cmd.Connection = _dbConnection;
            rv = cmd.ExecuteNonQuery();
            if (rv != 1)  { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); } 
            cmd.Dispose();
            DBClose();
            return rv;
        }



// DFSKLAR CREATED THIS MANUALLY
        public int SetBusRole(int ID, string RoleTypeAbbrev)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "update \"t_RBSR_AUFW_u_BusRole\" set \"c_u_RoleType\"=? where \"c_id\" = ?";
            cmd.Parameters.Add("c_u_RoleType", OdbcType.NVarChar, 100);
            cmd.Parameters["c_u_RoleType"].Value = (RoleTypeAbbrev != null ? (object)RoleTypeAbbrev : DBNull.Value);
            cmd.Parameters.Add("c_id", OdbcType.Int);
            cmd.Parameters["c_id"].Value = (object)ID;
            cmd.Connection = _dbConnection;
            rv = cmd.ExecuteNonQuery();
            if (rv != 1) { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); }
            cmd.Dispose();
            DBClose();
            return rv;
        }


        // DFSklar CREATED THIS FUNCTION MANUALLY:
        // DFSklar CREATED THIS FUNCTION MANUALLY:
        // DFSklar CREATED THIS FUNCTION MANUALLY:
        // DFSklar CREATED THIS FUNCTION MANUALLY:
        public int MoveBusRoleToTrashcan
            (int ID, int SubProcessID)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "update \"t_RBSR_AUFW_u_BusRole\" set \"c_u_Name\"=\"c_u_Name\"+'//DEL_'+?, \"c_r_SubProcess\"=? where \"c_id\" = ?";
            cmd.Parameters.Add("c_id2", OdbcType.NVarChar, 10);
            cmd.Parameters["c_id2"].Value = (object)(ID.ToString());
            cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
            cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
            cmd.Parameters.Add("c_id", OdbcType.Int);
            cmd.Parameters["c_id"].Value = (object)ID;
            cmd.Connection = _dbConnection;
            rv = cmd.ExecuteNonQuery();
            if (rv != 1) { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); }
            cmd.Dispose();
            DBClose();
            return rv;
        }
        // END OF SKLAR CHANGES
        // END OF SKLAR CHANGES
        // END OF SKLAR CHANGES
        // END OF SKLAR CHANGES
        // END OF SKLAR CHANGES






        /// <summary>
		/// 
		/// select a set of rows from table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListBusRole[]</returns>
		public returnListBusRole[] ListBusRole(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			returnListBusRole[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();

			if (extendedCriteria == null)
			  extendedCriteria = "";

			string runTimeCriteria = 
			  " c_u_Name NOT LIKE '%//DEL%' " +
			  (extendedCriteria.Length>0 ? " AND " + extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"abbrev\"", "\"c_u_Abbrev\"").Replace("\"ownerprimaryeid\"", "\"c_u_OwnerPrimaryEID\"").Replace("\"ownersecondaryeid\"", "\"c_u_OwnerSecondaryEID\"").Replace("\"designdetails\"", "\"c_u_DesignDetails\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"");


			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"abbrev\"", "\"c_u_Abbrev\"").Replace("\"ownerprimaryeid\"", "\"c_u_OwnerPrimaryEID\"").Replace("\"ownersecondaryeid\"", "\"c_u_OwnerSecondaryEID\"").Replace("\"designdetails\"", "\"c_u_DesignDetails\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"");

            
            if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_Abbrev\", \"c_u_OwnerPrimaryEID\", \"c_u_OwnerSecondaryEID\", \"c_u_DesignDetails\"" + FEATUREroletype + " FROM \"t_RBSR_AUFW_u_BusRole\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
                    cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_Abbrev\", \"c_u_OwnerPrimaryEID\", \"c_u_OwnerSecondaryEID\", \"c_u_DesignDetails\"" + FEATUREroletype + " FROM \"t_RBSR_AUFW_u_BusRole\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
                cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_Abbrev\", \"c_u_OwnerPrimaryEID\", \"c_u_OwnerSecondaryEID\", \"c_u_DesignDetails\"" + FEATUREroletype + " FROM \"t_RBSR_AUFW_u_BusRole\"" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " where (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListBusRole> rvl = new List<returnListBusRole>();
			while (dr.Read())
			{
				returnListBusRole cr = new returnListBusRole();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Name'"); } 
				else
					cr.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Description'"); } 
				else
					cr.Description = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					cr.Abbrev = null;
				else
					cr.Abbrev = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.OwnerPrimaryEID = null;
				else
					cr.OwnerPrimaryEID = dr.GetString(5);
				if (dr.IsDBNull(6))
					cr.OwnerSecondaryEID = null;
				else
					cr.OwnerSecondaryEID = dr.GetString(6);
				if (dr.IsDBNull(7))
					cr.DesignDetails = null;
				else
					cr.DesignDetails = dr.GetString(7);

                if (dr.IsDBNull(8))
                    cr.RoleType_Abbrev = null;
                else
                    cr.RoleType_Abbrev = dr.GetString(8);
                if (dr.IsDBNull(9))
                    cr.RoleType_Displayable = null;
                else
                    cr.RoleType_Displayable = dr.GetString(9);

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
		/// select a set of rows from table t_RBSR_AUFW_u_BusRole.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="SubProcessID"></param>
		/// <returns>returnListBusRoleBySubProcess[]</returns>
		public returnListBusRoleBySubProcess[] ListBusRoleBySubProcess(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int SubProcessID)
		{
			returnListBusRoleBySubProcess[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();

			if (extendedCriteria == null)
			  extendedCriteria = "";


			string runTimeCriteria = 
			  " c_u_Name NOT LIKE '%//DEL%' " +
			  (extendedCriteria.Length>0 ? " AND " + extendedCriteria.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"abbrev\"", "\"c_u_Abbrev\"").Replace("\"ownerprimaryeid\"", "\"c_u_OwnerPrimaryEID\"").Replace("\"ownersecondaryeid\"", "\"c_u_OwnerSecondaryEID\"").Replace("\"designdetails\"", "\"c_u_DesignDetails\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"");

			string runTimeSortOrder = (extendedSortOrder != null ? extendedSortOrder.ToLower() : string.Empty).Replace("\"id\"", "\"c_id\"").Replace("\"name\"", "\"c_u_Name\"").Replace("\"description\"", "\"c_u_Description\"").Replace("\"abbrev\"", "\"c_u_Abbrev\"").Replace("\"ownerprimaryeid\"", "\"c_u_OwnerPrimaryEID\"").Replace("\"ownersecondaryeid\"", "\"c_u_OwnerSecondaryEID\"").Replace("\"designdetails\"", "\"c_u_DesignDetails\"").Replace("\"subprocess\"", "\"c_r_SubProcess\"");
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
                    cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_Abbrev\", \"c_u_OwnerPrimaryEID\", \"c_u_OwnerSecondaryEID\", \"c_u_DesignDetails\"" + FEATUREroletype + " FROM \"t_RBSR_AUFW_u_BusRole\" WHERE \"c_r_SubProcess\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
				else
                    cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_Abbrev\", \"c_u_OwnerPrimaryEID\", \"c_u_OwnerSecondaryEID\", \"c_u_DesignDetails\"" + FEATUREroletype + " FROM \"t_RBSR_AUFW_u_BusRole\" WHERE \"c_r_SubProcess\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder) + " LIMIT " + maxRowsToReturn.Value;
			}
			else
                cmd.CommandText = "SELECT \"c_id\", \"c_u_Name\", \"c_u_Description\", \"c_r_SubProcess\", \"c_u_Abbrev\", \"c_u_OwnerPrimaryEID\", \"c_u_OwnerSecondaryEID\", \"c_u_DesignDetails\"" + FEATUREroletype + " FROM \"t_RBSR_AUFW_u_BusRole\" WHERE \"c_r_SubProcess\"=?" + (string.IsNullOrEmpty(runTimeCriteria) ? "" : " and (" + runTimeCriteria + ")") + (string.IsNullOrEmpty(runTimeSortOrder) ? "" : " order by " + runTimeSortOrder);
			cmd.Parameters.Add("1_SubProcessID", OdbcType.Int);
			cmd.Parameters["1_SubProcessID"].Value = (object)SubProcessID;
			for(int i=0; i<extendedParameters.Length; i++)
			{
				cmd.Parameters.Add("@ExtendedParam_"+i.ToString(),OdbcType.NVarChar);
				cmd.Parameters["@ExtendedParam_"+i.ToString()].Value = (object)extendedParameters[i];
			}
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListBusRoleBySubProcess> rvl = new List<returnListBusRoleBySubProcess>();
			while (dr.Read())
			{
				returnListBusRoleBySubProcess cr = new returnListBusRoleBySubProcess();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Name'"); } 
				else
					cr.Name = dr.GetString(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Description'"); } 
				else
					cr.Description = dr.GetString(2);
				if (dr.IsDBNull(3))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(3);
				if (dr.IsDBNull(4))
					cr.Abbrev = null;
				else
					cr.Abbrev = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.OwnerPrimaryEID = null;
				else
					cr.OwnerPrimaryEID = dr.GetString(5);
				if (dr.IsDBNull(6))
					cr.OwnerSecondaryEID = null;
				else
					cr.OwnerSecondaryEID = dr.GetString(6);
				if (dr.IsDBNull(7))
					cr.DesignDetails = null;
				else
					cr.DesignDetails = dr.GetString(7);

                if (dr.IsDBNull(8))
                    cr.RoleType_Abbrev = null;
                else
                    cr.RoleType_Abbrev = dr.GetString(8);
                if (dr.IsDBNull(9))
                    cr.RoleType_Displayable = null;
                else
                    cr.RoleType_Displayable = dr.GetString(9);

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
