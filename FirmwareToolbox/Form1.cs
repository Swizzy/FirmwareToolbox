namespace FirmwareToolbox {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Windows.Forms;

    internal sealed partial class Form1 : Form {
        private const string BaseListUrl = "http://gxarena.com/list.php/Firmwares/";
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
            typelist.Items.Add(new TypeListItem("PS3 Update", "PS3UPDAT.PUP", "PS3\\UPDATE\\", string.Format("{0}PS3", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("PS3 Patch", "PS3PATCH.PUP", "PS3\\UPDATE\\", string.Format("{0}PS3/Patch", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("PS4 Update", "PS4UPDAT.PUP", "PS4\\UPDATE\\", string.Format("{0}PS4", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("PS4 Recovery", "PS4UPDAT.PUP", "PS4\\UPDATE\\", string.Format("{0}PS4/Recovery", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("PSP", "EBOOT.PBP", "PSP\\Game\\UPDATE\\", string.Format("{0}PSP", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("PSPGO", "EBOOT.PBP", "PSP\\Game\\UPDATE\\", string.Format("{0}PSPGO", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("PSVita", "PSP2UPDAT.PUP", versionListUrl: string.Format("{0}PSPVITA", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("PSVita Pre-Install", "PSP2UPDAT.PUP", versionListUrl: string.Format("{0}PSPVITA/PRE", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("PSVita System Data", "PSP2UPDAT.PUP", versionListUrl: string.Format("{0}PSPVITA/SYS", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("Xbox 360", fileSystem: "FAT", dlType: TypeListItem.DownloadTypes.Zip, versionListUrl: string.Format("{0}XBOX360", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("Xbox 360 BETA", fileSystem: "FAT", dlType: TypeListItem.DownloadTypes.Zip, versionListUrl: string.Format("{0}XBOX360/Beta", BaseListUrl)));
            typelist.Items.Add(new TypeListItem("Xbox One", fileSystem: "NTFS", dlType: TypeListItem.DownloadTypes.Zip, versionListUrl: string.Format("{0}XBOXONE", BaseListUrl)));
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
            chkbtn.Enabled = versionslist.SelectedItem as VersionData != null && !string.IsNullOrEmpty(((VersionData) versionslist.SelectedItem).Hash);
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
            var path = GetDefaultPath();
            if(sfd.ShowDialog() == DialogResult.OK)
                path = sfd.FileName;
            else if(MessageBox.Show("Do you wish to download to the default location?\r\n\r\n" + path, "Default location?", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                return;
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            SetState(false);
            var wc = new WebClient();
            wc.DownloadFileCompleted += WcOnDownloadFileCompleted;
            wc.DownloadProgressChanged += WcDownloadProgressChanged;
            wc.DownloadFileAsync(new Uri(vd.Url), path);
        }

        private void WcDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) { status.Text = string.Format("Downloading... {0} of {1} Bytes done! ({2}%)", e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage); }

        private string GetDefaultPath() {
            var vd = versionslist.SelectedItem as VersionData;
            var tp = typelist.SelectedItem as TypeListItem;
            if(tp == null || vd == null)
                throw new InvalidOperationException();
            var file = !string.IsNullOrEmpty(tp.FileName) ? string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(tp.FileName), vd.Display, Path.GetExtension(tp.FileName)) : vd.Display;
            return string.Format("{0}\\Swizzy\\FWToolbox\\Firmwares\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), file);
        }

        private void SetState(bool state) {
            versionslist.Enabled = state;
            typelist.Enabled = state;
            if(state)
                VersionslistSelectedIndexChanged(null, null);
            else {
                dwlbtn.Enabled = false;
                chkbtn.Enabled = false;
            }
        }

        private void WcOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs) { SetState(true); }

        private static string ArrayToHex(IList<byte> value, int i = 0, int count = -1) {
            char[] hexCharTable = {
                '0',
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                'A',
                'B',
                'C',
                'D',
                'E',
                'F'
            };
            if(count == -1)
                count = value.Count - i;
            else
                count = count + i;
            var c = new char[count * 2];
            for(var p = 0; i < count;) {
                var d = value[i++];
                c[p++] = hexCharTable[d / 0x10];
                c[p++] = hexCharTable[d % 0x10];
            }
            return new string(c);
        }


        private void ChkbtnClick(object sender, EventArgs e) {
            var path = GetDefaultPath();
            var ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
                path = ofd.FileName;
            if(!File.Exists(path))
                return; // The user probably didn't intend to click this button...
            var expected = ((VersionData) versionslist.SelectedItem).Hash;
            var md5 = MD5.Create();
            var fs = new FileStream(path, FileMode.Open);
            var actual = ArrayToHex(md5.ComputeHash(fs));
            fs.Close();
            if(!expected.Equals(actual, StringComparison.CurrentCultureIgnoreCase)) {
                if (MessageBox.Show(string.Format("The hashes don't match!\r\nExpected:\r\n{0}\r\nActual:\r\n{1}\r\n\r\nDo you wish to delete the faulty file?", expected.ToUpper(), actual), "WARNING: Files don't match!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                    File.Delete(path);
                return;
            }
            MessageBox.Show("Hashes are a match!");

        }
    }
}