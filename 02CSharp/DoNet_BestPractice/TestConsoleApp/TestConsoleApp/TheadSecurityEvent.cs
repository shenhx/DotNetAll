using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestConsoleApp
{
    public class ResponseArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public class TheadSecurityEvent : ITest
    {
        public event EventHandler<ResponseArgs> SayHelloEvent;

#if DotNet_45
        public void Request(string msg) => Volatile.Read(ref SayHello)?.Invoke(this, new ResponseArgs { Message = msg });
#else
        public void Request(string msg)
        {
            EventHandler<ResponseArgs> tmpEvent = SayHelloEvent;
            if(tmpEvent != null)
            {
                tmpEvent(this, new ResponseArgs { Message = "hello,"+msg });
            }
        }
#endif


        /// <summary>
        /// 测试
        /// </summary>
        public void Test()
        {
            this.SayHelloEvent += TheadSecurityEvent_SayHelloEvent;
            this.Request("world");
        }

        private static void TheadSecurityEvent_SayHelloEvent(object sender, ResponseArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
