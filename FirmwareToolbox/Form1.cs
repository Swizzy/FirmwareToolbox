namespace FirmwareToolbox {
    using System;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Windows.Forms;

    internal sealed partial class Form1 : Form {
        private bool _autoUpdateVersionList;

        internal Form1() {
            InitializeComponent();
            var ver = Assembly.GetAssembly(typeof(Form1)).GetName().Version;
            var name = Assembly.GetAssembly(typeof(Form1)).GetName().Name;
            Text = string.Format("{0} v{1}.{2}", name, ver.Major, ver.Minor);
            Icon = Icon.ExtractAssociatedIcon(Assembly.GetAssembly(typeof(Form1)).Location);
            AddTypes();
            _autoUpdateVersionList = true;
        }

        private void AddTypes() {
            typelist.Items.Clear();
            typelist.Items.Add(new TypeListItem("PS3 Update", "PS3UPDAT.PUP", "PS3\\UPDATE\\", "http://gxarena.com/Firmwares/PS3/list.php"));
            typelist.Items.Add(new TypeListItem("PS3 Patch", "PS3PATCH.PUP", "PS3\\UPDATE\\", "http://gxarena.com/Firmwares/PS3/Patch/list.php"));
            typelist.Items.Add(new TypeListItem("PS4 Update", "PS4UPDAT.PUP", "PS4\\UPDATE\\", "http://gxarena.com/Firmwares/PS4/Update/list.php"));
            typelist.Items.Add(new TypeListItem("PS4 Recovery", "PS4UPDAT.PUP", "PS4\\UPDATE\\", "http://gxarena.com/Firmwares/PS4/Recovery/list.php"));
            typelist.Items.Add(new TypeListItem("PSP", "EBOOT.PBP", "PSP\\Game\\UPDATE\\", "http://gxarena.com/Firmwares/PSP/list.php"));
            typelist.Items.Add(new TypeListItem("PSPGO", "EBOOT.PBP", "PSP\\Game\\UPDATE\\", "http://gxarena.com/Firmwares/PSPGO/list.php"));
            //typelist.Items.Add(new TypeListItem("PSVita", "PSP2UPDAT.PUP" versionListUrl: "http://gxarena.com/Firmwares/PSVITA/list.php"); // No idea? 
            typelist.Items.Add(new TypeListItem("Xbox 360", fileSystem : "FAT", dlType : TypeListItem.DownloadTypes.Zip, versionListUrl : "http://gxarena.com/Firmwares/XBOX360/list.php"));
            typelist.Items.Add(new TypeListItem("Xbox 360 BETA", fileSystem : "FAT", dlType : TypeListItem.DownloadTypes.Zip, versionListUrl : "http://gxarena.com/Firmwares/XBOX360/BETA/list.php"));
            typelist.Items.Add(new TypeListItem("Xbox One", fileSystem : "NTFS", dlType : TypeListItem.DownloadTypes.Zip, versionListUrl : "http://gxarena.com/Firmwares/XBOXONE/list.php"));
            typelist.SelectedIndex = 0;
        }

        private void TypeLabelMouseHandler(object sender = null, EventArgs e = null) {
            TypeLabel.Enabled = false;
            TypeLabel.Enabled = true;
        }

        private void TypeLabelMouseHandler(object sender, MouseEventArgs e) { TypeLabelMouseHandler(); }

        private void TypelistSelectedIndexChanged(object sender, EventArgs e) {
            versionslist.DataSource = null;
            var itm = typelist.SelectedItem as TypeListItem;
            if(itm == null)
                return; //Dafuq?!
            if(itm.VersionsList.Count == 0)
                DownloadNewVersionList();
            versionslist.DataSource = itm.VersionsList;
        }

        private void VersionslistSelectedIndexChanged(object sender, EventArgs e) { dwlbtn.Enabled = versionslist.SelectedItem as VersionData != null && !string.IsNullOrEmpty(((VersionData) versionslist.SelectedItem).Url); }

        private void DownloadNewVersionList(bool manualUpdate = false) {
            var itm = typelist.SelectedItem as TypeListItem;
            if(itm == null)
                return; //Dafuq?!
            var path = string.Format("{0}\\Swizzy\\FWToolbox\\lists\\{1}.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), itm.Display);
            if(manualUpdate || _autoUpdateVersionList || !File.Exists(path)) {
                if(File.Exists(path))
                    File.Delete(path);
                var wc = new WebClient();
                wc.DownloadFile(itm.VersionListUrl, path);
            }
            if(!File.Exists(path))
                throw new Exception("Something went horribly wrong!!");
            XMLParser.ParseVersionListFile(path, itm.VersionsList);
        }
    }
}