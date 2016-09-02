function OnCheckboxChange(sender, arg)
{
    var thenode = (arg.get_node());
    var nodeid = thenode.get_id();


    if (thenode.get_checked()) {
	//
	// USER HAS CHECKMARKED SOMETHING
	//
	var tokens = nodeid.split("/");
	if (tokens[0] == "EntSet") {
	    if (tokens[1] == "WS") {
		//Token #2 is the subprocess ID
		//User is turning on workspace so we must turn off active (ACT).
		var targetid = "EntSet/ACT/" + tokens[2];
		if (MEGATREE.findNodeById(targetid) != null) {
		    MEGATREE.findNodeById(targetid).set_checked(false);
		}
		UnsetCheckmarksAllArchiveEASets(tokens[2]);
	    }
	    else if (tokens[1] == "ACT") {
		//Token #2 is the subprocess ID
		//User is turning on workspace so we must turn off active (ACT).
		var targetid = "EntSet/WS/" + tokens[2];
		if (MEGATREE.findNodeById(targetid) != null) {
		    MEGATREE.findNodeById(targetid).set_checked(false);
		}
		UnsetCheckmarksAllArchiveEASets(tokens[2]);
	    }
	    else {
		if (tokens[1] == "ARCHIVE") {
		    // USER HAS CHECKMARKED AN ARCHIVE ENTASSET.
		    // Uncheck the ACT and WS ones.
		    var targetid = "EntSet/ACT/" + tokens[2];
		    if (MEGATREE.findNodeById(targetid) != null) {
			MEGATREE.findNodeById(targetid).set_checked(false);
		    }
		    targetid = "EntSet/WS/" + tokens[2];
		    if (MEGATREE.findNodeById(targetid) != null) {
			MEGATREE.findNodeById(targetid).set_checked(false);
		    }
		    UnsetCheckmarksAllArchiveEASets(tokens[2]);
		    thenode.set_checked(true);
		}
	    }
	}
	else if (tokens[0] == "SP") {
	    SetCheckmarksForChildrenBusRoles(thenode, true);
	    //	    thenode.expand();
	}
	else if (tokens[0] == "PR") {
	    SetCheckmarksForChildrenSubprocesses(thenode, true);
	    //	    thenode.expand();
	}
	else if (tokens[0] == "BR") {
	    thenode.get_parentNode().set_checked(true);
	}
    }else{
	//
	// USER HAS *REMOVED* A CHECKMARK
	//
	var tokens = nodeid.split("/");
	if (tokens[0] == "EntSet") {
	    // Don't let them change the choice of WS/EASet in this manner.
	    thenode.set_checked(true);
	}
	if (tokens[0] == "SP") {
	    SetCheckmarksForChildrenBusRoles(thenode, false);
	}
    }
}



function UnsetCheckmarksAllArchiveEASets(subprid)
{
    // Uncheck all its siblings
    var targetparentid = "EntSet/FOLDERarchive/"+subprid;
    var targetparent = MEGATREE.findNodeById(targetparentid);
    if (targetparent != null) {		    
	SetCheckmarksForChildren(targetparent, false);
    }
}




function SetCheckmarksForChildrenBusRoles_INEFFICIENT(thenode, boolNewStatus)
{
    var nodelist = thenode.get_nodes();
    for (var i=0; i < nodelist.get_length(); i++) {
	var childnode = nodelist.getNode(i);
	var tokens = childnode.get_id().split("/");
	if (tokens[0] != "EntSet") {
	    nodelist.getNode(i).set_checked(boolNewStatus);
	}
    }
}





function SetCheckmarksForChildrenSubprocesses(thenode, boolNewStatus)
{
    var nodelist = thenode.get_nodes();
    var thelen = nodelist.get_length();
    var childnode = "";

    for (var i=0; i < thelen; i++) {
	childnode = nodelist.getNode(i);
	childnode.set_checked(boolNewStatus);
	SetCheckmarksForChildrenBusRoles(childnode, boolNewStatus);
    }
}

function SetCheckmarksForChildrenBusRoles(thenode, boolNewStatus)
{
    //thenode.expand();
    var nodelist = thenode.get_nodes();
    var thelen = nodelist.get_length();
    var childnode = "";

    for (var i=0; i < thelen; i++) {
	childnode = nodelist.getNode(i);
	if (childnode.get_id().substr(0,6) != "EntSet") {
	    childnode.set_checked(boolNewStatus);
	}
    }
}



function SetCheckmarksForChildren(thenode, boolNewStatus)
{
    var nodelist = thenode.get_nodes();
    var thelen = nodelist.get_length();
    var childnode = "";

    for (var i=0; i < thelen; i++) {
	childnode = nodelist.getNode(i);
	childnode.set_checked(boolNewStatus);
    }
}



function EVTHNDLchkUseAllRoles()
{
    MEGATREE.checkAll();
}



/*
When a pending-population node is expanded, this function
is called TWICE in succession.
First it is called and the node will have 0 children.
Then it is called and the node has children coming from the webload.
*/
var GLOBALonBeforeNodeExpand_curID = "---";
function OnBeforeNodeExpand(thetree, args)
{
    if (321 == OnNodeExpand(thetree, args)) {
	args.set_cancel(true);
    }
}

// Returns special code "321" if it happened to do some autocheckmarking
function OnNodeExpand(thetree, args)
{
    var thenode = args.get_node();
    var theID = thenode.get_id();
    var retval = "";
    if (theID == GLOBALonBeforeNodeExpand_curID) {
	if (GLOBALonBeforeNodeExpand_curchildcount < 
	    thenode.get_nodes().get_length()) 
	    {
		// If we get here, a webload just occurred
		// We want to checkmark automatically ALL the
		// newly loaded children IF the parent's checkmark
		// is on.
		if (thenode.get_checked()) {
		    SetCheckmarksForChildren(thenode, true);
		    retval = 321;
		}
	    }
    }else{
	GLOBALonBeforeNodeExpand_curID = theID;
    }
    GLOBALonBeforeNodeExpand_curchildcount = 
	thenode.get_nodes().get_length();
    return retval;
}


function OnWebLoadComplete(thenode, args)
{
    
}
