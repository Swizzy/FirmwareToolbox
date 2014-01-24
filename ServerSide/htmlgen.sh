#!/bin/bash
cd /homez.755/gxarena/www
#code goes below this line...
baseurl=http://gxarena.com/download.php

#scandir=www-data
#scandir="Firmwares/PS3/Patch"
scandirs=("Firmwares/PS3" "Firmwares/PS3/Patch" "Firmwares/PS4" "Firmwares/PS4/Recovery" "Firmwares/PSP" "Firmwares/PSPGO" "Firmwares/PSVITA" "Firmwares/PSVITA/PRE" "Firmwares/PSVITA/SYS" "Firmwares/XBOX360" "Firmwares/XBOX360/Beta" "Firmwares/XBOX360/SUFiles" "Firmwares/XBOX360/SUFiles/Beta" "Firmwares/XBOXONE");
for scandir in ${scandirs[*]} ; do
	alternate=0
    if [[ -f $scandir/table.xml ]] ; then
        rm $scandir/table.xml
    fi
    for file in $(ls $scandir/* | sort -r) ; do
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
                if [[ -f $path/$fname.count ]] ; then
                    count=$(cat $path/$fname.count)
                else
                    count=-
                fi
                if [[ -f $path/$fname.msg ]] ; then
                    msg=$(cat $path/$fname.msg)
                fi
				if [ $((alternate % 2)) == 0 ] ; then
					printf "\t\t\t<tr>\r\n\t\t\t\t<td><a href=\"$baseurl/$scandir/$fname.$ext\">$name</a></td>\r\n\t\t\t\t<td>$(cat $path/$fname.hash)</td>\r\n\t\t\t\t<td>$msg</td>\r\n\t\t\t\t<td>$count</td>\r\n\t\t\t</tr>\r\n" >> $scandir/table.xml
				else
					printf "\t\t\t<tr class=\"alt\">\r\n\t\t\t\t<td><a href=\"$baseurl/$scandir/$fname.$ext\">$name</a></td>\r\n\t\t\t\t<td>$(cat $path/$fname.hash)</td>\r\n\t\t\t\t<td>$msg</td>\r\n\t\t\t\t<td>$count</td>\r\n\t\t\t</tr>\r\n" >> $scandir/table.xml
				fi
				let alternate+=1
            fi
        fi
    done
done