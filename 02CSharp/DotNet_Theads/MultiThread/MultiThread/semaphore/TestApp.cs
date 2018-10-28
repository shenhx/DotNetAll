using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.semaphore
{
    public class TestApp
    {
        static SemaphoreSlim _semaphore = new SemaphoreSlim(4);

        static void AccessDataBase(string name, int seconds)
        {
            Console.WriteLine("{0}等待访问数据库", name);
            _semaphore.Wait();
            Console.WriteLine("{0}已经允许访问数据库", name);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("{0}已经完成！", name);
            _semaphore.Release();
        }
        
        public static void Run()
        {
            int length = 6;
            for (int i = 0; i < length; i++)
            {
                string threadName = "Thread" + i;
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(() => AccessDataBase(threadName, secondsToWait));
                t.Start();
            }
        }
    }
}
