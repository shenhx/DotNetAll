using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.countdown
{
    public class TestApp
    {
        static CountdownEvent _countdown = new CountdownEvent(2);

        static void PreformOperation(string message, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine(message);
            _countdown.Signal();
        }

        public static void Run()
        {
            Console.WriteLine("开始");
            var t1 = new Thread(() => PreformOperation("方法1 已经完成",4));
            var t2 = new Thread(() => PreformOperation("方法2 已经完成",8));
            t1.Start();
            t2.Start();
            _countdown.Wait();
            Console.WriteLine("已经完成");
            _countdown.Dispose();

        }
    }
}
