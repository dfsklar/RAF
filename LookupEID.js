function LookupEID(CTLIDeid,CTLtargetprefix,BOOLbequiet)
{
    var url = 
	"GuidedEditor/ajaxupdater.ashx?cmd=FindUserByEID&arg1=" + $F(CTLIDeid) + "&arg2=" + CTLtargetprefix;

    // This URL will return the surname then vertbar than firstname, if found.
    // Or it returns "NOTFOUND"
    
    if (BOOLbequiet) {

	new Ajax.Request(url, {
		method: 'get',
		    onSuccess: AJAXRESPONSEHANDLE_lookupeid_successsilent,
		    onException: AJAXRESPONSEHANDLE_lookupeid_silent,
		    onFailure: AJAXRESPONSEHANDLE_lookupeid_silent
		    }
	    );

    }else {
	new Ajax.Request(url, {
		method: 'get',
		    onSuccess: AJAXRESPONSEHANDLE_lookupeid_success,
		    onException: AJAXRESPONSEHANDLE_lookupeid_failure,
		    onFailure: AJAXRESPONSEHANDLE_lookupeid_failure
		    }
	    );
    }
}


function AJAXRESPONSEHANDLE_lookupeid_silent()
{}



// This is a pair of near-identical functions; they must be kept in sync.
function AJAXRESPONSEHANDLE_lookupeid_success(arg1,arg2,arg3) {
    var ARRresult = arg1.responseText.split("|");
    var emitctx = ARRresult[0];
    if (ARRresult[1] == "NOTFOUND") {
	alert("That EID was not found");
	$(emitctx+"last").value = "";
	$(emitctx+"first").value = "";
	return;
    }
    if (ARRresult[1].substr(0,5) == "USERI") {
	alert("That user is registered in the user database but without name details.  We invite you to add that information.");
	$(emitctx+"last").value = "";
	$(emitctx+"first").value = "";
	return;
    }
    $(emitctx+"last").value = ARRresult[1];
    $(emitctx+"first").value = ARRresult[2];
}
function AJAXRESPONSEHANDLE_lookupeid_successsilent(arg1,arg2,arg3) {
    var ARRresult = arg1.responseText.split("|");
    var emitctx = ARRresult[0];
    if (ARRresult[1] == "NOTFOUND") {
	//alert("That EID was not found");
	$(emitctx+"last").value = "";
	$(emitctx+"first").value = "";
	return;
    }
    if (ARRresult[1].substr(0,5) == "USERI") {
	//alert("That user is registered in the user database but without name details.  We invite you to add that information.");
	$(emitctx+"last").value = "";
	$(emitctx+"first").value = "";
	return;
    }
    $(emitctx+"last").value = ARRresult[1];
    $(emitctx+"first").value = ARRresult[2];
}



function AJAXRESPONSEHANDLE_lookupeid_failure(arg1,arg2,arg3) {
    alert("ERROR OCCURRED");
}

