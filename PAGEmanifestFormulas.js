$JQ(document).ready(function(){
	$JQ("#JQDLGeditFormula").dialog({
		autoOpen:false,
		    closeOnEscape: false,
		    modal:true,
		    minWidth:710, width:710,
		    minHeight:350, height:350,
		    buttons:{"OK":JQDLGeditFormula_OK, "Cancel":function(){$JQ(this).dialog("close");} }
	    });
    });

function JQDLGeditFormula_OK()
{
    var newval = $('TXTEDformula').value;
    GLOBcontextGridRow.setValue(GLOBcontextGridRow.getMember('c_u_Formula').get_column().get_columnNumber(), newval);
    Grid1.callback();
    $JQ("#JQDLGeditFormula").dialog("close");
}


function TestFormula()
{
    new Ajax.Updater
	('TXTformulaTest',
	 "GuidedEditor/ajaxupdater.ashx",
	     {parameters:
		 {
		     cmd: "ComputeSampleManifestStringsForApp",
			 arg1: GLOBcontextGridRow.getMember("c_id").Value,
			 arg2: $('TXTEDformula').value
			 }});
}


function MENUACT_renameAPP()
{
    var theid = GLOBcontextGridRow.getMember("c_id").Value;
    $JQ.alerts.prompt
	("Enter the new name for this application:",
	 GLOBcontextGridRow.getMember("c_u_Name").Value,
	 "Application Rename",
	 function(newval) 
	 {
	     if(newval) {
		 GLOBcontextGridRow.setValue(GLOBcontextGridRow.getMember('c_u_Name').get_column().get_columnNumber(), newval);
		 Grid1.callback();
	     }
	 }
	 );
}





function MENUACT_editFORMULA()
{
    var theid = GLOBcontextGridRow.getMember("c_id").Value;
    $('TXTappname').innerHTML = GLOBcontextGridRow.getMember("c_u_Name").Value;
    $('TXTEDformula').value =   GLOBcontextGridRow.getMember("c_u_Formula").Value;
    $('TXTformulaTest').innerHTML = "";
    TestFormula();
    $JQ("#JQDLGeditFormula").dialog("open");
}


function Grid1_onContextMenu(sender, eventArgs)
{
    var itemArray = Grid1.getSelectedItems();
    var length = itemArray.length;

    // GLOBAL
    GLOBcontextGridRow = eventArgs.get_item();

    // FORCE SELECTION OF THE ITEM INVOLVED
    Grid1.select(GLOBcontextGridRow);

    GridContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    GridContextMenu.set_contextData(eventArgs.get_item());
}




function RegNewApp()
{
    $JQ.alerts.prompt
	("Enter the name of the new application:",
	 "",
	 "Application Registration",
	 function(newval) 
	 {
	     if(newval) {
		 window.location.href = "PAGEmanifestFormulas.aspx?add=" + escape(newval);
	     }
	 });
}



function editGrid(rowId)
{
    Grid1.edit(Grid1.getItemFromClientId(rowId));
}
function editRow()
{
    Grid1.editComplete();
}
function Grid1_onCallbackError(sender, eventArgs)
{
    alert(eventArgs.get_errorMessage());
}
