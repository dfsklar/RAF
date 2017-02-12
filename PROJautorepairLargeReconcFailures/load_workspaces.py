import sys
import pickle

dict_workspaces = {} # key is subprocess, value is wsID of the ACTIVE workspace for this subprocess

with open('Workspaces.txt', 'r') as csvfile:
    linenum = 0
    ignorenum = 0
    for line in csvfile:
        linenum += 1
        row = line.split('\t')
        try:
            c_id = row[0]
            if (row[0] != 'c_id') and (row[1] == 'ACTIVE'):
                c_id = row[0]
                notused_intcid_notused = int(c_id)  # Just to make sure the field is an int, we want the exception to occur if not valid int
                subpr = row[4]
                intsubpr = int(subpr)
                if dict_workspaces.has_key(subpr):
                    print "MULTIPLE USES OF SUBPR %s" % subpr
                else:
                    dict_workspaces[subpr] = c_id
        except:
          print "Ignoring line: %s" % line
          ignorenum += 1

pickle.dump(dict_workspaces, open('workspaces.pkl', 'wb'))
print 'Number of IGNORED lines: %d' % ignorenum
