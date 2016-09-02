$JQ(document).ready(function(){

	$JQ("#JQDLGmsgs").dialog({
		autoOpen:false,
		    modal:true,
		    minWidth:220, width:700,
		    minHeight:220, height:500,
		    buttons:{"Close":function(){$JQ(this).dialog("close");} }
	    });
	// How to populate:
	//$('IFRAMEmsgs').innerHTML = "FJIEOWJINNERjfiwe<BR>";
	//for (i=0; i<200;i++) {
	//$('IFRAMEmsgs').innerHTML += "FJIEOWJINNER<BR>";
	//}


	$JQ("#JQDLGviewreadonly").dialog({
		autoOpen:false,
		    modal:true,
		    minWidth:700, width:700,
		    minHeight:500, height:500,
		    buttons:{"Close":function(){$JQ(this).dialog("close");} }
	    });

    });




var ARRfields =
[
 "Application",
 "StandardActivity",
 "RoleType",
 "System",
 "Platform",
 "EntitlementName",
 "EntitlementValue",
 "AuthObjValue",
 "FieldSecName",
 "FieldSecValue",
 "Level4SecName",
 "Level4SecValue",
 "Commentary"
 ];




function MENUACTviewreadonly()
{
    //    $('viewRO_TD_Application').innerHTML = GLOBcontextGridRow.getMember("c_u_Application").Value;
    ARRfields.each(function(item){
	    $('viewRO_TD_'+item).innerHTML = GLOBcontextGridRow.getMember("c_u_"+item).Value;
	}
	);

    new Ajax.Updater
	('viewRO_ManifestPrivString',
	 "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "ComputeManifestString",
			 arg1: GLOBcontextGridRow.getMember("c_id").Value
			 }});

    $JQ("#JQDLGviewreadonly").dialog("open");
}



function DLGCHGSTATcancel()
{
    DLGchgStatus.Close();
}
function DLGCHGSTATsubmit()
{
    // Send an ajax message to server with the new status codes
    new Ajax.Request
	("GuidedEditor/RecordEntitlementStatusMod.ashx",
	 {parameters:
	     {
		 entids: Grid1.GetSelectedKeys().toJSON(),
		     newstat: $('CHOOSEnewstatus').value,
		     oldstat: GLOBoldStatusToChangeFrom
		     },
		 onSuccess: function(origrequest)
		 {
		     var resp = origrequest.responseText;
		     $('IFRAMEmsgs').innerHTML = resp.replace(/\n/g,"<BR>");
		     $JQ("#JQDLGmsgs").dialog("option", "title", "Error messages and summary of activity");
		     $JQ("#JQDLGmsgs").dialog("open");
		     Grid1.callback();
		 },
		 onFailure: function(origrequest)
		 {
		     var resp = origrequest.responseText;
		     $('IFRAMEmsgs').innerHTML = resp;
		     $JQ("#JQDLGmsgs").dialog("option", "title", "MAJOR ERROR - READ CAREFULLY");
		     $JQ("#JQDLGmsgs").dialog("open");
		 },
		 onException: function(x) {alert("AJAX communication failure.");}
	 });

    DLGchgStatus.Close();
}




// THIS IS STILL IN USE AS OF JULY 7 2009
function LAUNCHdlogChangeStatus(CLIENTIDgridrow)
{
    GLOBcontextGridRow = Grid1.getItemFromClientId(CLIENTIDgridrow);


    if ("SAP" == (COMBOXchooseApp.getSelectedItem().Text)) {
	alert("Status of SAP entitlements are controlled by the SAP Designer, not by this grid.");
	return;
    }


    // If the row the user clicked on is already selected, do nothing
    // with the selection set and honor mult sels.
    // If the row the user clicked on is NOT already selected,
    // force it to be the sole selection.
    var boolThisItemIsSelected =
	WEBUI_UTIL_isItemSelected(Grid1,GLOBcontextGridRow);

    if ( ! boolThisItemIsSelected) {
	Grid1.select(GLOBcontextGridRow);
    }

    // Make sure everything in the selection block is at the very
    // same status setting.
    var curStatusSetting = GLOBcontextGridRow.getMember('c_u_Status').Value;
    GLOBoldStatusToChangeFrom = curStatusSetting;
    var ARRselitems = Grid1.getSelectedItems();
    for(var i = 0;i<ARRselitems.length;i++) {
	if (ARRselitems[i].getMember('c_u_Status').Value != curStatusSetting) {
	    alert("The set of currently selected items includes a variety of Status values.  To do bulk editing of status, make sure to select a set of rows with identical status settings.");
	    return;
	}
    }



    // These are just "suggested" new statuses and actually as of 9 July 2009,
    // these are not used anymore!
    switch (curStatusSetting) {
    case "P":
	GLOBnewStatusToChangeTo = "A (active/available)";
	break;
    case "A":
	GLOBnewStatusToChangeTo = "I (inactive)";
	break;
    case "I":
	GLOBnewStatusToChangeTo = "X (deleted)";
	break;
    case "X":
	GLOBnewStatusToChangeTo = "A (active/available)";
    }


    DLGchgStatus.set_modal(true);

    //alert($('FILLINnumrowsaffected'));  // Correct result: this is a SPAN element
    // But how do you then change the text of the span element?

    //alert($('FILLINnumrowsaffected').innerHTML);

    //alert($('FILLINnumrowsaffected').value);

    //$('FILLINnumrowsaffected').value = "FJIEOW";

    //alert($('FILLINnumrowsaffected').value);
    
    DLGchgStatus.Show();
}




function onShow_DLGchgStatus(dialog)
{
    EGRIDtherow = GLOBcontextGridRow;
    // This is the actual client-side complex-object row item , not just an index!

    $('FILLINnumrowsaffected').innerHTML = Grid1.getSelectedItems().length;

    //$('FILLINnewstatus').innerHTML = GLOBnewStatusToChangeTo;

    $('FILLINoldstatus').innerHTML = GLOBoldStatusToChangeFrom;
    

}



// -------------------------------




// If you want more logic in this function,
// borrow some code from SAPgridediting.js
function Grid1_onContextMenu(sender, eventArgs)
{
    var itemArray = Grid1.getSelectedItems();
    var length = itemArray.length;

    // GLOBAL
    GLOBcontextGridRow = eventArgs.get_item();

    // FORCE SELECTION OF THE ITEM INVOLVED
    Grid1.select(GLOBcontextGridRow);


    // DISABLE MENU ITEMS BASED ON CONTEXT
    mitem = (GridContextMenu.findItemById("mPBV_bulkassign"));
    mitem.set_visible(false);
    switch (GLOBcontextGridRow.getMember("c_u_Status").Value) 
	{
	case "P":
	    var mitem = (GridContextMenu.findItemById("mPBV_edit"));
	    mitem.set_visible(true);
	    mitem = (GridContextMenu.findItemById("mPBV_del"));
	    mitem.set_visible(true);
	    break;
	case "A":
	    var mitem = (GridContextMenu.findItemById("mPBV_edit"));
	    mitem.set_visible(false);
	    mitem = (GridContextMenu.findItemById("mPBV_del"));
	    mitem.set_visible(false);
	    mitem = (GridContextMenu.findItemById("mPBV_bulkassign"));
	    mitem.set_visible(true);
	    break;
	default:
	    break;
	}

    if ("SAP" == (COMBOXchooseApp.getSelectedItem().Text)) {
	GridContextMenu.findItemById("mPBV_clone").set_visible(false);
	GridContextMenu.findItemById("mPBV_edit").set_visible(false);
	GridContextMenu.findItemById("mPBV_del").set_visible(false);
    }

    GridContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    GridContextMenu.set_contextData(eventArgs.get_item());
}



function MENUACTclone()
{
    DLGeditRow.set_modal(true);
    DLGeditRow.set_title("Create NEW entitlement");
    GLOBALdlgeditSubmitActionType = "CLONE";
    GLOBALmodeOfDlgMaintRoleAssign = "n/a";
    DLGeditRow.Show();
}


function MENUACTedit()
{
    DLGeditRow.set_modal(true);
    DLGeditRow.set_title("Modify existing entitlement");
    GLOBALdlgeditSubmitActionType = "EDIT";
    GLOBALmodeOfDlgMaintRoleAssign = "n/a";
    DLGeditRow.Show();
}


function MENUACTusage_exportToCSV()
{
    var theid = GLOBcontextGridRow.getMember("c_id").Value;
    $('TheIframe').src = "export/RolesUsingEntitlement.ashx?entid=" + theid;
}



function MENUACTbulkassign()
{
    var theid = GLOBcontextGridRow.getMember("c_id").Value;
    window.location.href = "PAGE_Bents_bulkassign.aspx?entid=" + theid;
}





// Replacement version brings up the msgs dialog:
function MENUACTusage()
{
    var theid = GLOBcontextGridRow.getMember("c_id").Value;
    var theurl = "export/RolesUsingEntitlement.ashx";
    new Ajax.Request
	(theurl,
	 {parameters:
	     {
		 entid: theid
		     },
		 onSuccess: function(origrequest)
		 {
		     var resp = origrequest.responseText;
		     $('IFRAMEmsgs').innerHTML = resp.replace(/\n/g,"<BR>");
		     $JQ("#JQDLGmsgs").dialog("option", "title", "Usage list");
		     $JQ("#JQDLGmsgs").dialog("open");
		 },
		 onFailure: function(origrequest)
		 {
		     var resp = origrequest.responseText;
		     $('IFRAMEmsgs').innerHTML = resp;
		     $JQ("#JQDLGmsgs").dialog("option", "title", "FATAL ERROR - READ CAREFULLY");
		     $JQ("#JQDLGmsgs").dialog("open");
		 },
		 onException: function(x) {alert("AJAX communication failure.");}
	 });

}


function MENUACTdel()
{
    if (confirm('Are you sure?  Click OK to proceed with deletion, or click Cancel.')) {
	Grid1.deleteItem(GLOBcontextGridRow);
	Grid1.callback();
    }
}




function hovershow(launcheritem, x)
{
    var cumoffset = Element.cumulativeOffset(launcheritem);
    $('hoverpopup').style.top = cumoffset[1]+18;
    $('hoverpopup').style.left = cumoffset[0]+25;  //-$('hoverpopup').getWidth();
    $('hoverpopup').style.visibility = "Visible";

    var row = Grid1.getItemFromClientId(x);
    var text = (row.getMember("c_u_Commentary").Value);
    $('_hoverpopup').innerHTML = text;
}



function hoverhide()
{
    $('hoverpopup').style.visibility = "Hidden";
}
