using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncIO.http
{
    public class Server
    {
        readonly HttpListener _httpListener;

        const string RESPONSE_TEMPLATE = "{0}";

        public Server(int portNumber)
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(string.Format("http://+:{0}/", portNumber));
        }

        public async Task Start()
        {
            _httpListener.Start();
            while (true)
            {
                var ctx = await _httpListener.GetContextAsync();
                String response = string.Format(RESPONSE_TEMPLATE, DateTime.Now);
                using(var sw = new StreamWriter(ctx.Response.OutputStream))
                {
                    await sw.WriteAsync(response);
                    await sw.FlushAsync();
                }
            }
        }

        public async Task Stop()
        {
             _httpListener.Abort();
        }
    }
}
