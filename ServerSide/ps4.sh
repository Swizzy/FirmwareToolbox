#!/bin/bash
cd /homez.755/gxarena
#code goes below this line...
url=http://fjp01.ps4.update.playstation.net/update/ps4/list/us/ps4-updatelist.xml
xmldata=$(wget -qO- $url)
basedir='www/Firmwares'
updatefound=0
email=$(cat emails.txt)

function geturlhash() {
	echo $(echo -e "$1" | grep -o '[0-9a-fA-F]\{32\}/' | sed s/'\/'/''/g)
}

function getsize() {
	echo $(curl -sI "$1" | awk '/Content-Length/ { print $2 }')
}

function getpup() {
	if [[ $4 < 10 ]] ; then
		if [[ -f tmp.dl && $6 == TRUE ]] ; then
			rm tmp.dl
		elif [[ $5 < $(stat -c%s 'tmp.dl') ]] ; then
			rm tmp.dl
		fi
		wget -cO tmp.dl $1
		#echo "wget exitcode: $?"
		checksum=$(md5sum tmp.dl | awk '{print $1}')
		shopt -s nocasematch
		updatefound=1
		if [[ "$checksum" == "$2" ]] ; then
			shopt -u nocasematch
			echo "Checksums are a match! Copying the file to: $3"
			mv tmp.dl $3
		else
			shopt -u nocasematch
			getpup $1 $2 $3 $(($4 + 1)) $5 FALSE
		fi
    fi
}

function checkupdate() {
	local dir=$1
	local version=$2
	local url=$3
	local hash=$4
	local size=$5
	local label=$6
	
	if [[ -f $dir/$version.PUP && -f $dir/$version.hash ]] ; then
		local curr=$(cat $1/$2.hash)
		if [[ $hash != $curr ]] ; then
			getpup $url $hash $version"_2.PUP" 0 $size TRUE
			if [[ -f $dir/$version"_2.name" ]] ; then
				rm $dir/$version"_2.name"
			fi
			printf "$label v2" >> $dir/$version"_2.name"
			printf $hash >> $dir/$version"_2.hash"
		fi
	else
		getpup $url $hash $dir/$version.PUP 0 $size TRUE
		if [[ -f $dir/$version.name ]] ; then
			rm $dir/$version.name
		fi
		printf $label >> $dir/$version.name
		printf $hash >> $dir/$version.hash
	fi
}


syslabel=$(echo -e "$xmldata" | grep \<system_pup | head -1 | sed s/'"'/'\n'/g | head -2 | tail -1)
sysversion=$(echo -e "$xmldata" | grep \<system_pup | head -1 | sed s/'"'/'\n'/g | head -4 | tail -1)
syssize=$(echo -e "$xmldata" | grep \<update_data\ update -A4 | grep \<image\ size | head -1 | sed s/'"'/'\n'/g | head -2 | tail -1)
sysurl=$(echo -e "$xmldata" | grep \<update_data\ update -A4 | grep \<image\ size | head -1 | sed s/'">'/'\n'/g | head -2 | tail -1 | sed s/'?'/'\n'/g | head -1)
syshash=$(geturlhash $sysurl)
urlsize=$(getsize $sysurl)
if [[ $syssize != $urlsize ]] ; then
	syssize=$urlsize
fi

printf "syslabel: $syslabel\r\nsysversion: $sysversion\r\nsyssize: $syssize\r\nsysurl: $sysurl\r\nsyshash: $syshash\r\n"

checkupdate $basedir/PS4 $sysversion $sysurl $syshash $syslabel $syssize

reclabel=$(echo -e "$xmldata" | grep \<system_pup | head -2 | tail -1 | sed s/'"'/'\n'/g | head -2 | tail -1)
recversion=$(echo -e "$xmldata" | grep \<system_pup | head -2 | tail -1 | sed s/'"'/'\n'/g | head -4 | tail -1)
recsize=$(echo -e "$xmldata" | grep \<recovery_pup\ type -A4 | grep \<image\ size= | sed s/'"'/'\n'/g | head -2 | tail -1)
recurl=$(echo -e "$xmldata" | grep \<recovery_pup\ type -A4 | grep \<image\ size= | sed s/'">'/'\n'/g | head -2 | tail -1 | sed s/'?'/'\n'/g | head -1)
rechash=$(geturlhash $recurl)
urlsize=$(getsize $recurl)
if [[ $recsize != $urlsize ]] ; then
	recsize=$urlsize
fi

printf "reclabel: $reclabel\r\nrecversion: $recversion\r\nrecsize: $recsize\r\nrecurl: $recurl\r\nrechash: $rechash\r\n"

checkupdate $basedir/PS4/Recovery $recversion $recurl $rechash $reclabel $recsize

upddata=$(printf "syslabel: $syslabel\r\nsysversion: $sysversion\r\nsyssize: $syssize\r\nsysurl: $sysurl\r\nsyshash: $syshash\r\nreclabel: $reclabel\r\nrecversion: $recversion\r\nrecsize: $recsize\r\nrecurl: $recurl\r\nrechash: $rechash\r\n\r\n$xmldata")

if [[ $updatefound == 1 ]] ; then
	$(echo -e "$upddata" | mutt -s "PS4 Update found & Downloaded to GXArena!" $email)
else
	$(echo -e "$upddata" | mutt -s "PS4 Update check completed on GXArena!" $email)
fi