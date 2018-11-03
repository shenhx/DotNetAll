using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLinq.parallel
{
    public class AppTest
    {
        static string EmulateProcessing(string taskName)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(new Random(DateTime.Now.Millisecond).Next(250, 350)));
            Console.WriteLine(string.Format("{0} {1} {2}", taskName, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread));
            return taskName;
        }

        public static void Run()
        {
            Parallel.Invoke(() => EmulateProcessing("Task1"),
                () => EmulateProcessing("Task2"),
                () => EmulateProcessing("Task3"));
            Console.ReadLine();
            var cts = new CancellationTokenSource();
            var result = Parallel.ForEach(Enumerable.Range(1, 30),
                new ParallelOptions
                {
                    CancellationToken = cts.Token,
                    MaxDegreeOfParallelism = Environment.ProcessorCount,
                    TaskScheduler = TaskScheduler.Default
                }, (i, state) =>
                {
                    Console.WriteLine(i);
                    if (i == 20)
                    {
                        state.Break();
                        Console.WriteLine(state.IsStopped);
                    }
                });
            Console.WriteLine("------");
            Console.WriteLine(result.IsCompleted);
            Console.WriteLine(result.LowestBreakIteration);
        }
    }
}
