using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_Bytes.Core.Firebase_data
{
    internal class FIREBASE_DATA_API
    {
        private const string DatabaseURL = "https://m-bytes-default-rtdb.europe-west1.firebasedatabase.app/";
        private const string Secret = "w6Y5GwSBJ8n5ciXVH3eY9plKOd9kQC2uJghSOB7H";
        private static readonly HttpClient httpClient = new HttpClient();


        private static async Task AddDataToFirebase(string url, string jsonData)
        {
            try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // OK
                }
            }
            catch (Exception)
            {
            }
        }


        public static async Task<string> SEND_GET_DATA_(string path)
        {
            try
            {
                var url = $"{DatabaseURL}{path}.json?auth={Secret}"; 
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
