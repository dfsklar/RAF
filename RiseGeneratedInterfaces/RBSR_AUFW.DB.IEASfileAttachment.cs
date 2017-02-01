using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;
using _6MAR_WebApplication;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.307 (#348)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IEASfileAttachment
{
	/// <summary>
	/// Return value from method GetEASfileAttachment
	/// </summary>
	public struct returnGetEASfileAttachment
	{
		public int ID;
		public string Filename;
		public string Comment;
		public DateTime UploadDate;
		public int EntAssignmentSetID;
	}
	/// <summary>
	/// Return value from method ListEASfileAttachment
	/// </summary>
	public struct returnListEASfileAttachment
	{
		public int ID;
		public string Filename;
		public string Comment;
		public DateTime UploadDate;
		public int EntAssignmentSetID;
	}
	/// <summary>
	/// Return value from method ListEASfileAttachmentByEntAssignmentSet
	/// </summary>
	public struct returnListEASfileAttachmentByEntAssignmentSet
	{
		public int ID;
		public string Filename;
		public string Comment;
		public DateTime UploadDate;
		public int EntAssignmentSetID;
	}
	/// <summary>
	/// Return value from method DownloadEASfileAttachmentContent
	/// </summary>
	public struct returnDownloadEASfileAttachmentContent
	{
		public string transactionId;
		public bool endOfTransaction;
		public byte[] Content;
	}
	/// <summary>
	/// Return value from method UploadEASfileAttachmentContent
	/// </summary>
	public struct returnUploadEASfileAttachmentContent
	{
		public string transactionId;
		public int totalBytesUploaded;
	}
	/// <summary>
	/// Class implementing operations on:
	///     Table t_RBSR_AUFW_u_EASfileAttachment
	/// </summary>
	public class IEASfileAttachment : _6MAR_WebApplication.RISEBASE
	{
		public IEASfileAttachment() : this((OdbcConnection)null) { }
		public IEASfileAttachment(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IEASfileAttachment(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// 
		/// insert a row in table t_RBSR_AUFW_u_EASfileAttachment.
		/// </summary>
		/// <param name="Filename"></param>
		/// <param name="Comment"></param>
		/// <param name="UploadDate"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>The integer ID of the new object.</returns>
		public int NewEASfileAttachment(string Filename, string Comment, DateTime UploadDate, int EntAssignmentSetID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "INSERT INTO \"t_RBSR_AUFW_u_EASfileAttachment\"(\"c_u_Filename\",\"c_u_Comment\",\"c_u_UploadDate\",\"c_r_EntAssignmentSet\") VALUES(?,?,?,?)";
			if (_dbConnection.Driver.ToLower().StartsWith("sql"))
				cmd.CommandText += " SELECT convert(int,SCOPE_IDENTITY())";
			if (Filename == null) throw new Exception("Filename must not be null!");
			cmd.Parameters.Add("c_u_Filename", OdbcType.NVarChar, 256);
			cmd.Parameters["c_u_Filename"].Value = (Filename != null ? (object)Filename : DBNull.Value);
			if (Comment == null) throw new Exception("Comment must not be null!");
			cmd.Parameters.Add("c_u_Comment", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_Comment"].Value = (Comment != null ? (object)Comment : DBNull.Value);
			cmd.Parameters.Add("c_u_UploadDate", OdbcType.DateTime);
			cmd.Parameters["c_u_UploadDate"].Value = HELPERS.SetSafeDBDate(UploadDate);
			cmd.Parameters.Add("c_r_EntAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_EntAssignmentSet"].Value = (object)EntAssignmentSetID;
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
		/// delete a row from table t_RBSR_AUFW_u_EASfileAttachment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		public int DeleteEASfileAttachment(int ID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "delete from \"t_RBSR_AUFW_u_EASfileAttachment\" where \"c_id\" = ?";
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
		/// select a row from table t_RBSR_AUFW_u_EASfileAttachment.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEASfileAttachment</returns>
		public returnGetEASfileAttachment GetEASfileAttachment(int ID)
		{
			returnGetEASfileAttachment rv = new returnGetEASfileAttachment();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "select \"c_id\",\"c_u_Filename\",\"c_u_Comment\",\"c_u_UploadDate\",\"c_r_EntAssignmentSet\" from \"t_RBSR_AUFW_u_EASfileAttachment\" where \"c_id\"= ?";
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
					throw new Exception("Value 'null' is not allowed for 'Filename'");
				else
					rv.Filename = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'Comment'");
				else
					rv.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'UploadDate'");
				else
					rv.UploadDate = dr.GetDateTime(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'");
				else
					rv.EntAssignmentSetID = dr.GetInt32(4);
			}
			dr.Close();
			dr.Dispose();
			cmd.Dispose();
			DBClose();
			return rv;
		}
		/// <summary>
		/// 
		/// update a row in table t_RBSR_AUFW_u_EASfileAttachment.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Filename"></param>
		/// <param name="Comment"></param>
		/// <param name="UploadDate"></param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>Number of affected rows.</returns>
		public int SetEASfileAttachment(int ID, string Filename, string Comment, DateTime UploadDate, int EntAssignmentSetID)
		{
			int rv = 0;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			cmd.CommandText = "update \"t_RBSR_AUFW_u_EASfileAttachment\" set \"c_u_Filename\"=?,\"c_u_Comment\"=?,\"c_u_UploadDate\"=?,\"c_r_EntAssignmentSet\"=? where \"c_id\" = ?";
			if (Filename == null) throw new Exception("Filename must not be null!");
			cmd.Parameters.Add("c_u_Filename", OdbcType.NVarChar, 256);
			cmd.Parameters["c_u_Filename"].Value = (Filename != null ? (object)Filename : DBNull.Value);
			if (Comment == null) throw new Exception("Comment must not be null!");
			cmd.Parameters.Add("c_u_Comment", OdbcType.NVarChar, 1024);
			cmd.Parameters["c_u_Comment"].Value = (Comment != null ? (object)Comment : DBNull.Value);
			cmd.Parameters.Add("c_u_UploadDate", OdbcType.DateTime);
            cmd.Parameters["c_u_UploadDate"].Value = HELPERS.SetSafeDBDate(UploadDate);
			cmd.Parameters.Add("c_r_EntAssignmentSet", OdbcType.Int);
			cmd.Parameters["c_r_EntAssignmentSet"].Value = (object)EntAssignmentSetID;
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
		/// select a set of rows from table t_RBSR_AUFW_u_EASfileAttachment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <returns>returnListEASfileAttachment[]</returns>
		public returnListEASfileAttachment[] ListEASfileAttachment(int? maxRowsToReturn)
		{
			returnListEASfileAttachment[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Filename\", \"c_u_Comment\", \"c_u_UploadDate\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EASfileAttachment\"";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Filename\", \"c_u_Comment\", \"c_u_UploadDate\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EASfileAttachment\"" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Filename\", \"c_u_Comment\", \"c_u_UploadDate\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EASfileAttachment\"";
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEASfileAttachment> rvl = new List<returnListEASfileAttachment>();
			while (dr.Read())
			{
				returnListEASfileAttachment cr = new returnListEASfileAttachment();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'Filename'");
				else
					cr.Filename = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'Comment'");
				else
					cr.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'UploadDate'");
				else
					cr.UploadDate = dr.GetDateTime(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'");
				else
					cr.EntAssignmentSetID = dr.GetInt32(4);
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
		/// select a set of rows from table t_RBSR_AUFW_u_EASfileAttachment.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="EntAssignmentSetID"></param>
		/// <returns>returnListEASfileAttachmentByEntAssignmentSet[]</returns>
		public returnListEASfileAttachmentByEntAssignmentSet[] ListEASfileAttachmentByEntAssignmentSet(int? maxRowsToReturn, int EntAssignmentSetID)
		{
			returnListEASfileAttachmentByEntAssignmentSet[] rv = null;
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			if (maxRowsToReturn.HasValue && maxRowsToReturn.Value > 0)
			{
				if (_dbConnection.Driver.ToLower().StartsWith("sql"))
					cmd.CommandText = "SELECT TOP " + maxRowsToReturn.Value + " \"c_id\", \"c_u_Filename\", \"c_u_Comment\", \"c_u_UploadDate\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EASfileAttachment\" WHERE \"c_r_EntAssignmentSet\"=?";
				else
					cmd.CommandText = "SELECT \"c_id\", \"c_u_Filename\", \"c_u_Comment\", \"c_u_UploadDate\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EASfileAttachment\" WHERE \"c_r_EntAssignmentSet\"=?" + " LIMIT " + maxRowsToReturn.Value;
			}
			else
				cmd.CommandText = "SELECT \"c_id\", \"c_u_Filename\", \"c_u_Comment\", \"c_u_UploadDate\", \"c_r_EntAssignmentSet\" FROM \"t_RBSR_AUFW_u_EASfileAttachment\" WHERE \"c_r_EntAssignmentSet\"=?";
			cmd.Parameters.Add("1_EntAssignmentSetID", OdbcType.Int);
			cmd.Parameters["1_EntAssignmentSetID"].Value = (object)EntAssignmentSetID;
			OdbcDataReader dr = cmd.ExecuteReader();
			List<returnListEASfileAttachmentByEntAssignmentSet> rvl = new List<returnListEASfileAttachmentByEntAssignmentSet>();
			while (dr.Read())
			{
				returnListEASfileAttachmentByEntAssignmentSet cr = new returnListEASfileAttachmentByEntAssignmentSet();
				if (dr.IsDBNull(0))
					throw new Exception("Value 'null' is not allowed for 'ID'");
				else
					cr.ID = dr.GetInt32(0);
				if (dr.IsDBNull(1))
					throw new Exception("Value 'null' is not allowed for 'Filename'");
				else
					cr.Filename = dr.GetString(1);
				if (dr.IsDBNull(2))
					throw new Exception("Value 'null' is not allowed for 'Comment'");
				else
					cr.Comment = dr.GetString(2);
				if (dr.IsDBNull(3))
					throw new Exception("Value 'null' is not allowed for 'UploadDate'");
				else
					cr.UploadDate = dr.GetDateTime(3);
				if (dr.IsDBNull(4))
					throw new Exception("Value 'null' is not allowed for 'EntAssignmentSetID'");
				else
					cr.EntAssignmentSetID = dr.GetInt32(4);
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
		/// select a large column of a row in table t_RBSR_AUFW_u_EASfileAttachment.
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="startReadingAt"></param>
		/// <param name="maxBytesToRead"></param>
		/// <param name="ID"></param>
		/// <returns>returnDownloadEASfileAttachmentContent</returns>
		public returnDownloadEASfileAttachmentContent 
            DownloadEASfileAttachmentContent
            (int ID)
		{
            byte[] readBuf = null;
            long bytesRead = 0;
            
            returnDownloadEASfileAttachmentContent rv = new returnDownloadEASfileAttachmentContent();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
				cmd.CommandText = "select \"c_u_Content\" from \"t_RBSR_AUFW_u_EASfileAttachment\" where \"c_id\" = ?";
				cmd.Parameters.Add("c_id", OdbcType.Int);
				cmd.Parameters["c_id"].Value = (object)ID;
				cmd.Connection = _dbConnection;
				OdbcDataReader dr = cmd.ExecuteReader();
				if(dr.Read())
				{
					if(!dr.IsDBNull(0))
					{
                        readBuf = (byte[])dr.GetValue(0);
                        bytesRead = readBuf.GetLongLength(0);
					}
				}
				dr.Close();
				dr.Dispose();
			

        // byte[] readBuf = new byte[(maxBytesToRead>0 ? maxBytesToRead : 1)];
                rv.Content = readBuf; //  new byte[bytesRead];
            /*
			if (bytesRead > 0)
			{
				Array.ConstrainedCopy(readBuf, 0, rv.Content, 0, bytesRead);
			}
             * */
			cmd.Dispose();
			DBClose();
			return rv;
		}





		/// <summary>
		/// 
		/// update a large column of a row in table t_RBSR_AUFW_u_EASfileAttachment.
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="endOfTransaction"></param>
		/// <param name="ID"></param>
		/// <param name="Content"></param>
		/// <returns>returnUploadEASfileAttachmentContent</returns>
		public returnUploadEASfileAttachmentContent UploadEASfileAttachmentContent(int ID, string pathContentFile)
		{
			returnUploadEASfileAttachmentContent rv = new returnUploadEASfileAttachmentContent();
			DBConnect();
			OdbcCommand cmd = _dbConnection.CreateCommand();
			FileStream writeFS = null;
            rv.transactionId = "unused";

			if(true==true)
			{
                FileStream readFS = File.Open(pathContentFile, FileMode.Open, FileAccess.Read);
                // TempDir + "/" + rv.transactionId, );
				byte[] buf = new byte[readFS.Length];
				if (readFS.Read(buf, 0, buf.Length) != buf.Length) throw new Exception("Upload transaction failed!");
				readFS.Close();
				readFS.Dispose();
				File.Delete(pathContentFile);
				cmd.CommandText = "update \"t_RBSR_AUFW_u_EASfileAttachment\" set \"c_u_Content\"=? where \"c_id\" = ?";
				cmd.Parameters.Add("c_u_Content",OdbcType.Image);
				cmd.Parameters["c_u_Content"].Value = (buf.Length > 0 ? (object)buf : DBNull.Value);
				cmd.Parameters.Add("c_id", OdbcType.Int);
				cmd.Parameters["c_id"].Value = (object)ID;
				cmd.Connection = _dbConnection;
				int rowsAffected = cmd.ExecuteNonQuery();
				if (rowsAffected != 1) throw new Exception("Upload resulted in " + rowsAffected.ToString() + " objects being updated!");
			}
			cmd.Dispose();
			DBClose();
			return rv;
		}
	}
}
