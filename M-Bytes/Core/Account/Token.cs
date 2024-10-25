using KeyAuth;
using System;
using System.IO;

namespace M_Bytes.Core.Account
{
    internal class Token
    {
        public static void SetToken()
        {
            if (index.KeyAuthApp.response.success)
            {
                string token = index.KeyAuthApp.user_data.hwid;

                if (!string.IsNullOrEmpty(token))
                {
                    File.WriteAllText("rES.txt", token);
                }
            }
            else
            {
                Console.WriteLine("Authentication failed. No token available.");
            }
        }
    }
}
