using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThead_45.apm
{
    public class AppTest
    {
        private delegate string AsynchronousTask(string threadName);
        private delegate string IncompatibleAsyncronousTask(out int threadId);

        private static void Callback(IAsyncResult ar)
        {
            Console.WriteLine(ar.AsyncState);
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        private static string Test(string threadName)
        {
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Thread.CurrentThread.Name = threadName;
            return string.Format("thread name:{0}",Thread.CurrentThread.Name);
        }

        private static string Test(out int threadId)
        {
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            threadId = Thread.CurrentThread.ManagedThreadId;
            return string.Format("thread id:{0}", threadId);
        }

        public static void Run()
        {
            int threadId = 0;
            AsynchronousTask d = Test;
            IncompatibleAsyncronousTask e = Test;
            // Option1
            Task<string> task = Task<string>.Factory.FromAsync(d.BeginInvoke("AsyncTaskThread",Callback,"a delegate asynchronous call"),d.EndInvoke);
            task.ContinueWith(t => Console.WriteLine("callback is finished,now running a continuation!result:{0}", t.Result));

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(task.Status);
            Console.WriteLine();

            // Option2
            task = Task<string>.Factory.FromAsync(d.BeginInvoke, d.EndInvoke,"AsyncTaskThread", "a delegate asynchronous call");
            task.ContinueWith(t => Console.WriteLine("callback is finished,now running a continuation!result:{0}", t.Result));

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(task.Status);
            Console.WriteLine();

            // Option3
            IAsyncResult ar = e.BeginInvoke(out threadId, Callback, "a delegate asynchronous call");
            ar = e.BeginInvoke(out threadId, Callback, "a delegate asynchronous call");
            task = Task<string>.Factory.FromAsync(ar, _ => e.EndInvoke(out threadId, ar));
            task.ContinueWith(t => Console.WriteLine("callback is finished,now running a continuation!result:{0}", t.Result));

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(task.Status);
            Console.WriteLine();

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

    }
}
