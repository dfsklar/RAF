function AJAXsendCommentToServer()
{
    var applid = GLOBALcontextApplGridItem.getMember("c_id").Value;
    var url = "GuidedEditor/ajaxupdater.ashx";
    new Ajax.Request(url, {
	    method: 'post',
		parameters: {
		cmd: "SetCommentForBroleApplPair",
		    idbrole: $F($('IDrole')),
		    idcuredit: GLOBALcontextApplGridItem.getMember('FANid').Value,
		    text:    $F($('NOTESeditarea')),
		    idappl:  GLOBALcontextApplGridItem.getMember('c_id').Value
		    }
		,
		onSuccess: AJAXRESPONSEJQDLG_success,
		onException: AJAXRESPONSEJQDLG_failure,
		onFailure: AJAXRESPONSEJQDLG_failure
		}
	);
    
}

function AJAXRESPONSEJQDLG_success()
{
    Grid1.callback();
}

function AJAXRESPONSEJQDLG_failure()
{
    alert("Communication error with the server - Changes were likely NOT saved.");
}


function EVT_Grid_RowSelect(sender, args)
{
    var itemArray = sender.getSelectedItems();
    var length = itemArray.length;
    var G;
    if (length == 1) {
	G = itemArray[0];
    }else{
	alert("Multi-selection in this grid is not useful.  Please select only one row.");
	return;
    }

    GLOBALcontextApplGridItem = G;

    $('NOTESeditarea').value  = G.getMember("FANtext").Value;

}
