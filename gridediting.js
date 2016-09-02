var Grid1 = null;


function WEBUI_UTIL_isItemSelected(g,curItem)
{
    var clientid = curItem.get_clientId();
    var ARRselitems = g.getSelectedItems();
    for(var i = 0;i<ARRselitems.length;i++) {
	if (ARRselitems[i].get_clientId() == clientid) {
	    return true;
	}
    }
    return false;
}






function RENDERHTMLlaunchStatusChangeDialog(DataItem)
{
    return "<A href=\"javascript:LAUNCHdlogChangeStatus('" +
	DataItem.get_clientId() + "')\">"+ DataItem.GetMember("c_u_Status").Value + "</A>";
}


function REACTclickOnCommentIcon(clientID)
{
    Grid1.unSelectAll();
    GLOBcontextGridRow = Grid1.getItemFromClientId(clientID);
    Grid1.select(GLOBcontextGridRow);
    MENUACTviewreadonly();
}

function RENDERHTMLcommenticon(DataItem)
{
    if (DataItem.GetMember("c_u_Commentary").Value != '') {
	return "<IMG onclick='REACTclickOnCommentIcon(\""+DataItem.get_clientId()+"\");' src='images/tinycomment.gif'/></IMG>";
    }else{
	return "";
    }
}

function RENDERHTMLlaunchVectorManagement(DataItem)
{
    var curstat = DataItem.getMember("c_u_Status").Value;
    var STRbuild = 
	"<a href=\"javascript:MENUACTclone('" + DataItem.get_clientId() + "');\">Clon</a>";
    if (curstat == 'P') {
	STRbuild +=
	    "|<a href=\"javascript:MENUACTedit('" + DataItem.get_clientId() + "');\">Ed</a>";
	STRbuild +=
	    "|<a href=\"javascript:MENUACTdel('" + DataItem.get_clientId() + "');\">Del</a>";
    }
    return STRbuild;
}



var GLOBarrayValuesPreEdit = new Hash();
var GLOBarrayValuesPostEdit = new Hash();

var GLOBhandlesToDhtmlxComboboxes = new Hash();

var ARRoldBroleList = new Array();
var ARRoldEditStatus = new Array();

function RecordOldEditStatusForMultiRowSelect(theVal,theIndex)
{
    //	 ARRoldEditStatus.push(Grid1.getItemFromKey(0,theVal).getMember('c_u_EditStatus').get_text().strip());
    //	 ARRoldBroleList.push(Grid1.getItemFromKey(0,theVal).getMember('BusRolesLinked').get_text().strip());
}






var GLOBcontextGridRow;


	 




function MENUACTnewblankentitlement()
{
    DLGeditRow.set_modal(true);
    DLGeditRow.set_title("Create NEW entitlement");
    GLOBALdlgeditSubmitActionType = "NEWFRESH";
    GLOBALmodeOfDlgMaintRoleAssign = "n/a";
    DLGeditRow.Show();
    // CloneRow(Grid1, GridContextMenu.get_contextData());
}






function DLGEDITcancel()
{
    DLGeditRow.Close();
}
function DLGROLEMAINTcancel()
{
    DLGmaintRoleAssign.Close();
}







function DLGEDITsubmit()
{
    SubmitEditExisting("full");
    DLGeditRow.Close();
}
function DLGROLEMAINTsubmit()
{
    SubmitEditExisting("rolelinks");
    DLGmaintRoleAssign.Close();
}



// mode is either "full" or "rolelinks"
var GLOBALsubmitmode;


function RefreshManifestVerificationString()
{
    GLOBALentfieldlist.each
	    (
	     function(entfieldname) {
		 if (entfieldname == "Commentary") {
		     GLOBarrayValuesPostEdit.set(entfieldname,
						 $F("DIVcommentary").strip());
		 }
		 else if (entfieldname == "Status") {
		     GLOBarrayValuesPostEdit.set(entfieldname, "NOCHANGE");
		 }else{
		     GLOBarrayValuesPostEdit.set(entfieldname,
						 GLOBhandlesToDhtmlxComboboxes[entfieldname].getActualValue().strip());
		 }
	     }
	     );

    var jsonPOST = GLOBarrayValuesPostEdit.toJSON();

    new Ajax.Updater
	("DLGentitelementEditor_manifestverif",
	 "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "ComputeManifestString",
			 arg1: jsonPOST
			 }});
    
}





function SubmitEditExisting(submitmode)
{
    GLOBALsubitmode = submitmode;

    if (submitmode == "full") {
	// Collect all the values from the dialog's controls.
	GLOBALentfieldlist.each
	    (
	     function(entfieldname) {
		 if (entfieldname == "Commentary") {
		     GLOBarrayValuesPostEdit.set(entfieldname,
						 $F("DIVcommentary").strip());
		 }
		 else if (entfieldname == "Status") {
		     GLOBarrayValuesPostEdit.set(entfieldname, "NOCHANGE");
		 }else{
		     GLOBarrayValuesPostEdit.set(entfieldname,
						 GLOBhandlesToDhtmlxComboboxes[entfieldname].getActualValue().strip());
		 }
	     }
	     );

	// Now we have all the entitlement fields.  But we do
	// not yet have the newly selected list of linked busroles.

    }

    var jsonPRE = GLOBarrayValuesPreEdit.toJSON();
    var jsonPOST = GLOBarrayValuesPostEdit.toJSON();

    //alert(jsonPRE);
    //alert(jsonPOST);

    var BOOLentitlementChange = (jsonPRE != jsonPOST) && (jsonPOST!="{}");
    var BOOLlinkChange = false;  //newSetLinkedBroles.strip() != GLOBALlinkedBusRoles.strip();


    //alert(BOOLentitlementChange);
    //alert(BOOLlinkChange);

    if (!BOOLentitlementChange && !BOOLlinkChange) {
	//alert(GLOBALlinkedBusRoles);
	//alert(newSetLinkedBroles);
	alert("NOTE: No change was detected, so the submission had no effect.");
    }else{

	if (!BOOLlinkChange) {
	    newSetLinkedBroles = "--@--NOCHANGE--@--";
	}
	if (!BOOLentitlementChange) {
	    jsonPOST = "--@--NOCHANGE--@--";
	}


	ARRoldBroleList = new Array();
	ARRoldEditStatus = new Array();
		  
	Grid1.GetSelectedKeys().each(RecordOldEditStatusForMultiRowSelect);

	// Send an ajax message to server with the full "new" vector in JSON format.
	new Ajax.Request
	    ("GuidedEditor/RecordEntitlementMod.ashx",
	     {parameters:
		 {
		     mode: GLOBALsubmitmode,
			 wserowids: Grid1.GetSelectedKeys().toJSON(),
			 newvector: jsonPOST,
			 oldvector: jsonPRE,
			 edittype: GLOBALdlgeditSubmitActionType,
			 mode: GLOBALmodeOfDlgMaintRoleAssign,
			 multiselOldEditStat: ARRoldEditStatus.toJSON()
			 },
		     onSuccess: function(origrequest)
		     {
			 var resp = origrequest.responseText;
			 Grid1.callback();
			 if (resp != "") {
			     alert("The new item has been selected but it may lie on another page.  Autoscrolling to show the new item is not working yet.");
			     Grid1.selectByKey(parseInt(origrequest.responseText));
			     var ARRselitems = Grid1.GetSelectedItems();
			     //var IDXselitem = ARRselitems[0].get_index();
			     //alert(IDXselitem);
			 }
			 //Grid1.selectByKey(EGRIDtheentitlementID);
		     },
		     onFailure: function(origrequest)
		     {
			 alert("ERROR/Warning: " + origrequest.responseText.substr(0,150));
		     },
		     onException: function(origrequest)
		     {
			 alert("ERROR/Warning: " + origrequest.responseText.substr(0,150));
		     }
	     });
    }
}



// getRow requires an index!
//	 Grid1.get_table().getRow(1).setValue(1, 'M', true);

// GLOBAL set when the context menu was popped up
//	 rightclickItem.setValue(1,'M',true);







function dlogloadcompletion(f1,f2,f3)
{
}





function FUNCdhtmlxChildComboOnXmlLoaded(f1,f2)
{
    // Do not really need this right now.
}



// GLOBAL: list of all the persisted field names in an entitlement
var GLOBALentfieldlist = 
    [
     'StandardActivity',
     'RoleType',
     'Application',
     'System',
     'Platform',
     'EntitlementName',
     'EntitlementValue',
     'AuthObjValue',
     'FieldSecName',
     'FieldSecValue',
     'Level4SecName',
     'Level4SecValue',
     'Commentary'
     ];






//forEach event handler
function RecordValuesPreEdit(theVal,theIndex)
{
    //alert(theVal);
    GLOBarrayValuesPreEdit.set(theVal, EGRIDtherow.getMember('c_u_'+theVal).get_text().strip());
}
function EmptyValuesPreEdit(theVal,theIndex)
{
    GLOBarrayValuesPreEdit.set(theVal, "");
}





// Here is where we will find out the info about which row is
// selected in the grid, and all its info, and populate the
// comboboxes.

// THIS IS CALLED AUTOMATICALLY AS AN EVENT HANDLER (OnShow) REGISTERED WITH THE DIALOG
function onShow_DLGeditRow(dialog)
{
    EGRIDtherow = GLOBcontextGridRow;
    // This is the actual client-side complex-object row item , not just an index!

    if (EGRIDtherow == null) {
	//
	// I DON'T THINK THIS IS EVER USED
	//
	alert("ALERT line 246 of gridediting.js");
	GLOBALentfieldlist.each(EmptyValuesPreEdit);
	EGRIDtheStdAct = "";
	EGRIDtheRoleType = "";
	EGRIDthesystem = "";
	EGRIDtheappl = "";
	EGRIDtheplatform = "";
	EGRIDtheEntN = "";
	EGRIDtheEntV = "";
	EGRIDauthobj = "";
	EGRIDfsn = "";
	EGRIDfsv = "";
	EGRIDlev4sn = "";
	EGRIDlev4sv = "";
	EGRIDcmt = "";
	EGRIDtheentitlementID = "";
	GLOBALlinkedBusRoles = "";
    }
    else {

	GLOBALentfieldlist.each(RecordValuesPreEdit);

	EGRIDtheStdAct = (EGRIDtherow.getMember('c_u_StandardActivity').get_text());
	EGRIDtheRoleType = (EGRIDtherow.getMember('c_u_RoleType').get_text());
	EGRIDthesystem = (EGRIDtherow.getMember('c_u_System').get_text());
	EGRIDtheappl = (EGRIDtherow.getMember('c_u_Application').get_text());
	EGRIDtheplatform = (EGRIDtherow.getMember('c_u_Platform').get_text());
	EGRIDtheEntN = (EGRIDtherow.getMember('c_u_EntitlementName').get_text());
	EGRIDtheEntV = (EGRIDtherow.getMember('c_u_EntitlementValue').get_text());
	EGRIDauthobj = (EGRIDtherow.getMember('c_u_AuthObjValue').get_text());
	EGRIDfsn = (EGRIDtherow.getMember('c_u_FieldSecName').get_text());
	EGRIDfsv = (EGRIDtherow.getMember('c_u_FieldSecValue').get_text());
	EGRIDlev4sn = (EGRIDtherow.getMember('c_u_Level4SecName').get_text());
	EGRIDlev4sv = (EGRIDtherow.getMember('c_u_Level4SecValue').get_text());
	EGRIDcmt = (EGRIDtherow.getMember('c_u_Commentary').get_text());
	EGRIDtheentitlementID = (EGRIDtherow.getMember('c_id').get_text());
	EGRIDtheStatus = (EGRIDtherow.getMember('c_u_Status').get_text());
    }

    var z = "";

    var ARRnm = GLOBALentfieldlist;

    var i = 0;

    /* BEHAV CHANGE BY DFSKLAR ON 16 May:
       All fields range of values are scoped to the current application (EGRIDtheappl).
    */

    z = new dhtmlXCombo("DIVchooseSTDACT","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z;
    z.loadXML("GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=StandardActivity&select=" + escape(EGRIDtheStdAct), dlogloadcompletion);
    z.enableFilteringMode(false);

    z = new dhtmlXCombo("DIVchooseROLETYPE","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z;
    z.loadXML("GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=RoleType&select=" + escape(EGRIDtheRoleType), dlogloadcompletion);
    z.enableFilteringMode(false);

    z = new dhtmlXCombo("DIVchooseAPPL","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z;
    z.loadXML("GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=Application&select=" + escape(EGRIDtheappl), dlogloadcompletion);
    z.enableFilteringMode(false);
    z.disable(true);

    COMBOsystem = new dhtmlXCombo("DIVchooseSYSTEM","CHOOSE_"+ARRnm[i],200); i++;
    z=COMBOsystem;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z;

    z.loadXML("GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=System&select=" + escape(EGRIDthesystem), dlogloadcompletion);
    z.enableFilteringMode(false);
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z;

    var z2=new dhtmlXCombo("DIVchoosePLATFORM","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z2;
    COMBOplat = z2;
    z.attachChildCombo(z2,"GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=Platform&select=" + escape(EGRIDtheplatform));

    var z3=new dhtmlXCombo("DIVchooseENAME","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z3;
    z.attachChildCombo(z3,"GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=EntitlementName&select=" + escape(EGRIDtheEntN));
    COMBOename = z3;
	 
    z3=new dhtmlXCombo("DIVchooseEVALUE","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z3;
    z.attachChildCombo(z3,"GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=EntitlementValue&select=" + escape(EGRIDtheEntV));
    COMBOeval = z3;
	 
    z3=new dhtmlXCombo("DIVchooseAUTHOBJ","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z3;
    z.attachChildCombo(z3,"GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=AuthObjValue&select=" + escape(EGRIDauthobj));
    COMBOauthobjval = z3;
	 
    z3=new dhtmlXCombo("DIVchooseFLDSECN","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z3;
    z.attachChildCombo(z3,"GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=FieldSecName&select=" + escape(EGRIDfsn));
    COMBOfsn = z3;
	 
    z3=new dhtmlXCombo("DIVchooseFLDSECV","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z3;
    z.attachChildCombo(z3,"GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=FieldSecValue&select=" + escape(EGRIDfsv));
    COMBOfsv = z3;
	 
    z3=new dhtmlXCombo("DIVchooseL4N","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z3;
    z.attachChildCombo(z3,"GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=Level4SecName&select=" + escape(EGRIDlev4sn));
    COMBOlev4sn = z3;
	 
    z3=new dhtmlXCombo("DIVchooseL4V","CHOOSE_"+ARRnm[i],200); i++;
    GLOBhandlesToDhtmlxComboboxes[GLOBALentfieldlist[i-1]] = z3;
    z.attachChildCombo(z3,"GuidedEditor/SeloptionFactory.aspx?appscope="+EGRIDtheappl+"&qcol=Level4SecValue&select=" + escape(EGRIDlev4sv));
    COMBOlev4sv = z3;

    $("DIVcommentary").value = EGRIDcmt;

    //	 xyz32463 = new Ajax.Updater("DIVmultichooseBRole","ajaxtest.htm");
    /*
      xyz32463 = new Ajax.Updater("DIVmultichooseBRole","HNDLPOST_busrolelist.ashx", {
      parameters: { IDsubprocess: $F("ctl00_HIDDENidSubpr"),
      wserowid: EGRIDtheentitlementID,
      linkedbusroles: GLOBALlinkedBusRoles
      }
      } );
    */


    // PRE-SELECT IN THE LINKED-ROLE LIST
    //	 var ARRbroles = GLOBALlinkedBusRoles.split(" ");
    //	 ARRbroles.forEach(PreSelectBRoles);
}



function onShow_DLGmaintRoleAssigns(dialog)
{
    
    // These defaults are overridden below if SPEC mode
    EGRIDtherow = null;
    EGRIDtheentitlementID = "";
    GLOBALlinkedBusRoles = "";


    switch (GLOBALmodeOfDlgMaintRoleAssign) {
    case 'SPECIFY':
	$("TXTexplainmaintrolecontrols").update
	    (
	     "Specify the roles that should be linked to the currently selected entitlement.  Highlight roles that should be linked; unhighlight those that should not be linked.");
	EGRIDtherow = GridContextMenu.get_contextData();
	EGRIDtheentitlementID = (EGRIDtherow.getMember('c_id').get_text());
	GLOBALlinkedBusRoles = EGRIDtherow.getMember('BusRolesLinked').get_text().strip();
	break;
    case 'ADD':
	DLGmaintRoleAssign.set_title("Bulk Assignment Editor");
	$("TXTexplainmaintrolecontrols").update
	    (
	     "Highlight any roles that should be ADDED to the assigned-role list for all currently selected entitlements.");
	break;
    case 'DEL':
	DLGmaintRoleAssign.set_title("Bulk Assignment Editor");
	$("TXTexplainmaintrolecontrols").update
	    (
	     "Highlight any roles that should be REMOVED from the assigned-role list for all currently selected entitlements.");
	break;
    }
						

    xyz32463 = new Ajax.Updater
	("DIVmultichooseBRole2","HNDLPOST_busrolelist.ashx", 
	 {
	     parameters: {
		 IDsubprocess: $F("ctl00_HIDDENidSubpr"),
		 wserowid: EGRIDtheentitlementID,
		 linkedbusroles: GLOBALlinkedBusRoles,
		 mode: GLOBALmodeOfDlgMaintRoleAssign
	     }
	 } 
	 );
}




function PreSelectBRoles(f1,f2)
{
    var SELbroles = $("SELECTlinkedBRoles");
}



function dialogclose(dialog)
{
}

function dialogdrag(dialog)
{
}
function dialogdrop(dialog)
{
}

function dialogfocus(dialog)
{
}




 
function Text1_onclick() {

}

function Button1_onclick() {

}















function CloneCell(CLIOBJgridrow, CLIOBJbabyrow, strColname)
{
    var srccell  = CLIOBJgridrow.getMember(strColname);
    var curval = srccell.get_text();
    var colnum = srccell.get_column().get_columnNumber();
    CLIOBJbabyrow.setValue(colnum,curval, true);  // DO NOT PERSIST YET!!
}


function NewBlankRow(CLIOBJgrid, CLIOBJgridrow)
{
    var CLIOBJbabyrow = CLIOBJgrid.get_table().addEmptyRow(CLIOBJgridrow.get_index());
    CLIOBJgrid.edit(CLIOBJbabyrow);
}


function CloneRow(CLIOBJgrid, CLIOBJgridrow)
{
    var CLIOBJbabyrow = CLIOBJgrid.get_table().addEmptyRow(CLIOBJgridrow.get_index());

    // THIS WORKS:
    //CLIOBJbabyrow.setValue(2,'david');

    //CLIOBJbabyrow.setValue(0,"BABY3829", true);  

    // BUT ALL OF THIS FAILS:
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'BusRolesLinked');
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'c_u_StandardActivity');
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'c_u_RoleType');
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'c_u_System');
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'c_u_Platform');
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'c_u_EntitlementName');
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'c_u_EntitlementValue');
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'c_u_AuthObjValue');
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'c_u_FieldSecName');
    CloneCell(CLIOBJgridrow, CLIOBJbabyrow, 'c_u_FieldSecValue');

    // OK, now none of the above was done with persistence.
    // The server still knows nothing about this object.
    CLIOBJgrid.edit("BABY3829");
}



// For the time being, selecting a single row will do nothing
// more than generate the manifest string (via an ajax server call)
// and display that in a special place DIV on the webpage:
//    <DIV ID='PreviewManifestString'>
function Grid1_onItemSelect(sender, eventArgs)
{
    alert("FEW");
    var itemArray = Grid1.getSelectedItems();
    var length = itemArray.length;
    if (length == 1) {
	// Bug in the design of ComponentArts grid client-side: no way to JSONize an item (row).
	// The problem is that GridCell has a ptr to GridRow and therefore infinite recursion.
	// alert(JSON.stringify(itemArray[0].get_cells()));
	new Ajax.Updater('PreviewManifestString','PreviewManifestString.ashx', {
		parameters: { 
		    'IDwsent': itemArray[0].Id
			} } );
    }
}



function Grid1_onContextMenu______(sender, eventArgs)
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
	    // DO NOT ALLOW EDITING. 
	    GridContextMenuNOEDIT.showContextMenuAtEvent(eventArgs.get_event());
	    GridContextMenuNOEDIT.set_contextData(eventArgs.get_item());
	}else{
	    GridContextMenu.showContextMenuAtEvent(eventArgs.get_event());
	    GridContextMenu.set_contextData(eventArgs.get_item());
	}
    }else{
	// These statements are for forcing the selection to be JUST the item right-clicked on:
	Grid1.select(eventArgs.get_item());
	GridContextMenu.showContextMenuAtEvent(eventArgs.get_event());
	GridContextMenu.set_contextData(eventArgs.get_item());
    }

}





/* THIS TRIO OF METHODS WOULD ALLOW YOU TO REQUIRE EXTRA CONFIRMATION FOR 
   EDITING ACTIONS WHEN ABOUT TO ACTUALLY BE PERFORMED.
   Right now, I only do this for delete.
*/
function Grid1_onItemBeforeInsert(sender, eventArgs)
{
    return;
    if (document.getElementById('chkConfirmInsert').checked)
	if (!confirm("Insert record?"))
	    eventArgs.set_cancel(true);
}

function Grid1_onItemBeforeUpdate(sender, eventArgs)
{
    return;
    if (document.getElementById('chkConfirmUpdate').checked)
	if (!confirm("Update record?"))
	    eventArgs.set_cancel(true);
}

function Grid1_onItemBeforeDelete(sender, eventArgs)
{
    if (!confirm("Are you SURE you want to DELETE this record?"))
	eventArgs.set_cancel(true);
}
  






/* IMPORTANT:
   This would be called when initiating the actual update to the server, to find out
   what the user has chosen in the combobox on the client side.
   If this fails, combobox edits don't have any effect!
*/
function getCategory()
{
    return ComboBoxChooseSystem1.getSelectedItem().get_text();
}


function setCategory(DataItem)
{
    ComboBoxChooseSystem1.beginUpdate();

    for(var i = 0; i < ComboBoxChooseSystem1.get_itemCount(); i++)
	{
	    var item = ComboBoxChooseSystem1.getItem(i);

	    if(item.get_text() == DataItem.getMember('c_u_System').get_text())
		{
		    ComboBoxChooseSystem1.selectItem(item);
		    break;
		}
	}

    ComboBoxChooseSystem1.endUpdate();
}





/*
  function getValue()
  {
  var selectedDate = Picker1.getSelectedDate();
  var formattedDate = Picker1.formatDate(selectedDate, 'MM/dd/yyyy');
  return [selectedDate, formattedDate];
  }

  function setValue(DataItem)
  {
  var selectedDate = DataItem.getMember('LastOrderedOn').get_object();
  Picker1.setSelectedDate(selectedDate);
  }
*/




function RENDEReditstatus(gridrowitem)
{
    var v = (gridrowitem.getMember('c_u_EditStatus').get_value());
    // v is now a number, e.g. 32 means "NEW"
    if (v >= 32) {
	return "<IMG alt='NEW entitlement' src='media/ADDED.gif'/>";
    }
    var retval = "";
    if (v & 8) {
	retval += "<IMG alt='Change in list of business roles' src='media/EDITEDbusroles.gif'/>";
    }
    if (v & 4) {
	retval += "<IMG  alt='Change in at least one data field' src='media/EDITEDvector.gif'/>";
    }
    else if (v & 2) {
	retval += "<IMG alt='Change in only the commentary' src='media/EDITEDcommentary.gif'/>";
    }
    return retval;
}







function MENUACTnewblank()
{
    CloneRow(Grid1, GridContextMenu.get_contextData());
}


function MENUACTroleassign(param)
{
    // PARAM values are:
    //    SPECIFY (when one row selected)
    //    ADD (when multiple selected)
    //    DEL (when multiple selected)
    DLGmaintRoleAssign.set_modal(true);
    GLOBALmodeOfDlgMaintRoleAssign = param;
    GLOBALdlgeditSubmitActionType = "EDIT";
    GLOBarrayValuesPreEdit = new Hash();
    GLOBarrayValuesPostEdit = new Hash();
    DLGmaintRoleAssign.Show();
}
