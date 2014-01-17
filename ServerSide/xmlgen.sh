#!/bin/bash
cd /homez.755/gxarena/www
#code goes below this line...
baseurl=http://gxarena.com/download.php

#scandir=www-data
#scandir="Firmwares/PS3/Patch"
scandirs=("Firmwares/PS3" "Firmwares/PS3/Patch" "Firmwares/PS4" "Firmwares/PS4/Recovery" "Firmwares/PSP" "Firmwares/PSPGO" "Firmwares/PSVITA" "Firmwares/PSVITA/PRE" "Firmwares/PSVITA/SYS" "Firmwares/XBOX360" "Firmwares/XBOX360/Beta" "Firmwares/XBOX360/SUFiles" "Firmwares/XBOX360/SUFiles/Beta" "Firmwares/XBOXONE");
for scandir in ${scandirs[*]} ; do
    if [[ -f $scandir/list.xml ]] ; then
        rm $scandir/list.xml
    fi
    printf "<?xml version=\"1.0\"?>\r\n<xml>\r\n" >> $scandir/list.xml
    for file in $scandir/* ; do
        if [[ -f $file ]]
            then if [[ $file != *.hash ]] && [[ $file != *.name ]] && [[ $file != *.msg ]] && [[ $file != *.count ]] && [[ $file != *.xml ]] && [[ $file != *.php ]] ; then
                path=${file%/*}
                name=${file##*/}
                ext=${name##*.}
                fname=${name%.*}
                if [[ ! -f $path/$fname.hash ]] ; then
                    md5sum $file | awk '{ print $1 }' >> $path/$fname.hash
                fi
                if [[ -f $path/$fname.name ]] ; then
                    name=$(cat $path/$fname.name)
                else
                    name=$fname
                fi
                if [[ -f $path/$fname.msg ]] ; then
                    msg=$(cat $path/$fname.msg)
                fi
                printf "\t<entry name=\"$name\" hash=\"$(cat $path/$fname.hash)\" url=\"$baseurl/$scandir/$fname.$ext\" msg=\"$msg\"/>\r\n" >> $scandir/list.xml
            fi
        fi
    done
	printf "\t<generated timestamp=\"$(date +%s)\"/>\r\n</xml>" >> $scandir/list.xml
done