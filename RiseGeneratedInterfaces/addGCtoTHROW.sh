for X in *.DB.I*.cs
do
    echo PROCESSING $X
    perl addGCtoTHROW.pl < $X > /tmp/$X
    cmp -s $X /tmp/$X || mv /tmp/$X $X
done
