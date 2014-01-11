<?php
$dir = substr($_SERVER["PATH_INFO"], 1).'/';
$site = "http://".$_SERVER["SERVER_NAME"].'/download.php/'.$dir;
$xml = new SimpleXMLElement('<xml/>');
$ffs = scandir($dir);
foreach($ffs as $ff){
 if($ff != '.' && $ff != '..' && !is_dir($dir.$ff)) {
    $ext = pathinfo($ff, PATHINFO_EXTENSION);
    if (strcasecmp($ext, 'php') == 0)
            continue;
    if (strcasecmp($ext, 'hash') == 0)
            continue;
    if (strcasecmp($ext, 'name') == 0)
            continue;
    if (strcasecmp($ext, 'msg') == 0)
            continue;
    if (strcasecmp($ext, 'count') == 0)
            continue;
    $entry = $xml->addChild('entry');
    $file = pathinfo($ff, PATHINFO_FILENAME);
    if (file_exists($dir.$file.'.name'))
        $entry->addAttribute('name', file_get_contents($dir.$file.'.name'));
    else
        $entry->addAttribute('name', $file);
    if (file_exists($dir.$file.'.hash'))
        $entry->addAttribute('hash', file_get_contents($dir.$file.'.hash'));
    else {
        $hash = md5_file($dir.$ff);
        $entry->addAttribute('hash', $hash);
        file_put_contents($dir.$file.'.hash', $hash);
    }            
    $entry->addAttribute('url',$site.$ff);
    if (file_exists($dir.$file.'.msg'))
        $entry->addAttribute('msg',file_get_contents($dir.$file.'.msg'));
    else
        $entry->addAttribute('msg','');
 }
}
Header('Content-type: text/xml');
print($xml->asXML());
?>