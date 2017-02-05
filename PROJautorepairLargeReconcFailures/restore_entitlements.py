import csv
import sys
import pickle

dict_workspaces = pickle.load(open('workspaces.pkl', 'rb'))
dict_broles = pickle.load(open('broles.pkl', 'rb'))
dict_ents = pickle.load(open('ents.pkl', 'rb'))
set_redundant = pickle.load(open('manifests_ambiguous.pkl', 'rb'))

set_inuse_ambiguous = set()

with open('Result of IDM reconcile based on WS-5139.csv', 'rU') as csvfile:
    reader = csv.reader(csvfile)
    linenum = 0
    errcounts = {
        'unknown_role': 0,
        'unknown_manifest': 0,
        'ambiguous_manifest': 0
        }
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
                    errcounts['unknown_role'] += 1
                    print "On line %d - unknown role %s" % (linenum, rolename)
                elif not dict_ents.has_key(ent):
                    errcounts['unknown_manifest'] += 1
                    print "On line %d - unknown entsummary: %s" % (linenum, ent)
                elif ent in set_redundant:
                    set_inuse_ambiguous.add(ent)
                    errcounts['ambiguous_manifest'] += 1
                    print "On line %d - entsummary is ambiguous: %s" % (linenum, ent)
                else:
                    oknum +=1

print "Number of entitlements we can restore without ambiguity: %d" % oknum
print "Number we cannot restore with full certainty: " + str(errcounts)

print '. . . . .'
print "Number of detected ambiguous manifests: %d" % len(set_redundant)
print "Number of actually-in-use ambiguous manifests: %d" % len(set_inuse_ambiguous)
