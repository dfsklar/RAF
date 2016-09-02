using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.Odbc;
using System.IO;
using RBSR_AUFW.DB.IUser;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IUser
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IUser.IUser
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IUser/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IUser : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IUser.IUser obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser.IUser.NewUser to insert a row in table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="EID"></param>
		/// <param name="Name"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewUser(string EID, string Name)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser.IUser obj = new RBSR_AUFW.DB.IUser.IUser(dbconn);
			return obj.NewUser(EID, Name);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser.IUser.DeleteUser to delete a row from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteUser(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser.IUser obj = new RBSR_AUFW.DB.IUser.IUser(dbconn);
			return obj.DeleteUser(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser.IUser.GetUser to select a row from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetUser</returns>
		[WebMethod]
		public returnGetUser GetUser(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser.IUser obj = new RBSR_AUFW.DB.IUser.IUser(dbconn);
			return obj.GetUser(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser.IUser.SetUser to update a row in table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="EID"></param>
		/// <param name="Name"></param>
		/// <param name="PrivilegeLevel"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetUser(int ID, string EID, string Name, string PrivilegeLevel)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser.IUser obj = new RBSR_AUFW.DB.IUser.IUser(dbconn);
			return obj.SetUser(ID, EID, Name, PrivilegeLevel);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IUser.IUser.ListUser to select a set of rows from table t_RBSR_AUFW_u_User.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListUser[]</returns>
		[WebMethod]
		public returnListUser[] ListUser(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IUser.IUser obj = new RBSR_AUFW.DB.IUser.IUser(dbconn);
			return obj.ListUser(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
	}
}
