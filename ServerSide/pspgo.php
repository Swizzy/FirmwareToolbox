<?php
$data = file_get_contents('http://fe01.psp.update.playstation.org/update/psp/list2/jp/psp-updatelist.txt');
//echo $data;
foreach (explode(PHP_EOL, $data) as $line) {
    if (strlen($ver) > 0 && strlen($url) > 0)
        break;
    foreach (explode(";", $line) as $part) {
        if (stripos($part, "#SystemSoftwareVersion=") === 0)
            $ver = substr($part, strpos($part, "=") + 1);
        if (stripos($part, "CDN=") === 0) {
            $url = substr($part, strpos($part, "=") + 1);
            $url = substr($url, 0, strrpos($url, "?"));
            $hash = substr($url, strrpos($url, "_") + 1, 32);
        }
    }
}
echo $ver."\n";
echo $url."\n";
echo $hash;
?>