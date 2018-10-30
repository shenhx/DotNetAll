using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.awaitable
{
    public class AppTest
    {
        async static Task AsynchronousProcessing()
        {
            var sync = new CustomAwaitable(true);
            string result = await sync;
            Console.WriteLine(result);

            var async = new CustomAwaitable(false);
            result = await async;
            Console.WriteLine(result);
        }

        public static void Run()
        {
            Task t = AsynchronousProcessing();
            t.Wait();
        }
    }

    internal class CustomAwaitable
    {
        private readonly bool _completeSynchronously;

        public CustomAwaitable(bool completeSynchronously)
        {
            this._completeSynchronously = completeSynchronously;
        }

        public CustomAwaiter GetAwaiter()
        {
            return new CustomAwaiter(_completeSynchronously);
        }
    }

    internal class CustomAwaiter : INotifyCompletion
    {
        private readonly bool _completeSynchronously;
        private string _result = "Completed synchronously";

        public CustomAwaiter(bool completeSynchronously)
        {
            _completeSynchronously = completeSynchronously;
        }

        public void OnCompleted(Action continuation)
        {
            ThreadPool.QueueUserWorkItem(state => {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                _result = GetInfo();
                if (continuation != null)
                    continuation();
            });
        }

        private string GetInfo()
        {
            return string.Format("{0} {1}",Thread.CurrentThread.ManagedThreadId,Thread.CurrentThread.IsThreadPoolThread);
        }

        public string GetResult()
        {
            return _result;
        }

        public bool IsCompleted
        {
            get
            {
                return _completeSynchronously;
            }
        }
    }
}
