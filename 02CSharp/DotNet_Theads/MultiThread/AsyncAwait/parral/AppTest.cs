using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.parral
{
    public class AppTest
    {

        async static Task AsynchronousProcessing()
        {
            Task<string> t1 = GetInfoAsync("Task1", 3);
            Task<string> t2 = GetInfoAsync("Task2", 3);
            string[] results = await Task.WhenAll(t1, t2);
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        async static Task<string> GetInfoAsync(string name, int seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            return string.Format("{0} {1} {2}",name,Thread.CurrentThread.ManagedThreadId,Thread.CurrentThread.IsThreadPoolThread);
        }

        public static void Run()
        {
            Task t = AsynchronousProcessing();
            t.Wait();
        }
    }
}
