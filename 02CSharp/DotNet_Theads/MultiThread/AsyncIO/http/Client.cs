using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncIO.http
{
    public class Client
    {

        public static async Task GetResponseAsync(string url)
        {
            using(var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                string responseHeaders = responseMessage.Headers.ToString();
                string response = await responseMessage.Content.ReadAsStringAsync();

                Console.WriteLine("headers:");
                Console.WriteLine(responseHeaders);
                Console.WriteLine("body:");
                Console.WriteLine(response);
            }
        }

    }
}
