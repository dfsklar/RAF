
// Expects there to be a global variable GLOBALstdgrideditContextMenuXml with the XML filename of the menu to show.
function Grid1_onContextMenu(sender, eventArgs)
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
