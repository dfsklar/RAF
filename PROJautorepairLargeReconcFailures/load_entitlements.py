import csv
import sys
import pickle

# Read the IDM reconcile and for each "remove entitlement" row create an INSERT statement that will
# simply patch the currently ACTIVE workspace for the relevant subprocess.

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
                genmanifestraw = row[14]
                genmanifest = genmanifestraw.rstrip()
                if genmanifest != genmanifestraw:
                    needingstrip += 1
                if (len(genmanifest) < 1) or (genmanifest=='NULL'):
                    num_nomanifest += 1
                elif dict_entitlements.has_key(genmanifest):
                    print 'REDUNDANT: %s' % row
                    num_redundant += 1
                else:
                    dict_entitlements[genmanifest] = c_id
                    numloaded += 1
            except:
                ignorenum += 1

pickle.dump(dict_entitlements, open('ents.pkl', 'wb'))
print 'Number of IGNORED lines: %d' % ignorenum
print 'Number of manifests loaded: %d' % numloaded
print 'Number of manifests that needed rstrip: %d' % needingstrip
print 'Number of manifests that need regeneration in order to be loaded: %d' % num_nomanifest
print 'Number of manifests seen multiple times: %d' % num_redundant


