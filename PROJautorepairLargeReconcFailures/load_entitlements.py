import sys
import pickle


dict_formulas = pickle.load(open('mvformulas.pkl', 'rb'))

# KEY: generated manifest value
# VALUE: array of all rows that generated that manifest value
dict_entitlements = {}
set_redundants = set()


with open('Entitlement.txt', 'r') as csvfile:
    linenum = 0
    ignorenum = 0
    needingstrip = 0
    numloaded = 0
    num_nomanifest = 0
    num_redundant = 0
    for line in csvfile:
        row = line.strip().split('\t')
        linenum += 1
        if not (row[0] == 'c_id'):
            try:
                c_id = row[0]
                system = row[3]
                platform = Platform = row[4]
                entitlementname = row[5]
                entitlementvalue = EntitlementValue = row[6]
                authobjname = row[7]
                authobjvalue = row[8]
                fieldsecname = row[9]
                fieldsecvalue = FieldSecValue = row[10]
                level4secname = row[11]
                level4secvalue = row[12]
                appname = row[15]
                prehash = row[16]
                status = row[17]

                if status == 'X':
                  continue
                if status == 'I':
                  continue
                if prehash[0:6] == 'XXredu':
                  continue

                # Turns out that the generated manifest value stored in the Ents table is basically useless/obsolete.
                #   genmanifestraw = row[14].rstrip()
                # So we are going to regenerate the manifest from the formula for this application
                
                try:
                    formula = dict_formulas[appname]
                except:
                    print row
                    print "FATAL!!!! NO FORMULA FOR THIS APP: " + appname
                    print '=============='
                    sys.exit(1)
                    
                try:
                    genmanifest = ''
                    genmanifest = eval(formula)
                except:
                    print '================'
                    print("FATAL!!!!!!:", sys.exc_info()[0])
                    print formula
                    print '. . .'
                    print genmanifest
                    sys.exit(1)

                if (len(genmanifest) < 1) or (genmanifest=='NULL'):
                    num_nomanifest += 1
                elif dict_entitlements.has_key(genmanifest):
                    dict_entitlements[genmanifest].append(row)
                    set_redundants.add(genmanifest)
                else:
                    dict_entitlements[genmanifest] = [row]
                    numloaded += 1
            except:
                ignorenum += 1

for bad_manifest in set_redundants:
    print '========== OVERUSED MANIFEST ========'
    print bad_manifest
    for x in dict_entitlements[bad_manifest]:
        print x

pickle.dump(dict_entitlements, open('ents.pkl', 'wb'))
pickle.dump(set_redundants, open('manifests_ambiguous.pkl', 'wb'))

print 'Number of IGNORED lines: %d' % ignorenum
print 'Number of manifests loaded: %d' % numloaded
print 'Number of lines with no manifest: %d' % num_nomanifest
print '^^^ of the above, how many are ambiguous: %d' % len(set_redundants)
