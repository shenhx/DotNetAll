using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolTest.canceltoken
{
    public class TestApp
    {
        static void AsyncOperation1(CancellationToken token)
        {
            int length = 5;
            for (int i = 0; i < length; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("the first task has been canceled.");
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine("the first task has completed successful");
        }

        static void AsyncOperation2(CancellationToken token)
        {
            try
            {
                int length = 5;
                for (int i = 0; i < length; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                Console.WriteLine("the first task has completed successful");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("the second task has been canceled.");
            }
        }

        static void AsyncOperation3(CancellationToken token)
        {
            bool cancellationFlag = false;
            token.Register(() => cancellationFlag = true);
            int length = 5;
            for (int i = 0; i < length; i++)
            {
                if (cancellationFlag)
                {
                    Console.WriteLine("the third task has been canceled.");
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine("the first task has completed successful");
        }

        public static void Run()
        {
            using (var cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(_ => AsyncOperation1(token));
                Thread.Sleep(TimeSpan.FromSeconds(2));
                cts.Cancel();
            }
            using (var cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(_ => AsyncOperation2(token));
                Thread.Sleep(TimeSpan.FromSeconds(2));
                cts.Cancel();
            }
            using (var cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(_ => AsyncOperation3(token));
                Thread.Sleep(TimeSpan.FromSeconds(2));
                cts.Cancel();
            }

        }
    }
}
