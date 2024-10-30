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

namespace M_Bytes
{
    public partial class index : Form
    {

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

                ApplyFontToAllLabels();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                logger.Info("Started M-Bytes");


                CheckNetwork.CheckConnection();
                //KeyAuthApp.init();

                // Set User Remembre
                StartUPLoad.CheckUser(guna2TextBox1);


                EnableDarkMode();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
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
                guna2HtmlLabel3.Font = customFont; 
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

        private void ApplyFontToAllLabels()
        {
            if (customFonts.Families.Length > 0)
            {
                Font customFont = new Font(customFonts.Families[0], 12, FontStyle.Regular);

                foreach (Control control in this.Controls)
                {
                    if (control is Label label)
                    {
                        label.Font = customFont;
                    }
                }
            }
            else
            {
                MessageBox.Show("Failed to load the custom font.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                user = guna2TextBox1.Text;
                password = guna2TextBox2.Text;

                await Task.Run(() => KeyAuthApp.login(user, password));

                if (KeyAuthApp.response.success)
                {
                    MSG.SH("Authentication\nSuccess", "redirect to panel ...");

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
            catch (Exception ex) {

                MSG.SH("Failed !", $"{ex.Message}");
            }
            
        }


        public static void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
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

        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            
            //pictureBox4.Visible = true;
            /// This if Done !!
            /// 


             
        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private async void guna2Button8_Click(object sender, EventArgs e)
        {
            try
            {
                circlebar.Visible = true;

                if (guna2CheckBox3.Checked)
                {
                    Task Cleanup = Task.Run(async () =>
                    {
                        await EngineCL.CleanTemp();
                        await EngineCL.RemoveStartUpFiles();
                        await EngineCL.CleanPerfetch();
                    });

                    MSG.SH("Done !", "Task Completed ...");
                    await Cleanup;
                }
                else if (!guna2CheckBox3.Checked)
                {
                    Task Cleanup = Task.Run(async () =>
                    {
                        await EngineCL.CleanPerfetch();
                        await EngineCL.CleanTemp();
                    });

                    MSG.SH("Done !", "Task Completed ...");
                    await Cleanup;
                }
            }
            finally
            {
                circlebar.Visible = false;
            }
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
                    await Task.Run(() => EngineCL.DELETE_ALL_DEBLUCATE_FILES());
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

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            auth.BringToFront();
        }
    }
}
