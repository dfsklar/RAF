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
using RBSR_AUFW.DB.IEntAssignmentSet;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IEntAssignmentSet
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IEntAssignmentSet/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IEntAssignmentSet : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet.NewEntAssignmentSet to insert a row in table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewEntAssignmentSet(int SubProcessID, int UserID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet obj = new RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet(dbconn);
			return obj.NewEntAssignmentSet(SubProcessID, UserID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet.DeleteEntAssignmentSet to delete a row from table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteEntAssignmentSet(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet obj = new RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet(dbconn);
			return obj.DeleteEntAssignmentSet(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet.GetEntAssignmentSet to select a row from table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetEntAssignmentSet</returns>
		[WebMethod]
		public returnGetEntAssignmentSet GetEntAssignmentSet(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet obj = new RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet(dbconn);
			return obj.GetEntAssignmentSet(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet.SetEntAssignmentSet to update a row in table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Status"></param>
		/// <param name="DATETIMElock"></param>
		/// <param name="Commentary"></param>
		/// <param name="SubProcessID"></param>
		/// <param name="UserID"></param>
		/// <param name="DATETIMEbirth"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetEntAssignmentSet(int ID, string Status, DateTime? DATETIMElock, string Commentary, int SubProcessID, int UserID, DateTime? DATETIMEbirth)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet obj = new RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet(dbconn);
			return obj.SetEntAssignmentSet(ID, Status, DATETIMElock, Commentary, SubProcessID, UserID, DATETIMEbirth);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet.ListEntAssignmentSet to select a set of rows from table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListEntAssignmentSet[]</returns>
		[WebMethod]
		public returnListEntAssignmentSet[] ListEntAssignmentSet(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet obj = new RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet(dbconn);
			return obj.ListEntAssignmentSet(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet.ListEntAssignmentSetBySubProcess to select a set of rows from table t_RBSR_AUFW_u_EntAssignmentSet.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <param name="SubProcessID"></param>
		/// <returns>returnListEntAssignmentSetBySubProcess[]</returns>
		[WebMethod]
		public returnListEntAssignmentSetBySubProcess[] ListEntAssignmentSetBySubProcess(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder, int SubProcessID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet obj = new RBSR_AUFW.DB.IEntAssignmentSet.IEntAssignmentSet(dbconn);
			return obj.ListEntAssignmentSetBySubProcess(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder, SubProcessID);
		}
	}
}
