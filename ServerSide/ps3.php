<?php
$data = file_get_contents('http://fus01.ps3.update.playstation.net/update/ps3/list/us/ps3-updatelist.txt');
foreach (explode(PHP_EOL, $data) as $line) {
    $ispatch = 0;
    $ver = "";
    $url = "";
    $hash = "";
    foreach (explode(";", $line) as $part) {
        if (strlen($ver) > 0 && strlen($url) > 0) {
            if ($ispatch > 0) {
                $patchver = $ver;
                $patchurl = $url;
                $patchhash = $hash;
            }
            else {
                $sysver = $ver;
                $sysurl = $url;
                $syshash = $hash;
            }
        }
        if (stripos($part, "SystemSoftwareVersion=") === 0)
            $ver = substr($part, strpos($part, "=") + 1);
        if (stripos($part, "CDN=") === 0) {
            $url = substr($part, strpos($part, "=") + 1);
            $hash = substr($url, strrpos($url, '_') + 1, 32);
            $ispatch = stripos($part, "PS3PATCH.PUP");
        }
    }
}
echo "patch:\n";
echo $patchver."\n";
echo $patchurl."\n";
echo $patchhash."\n";
echo "system:\n";
echo $sysver."\n";
echo $sysurl."\n";
echo $syshash;
?>