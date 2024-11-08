using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace M_Bytes_installer
{
    internal class Request
    {
        public static async Task<bool> GET(string URI, string Download_path)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(URI);
                if (response.IsSuccessStatusCode)
                {
                    using (var fileStream = new FileStream(Download_path, FileMode.Create))
                    {
                        await response.Content.CopyToAsync(fileStream);
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    return false;
                }
            }
        }
    }
}