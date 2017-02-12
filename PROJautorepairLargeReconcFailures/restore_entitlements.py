import sys
import pickle

dict_workspaces = pickle.load(open('workspaces.pkl', 'rb'))  # key is subprocess, value is wsID
dict_broles = pickle.load(open('broles.pkl', 'rb'))   # key is name of busrole, value is broleID
dict_ents = pickle.load(open('ents.pkl', 'rb'))
set_redundant = pickle.load(open('manifests_ambiguous.pkl', 'rb'))

set_unknown_roles = set()

set_unknown_manifests = set()
set_inuse_ambiguous = set()


def sql_insert(subpr, rolename, ent):
  idWORKSPACE = int(dict_workspaces[str(subpr)])
  idBROLE = int(dict_broles[rolename])
  idENT = int(dict_ents[ent][-1][0])
  print "INSERT INTO \"t_RBSR_AUFW_u_EntAssignment\"(\"c_r_EntAssignmentSet\",\"c_r_BusRole\",\"c_r_Entitlement\",\"c_u_Status\") " + ("VALUES(%d, %d, %d, 'A');" % (idWORKSPACE, idBROLE, idENT));
  if False:
    print subpr
    print idWORKSPACE
    print rolename
    print idBROLE
    print 
    print ent



with open('idm_reconcile_result_ws5139.txt', 'rU') as csvfile:
    linenum = 0
    errcounts = {
        'unknown_role': 0,
        'unknown_manifest': 0,
        'ambiguous_manifest': 0
        }
    attemptnum = 0
    oknum = 0
    for line in csvfile:
        row = line.rstrip().split('\t')
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
                    print >> sys.stderr, "On line %d - unknown role %s" % (linenum, rolename)
                elif not dict_ents.has_key(ent):
                    errcounts['unknown_manifest'] += 1
                    set_unknown_manifests.add(str(subpr) + ' >>>> ' + ent)
                    print >> sys.stderr, "On line %d - unknown entsummary: %s" % (linenum, ent)
                elif ent in set_redundant:
                    sql_insert(subpr, rolename, ent);
                    set_inuse_ambiguous.add(ent)
                    errcounts['ambiguous_manifest'] += 1
                    # print >> sys.stderr, "On line %d - entsummary is ambiguous: %s" % (linenum, ent)
                else:
                    sql_insert(subpr, rolename, ent);
                    oknum +=1

print >> sys.stderr, "Number of entitlement-assignemnts we need to restore: %d" % attemptnum
print >> sys.stderr, "  > Number we CAN restore without ambiguity: %d" % oknum
print >> sys.stderr, "  > Number we CAN restore by guessing to resolve ambiguity: %d" % errcounts['ambiguous_manifest']
print >> sys.stderr, "  > Number we CANNOT restore (organized by reason for failure): %s" % str(errcounts)

print >> sys.stderr, '. . . . .'
print >> sys.stderr, "Number of detected ambiguous manifests: %d" % len(set_redundant)
print >> sys.stderr, "Number of actually-in-use ambiguous manifests: %d" % len(set_inuse_ambiguous)
with open('LISTinuseAmbiguousManifests.txt', 'w') as myfile:
    for x in set_inuse_ambiguous:
        myfile.write('\n' + x + '\n')
        for y in dict_ents[x]:
            y[16] = ''
            myfile.write('    ' + str(y) + '\n')

print >> sys.stderr, '. . . . .'
print >> sys.stderr, "Number of actually-in-use unknown manifests: %d" % len(set_unknown_manifests)
for x in sorted(set_unknown_manifests):
    print >> sys.stderr, '     %s' % x

print >> sys.stderr, '. . . . .'
print >> sys.stderr, "Number of actually-in-use unknown business roles: %d" % len(set_unknown_roles)
for x in sorted(set_unknown_roles):
    print >> sys.stderr, '     %s' % x
