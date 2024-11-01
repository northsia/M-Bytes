using Guna.UI2.WinForms;
using KeyAuth;
using M_Bytes.Core.Account;
using M_Bytes.Core.EngineCL;
using M_Bytes.Core.Messages;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using FirebaseAdmin.Auth;
using Auth0.OidcClient;
using IdentityModel.OidcClient;
using M_Bytes.Core.Firebase_data;

namespace M_Bytes
{
    public partial class index : Form
    {

        /// <summary>
        /// This M-Bytes UI ==> User InterFace
        /// Created by : Meed
        /// </summary>



        /// <summary>
        /// logger For Debug ...
        /// </summary>
        public static readonly Logger logger = LogManager.GetCurrentClassLogger();


        public static string user;
        public static string password;
        string api_user;
        string Directory_For_Duplicate;

        public static api KeyAuthApp = new api(
            name: "M-Bytes", // App name - VaultCord.com FREE Discord backup bot for members & your entire server saved from terms and nukes!
            ownerid: "4Mgrdto854", // Account ID
            version: "1.0" // Application version. Used for automatic downloads see video here https://www.youtube.com/watch?v=kW195PLCBKs
        );

        private PrivateFontCollection customFonts = new PrivateFontCollection();

        public index()
        {
            try
            {
                InitializeComponent();

                LoadCustomFont("Fonts/Gotham.otf");

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }


        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        private void EnableDarkMode()
        {
            int isDarkMode = 1;
            DwmSetWindowAttribute(this.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref isDarkMode, sizeof(int));
        }


        private void LoadLabelFont()
        {
            Font LabelFont = new Font(customFonts.Families[0], 19, FontStyle.Regular);
            Font NameFont = new Font(customFonts.Families[0], 12, FontStyle.Regular);

            guna2HtmlLabel4.Font = LabelFont;
            label1.Font = NameFont;
            guna2HtmlLabel5.Font = LabelFont;
            guna2HtmlLabel6.Font = LabelFont;
            guna2HtmlLabel3.Font = LabelFont;


        }

        private async void SetCPR()
        {
            try
            {
                string CPR = await FIREBASE_DATA_API.SEND_GET_DATA_("Copyright");

                CPR = CPR?.Replace("\"", string.Empty);

                if (!string.IsNullOrEmpty(CPR))
                {
                    guna2HtmlLabel2.Text = CPR;
                }
                else
                {
                    guna2HtmlLabel2.Text = "M-Bytes | m33d";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }




        private void LoadCustomFont(string fontPath)
        {
            if (!System.IO.File.Exists(fontPath))
            {
                MessageBox.Show("Font file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IntPtr fontPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * 1024);
            try
            {
                customFonts.AddFontFile(fontPath);
                Font customFont = new Font(customFonts.Families[0], 12, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading font: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Marshal.FreeCoTaskMem(fontPtr);
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                guna2Button1.Enabled = true;
                guna2Button1.Cursor = Cursors.Default;

            }
            else if (guna2Button1.Checked == false)
            {
                guna2Button1.Enabled = false;
                guna2Button1.Cursor = Cursors.No;
            }
        }

        private void auth_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                guna2CircleProgressBar1.Show();

                await Task.Delay(3000);

                user = guna2TextBox1.Text;
                password = guna2TextBox2.Text;

                await Task.Run(() => KeyAuthApp.login(user, password));

                if (KeyAuthApp.response.success)
                {
                    MSG.SH("Authentication\nSuccess !", "redirect to panel ...");

                    user_data.Save(KeyAuthApp.user_data.username.ToString());

                    // set UserName Profile

                    label1.Text = KeyAuthApp.user_data.username.ToString();

                    Text = "M-Bytes | Home";

                    main.BringToFront();
                }
                else
                {
                    string message = KeyAuthApp.response.message;
                    MSG.SH("Authentication\nFailed !", message);
                }
            }
            catch (Exception ex)
            {

                MSG.SH("Failed !", $"{ex.Message}");
            }
        }



        public static void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e) // PASSWD AUTH
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.PasswordChar == '\0')
            {
                guna2TextBox2.PasswordChar = '*';
            }
            else if (guna2TextBox2.PasswordChar == '*')
            {
                guna2TextBox2.PasswordChar = '\0';
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (option.Visible == false)
            {
                option.Visible = true;
            }
            else
            {
                option.Visible = false;
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e) // USER AUTH
        {

        }

        private void main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FullCL.BringToFront();

        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            
        }


        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox3_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void center_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {
            Duplicate.BringToFront();
        }

        private async void guna2Button9_Click(object sender, EventArgs e)
        {
            try
            {
                circlebar.Show();

                if (guna2CheckBox2.Checked && !string.IsNullOrWhiteSpace(guna2TextBox3.Text))
                {
                    await Task.Run(() => EngineCL.DeleteDuplicateFilesAsync(guna2TextBox3.Text));
                }
                else if (!guna2CheckBox2.Checked)
                {
                    MSG.SH("Nope","You Must Check the Box !");
                }

                MSG.SH("Done !", "Done Remove Duplicate ...");
            }
            catch (Exception ex)
            {
                MSG.SH("Error", $"An error occurred: {ex.Message}");
            }
            finally
            {
                circlebar.Hide();
            }
        }



        private void guna2CheckBox2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (guna2CheckBox2.Checked) 
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string Directory_For_Duplicate = folderBrowserDialog.SelectedPath;
                    guna2TextBox3.Text = Directory_For_Duplicate;
                }
            }

        }

        private void Duplicate_Paint(object sender, PaintEventArgs e)
        {

        }

        private void index_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.Info("Application is closing at {0}", DateTime.Now);
        }

        private async void guna2Button10_Click(object sender, EventArgs e)
        {
            circlebar.Show();

            await Task.Delay(600);


            auth.BringToFront();

            circlebar.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            BrowserCL.BringToFront();
        }

        private async void guna2Button11_Click(object sender, EventArgs e)
        {
            try
            {
                circlebar.Visible = true;

                if (guna2CheckBox3.Checked)
                {
                    Task Cleanup = Task.Run(async () =>
                    {
                        await EngineCL.CleanAllBrowserCaches();
                        await EngineCL.RegistryClean();
                        await EngineCL.MemoryClean();
                        await EngineCL.CleanTemp();
                        await EngineCL.RemoveStartUpFiles();
                        await EngineCL.CleanPrefetch();
                    });

                    MSG.SH("Done !", "Cleaning Seccess !");
                    await Cleanup;
                }
                else if (!guna2CheckBox3.Checked)
                {
                    Task Cleanup = Task.Run(async () =>
                    {
                        await EngineCL.CleanAllBrowserCaches();
                        await EngineCL.RegistryClean();
                        await EngineCL.MemoryClean();
                        await EngineCL.CleanTemp();
                        await EngineCL.CleanPrefetch();
                    });

                    await Cleanup;
                }
            }
            finally
            {
                MSG.SH("Done !", "Task Completed ...");
                circlebar.Visible = false;
            }

        }

        private void BrowserCL_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Blocker.BringToFront();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileDialog.FileName;
                guna2TextBox4.Text = filePath;
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private async void guna2Button12_Click(object sender, EventArgs e)
        {
            string filePath = guna2TextBox4.Text;

            if (File.Exists(filePath))
            {
                await Task.Run(() => EngineCL.BLOCK_FIREWALL_APP(filePath));
            }

        }

        private async void guna2Button13_Click(object sender, EventArgs e)
        {
            string filePath = guna2TextBox4.Text;

            if (File.Exists(filePath))
            {
                await Task.Run(() => EngineCL.UNBLOCK_FIREWALL_APP(filePath));
            }
        }

        private void guna2HtmlLabel3_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2CircleProgressBar1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }



        private async void index_Load(object sender, EventArgs e)
        {
            try
            {

                SetCPR();

                KeyAuthApp.init();


                EnableDarkMode();

                CheckNetwork.CheckConnection();

                
                // Set User Remembre
                StartUPLoad.CheckUser(guna2TextBox1);



                LoadLabelFont();

                logger.Info("Started M-Bytes");
                logger.Info("Starting Core.EngineCl ...");
                await Task.Delay(2000);
                logger.Info($"{nameof(index_Load)} Started");
                logger.Info("Core.EngineCL Started [+]");

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }



        private void Blocker_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
