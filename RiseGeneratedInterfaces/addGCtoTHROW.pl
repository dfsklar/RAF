# STDIN: contents of a .cs file
# STDOUT: modified contents

while (<STDIN>) {
    chomp;
    s/\r//;
    $regularline = 1;

    if (/(throw new Exception\(.*\);)/) {
	$linestart = $`;
	$matchfrag = $1;
	if ( ! ($linestart =~ /DBClose/)) {
	    $regularline = '';
	}
	if ( ! $regularline) {
	    print $linestart;
	    print " { cmd.Dispose(); DBClose(); " . $matchfrag . " } \n";
	}
    }
    elsif (/OdbcDataReader dri = cmd.ExecuteReader/) {
	if ( ! /DBClose/) {
	    $regularline = '';
	}
	if ( ! $regularline) {
	    print "OdbcDataReader dri=null; try{dri=cmd.ExecuteReader();}catch(Exception edri){cmd.Dispose();DBClose();throw edri;}\n";
	}
    }
    elsif (/dri.Read/) {
	if ( ! /DBClose/) {
	    $regularline = '';
	}
	if ( ! $regularline) {
	    print "try{dri.Read();} catch(Exception edri){cmd.Dispose();DBClose();throw edri;}\n";
	}
    }



    if ($regularline) {
	print $_ . "\n";
    }
}
