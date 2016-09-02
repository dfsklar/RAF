function MENUACT_editProperties()
{
    var theitem = ContextMenu.get_contextData();
    $JQ.alerts.prompt
	("",
	 theitem.getMember("c_u_Commentary").Value,
	 "Commentary Editor",
	 function(newval) 
	 {
	     if(newval)
	     new Ajax.Request
		 (
		  "GuidedEditor/ajaxupdater.ashx",
		  {
		      parameters:
		      {
			  cmd: "EDIT_SingleRowSingleColumn",
			      arg1: "EntAssignmentSet",
			      arg2: theitem.getMember('c_id').Value,
			      arg3: "c_u_Commentary",
			      arg4: newval
			      },
		      onSuccess: function(origrequest)
			  {
			      window.location.replace("PAGE_BRoles_Workspace.aspx");
			  },
			  onException: AJAXRESPONSEJQDLG_failure,
			  onFailure: AJAXRESPONSEJQDLG_failure
			  }
		  );
	 }
	 );
}




function MENUACT_delete()
{
    if (confirm("Are you sure you want to delete this workspace?")) {
	new Ajax.Request
	    (
	     "utilities/CleaningCrew.ashx",
	     {parameters:
		 {
		     cmd: "WSdel",
			 arg1: ContextMenu.get_contextData().getMember('c_id').Value
			 },
		     onSuccess: function(origrequest)
		     {
			 window.location.replace("PAGE_BRoles_Workspace.aspx");
		     },
		     onException: AJAXRESPONSEJQDLG_failure,
		     onFailure: AJAXRESPONSEJQDLG_failure
		     }
	     );
    }
}






function Grid_onContextMenu(sender,eventArgs)
{
    var curitem = eventArgs.get_item();

    // These statements are for forcing the selection to be JUST the item right-clicked on:
    Grid1.select(curitem);

    var menuitemDEL =  (ContextMenu.findItemById('DEL'));
    menuitemDEL.set_enabled("WORKSPACE" == curitem.getMember('c_u_Status').Value);
    menuitemDEL.set_visible("WORKSPACE" == curitem.getMember('c_u_Status').Value);

    ContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    ContextMenu.set_contextData(eventArgs.get_item());

    // GLOBattachmentid = eventArgs.get_item().getMember("c_id").Value;
    //    alert(GLOBattachmentid);
}


function Grid_onItemSelect()
{
}










function LaunchWorkspace(idSubpr, idUser)
{
    $JQ.alerts.multilineprompt
	("Enter initial commentary:",
	 "",
	 "New Workspace",
	 function(newval) 
	 {
	     if(newval)
	     new Ajax.Request
		 (
		  "GuidedEditor/ajaxupdater.ashx",
		  {parameters:
		      {
			  cmd: "CreateWorkspaceOnEntAssSet",
			      arg1: idSubpr,
			      arg2: idUser,
			      arg3: newval
			      },
			  onSuccess: function(origrequest)
			  {
			      window.location.replace("PAGE_BRoles_Workspace.aspx");
			  },
			  onException: AJAXRESPONSEJQDLG_failure,
			  onFailure: AJAXRESPONSEJQDLG_failure
			  }
		  );
	 });
}



function AJAXRESPONSEJQDLG_failure(transport)
{
    alert("ERROR/Warning: " + transport.responseText);
    AJAXRESPONSEJQDLG_close();
}
