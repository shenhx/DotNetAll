using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.lambda
{
    public class AppTest
    {
        async static Task AsynchronousProcessing()
        {
            Func<string, Task<string>> asyncLambda = async name =>
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                return string.Format("{0} {1} {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            };

            string result = await asyncLambda("async lambda");
            Console.WriteLine(result);
        }

        public static void Run()
        {
            Task t = AsynchronousProcessing();
            t.Wait();
        }

    }
}
