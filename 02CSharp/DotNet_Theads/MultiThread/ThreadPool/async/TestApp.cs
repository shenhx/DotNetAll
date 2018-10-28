using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest.async
{
    public class TestApp
    {
        private delegate string RunOnThreadPool(out int threadId);

        private static void Callback(IAsyncResult ar)
        {
            Console.WriteLine("Callback:" + ar.AsyncState);
            Console.WriteLine("Callback:" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Callback:" + Thread.CurrentThread.IsThreadPoolThread);
        }

        private static string Test(out int threadId)
        {
            Console.WriteLine("Test:" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Test:" + Thread.CurrentThread.IsThreadPoolThread);
            threadId = Thread.CurrentThread.ManagedThreadId;
            return string.Format("Test:" + "thread pool worker thread id is {0}", threadId);
        }

        public static void Run(string[] args)
        {
            int threadId = 0;
            RunOnThreadPool poolDelegate = Test;
            var t = new Thread(() => Test(out threadId));
            t.Start();
            t.Join();
            Console.WriteLine("main:" + threadId);
            Console.WriteLine("main:" + "---------------------");
            // 异步实际是用线程池进程再运行
            IAsyncResult ar = poolDelegate.BeginInvoke(out threadId, Callback, "nema");
            ar.AsyncWaitHandle.WaitOne();
            string result = poolDelegate.EndInvoke(out threadId, ar);
            Console.WriteLine("main:" + threadId);
            Console.WriteLine("main:" + result);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.ReadKey();
        }
    }
}
