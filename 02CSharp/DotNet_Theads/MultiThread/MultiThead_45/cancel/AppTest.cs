using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThead_45.cancel
{
    public class AppTest
    {

        private static int TaskMethod(string name,int seconds,CancellationToken token)
        {
            Console.WriteLine("{0} {1} {2}", name,
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            for (int i = 0; i < seconds; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                if (token.IsCancellationRequested)
                {
                    return -1;
                }
            }
            return 42 * seconds;
        }

        public static void Run()
        {
            var cts = new CancellationTokenSource();
            var longTask = new Task<int>(() => TaskMethod("Task1", 10, cts.Token),cts.Token);
            Console.WriteLine(longTask.Status);

            cts.Cancel();
            Console.WriteLine(longTask.Status);

            cts = new CancellationTokenSource();
            longTask = new Task<int>(() => TaskMethod("Task2", 10, cts.Token), cts.Token);
            longTask.Start();
            int length = 5;
            for (int i = 0; i < length; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
            cts.Cancel();
            for (int i = 0; i < length; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
            Console.WriteLine("结果：{0}", longTask.Result);

        }

    }
}
