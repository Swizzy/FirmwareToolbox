#!/bin/bash
cd /homez.755/gxarena
#code goes below this line...

function getfile() {
	local cycle=$1
	local url=$2
	local target=$3
	local rmold=$4
	local size=$5
	if [[ ! -z $6 ]] ; then
		local hash=$6
	fi

	if (( $cycle < 10 )) ; then
		if [[ -f tmp.dl ]] ; then
			if [[ $rmold == TRUE || (( $size < $(stat -c%s tmp.dl) )) ]] ; then
				rm tmp.dl
			fi
		fi
		wget -qcO tmp.dl $url
		if (( $size == $(stat -c%s tmp.dl) )) ; then
			if [[ -f tmp.dl && -n $hash ]] ; then
				local checksum=$(md5sum tmp.dl | awk '{print $1}')
				shopt -s nocasematch
				if [[ $checksum == $hash ]] ; then
					shopt -u nocasematch
					mv tmp.dl $target
					exit 0
				else
					getfile $(( $cycle + 1 )) $url $target TRUE $size $hash
				fi
			elif [[ -f tmp.dl ]] ; then
				mv tmp.dl $target
				exit 0
			fi
		else
			getfile $(( $cycle + 1 )) $url $target FALSE $size $hash
		fi
	else
		exit 1
	fi
}

getfile 0 $1 $2 $3 TRUE $4 $5
