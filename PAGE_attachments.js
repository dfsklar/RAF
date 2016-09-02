function Grid_onContextMenu(sender,eventArgs)
{
    // These statements are for forcing the selection to be JUST the item right-clicked on:
    GRIDlistAttachments.select(eventArgs.get_item());
    ContextMenu.showContextMenuAtEvent(eventArgs.get_event());
    ContextMenu.set_contextData(eventArgs.get_item());
    GLOBattachmentid = eventArgs.get_item().getMember("c_id").Value;
    //    alert(GLOBattachmentid);
}

function Grid_onItemSelect()
{
}

function MENUACT_download()
{
    //    alert($('TheIframe'));
    $('TheIframe').src = "export/Attachment.ashx?id=" + GLOBattachmentid;
}

function MENUACT_delete()
{
    alert("Not yet implemented");
}
