#!/bin/bash
cd /homez.755/gxarena
#code goes below this line...

url='http://fjp01.psp2.update.playstation.net/update/psp2/list/us/psp2-updatelist.xml'
xmldata=$(wget -qO- $url)
basedir='www/Firmwares'
updatefound=0

function checkupdatecode() {
	exitcode=$1
	if (( exitcode == 0 )) ; then
        	updatefound=1
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

# These 2 are for ALL parts
label=$(echo -e "$xmldata" | grep '\<version' | grep 'label=' | sed s/'"'/'\n'/g | head -4 | tail -1)
version=$(echo -e "$xmldata" | grep '\<version' | grep 'system_version=' | sed s/'"'/'\n'/g | head -2 | tail -1)
printf "Label: $label\r\nVersion: $version\r\n\r\n"
#

# These are just the update...
updsize=$(echo -e "$xmldata" | grep \<update_data\ update_type -A2 | grep \<image\ size | head -1 | sed s/'"'/'\n'/g | head -2 | tail -1)
updurl=$(echo -e "$xmldata" | grep \<update_data\ update_type -A2 | grep \<image\ size | head -1 | sed s/'">'/'\n'/g | head -2 | tail -1 | sed s/'?'/'\n'/g | head -1)
updhash=$(geturlhash $updurl)
urlsize=$(getsize $updurl)
if [[ $updsize != $urlsize ]] ; then
	updsize=$urlsize
fi
printf "updsize: $updsize\r\nupdurl: $updurl\r\nupdhash: $updhash\r\n"
#

## These are the recovery ones...
# These are the systemdata recovery ones...
syssize=$(echo -e "$xmldata" | grep \<recovery\ spkg_type -A4 | grep 'systemdata' -A4 | grep \<image\ spkg_version | head -1 | sed s/'"'/'\n'/g | head -4 | tail -1)
sysurl=$(echo -e "$xmldata" | grep \<recovery\ spkg_type -A4 | grep 'systemdata' -A4 | grep \<image\ spkg_version | head -1 | sed s/'">'/'\n'/g | head -2 | tail -1 | sed s/'?'/'\n'/g | head -1)
syshash=$(geturlhash $sysurl)
urlsize=$(getsize $sysurl)
if [[ $syssize != $urlsize ]] ; then
	syssize=$urlsize
fi
printf "syssize: $syssize\r\nsysurl: $sysurl\r\nsyshash: $syshash\r\n"
#

# These are the pre-install recovery ones...
presize=$(echo -e "$xmldata" | grep \<recovery\ spkg_type -A4 | grep 'preinst' -A4 | grep \<image\ spkg_version | sed s/'"'/'\n'/g | head -4 | tail -1)
preurl=$(echo -e "$xmldata" | grep \<recovery\ spkg_type -A4 | grep 'preinst' -A4 | grep \<image\ spkg_version | sed s/'">'/'\n'/g | head -2 | tail -1 | sed s/'?'/'\n'/g | head -1)
prehash=$(geturlhash $preurl)
urlsize=$(getsize $preurl)
if [[ $presize != $urlsize ]] ; then
	presize=$urlsize
fi
printf "presize: $presize\r\npreurl: $preurl\r\nprehash: $prehash\r\n"
##

echo "Checking PSVita Update..."
./updatecheck.sh $updurl $basedir/PSVita $version $updsize PUP $label $updhash
checkupdatecode $?
echo "Checking PSVita Systemdata..."
./updatecheck.sh $sysurl $basedir/PSVita/SYS $version $syssize PUP $label $syshash
checkupdatecode $?
echo "Checking PSVita Pre-install..."
./updatecheck.sh $preurl $basedir/PSVita/PRE $version $presize PUP $label $prehash
checkupdatecode $?
if (( $updatefound == 1 )) ; then
	file=$basedir/PSVita/XML/$version.xml
	if [[ ! -f $file ]] ; then
		echo -e "$xmldata" >> $file
	#else
		#TODO: Add loop for silent version updates!
	fi
fi
