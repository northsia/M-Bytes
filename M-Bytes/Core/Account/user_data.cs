using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using M_Bytes.Core.Messages;

namespace M_Bytes.Core.Account
{
    internal class user_data
    {

        public static void Save(string user)
        {
            try
            {
                string dir = "/user_data";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string filePath = Path.Combine(dir, "user.json");

                if (!File.Exists(filePath))
                {
                    using (File.Create(filePath)) { }
                }

                var userData = new { User = user };
                string jsonString = JsonSerializer.Serialize(userData);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                MSG.SH("Error",ex.Message);
            }

        }

    }
}
