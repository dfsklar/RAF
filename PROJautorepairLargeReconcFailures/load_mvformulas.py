import csv
import sys
import pickle

dict_mvf = {}
linenum = 0
ignorenum = 0
with open('MVFormula.csv', 'r') as csvfile:
    for line in csvfile:
        linenum += 1
        line = line.rstrip()
        try:
            (c_id, appname, entval, authobj, fieldsecname, formula) = line.split(',')
        except:
            print "BAD LINE: %s" % line
            ignorenum += 1
        if not c_id == 'c_id':
            if (fieldsecname != 'NULL') or (entval != 'NULL') or (authobj != 'NULL'):
                print "BAD LINE: %s // %s,%s,%s" % (line, fieldsecname, entval, authobj)
                ignorenum += 1
            else:
                dict_mvf[appname] = formula.replace('&gt;','>').replace('&amp;','&')

pickle.dump(dict_mvf, open('mvformulas.pkl', 'wb'))
print 'Number of IGNORED lines: %d' % ignorenum
