#!/bin/bash
cd /homez.755/gxarena/www
#code goes below this line...
while read scandir; do
	if [[ -d $scandir ]] ; then
		$(ls $scandir | grep 'hash$' >> /dev/null)
		if (( $? == 1 )) ; then
			echo "Directory $scandir is already clean of hash files..."
		else
			echo "Cleaning $scandir of .hash files..."
			rm $scandir/*.hash
		fi
	else
		echo "Directory $scandir don't exist..."
	fi
done < '../alldirs.txt'
