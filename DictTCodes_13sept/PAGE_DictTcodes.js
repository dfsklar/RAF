$JQ(document).ready(JQdeclareDialogs);


function JQdeclareDialogs()
{
    $JQ("#JQDLGeditOrgAxis").dialog({
	    autoOpen:false,
		closeOnEscape: false,
		modal:true,
		minWidth:500, width:500,
		minHeight:190, height:190,
		buttons:{"OK":JQDLGeditOrgAxis_OK, "Cancel":function(){$JQ(this).dialog("close");} }
	    });
}



function JQDLGeditOrgAxis_OK()
{
    GLOBALdialogToClose = $JQ("#JQDLGeditOrgAxis");
    GLOBALgridToRefresh = GRID_orgcat;
    var url = "GuidedEditor/ajaxupdater.ashx";
    var curID;
    if (GLOBALmenuContextOrgAxis) {
	curID = GLOBALmenuContextOrgAxis.getMember('c_id').Value;
    }else{
	curID = -342;
    }
    new Ajax.Request(url, {
	    method: 'post',
		parameters: {
		cmd: "JQDLGeditOrgAxis",
		    arg1: curID,
		    arg2: $F($('JQDLGeditOrgAxis_sapname')),
		    arg3: $F($('JQDLGeditOrgAxis_descr'))
		    },
		onSuccess: AJAXRESPONSEJQDLG_success
		}
	);
}

function AJAXRESPONSEJQDLG_success(transport)
{
    GLOBALdialogToClose.dialog("close");
    GLOBALgridToRefresh.callback();
}




function MENUACT_orgaxis_newfromscratch()
{
    GLOBALmenuContextOrgAxis = null;

    $('JQDLGeditOrgAxis_sapname').value = '';
    $('JQDLGeditOrgAxis_descr').value = '';
    $('JQDLGeditOrgAxis_legalvals').value = 'EU,NA,NA+EU';

    $JQ("#JQDLGeditOrgAxis").dialog("open");
}


function MENUACT_orgaxis_edit()
{
    var itemArray = GRID_orgcat.getSelectedItems();
    GLOBALmenuContextOrgAxis = itemArray[0];

    $('JQDLGeditOrgAxis_sapname').value = GLOBALmenuContextOrgAxis.getMember('c_u_SAP_Name').Value;
    $('JQDLGeditOrgAxis_descr').value = GLOBALmenuContextOrgAxis.getMember('c_u_English_Name').Value;

    // Obtain the legal list of org values from the client-side API of the orgval listbox
    var i = 0;
    while (true) {
	var griditem = GRID_orgvalue.get_table().getRow(i);
	alert(griditem);
	break;
	i++;
	if (griditem) {
	    alert(griditem.getMember("c_u_Name").Text);
	    break;
	}
    }

    $('JQDLGeditOrgAxis_legalvals').value = 'EU,NA,NA+EU';

    $JQ("#JQDLGeditOrgAxis").dialog("open");
}



function GridOrgCat_onContextMenu(sender, eventArgs)
{
    // These statements are for forcing the selection to be JUST the item right-clicked on:
    GRID_orgcat.select(eventArgs.get_item());
    GridOrgAxisContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    GridOrgAxisContextMenu.set_contextData(eventArgs.get_item());
}


function GridOrgCat_onItemSelect(sender, eventArgs)
{
    var itemArray = GRID_orgcat.getSelectedItems();
    var length = itemArray.length;
    if (length == 1) {
	// Send the ID of the orgaxis as the parameter
	CALLBACK_GRID_orgvalue.Callback(itemArray[0].getMember('c_id').Value);
    }
}



function BTNaddTcodeRow_Click()
{
    var newtcode = prompt("Enter the TCode ID to be registered","");
    if (newtcode) {
	new Ajax.Request
	    (
	     "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "RegisterNewTcode",
			 arg1: newtcode
			 },
		     onSuccess: function(origrequest)
		     {
			 Grid1.callback();
		     },
		     onException: AJAXRESPONSEJQDLG_exception,
		     onFailure: AJAXRESPONSEJQDLG_failure
		     }
	     );
    }
}






function BTNaddOrgCategory_Click()
{
    var newtcode = prompt("Enter the name of the SAP org-category to be registered","");
    if (newtcode) {
	new Ajax.Request
	    (
	     "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "RegisterNewSAPOrgCat",
			 arg1: newtcode
			 },
		     onSuccess: function(origrequest)
		     {
			 Grid1.callback();
		     },
		     onException: AJAXRESPONSEJQDLG_exception,
		     onFailure: AJAXRESPONSEJQDLG_failure
		     }
	     );
    }
}




function BTNaddOrgValue_Click()
{
    var newtcode = prompt("Enter the name of the SAP org value to be registered","");
    if (newtcode) {
	new Ajax.Request
	    (
	     "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "RegisterNewSAPOrgValue",
			 arg1: newtcode
			 },
		     onSuccess: function(origrequest)
		     {
			 Grid1.callback();
		     },
		     onException: AJAXRESPONSEJQDLG_exception,
		     onFailure: AJAXRESPONSEJQDLG_failure
	     }
	     );
    }
}





function AJAXRESPONSEJQDLG_failure(transport)
{
    alert("ERROR/Warning: " + transport.responseText);
    AJAXRESPONSEJQDLG_close();
}

function AJAXRESPONSEJQDLG_exception(requestor, excobj)
{
    alert("EXCEPTION OCCURRED. Note that your intended action probably did not take effect.");
    AJAXRESPONSEJQDLG_close();
}
