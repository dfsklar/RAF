using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//c5239606-e81b-4173-a133-8a3bf82326d1
//
// New Model (RBSR_AUFW)
// Version 1.374 (#382)
//
// 
// 
//
//c5239606-e81b-4173-a133-8a3bf82326d1

namespace RBSR_AUFW.DB.IUser1
{
	/// <summary>
	/// Return value from method GetUser
	/// </summary>
	public struct returnGetUser
	{
		public int ID;
		public string EID;
		public string Name;
	}
	/// <summary>
	/// Return value from method ListUser
	/// </summary>
	public struct returnListUser
	{
		public int ID;
		public string EID;
		public string Name;
	}
	/// <summary>
	/// Return value from method GetUserEditingWorkspace
	/// </summary>
	public struct returnGetUserEditingWorkspace
	{
		public int ID;
		public int UserID;
		public int EditingWorkspaceID;
	}
	/// <summary>
	/// Return value from method ListUserEditingWorkspace
	/// </summary>
	public struct returnListUserEditingWorkspace
	{
		public int ID;
		public int UserID;
		public int EditingWorkspaceID;
	}
	/// <summary>
	/// Return value from method GetUserEntAssignmentSet
	/// </summary>
	public struct returnGetUserEntAssignmentSet
	{
		public int ID;
		public int UserID;
		public int EntAssignmentSetID;
	}
	/// <summary>
	/// Return value from method ListUserEntAssignmentSet
	/// </summary>
	public struct returnListUserEntAssignmentSet
	{
		public int ID;
		public int UserID;
		public int EntAssignmentSetID;
	}
	/// <summary>
	/// Return value from method GetUserTcodeAssignmentSet
	/// </summary>
	public struct returnGetUserTcodeAssignmentSet
	{
		public int ID;
		public int UserID;
		public int TcodeAssignmentSetID;
	}
	/// <summary>
	/// Return value from method ListUserTcodeAssignmentSet
	/// </summary>
	public struct returnListUserTcodeAssignmentSet
	{
		public int ID;
		public int UserID;
		public int TcodeAssignmentSetID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_User
	///     Table t_RBSR_AUFW_r_UserEditingWorkspace
	///     Table t_RBSR_AUFW_r_UserEntAssignmentSet
	///     Table t_RBSR_AUFW_r_UserTcodeAssignmentSet
	/// </summary>
	public class IUser1
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
		public IUser1() {}
		public IUser1(string connectionString)
		{
			_dbConnection = new OdbcConnection(connectionString);
		}
		public IUser1(OdbcConnection dbConnection)
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
			else if (driver.StartsWith("sql"))
			{
				if (spec.ToLower()=="get id") return "; select convert(int,SCOPE_IDENTITY())";
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
		/// insert a row in table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="EID"></param>
		/// <param name="Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewUser(string EID, string Name)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "insert into \"t_RBSR_AUFW_u_User\" (\"c_u_EID\",\"c_u_Name\") values(?,?) " + SpecSQL("get id");
			if(EID == null)  { cmd.Dispose(); DBClose(); throw new Exception("EID must not be null!"); } 
			cmd.Parameters.Add("c_u_EID",OdbcType.NVarChar,50);
			cmd.Parameters["c_u_EID"].Value = (EID!= null ? (object)EID : DBNull.Value);
			if(Name == null)  { cmd.Dispose(); DBClose(); throw new Exception("Name must not be null!"); } 
			cmd.Parameters.Add("c_u_Name",OdbcType.NVarChar,50);
			cmd.Parameters["c_u_Name"].Value = (Name!= null ? (object)Name : DBNull.Value);
			cmd.Connection = _dbConnection;
OdbcDataReader dri=null; try{dri=cmd.ExecuteReader();}catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
try{dri.Read();} catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
			rv = (dri.IsDBNull(0) ? 0 : dri.GetInt32(0));
			dri.Close();
			dri.Dispose();
			if (rv == 0)  { cmd.Dispose(); DBClose(); throw new Exception("Insert operation failed!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// delete a row from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteUser(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_User\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUser</returns>
		public returnGetUser GetUser(int ID)
		{
			returnGetUser rv = new returnGetUser();
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_EID\",\"c_u_Name\" from \"t_RBSR_AUFW_u_User\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				}
				if(!dr.IsDBNull(1))
				{
					rv.EID = dr.GetString(1);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EID'"); } 
				}
				if(!dr.IsDBNull(2))
				{
					rv.Name = dr.GetString(2);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Name'"); } 
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
		/// update a row in table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EID"></param>
		/// <param name="Name"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetUser(int ID, string EID, string Name)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_User\" set \"c_u_EID\"=?,\"c_u_Name\"=? where \"c_id\" = ?";
			if(EID == null)  { cmd.Dispose(); DBClose(); throw new Exception("EID must not be null!"); } 
			cmd.Parameters.Add("c_u_EID",OdbcType.NVarChar,50);
			cmd.Parameters["c_u_EID"].Value = (EID!= null ? (object)EID : DBNull.Value);
			if(Name == null)  { cmd.Dispose(); DBClose(); throw new Exception("Name must not be null!"); } 
			cmd.Parameters.Add("c_u_Name",OdbcType.NVarChar,50);
			cmd.Parameters["c_u_Name"].Value = (Name!= null ? (object)Name : DBNull.Value);
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
		/// select a set of rows from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListUser[]</returns>
		public returnListUser[] ListUser(int? maxRowsToReturn)
		{
			returnListUser[] rv = null;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select" + (maxRowsToReturn.HasValue && maxRowsToReturn.Value!=0 ? " top " + maxRowsToReturn.Value.ToString() : "") + " " + "\"c_id\",\"c_u_EID\",\"c_u_Name\" from \"t_RBSR_AUFW_u_User\"";
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListUser> rvl = new List<returnListUser>();
			while(dr.Read())
			{
				returnListUser cr = new returnListUser();
				if(!dr.IsDBNull(0))
				{
					cr.ID = dr.GetInt32(0);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				}
				if(!dr.IsDBNull(1))
				{
					cr.EID = dr.GetString(1);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EID'"); } 
				}
				if(!dr.IsDBNull(2))
				{
					cr.Name = dr.GetString(2);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'Name'"); } 
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
		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="EditingWorkspaceID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewUserEditingWorkspace(int UserID, int EditingWorkspaceID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "insert into \"t_RBSR_AUFW_r_UserEditingWorkspace\" (\"c_r_User\",\"c_r_EditingWorkspace\") values(?,?) " + SpecSQL("get id");
			cmd.Parameters.Add("c_r_User",OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			cmd.Parameters.Add("c_r_EditingWorkspace",OdbcType.Int);
			cmd.Parameters["c_r_EditingWorkspace"].Value = (object)EditingWorkspaceID;
			cmd.Connection = _dbConnection;
OdbcDataReader dri=null; try{dri=cmd.ExecuteReader();}catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
try{dri.Read();} catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
			rv = (dri.IsDBNull(0) ? 0 : dri.GetInt32(0));
			dri.Close();
			dri.Dispose();
			if (rv == 0)  { cmd.Dispose(); DBClose(); throw new Exception("Insert operation failed!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// delete a row from table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteUserEditingWorkspace(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_r_UserEditingWorkspace\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUserEditingWorkspace</returns>
		public returnGetUserEditingWorkspace GetUserEditingWorkspace(int ID)
		{
			returnGetUserEditingWorkspace rv = new returnGetUserEditingWorkspace();
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select \"c_id\",\"c_r_User\",\"c_r_EditingWorkspace\" from \"t_RBSR_AUFW_r_UserEditingWorkspace\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				}
				if(!dr.IsDBNull(1))
				{
					rv.UserID = dr.GetInt32(1);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				}
				if(!dr.IsDBNull(2))
				{
					rv.EditingWorkspaceID = dr.GetInt32(2);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditingWorkspaceID'"); } 
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
		/// update a row in table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="UserID"></param>
		/// <param name="EditingWorkspaceID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetUserEditingWorkspace(int ID, int UserID, int EditingWorkspaceID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_r_UserEditingWorkspace\" set \"c_r_User\"=?,\"c_r_EditingWorkspace\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_r_User",OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			cmd.Parameters.Add("c_r_EditingWorkspace",OdbcType.Int);
			cmd.Parameters["c_r_EditingWorkspace"].Value = (object)EditingWorkspaceID;
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
		/// select a set of rows from table t_RBSR_AUFW_r_UserEditingWorkspace.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListUserEditingWorkspace[]</returns>
		public returnListUserEditingWorkspace[] ListUserEditingWorkspace(int? maxRowsToReturn)
		{
			returnListUserEditingWorkspace[] rv = null;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select" + (maxRowsToReturn.HasValue && maxRowsToReturn.Value!=0 ? " top " + maxRowsToReturn.Value.ToString() : "") + " " + "\"c_id\",\"c_r_User\",\"c_r_EditingWorkspace\" from \"t_RBSR_AUFW_r_UserEditingWorkspace\"";
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListUserEditingWorkspace> rvl = new List<returnListUserEditingWorkspace>();
			while(dr.Read())
			{
				returnListUserEditingWorkspace cr = new returnListUserEditingWorkspace();
				if(!dr.IsDBNull(0))
				{
					cr.ID = dr.GetInt32(0);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				}
				if(!dr.IsDBNull(1))
				{
					cr.UserID = dr.GetInt32(1);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				}
				if(!dr.IsDBNull(2))
				{
					cr.EditingWorkspaceID = dr.GetInt32(2);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EditingWorkspaceID'"); } 
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
		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewUserEntAssignmentSet(int UserID, int EntAssignmentSetID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "insert into \"t_RBSR_AUFW_r_UserEntAssignmentSet\" (\"c_r_User\",\"c_r_EntAssignmentSet\") values(?,?) " + SpecSQL("get id");
			cmd.Parameters.Add("c_r_User",OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			cmd.Parameters.Add("c_r_EntAssignmentSet",OdbcType.Int);
			cmd.Parameters["c_r_EntAssignmentSet"].Value = (object)EntAssignmentSetID;
			cmd.Connection = _dbConnection;
OdbcDataReader dri=null; try{dri=cmd.ExecuteReader();}catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
try{dri.Read();} catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
			rv = (dri.IsDBNull(0) ? 0 : dri.GetInt32(0));
			dri.Close();
			dri.Dispose();
			if (rv == 0)  { cmd.Dispose(); DBClose(); throw new Exception("Insert operation failed!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// delete a row from table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteUserEntAssignmentSet(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_r_UserEntAssignmentSet\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUserEntAssignmentSet</returns>
		public returnGetUserEntAssignmentSet GetUserEntAssignmentSet(int ID)
		{
			returnGetUserEntAssignmentSet rv = new returnGetUserEntAssignmentSet();
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select \"c_id\",\"c_r_User\",\"c_r_EntAssignmentSet\" from \"t_RBSR_AUFW_r_UserEntAssignmentSet\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				}
				if(!dr.IsDBNull(1))
				{
					rv.UserID = dr.GetInt32(1);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				}
				if(!dr.IsDBNull(2))
				{
					rv.EntAssignmentSetID = dr.GetInt32(2);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
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
		/// update a row in table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="UserID"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetUserEntAssignmentSet(int ID, int UserID, int EntAssignmentSetID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_r_UserEntAssignmentSet\" set \"c_r_User\"=?,\"c_r_EntAssignmentSet\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_r_User",OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			cmd.Parameters.Add("c_r_EntAssignmentSet",OdbcType.Int);
			cmd.Parameters["c_r_EntAssignmentSet"].Value = (object)EntAssignmentSetID;
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
		/// select a set of rows from table t_RBSR_AUFW_r_UserEntAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListUserEntAssignmentSet[]</returns>
		public returnListUserEntAssignmentSet[] ListUserEntAssignmentSet(int? maxRowsToReturn)
		{
			returnListUserEntAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select" + (maxRowsToReturn.HasValue && maxRowsToReturn.Value!=0 ? " top " + maxRowsToReturn.Value.ToString() : "") + " " + "\"c_id\",\"c_r_User\",\"c_r_EntAssignmentSet\" from \"t_RBSR_AUFW_r_UserEntAssignmentSet\"";
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListUserEntAssignmentSet> rvl = new List<returnListUserEntAssignmentSet>();
			while(dr.Read())
			{
				returnListUserEntAssignmentSet cr = new returnListUserEntAssignmentSet();
				if(!dr.IsDBNull(0))
				{
					cr.ID = dr.GetInt32(0);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				}
				if(!dr.IsDBNull(1))
				{
					cr.UserID = dr.GetInt32(1);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				}
				if(!dr.IsDBNull(2))
				{
					cr.EntAssignmentSetID = dr.GetInt32(2);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'"); } 
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
		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewUserTcodeAssignmentSet(int UserID, int TcodeAssignmentSetID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "insert into \"t_RBSR_AUFW_r_UserTcodeAssignmentSet\" (\"c_r_User\",\"c_r_TcodeAssignmentSet\") values(?,?) " + SpecSQL("get id");
			cmd.Parameters.Add("c_r_User",OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			cmd.Parameters.Add("c_r_TcodeAssignmentSet",OdbcType.Int);
			cmd.Parameters["c_r_TcodeAssignmentSet"].Value = (object)TcodeAssignmentSetID;
			cmd.Connection = _dbConnection;
OdbcDataReader dri=null; try{dri=cmd.ExecuteReader();}catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
try{dri.Read();} catch(Exception edri){cmd.Dispose();DBClose();throw edri;}
			rv = (dri.IsDBNull(0) ? 0 : dri.GetInt32(0));
			dri.Close();
			dri.Dispose();
			if (rv == 0)  { cmd.Dispose(); DBClose(); throw new Exception("Insert operation failed!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// delete a row from table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteUserTcodeAssignmentSet(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_r_UserTcodeAssignmentSet\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUserTcodeAssignmentSet</returns>
		public returnGetUserTcodeAssignmentSet GetUserTcodeAssignmentSet(int ID)
		{
			returnGetUserTcodeAssignmentSet rv = new returnGetUserTcodeAssignmentSet();
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select \"c_id\",\"c_r_User\",\"c_r_TcodeAssignmentSet\" from \"t_RBSR_AUFW_r_UserTcodeAssignmentSet\" where \"c_id\"= ?";
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
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				}
				if(!dr.IsDBNull(1))
				{
					rv.UserID = dr.GetInt32(1);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				}
				if(!dr.IsDBNull(2))
				{
					rv.TcodeAssignmentSetID = dr.GetInt32(2);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
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
		/// update a row in table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="UserID"></param>
		/// <param name="TcodeAssignmentSetID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetUserTcodeAssignmentSet(int ID, int UserID, int TcodeAssignmentSetID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_r_UserTcodeAssignmentSet\" set \"c_r_User\"=?,\"c_r_TcodeAssignmentSet\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_r_User",OdbcType.Int);
			cmd.Parameters["c_r_User"].Value = (object)UserID;
			cmd.Parameters.Add("c_r_TcodeAssignmentSet",OdbcType.Int);
			cmd.Parameters["c_r_TcodeAssignmentSet"].Value = (object)TcodeAssignmentSetID;
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
		/// select a set of rows from table t_RBSR_AUFW_r_UserTcodeAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListUserTcodeAssignmentSet[]</returns>
		public returnListUserTcodeAssignmentSet[] ListUserTcodeAssignmentSet(int? maxRowsToReturn)
		{
			returnListUserTcodeAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = new OdbcCommand();
			cmd.CommandText = "select" + (maxRowsToReturn.HasValue && maxRowsToReturn.Value!=0 ? " top " + maxRowsToReturn.Value.ToString() : "") + " " + "\"c_id\",\"c_r_User\",\"c_r_TcodeAssignmentSet\" from \"t_RBSR_AUFW_r_UserTcodeAssignmentSet\"";
			cmd.Connection = _dbConnection;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListUserTcodeAssignmentSet> rvl = new List<returnListUserTcodeAssignmentSet>();
			while(dr.Read())
			{
				returnListUserTcodeAssignmentSet cr = new returnListUserTcodeAssignmentSet();
				if(!dr.IsDBNull(0))
				{
					cr.ID = dr.GetInt32(0);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				}
				if(!dr.IsDBNull(1))
				{
					cr.UserID = dr.GetInt32(1);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'UserID'"); } 
				}
				if(!dr.IsDBNull(2))
				{
					cr.TcodeAssignmentSetID = dr.GetInt32(2);
				}
				else
				{
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'TcodeAssignmentSetID'"); } 
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
