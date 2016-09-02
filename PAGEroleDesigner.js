var INTnumChangesOutstanding = 0;

function REACTgridDoneLoading()
{
    $('ctl00_ContentPlaceHolder1_PANELcond_goback').style.visibility = "visible";
    $('ctl00_ContentPlaceHolder1_PANELcond_changesExist').style.visibility = "hidden";
}

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


function RespondToCheckboxChangeNATIVE(rowid,eventargs)
{
    Grid1.beginUpdate();
    var griditem = eventargs.get_item();
    var gritem = griditem;
    var oldValue = 
	griditem.getMember('BOOLallocatedForThisRole').get_text();
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








function COMBOXchooseApp_OnLoad(sender)
{
    $('ctl00_ContentPlaceHolder1_PANELcond_changesExist').style.visibility = "hidden";
    alert("chooseapp onload");
}

function COMBOXchooseApp_OnInit(sender)
{
    //	 sender.selectItemByIndex(6);
}

function COMBOXchooseApp_OnChange()
{
    alert("About to call callback");
    Grid1.callback();
}


function RENDEReditstatus(gridrowitem)
{
    var v = (gridrowitem.getMember('TEassEditStatus').get_value());

    if (v == "N") {
	return "<IMG alt='NEW entitlement' src='media/ADDED.gif'/>";
    }
    if (v == "X") {
	return "<IMG alt='DELETED entitlement' src='media/DELETED.gif'/>";
    }
    return "";
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
