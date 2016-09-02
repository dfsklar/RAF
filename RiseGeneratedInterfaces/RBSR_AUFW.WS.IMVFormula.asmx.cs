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
using RBSR_AUFW.DB.IMVFormula;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.WS.IMVFormula
{
	/// <summary>
	/// Web service for publishing RBSR_AUFW.DB.IMVFormula.IMVFormula
	/// </summary>
	[WebService(Namespace = "http://RBSR_AUFW.WS.IMVFormula/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class IMVFormula : System.Web.Services.WebService
	{
		protected string GetConnectionString(string systemid)
		{
			return System.Configuration.ConfigurationManager.AppSettings[systemid];
		}
		protected void AssignTemporaryDirectory(RBSR_AUFW.DB.IMVFormula.IMVFormula obj)
		{
			string wsAppData = HttpContext.Current.Server.MapPath(".") + "\\App_Data";
			if (!Directory.Exists(wsAppData))
				Directory.CreateDirectory(wsAppData);
			obj.TempDir = wsAppData;
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IMVFormula.IMVFormula.NewMVFormula to insert a row in table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="KEYapplication"></param>
		/// <returns>The integer ID of the new object.</returns>
		[WebMethod]
		public int NewMVFormula(string KEYapplication)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IMVFormula.IMVFormula obj = new RBSR_AUFW.DB.IMVFormula.IMVFormula(dbconn);
			return obj.NewMVFormula(KEYapplication);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IMVFormula.IMVFormula.DeleteMVFormula to delete a row from table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int DeleteMVFormula(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IMVFormula.IMVFormula obj = new RBSR_AUFW.DB.IMVFormula.IMVFormula(dbconn);
			return obj.DeleteMVFormula(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IMVFormula.IMVFormula.GetMVFormula to select a row from table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>returnGetMVFormula</returns>
		[WebMethod]
		public returnGetMVFormula GetMVFormula(int ID)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IMVFormula.IMVFormula obj = new RBSR_AUFW.DB.IMVFormula.IMVFormula(dbconn);
			return obj.GetMVFormula(ID);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IMVFormula.IMVFormula.SetMVFormula to update a row in table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="KEYapplication"></param>
		/// <param name="MATCHentVal"></param>
		/// <param name="MATCHauthObj"></param>
		/// <param name="MATCHfieldSecName"></param>
		/// <param name="Formula"></param>
		/// <returns>Number of affected rows.</returns>
		[WebMethod]
		public int SetMVFormula(int ID, string KEYapplication, string MATCHentVal, string MATCHauthObj, string MATCHfieldSecName, string Formula)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IMVFormula.IMVFormula obj = new RBSR_AUFW.DB.IMVFormula.IMVFormula(dbconn);
			return obj.SetMVFormula(ID, KEYapplication, MATCHentVal, MATCHauthObj, MATCHfieldSecName, Formula);
		}
		/// <summary>
		/// 
		/// Uses RBSR_AUFW.DB.IMVFormula.IMVFormula.ListMVFormula to select a set of rows from table t_RBSR_AUFW_u_MVFormula.
		/// </summary>
		/// <param name="maxRowsToReturn">Max number of rows to return. If null or 0 all rows are returned.</param>
		/// <param name="extendedCriteria">Statement appended to the end of the WHERE-clause</param>
		/// <param name="extentedParameters">Values bound to the query (?) marks in <code>extendedCriteria</code></param>
		/// <param name="extendedSortOrder">Statement appended to the end of the ORDER BY-clause</param>
		/// <returns>returnListMVFormula[]</returns>
		[WebMethod]
		public returnListMVFormula[] ListMVFormula(int? maxRowsToReturn, string extendedCriteria, string[] extendedParameters, string extendedSortOrder)
		{
			OdbcConnection dbconn = new OdbcConnection(GetConnectionString("RBSR_AUFW"));
			RBSR_AUFW.DB.IMVFormula.IMVFormula obj = new RBSR_AUFW.DB.IMVFormula.IMVFormula(dbconn);
			return obj.ListMVFormula(maxRowsToReturn, extendedCriteria, extendedParameters, extendedSortOrder);
		}
	}
}
