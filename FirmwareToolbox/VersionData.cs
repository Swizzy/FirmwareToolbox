namespace FirmwareToolbox {
    internal class VersionData {
        internal readonly string Display;
        internal readonly string Hash;
        internal readonly string Url;
        internal readonly string WarningMessage;

        public VersionData(string display, string hash, string url, string warningMessage = null) {
            Url = url;
            Display = display;
            Hash = hash;
            WarningMessage = warningMessage;
        }

        public override string ToString() { return Display; }
    }
}