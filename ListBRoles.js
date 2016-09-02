$JQ(document).ready(JQdeclareDialogs);


function JQdeclareDialogs()
{
    CALLBACKadditionalRoles.add_callbackComplete(EVTHNDL_callbackAdditionalRoles_callbackcomplete);


	$JQ("#JQDLGbusroleProperties").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:680, width:680,
		    minHeight:300, height:300,
		    buttons:{"OK":JQDLGbusroleProperties_OK, "Cancel":function(){$JQ(this).dialog("close");} }
	    });
	$JQ("#JQDLGchangeMgmt").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:911, width:911,
		    minHeight:200, height:200,
		    buttons:{"Close":function(){$JQ(this).dialog("close");} }
	    });
	$JQ("#JQDLGsupplementalRoleList").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:780, width:930,
		    minHeight:640, height:640,
		    buttons:{
		    "Close":function(){$JQ(this).dialog("close");}
		    //,
		    //"Add New":function(){GRIDeditaddrole_LaunchJQdialog_ADDNEW();}
		}
	    });
	$JQ("#JQDLGroleowners").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:680, width:900,
		    minHeight:200, height:200,
		    buttons:{
		    "Close":function(){$JQ(this).dialog("close");},
			"Add New":function(){GRIDroleowners_LaunchJQdialog_ADDNEW();}
		}
	    });
	$JQ("#JQDLGeditChgMgmtRow").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:480, width:480,
		    minHeight:400, height:400,
		    buttons:{
		    "OK":JQDLGeditChgMgmtRow_OK, 
			"Cancel":function(){$JQ(this).dialog("close");}
		}
	    });
	$JQ("#JQDLGeditAdditionalRoleRow").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:480, width:480,
		    minHeight:400, height:400,
		    buttons:{"OK":JQDLGeditAdditionalRoleRow_OK, 
			"Cancel":GRIDeditaddrole_CloseJQdialog 
			}
	    });
	$JQ("#JQDLGeditRoleOwnerRow").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:480, width:480,
		    minHeight:400, height:400,
		    buttons:{"OK":JQDLGeditRoleOwner_OK, 
			"Cancel":function(){$JQ(this).dialog("close");}
		}
	    });
}




var GLOBALdialogToClose = "";



function JQDLGeditRoleOwner_OK()
{
    GLOBALdialogToClose = $JQ("#JQDLGeditRoleOwnerRow");
    var url = "GuidedEditor/ajaxupdater.ashx";
    new Ajax.Request(url, {
	    method: 'post',
		parameters: {
		cmd: "JQDLGeditRoleOwner",
		    idcuredit: $F($('editroleowner_HIDDEN_ID')),
		    JQDLGbp_id: $F($('JQDLGbp_id')),
		    deleteme: $('editroleowner_CHK_DEL').checked,
		    geo: $F($('TD_editroleowner_INPUT_GEO')),
		    eid: $F($('TD_editroleowner_INPUT_EID')),
		    rank: $F($('TD_editroleowner_SELECT_RANK')),
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





function JQDLGeditAdditionalRoleRow_OK()
{
    var idSelectedBRole = ComboBoxChooseBRole.getSelectedItem().get_value();

    var url = "GuidedEditor/ajaxupdater.ashx";

    var HASHparams1 = $JQ(":input");


    GLOBALdialogToClose = $JQ("#JQDLGeditAdditionalRoleRow");

    new Ajax.Request(url, {
	    method: 'post',
		postBody: jQuery.param(HASHparams1) + "&cmd=JQDLGeditAdditionalRoleRow"
		+ "&comment=" + escape($('editaddrole_TEXTAREA_COMMENTS').value)
		+ "&deleteme=" + escape($('editaddrole_CHK_DEL').checked)
		+ "&idSupplBrole=" + idSelectedBRole
		,
		onSuccess: AJAXRESPONSEJQDLG_success,
		onException: AJAXRESPONSEJQDLG_failure,
		onFailure: AJAXRESPONSEJQDLG_failure
		}
	);
}

function JQDLGeditChgMgmtRow_OK()
{
    var url = "GuidedEditor/ajaxupdater.ashx";

    var HASHparams1 = $JQ(":input");
    var HASHparams2 = $JQ(":textarea");

    new Ajax.Request(url, {
	    method: 'post',
		postBody: jQuery.param(HASHparams1) + "&cmd=JQDLGeditChgMgmt"
		+ "&comment=" + escape($('editchgmgmtrow_TEXTAREA_COMMENTS').value),
		onSuccess: AJAXRESPONSEJQDLG_success,
		onException: AJAXRESPONSEJQDLG_failure,
		onFailure: AJAXRESPONSEJQDLG_failure
		}
	);
}




function JQDLGbusroleProperties_OK()
{
    // This will serialize the dialog input controls:
    var HASHparams = $JQ(":input");


    //HASHparams["cmd"] = "JQDLGbusroleProperties";
    var url = "GuidedEditor/ajaxupdater.ashx";

    new Ajax.Request(url, {
	    method: 'post',
		postBody: jQuery.param(HASHparams) + "&cmd=JQDLGbusroleProperties",
		XXXparameters: HASHparams,
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
    GRIDchangeMgmt.callback();
    GRIDadditionalRoles.callback();
    GRIDroleowners.callback();
    $('spinnerImg').style.display = "none";
}

function AJAXRESPONSEJQDLG_failure(transport)
{
    $('spinnerImg').style.display = "none";
    alert("ERROR/Warning: " + transport.responseText);
    AJAXRESPONSEJQDLG_close();
}

function AJAXRESPONSEJQDLG_close()
{
    $('spinnerImg').style.display = "none";
    if (GLOBALdialogToClose) {
	GLOBALdialogToClose.dialog("close");
	GLOBALdialogToClose = "";
    }else{
	$JQ("#JQDLGbusroleProperties").dialog("close");
	$JQ("#JQDLGeditChgMgmtRow").dialog("close");
    }
}









function EVTHNDL_datechange(are)
{
}



// Launch the dialog for editing an additional-role row.
// Two launchers, one for adding new and one for editing.
function GRIDeditaddrole_LaunchJQdialog_ADDNEW()
{
    $('editaddrole_HIDDEN_ID').value  = "ADD";
    GRIDeditaddrole_LaunchJQdialog();
}

function GRIDroleowners_LaunchJQdialog_ADDNEW()
{
    $('editroleowner_HIDDEN_ID').value  = "ADD";
    $('ROW_editroleowner_CHK_DEL').style.visibility = "hidden";
    GRIDeditroleowner_LaunchJQdialog();
}




function GRIDeditaddrole_LaunchJQdialog()
{    
    $('HIDDEN_CLOSET').appendChild
	($('WRAPPER_combobox_rolechooser'));

    $('editaddrole_CHK_DEL').checked = false;
    $('ROWdeleteme').style.visibility = "hidden";


    $JQ("#JQDLGeditAdditionalRoleRow").dialog("open");

    $JQ("#editaddrole_INPUT_EXPIRDATE").datepicker({showOn:'both', dateFormat:'yy-M-dd'});
    $JQ("#editaddrole_INPUT_RECERTSTARTDATE").datepicker({showOn:'both', dateFormat:'yy-M-dd'});

    // This works around the problem with the datepicker hidden behind the dialog
    $JQ("#ui-datepicker-div").css("z-index",99999);

    // This movement MUST occur after the dialog.open call, to
    //  workaround the jquery damaging-webui bug.
    $('TD_editaddrole_INPUT_ROLE').appendChild
	($('WRAPPER_combobox_rolechooser'));
}


function GRIDeditaddrole_CloseJQdialog()
{
    $('HIDDEN_CLOSET').appendChild
	($('WRAPPER_combobox_rolechooser'));
    $JQ("#JQDLGeditAdditionalRoleRow").dialog("close");
}







function GRIDeditroleowner_LaunchJQdialog()
{    
    $('editroleowner_CHK_DEL').checked = false;
    $('ROW_editroleowner_CHK_DEL').style.visibility = "hidden";

    $('TD_editroleowner_INPUT_EID').value = "";
    $('TD_editroleowner_INPUT_GEO').value = "";

    $('JQDLGbp_primown_namelast').value = "";
    $('JQDLGbp_primown_namefirst').value = "";

    $JQ("#JQDLGeditRoleOwnerRow").dialog("open");
}






function GRIDchgmgmt_LaunchJQdialog(gritem)
{
    $('editchgmgmtrow_LABEL_WHAT').innerHTML = gritem.getMember("c_u_EventType").Value;
    $('editchgmgmtrow_INPUT_WHO').value = gritem.getMember("c_u_Who").Value;

    
    // This next line will bring in the rendered version of the date instead of
    // the Java Data object.
    var thedate = gritem.getMember("c_u_TimeStamp").Text;

    if (thedate == "") {
	var today = new Date();
	$('editchgmgmtrow_INPUT_WHEN').value = 
	    today.getFullYear() + "-" + (today.getMonth()+1) + "-" + today.getDate();
    }
    else{
	$('editchgmgmtrow_INPUT_WHEN').value = thedate;
    }

    $('editchgmgmtrow_HIDDEN_ID').value = gritem.getMember("c_id").Value;

    $('editchgmgmtrow_TEXTAREA_COMMENTS').value = gritem.getMember("c_u_Commentary").Value;

    $JQ("#JQDLGeditChgMgmtRow").dialog("open");

    $JQ("#editchgmgmtrow_INPUT_WHEN").datepicker({showOn:'both', dateFormat:'yy-M-dd'});
    $JQ("#ui-datepicker-div").css("z-index",99999);

}




function LaunchJQdialog_SupplementalRoles(gritem)
{
    GLOBALcontextOfDialog = gritem;

    $('JQDLGbp_id').value = gritem.getMember("c_id").Value;

    // THIS IS WORKING!

    // Step 1: send the server the integer id of the currently selected busrole.
    // The server will take that info and stuff it into a session variable that
    // the SQL data source in the aspx will be using.
    GLOBALdialogToLaunch = "SupplementalRoles";
    CALLBACKadditionalRoles.callback(gritem.getMember("c_id").Value);


    $JQ("#JQDLGsupplementalRoleList").dialog("option", "title", 
           "Additional Roles Allowed for: " + gritem.getMember("c_u_Name").Value);
}




function LaunchJQdialog_RoleOwners(gritem)
{
    GLOBALcontextOfDialog = gritem;

    $('JQDLGbp_id').value = gritem.getMember("c_id").Value;

    // THIS IS WORKING!

    // Step 1: send the server the integer id of the currently selected busrole.
    // The server will take that info and stuff it into a session variable that
    // the SQL data source in the aspx will be using.
    GLOBALdialogToLaunch = "RoleOwners";
    CALLBACKadditionalRoles.callback(gritem.getMember("c_id").Value);

    $JQ("#JQDLGroleowners").dialog("option", "title", 
           "Role Owners for: " + gritem.getMember("c_u_Name").Value);
}



function EVTHNDL_callbackAdditionalRoles_callbackcomplete()
{
    // Amazingly step 2 can be done right when step 1 is being done.
    // But I've noticed a delay; the dialog will first come up with
    // previous data and then autofix itself.  We might want to have the
    // rest of this function occur only when the callback finished the
    // roundtrip?

    // Step 2: tell the grid to get a fresh view of the data.  It should
    // now get the data associated with the new busrole selection.


    if (GLOBALdialogToLaunch == "RoleOwners") {

	GRIDroleowners.callback();

	$('HIDDEN_CLOSET').appendChild 
	    ($('WRAPPER_GRIDroleowners'));

	$JQ("#JQDLGroleowners").dialog("open");
	
	$('TARGETHOME_GRIDroleowners').appendChild 
	    ($('WRAPPER_GRIDroleowners'));

    }else{

	GRIDadditionalRoles.callback();

	$('HIDDEN_CLOSET').appendChild 
	    ($('WRAPPER_GRIDadditionalRoles'));

	$JQ("#JQDLGsupplementalRoleList").dialog("open");
	
	$('TARGETHOME_GRIDadditionalRoles').appendChild 
	    ($('WRAPPER_GRIDadditionalRoles'));
    }
}




// New feature on 30-Sept-2009:
// If passed a null gritem, becomes a launcher
// of a properties dialog for creating a NEW role!
//
function LaunchJQdialog(gritem)
{
     GLOBALcontextOfDialog = gritem;
     if (gritem) {
	 $('JQDLGbp_id').value = gritem.getMember("c_id").Value;
	 $('JQDLGbp_name').value = gritem.getMember("c_u_Name").Value;
	 $('JQDLGbp_descr').value = gritem.getMember("c_u_Description").Value;
	 $('JQDLGbp_designdetails').value = gritem.getMember("c_u_DesignDetails").Value;
	 $('JQDLGbp_roletype').value = gritem.getMember("c_u_RoleType").Value;
     }else{
	 $('JQDLGbp_id').value = "";
	 $('JQDLGbp_name').value = "";
	 $('JQDLGbp_descr').value = "";
	 $('JQDLGbp_designdetails').value = "";
	 $('JQDLGbp_roletype').value = 'F';
     }

    $JQ("#JQDLGbusroleProperties").dialog("open");

    //Causing problems with statement handles being reused?:
    //LookupEID("JQDLGbp_primown_eid", "JQDLGbp_primown_name",true);
    //LookupEID("JQDLGbp_secown_eid", "JQDLGbp_secown_name",true);
}





function Grid1_onCallbackError(sender, eventArgs)
{
    alert(eventArgs.get_errorMessage());
}



function GRIDchgmgmt_RenderEditArea(dataitem)
{
    var retstr = "";
    retstr +=
	"<a href=\"javascript:GRIDchgmgmt_LaunchJQdialog(GRIDchangeMgmt.getItemFromClientId('" + dataitem.ClientId + "'));\">&gt;</a>";
    return retstr;
}



function RenderEditArea(dataitem)
{
    var retstr;
    if (GLOBALpageIsEditable) {


	retstr =
	    "<A href='PAGEroleDesAppList.aspx?RoleID=" + DataItem.GetMember("c_id").Value + "'>Design</A>|";


	if (3 == 3897) {
	    // This next statement performs the OLD in-place editing, no the JQuery dialog
	    retstr +=
		"<a href=\"javascript:Grid1.edit(Grid1.getItemFromClientId('" + DataItem.ClientId + "'));\">Prop</a>";
	}
	else {
	    // This statement uses JQuery
	    retstr +=
		"<a href=\"javascript:LaunchJQdialog(Grid1.getItemFromClientId('" + DataItem.ClientId + "'));\">Prop</a>";
	}



	retstr +=
	    "|<A href=\"javascript:LaunchJQdialog_SupplementalRoles(Grid1.getItemFromClientId('" + DataItem.ClientId + "'));\">Suppl</A>";


	retstr +=
	    "|<A href=\"javascript:LaunchJQdialog_RoleOwners(Grid1.getItemFromClientId('" + DataItem.ClientId + "'));\">Owners</A>";


	retstr +=
	    "|<A href=\"javascript:CloneBusRole(" + DataItem.GetMember("c_id").Value + ");\">Clone</A>";



	if (dataitem.getMember("KOUNT").Value == 0) {
	    retstr += "|<A href='javascript:DelRow(\"" + DataItem.ClientId + "\");'>Del</A>";
	}
    }
    else{
	retstr =
	    "<A href='PAGEroleDesAppList.aspx?RoleID=" + DataItem.GetMember("c_id").Value + "'>View</A>";
    }
    return retstr;
}



function LaunchChangeMgmt(idofws)
{
    CALLBACKinitChangeMgmt.Callback(idofws);
}
function LaunchChangeMgmt_Continue()
{
    GRIDchangeMgmt.callback();
    $('HIDDEN_CLOSET').appendChild 
	($('WRAPPER_GRIDchangeMgmt'));
    $JQ("#JQDLGchangeMgmt").dialog("open");
    $('TARGETHOME_GRIDchangeMgmt').appendChild 
	($('WRAPPER_GRIDchangeMgmt'));
}




function PublishWorkspace(idofws)
{
    $JQ.alerts.prompt
	("Please enter the comment that will permanently describe this published workspace:",
	 $('commentOnThisWorkspace').value,
	 "Publish Action",
	 function(newval) 
	 {
	     if(newval)
		 new Ajax.Request
		     (
		      "GuidedEditor/ajaxupdater.ashx",
		      {parameters:
			  {
			      cmd: "PublishEntAssSet",
				  arg1: idofws,
				  arg2: newval
				  },
			      onSuccess: function(origrequest)
			      {
				  window.location.replace("PAGE_BRoles_Workspace.aspx");
			      },
			      onFailure: AJAXRESPONSEJQDLG_failure,
			      onException: AJAXRESPONSEJQDLG_failure
			      }
		      );
	 }
	 );
}

    

function CloneBusRole(idofrole)
{
    var newrolename = prompt("Please enter a unique name for this new role");
    if (newrolename) {
	new Ajax.Request
	    (
	     "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "CloneBusRole",
			 arg1: idofrole,
			 arg2: newrolename,
			 arg3: GLOBALidworkspace
			 },
		     onSuccess: function(origrequest)
		     {
			 Grid1.callback();
			 alert(origrequest.responseText);
		     },
		     onException: AJAXRESPONSEJQDLG_failure,
		     onFailure: AJAXRESPONSEJQDLG_failure
	     }
	     );
    }
}

function DelRow(dataitemClientID) 
{
    if (confirm('Are you sure?  Click OK to proceed with the deletion.')) {
	var itemtokill = Grid1.getItemFromClientId(dataitemClientID);
	Grid1.deleteItem(itemtokill);
    }
}






function EVT_GRIDroleowners_RowSelect(sender, args)
{
    var itemArray = sender.getSelectedItems();
    var length = itemArray.length;
    var G;
    if (length == 1) {
	G = itemArray[0];
    }else{
	alert("Multi-selection in this grid is not useful.  Please select only one row.");
	return;
    }

    $('editroleowner_HIDDEN_ID').value  = G.getMember("c_id").Value;

    $('TD_editroleowner_INPUT_EID').value = G.getMember("c_u_EID").Value;
    $('TD_editroleowner_INPUT_GEO').value = G.getMember("c_u_Geography").Value;
    $('TD_editroleowner_SELECT_RANK').value = G.getMember("RankCode").Value;
    //alert(G.getMember("RankCode").Value);

    LookupEID('TD_editroleowner_INPUT_EID','JQDLGbp_primown_name');

    $('JQDLGbp_primown_namelast').value = "";
    $('JQDLGbp_primown_namefirst').value = "";
    
    $('ROW_editroleowner_CHK_DEL').style.visibility = 'visible';
    $('editroleowner_CHK_DEL').checked = false;

    $JQ("#JQDLGeditRoleOwnerRow").dialog("open");
}




function Grid1_onContextMenu(sender, eventArgs)
{
    var itemArray = sender.getSelectedItems();
    var length = itemArray.length;

    // GLOBAL
    GLOBcontextGridRow = eventArgs.get_item();

    // FORCE SELECTION OF THE ITEM INVOLVED
    sender.select(GLOBcontextGridRow);


    // DISABLE MENU ITEMS BASED ON CONTEXT
    if (GLOBALpageIsEditable) {
	GridContextMenu.showContextMenuAtEvent(eventArgs.get_event());
	GridContextMenu.set_contextData(eventArgs.get_item());
    }else{
	GridContextMenu_ReadOnly.showContextMenuAtEvent(eventArgs.get_event());
	GridContextMenu_ReadOnly.set_contextData(eventArgs.get_item());
    }
}


function MENUACTdiveIntoDesign()
{
    var DataItem = GLOBcontextGridRow;
    window.location.replace("PAGEroleDesAppList.aspx?RoleID=" + 
			    DataItem.GetMember("c_id").Value);
}

function MENUACTproperties()
{
    var DataItem = GLOBcontextGridRow;
    LaunchJQdialog(Grid1.getItemFromClientId(DataItem.ClientId));
}

function MENUACTadditroles()
{
    var DataItem = GLOBcontextGridRow;
    LaunchJQdialog_SupplementalRoles(Grid1.getItemFromClientId(DataItem.ClientId));
}

function MENUACTowners()
{
    var DataItem = GLOBcontextGridRow;
    LaunchJQdialog_RoleOwners(Grid1.getItemFromClientId(DataItem.ClientId));
}

function MENUACTclone()
{
    var DataItem = GLOBcontextGridRow;
    CloneBusRole(DataItem.getMember("c_id").Value);
}

function MENUACTdelete()
{
    var DataItem = GLOBcontextGridRow;
    DelRow(DataItem.ClientId);
}










var GLOBALcheckboxInteractionInProgress = "";

function EVT_GridSupplRoles_RowSelect(sender, args)
{
    if (GLOBALcheckboxInteractionInProgress == "YES")
	{
	    GLOBALcheckboxInteractionInProgress = "";
	    return;
	}


    var itemArray = sender.getSelectedItems();
    var length = itemArray.length;
    var G;
    if (length == 1) {
	G = itemArray[0];
    }else{
	alert("Multi-selection in this grid is not useful.  Please select only one row.");
	return;
    }


    if ("" == G.getMember("c_id").Value)
	return;

    

    var idABR = G.getMember("c_u_idAdditionalBusRole").Value;
    var itemInCB = ComboBoxChooseBRole.findItemByProperty("Value", idABR);
    ComboBoxChooseBRole.selectItem(itemInCB);
    

    // Recertification (two controls)
    $('editaddrole_SELECT_RECERTINTERVAL').value =
	G.getMember("c_u_RecertificationInterval").Value;
    $('editaddrole_INPUT_RECERTSTARTDATE').value =
	G.getMember("DATErecertstart").Value;

    
    // Expiration
    $('editaddrole_INPUT_EXPIRDATE').value =
	G.getMember("DATEexpir").Value;
    


    // Enable the datepicker UI
    //
    $JQ("#editaddrole_INPUT_EXPIRDATE").datepicker({showOn:'both', dateFormat:'yy-M-dd'});
    $JQ("#editaddrole_INPUT_RECERTSTARTDATE").datepicker({showOn:'both', dateFormat:'yy-M-dd'});
    // This works around the problem with the datepicker hidden behind the dialog
    $JQ("#ui-datepicker-div").css("z-index",99999);


    $('editaddrole_HIDDEN_ID').value  = G.getMember("c_id").Value;


    $('editaddrole_CHK_DEL').checked = false;
    $('ROWdeleteme').style.visibility = "visible";


    // MAKE SURE ANY WEB.UI WIDGETS ARE NOT HOUSED IN THE DIALOG
    // DURING THE open PROCEDURE.
    //
    $('HIDDEN_CLOSET').appendChild
	($('WRAPPER_combobox_rolechooser'));
    $JQ("#JQDLGeditAdditionalRoleRow").dialog("open");
    // This movement MUST occur after the dialog.open call, to
    //  workaround the jquery damaging-webui bug.
    $('TD_editaddrole_INPUT_ROLE').appendChild
	($('WRAPPER_combobox_rolechooser'));
}






function RespondToCheckboxChangeNATIVE(rowid,eventargs)
{
    GLOBALcheckboxInteractionInProgress = "YES";

    var url = "GuidedEditor/ajaxupdater.ashx";

    var griditem = eventargs.get_item();
    var gritem = griditem;
    var oldValue = 
	griditem.getMember('BoolRegisteredNow').get_text();
    // We now have the OLD value before it was toggled.
    if (oldValue == 'false') {
	// The user is ADDING this to the set of registered additional roles
	$('spinnerImg').style.display = "";
	new Ajax.Request(url, {
		method: 'post',
		    parameters: {
		    cmd: "JQDLGeditAdditionalRoleRow",
			editaddrole_HIDDEN_ID: "ADD",
			idSupplBrole: griditem.getMember('IDrole').Value,
			JQDLGbp_id:   griditem.getMember('IDroleToWhichGranted').Value,
			editaddrole_INPUT_EXPIRDATE: "",
			editaddrole_INPUT_RECERTSTARTDATE: "",
			editaddrole_SELECT_RECERTINTERVAL: "",
			comment: ""
			},
		    onSuccess: AJAXRESPONSEJQDLG_success_AdditRole,
		    onException: AJAXRESPONSEJQDLG_failure,
		    onFailure: AJAXRESPONSEJQDLG_failure
		    });
    }
    else {
	// The user is ADDING this to the set of registered additional roles
	$('spinnerImg').style.display = "";
	new Ajax.Request(url, {
		method: 'post',
		    parameters: {
		    cmd: "JQDLGeditAdditionalRoleRow",
			deleteme: "true",
			editaddrole_HIDDEN_ID: griditem.getMember('c_id').Value,
			idSupplBrole: griditem.getMember('IDrole').Value,
			JQDLGbp_id:   griditem.getMember('IDroleToWhichGranted').Value,
			editaddrole_INPUT_EXPIRDATE: "",
			editaddrole_INPUT_RECERTSTARTDATE: "",
			editaddrole_SELECT_RECERTINTERVAL: "",
			comment: ""
			},
		    onSuccess: AJAXRESPONSEJQDLG_success_AdditRole,
		    onException: AJAXRESPONSEJQDLG_failure,
		    onFailure: AJAXRESPONSEJQDLG_failure
		    });
    }

}


function AJAXRESPONSEJQDLG_success_AdditRole(transport)
{
    AJAXRESPONSEJQDLG_close();
    GRIDadditionalRoles.callback();
    $('spinnerImg').style.display = "none";
}
