using System;
using System.IO;
using System.Text.Json;
using Guna.UI2.WinForms;

namespace M_Bytes.Core.EngineCL
{
    internal class StartUPLoad
    {
        /// <summary>
        /// Form Load Functions
        /// </summary>
        /// 

        public static void CreateFileIM()
        {
            string Current = Environment.CurrentDirectory;

            string path = Path.Combine(Current, "Settings");

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CheckUser(Guna2TextBox textBox) 
        {
            string currentDir = Environment.CurrentDirectory;
            string filePath = Path.Combine(currentDir, "user_data", "user.json");

            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);

                var userData = JsonSerializer.Deserialize<UserData>(jsonString);

                textBox.Text = userData.User;
            }
            else
            {
                textBox.Text = ""; 
            }
        }
    }

    public class UserData
    {
        public string User { get; set; }
    }
}
