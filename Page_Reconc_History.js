function MENUACT_reconchist_exportforexec()
{
    var theitem = ContextMenu.get_contextData();
    var id = theitem.getMember("c_id").Value;
    $('DumpFrame').src = 
	"export/ReconcfromSnapshot.ashx?mode=exec&id="+id;
    $JQ.alerts.confirm
	("Please verify the spreadsheet and archive it to a safe location. Click OK to request the exported items be marked as 'Done'.  Click Cancel if there is any problem, to ensure you can retry this export.",
	 "Confirm Receipt",
	 function(newval)
	 {
	     if (newval) {
		 $('DumpFrame').src = 
		     "export/ReconcfromSnapshot.ashx?mode=markdone&id="+id;
	     }
	 }
	 );
}


function MENUACT_editProperties()
{
    var theitem = ContextMenu.get_contextData();
    $JQ.alerts.multilineprompt
	("",
	 theitem.getMember("c_u_Comment").Value,
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
			      arg1: "ReconcReport",
			      arg2: theitem.getMember('c_id').Value,
			      arg3: "c_u_Comment",
			      arg4: newval
			      },
		      onSuccess: function(origrequest)
			  {
			      window.location.replace("PAGE_Reconc_History.aspx");
			  },
			  onException: AJAXRESPONSEJQDLG_failure,
			  onFailure: AJAXRESPONSEJQDLG_failure
			  }
		  );
	 }
	 );
}



function XXXXXXXXXXX_MENUACT_delete()
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

    ContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    ContextMenu.set_contextData(eventArgs.get_item());
}


function Grid_onItemSelect()
{
}










function LaunchWorkspace(idSubpr, idUser)
{
    $JQ.alerts.prompt
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
