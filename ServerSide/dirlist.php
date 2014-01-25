<?php //error_reporting(0); ?>
<html>
	<head>
		<title>GXArena FW Repo</title>
		<style type="text/css">			
			body { margin:0; padding:0; font-family: verdana; font-size: 9pt;}
			table { margin-top:5px; margin-left:auto; margin-right:auto; }
			table, td, th { font-size:1em; padding:3px 7px 2px 7px; border-collapse:collapse; border:1px solid #A7C942;}
			th { background-color:#A7C942; }
			th.header { font-size:1.1em; text-align:left; padding-top:5px; padding-bottom:4px; color:#ffffff; }
			td { text-align:center; }
			tr.alt td { color:#000000; background-color:#EAF2D3; }
			td.hash { text-transform:uppercase; padding-top:10px; padding-bottom:10px; margin:0; }
			td.msg { text-align:left; }
			#nav ul{ margin-left:auto; margin-right:auto; list-style-type:none; padding:0; overflow:hidden; margin-bottom:0px; }
			#nav ul.top li { float:left; margin-right:5px; }
			#nav ul.top a:link, ul.top a:visited { padding-left:20px; padding-right:20px; border: 1px solid #A7C942; border-radius:10px; }
			#nav ul.sub { position:fixed; z-index:999; }
			#nav ul.sub li { float:none; }
			#nav ul.sub a { margin-top:8px; border: 1px solid #d2d9bd; border-radius:10px; }
			#nav ul.sub a:link, ul.sub a:visited { padding-left:4px; padding-right:4px; }
			#nav ul.sub li a { color:#000000; font-weight:normal; background-color:#d2d9bd; }
			#nav ul.sub li a:hover, ul.sub li a:active { background-color:#7A991A; border-color:#7A991A; }
			#nav li { display: block; position:relative; }
			#nav ul ul { display:none; width:inherit; }
			#nav ul li:hover > ul { display: block; }			
			#nav ul a:link, ul a:visited { display:block; font-weight:bold; color:#FFFFFF; background-color:#A7C942; text-align:center; padding:4px; text-decoration:none; }			
			#nav li:hover > a:hover{ background-color:#7A991A; border-color:#7A991A; }
			#nav li:hover > a { background-color:#d2d9bd; color:#000000; }
			p { text-align:left; }
			#footer { text-align: right; font-size:7pt; }
			.underline { text-decoration:underline; }
			.bold { font-weight:bold; }
			.bcrumbs { text-align:left; font-size:7pt; color:#FFFFFF; background-color:#A7C942; }
			.bcrumbs a:link, .bcrumbs a:visited { color:#FFFFFF; border:1px solid #A7C942; border-radius:5px; }
			.bcrumbs a:hover { color:#000000; border-color:#7A991A; background-color:#7A991A; }
		</style>
	</head>
	<body>
		<table>
			<tr id="nav">
				<th colspan="5">
					<ul class="top">
						<li><a href="/Firmwares/">Home</a></li>						
						<li>
							<a href="/Firmwares/PS3/">PS3</a>
							<ul class="sub">
								<li><a href="/Firmwares/PS3/Patch/">Patch</a></li>
							</ul>
						</li>
						<li>
							<a href="/Firmwares/PS4/">PS4</a>
							<ul class="sub">
								<li><a href="/Firmwares/PS4/Recovery/">Recovery</a></li>
							</ul>
						</li>
						<li><a href="/Firmwares/PSP/">PSP</a></li>
						<li><a href="/Firmwares/PSPGo/">PSPGo</a></li>
						<li>
							<a href="/Firmwares/PSVita/">PSVita</a>
							<ul class="sub">
								<li><a href="/Firmwares/PSVita/PRE/">Pre-Install</a></li>
								<li><a href="/Firmwares/PSVita/SYS/">System Data</a></li>
							</ul>
						</li>
						<li>
							<a href="/Firmwares/Xbox360/">Xbox 360</a>
							<ul class="sub">
								<li><a href="/Firmwares/Xbox360/SUFiles/">SU Only</a></li>
								<li><a href="/Firmwares/Xbox360/Beta/">Beta</a></li>
								<li><a href="/Firmwares/Xbox360/SUFiles/Beta/">Beta (SU Only)</a></li>
							</ul>
						</li>
						<li><a href="/Firmwares/XboxOne/">Xbox One</a></li>
						<li>
							<a href="/Firmwares/Tools/">Tools</a>
							<!--
							<ul class="sub">
								<li><a href="/Firmwares/Tools/FWToolbox/">FW Toolbox</a></li>
								<li><a href="/Firmwares/Tools/FWToolbox/Plugins/">FWToolbox Plugins</a></li>
							</ul>
							-->
						</li>
					</ul>
				</th>
			</tr>
			<tr>
				<td class="bcrumbs"
<?php 
$dir=substr($_SERVER['REQUEST_URI'], 1, strrpos($_SERVER['REQUEST_URI'], "/"));
if ($dir != "Firmwares/")
	echo  "colspan=\"5\">You are here: <a href=\"/Firmwares/\">Home</a>";
else
	echo  ">You are here: <a href=\"/Firmwares/\">Home</a></td>";
if ($dir != "Firmwares/") {
	$bdir = "/Firmwares/";
	foreach (explode("/", $dir) as $part) {
		if ($part == "Firmwares" || $part == "/")
			continue;
		if ($part == "")
			break;
		echo " &gt; <a href=\"".$bdir.$part."\">".$part."</a>";
		$bdir.=$part."/";
	}
	echo "
				</td>";
}
if ($dir == "Firmwares/Tools/" || $dir == "Firmwares/FWToolbox/Plugins/") {
	echo '
			<tr>
				<th class=\"header\">Name</th>
				<th class=\"header\">MD5 Hash</th>
				<th class=\"header\">Filesize</th>
				<th class=\"header\">Comments</th>
				<th class=\"header\">Downloads</th>
			</tr>
';
	include($dir."/table.xml");
	$lastupdated = date("F d Y H:i:s", filemtime($dir."/table.xml"));
}
else if ($dir != "Firmwares/" && $dir != "/") {
	if (file_exists($dir."/table.xml"))
	{
		echo '
			<tr>
				<th class=\"header\">Version</th>
				<th class=\"header\">MD5 Hash</th>
				<th class=\"header\">Filesize</th>
				<th class=\"header\">Comments</th>
				<th class=\"header\">Downloads</th>
			</tr>
';
		include($dir."/table.xml");
		$lastupdated = date("F d Y H:i:s", filemtime($dir."/table.xml"));
	}
	else
		unset($lastupdated);
}
else {
	$lastupdated = date("F d Y H:i:s", filemtime("dirlist.php"));
	echo "
			<tr>
				<td>
				<h1>Welcome to GXArena FW Repo!</h1>
				<p>This site has been created for you to be able to download <span class=\"underline\">ALL</span> Original Console Firmware updates that are available without looking all around the web to find it all... the site is regenerated every 24h (the update list/download count)</p><br />
				<p>This site also have scripts that check for new PS3, PS4, PSP, PSPGo and PSVita updates that also run every 24h these scripts download these updates (if they exist) so, it should pretty much run itself for that part...</p><br />
				<!--<p>You may want to look at our own application: <a href=\"/Firmwares/Tools/FWToolbox/\">FW Toolbox</a>, it's been made for you to be able to download and use all updates you can find on this site, it is upgradeable through <a href=\"/Firwmares/Tools/FWToolbox/Plugins/\">plugins</a></p><br />-->
				<p>This site has been setup and is maintained by:</p>
				<p><span class=\"bold\">Swizzy</span> - Responsible for everything Xbox 360/Xbox One related and FW Toolbox</p>
				<p><span class=\"bold\">Stryp</span> - Responsible for server maintenance and paying the bills ;)</p>
				<p>There has been others whom have been working on this site along with us, but they're so many that we cannot mention everyone here...</p>
				</td>
			</tr>
";
}
if (isset($lastupdated))
{
	echo "			<tr>\r\n				<td";
	if ($dir != "Firmwares/")
		echo " colspan=\"5\"";
	echo "><p id=\"footer\">This page was last updated: ".$lastupdated."</p></td>\r\n			</tr>";
}
?>

		</table>
	</body>
</html>