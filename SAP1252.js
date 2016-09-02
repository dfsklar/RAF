$JQ(document).ready(JQdeclareDialogs);
function JQdeclareDialogs()
{
    $JQ("#JQDLGeditRow").dialog({
	    autoOpen:false,
		closeOnEscape: false,
		modal:true,
		minWidth:450, width:450,
		minHeight:295, height:295,
		buttons:{
		"OK":function(){
		    AJAXSENDeditrowcompletion();
		},
		    "Cancel":function(){
			$JQ(this).dialog("close");
		    }
	    }
	});
}



function Grid1_onContextMenu(sender, eventArgs)
{
    if (IsReadOnly()) {
	alert("You are viewing this in READ-ONLY mode.");
	return;
    }

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

    var menutoshow;

    if (BOOLclickOnAlreadySelectedItem) {
	if (length>1) {
	    // DO NOT ALLOW EDITING. 
	    GridContextMenuNOEDIT.showContextMenuAtEvent(eventArgs.get_event());
	    GridContextMenuNOEDIT.set_contextData(eventArgs.get_item());
	    menutoshow = GridContextMenuNOEDIT;
	}else{
	    GridContextMenu.showContextMenuAtEvent(eventArgs.get_event());
	    GridContextMenu.set_contextData(eventArgs.get_item());
	    GridContextMenu.findItemById("menuDataSAP_UnDelete").set_visible(false);
	    menutoshow = GridContextMenu;
	}
    }else{
	// These statements are for forcing the selection to be JUST the item right-clicked on:
	menutoshow = GridContextMenu;
	Grid1.select(eventArgs.get_item());
	//GridContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    }


    GridContextMenu.findItemById("menuDataSAP_Edit").set_visible(true);
    GridContextMenu.findItemById("menuDataSAP_NewClone").set_visible(true);
    GridContextMenu.findItemById("menuDataSAP_NewBlank").set_visible(true);
    GridContextMenu.findItemById("menuDataSAP_Delete").set_visible(true);
    GridContextMenu.findItemById("menuDataSAP_UnDelete").set_visible(false);

    if (menutoshow == GridContextMenu) {
	GridContextMenu.set_contextData(eventArgs.get_item());
	if (eventArgs.get_item().getMember('c_u_EditStatus').Value & 4) {
	    GridContextMenu.findItemById("menuDataSAP_Edit").set_visible(false);
	    GridContextMenu.findItemById("menuDataSAP_NewClone").set_visible(false);
	    GridContextMenu.findItemById("menuDataSAP_NewBlank").set_visible(false);
	    GridContextMenu.findItemById("menuDataSAP_Delete").set_visible (false);
	    GridContextMenu.findItemById("menuDataSAP_UnDelete").set_visible(true);
	}
    }
    menutoshow.showContextMenuAtEvent(eventArgs.get_event());
}





function Grid1_onItemSelect()
{
}



function MENUACTedit()
{
    JDLGeditRow_Launch(rightclickItem.getMember('c_id').Value);
}

function MENUACTclone()
{
    JDLGeditRow_Launch("ADD");
}



function MENUACTnewblank()
{
    if (IsReadOnly()) {
	alert("You are viewing this in READ-ONLY mode.");
	return;
    }
    rightclickItem = null;
    JDLGeditRow_Launch("ADD");
}




function RENDEReditstatus(gridrowitem)
{
    var v = (gridrowitem.getMember('c_u_EditStatus').get_value());
    // v is now a number, e.g. 32 means "NEW"
    if (v & 2) {
	return "<IMG alt='NEW entitlement' src='media/ADDED.gif'/>";
    }
    if (v & 4) {
	return "<IMG alt='DELETED entitlement' src='media/DELETED.gif'/>";
    }

    var retval = "";
    if (v & 8) {
	retval += "<IMG alt='Change occurred in at least one entitlement field' src='media/EDITEDvector.gif'/>";
    }
    if (v & 1) {
	retval += "<IMG alt='Change occurred in the commentary' src='media/EDITEDcommentary.gif'/>";
    }
    return retval;
}








function JDLGeditRow_Launch(IDtass)
{
    $('HIDcurrowid').value = IDtass;
    if (rightclickItem) {
	$('DLGCTL_c_u_FieldName').value = rightclickItem.getMember('c_u_FieldName').Value;
	$('DLGCTL_c_u_RangeLow').value = rightclickItem.getMember('c_u_RangeLow').Value;
	$('DLGCTL_c_u_RangeHigh').value = rightclickItem.getMember('c_u_RangeHigh').Value;
    }else{
	$('DLGCTL_c_u_FieldName').value = "";
	$('DLGCTL_c_u_RangeLow').value = "";
	$('DLGCTL_c_u_RangeHigh').value = "";
    }

    var itemInCB;


    
    $JQ("#JQDLGeditRow").dialog("open");

}








function AJAXSENDeditrowcompletion()
{
    var VALidentit = "";
    if (rightclickItem) {
	VALidentit = rightclickItem.getMember('c_id').Value;
    }
    new Ajax.Request
	(
	 "GuidedEditor/ajaxupdater.ashx",
	 {parameters:
	     {
		 cmd: "Edit1252row",
		     IDtass: $F($('HIDcurrowid')),
		     IDuser: $F($('HIDuserid')),
		     IDws: $F($('HIDsapwsid')),
		     ipaddr: $F($('HIDDENipaddr')),
		     IDentit: VALidentit,
		     IDrole: $F($('HIDroleid')), /* aspx gets this from codebehind class, which comes from querystring RoleID */
		     c_u_Field: $F($('DLGCTL_c_u_FieldName')),
		     c_u_RangeLow: $F($('DLGCTL_c_u_RangeLow')),
		     c_u_RangeHigh: $F($('DLGCTL_c_u_RangeHigh'))
		     },
		 onSuccess: function(origrequest)
		 {
		     $JQ("#JQDLGeditRow").dialog("close");
		     Grid1.callback();
		 },
		onException: AJAXRESPONSEJQDLG_failure,
		onFailure: AJAXRESPONSEJQDLG_failure
	 }
	 );
}





function AJAXRESPONSEJQDLG_failure(transport)
{
    alert("ERROR/Warning: " + transport.responseText);
    $JQ("#JQDLGeditRow").dialog("close");
}



function MENUACTundelete()
{
    var VALidentit = "";
    if (rightclickItem) {
	VALidentit = rightclickItem.getMember('c_id').Value;
    }
    new Ajax.Request
	(
	 "GuidedEditor/ajaxupdater.ashx",
	 {parameters:
	     {
		 cmd: "Undelete1252row",
		     IDuser: $F($('HIDuserid')),
		     IDws: $F($('HIDsapwsid')),
		     ipaddr: $F($('HIDDENipaddr')),
		     IDentit: VALidentit,
		     IDrole: $F($('HIDroleid')) /* aspx gets this from codebehind class, which comes from querystring RoleID */
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



function MENUACTdelete()
{
    var VALidentit = "";
    if (rightclickItem) {
	VALidentit = rightclickItem.getMember('c_id').Value;
    }
    new Ajax.Request
	(
	 "GuidedEditor/ajaxupdater.ashx",
	 {parameters:
	     {
		 cmd: "Delete1252row",
		     IDuser: $F($('HIDuserid')),
		     IDws: $F($('HIDsapwsid')),
		     ipaddr: $F($('HIDDENipaddr')),
		     IDentit: VALidentit,
		     IDrole: $F($('HIDroleid')) /* aspx gets this from codebehind class, which comes from querystring RoleID */
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
