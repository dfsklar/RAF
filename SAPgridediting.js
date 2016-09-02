$JQ(document).ready(JQdeclareDialogs);
function JQdeclareDialogs()
{
    $JQ("#JQDLGeditRow").dialog({
	    autoOpen:false,
		closeOnEscape: false,
		modal:true,
		minWidth:450, width:450,
		minHeight:415, height:415,
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





// This will automatically capture the case of no selection
// and return "" instead.
function SafeComboboxGetSel(CBOX)
{
    if (CBOX.getSelectedItem()) {
	return CBOX.getSelectedItem().get_value();
    }else{
	return "";
    }
}


function AJAXSENDeditrowcompletion()
{
    var VALidentit = "";
    if (rightclickItem) {
	VALidentit = rightclickItem.getMember('IDTcodeEnt').Value;
    }

    new Ajax.Request
	(
	 "GuidedEditor/ajaxupdater.ashx",
	 {parameters:
	     {
		 cmd: "EditSAPentitlement",
		     IDtass: $F($('HIDcurrowid')),
		     IDuser: $F($('HIDuserid')),
		     IDws: $F($('HIDsapwsid')),
		     ipaddr: $F($('HIDDENipaddr')),
		     IDentit: VALidentit,
		     IDrole: $F($('HIDroleid')), /* aspx gets this from codebehind class, which comes from querystring RoleID */
		     c_u_TCode: $F($('DLGCTL_c_u_TCode')),
		     c_u_Comment: $F($('DLGCTL_c_u_Comment')),
		     c_u_ActivityFolder: $F($('DLGCTL_c_u_ActivityFolder')),
		     c_u_Type: SafeComboboxGetSel(COMBOBOXtype),
		     c_u_AccessLevel: SafeComboboxGetSel(COMBOBOXaccesslevel)
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


function JDLGeditRow_Launch(IDtass)
{
    $('HIDcurrowid').value = IDtass;
    $('TXTtcodedescr').innerHTML = "";
    if (rightclickItem) {
	$('DLGCTL_c_u_TCode').value = rightclickItem.getMember('c_u_TCode').Value;
	$('DLGCTL_c_u_ActivityFolder').value = rightclickItem.getMember('c_u_ActivityFolder').Value;
	$('DLGCTL_c_u_Comment').value = rightclickItem.getMember('CommentOnEntitlement').Value;
	COMBOBOXaccesslevel.selectItem
	    (
	     COMBOBOXaccesslevel.findItemByProperty("Value", 
						    rightclickItem.getMember("c_u_AccessLevel").Value));
	COMBOBOXtype.selectItem
	    (
	     COMBOBOXtype.findItemByProperty("Value", 
						    rightclickItem.getMember("c_u_Type").Value));


    }else{

	$('DLGCTL_c_u_TCode').value = "";
	$('DLGCTL_c_u_ActivityFolder').value = "";
	$('DLGCTL_c_u_Comment').value = "";
	COMBOBOXaccesslevel.selectItem
	    (
	     COMBOBOXaccesslevel.findItemByProperty("Value", "U"));
	COMBOBOXtype.selectItem
	    (
	     COMBOBOXtype.findItemByProperty("Value", "M"));
    }

    var itemInCB;



    
    $('HIDDEN_CLOSET').appendChild 
	($('WRAPPER_COMBOBOXtype'));
    $('HIDDEN_CLOSET').appendChild 
	($('WRAPPER_COMBOBOXaccesslevel'));

    $JQ("#JQDLGeditRow").dialog("open");

    $('TARGET_COMBOBOXtype').appendChild 
	($('WRAPPER_COMBOBOXtype'));
    $('TARGET_COMBOBOXaccesslevel').appendChild 
	($('WRAPPER_COMBOBOXaccesslevel'));

}



function MENUACTedit()
{
    if (IsReadOnly()) {
	alert("You are viewing this in READ-ONLY mode.");
	return;
    }
    JDLGeditRow_Launch(rightclickItem.getMember('IDtass').Value);
}

function MENUACTclone()
{
    if (IsReadOnly()) {
	alert("You are viewing this in READ-ONLY mode.");
	return;
    }
    JDLGeditRow_Launch("ADD");
}


// This is actually a TOGGLER, so it 
// also does undeletion.
function MENUACTdelete()
{
    if (IsReadOnly()) {
	alert("You are viewing this in READ-ONLY mode.");
	return;
    }
    //alert("Entering MENUACTdelete");
    var itemArray = Grid1.getSelectedItems();
    Grid1.beginUpdate();
    for (var i = 0;i<itemArray.length;i++) {
	//var editstatus = itemArray[i].getMember('c_u_EditStatus').Value;
	Grid1.deleteItem(itemArray[i]);
     }
    Grid1.endUpdate();
}

function MENUACTundelete()
{
    if (IsReadOnly()) {
	alert("You are viewing this in READ-ONLY mode.");
	return;
    }
    MENUACTdelete();
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


function LookupTcode()
{
    var newtcode = $F($('DLGCTL_c_u_TCode'));
    $('TXTtcodedescr').innerHTML = "...";
    var xxx = new Ajax.Updater
	("TXTtcodedescr", "GuidedEditor/ajaxupdater.ashx",
	 {
	     parameters: {
		 cmd: "FindTcode",
		 arg1: newtcode
	     }
	 }
	 );
}



	     

function JSFUNCinitTcodeDescr(DataItem)
{
    $('txtTCODEDESCR').innerHTML = DataItem.getMember('TCodeDescr').Value;
}



function JSFUNCinitCommentBox(DataItem)
{
    $('TXTAREAcommentbox').value = DataItem.getMember('c_u_Commentary').get_text();
}
function JSFUNCgetvalFromCommentBox()
{
    return $('TXTAREAcommentbox').value;
}


function JSFUNCinitComboboxOrgAxisList(DataItem)
{
    alert("NO LONGER USED JSFUNCinitComboboxOrgAxisList");
    var ComboBox1 = COMBOBOXOrgAxisList;
    COMBOBOXOrgAxisList.beginUpdate();
    ComboBox1.unSelect();
    alert(DataItem.getMember('c_u_OrgAxisList').get_text());
    for(var i = 0; i < ComboBox1.get_itemCount(); i++)
	{
	    var item = ComboBox1.getItem(i);
	    if(item.get_text() == DataItem.getMember('c_u_OrgAxisList').get_text())
		{
		    ComboBox1.selectItem(item);
		    break;
		}
	}
    ComboBox1.endUpdate();
    ComboBox1.Initialize();
}
function JSFUNCinitComboboxOrgValue(DataItem)
{
    var ComboBox1 = COMBOBOXOrgValue;
    ComboBox1.unSelect();
    ComboBox1.beginUpdate();
    for(var i = 0; i < ComboBox1.get_itemCount(); i++)
	{
	    var item = ComboBox1.getItem(i);
	    if(item.get_text() == DataItem.getMember('c_u_OrgValue').get_text())
		{
		    ComboBox1.selectItem(item);
		    break;
		}
	}
    ComboBox1.endUpdate();
    ComboBox1.Initialize();
}
function JSFUNCinitComboboxSAProle(DataItem)
{
    var ComboBox1 = COMBOBOXsaprole;
    ComboBox1.unSelect();
    ComboBox1.beginUpdate();
    for(var i = 0; i < ComboBox1.get_itemCount(); i++)
	{
	    var item = ComboBox1.getItem(i);
	    if(item.get_text() == DataItem.getMember('SAProlename').get_text())
		{
		    ComboBox1.selectItem(item);
		    break;
		}
	}
    ComboBox1.endUpdate();
    ComboBox1.Initialize();
}







function JSFUNCgetvalFromComboboxOrgAxisList()
{
    var ComboBox1 = COMBOBOXOrgAxisList;
    var item = ComboBox1.getSelectedItem();
    if (item == null) {
	return null;
    }else{
	return item.get_value();  //used to be get_value
    }
}
function JSFUNCgetvalFromComboboxOrgValue()
{
    var ComboBox1 = COMBOBOXOrgValue;
    var item = ComboBox1.getSelectedItem();
    if (item == null) {
	return null;
    }else{
	return item.get_text();
    }
}
function JSFUNCgetvalFromComboboxSAProle()
{
    var ComboBox1 = COMBOBOXsaprole;
    var item = ComboBox1.getSelectedItem();
    return item.get_value();  //used to be get_value
}



function sapnoop()
{
}




function CloneRow(Grid1)
{
    var itemArray = Grid1.getSelectedItems();
    var length = itemArray.length;
    if (length > 1) {
	alert("Cloning can only be done if exactly one row is selected.");
	return;
    }
    if (length < 1) {
	alert("Select a row that you wish to clone");
	return;
    }

    GLOBALitemToClone = itemArray[0];
    // The cursel item's index is singleGridItem.get_index()

    // This is successful for finding current values (labels, not behindthescenes IDs)
    //alert(singleGridItem.getMember('SAProlename').get_text());

	 
    GLOBALcloneRecipient = Grid1.Table.addEmptyRow(GLOBALitemToClone.get_index());

    GLOBALfieldsForCloning.each(CloneSingleField);

    // Tell this row it was an addition so it shows proper icon
    GLOBALcloneRecipient.setValue(2, 2, true);
    
    // Invite the user to edit this new clone:
    Grid1.select(GLOBALcloneRecipient);
    Grid1.edit(GLOBALcloneRecipient);
}


function CloneSingleField(fieldname)
{
    var colnumSAProle = GLOBALitemToClone.getMember(fieldname).get_column().get_columnNumber();
    GLOBALcloneRecipient.setValue(colnumSAProle, GLOBALitemToClone.getMember(fieldname).get_text(), true);
}



var GLOBALfieldsForCloning =
    [
     "c_u_TCode",
     "TCodeDescr",
     "c_u_AuthObj",
     "c_u_Activity",
     "c_u_OrgAxisList",
     "c_u_OrgValue"
     ];



function EVENT_SelChange_COMBOBOXtype(sender, eventArgs)
{
    return;

    // We know that we ARE getting here.
    /*
    COMBOBOXOrgValue.disable();
    if (sender.getSelectedItem() == null) {
	return;
    }
    COMBOBOXOrgValue.filter(sender.getSelectedItem().get_value());
    */
}

function EVENT_FilterCompleted_COMBOBOXOrgValue(sender, eventArgs)
{
    COMBOBOXOrgValue.enable();
}
