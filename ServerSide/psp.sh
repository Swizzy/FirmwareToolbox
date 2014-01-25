#!/bin/bash
cd /homez.755/gxarena
#code goes below this line...
url=http://fj01.psp.update.playstation.org/update/jp/psp-updatelist.txt
gourl=http://fe01.psp.update.playstation.org/update/psp/list2/jp/psp-updatelist.txt
srcdata=$(wget -qO- $url)
godata=$(wget -qO- $gourl)
basedir='www/Firmwares'
updatefound=0

function geturlhash() {
	echo $(echo -e "$1" | grep -o '[0-9a-fA-F]\{32\}/' | sed s/'\/'/''/g)
}

function getsize() {
	echo $(curl -sI "$1" | awk '/Content-Length/ { print $2 }')
}

function getpbp() {
	local url=$1
	local hash=$2
	local target=$3
	local loop=$4
	local size=$5
	local rmold=$6
	if [[ $loop < 10 ]] ; then
		if [[ -f tmp.dl && $rmold == TRUE ]] ; then
			rm tmp.dl
		elif [[ $size < $(stat -c%s 'tmp"$dlnum".dl') ]] ; then
			rm tmp.dl
		fi
		wget -cO tmp.dl $url
		checksum=$(md5sum tmp.dl | awk '{print $1}')
		shopt -s nocasematch
		if [[ "$checksum" == "$hash" ]] ; then
			updatefound=1
			echo "Checksums are a match! Copying the file to: $target"
			mv tmp.dl $target
			shopt -u nocasematch
			getpbp $url $hash $target $(($loop + 1)) $size FALSE
		fi
	fi
}

function checkupdate() {
	local dir=$1
	local version=$2
	local url=$3
	local hash=$4
	local size=$5
	
	if [[ -f $dir/$version.PBP && -f $dir/$version.hash ]] ; then
		local curr=$(cat $1/$2.hash)
		if [[ $hash != $curr ]] ; then
			getpbp $url $hash $dir/$version"_2.PBP" 0 $size TRUE
			if [[ -f $bdir/$version"_2.name" ]] ; then
				rm $dir/$version"_2.name"
			fi
			printf "$version v2" >> $dir/$version"_2.name"
			printf $hash >> $dir/$version"_2.hash"
		fi
	else
		getpbp $url $hash $dir/$version.PBP 0 $size TRUE
		printf $hash >> $dir/$version.hash
	fi
}

pspurl=$(echo -e "$srcdata" | grep -io 'http.*EBOOT.*;' | sed s/';'/'\n'/g | head -1)
pspver=$(echo -e "$srcdata" | grep -io 'SystemSoftwareVersion=.*;' | sed s/'='/'\n'/g | head -2 | tail -1 | sed s/';'/'\n'/g | head -1)
pspsize=$(getsize $pspurl)
psphash=$(geturlhash $pspurl)

gourl=$(echo -e "$godata" | grep -io 'http.*EBOOT.*;' | sed s/';'/'\n'/g | head -1)
gover=$(echo -e "$godata" | grep -io 'SystemSoftwareVersion=.*;' | sed s/'='/'\n'/g | head -2 | tail -1 | sed s/';'/'\n'/g | head -1)
gosize=$(getsize $gourl)
gohash=$(geturlhash $gourl)

checkupdate $basedir/PSP $pspver $pspurl $psphash $pspsize
if [[ updatefound == 1 ]]
	updatefound=0
	$(printf "version: $pspver\r\nurl: $pspurl\r\nsize: $pspsize\r\nhash: $psphash\r\n\r\n$(echo -e "$srcdata")" | mutt -s "PSP Update found & Downloaded to GXArena!" $email)
else
	$(printf "version: $pspver\r\nurl: $pspurl\r\nsize: $pspsize\r\nhash: $psphash\r\n\r\n$(echo -e "$srcdata")" | mutt -s "PSP Update check completed on GXArena!" $email)
fi
checkupdate $basedir/PSPGo $gover $gourl $gohash $gosize
if [[ updatefound == 1 ]]
	$(printf "version: $gover\r\nurl: $gourl\r\nsize: $gosize\r\nhash: $gohash\r\n\r\n$(echo -e "$godata")" | mutt -s "PSPGo Update found & Downloaded to GXArena!" $email)
else
	$(printf "version: $gover\r\nurl: $gourl\r\nsize: $gosize\r\nhash: $gohash\r\n\r\n$(echo -e "$godata")" | mutt -s "PSPGo Update check completed on GXArena!" $email)
fi