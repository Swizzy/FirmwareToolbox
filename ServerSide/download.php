<?php
error_reporting(0);
function readfile_chunked ($fname) {
    $chunksize = 1*(1024*1024); // how many bytes per chunk 
    $buffer = ''; 
    $handle = fopen($fname, 'rb'); 
    if ($handle === false)
        return false; 
    while (!feof($handle)) { 
        $buffer = fread($handle, $chunksize); 
        print $buffer; 
    } 
    return fclose($handle); 
}
$file = substr($_SERVER["PATH_INFO"], 1);
if (file_exists($file)) {
    //Send file...
    header("Pragma: public");
    header("Expires: 0");
    header("Cache-Control: must-revalidate, post-check=0, pre-check=0");
    header("Content-Type: application/force-download");
    header("Content-Type: application/octet-stream");
    header("Content-Type: application/download");
    header("Content-Disposition: attachment; filename=\"".basename($file)."\";" );
    header("Content-Transfer-Encoding: binary");
    header("Content-Length: ".filesize($file));
    readfile_chunked($file);
    //Update count...
    $cntfile = substr($file, 0, strrpos($file, ".")).".count";
    $downCount = 1;
    if (file_exists($cntfile))
        $downCount += intval(file_get_contents($cntfile));        
    file_put_contents($cntfile ,$downCount);
}
?>