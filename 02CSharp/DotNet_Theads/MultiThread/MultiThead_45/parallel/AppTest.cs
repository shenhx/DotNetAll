using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThead_45.parallel
{
    public class AppTest
    {
        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("{0} {1} {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            return 42 * seconds;
        }

        public static void Run()
        {
            var firstTask = new Task<int>(() => TaskMethod("Task1", 3));
            var secondTask = new Task<int>(() => TaskMethod("Task2", 2));
            var whenAllTask = Task.WhenAll(firstTask, secondTask);
            whenAllTask.ContinueWith(t => Console.WriteLine("{0} {1} {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread));

            firstTask.Start();
            secondTask.Start();
            Thread.Sleep(TimeSpan.FromSeconds(4));

            var tasks = new List<Task<int>>();
            for (int i = 0; i < 4; i++)
            {
                int counter = i;
                var task = new Task<int>(() => TaskMethod(string.Format("Task {0}",counter),counter));
                tasks.Add(task);
                task.Start();
            }
            while(tasks.Count > 0)
            {
                var completedTask = Task.WhenAny(tasks).Result;
                tasks.Remove(completedTask);
                Console.WriteLine("{0}",completedTask.Result);
            }

            Thread.Sleep(TimeSpan.FromSeconds(1));

        }
    }
}
