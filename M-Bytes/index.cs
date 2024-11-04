using Guna.UI2.WinForms;
using KeyAuth;
using M_Bytes.Core.Account;
using M_Bytes.Core.EngineCL;
using M_Bytes.Core.Messages;
using Microsoft.Win32.SafeHandles;
using System;
using System.Text.Json;
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
using dotenv.net;
using Microsoft.Win32;
using System.Diagnostics;
using M_Bytes.Core.Sound;

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


        private static readonly string name;

        
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

                await Task.Delay(1000);

                user = guna2TextBox1.Text;
                password = guna2TextBox2.Text;

                await Task.Run(() => KeyAuthApp.login(user, password));

                if (KeyAuthApp.response.success)
                {

                    ProtecterEngine.SESSION_LGN = 1;

                    MSG.SH("Authentication\nSuccess !", "redirect to panel ...", guna2ToggleSwitch1);

                    user_data.Save(KeyAuthApp.user_data.username.ToString());

                    // set UserName Profile

                    label1.Text = KeyAuthApp.user_data.username.ToString();

                    Text = "M-Bytes | Home";

                    main.BringToFront();
                }
                else
                {
                    ProtecterEngine.SESSION_LGN = 0;

                    string message = KeyAuthApp.response.message;

                    MSG.SH("Authentication\nFailed !", message, guna2ToggleSwitch1);
                }
            }
            catch (Exception ex)
            {
                ProtecterEngine.SESSION_LGN = 0;

                MSG.SH("Failed !", $"{ex.Message}", guna2ToggleSwitch1);
            }
            finally
            {
                guna2CircleProgressBar1.Hide();
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

            ProtecterEngine.PROTECTER_ENGINE_PROTECT_();

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
            ProtecterEngine.PROTECTER_ENGINE_PROTECT_();
            FullCL.BringToFront();

        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private async void guna2Button8_Click(object sender, EventArgs e) // FULL CL
        {
            try
            {
                ProtecterEngine.PROTECTER_ENGINE_PROTECT_();

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
                await Task.Delay(5000);

                MSG.SH("Done !", "Cleaning Seccess !", guna2ToggleSwitch1);
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
            ProtecterEngine.PROTECTER_ENGINE_PROTECT_();
            Duplicate.BringToFront();
        }

        private async void guna2Button9_Click(object sender, EventArgs e)
        {
            try
            {
                ProtecterEngine.PROTECTER_ENGINE_PROTECT_();

                circlebar.Show();

                if (guna2CheckBox2.Checked && !string.IsNullOrWhiteSpace(guna2TextBox3.Text))
                {
                    await Task.Run(() => EngineCL.DeleteDuplicateFilesAsync(guna2TextBox3.Text));
                }
                else if (!guna2CheckBox2.Checked)
                {
                    MSG.SH("Nope","You Must Check the Box !", guna2ToggleSwitch1);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                await Task.Delay(5000);

                MSG.SH("Done !", "Done Remove Duplicate ...", guna2ToggleSwitch1);

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
            ProtecterEngine.PROTECTER_ENGINE_PROTECT_();

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
            this.Text = "M-Bytes";

            circlebar.Show();

            await Task.Delay(600);


            auth.BringToFront();

            circlebar.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            ProtecterEngine.PROTECTER_ENGINE_PROTECT_();
            BrowserCL.BringToFront();
        }

        private async void guna2Button11_Click(object sender, EventArgs e)
        {
            try
            {
                ProtecterEngine.PROTECTER_ENGINE_PROTECT_();

                circlebar.Visible = true;

                if (guna2CheckBox3.Checked)
                {
                    Task Cleanup = Task.Run(async () =>
                    {
                        await EngineCL.CleanAllBrowserCaches();
                    });

                    await Cleanup;
                }
                else if (!guna2CheckBox3.Checked)
                {
                    Task Cleanup = Task.Run(async () =>
                    {
                        await EngineCL.CleanAllBrowserCaches();

                    });

                    await Cleanup;
                }
            }
            finally
            {
                await Task.Delay(5000);

                MSG.SH("Done !", "Browser Cleaned !", guna2ToggleSwitch1);
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
            ProtecterEngine.PROTECTER_ENGINE_PROTECT_();
            Blocker.BringToFront();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {

            ProtecterEngine.PROTECTER_ENGINE_PROTECT_();
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
            try
            {


                ProtecterEngine.PROTECTER_ENGINE_PROTECT_();

                circlebar.Show();
                
                string filePath = guna2TextBox4.Text;

                MessageBox.Show(filePath);

                if (File.Exists(filePath))
                {
                    await Task.Run(() => EngineCL.BLOCK_FIREWALL_APP(filePath));
                }
            }
            catch (Exception ex)
            {
                MSG.SH("Error",ex.Message, guna2ToggleSwitch1);
            }
            finally
            {

                await Task.Delay(5000);
                
                circlebar.Hide();

            }


        }

        private async void guna2Button13_Click(object sender, EventArgs e)
        {
            try
            {
                ProtecterEngine.PROTECTER_ENGINE_PROTECT_();

                circlebar.Show();

                string filePath = guna2TextBox4.Text;

                if (File.Exists(filePath))
                {
                    await Task.Run(() => EngineCL.UNBLOCK_FIREWALL_APP(filePath));
                }
            }
            finally
            {
                await Task.Delay(5000);

                circlebar.Hide();
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

        private void LoadCustomFont()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();

            string resource = "M-Bytes.Gotham.otf";

            using (Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                if (fontStream != null)
                {
                    byte[] fontData = new byte[fontStream.Length];
                    fontStream.Read(fontData, 0, fontData.Length);

                    IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
                    Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                    pfc.AddMemoryFont(fontPtr, fontData.Length);
                    Marshal.FreeCoTaskMem(fontPtr);
                }
            }

            foreach (Control control in this.Controls)
            {
                control.Font = new Font(pfc.Families[0], control.Font.Size);
            }
        }

        private async void index_Load(object sender, EventArgs e)
        {
            try
            {
                ProtecterEngine.PROTECTER_ENGINE_PROTECT_ONLOAD_();

                CheckNetwork.CheckConnection();

                LoadTheLastRecoveryDate();

                CheckIfNotificationsEnabled();

                SetCPR();

                KeyAuthApp.init();


                EnableDarkMode();

                
                // Set User Remembre
                StartUPLoad.CheckUser(guna2TextBox1);

                LoadCustomFont();

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


        private void LoadTheLastRecoveryDate()
        {
            try
            {
                // Define the path to the JSON file
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_data", "last_Recovery_date.json");

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read the JSON data from the file
                    string jsonData = File.ReadAllText(filePath);

                    // Deserialize the JSON data to an anonymous object
                    var recoveryData = JsonSerializer.Deserialize<RecoveryData>(jsonData);

                    if (recoveryData != null)
                    {
                        // Access the properties from the recovery data
                        string description = recoveryData.Description;
                        string date = recoveryData.Date;


                        guna2TextBox6.Text = date;

                        // Log or display the recovery data as needed
                        logger.Info($"Last Recovery Description: {description}");
                        logger.Info($"Last Recovery Date: {date}");
                    }
                }
                else
                {
                    logger.Warn("ENGINE.CL: last_Recovery_date.json file does not exist.");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"ENGINE.CL: Error loading the last recovery date. {ex.Message}");
            }
        }

        // Define a class to match the structure of the JSON data
        public class RecoveryData
        {
            public string Description { get; set; }
            public string Date { get; set; }
        }


        private async void guna2Button15_Click(object sender, EventArgs e) // Recovery
        {
            circlebar.Show();

            ProtecterEngine.PROTECTER_ENGINE_PROTECT_();

            string discrp = guna2TextBox5.Text; 
            string recovery_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                logger.Info($"ENGINE.CL: Recovery Point Creation Started at {recovery_date}");

                // Define the path and JSON content
                string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_data");
                string filePath = Path.Combine(directoryPath, "last_Recovery_date.json");

                // Ensure directory exists
                Directory.CreateDirectory(directoryPath);

                // Create the recovery data as an object
                var recoveryData = new
                {
                    Description = discrp,
                    Date = recovery_date
                };

                // Serialize to JSON and write to file
                string jsonData = JsonSerializer.Serialize(recoveryData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);

                // Create the recovery point
                await Task.Run(() => EngineCL.CREAT_RECOVERY_POINT(discrp));

                logger.Info("ENGINE.CL: Recovery Point Created Successfully");
            }
            catch (Exception ex)
            {
                logger.Error($"ENGINE.CL: Recovery Point Creation Failed. Error: {ex.Message}");
            }
            finally
            {
                await Task.Delay(5000);

                circlebar.Hide();
            }
        }

        private void recovery_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            ProtecterEngine.PROTECTER_ENGINE_PROTECT_();
            recovery.BringToFront();
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            settings.BringToFront();
        }

        private void guna2Button17_Click(object sender, EventArgs e)
        {
            EngineCL.OpenUrl("https://meed-mh.online");
        }

        private void guna2CustomRadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void guna2ToggleSwitch1_CheckedChanged_1(object sender, EventArgs e)
        {
            Player.PlayClickSound(guna2ToggleSwitch1);


            var path = Path.Combine(Directory.GetCurrentDirectory(), "settings", "settings.json");


            if (guna2ToggleSwitch1.Checked)
            {
                var settings = new Dictionary<string, bool>
                {
                    { "NotificationsEnabled", true },
                };


                CreateJsonData(path, settings); 
            }
            else
            {
                var settings = new Dictionary<string, bool>
                {
                    { "NotificationsEnabled", false },
                };


                CreateJsonData(path, settings);
            }





            
        }

        public static void CreateJsonData<T>(string filePath, T data)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating JSON data: {ex.Message}");
            }
        }


            private void CheckIfNotificationsEnabled()
            {
            string settingsDirectory = "settings"; 
            string settingsFilePath = Path.Combine(settingsDirectory, "settings.json"); // Path to the settings.json file

            if (File.Exists(settingsFilePath))
            {
                string json = File.ReadAllText(settingsFilePath);

                var settings = JsonSerializer.Deserialize<Dictionary<string, bool>>(json);

                if (settings != null && settings.TryGetValue("NotificationsEnabled", out bool isEnabled))
                {
                    guna2ToggleSwitch1.Checked = isEnabled;
                }
            }
            else
            {
                guna2ToggleSwitch1.Checked = false;
            }
        }



        private void option_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
