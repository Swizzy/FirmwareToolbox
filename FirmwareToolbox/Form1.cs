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
            _autoUpdateVersionList = true;
            var ver = Assembly.GetAssembly(typeof(Form1)).GetName().Version;
            var name = Assembly.GetAssembly(typeof(Form1)).GetName().Name;
            Text = string.Format("{0} v{1}.{2}", name, ver.Major, ver.Minor);
            Icon = Icon.ExtractAssociatedIcon(Assembly.GetAssembly(typeof(Form1)).Location);
            AddTypes();
        }

        private void AddTypes() {
            typelist.Items.Clear();
            typelist.Items.Add(new TypeListItem("PS3 Update", "PS3UPDAT.PUP", "PS3\\UPDATE\\", "http://gxarena.com/Firmwares/PS3/list.php"));
            typelist.Items.Add(new TypeListItem("PS3 Patch", "PS3PATCH.PUP", "PS3\\UPDATE\\", "http://gxarena.com/Firmwares/PS3/Patch/list.php"));
            typelist.Items.Add(new TypeListItem("PS4 Update", "PS4UPDAT.PUP", "PS4\\UPDATE\\", "http://gxarena.com/Firmwares/PS4/list.php"));
            typelist.Items.Add(new TypeListItem("PS4 Recovery", "PS4UPDAT.PUP", "PS4\\UPDATE\\", "http://gxarena.com/Firmwares/PS4/Recovery/list.php"));
            typelist.Items.Add(new TypeListItem("PSP", "EBOOT.PBP", "PSP\\Game\\UPDATE\\", "http://gxarena.com/Firmwares/PSP/list.php"));
            typelist.Items.Add(new TypeListItem("PSPGO", "EBOOT.PBP", "PSP\\Game\\UPDATE\\", "http://gxarena.com/Firmwares/PSPGO/list.php"));
            //typelist.Items.Add(new TypeListItem("PSVita", "PSP2UPDAT.PUP" versionListUrl: "http://gxarena.com/Firmwares/PSVITA/list.php"); // No idea? 
            typelist.Items.Add(new TypeListItem("Xbox 360", fileSystem : "FAT", dlType : TypeListItem.DownloadTypes.Zip, versionListUrl : "http://gxarena.com/Firmwares/XBOX360/list.php"));
            typelist.Items.Add(new TypeListItem("Xbox 360 BETA", fileSystem : "FAT", dlType : TypeListItem.DownloadTypes.Zip, versionListUrl : "http://gxarena.com/Firmwares/XBOX360/Beta/list.php"));
            typelist.Items.Add(new TypeListItem("Xbox One", fileSystem : "NTFS", dlType : TypeListItem.DownloadTypes.Zip, versionListUrl : "http://gxarena.com/Firmwares/XBOXONE/list.php"));
            typelist.SelectedIndex = 0;
        }

        private void TypelistSelectedIndexChanged(object sender, EventArgs e) {
            versionslist.DataSource = null;
            var itm = typelist.SelectedItem as TypeListItem;
            if(itm == null)
                return; //Dafuq?!
            if(itm.VersionsList.Count == 0)
                DownloadNewVersionList();
            versionslist.DataSource = itm.VersionsList;
        }

        private void VersionslistSelectedIndexChanged(object sender, EventArgs e) {
            dwlbtn.Enabled = versionslist.SelectedItem as VersionData != null && !string.IsNullOrEmpty(((VersionData) versionslist.SelectedItem).Url);
            chkbtn.Enabled = versionslist.SelectedItem as VersionData != null && !string.IsNullOrEmpty(((VersionData)versionslist.SelectedItem).Hash);
        }

        private void DownloadNewVersionList(bool manualUpdate = false) {
            var itm = typelist.SelectedItem as TypeListItem;
            if(itm == null)
                return; //Dafuq?!
            var path = string.Format("{0}\\Swizzy\\FWToolbox\\lists\\{1}.xml", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), itm.Display);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
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

        private void DwlbtnClick(object sender, EventArgs e) {
            var vd = versionslist.SelectedItem as VersionData;
            var tp = typelist.SelectedItem as TypeListItem;
            if(tp == null || vd == null)
                return; // We have ourselves a BUG!!!!!!!! WE MUST CRUSH IT!!
            var sfd = new SaveFileDialog {
                FileName = !string.IsNullOrEmpty(tp.FileName) ? string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(tp.FileName), vd.Display, Path.GetExtension(tp.FileName)) : vd.Display
            };
        }
    }
}