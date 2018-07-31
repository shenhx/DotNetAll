using ConsoleApplication4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(YzjAuth1.getOauthToken("ABC123456", "helloworld"));

            HttpClient client = new HttpClient();
            var reqJson = "{\"appId\": \"xxxxxx\",\"secret\": \"轻应用secret\",\"timestamp\": 1522305194157,\"scope\": \"app\"}";
            ByteArrayContent content = new ByteArrayContent(System.Text.Encoding.Default.GetBytes (reqJson));
            var respMsg = client.PostAsync("https://www.yunzhijia.com/gateway/oauth2/token/getAccessToken", content);
            HttpResponseMessage mess = respMsg.Result;
            Task<string> msgBody = mess.Content.ReadAsStringAsync();
            Console.WriteLine(msgBody.Result);

            //Dictionary<string, string> keyValues = new Dictionary<string, string>();
            //keyValues["userName"] = "admin"; keyValues["password"] = "123";
            //FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
            //var respMsg = client.PostAsync("https://www.yunzhijia.com/gateway/oauth2/token/getAccessToken", content);
            //// 不要错误的调用 了 PutAsync，应该是 PostAsync 
            //HttpResponseMessage mess = respMsg.Result;
            //Task<string> msgBody = mess.Content.ReadAsStringAsync();
            
            Console.ReadKey();
        }
    }
}
