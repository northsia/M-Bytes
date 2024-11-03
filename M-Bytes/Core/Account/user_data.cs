using M_Bytes.Core.Messages;
using System;
using System.IO;
using System.Text.Json;

namespace M_Bytes.Core.Account
{
    internal class user_data
    {
        public static void Save(string user)
        {
            try
            {
                string current_dir = Directory.GetCurrentDirectory();

                string dir = current_dir + "/user_data";
                string filePath = Path.Combine(dir, "user.json");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var userData = new { User = user };
                string jsonString = JsonSerializer.Serialize(userData);

                File.WriteAllText(filePath, jsonString);
            }
            catch
            {
                
            }
        }
    }
}
