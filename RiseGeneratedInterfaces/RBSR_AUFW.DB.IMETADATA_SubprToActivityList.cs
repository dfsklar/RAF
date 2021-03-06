﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.205 (#246)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IMETADATA_SubprToActivityList
{
	/// <summary>
	/// Return value from method GetMETADATA_SubprToActivityList
	/// </summary>
	public struct returnGetMETADATA_SubprToActivityList
	{
		public int ID;
		public double? Sequence;
		public string NodeType;
		public bool? BOOLisKeyPoint;
		public string Text;
		public string ListIdsBusRoles;
		public string ListIdsApps;
		public int SubProcessID;
	}
	/// <summary>
	/// Return value from method ListMETADATA_SubprToActivityList
	/// </summary>
	public struct returnListMETADATA_SubprToActivityList
	{
		public int ID;
		public double? Sequence;
		public string NodeType;
		public bool? BOOLisKeyPoint;
		public string Text;
		public string ListIdsBusRoles;
		public string ListIdsApps;
		public int SubProcessID;
	}
	/// <summary>
	/// Return value from method ListMETADATA_SubprToActivityListBySubProcess
	/// </summary>
	public struct returnListMETADATA_SubprToActivityListBySubProcess
	{
		public int ID;
		public double? Sequence;
		public string NodeType;
		public bool? BOOLisKeyPoint;
		public string Text;
		public string ListIdsBusRoles;
		public string ListIdsApps;
		public int SubProcessID;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_METADATA_SubprToActivityList
	/// </summary>
	public class IMETADATA_SubprToActivityList : _6MAR_WebApplication.RISEBASE
	{
		public IMETADATA_SubprToActivityList() : this((OdbcConnection)null) { }
		public IMETADATA_SubprToActivityList(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IMETADATA_SubprToActivityList(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_METADATA_SubprToActivityList.
		/// </summary>
		/// <param name="NodeType"></param>
		/// <param name="SubProcessID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewMETADATA_SubprToActivityList(string NodeType, int SubProcessID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\"(\"c_u_NodeType\",\"c_r_SubProcess\") VALUES(?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (NodeType == null)  { cmd.Dispose(); DBClose(); throw new Exception("NodeType must not be null!"); } 
			cmd.Parameters.Add("c_u_NodeType", OdbcType.NVarChar, 5);
			cmd.Parameters["c_u_NodeType"].Value = (NodeType != null ? (object)NodeType : DBNull.Value);
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
		/// delete a row from table t_RBSR_AUFW_u_METADATA_SubprToActivityList.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteMETADATA_SubprToActivityList(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_METADATA_SubprToActivityList.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetMETADATA_SubprToActivityList</returns>
		public returnGetMETADATA_SubprToActivityList GetMETADATA_SubprToActivityList(int ID)
		{
			returnGetMETADATA_SubprToActivityList rv = new returnGetMETADATA_SubprToActivityList();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_Sequence\",\"c_u_NodeType\",\"c_u_BOOLisKeyPoint\",\"c_u_Text\",\"c_u_ListIdsBusRoles\",\"c_u_ListIdsApps\",\"c_r_SubProcess\" from \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\" where \"c_id\"= ?";
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
					rv.Sequence = null;
				else
					rv.Sequence = typeof(float).Equals(dr.GetFieldType(1))? (double)dr.GetFloat(1): dr.GetDouble(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'NodeType'"); } 
				else
					rv.NodeType = dr.GetString(2);
				if (dr.IsDBNull(3))
					rv.BOOLisKeyPoint = null;
				else
					rv.BOOLisKeyPoint = typeof(short).IsAssignableFrom(dr.GetFieldType(3))? (dr.GetInt16(3) != 0): dr.GetBoolean(3);
				if (dr.IsDBNull(4))
					rv.Text = null;
				else
					rv.Text = dr.GetString(4);
				if (dr.IsDBNull(5))
					rv.ListIdsBusRoles = null;
				else
					rv.ListIdsBusRoles = dr.GetString(5);
				if (dr.IsDBNull(6))
					rv.ListIdsApps = null;
				else
					rv.ListIdsApps = dr.GetString(6);
				if (dr.IsDBNull(7))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					rv.SubProcessID = dr.GetInt32(7);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_METADATA_SubprToActivityList.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Sequence"></param>
		/// <param name="NodeType"></param>
		/// <param name="BOOLisKeyPoint"></param>
		/// <param name="Text"></param>
		/// <param name="ListIdsBusRoles"></param>
		/// <param name="ListIdsApps"></param>
		/// <param name="SubProcessID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetMETADATA_SubprToActivityList(int ID, double? Sequence, string NodeType, bool? BOOLisKeyPoint, string Text, string ListIdsBusRoles, string ListIdsApps, int SubProcessID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\" set \"c_u_Sequence\"=?,\"c_u_NodeType\"=?,\"c_u_BOOLisKeyPoint\"=?,\"c_u_Text\"=?,\"c_u_ListIdsBusRoles\"=?,\"c_u_ListIdsApps\"=?,\"c_r_SubProcess\"=? where \"c_id\" = ?";
			cmd.Parameters.Add("c_u_Sequence", OdbcType.Real);
			cmd.Parameters["c_u_Sequence"].Value = (Sequence != null ? (object)Sequence : DBNull.Value);
			if (NodeType == null)  { cmd.Dispose(); DBClose(); throw new Exception("NodeType must not be null!"); } 
			cmd.Parameters.Add("c_u_NodeType", OdbcType.NVarChar, 5);
			cmd.Parameters["c_u_NodeType"].Value = (NodeType != null ? (object)NodeType : DBNull.Value);
			cmd.Parameters.Add("c_u_BOOLisKeyPoint", OdbcType.Bit);
			cmd.Parameters["c_u_BOOLisKeyPoint"].Value = (BOOLisKeyPoint != null ? (object)BOOLisKeyPoint : DBNull.Value);
			cmd.Parameters.Add("c_u_Text", OdbcType.NVarChar, 500);
			cmd.Parameters["c_u_Text"].Value = (Text != null ? (object)Text : DBNull.Value);
			cmd.Parameters.Add("c_u_ListIdsBusRoles", OdbcType.NVarChar, 200);
			cmd.Parameters["c_u_ListIdsBusRoles"].Value = (ListIdsBusRoles != null ? (object)ListIdsBusRoles : DBNull.Value);
			cmd.Parameters.Add("c_u_ListIdsApps", OdbcType.NVarChar, 500);
			cmd.Parameters["c_u_ListIdsApps"].Value = (ListIdsApps != null ? (object)ListIdsApps : DBNull.Value);
			cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
			cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
			cmd.Parameters.Add("c_id", OdbcType.Int);
			cmd.Parameters["c_id"].Value = (object)ID;
			cmd.Connection = _dbConnection;
			rv = cmd.ExecuteNonQuery();
			if (rv != 1)  { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); } 
			cmd.Dispose();
			DBClose();
			return rv;
		}



        public int SetSequence
            (int ID, double Sequence)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "update \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\" set \"c_u_Sequence\"=? where \"c_id\" = ?";
            cmd.Parameters.Add("c_u_Sequence", OdbcType.Real);
            cmd.Parameters["c_u_Sequence"].Value = (Sequence != null ? (object)Sequence : DBNull.Value);
            cmd.Parameters.Add("c_id", OdbcType.Int);
            cmd.Parameters["c_id"].Value = (object)ID;
            cmd.Connection = _dbConnection;
            rv = cmd.ExecuteNonQuery();
            if (rv != 1)  { cmd.Dispose(); DBClose(); throw new Exception("Update resulted in " + rv.ToString() + " objects being updated!"); } 
            cmd.Dispose();
            DBClose();
            return rv;
        }






        public int SetMETADATA_SubprToActivityList(int ID, string NodeType, bool? BOOLisKeyPoint, string Text, string ListIdsBusRoles, string ListIdsApps, int SubProcessID)
        {
            int rv = 0;
            DBConnect();
            OdbcCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "update \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\" set \"c_u_NodeType\"=?,\"c_u_BOOLisKeyPoint\"=?,\"c_u_Text\"=?,\"c_u_ListIdsBusRoles\"=?,\"c_u_ListIdsApps\"=?,\"c_r_SubProcess\"=? where \"c_id\" = ?";
            if (NodeType == null)  { cmd.Dispose(); DBClose(); throw new Exception("NodeType must not be null!"); } 
            cmd.Parameters.Add("c_u_NodeType", OdbcType.NVarChar, 5);
            cmd.Parameters["c_u_NodeType"].Value = (NodeType != null ? (object)NodeType : DBNull.Value);
            cmd.Parameters.Add("c_u_BOOLisKeyPoint", OdbcType.Bit);
            cmd.Parameters["c_u_BOOLisKeyPoint"].Value = (BOOLisKeyPoint != null ? (object)BOOLisKeyPoint : DBNull.Value);
            cmd.Parameters.Add("c_u_Text", OdbcType.NVarChar, 500);
            cmd.Parameters["c_u_Text"].Value = (Text != null ? (object)Text : DBNull.Value);
            cmd.Parameters.Add("c_u_ListIdsBusRoles", OdbcType.NVarChar, 200);
            cmd.Parameters["c_u_ListIdsBusRoles"].Value = (ListIdsBusRoles != null ? (object)ListIdsBusRoles : DBNull.Value);
            cmd.Parameters.Add("c_u_ListIdsApps", OdbcType.NVarChar, 500);
            cmd.Parameters["c_u_ListIdsApps"].Value = (ListIdsApps != null ? (object)ListIdsApps : DBNull.Value);
            cmd.Parameters.Add("c_r_SubProcess", OdbcType.Int);
            cmd.Parameters["c_r_SubProcess"].Value = (object)SubProcessID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_METADATA_SubprToActivityList.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListMETADATA_SubprToActivityList[]</returns>
		public returnListMETADATA_SubprToActivityList[] ListMETADATA_SubprToActivityList(int? maxRowsToReturn)
		{
			returnListMETADATA_SubprToActivityList[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Sequence\", \"c_u_NodeType\", \"c_u_BOOLisKeyPoint\", \"c_u_Text\", \"c_u_ListIdsBusRoles\", \"c_u_ListIdsApps\", \"c_r_SubProcess\" FROM \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Sequence\", \"c_u_NodeType\", \"c_u_BOOLisKeyPoint\", \"c_u_Text\", \"c_u_ListIdsBusRoles\", \"c_u_ListIdsApps\", \"c_r_SubProcess\" FROM \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Sequence\", \"c_u_NodeType\", \"c_u_BOOLisKeyPoint\", \"c_u_Text\", \"c_u_ListIdsBusRoles\", \"c_u_ListIdsApps\", \"c_r_SubProcess\" FROM \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListMETADATA_SubprToActivityList> rvl = new List<returnListMETADATA_SubprToActivityList>();
			while (dr.Read())
			{
				returnListMETADATA_SubprToActivityList cr = new returnListMETADATA_SubprToActivityList();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					cr.Sequence = null;
				else
					cr.Sequence = typeof(float).Equals(dr.GetFieldType(1))? (double)dr.GetFloat(1): dr.GetDouble(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'NodeType'"); } 
				else
					cr.NodeType = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.BOOLisKeyPoint = null;
				else
					cr.BOOLisKeyPoint = typeof(short).IsAssignableFrom(dr.GetFieldType(3))? (dr.GetInt16(3) != 0): dr.GetBoolean(3);
				if (dr.IsDBNull(4))
					cr.Text = null;
				else
					cr.Text = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.ListIdsBusRoles = null;
				else
					cr.ListIdsBusRoles = dr.GetString(5);
				if (dr.IsDBNull(6))
					cr.ListIdsApps = null;
				else
					cr.ListIdsApps = dr.GetString(6);
				if (dr.IsDBNull(7))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(7);
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
		/// select a set of rows from table t_RBSR_AUFW_u_METADATA_SubprToActivityList.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="SubProcessID"></param>
		/// <returns>returnListMETADATA_SubprToActivityListBySubProcess[]</returns>
		public returnListMETADATA_SubprToActivityListBySubProcess[] ListMETADATA_SubprToActivityListBySubProcess(int? maxRowsToReturn, int SubProcessID)
		{
			returnListMETADATA_SubprToActivityListBySubProcess[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Sequence\", \"c_u_NodeType\", \"c_u_BOOLisKeyPoint\", \"c_u_Text\", \"c_u_ListIdsBusRoles\", \"c_u_ListIdsApps\", \"c_r_SubProcess\" FROM \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\" WHERE \"c_r_SubProcess\"=? ORDER BY c_u_Sequence";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Sequence\", \"c_u_NodeType\", \"c_u_BOOLisKeyPoint\", \"c_u_Text\", \"c_u_ListIdsBusRoles\", \"c_u_ListIdsApps\", \"c_r_SubProcess\" FROM \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\" WHERE \"c_r_SubProcess\"=? ORDER BY c_u_Sequence " + " LIMIT " + maxRowsToReturn.Value;
			}
			else
                cmd.CommandText = "SELECT \"c_id\", \"c_u_Sequence\", \"c_u_NodeType\", \"c_u_BOOLisKeyPoint\", \"c_u_Text\", \"c_u_ListIdsBusRoles\", \"c_u_ListIdsApps\", \"c_r_SubProcess\" FROM \"t_RBSR_AUFW_u_METADATA_SubprToActivityList\" WHERE \"c_r_SubProcess\"=?  ORDER BY c_u_Sequence";
			cmd.Parameters.Add("1_SubProcessID", OdbcType.Int);
			cmd.Parameters["1_SubProcessID"].Value = (object)SubProcessID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListMETADATA_SubprToActivityListBySubProcess> rvl = new List<returnListMETADATA_SubprToActivityListBySubProcess>();
			while (dr.Read())
			{
				returnListMETADATA_SubprToActivityListBySubProcess cr = new returnListMETADATA_SubprToActivityListBySubProcess();
				if (dr.IsDBNull(0))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'ID'"); } 
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					cr.Sequence = null;
				else
					cr.Sequence = typeof(float).Equals(dr.GetFieldType(1))? (double)dr.GetFloat(1): dr.GetDouble(1);
				if (dr.IsDBNull(2))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'NodeType'"); } 
				else
					cr.NodeType = dr.GetString(2);
				if (dr.IsDBNull(3))
					cr.BOOLisKeyPoint = null;
				else
					cr.BOOLisKeyPoint = typeof(short).IsAssignableFrom(dr.GetFieldType(3))? (dr.GetInt16(3) != 0): dr.GetBoolean(3);
				if (dr.IsDBNull(4))
					cr.Text = null;
				else
					cr.Text = dr.GetString(4);
				if (dr.IsDBNull(5))
					cr.ListIdsBusRoles = null;
				else
					cr.ListIdsBusRoles = dr.GetString(5);
				if (dr.IsDBNull(6))
					cr.ListIdsApps = null;
				else
					cr.ListIdsApps = dr.GetString(6);
				if (dr.IsDBNull(7))
					 { cmd.Dispose(); DBClose(); throw new Exception("Value 'null' is not allowed for 'SubProcessID'"); } 
				else
					cr.SubProcessID = dr.GetInt32(7);
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
