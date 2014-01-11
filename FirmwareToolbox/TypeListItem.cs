namespace FirmwareToolbox {
    using System;
    using System.Collections.Generic;

    internal sealed class TypeListItem {
        internal readonly DownloadTypes DLType;
        internal readonly string Display;
        internal readonly string FileName;
        internal readonly string FilePath;
        internal readonly string FileSystem;
        internal readonly string VersionListUrl;
        internal readonly List<VersionData> VersionsList = new List<VersionData>();
        internal DateTime LastUpdate = DateTime.Now;

        internal TypeListItem(string display, string fileName = "", string filePath = "", string versionListUrl = "", string fileSystem = "FAT32", DownloadTypes dlType = DownloadTypes.File) {
            Display = display;
            FileName = fileName;
            FilePath = filePath;
            FileSystem = fileSystem;
            DLType = dlType;
            VersionListUrl = versionListUrl;
        }

        public override string ToString() { return Display; }

        #region Nested type: DownloadTypes

        internal enum DownloadTypes {
            File,
            Zip
        }

        #endregion
    }
}