using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncIO.http
{
    public class AppTest
    {
        public static void Run()
        {
            var server = new Server(portNumber: 1234);
            var t = Task.Run(() => server.Start());
            Console.WriteLine("start...");
            Console.WriteLine();

            Client.GetResponseAsync("http://localhost:1234").GetAwaiter().GetResult();
            server.Stop().GetAwaiter().GetResult();
        }
    }
}
