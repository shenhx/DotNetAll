using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThead_45.exception
{
    public class AppTest
    {

        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("{0} {1} {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            throw new Exception("Boom!");
            return 42 * seconds;
        }

        public static void Run()
        {
            Task<int> task;
            try
            {
                task = Task.Run(() => TaskMethod("Task1", 2));
                int result = task.Result;
                Console.WriteLine("Result:{0}",result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught:{0}", ex);
            }
            Console.WriteLine("----------------");
            Console.WriteLine();

            try
            {
                task = Task.Run(() => TaskMethod("Task2", 2));
                int result = task.GetAwaiter().GetResult();
                Console.WriteLine("Result:{0}",result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex);
            }
            Console.WriteLine("--------------");
            Console.WriteLine();

            var t1 = new Task<int>(() => TaskMethod("Task3", 3));
            var t2 = new Task<int>(() => TaskMethod("Task4", 4));
            var complexTask = Task.WhenAll(t1, t2);
            var exceptionHandler = complexTask.ContinueWith(t => Console.WriteLine("{0}", t.Exception),TaskContinuationOptions.OnlyOnFaulted);
            t1.Start();
            t2.Start();

            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

    }
}
