$JQ(document).ready(function(){

	$JQ("#JQDLGmsgs").dialog({
		autoOpen:false,
		    modal:true,
		    minWidth:220, width:700,
		    minHeight:220, height:500,
		    buttons:{"Close":function(){$JQ(this).dialog("close");} }
	    })});




var INTnumChangesOutstanding = 0;

function Submit()
{

    // This is all you need to announce to the server:
    Grid1.callback();

    // TRYING postback: well it did announce the proper thyings to the server
    // but the grid was then left unrefreshed.
    //  Grid1.postback();

    $('ctl00_ContentPlaceHolder1_PANELcond_goback').style.visibility = "visible";
    $('ctl00_ContentPlaceHolder1_PANELcond_changesExist').style.visibility = "hidden";
}


function REACTgridDoneLoading()
{
    $('ctl00_ContentPlaceHolder1_PANELcond_goback').style.visibility = "visible";
    $('ctl00_ContentPlaceHolder1_PANELcond_changesExist').style.visibility = "hidden";
}



function REACTgridDoneCallback()
{
    var msgs = Grid1.get_callbackParameter();
    $('IFRAMEmsgs').innerHTML = msgs.replace(/\n/g,"<BR>");
    if (msgs.indexOf("OPERATION ABORTED") >= 0) {
	$JQ("#JQDLGmsgs").dialog("option", "title", "ERROR(S) -- Read carefully");
    }else{
	$JQ("#JQDLGmsgs").dialog("option", "title", "Messages about the completed operation");
    }

    $JQ("#JQDLGmsgs").dialog("open");
}


function RENDERchgstatus(gridrowitem)
{
    var v = (gridrowitem.getMember('INTeditStatus').get_value());
    // v is now a number, e.g. 32 means "NEW"
    if (v > 0) {
	//return "<IMG alt='NEW entitlement' src='media/ADDED.gif'/>";
	return "<SPAN style='font-weight:bold;color:red'>!!!</SPAN>";
    }
    return "";
}



function RespondToCheckboxChangeNATIVE(rowid,eventargs)
{
    Grid1.beginUpdate();
    var griditem = eventargs.get_item();
    var gritem = griditem;
    var oldValue = 
	griditem.getMember('InUse').get_text();
    // We now have the OLD value before it was toggled.

    var newValue = 'false';
    if (oldValue == 'false') {
	newValue = 'true';
    }

    var colnum = gritem.getMember('INTeditStatus').get_column().get_columnNumber();
    var newtogglevalue =  (1 + (gritem.getMember('INTeditStatus').Value)) % 2;
    if (newtogglevalue == 1) {
	INTnumChangesOutstanding++;
    }else{
	INTnumChangesOutstanding--;
    }
    gritem.setValue(colnum,newtogglevalue,true);

    Grid1.endUpdate();

    if (INTnumChangesOutstanding > 0) {
	$('ctl00_ContentPlaceHolder1_PANELcond_goback').style.visibility = "hidden";
	$('ctl00_ContentPlaceHolder1_PANELcond_changesExist').style.visibility = "visible";
    }else{
	$('ctl00_ContentPlaceHolder1_PANELcond_goback').style.visibility = "visible";
	$('ctl00_ContentPlaceHolder1_PANELcond_changesExist').style.visibility = "hidden";
    }
		  
}
