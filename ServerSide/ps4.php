<?php
$xml = simplexml_load_string(file_get_contents('http://fjp01.ps4.update.playstation.net/update/ps4/list/us/ps4-updatelist.xml'));
//print_r($xml);
$sysver = $xml->region[0]->system_pup["label"];
$sysurl = $xml->region[0]->system_pup[0]->update_data[0]->image[0];
$sysurl = substr($sysurl, 0, strrpos($sysurl, '?'));
$syshash = substr($sysurl, strrpos($sysurl, '_') + 1, 32);
echo "system:\n";
echo $sysver."\n";
echo $sysurl."\n";
echo $syshash."\n";
$recver = $xml->region[0]->recovery_pup[0]->system_pup["label"];
$recurl = $xml->region[0]->recovery_pup[0]->image[0];
$recurl = substr($recurl, 0, strrpos($recurl, '?'));
$rechash = substr($recurl, strrpos($recurl, '_') + 1, 32);
echo "recovery:\n";
echo $recver."\n";
echo $recurl."\n";
echo $rechash;
?>