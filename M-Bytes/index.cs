using KeyAuth;
using M_Bytes.Core.Account;
using M_Bytes.Core.Messages;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_Bytes
{
    public partial class index : Form
    {

        string user;
        string password;


        public static api KeyAuthApp = new api(
            name: "M-Bytes", // App name - VaultCord.com FREE Discord backup bot for members & your entire server saved from terms and nukes!
            ownerid: "4Mgrdto854", // Account ID
            version: "1.0" // Application version. Used for automatic downloads see video here https://www.youtube.com/watch?v=kW195PLCBKs
        );


        public index()
        {
            InitializeComponent();
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
            KeyAuthApp.init();
            EnableDarkMode();


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

                // Set Token 

                Token.SetToken();


                user = guna2TextBox1.Text;
                password = guna2TextBox2.Text;

                await Task.Run(() => KeyAuthApp.login(user, password));

                if (KeyAuthApp.response.success)
                {
                    MSG.SH("Authentication\nSuccess !", "redirect to panel ...");

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


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
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
    }
}
