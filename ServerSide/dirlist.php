<html>
	<head>
		<title>GXArena FW Repo</title>
<?php 
$dir=substr($_SERVER['REQUEST_URI'], 1, strrpos($_SERVER['REQUEST_URI'], "/"));
if ($dir != "Firmwares/")
{
echo '		<style type="text/css">
			body { margin: 0; padding: 0; }
			table { margin-left: auto; margin-right: auto; }
			table, td, th { border-collapse:collapse; font-size:1em; border:1px solid #98bf21; padding:3px 7px 2px 7px; }
			th { font-size:1.1em; text-align:left; padding-top:5px; padding-bottom:4px; background-color:#A7C942; color:#ffffff; }
			td { text-align: center; }
			tr.alt td { color:#000000; background-color:#EAF2D3; }
		</style>
	</head>
	<body>
		<table>
			<tr>
				<th>Version</th>
				<th>MD5 Hash</th>
				<th>Notes/Warnings</th>
				<th>Downloads</th>
			</tr>';
include($dir."/table.xml");
echo '		</table>';
}
else
{
echo '	</head>
	<body>
		<p>SHIT!!!!</p>
';
}
?>
</body>
</html>