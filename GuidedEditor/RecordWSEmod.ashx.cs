using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Web.Script.Serialization;
using RBSR_AUFW.DB.IEventLog;



namespace _6MAR_WebApplication.GuidedEditor
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class Handler1 : IHttpHandler, IReadOnlySessionState
  {

	 public void ProcessRequest(HttpContext context)
	 {
		AFWACsession session = context.Session["AFWACSESSION"] as AFWACsession;

		context.Response.ContentType = "text/plain";

		/*
		 * 								 wserowid: EGRIDtheentitlementID,
		 wsid: ...
		 newvector: jsonPOST,
		 newbroles: newSetLinkedBroles
		*/
		System.Web.Script.Serialization.JavaScriptSerializer UTIL =
		  new System.Web.Script.Serialization.JavaScriptSerializer();


        throw new Exception("NYI"
           );




         /*
		string JSONidsOfSelRows = (context.Request.Params["wserowids"]);
		int IDws = int.Parse(context.Request.Params["wsid"]);
		int IDsubpr = int.Parse(context.Request.Params["subprid"]);
		string JSONnewval = (context.Request.Params["newvector"]);
		string JSONoldval = (context.Request.Params["oldvector"]);
		string newbroles = (context.Request.Params["newbroles"]);
		string oldbroles = (context.Request.Params["oldbroles"]);

		Array ARRmultiselOldEditStat =
		  UTIL.DeserializeObject
		  (context.Request.Params["multiselOldEditStat"]) as Array;
		Array ARRmultiselOldBroleList =
		  UTIL.DeserializeObject
		  (context.Request.Params["multiselOldBroleList"]) as Array;


		// will be either ADD or DEL
		string rolelinksIncrementalUpdateType = (context.Request.Params["mode"]);

		// edittype will be either EDIT or CLONE or NEWFRESH
		string edittype = (context.Request.Params["edittype"]);



		Object OBJarrofids = UTIL.DeserializeObject(JSONidsOfSelRows);
		Array arrofids = OBJarrofids as Array;

		// This will be used for any situation where there is only one row
		// being worked on (the most common situation).
		int IDwserow = -1;
		if (edittype != "NEWFRESH")
		  {
			 IDwserow = int.Parse(arrofids.GetValue(0) as string);
		  }
		// arrofids.GetLength()   .GetValue(idx)   etc.




		/////////////////////////////////////
		//
		// MAKE CHANGES TO THE FIELDS OF THE VECTOR
		//


		if (JSONnewval != "--@--NOCHANGE--@--")
		  {
			 //
			 // There was a change in the entitlements
			 //

			 // In this case, there is only one row that is being worked on.
			 // ASSERT(arrofids.GetLength() == 1



			 Object deserresult = UTIL.DeserializeObject(JSONnewval);
			 System.Collections.Generic.Dictionary<string, object> THERESULT =
				deserresult as System.Collections.Generic.Dictionary<string, object>;






			 // Check to ensure nothing else in workspace has very same vector

			 bool changeWasOnlyCosmetic = false;
            
			 int existingID =
				HELPERS.FindEntitlementByChecksum(IDws, THERESULT);
			 // The retval is -1 if nothing found with that exact entitlement set
			 if (existingID >= 0)
				{
				  // If we get here, an existing row was found with exact same checksum.
				  // If it is the same row as this one being edited, then this
				  // means that only non-significant fields were changed, e.g. commentary.


				  if ((existingID == IDwserow) && (edittype == "EDIT"))
					 {
						changeWasOnlyCosmetic = true;
					 }
				  else
					 {
						// Whoops, something else has the same vector.
						// LOGIC ERROR HERE: Assumes checksum for two different vectors will never be identical.
						throw new Exception("Workspace already has a row representing that entitlement vector.");
					 }
				}


			 switch (edittype)
				{

				case "EDIT":
				  Object deserresultOLD = UTIL.DeserializeObject(JSONoldval);
				  System.Collections.Generic.Dictionary<string, object> THEOLDRESULT =
					 deserresultOLD as System.Collections.Generic.Dictionary<string, object>;

                   
				  HELPERS.WSEntitlementVectorUpdate
					 (IDws, IDwserow, THERESULT, THEOLDRESULT, changeWasOnlyCosmetic,
					  session.idUser, context.Request.ServerVariables["REMOTE_ADDR"], UTIL
					  );
				  break;

				case "CLONE":
				case "NEWFRESH":
				  IDwserow = HELPERS.WSEntitlementVectorCreate(IDws, THERESULT);
				  if (newbroles == "--@--NOCHANGE--@--")
					 {
						newbroles = oldbroles;
					 }
				  oldbroles = "";
				  context.Response.Write(IDwserow.ToString());
				  break;
				}
		  }




		////////////////////////////
		// MAKE ANY CHANGES TO THE LINKAGE TO BUS.ROLES
		//
		// The logic involved is based on which mode: SPECIFY, ADD, or DEL

		bool broleChangesOccurred = false;

		switch (rolelinksIncrementalUpdateType)
		  {
		  case "SPECIFY":
		  case "n/a":
			 broleChangesOccurred =
				UpdateBusRoleLinksFULL(oldbroles, newbroles, IDsubpr, IDws, IDwserow);
			 if (broleChangesOccurred)
				{
				  HELPERS.RunSql
					 ("UPDATE t_RBSR_AUFW_u_WorkspaceEntitlement " +
					  "SET c_u_EditStatus = c_u_EditStatus | 8 " +
					  "WHERE c_id = " + IDwserow);
				  IEventLog LOG = new IEventLog(HELPERS.NewOdbcConn());
				  int IDlog = LOG.NewEventLog(
														DateTime.Now, session.idUser,
														context.Request.ServerVariables["REMOTE_ADDR"],
														"OrigWSRoles");
				  LOG.SetEventLog(IDlog,
										DateTime.Now, session.idUser, 
										context.Request.ServerVariables["REMOTE_ADDR"], "OrigWSRoles",
										IDwserow,
										oldbroles);
				}
			 break;

		  default:
			 UpdateBusRoleLinksINCR(rolelinksIncrementalUpdateType, newbroles, IDsubpr, IDws, arrofids,
											ARRmultiselOldEditStat, ARRmultiselOldBroleList,
											session.idUser,
											context.Request.ServerVariables["REMOTE_ADDR"]);
			 break;
		  }
          */



	 }






	 private void UpdateBusRoleLinksINCR
		(string mode, string newbroles, int IDsubpr, int IDws, Array IDSwsentrow,
		 Array ARRmultiselOldEditStat,
		 Array ARRmultiselOldBroleList,
		 int idUser, string strIPaddr)
	 {
		for (int i=0; i < IDSwsentrow.Length; i++) {
		  string _IDwserow = IDSwsentrow.GetValue(i) as string;
		  int IDwserow = int.Parse(_IDwserow);
 
		  string[] _newones = newbroles.Split(' ');
		  foreach (string newone in _newones)
			 {
				if (newone == "")
				  continue;

				int IDbrole = HELPERS.FindBusRoleByAbbrev(newone, IDsubpr);

				bool chgWasPerformed = false;

				switch (mode)
				  {
				  case "ADD":
					 chgWasPerformed =
						HELPERS.RecordLinkFromBusRoleToEntitlementVector(
																					HELPERS.NewOdbcConn(), IDbrole, IDwserow, IDws);
					 break;

				  case "DEL":
					 chgWasPerformed
						=
						HELPERS.DestroyLinkFromBusRoleToWSEntitVector
						(
						 HELPERS.NewOdbcConn(),
						 IDbrole,
						 IDwserow,
						 IDws);
						 
					 break;

				  default:
					 throw new Exception("Invalid mode parameter sent to UpdateBusRoleLinksINCR");
				  }

				if (chgWasPerformed)
				  {
					 HELPERS.RunSql
						("UPDATE t_RBSR_AUFW_u_WorkspaceEntitlement " +
						 "SET c_u_EditStatus = c_u_EditStatus | 8 " +
						 "WHERE c_id = " + IDwserow);
					 IEventLog LOG = new IEventLog(HELPERS.NewOdbcConn());
					 int oldEditStatus = Int32.Parse(ARRmultiselOldEditStat.GetValue(i).ToString());
					 if ( 0 == (oldEditStatus & 8)) {
						string oldbroles = ARRmultiselOldBroleList.GetValue(i) as string;
						int IDlog = LOG.NewEventLog(
															 DateTime.Now, idUser,
															 strIPaddr,
															 "OrigWSRoles");
						LOG.SetEventLog(IDlog,
											 DateTime.Now, idUser,
											 strIPaddr, "OrigWSRoles",
											 IDwserow,
											 oldbroles);
					 }
				  }

			 }
		}
            
	 }









	 // Returns true if change did indeed occur
	 private
		bool UpdateBusRoleLinksFULL(string oldbroles,string newbroles,int IDsubpr,int IDws,int IDwserow)
	 {
		if (newbroles == "--@--NOCHANGE--@--")
		  {
			 return false;
		  }
		else
		  {
			 bool retval = false;

			 // Both old/newbroles are a space-separated list in a single string
			 string[] _oldones = oldbroles.Split(' ');
			 Hashtable oldones = new Hashtable();
			 foreach (string s in _oldones)
				{
				  oldones.Add(s, 1);
				}

			 string[] _newones = newbroles.Split(' ');
			 foreach (string newone in _newones)
				{
				  if (newone == "")
					 {
						continue;
					 }


				  if (oldones.ContainsKey(newone))
					 {
						// No change needed.
						oldones.Remove(newone);
					 }
				  else
					 {
						// This is a NEW relationship that needs to be recorded.
						HELPERS.RecordLinkFromBusRoleToEntitlementVector
						  (
							HELPERS.NewOdbcConn(),
							HELPERS.FindBusRoleByAbbrev(newone, IDsubpr),
							IDwserow,
							IDws);
						retval = true;
					 }
				}
			 // Anything left in the "oldones" array is a relationship that needs
			 // to be deleted.
			 foreach (object _obso in oldones.Keys)
				{
				  string obsoleteone = (string)_obso;
				  if (obsoleteone == "")
					 continue;

				  HELPERS.DestroyLinkFromBusRoleToWSEntitVector
					 (
					  HELPERS.NewOdbcConn(),
					  HELPERS.FindBusRoleByAbbrev(obsoleteone, IDsubpr),
					  IDwserow,
					  IDws);
				  retval = true;
				}

			 return retval;
		  }
	 }

 	
	 
        
        
        
        
        
        
	 public bool IsReusable
	 {
		get
		  {
			 return false;
		  }
	 }
  }
}
