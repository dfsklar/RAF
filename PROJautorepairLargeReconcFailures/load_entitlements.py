import csv
import sys
import pickle


dict_formulas = pickle.load(open('mvformulas.pkl', 'rb'))


# Must first read in: Entitlement.csv and store it keyed by: GENmanifestValue
dict_entitlements = {}
with open('Entitlement.csv', 'rb') as csvfile:
    dialect = csv.Sniffer().sniff(csvfile.read(1024))
    csvfile.seek(0)
    reader = csv.reader(csvfile, dialect)
    linenum = 0
    ignorenum = 0
    needingstrip = 0
    numloaded = 0
    num_nomanifest = 0
    num_redundant = 0
    for row in reader:
        linenum += 1
        if not (row[0] == 'c_id'):
            try:
                c_id = row[0]
                system = row[3]
                platform = row[4]
                entitlementname = row[5]
                entitlementvalue = row[6]
                authobjname = row[7]
                authobjvalue = row[8]
                fieldsecname = row[9]
                fieldsecvalue = row[10]
                level4secname = row[11]
                level4secvalue = row[12]
                appname = row[15]
                # Turns out that the generated manifest value stored in the Ents table is basically useless/obsolete.
                #   genmanifestraw = row[14].rstrip()
                # So we are going to regenerate the manifest from the formula for this application
                try:
                    formula = dict_formulas[appname]
                except:
                    print row
                    print ".... NO FORMULA FOR THIS APP: " + appname
                    print '=============='
                    sys.exit(1)
                #print "=========="
                #print formula
                try:
                    genmanifest = eval(formula)
                    #print genmanifest
                except:
                    print("Unexpected error:", sys.exc_info()[0])
                if (len(genmanifest) < 1) or (genmanifest=='NULL'):
                    num_nomanifest += 1
                elif dict_entitlements.has_key(genmanifest):
                    print 'REDUNDANT: %s /// %s' % (genmanifest, row)
                    num_redundant += 1
                else:
                    dict_entitlements[genmanifest] = c_id
                    numloaded += 1
            except:
                ignorenum += 1

pickle.dump(dict_entitlements, open('ents.pkl', 'wb'))
print 'Number of IGNORED lines: %d' % ignorenum
print 'Number of manifests loaded: %d' % numloaded
print 'Number of manifests seen multiple times: %d' % num_redundant


