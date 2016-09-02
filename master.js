window.onresize = resizeImage;
window.onload = resizeImage;

var statusheaderoptimalwidth = 600;

function resizeImage() {
    var winW, winH;
    if (self.innerWidth) {
        winW = self.innerWidth;
        winH = self.innerHeight;
    } else if (document.documentElement && document.documentElement.clientWidth) {
        winW = document.documentElement.clientWidth;
        winH = document.documentElement.clientHeight;
    } else if (document.body) {
        winW = document.body.clientWidth;
        winH = document.body.clientHeight;
    }

	 $('IDstatusheader').style.top = 5;
	 $('IDstatusheader').style.left = 
		  winW - statusheaderoptimalwidth - 5;
	 $('IDstatusheader').style.width = statusheaderoptimalwidth;
	 $('IDstatusheader').style.visibility = "visible";
}




// This is for use for any grid row item that honors
// the 8,4,2,1 semantics specified in the documentation
// in the RISE document's tab called "SAPTcodeASSIGN".
function RENDEReditstatus_canonical(gridrowitem)
{
    var v = (gridrowitem.getMember('c_u_EditStatus').get_value());
    // v is now a number, e.g. 32 means "NEW"
    if ((v & 6) == 6) {
	return "<IMG alt='New entitlement now being undone' src='media/ADDED.gif'/><IMG alt='New entitlement now being undone' src='media/DELETED.gif'/>";
    }
    if (v & 2) {
	return "<IMG alt='NEW entitlement' src='media/ADDED.gif'/>";
    }
    if (v & 4) {
	return "<IMG alt='DELETED entitlement' src='media/DELETED.gif'/>";
    }

    var retval = "";
    if (v & 8) {
	retval += "<IMG alt='Change occurred in at least one entitlement field' src='media/EDITEDvector.gif'/>";
    }
    if (v & 1) {
	retval += "<IMG alt='Change occurred in the commentary' src='media/EDITEDcommentary.gif'/>";
    }
    return retval;
}
