
function ProcessSelectedObj()
{
    var arrSel = GRID_authobjects.getSelectedItems();
    if (arrSel.length == 0) {
	$JQ.alerts.alert("Please select an object from the list first!");
	return;
    }
    if (arrSel.length > 1) {
	$JQ.alerts.alert("We do not yet support bulk addition of multiple objects.");
	return;
    }
    
    // Now we use AJAX to inform the server of the requested action.
    var objid = (arrSel[0].getMember("c_id").Value);

    new Ajax.Request
	(
	 "GuidedEditor/ajaxupdater.ashx",
	 {parameters:
	     {
		 cmd: "SAP_AddObjsTo1251",
		     IDauthobj: objid,
		     IDsaprole: $F('RoleID')
		     },
		 onSuccess: function(origrequest)
		 {
		     window.location = ($F('returnurl'));
		 },
		onException: DlogAddObjs_AJAXRESPONSEJQDLG_failure,
		onFailure: DlogAddObjs_AJAXRESPONSEJQDLG_failure
	 }
	 );
}

function DlogAddObjs_AJAXRESPONSEJQDLG_failure(transport)
{
    $JQ.alerts.alert("The action failed: " + transport.responseText);
}

