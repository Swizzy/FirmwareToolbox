<?php
$xml = simplexml_load_string(file_get_contents('http://fjp01.psp2.update.playstation.net/update/psp2/list/us/psp2-updatelist.xml'));
//print_r($xml);
$ver = $xml->region[0]->version["label"];

$sysurl = $xml->region[0]->version[0]->update_data[0]->image[0];
$sysurl = substr($sysurl, 0, strrpos($sysurl, '?'));
$syshash = substr($sysurl, strrpos($sysurl, '_') + 1, 32);

$preurl = $xml->region[0]->recovery[0]->image[0];
$preurl = substr($preurl, 0, strrpos($preurl, '?'));
$prehash = substr($preurl, strrpos($preurl, '_') + 1, 32);

$sdurl = $xml->region[0]->recovery[1]->image[0];
$sdurl = substr($sdurl, 0, strrpos($sdurl, '?'));
$sdhash = substr($sdurl, strrpos($sdurl, '_') + 1, 32);

echo $ver."\n";
echo "Update:\n";
echo $sysurl."\n";
echo $syshash."\n";
echo "System Data:\n";
echo $sdurl."\n";
echo $sdhash."\n";
echo "Pre-Install:\n";
echo $preurl."\n";
echo $prehash."\n";
?>