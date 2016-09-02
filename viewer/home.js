function SearchForRole()
{
    var srchstring = $('TXTrolenamesrch').value;
    window.location.replace("LISTbusroles_byOwner.aspx?mode=search&srch=" 
			    + escape(srchstring));
}


function EVTonFocus_TXTrolenamesrch()
{
    $('BTNrolenamesrch').focus();
}
