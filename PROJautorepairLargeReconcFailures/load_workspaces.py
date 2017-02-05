import csv
import sys
import pickle

dict_workspaces = {} # key is subprocess, value is wsID

with open('Workspaces_EntAssignmentSet.csv', 'rb') as csvfile:
    reader = csv.reader(csvfile)
    linenum = 0
    ignorenum = 0
    for row in reader:
        linenum += 1
        try:
            c_id = row[0]
            if not (row[0] == 'c_id'):
                c_id = row[0]
                intcid = int(c_id)  # Just to make sure the field is an int
                subpr = row[4]
                intsubpr = int(subpr)
                if dict_workspaces.has_key(subpr):
                    print "MULTIPLE USES OF SUBPR %s" % subpr
                else:
                    dict_workspaces[subpr] = c_id
        except:
            ignorenum += 1

pickle.dump(dict_workspaces, open('workspaces.pkl', 'wb'))
print 'Number of IGNORED lines: %d' % ignorenum