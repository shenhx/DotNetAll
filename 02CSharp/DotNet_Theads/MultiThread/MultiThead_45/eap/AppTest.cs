using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThead_45.eap
{
    public class AppTest
    {
        static int TaskMethod(string name,int seconds)
        {
            Console.WriteLine("{0} {1} {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            return 42 * seconds;
        }

        public static void Run()
        {
            var tcs = new TaskCompletionSource<int>();

            var worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += (sender,eventArgs) => {
                if(eventArgs.Error != null)
                {
                    tcs.SetException(eventArgs.Error);
                }
                else if (eventArgs.Cancelled)
                {
                    tcs.SetCanceled();
                }
                else
                {
                    tcs.SetResult((int)eventArgs.Result);
                }
            };
        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = TaskMethod("background worker", 5);
        }
       
    }
}
