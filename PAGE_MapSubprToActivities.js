$JQ(document).ready(JQdeclareDialogs);


function JQdeclareDialogs()
{
    $JQ("#JQDLGeditRow").dialog({
	    autoOpen:false,
		modal:true,
		closeOnEscape: false,
		minWidth:680, width:680,
		minHeight:580, height:580,
		buttons:{"OK":EVT_DLG_rowedit_OK, "Cancel":function(){
		    $('HIDDEN_CLOSET').appendChild ($('GridTable'));
		    $JQ(this).dialog("close");} 
	    }
	});
}




function EVTHNDL_GridDoneLoading()
{
    if ($('HIDDEN_canedit').value == "False") {
	GridMain.setProperty("ItemDraggingEnabled",false);
    }
}







// COPY ITEMS FROM A ROW OF THE MAIN GRID INTO THE EDIT POPUPBOX
function EVT_GridMain_ItemSelect(sender, args)
{
    // IF READONLY, IGNORE!
    if ($('HIDDEN_canedit').value == "False") {
	return;
    }


    var itemArray = GridMain.getSelectedItems();
    var length = itemArray.length;
    if (length == 1) {
	G = itemArray[0];
    }else{
	alert("Multi-selection in this grid is not useful.  Please select only one row.");
	return;
    }

    $('TXTactivityname').value = G.getMember("c_u_Text").Value;
    $('SELnodetype').value = G.getMember("c_u_NodeType").Value;
    $('CHKBOXisKey').checked = G.getMember("c_u_BOOLisKeyPoint").Value;


    // Pre-select the selected applications
    var ARRvals = G.getMember("c_u_ListIdsApps").Text.split(',');
    GRID_SelectApplications.unSelectAll();
    var i;
    for (i=0; i<ARRvals.length; i++) {
	GRID_SelectApplications.selectByKey(ARRvals[i],0,true);
    }
    GRID_SelectApplications.render();



    // Pre-select the selected busroles
    var ARRvals = G.getMember("c_u_ListIdsBusRoles").Text.split(',');
    GRID_SelectBusRoles.unSelectAll();
    var i;
    for (i=0; i<ARRvals.length; i++) {
	GRID_SelectBusRoles.selectByKey(ARRvals[i],0,true);
    }
    GRID_SelectBusRoles.render();


    //  POPUP

    $JQ("#JQDLGeditRow").dialog("open");


    $('WRAPPERtableDeployed').appendChild ($('GridTable'));
    
}




function EVT_DLG_rowedit_OK()
{
    // SEND THE INFO TO THE SERVER VIA AJAX
    // THEN, HAVE THE MAIN GRID DO A CALLBACK TO REFRESH.

    var url = "GuidedEditor/ajaxupdater.ashx";
    new Ajax.Request(url, {
	    method: 'post',
		parameters: {
		cmd: "UpdateSubprActivityRecord",
		    idsubpr: $F($('HIDDEN_idSubPr')),
		    idcuredit: GridMain.getSelectedKeys()[0],
		    text: $F($('TXTactivityname')),
		    nodetype: $F($('SELnodetype')),
		    boolIsKey: $F($('CHKBOXisKey')),
		    listSelApps: GRID_SelectApplications.getSelectedKeys().join(","),
		    listSelBRoles: GRID_SelectBusRoles.getSelectedKeys().join(",")
		    },
		onSuccess: AJAXRESPONSEJQDLG_success,
		onException: AJAXRESPONSEJQDLG_failure,
		onFailure: AJAXRESPONSEJQDLG_failure
		}
	);
}



function AJAXRESPONSEJQDLG_success(transport)
{
    // These next three lines fail on IE7, work on IE8:
//    $('HIDDEN_CLOSET').appendChild ($('GridTable'));
//    $JQ("#JQDLGeditRow").dialog("close");
//    GridMain.callback();

   // So this is the interim fix for IE7:
   window.location = "PAGE_MapSubprToActivities.aspx";
}





function AJAXRESPONSEJQDLG_failure(transport)
{
}
		    







/* RESEQUENCING VIA DRAGDROP */

function EVT_GridMain_Resequence(sender, eventArgs)
{
    // DRAGGEE is the item being dragged
    var idxDraggee = eventArgs.get_item().get_index();
    
    var seqDragFrom = (eventArgs.get_item().getMember("c_u_Sequence").Value);

    //alert(eventArgs.get_target());

    if ( ! eventArgs.get_target()) {
	return;
    }

    var idxDrop = eventArgs.get_target().get_index();

    //alert(idxDraggee);
    //alert(idxDrop);

    if (idxDraggee == idxDrop) {
	return;
    }


    var seqDrop = eventArgs.get_target().getMember("c_u_Sequence").Value;

    //alert(seqDrop);

    var seqNEW;

    if (idxDrop == 0) {
	seqNEW = seqDrop / 2.0;
    }else{
	if (seqDrop < seqDragFrom) {
	    // Moving an existing item UPWARDS (towards the top)
	    var seqOneAbove = GridMain.get_table().getRow(idxDrop-1).getMember("c_u_Sequence").Value;
	    seqNEW = (seqOneAbove + seqDrop) / 2.0;
	}
	else{
	    // Moving an existing item DOWNWARDS (towards the bottom)
	    var rowBelow = GridMain.get_table().getRow(idxDrop+1);
	    if (rowBelow) {
		var seqOneBelow = GridMain.get_table().getRow(idxDrop+1).getMember("c_u_Sequence").Value;
		seqNEW = (seqOneBelow + seqDrop) / 2.0;
	    }else{
		var seqLastOne = GridMain.get_table().getRow(idxDrop).getMember("c_u_Sequence").Value;
		seqNEW = seqLastOne + 100;
	    }
	}
    }
    

    //alert(eventArgs.get_item().getMember("c_id").Value);
    //return;

    var url = "GuidedEditor/ajaxupdater.ashx";
    new Ajax.Request(url, {
	    method: 'post',
		parameters: {
		    cmd: "RequenceSubprActivityRecord",
		    idcuredit: eventArgs.get_item().getMember("c_id").Value,
		    newseqnum: seqNEW
		    },
		onSuccess: function(req) { GridMain.callback(); },
		onFailure: AJAXRESPONSEJQDLG_failure,
		onException: AJAXRESPONSEJQDLG_failure
		}
	);
	
}
