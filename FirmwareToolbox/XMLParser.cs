namespace FirmwareToolbox {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Xml;

    internal static class XMLParser {
        internal static void ParseVersionListFile(string listfile, List<VersionData> list) {
            list.Clear();
            if(!File.Exists(listfile))
                throw new FileNotFoundException();
            using (var xml = XmlReader.Create(listfile)) {
                while (xml.Read())
                {
                    if(!xml.Name.Equals("entry", StringComparison.CurrentCultureIgnoreCase))
                        continue;
                    list.Add(new VersionData(xml["name"], xml["hash"], xml["url"], xml["msg"]));
                }
            }
            list.Reverse();
        }

        internal static void ParseVersionListUrl(string url, List<VersionData> list) {
            var wc = new WebClient();
            var path = Path.GetTempFileName();
            wc.DownloadFile(url, path);
            ParseVersionListFile(path, list);
            File.Delete(path);
        }
    }
}