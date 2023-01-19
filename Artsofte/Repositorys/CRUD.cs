using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Artsofte.Core
{
    public static class CRUD
    {
        public static HttpClient httpClient = new HttpClient();
        public static string Host = "https://localhost:7209/api/";

        public static void Create(string url, string json)
        {
            Answer(httpClient.PostAsync($"{Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
        }
        public static string Read(string url)
        {
            return httpClient.GetAsync($"{Host}{url}").Result.Content.ReadAsStringAsync().Result;
        }
        [HttpPut]
        public static void Update(string url, string json)
        {
            Answer(httpClient.PutAsync($"{Host}{url}", new StringContent(json, Encoding.UTF8, "application/json")).Result.ToString());
            Console.WriteLine($"Current Url = {Host}{url}");
        }
        public static void Delete(string url)
        {
            Answer(httpClient.DeleteAsync($"{Host}{url}").Result.Content.ReadAsStringAsync().Result);
        }
        private static void Answer(string answer)
        {
            if (!answer.Contains("200")) Console.WriteLine(answer);
        }
    }
}
