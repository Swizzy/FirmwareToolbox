#!/bin/bash
cd /homez.755/gxarena/www/Firmwares
#code goes below this line...
scandirs=("XBOX360" "XBOX360/Beta" "XBOX360/SUFiles" "XBOX360/SUFiles/Beta");
for scandir in ${scandirs[*]} ; do
    for file in $scandir/* ; do
        if [[ -f $file ]]
            then if [[ $file != *.hash ]] && [[ $file != *.name ]] && [[ $file != *.msg ]] && [[ $file != *.count ]] && [[ $file != *.xml ]] && [[ $file != *.php ]] ; then
                path=${file%/*}
                name=${file##*/}
                fname=${name%.*}
                if [[ ! -f $path/$fname.name ]] ; then
                     echo "2.0.${fname//[!0-9]/}.0" >> $path/$fname.name
                fi
            fi
        fi
    done
done