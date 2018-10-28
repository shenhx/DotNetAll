using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.manualreset
{
    public class TestApp
    {
        static ManualResetEventSlim _mainEvent = new ManualResetEventSlim(false);

        static void TravelThroughGates(string threadName, int seconds)
        {
            Console.WriteLine("等待：{0}", threadName);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            _mainEvent.Wait();
            Console.WriteLine("开始办理{0}的业务",threadName);
            Console.WriteLine("{0}办理完，已经离开", threadName);
        }

        public static void Run()
        {
            var t1 = new Thread(() => TravelThroughGates("t1",5));
            var t2 = new Thread(() => TravelThroughGates("t2", 6));
            var t3 = new Thread(() => TravelThroughGates("t3", 12));
            t1.Start();
            t2.Start();
            t3.Start();
            Thread.Sleep(TimeSpan.FromSeconds(6));
            Console.WriteLine("银行开门");
            _mainEvent.Set();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            _mainEvent.Reset();
            Console.WriteLine("银行关门");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine("银行重新开门");
            _mainEvent.Set();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("银行下班");
            _mainEvent.Reset();
        }

    }
}
