import sys
import pickle

dict_busroles = {}   # KEY rolename, VALUE ID

with open('BusRole.txt', 'r') as csvfile:
    linenum = 0
    ignorenum = 0
    for line in csvfile:
        row = line.split('\t')
        linenum += 1
        try:
            c_id = row[0]
            if not (row[0] == 'c_id'):
                c_id = row[0]
                intcid = int(c_id)
                brole = row[1]
                if dict_busroles.has_key(brole):
                    print "MULTIPLE USES OF BROLE %s" % brole
                else:
                    dict_busroles[brole] = c_id
        except:
            ignorenum += 1

pickle.dump(dict_busroles, open('broles.pkl', 'wb'))
print 'Number of IGNORED lines: %d' % ignorenum
