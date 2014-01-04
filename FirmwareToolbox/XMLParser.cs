namespace FirmwareToolbox {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Xml;

    internal static class XMLParser {
        internal static void ParseVersionListFile(string listfile, List<VersionData> list) {
            var tmplst = new SortedDictionary<string, VersionData>();
            if(!File.Exists(listfile))
                throw new FileNotFoundException();
            using (var xml = XmlReader.Create(listfile)) {
                while (xml.Read())
                {
                    if (!xml.IsStartElement())
                        continue;
                    if(!xml.Name.Equals("entry", StringComparison.CurrentCultureIgnoreCase))
                        continue;
                    if (!string.IsNullOrEmpty(xml["name"]) && !tmplst.ContainsKey(xml["name"]))
                        tmplst.Add(xml["name"], new VersionData(xml["name"], xml["hash"], xml["url"], xml["msg"]));
                }
            }
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