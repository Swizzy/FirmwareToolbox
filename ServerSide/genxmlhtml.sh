#!/bin/bash
cd /homez.755/gxarena/www
#code goes below this line...
baseurl='http://gxarena.com/download.php' # define the base url
excls=$(cat $HOME'/exceptions.txt' | tr '\n' '|' | sed s/'|$'//) # get exceptions (files and extensions) and remove the last | as we don't want to exclude EVERYTHING
while read scandir; do # loop directories as defined in $HOME/alldirs.txt...
	alternate=0 # reset alternate counter...
	if [[ -d $scandir ]] ; then # check that $scandir is a directory that actually exist, otherwise ignore it...
		echo "Processing $scandir..."
		if [[ -f $scandir/list.xml ]] ; then # we want to remove old ones...
			rm $scandir/list.xml
		fi
		if [[ -f $scandir/table.xml ]] ; then # we want to remove old ones...
			rm $scandir/table.xml
		fi
		printf "<?xml version=\"1.0\"?>\r\n<xml>\r\n" >> $scandir/list.xml #we want to init the xml properly
		for file in $(ls $scandir/* | sort -r | egrep -v \($excls\)) ; do # loop files that are NOT in the exlude list, and sort them as latest first (by name)
			if [[ ! -f $file ]] ; then continue ; fi # check that we are working on files not subfolders...
			path=${file%/*} # get path name
			name=${file##*/} # get filename with extension
			ext=${name##*.} # get extension
			fname=${name%.*} # get filename only
			if [[ ! -f $path/$fname.hash ]] ; then # check if the hash exist, if not create it!
				echo "Hashing $file"
				md5sum $file | awk '{ print $1 }' >> $path/$fname.hash
			fi
			if [[ -f $path/$fname.name ]] ; then # look for a name file, if it doesn't exist, we want the filename without extension...
				name=$(cat $path/$fname.name)
			else
				name=$fname
			fi
			if [[ -f $path/$fname.msg ]] ; then # look for a msg file, if it doesn't exist, msg should be empty...
				msg=$(cat $path/$fname.msg)
			else
				msg=''
			fi
			if [[ -f $path/$fname.count ]] ; then # look for download count, if there is no such file, it's 0 which we set as - for easier viewing
				dlcount=$(cat $path/$fname.count)
			else
				dlcount='-'
			fi
			fsize=$(stat -c%s $file) # get filesize in bytes
			fsizeh=$(~/numfmt --to=iec $fsize)B # turn filesize into human-readable form
			printf "\t<entry name=\"$name\" hash=\"$(cat $path/$fname.hash)\" url=\"$baseurl/$scandir/$fname.$ext\" msg=\"$msg\" filesize=\"$fsize\" dlcount=\"$dlcount\" fsize=\"$fsize\"/>\r\n" >> $scandir/list.xml
			msg=${msg//$'\n'/"<br />"} # replace all new line with <br /> for html output
			if [ $((alternate % 2)) == 0 ] ; then
				printf "\t\t\t<tr>\r\n\t\t\t\t<td class=\"ver\"><a href=\"$baseurl/$scandir/$fname.$ext\">$name</a></td>\r\n\t\t\t\t<td class=\"hash\">$(cat $path/$fname.hash)</td>\r\n\t\t\t\t<td>$fsizeh</td>\r\n\t\t\t\t<td class=\"msg\">$msg</td>\r\n\t\t\t\t<td class=\"dlcount\">$dlcount</td>\r\n\t\t\t</tr>\r\n" >> $scandir/table.xml
			else
				printf "\t\t\t<tr class=\"alt\">\r\n\t\t\t\t<td class=\"ver\"><a href=\"$baseurl/$scandir/$fname.$ext\">$name</a></td>\r\n\t\t\t\t<td class=\"hash\">$(cat $path/$fname.hash)</td>\r\n\t\t\t\t<td>$fsizeh</td>\r\n\t\t\t\t<td class=\"msg\">$msg</td>\r\n\t\t\t\t<td class=\"dlcount\">$dlcount</td>\r\n\t\t\t</tr>\r\n" >> $scandir/table.xml
			fi
			alternate=$((alternate + 1)) # increment alternate counter
		done
		printf "\t<generated timestamp=\"$(date +%s)\"/>\r\n</xml>" >> $scandir/list.xml
	fi
done < $HOME'/alldirs.txt'
