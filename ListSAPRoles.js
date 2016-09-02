$JQ(document).ready(JQdeclareDialogs);


function JQdeclareDialogs()
{

	$JQ("#JQDLGsaproleProperties").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:680, width:680,
		    minHeight:300, height:300,
		    buttons:{"OK":JQDLGsaproleProperties_OK, "Cancel":function(){$JQ(this).dialog("close");} }
	    });

	$JQ("#JQDLGchangeMgmt").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:911, width:911,
		    minHeight:200, height:200,
		    buttons:{"Close":function(){$JQ(this).dialog("close");} }
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

}


var GLOBALdialogToClose = "";














function RenderEditArea(dataitem)
{
    var retstr;
    if (GLOBALpageIsEditable) {

	retstr =
	    "<a href=\"javascript:Grid1.edit(Grid1.getItemFromClientId('" 
	    + DataItem.ClientId + "'));\">&gt;&gt;</a>";

    }else{
	retstr = 
	    "<A href='SAPEntitlementWorkspace.aspx?RoleID=" 
	    + DataItem.GetMember("c_id").Value 
	    + "&RoleName=" + escape(DataItem.GetMember("c_u_Name").Value)
	    + "&RolePlatform=" + escape(DataItem.GetMember("c_u_Platform").Value)
	    + "'>View</A>";
    }

    return retstr;
}





function DeleteSAPRole()
{
    var cmt = prompt("Please enter the reason for this deletion action:", "");
    if (cmt) {
	new Ajax.Request
	    (
	     "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "DeleteSAPRole",
			 id: GLOBcontextGridRow.getMember("c_id").Value,
			 comment: cmt
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
}




function Grid1_onCallbackError(sender, eventArgs)
{
    alert(eventArgs.get_errorMessage());
}


function PublishWorkspace(idofws)
{
    var cmt = prompt("Please enter the comment that should describe this new published SAP entitlement set:");
    if (cmt) {
	new Ajax.Request
	    (
	     "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "PublishSAPEntAssSet",
			 arg1: idofws,
			 arg2: cmt
			 },
		     onSuccess: function(origrequest)
		     {
			 window.location.replace("Page_SAP_History.aspx");
		     },
		     onException: AJAXRESPONSEJQDLG_failure,
		     onFailure: AJAXRESPONSEJQDLG_failure
		     }
	     );
    }
}








function LaunchChangeMgmt(idofws)
{
    //CALLBACKinitChangeMgmt.Callback(idofws);
    LaunchChangeMgmt_Continue();
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

function GRIDchgmgmt_RenderEditArea(dataitem)
{
    var retstr = "";
    retstr +=
	"<a href=\"javascript:GRIDchgmgmt_LaunchJQdialog(GRIDchangeMgmt.getItemFromClientId('" + dataitem.ClientId + "'));\">&gt;</a>";
    return retstr;
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



function JQDLGeditChgMgmtRow_OK()
{
    var url = "GuidedEditor/ajaxupdater.ashx";

    GLOBALdialogToClose = $JQ("#JQDLGeditChgMgmtRow");

    var HASHparams1 = $JQ(":input");
    var HASHparams2 = $JQ(":textarea");

    new Ajax.Request(url, {
	    method: 'post',
		postBody: jQuery.param(HASHparams1) + "&cmd=JQDLGeditChgMgmt_SAP"
		+ "&comment=" + escape($('editchgmgmtrow_TEXTAREA_COMMENTS').value),
		onSuccess: function(transport) 
		{
		    GRIDchangeMgmt.callback();
		    $JQ("#JQDLGeditChgMgmtRow").dialog("close");
		},
		onException: AJAXRESPONSEJQDLG_failure,
		onFailure: AJAXRESPONSEJQDLG_failure
		}
	);
}






function AJAXRESPONSEJQDLG_failure(transport)
{
    alert("ERROR/Warning: " + transport.responseText);
    if (GLOBALdialogToClose) {
	GLOBALdialogToClose.dialog("close");
	GLOBALdialogToClose = "";
    }
}






function Grid1_onContextMenu(sender, eventArgs)
{
    var itemArray = sender.getSelectedItems();
    var length = itemArray.length;

    // GLOBAL
    GLOBcontextGridRow = eventArgs.get_item();
    GLOBevent = eventArgs.get_event();

    // FORCE SELECTION OF THE ITEM INVOLVED
    sender.select(GLOBcontextGridRow);

    // The set-cur-sap-role-on-server callbacks are now going to be done in the event handlers
    // for the actual menu items.

    GridContextMenu.showContextMenuAtEvent(GLOBevent);
    GridContextMenu.set_contextData(GLOBcontextGridRow);
}



function CALLBACKselectCurRole_Continue()
{
    switch (GLOBALdialogToLaunch) {
    case "RoleOwners":
	//GRIDroleowners.Callback();
	$('HIDDEN_CLOSET').appendChild 
	    ($('WRAPPER_GRIDroleowners'));

	$JQ("#JQDLGroleowners").dialog("option", "title", 
				       "Role Owners for: " + GLOBcontextGridRow.getMember("c_u_Name").Value);
	$JQ("#JQDLGroleowners").dialog("open");

	$('TARGETHOME_GRIDroleowners').appendChild 
	    ($('WRAPPER_GRIDroleowners'));
	break;
    }	

}



function MENUACTdiveIntoDesign()
{
    window.location.replace("SAPEntitlementWorkspace.aspx?RoleID="
			    + GLOBcontextGridRow.getMember("c_id").Value
			    + "&RoleName=" 
			    + escape(GLOBcontextGridRow.getMember("c_u_Name").Value)
			    + "&RolePlatform=" 
			    + escape(GLOBcontextGridRow.GetMember("c_u_Platform").Value)
			    );
}





function MENUACTdiveIntoDesign1252()
{
    window.location.replace("SAP1252Workspace.aspx?RoleID="
			    + GLOBcontextGridRow.getMember("c_id").Value
			    + "&RoleName=" 
			    + escape(GLOBcontextGridRow.getMember("c_u_Name").Value)
			    + "&RolePlatform=" 
			    + escape(GLOBcontextGridRow.GetMember("c_u_Platform").Value)
			    );
}
function MENUACTdiveIntoDesign1251()
{
    window.location.replace("SAP1251Workspace.aspx?RoleID="
			    + GLOBcontextGridRow.getMember("c_id").Value
			    + "&RoleName=" 
			    + escape(GLOBcontextGridRow.getMember("c_u_Name").Value)
			    + "&RolePlatform=" 
			    + escape(GLOBcontextGridRow.GetMember("c_u_Platform").Value)
			    );
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
    $('TD_editroleowner_SELECT_RANK').value = G.getMember("c_u_Rank").Value;

    $('JQDLGbp_primown_namelast').value = "";
    $('JQDLGbp_primown_namefirst').value = "";
    
    $('ROW_editroleowner_CHK_DEL').style.visibility = 'visible';
    $('editroleowner_CHK_DEL').checked = false;

    $JQ("#JQDLGeditRoleOwnerRow").dialog("open");
}



function MENUACTowners()
{
    var DataItem = GLOBcontextGridRow;
    LaunchJQdialog_RoleOwners(Grid1.getItemFromClientId(DataItem.ClientId));
}



function LaunchJQdialog_RoleOwners(gritem)
{
    // The context of the dialog is a single SAP role
    GLOBALcontextOfDialog = gritem;

    // Step 1: send the server the integer id of the currently selected SAProle
    // The server will take that info and stuff it into the hidden field
    // that the SQL data source points to.
    GLOBALdialogToLaunch = "RoleOwners";

    // This doesn't cause the hiddenfield to callback but it's supposed to (onvaluehcanged!)
    //$('ctl00_ContentPlaceHolder1_HIDFIELDcurRoleId').value = gritem.getMember("c_id").Value;

    CALLBACKselectCurRole.callback(gritem.getMember("c_id").Value);
}




function GRIDroleowners_LaunchJQdialog_ADDNEW()
{
    $('editroleowner_HIDDEN_ID').value  = "ADD";
    $('ROW_editroleowner_CHK_DEL').style.visibility = "hidden";
    GRIDeditroleowner_LaunchJQdialog();
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

function JQDLGeditRoleOwner_OK()
{
    GLOBALdialogToClose = $JQ("#JQDLGeditRoleOwnerRow");
    var url = "GuidedEditor/ajaxupdater.ashx";
    new Ajax.Request(url, {
	    method: 'post',
		parameters: {
		cmd: "JQDLGeditSAPRoleOwner",
		    idcuredit: $F($('editroleowner_HIDDEN_ID')),
		    idsaprole: GLOBALcontextOfDialog.getMember("c_id").Value,
		    deleteme: $('editroleowner_CHK_DEL').checked,
		    geo: $F($('TD_editroleowner_INPUT_GEO')),
		    eid: $F($('TD_editroleowner_INPUT_EID')),
		    rank: $F($('TD_editroleowner_SELECT_RANK')),
		    NAMEsur: $F($('JQDLGbp_primown_namelast')),
		    NAMEfirst: $F($('JQDLGbp_primown_namefirst'))
		    }
		,
		onSuccess: function(transport) 
		{
		    GLOBALdialogToLaunch = "";
		    CALLBACKselectCurRole.callback(GLOBALcontextOfDialog.getMember("c_id").Value);
		    $JQ("#JQDLGeditRoleOwnerRow").dialog("close");
		},
		onException: AJAXRESPONSEJQDLG_failure,
		onFailure: AJAXRESPONSEJQDLG_failure
		}
	);
}



function AddNewRole()
{
    GLOBcontextGridRow = null;

    MENUACTproperties();
    
    //Grid1.Table.AddRow();
}




function MENUACTproperties()
{
    GLOBALcontextOfDialog = GLOBcontextGridRow;
    var gritem = GLOBcontextGridRow;
     if (gritem) {
	 $('JQDLGbp_id').value = gritem.getMember("c_id").Value;
	 $('JQDLGbp_name').value = gritem.getMember("c_u_Name").Value;
	 $('JQDLGbp_description').value = gritem.getMember("c_u_Description").Value;
	 $('JQDLGbp_system').value = gritem.getMember("c_u_System").Value;
	 $('JQDLGbp_platform').value = gritem.getMember("c_u_Platform").Value;
	 $('JQDLGbp_roleactivity').value = gritem.getMember("c_u_RoleActivity").Value;
	 $('JQDLGbp_roletype').value = gritem.getMember("c_u_RoleType").Value;
     }else{
	 $('JQDLGbp_id').value = "";
	 $('JQDLGbp_name').value = "";
	 $('JQDLGbp_description').value = "";
	 $('JQDLGbp_system').value = "";
	 $('JQDLGbp_platform').value = "";
	 $('JQDLGbp_roleactivity').value = "";
	 $('JQDLGbp_roletype').value = "";
     }

    $JQ("#JQDLGsaproleProperties").dialog("open");

    //Causing problems with statement handles being reused?:
    //LookupEID("JQDLGbp_primown_eid", "JQDLGbp_primown_name",true);
    //LookupEID("JQDLGbp_secown_eid", "JQDLGbp_secown_name",true);
}




function SETfromDLG(fldname)
{
    var lowercasefldname = fldname.toLowerCase();
    GLOBcontextGridRow.setValue(GLOBcontextGridRow.getMember('c_u_'+fldname).get_column().get_columnNumber(),
				$F($('JQDLGbp_'+lowercasefldname)));
}

function JQDLGsaproleProperties_OK()
{
    if (GLOBcontextGridRow == null) {
	JQDLGsaproleProperties_OK_BrandNewRole();
	return;
    }
    else {
	SETfromDLG("Name");
	SETfromDLG("Description");
	SETfromDLG("System");
	SETfromDLG("Platform");
	SETfromDLG("RoleActivity");
	SETfromDLG("RoleType");
	Grid1.callback();
	$JQ("#JQDLGsaproleProperties").dialog("close");
    }
}

function JQDLGsaproleProperties_OK_BrandNewRole()
{
    // This will serialize the dialog input controls:
    var HASHparams = $JQ(":input");

    var url = "GuidedEditor/ajaxupdater.ashx";

    new Ajax.Request(url, {
	    method: 'post',
		postBody: jQuery.param(HASHparams) + "&cmd=JQDLGsaproleProperties",
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
}

function AJAXRESPONSEJQDLG_close()
{
    $JQ("#JQDLGsaproleProperties").dialog("close");
}

    
