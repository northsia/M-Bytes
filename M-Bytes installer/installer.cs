using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace M_Bytes_installer
{
    public partial class installer : Form
    {

        bool Run;
        

        string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string File_Path = "C:\\Program Files\\M33D Projects\\M-Bytes";
        string zipFilePath = Path.Combine("C:\\Program Files\\M33D Projects\\M-Bytes", "M-Bytes_v2.1.zip");
        string tempPath = Path.Combine(Path.GetTempPath(), "M-Bytes_v2.1.zip");

        string exe_Path = "C:\\Program Files\\M33D Projects\\M-Bytes\\M-Bytes.exe";

        public installer()
        {
            InitializeComponent();
        }

        private void installer_Load(object sender, EventArgs e)
        {
        }

        private void CreateShortcut()
        {
            object shDesktop = (object)"Desktop";

            WshShell shell = new WshShell();

            string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\M-Bytes.lnk";

            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);

            shortcut.Description = "M-Bytes Shortcut";

            shortcut.Hotkey = "Ctrl+Shift+N";

            shortcut.TargetPath = exe_Path;

            shortcut.Save();
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Proccess.Show();
                Proccess.Value = 0;

                if (!Directory.Exists(File_Path))
                {
                    Directory.CreateDirectory(File_Path);
                }

                Proccess.Value += 10;

                await Download();
                Proccess.Value += 40;

                await Task.Run(() => System.IO.File.Copy(tempPath, zipFilePath, true));
                Proccess.Value += 20;

                await Task.Run(() => Unzip(zipFilePath, File_Path));
                Proccess.Value += 20;

                if (System.IO.File.Exists(zipFilePath))
                {
                    System.IO.File.Delete(zipFilePath);
                }

                Process.Start(exe_Path);

                if (Run)
                {
                    CreateShortcut();
                }

                Proccess.Value = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        async Task Download()
        {
            string URI = await FIREBASE_DATA_API.SEND_GET_DATA_("Down_Path");

            MessageBox.Show(URI.Replace("\"", " "));

            await Request.GET(URI.Replace("\"", " "), tempPath);
        }

        void Unzip(string zipFilePath, string extractPath)
        {
            try
            {
                if (System.IO.File.Exists(zipFilePath))
                {
                    ZipFile.ExtractToDirectory(zipFilePath, extractPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Unzip: " + ex.Message);
            }
        }

        private void Proccess_ValueChanged(object sender, EventArgs e)
        {
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox1.Checked)
            {
                Run = true;
            }
            else
            {
                Run = false;
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
