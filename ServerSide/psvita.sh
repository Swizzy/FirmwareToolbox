#!/bin/bash
cd /homez.755/gxarena
#code goes below this line...
url=http://fjp01.psp2.update.playstation.net/update/psp2/list/us/psp2-updatelist.xml
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
			if [[ -f $bdir/$version"_2.name" ]] ; then
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

## These are for ALL of them
label=$(echo -e "$xmldata" | grep '\<version' | grep 'label=' | sed s/'"'/'\n'/g | head -2 | tail -1)
version=$(echo -e "$xmldata" | grep '\<version' | grep 'system_version=' | sed s/'"'/'\n'/g | head -2 | tail -1)
##

##These are just for the Update...
updsize=$(echo -e "$xmldata" | grep \<update_data\ update_type -A2 | grep \<image\ size | head -1 | sed s/'"'/'\n'/g | head -2 | tail -1)
updurl=$(echo -e "$xmldata" | grep \<update_data\ update_type -A2 | grep \<image\ size | head -1 | sed s/'">'/'\n'/g | head -2 | tail -1 | sed s/'?'/'\n'/g | head -1)
updhash=$(geturlhash $updurl)
urlsize=$(getsize $updurl)
if [[ $updsize != $urlsize ]] ; then
	updsize=$urlsize
fi
printf "label: $label\r\nversion: $version\r\nupdsize: $updsize\r\nupdurl: $updurl\r\nupdhash: $updhash\r\n"
checkupdate $basedir/PSVita $version $updurl $updhash $label $updsize
##

##these are for the system data
syssize=$(echo -e "$xmldata" | grep \<recovery \spkg_type -A4 | grep 'systemdata' -A4 | grep \<image\ spkg_version | head -1 | sed s/'"'/'\n'/g | head -4 | tail -1)
sysurl=$(echo -e "$xmldata" | grep \<recovery \spkg_type -A4 | grep 'systemdata' -A4 | grep \<image\ spkg_version | head -1 | sed s/'">'/'\n'/g | head -2 | tail -1 | sed s/'?'/'\n'/g | head -1)
syshash=$(geturlhash $sysurl)
urlsize=$(getsize $sysurl)
if [[ $syssize != $urlsize ]] ; then
	syssize=$urlsize
fi
printf "syssize: $syssize\r\nsysurl: $sysurl\r\nsyshash: $syshash\r\n"
checkupdate $basedir/PSVita/SYS $version $sysurl $syshash $label $syssize
##

##these are for the pre-install
presize=$(echo -e "$xmldata" | grep \<recovery \spkg_type -A4 | grep 'preinst' -A4 | grep \<image\ spkg_version | sed s/'"'/'\n'/g | head -4 | tail -1)
preurl=$(echo -e "$xmldata" | grep \<recovery \spkg_type -A4 | grep 'preinst' -A4 | grep \<image\ spkg_version | sed s/'">'/'\n'/g | head -2 | tail -1 | sed s/'?'/'\n'/g | head -1)
prehash=$(geturlhash $preurl)
urlsize=$(getsize $preurl)
if [[ $presize != $urlsize ]] ; then
	presize=$urlsize
fi
printf "presize: $presize\r\npreurl: $preurl\r\nprehash: $prehash\r\n"
checkupdate $basedir/PSVita/PRE $version $preurl $prehash $label $presize
##

upddata=$(printf "updlabel: $updlabel\r\nupdversion: $updversion\r\nupdsize: $updsize\r\nsysurl: $updurl\r\nsyshash: $updhash\r\nsyssize: $syssize\r\nsysurl: $sysurl\r\nsyshash: $syshash\r\npresize: $presize\r\npreurl: $preurl\r\nprehash: $prehash\r\n\r\n$xmldata")

if [[ $updatefound == 1 ]] ; then
	$(echo -e "$upddata" | mutt -s "PSVita Update found & Downloaded to GXArena!" $email)
else
	$(echo -e "$upddata" | mutt -s "PSVita Update check completed on GXArena!" $email)
fi