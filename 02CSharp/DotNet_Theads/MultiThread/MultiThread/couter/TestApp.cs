using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.couter
{
    public class TestApp
    {
        static void TestCounter(CouterBase c)
        {
            int length = 1000000;
            for (int i = 0; i < length; i++)
            {
                c.Increment();
                c.Descrement();
            }
        }

        public static void Run(string[] args)
        {
            Console.WriteLine("Incorrect counter");
            var c = new UnSafeCouter();
            var t1 = new Thread(() => TestCounter(c));
            var t2 = new Thread(() => TestCounter(c));
            var t3 = new Thread(() => TestCounter(c));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            Console.WriteLine(c.Count);

            var c1 = new CounterNoLock();
            t1 = new Thread(() => TestCounter(c1));
            t2 = new Thread(() => TestCounter(c1));
            t3 = new Thread(() => TestCounter(c1));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            Console.WriteLine(c1.Count);
        }
    }

}
