import csv
import sys
import pickle

dict_workspaces = pickle.load(open('workspaces.pkl', 'rb'))
dict_broles = pickle.load(open('broles.pkl', 'rb'))
dict_ents = pickle.load(open('ents.pkl', 'rb'))
set_redundant = pickle.load(open('manifests_ambiguous.pkl', 'rb'))

set_unknown_roles = set()

set_unknown_manifests = set()
set_inuse_ambiguous = set()

with open('Result of IDM reconcile based on WS-5139.csv', 'rU') as csvfile:
    reader = csv.reader(csvfile)
    linenum = 0
    errcounts = {
        'unknown_role': 0,
        'unknown_manifest': 0,
        'ambiguous_manifest': 0
        }
    attemptnum = 0
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
                attemptnum += 1
                subpr = int(row[4])
                if not dict_broles.has_key(rolename):
                    errcounts['unknown_role'] += 1
                    set_unknown_roles.add(str(subpr) + ' >>>> ' + rolename)
                    print "On line %d - unknown role %s" % (linenum, rolename)
                elif not dict_ents.has_key(ent):
                    errcounts['unknown_manifest'] += 1
                    set_unknown_manifests.add(str(subpr) + ' >>>> ' + ent)
                    print "On line %d - unknown entsummary: %s" % (linenum, ent)
                elif ent in set_redundant:
                    set_inuse_ambiguous.add(ent)
                    errcounts['ambiguous_manifest'] += 1
                    print "On line %d - entsummary is ambiguous: %s" % (linenum, ent)
                else:
                    oknum +=1

print "Number of entitlement-assignemnts we need to restore: %d" % attemptnum
print "  > Number we CAN restore without ambiguity: %d" % oknum
print "  > Number we CANNOT restore (organized by reason for failure): %s" % str(errcounts)

print '. . . . .'
print "Number of detected ambiguous manifests: %d" % len(set_redundant)
print "Number of actually-in-use ambiguous manifests: %d" % len(set_inuse_ambiguous)
with open('LISTinuseAmbiguousManifests.txt', 'w') as myfile:
    for x in set_inuse_ambiguous:
        myfile.write('\n' + x + '\n')
        for y in dict_ents[x]:
            y[16] = ''
            myfile.write('    ' + str(y) + '\n')

print '. . . . .'
print "Number of actually-in-use unknown manifests: %d" % len(set_unknown_manifests)
for x in sorted(set_unknown_manifests):
    print '     %s' % x

print '. . . . .'
print "Number of actually-in-use unknown business roles: %d" % len(set_unknown_roles)
for x in sorted(set_unknown_roles):
    print '     %s' % x
