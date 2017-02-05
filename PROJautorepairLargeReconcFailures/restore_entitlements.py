import csv
import sys
import pickle

dict_workspaces = pickle.load(open('workspaces.pkl', 'rb'))
dict_broles = pickle.load(open('broles.pkl', 'rb'))
dict_ents = pickle.load(open('ents.pkl', 'rb'))


with open('Result of IDM reconcile based on WS-5139.csv', 'rU') as csvfile:
    reader = csv.reader(csvfile)
    linenum = 0
    ignorenum = 0
    oknum = 0
    for row in reader:
        linenum += 1
        if row[0] != 'Role Name':
            rolename = row[0]
            ent = row[1]
            try:
                action = row[2]
            except:
                print linenum
                print row
                sys.exit(1)
            obj = row[3]
            if action == 'Remove' and obj == 'Entitlement':
                subpr = int(row[4])
                if not dict_broles.has_key(rolename):
                    print "On line %d - unknown role %s" % (linenum, rolename)
                elif not dict_ents.has_key(ent):
                    print "On line %d - unknown entsummary: %s" % (linenum, ent)
                else:
                    oknum +=1

print "Actual number we could actually restore: %d" % oknum
                    
