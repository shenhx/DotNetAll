using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThead_45.composite
{
    public class AppTest
    {
        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("Task {0} is running on a threa id {1}.Is Thread Pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId,
                                      Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return 42 * seconds;
        }

        public static void Run()
        {
            var firstTask = new Task<int>(() => TaskMethod("First Task",3));
            var secondTask = new Task<int>(() => TaskMethod("Second Task", 2));
            firstTask.ContinueWith(t => Console.WriteLine("Task {0} is running on a threa id {1}.Is Thread Pool thread:{2}", t.Result, Thread.CurrentThread.ManagedThreadId,
                                      Thread.CurrentThread.IsThreadPoolThread), TaskContinuationOptions.NotOnRanToCompletion);

            firstTask.Start();
            secondTask.Start();
            Thread.Sleep(TimeSpan.FromSeconds(4));
            Task continuation = secondTask.ContinueWith(t => Console.WriteLine("Task {0} is running on a threa id {1}.Is Thread Pool thread:{2}", t.Result, Thread.CurrentThread.ManagedThreadId,
                                      Thread.CurrentThread.IsThreadPoolThread), TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);

            continuation.GetAwaiter().OnCompleted(() => Console.WriteLine("Task continuation is completed on a threa id {0}.Is Thread Pool thread:{1}", Thread.CurrentThread.ManagedThreadId,
                                      Thread.CurrentThread.IsThreadPoolThread));

            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine();

            firstTask = new Task<int>(() => {
                var innerTask = Task.Factory.StartNew(() => TaskMethod("Second Task",5),TaskCreationOptions.AttachedToParent);
                innerTask.ContinueWith(t => TaskMethod("Third Task", 2), TaskContinuationOptions.AttachedToParent);
                return TaskMethod("First Task", 2);
            });

            firstTask.Start();

            while (!firstTask.IsCompleted)
            {
                Console.WriteLine(firstTask.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(firstTask.Status);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            
        }
    }
}
