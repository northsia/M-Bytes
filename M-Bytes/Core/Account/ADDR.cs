using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace M_Bytes.Core.Account
{
    internal class ADDR
    {
        public static async Task<string> GetPublicIpAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.ipify.org");
                    response.EnsureSuccessStatusCode();

                    string publicIp = await response.Content.ReadAsStringAsync();
                    return publicIp;
                }
                catch
                {
                    return null;
                }
            }
        }
    }

}
