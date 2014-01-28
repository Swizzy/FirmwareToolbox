#!/bin/bash
cd /homez.755/gxarena
#code goes below this line...

url='http://fjp01.ps4.update.playstation.net/update/ps4/list/us/ps4-updatelist.xml'
xmldata=$(wget -qO- $url)
basedir='www/Firmwares'
updatefound=0

function checkupdatecode() {
	exitcode=$1
	if (( exitcode == 0 )) ; then
        	updatefound=1
		echo "Update downloaded successfully!"
	else
        	case $exitcode in
                	2)
                        	echo "There is no newer version..."
	                        ;;
        	        *)
                	        echo "There was an error of type: $exitcode"
                        	;;
	        esac
	fi
}

function geturlhash() {
	echo $(echo -e "$1" | grep -o '[0-9a-fA-F]\{32\}/' | sed s/'\/'//g) # Search for all occurances of a 32-character long HEX string (128-bit MD5 hash) in the url and extract it (it should also end with a /)
}

function getsize() {
	echo $(curl -sI "$1" | awk '/Content-Length/ { print $2 }') # Download the header only and extract the "Content-Length" from it...
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

#TODO: Make it run the rest of the scripts...
echo "Checking PS4 Update..."
./updatecheck.sh $sysurl $basedir/PS4 $sysversion $syssize PUP $syslabel $syshash
checkupdatecode $?
echo "Checking PS4 Recovery..."
./updatecheck.sh $recurl $basedir/PS4/Recovery $recversion $recsize PUP $reclabel $rechash
checkupdatecode $?
