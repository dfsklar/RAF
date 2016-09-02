$JQ(document).ready(JQdeclareDialogs);


function JQdeclareDialogs()
{
    CALLBACKadditionalRoles.add_callbackComplete(EVTHNDL_callbackAdditionalRoles_callbackcomplete);


	$JQ("#JQDLGbusroleProperties").dialog({
		autoOpen:false,
		    modal:true,
		    minWidth:680, width:680,
		    minHeight:500, height:500,
		    buttons:{"OK":JQDLGbusroleProperties_OK, "Cancel":function(){$JQ(this).dialog("close");} }
	    });
	$JQ("#JQDLGchangeMgmt").dialog({
		autoOpen:false,
		    modal:true,
		    minWidth:680, width:680,
		    minHeight:200, height:200,
		    buttons:{"Close":function(){$JQ(this).dialog("close");} }
	    });
	$JQ("#JQDLGsupplementalRoleList").dialog({
		autoOpen:false,
		    modal:true,
		    minWidth:680, width:900,
		    minHeight:200, height:200,
		    buttons:{"Close":function(){$JQ(this).dialog("close");} }
	    });
	$JQ("#JQDLGeditChgMgmtRow").dialog({
		autoOpen:false,
		    modal:true,
		    minWidth:480, width:480,
		    minHeight:400, height:400,
		    buttons:{"OK":JQDLGeditChgMgmtRow_OK, "Cancel":function(){$JQ(this).dialog("close");} }
	    });
	$JQ("#JQDLGeditAdditionalRoleRow").dialog({
		autoOpen:false,
		    modal:true,
		    minWidth:480, width:480,
		    minHeight:400, height:400,
		    buttons:{"OK":JQDLGeditAdditionalRoleRow_OK, "Cancel":GRIDeditaddrole_CloseJQdialog }
	    });

}




var GLOBALdialogToClose = "";


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
    var HASHparams = $JQ(":input");

    //HASHparams["cmd"] = "JQDLGbusroleProperties";
    var url = "GuidedEditor/ajaxupdater.ashx";

    // This will serialize the dialog input controls:
    // jQuery.param($JQ(":input"))
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
}
function AJAXRESPONSEJQDLG_failure(transport)
{
    alert("Failure occurred - changes were not necessarily retained.");
    AJAXRESPONSEJQDLG_close();
}

function AJAXRESPONSEJQDLG_close()
{
    if (GLOBALdialogToClose) {
	GLOBALdialogToClose.dialog("close");
	GLOBALdialogToClose = "";
    }else{
	$JQ("#JQDLGbusroleProperties").dialog("close");
	$JQ("#JQDLGeditChgMgmtRow").dialog("close");
    }
}







function LookupEID(CTLIDeid,CTLtargetprefix,BOOLbequiet)
{
    var url = 
	"GuidedEditor/ajaxupdater.ashx?cmd=FindUserByEID&arg1=" + $F(CTLIDeid) + "&arg2=" + CTLtargetprefix;

    // This URL will return the surname then vertbar than firstname, if found.
    // Or it returns "NOTFOUND"
    
    if (BOOLbequiet) {

	new Ajax.Request(url, {
		method: 'get',
		    onSuccess: AJAXRESPONSEHANDLE_lookupeid_successsilent,
		    onException: AJAXRESPONSEHANDLE_lookupeid_silent,
		    onFailure: AJAXRESPONSEHANDLE_lookupeid_silent
		    }
	    );

    }else {
	new Ajax.Request(url, {
		method: 'get',
		    onSuccess: AJAXRESPONSEHANDLE_lookupeid_success,
		    onException: AJAXRESPONSEHANDLE_lookupeid_failure,
		    onFailure: AJAXRESPONSEHANDLE_lookupeid_failure
		    }
	    );
    }
}


function AJAXRESPONSEHANDLE_lookupeid_silent()
{}



// This is a pair of near-identical functions; they must be kept in sync.
function AJAXRESPONSEHANDLE_lookupeid_success(arg1,arg2,arg3) {
    var ARRresult = arg1.responseText.split("|");
    var emitctx = ARRresult[0];
    if (ARRresult[1] == "NOTFOUND") {
	alert("That EID was not found");
	$(emitctx+"last").value = "";
	$(emitctx+"first").value = "";
	return;
    }
    if (ARRresult[1].substr(0,5) == "USERI") {
	alert("That user is registered in the user database but without name details.  We invite you to add that information.");
	$(emitctx+"last").value = "";
	$(emitctx+"first").value = "";
	return;
    }
    $(emitctx+"last").value = ARRresult[1];
    $(emitctx+"first").value = ARRresult[2];
}
function AJAXRESPONSEHANDLE_lookupeid_successsilent(arg1,arg2,arg3) {
    var ARRresult = arg1.responseText.split("|");
    var emitctx = ARRresult[0];
    if (ARRresult[1] == "NOTFOUND") {
	//alert("That EID was not found");
	$(emitctx+"last").value = "";
	$(emitctx+"first").value = "";
	return;
    }
    if (ARRresult[1].substr(0,5) == "USERI") {
	//alert("That user is registered in the user database but without name details.  We invite you to add that information.");
	$(emitctx+"last").value = "";
	$(emitctx+"first").value = "";
	return;
    }
    $(emitctx+"last").value = ARRresult[1];
    $(emitctx+"first").value = ARRresult[2];
}



function AJAXRESPONSEHANDLE_lookupeid_failure(arg1,arg2,arg3) {
    alert("ERROR OCCURRED");
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


function GRIDeditaddrole_LaunchJQdialog()
{    
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
    $JQ("#JQDLGeditAdditionalRoleRow").dialog("close");
    $('HIDDEN_CLOSET').appendChild
	($('WRAPPER_combobox_rolechooser'));
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
}




function LaunchJQdialog_SupplementalRoles(gritem)
{
    GLOBALcontextOfDialog = gritem;

    $('JQDLGbp_id').value = gritem.getMember("c_id").Value;

    $('ctl00_HIDDENcurbrole').value = gritem.getMember("c_id").Value;
    $('ctl00_HIDDENcurbrole').callback();

    // Step 1: send the server the integer id of the currently selected busrole.
    // The server will take that info and stuff it into a session variable that
    // the SQL data source in the aspx will be using.
    CALLBACKadditionalRoles.callback(gritem.getMember("c_id").Value);

    $JQ("#JQDLGsupplementalRoleList").dialog("option", "title", 
           "Additional Roles Allowed for: " + gritem.getMember("c_u_Name").Value);
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
    //GRIDadditionalRoles.callback();

    $JQ("#JQDLGsupplementalRoleList").dialog("open");
}



function LaunchJQdialog(gritem)
{
     GLOBALcontextOfDialog = gritem;
    $('JQDLGbp_id').value = gritem.getMember("c_id").Value;
    $('JQDLGbp_name').value = gritem.getMember("c_u_Name").Value;
    $('JQDLGbp_descr').value = gritem.getMember("c_u_Description").Value;
    $('JQDLGbp_primown_eid').value = gritem.getMember("c_u_OwnerPrimaryEID").Value;
    $('JQDLGbp_secown_eid').value = gritem.getMember("c_u_OwnerSecondaryEID").Value;
    $('JQDLGbp_designdetails').value = gritem.getMember("c_u_DesignDetails").Value;


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
    $JQ("#JQDLGchangeMgmt").dialog("open");
}




function PublishWorkspace(idofws)
{
    var cmt = prompt("Please enter the comment that should describe this new entitlement set:");
    if (cmt) {
	new Ajax.Request
	    (
	     "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "PublishEntAssSet",
			 arg1: idofws,
			 arg2: cmt
			 },
		     onSuccess: function(origrequest)
		     {
			 window.location.replace("PAGE_BRoles_Workspace.aspx");
		     }
	     }
	     );
    }
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
		     }
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
