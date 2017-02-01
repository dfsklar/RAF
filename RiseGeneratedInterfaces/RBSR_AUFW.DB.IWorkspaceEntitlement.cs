using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.IO;

//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74
//
// New Model (RBSR_AUFW)
// Version 1.150 (#191)
//
// 
// 
//
//3c6e8d24-b5d1-4af5-8771-4728cd3b6c74

namespace RBSR_AUFW.DB.IWorkspaceEntitlement
{
	/// <summary>
	/// Class implementing operations on:
	/// </summary>
	public class IWorkspaceEntitlement : _6MAR_WebApplication.RISEBASE
	{
		public IWorkspaceEntitlement() : this((OdbcConnection)null) { }
		public IWorkspaceEntitlement(string connectionString) : this(new OdbcConnection(connectionString)) { }
		public IWorkspaceEntitlement(OdbcConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

	}
}
