using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThead_45.task
{
    public class TestApp
    {

        static void TaskMethod(string name)
        {
            Console.WriteLine("Task {0} 运行在ID为{1}的进程（{2}线程池线程）",name,Thread.CurrentThread.ManagedThreadId,Thread.CurrentThread.IsThreadPoolThread?"":"不是");
        }

        public static void Run()
        {
            var t1 = new Task(() =>  TaskMethod("t1"));
            var t2 = new Task(() => TaskMethod("t2"));
            t2.Start();
            t1.Start();
            Task.Run(() => TaskMethod("t3"));
            Task.Factory.StartNew(() => TaskMethod("t4"));
            Task.Factory.StartNew(() => TaskMethod("t5"), TaskCreationOptions.LongRunning);
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

    }
}
