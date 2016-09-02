$JQ(document).ready(JQdeclareDialogs);

function JQdeclareDialogs()
{
    $JQ("#JQDLGassign").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:480, width:480,
		    minHeight:200, height:200,
		    buttons:{"OK":JQDLGassign_OK, 
			"Cancel":function(){$JQ(this).dialog("close");}
		}
	});
}

function MENUACT_editAssignment()
{
    //  $('reconcdiff_HIDDEN_ID').value = gritem.getMember("c_id").Value;
    $('editroleowner_CHK_DEL').checked = false;
    $('JQDLGbp_primown_namelast').value = "";
    $('JQDLGbp_primown_namefirst').value = "";
    $('TD_editroleowner_INPUT_EID').value = 
	rightclickItem.getMember("c_u_EID").Value;
    $JQ("#JQDLGassign").dialog("open");
}

function JQDLGassign_OK()
{
    GLOBALdialogToClose = $JQ("#JQDLGassign");
    var url = "GuidedEditor/ajaxupdater.ashx";

    var itemArray = Grid1.getSelectedItems();
    var idlist = "";
    for (var i = 0;i<itemArray.length;i++) {
	idlist +=  (","+itemArray[i].getMember('c_id').Value);
    }

    new Ajax.Request(url, {
	    method: 'post',
		parameters: {
		cmd: "JQDLGassignReconcDiff",
		    idcuredit: idlist,
		    deleteme: $('editroleowner_CHK_DEL').checked,
		    eid: $F($('TD_editroleowner_INPUT_EID')),
		    NAMEsur: $F($('JQDLGbp_primown_namelast')),
		    NAMEfirst: $F($('JQDLGbp_primown_namefirst'))
		    }
		,
		onSuccess: AJAXRESPONSEJQDLG_success,
		onException: AJAXRESPONSEJQDLG_failure,
		onFailure: AJAXRESPONSEJQDLG_failure
		}
	);
}

function AJAXRESPONSEJQDLG_success(transport)
{
    AJAXRESPONSEJQDLG_close();
    Grid1.callback();
}
function AJAXRESPONSEJQDLG_close()
{
    if (GLOBALdialogToClose) {
	GLOBALdialogToClose.dialog("close");
	GLOBALdialogToClose = "";
    }
}




function hovershow(launcheritem, x)
{
    var cumoffset = Element.cumulativeOffset(launcheritem);
    $('hoverpopup').style.top = cumoffset[1]+18;
    $('hoverpopup').style.left = cumoffset[0]-$('hoverpopup').getWidth();
    $('hoverpopup').style.visibility = "Visible";

    var row = Grid1.getItemFromClientId(x);
    var text = (row.getMember("c_u_Comment").Value);
    $('_hoverpopup').innerHTML = text;
}



function hoverhide()
{
    $('hoverpopup').style.visibility = "Hidden";
}


function MENUACT_editComment()
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
			      arg1: "ReconcDiffItem",
			      arg2: theitem.getMember('c_id').Value,
			      arg3: "c_u_Comment",
			      arg4: newval
			      },
		      onSuccess: function(origrequest)
			  {
			      Grid1.callback();
			      //window.location.replace("PAGE_Reconc_Analysis.aspx");
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







function Grid_onContextMenu(sender, eventArgs)
{
    var itemArray = Grid1.getSelectedItems();
    var length = itemArray.length;

    // GLOBAL
    rightclickItem = eventArgs.get_item();

    var BOOLclickOnAlreadySelectedItem = false;

    for (var i = 0;i<length;i++) {
	if (rightclickItem.Id == itemArray[i].Id) {
	    BOOLclickOnAlreadySelectedItem = true;
	}
    }

    if (BOOLclickOnAlreadySelectedItem) {
	if (length>1) {
	    // Any special logic needed if mult-items selected?
	}
	ContextMenu.showContextMenuAtEvent(eventArgs.get_event());
	ContextMenu.set_contextData(eventArgs.get_item());
    }else{
	// These statements are for forcing the selection to be JUST the item right-clicked on:
	Grid1.select(eventArgs.get_item());
	ContextMenu.showContextMenuAtEvent(eventArgs.get_event());
	ContextMenu.set_contextData(eventArgs.get_item());
    }

}




function Grid_onItemSelect()
{
}




function MENUACT_editStatus(newval)
{
    var itemArray = Grid1.getSelectedItems();
    var idlist = "";
    for (var i = 0;i<itemArray.length;i++) {
	idlist +=  (","+itemArray[i].getMember('c_id').Value);
    }

    new Ajax.Request
		 (
		  "GuidedEditor/ajaxupdater.ashx",
		  {
		      parameters:
		      {
			  cmd: "EDIT_MultiRowsSingleColumn",
			      arg1: "ReconcDiffItem",
			      arg2: idlist.substr(1),
			      arg3: "c_u_Status",
			      arg4: newval
			      },
		      onSuccess: function(origrequest)
			  {
			      Grid1.callback();
			  },
			  onException: AJAXRESPONSEJQDLG_failure,
			  onFailure: AJAXRESPONSEJQDLG_failure
			  }
		  );
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
