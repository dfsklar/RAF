$JQ(document).ready(JQdeclareDialogs);


function JQdeclareDialogs()
{
    $JQ("#JQDLGnamePlusDescr").dialog({
				autoOpen:false,
					 closeOnEscape: false,
					 modal:true,
					 minWidth:500, width:500,
					 minHeight:150, height:150,
					 buttons:{"OK":JQDLGnamePlusDescr_OK, "Cancel":function(){$JQ(this).dialog("close");} }
		  });
}


// targettype:  "AuthObj Class" or "TCode" or "Auth Object"
function LaunchNameDescrDlog_newfromscratch(targettype, assocgrid)
{
	 GLOBAL_context_NameDescrDlog_targettype = targettype;
	 GLOBAL_context_NameDescrDlog_assocgrid = assocgrid;

    GLOBAL_context_NameDescrDlog_objid = null;

    $('JQDLGnamePlusDescr_name').value = '';
    $('JQDLGnamePlusDescr_descr').value = '';

 	 if (targettype != "Auth Object") {
 		  $('JQDLGnamePlusDescr_sectionrow_class').style.visibility = 'hidden';
 	 }else{
 		  $('JQDLGnamePlusDescr_sectionrow_class').style.visibility = 'visible';
 	 }

	 $JQ("#JQDLGnamePlusDescr").dialog('option','title', 'ADD NEW ' + targettype);

    $JQ("#JQDLGnamePlusDescr").dialog("open");
}
function LaunchNameDescrDlog_editexisting(targettype, assocgrid, clientid)
{
	 alert("NYI");

	 GLOBAL_context_NameDescrDlog_targettype = targettype;
	 GLOBAL_context_NameDescrDlog_assocgrid = assocgrid;

    GLOBAL_context_NameDescrDlog_objid = null;

    $('JQDLGnamePlusDescr_name').value = '';
    $('JQDLGnamePlusDescr_descr').value = '';

	 $JQ("#JQDLGnamePlusDescr").dialog('option','title', 'ADD NEW ' + targettype);

    $JQ("#JQDLGnamePlusDescr").dialog("open");
}



function JQDLGnamePlusDescr_OK()
{
    GLOBALdialogToClose = $JQ("#JQDLGnamePlusDescr");

    GLOBALgridToRefresh = GLOBAL_context_NameDescrDlog_assocgrid;

    var url = "GuidedEditor/ajaxupdater.ashx";

    new Ajax.Request(url, {
				method: 'post',
					 parameters: {
					 cmd: "JQDLGnamePlusDescr",
						  targetid: GLOBAL_context_NameDescrDlog_objid,
						  targettype: GLOBAL_context_NameDescrDlog_targettype,
						  authclassid: GLOBALselectedAuthClassUUID,
						  name: $F($('JQDLGnamePlusDescr_name')),
						  descr: $F($('JQDLGnamePlusDescr_descr'))
						  },
					 onSuccess: AJAXRESPONSEJQDLG_success,
					 onException: AJAXRESPONSEJQDLG_failure,
					 onFailure: AJAXRESPONSEJQDLG_failure
					 });
}


function AJAXRESPONSEJQDLG_failure(transport)
{
    alert("ERROR/Warning: " + transport.responseText);
    GLOBALdialogToClose.dialog("close");
}

function AJAXRESPONSEJQDLG_success(transport)
{
    GLOBALdialogToClose.dialog("close");
    GLOBALgridToRefresh.callback();
}







function MENUACT_editDESCR()
{
    var itemArray = Grid1.getSelectedItems();
    var length = itemArray.length;

    // GLOBAL
    GLOBcontextGridRow = itemArray[0];

    $JQ.alerts.prompt
	("Please enter the revised description",
	 GLOBcontextGridRow.getMember("c_u_Description").Value,
	 "Description Editor",
	 function(newval) 
	 {
	     if(newval) {
		 GLOBcontextGridRow.setValue(GLOBcontextGridRow.getMember('c_u_Description').get_column().get_columnNumber(), newval);
		 Grid1.callback();
	     }
	 });
    


}



function MENUACT_orgaxis_edit()
{
    var itemArray = GRID_orgcat.getSelectedItems();
    GLOBALmenuContextOrgAxis = itemArray[0];

    $('JQDLGnamePlusDescr_sapname').value = GLOBALmenuContextOrgAxis.getMember('c_u_SAP_Name').Value;
    $('JQDLGnamePlusDescr_descr').value = GLOBALmenuContextOrgAxis.getMember('c_u_English_Name').Value;
    $('JQDLGnamePlusDescr_legalvals').value = GLOBALmenuContextOrgAxis.getMember('c_u_LegalValues').Value;

    $JQ("#JQDLGnamePlusDescr").dialog("open");
}



function MENUACT_orgaxis_delete()
{
    var itemArray = GRID_orgcat.getSelectedItems();
    GLOBALmenuContextOrgAxis = itemArray[0];

    if (confirm('Are you sure you want to DELETE this permanently?  Click OK to proceed with deletion, or click Cancel.')) {
	var url = "GuidedEditor/ajaxupdater.ashx";
	var curID;
	GLOBALdialogToClose = $JQ("#JQDLGnamePlusDescr");
	GLOBALgridToRefresh = GRID_orgcat;
	if (GLOBALmenuContextOrgAxis) {
	    curID = GLOBALmenuContextOrgAxis.getMember('c_id').Value;
	}else{
	    alert("Deletion aborted - no selected row to delete!");
	    return;
	}
	new Ajax.Request(url, {
		method: 'post',
		    parameters: {
		    cmd: "JQDLGdeleteOrgAxis",
			arg1: curID
			},
		    onSuccess: AJAXRESPONSEJQDLG_success
		    }
	    );
    }
}



function GridOrgCat_onContextMenu(sender, eventArgs)
{
    // These statements are for forcing the selection to be JUST the item right-clicked on:
    GRID_orgcat.select(eventArgs.get_item());
    GridOrgAxisContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    GridOrgAxisContextMenu.set_contextData(eventArgs.get_item());
}
function GridTCodes_onContextMenu(sender, eventArgs)
{
    // These statements are for forcing the selection to be JUST the item right-clicked on:
    Grid1.select(eventArgs.get_item());
    GridTCodesContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    GridTCodesContextMenu.set_contextData(eventArgs.get_item());
}
function GridAuthClasses_onContextMenu(sender, eventArgs)
{
    // These statements are for forcing the selection to be JUST the item right-clicked on:
    GRID_authobjclasses.select(eventArgs.get_item());
    GridAuthClassContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    GridAuthClassContextMenu.set_contextData(eventArgs.get_item());
}
function GridAuthObjs_onContextMenu(sender, eventArgs)
{
    // These statements are for forcing the selection to be JUST the item right-clicked on:
    GRID_authobjects.select(eventArgs.get_item());
    GridAuthObjContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    GridAuthObjContextMenu.set_contextData(eventArgs.get_item());
}




function MaintainListRelatedFields()
{
	 var ARRselitems = GRID_authobjects.getSelectedItems();
	 window.location = "DLOGmaintainListOfRelatedFields.aspx?contextobj=" + ARRselitems[0].getMember('c_id').Value;
}

function RegisterNewAuthObj_fromClassList()
{
	 var ARRselitems = GRID_authobjclasses.getSelectedItems();
	 GLOBALselectedAuthClassUUID = ARRselitems[0].getMember('c_id').Value;
	 GLOBALselectedAuthClassName = ARRselitems[0].getMember('c_u_Name').Value;
	 $('JQDLGnamePlusDescr_class').value = GLOBALselectedAuthClassName;
	 LaunchNameDescrDlog_newfromscratch("Auth Object", GRID_authobjects);
}




function GridOrgCat_onItemSelect(sender, eventArgs)
{
    var itemArray = GRID_orgcat.getSelectedItems();
    var length = itemArray.length;
    if (length == 1) {
	// Send the ID of the orgaxis as the parameter
	// CALLBACK_GRID_orgvalue.Callback(itemArray[0].getMember('c_id').Value);
    }
}
function GridTCodes_onItemSelect(sender, eventArgs)
{
    var itemArray = Grid1.getSelectedItems();
    var length = itemArray.length;
    if (length == 1) {
	// Send the ID of the orgaxis as the parameter
	// CALLBACK_GRID_orgvalue.Callback(itemArray[0].getMember('c_id').Value);
    }
}



function BTNaddTcodeRow_Click()
{
    $JQ.alerts.prompt
	("Enter the TCode ID to be registered",
	 "",
	 "Register new TCode",
	 function(newTC)
	 {
	     if (newTC) {
		 GLOBnewTC = newTC;
		 $JQ.alerts.prompt
		     ("Enter a Description",
		      "",
		      "Register new TCode",
		      function(newDESCR)
		      {
			  if (newDESCR) {
			      new Ajax.Request(
					       "GuidedEditor/ajaxupdater.ashx",
					       {parameters:
						   {
						       cmd: "RegisterNewTcode",
							   arg1: GLOBnewTC,
							   arg2: newDESCR
							   },
						       onSuccess: function(origrequest)
						       {
							   Grid1.callback();
						       },
						       onException: AJAXRESPONSEJQDLG_exception,
						       onFailure: AJAXRESPONSEJQDLG_failure
						       })}})}});
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





function hovershow(launcheritem, x)
{
    var cumoffset = Element.cumulativeOffset(launcheritem);
    $('hoverpopup').style.top = cumoffset[1]+0;
    $('hoverpopup').style.left = cumoffset[0]+25;  //-$('hoverpopup').getWidth();
    $('hoverpopup').style.visibility = "Visible";

	 if (x != "-1") {
		  var row = GRID_authobjects.getItemFromClientId(x);
		  var text = row.getMember('c_id').Value;
		  //"The plan is this: that the list of legal fields for this particular auth object will appear here...";

		  new Ajax.Updater("_hoverpopup", 
		     "GuidedEditor/ajaxupdater.ashx",
		     {
			 parameters: {
			     cmd: "EmitAuthFieldListForGivenAuthObj",
				 arg1: row.getMember('c_id').Value
				 }
		     }
		     );
	 }
	 else
		  {
				$('_hoverpopup').innerHTML = "<i>Click to see the list of related fields...</i>";
		  }
}



function hoverhide()
{
    $('hoverpopup').style.visibility = "Hidden";
}



function AnnounceNYI()
{
    alert("Not Yet Implemented");
}
