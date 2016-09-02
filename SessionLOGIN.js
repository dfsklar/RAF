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



function MENUACT_renamePROCESS()
{
    $JQ.alerts.prompt
	("Please enter the revised name for this PROCESS:",
	 GLOBcontextGridRow.getMember("NameOfProcess").Value,
	 "Process Rename",
	 function(newval) 
	 {
	     if(newval) {
		 GLOBcontextGridRow.setValue(GLOBcontextGridRow.getMember('NameOfProcess').get_column().get_columnNumber(), newval);
		 Grid1.callback();
	     }
	 });
}



function MENUACT_descrPROCESS()
{
    $JQ.alerts.prompt
	("Please enter the revised description for this PROCESS:",
	 GLOBcontextGridRow.getMember("DescrOfProcess").Value,
	 "Process Description",
	 function(newval) 
	 {
	     if(newval) {
		 GLOBcontextGridRow.setValue(GLOBcontextGridRow.getMember('DescrOfProcess').get_column().get_columnNumber(), newval);
		 Grid1.callback();
	     }
	 });
}


function MENUACT_renameSUBPROCESS()
{
    $JQ.alerts.prompt
	("Please enter the revised name for this SUBPROCESS:",
	 GLOBcontextGridRow.getMember("NameOfSubprocess").Value,
	 "SubProcess Rename",
	 function(newval) 
	 {
	     if(newval) {
		 GLOBcontextGridRow.setValue(GLOBcontextGridRow.getMember('NameOfSubprocess').get_column().get_columnNumber(), newval);
		 Grid1.callback();
	     }
	 });
}







// This was a test of drag/drop, not used in this webpage.
// Dragging not enabled so this is no longer called.
function Grid1_ondrop(sender, eventArgs)
{
    //    alert(eventArgs.get_item().get_index());  //WORKS!
    //    alert(eventArgs.get_target().get_index());  //WORKS!

    eventArgs.get_item().setValue(2, "XXXX");
    eventArgs.get_item().setValue(3, "XXXX");
    Grid1.sort(2,true);
}








function MENUACT_delete ()
{
    var gridrow = GLOBcontextGridRow;
    var nameOfSubpr =
	gridrow.getMember('NameOfProcess').Value + " / " + gridrow.getMember('NameOfSubprocess').Value;
    var idOfSubpr = gridrow.getMember('c_id').Value;

    $JQ.alerts.prompt
	("To confirm the deletion of '"+nameOfSubpr+"', please enter a reason and then hit OK:",
	 "",
	 "Subprocess Deletion Confirmation",
	 function(newval) 
	 {
	     if(newval) {
		 alert("This subprocess (" + nameOfSubpr + ") will be hidden from view, but it can be restored via the administrators.");
		 gridrow.setValue(gridrow.getMember('StatusOfSubprocess').get_column().get_columnNumber(), "Deleted");
		 gridrow.setValue(gridrow.getMember('NameOfSubprocess').get_column().get_columnNumber(), 
				  "DELETED_" + idOfSubpr + "_" + nameOfSubpr);
		 Grid1.callback();
	     }
	 });
}
