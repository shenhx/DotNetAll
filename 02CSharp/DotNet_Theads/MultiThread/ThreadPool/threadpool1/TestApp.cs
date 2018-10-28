using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ThreadPoolTest.threadpool1
{
    public class TestApp
    {
        static void UseThreads(int numberOfOperations)
        {
            using (var countdown = new CountdownEvent(numberOfOperations))
            {
                int length = numberOfOperations;
                for (int i = 0; i < length; i++)
                {
                    var thread = new Thread(() => {
                        Console.Write(Thread.CurrentThread.ManagedThreadId);
                        Console.Write(" ");
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                        countdown.Signal();
                    });
                    thread.Start();
                }
                countdown.Wait();
                Console.WriteLine();
            }
        }

        static void UseThreadPool(int numberOfOperations)
        {
            using (var countdown = new CountdownEvent(numberOfOperations))
            {
                int length = numberOfOperations;
                for (int i = 0; i < length; i++)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem((obj) =>
                    {
                        Console.Write(Thread.CurrentThread.ManagedThreadId);
                        Console.Write(" ");
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                        countdown.Signal();
                    });
                }
                countdown.Wait();
                Console.WriteLine();
            }
        }

        public static void Run()
        {
            const int numberOfOperations = 500;
            var sw = new Stopwatch();
            sw.Start();
            UseThreads(numberOfOperations);
            sw.Stop();
            Console.WriteLine("耗时：{0}毫秒", sw.ElapsedMilliseconds);
            sw.Reset();
            sw.Start();
            UseThreadPool(numberOfOperations);
            sw.Stop();
            Console.WriteLine("耗时：{0}毫秒", sw.ElapsedMilliseconds);
        }
    }
}
