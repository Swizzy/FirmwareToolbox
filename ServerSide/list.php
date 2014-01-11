<?php
function curPageURL() {
 $pageURL = 'http';
 if ($_SERVER["HTTPS"] == "on") {$pageURL .= "s";}
 $pageURL .= "://";
 if ($_SERVER["SERVER_PORT"] != "80") {
  $pageURL .= $_SERVER["SERVER_NAME"].":".$_SERVER["SERVER_PORT"].$_SERVER["REQUEST_URI"];
 } else {
  $pageURL .= $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];
 }

 return substr($pageURL, 0, strrpos($pageURL,"/")+1);
}
$site = curPageURL();
$xml = new SimpleXMLElement('<xml/>');
$ffs = scandir('.');
foreach($ffs as $ff){
 if($ff != '.' && $ff != '..' && !is_dir($ff)) {
    $ext = pathinfo($ff, PATHINFO_EXTENSION);
    if (strcasecmp($ext, 'php') == 0)
            continue;
    if (strcasecmp($ext, 'hash') == 0)
            continue;
    if (strcasecmp($ext, 'name') == 0)
            continue;
    if (strcasecmp($ext, 'msg') == 0)
            continue;
    $entry = $xml->addChild('entry');
    $file = pathinfo($ff, PATHINFO_FILENAME);
    if (file_exists($file.'.name'))
        $entry->addAttribute('name', file_get_contents($file.'.name'));
    else
        $entry->addAttribute('name', $file);
    if (file_exists($file.'.hash'))
        $entry->addAttribute('hash', file_get_contents($file.'.hash'));
    else {
        $hash = md5_file($ff);
        $entry->addAttribute('hash', $hash);
        file_put_contents($file.'.hash', $hash);
    }            
    $entry->addAttribute('url',$site.$ff);
    if (file_exists($file.'.msg'))
        $entry->addAttribute('msg',file_get_contents($file.'.msg'));
    else
        $entry->addAttribute('msg','');
 }
}
Header('Content-type: text/xml');
print($xml->asXML());
?>