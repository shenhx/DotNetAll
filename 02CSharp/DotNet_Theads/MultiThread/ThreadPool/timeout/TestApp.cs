using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadPoolTest.timeout
{
    public class TestApp
    {
        static void RunOperations(TimeSpan workerOperationTimeout)
        {
            using (var evt = new ManualResetEvent(false))
            {
                using (var cts = new CancellationTokenSource())
                {
                    var worker = ThreadPool.RegisterWaitForSingleObject(evt, (state, isTImedOut) => WorkerOperationWait(cts,isTImedOut),null,workerOperationTimeout,true);
                    Console.WriteLine("starting long running operation...");
                    ThreadPool.QueueUserWorkItem(_ => workerOperation(cts.Token, evt));
                    Thread.Sleep(workerOperationTimeout.Add(TimeSpan.FromSeconds(2)));
                    worker.Unregister(evt);
                }
            }
        }

        private static void workerOperation(CancellationToken cancellationToken, ManualResetEvent evt)
        {
            int length = 6;
            for (int i = 0; i < length; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            evt.Set();
        }

        private static void WorkerOperationWait(CancellationTokenSource cts, bool isTImedOut)
        {
            if (isTImedOut)
            {
                cts.Cancel();
                Console.WriteLine("worker operation timed out and was canceled.");
            }
            else
            {
                Console.WriteLine("worker operation works successfully");
            }

        }

        public static void Run()
        {
            RunOperations(TimeSpan.FromSeconds(5));
            RunOperations(TimeSpan.FromSeconds(8));
        }
    }
}
