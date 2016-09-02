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
using RBSR_AUFW.DB.ITcodeDictionary;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.ITcodeDictionary
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.ITcodeDictionary/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class ITcodeDictionary : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary.NewTcodeDictionary to insert a row in table t_RBSR_AUFW_u_TcodeDictionary.
		/// </summary>
		/// <param name="TcodeID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewTcodeDictionary(string TcodeID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary obj = new RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary(dbconn);
			return obj.NewTcodeDictionary(TcodeID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary.DeleteTcodeDictionary to delete a row from table t_RBSR_AUFW_u_TcodeDictionary.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteTcodeDictionary(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary obj = new RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary(dbconn);
			return obj.DeleteTcodeDictionary(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary.GetTcodeDictionary to select a row from table t_RBSR_AUFW_u_TcodeDictionary.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetTcodeDictionary</returns>
		[WebMethod]
		public returnGetTcodeDictionary GetTcodeDictionary(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary obj = new RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary(dbconn);
			return obj.GetTcodeDictionary(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary.SetTcodeDictionary to update a row in table t_RBSR_AUFW_u_TcodeDictionary.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="TcodeID"></param>
		/// <param name="Description"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetTcodeDictionary(int ID, string TcodeID, string Description)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary obj = new RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary(dbconn);
			return obj.SetTcodeDictionary(ID, TcodeID, Description);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary.ListTcodeDictionary to select a set of rows from table t_RBSR_AUFW_u_TcodeDictionary.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListTcodeDictionary[]</returns>
		[WebMethod]
		public returnListTcodeDictionary[] ListTcodeDictionary(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary obj = new RBSR_AUFW.DB.ITcodeDictionary.ITcodeDictionary(dbconn);
			return obj.ListTcodeDictionary(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
	}
}
