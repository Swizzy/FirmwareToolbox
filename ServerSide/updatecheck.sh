#!/bin/bash
cd /homez.755/gxarena
#code goes below this line...

url=$1
dir=$2
version=$3
size=$4
ext=$5
label=$6
hash=$7
if [[ -f $dir/$version.$ext && -f $dir/$version.hash ]] ; then
	if [[ ! -f $dir/$version"_2.$ext" || ! -f $dir/$version"_2.hash" ]] ; then
		oldhash=$(cat $dir/$version.hash)
		shopt -s nocasematch
		if [[ $hash != $oldhash ]] ; then
			shopt -u nocasematch
			./updatedl.sh $url $dir/$version"_2.$ext" $size $hash
			exitcode=$?
			if (( $exitcode == 0)) ; then
				printf "$label v2" >> $dir/$version"_2.name"
			else
				exit $exitcode
			fi
		else
			shopt -u nocasematch
			exit 2
		fi
	else
		for (( i=2; ;i++ )) ; do
			if [[ -f $dir/$version"_"$(( $i + 1 )).$ext && -f $dir/$version"_"$(( $i + 1 )).hash ]] ; then
				continue
			fi
			if [[ -f $dir/$version"_$i.$ext" && -f $dir/$version"_$i.hash" ]] ; then
				oldhash=$(cat $dir/$version"_$i.hash")
				shopt -s nocasematch
				if [[ $hash != $oldhash ]] ; then
					shopt -u nocasematch
					./updatedl.sh $url $dir/$version"_"$(( $i + 1 )).$ext $size $hash
					exitcode=$?
					if (( $exitcode == 0 )) ; then
						printf "$label v"$(( $i + 1 )) >> $dir/$version"_"$(( $i + 1 )).name
					else
						exit $exitcode
					fi
				else
					shopt -u nocasematch
					exit 2
				fi
			else
				exit -1
			fi
		done
	fi
else
	./updatedl.sh $url $dir/$version.$ext $size $hash
fi
