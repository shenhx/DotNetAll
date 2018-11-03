using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLinq.paras
{
    public class AppTest
    {
        static string EmulateProcessing(string typeName)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(new Random(DateTime.Now.Millisecond).Next(250, 350)));
            Console.WriteLine(string.Format("{0} {1} {2}", typeName, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread));
            return typeName;
        }

        static IEnumerable<string> GetTypes()
        {
            return from assembly in
                       AppDomain.CurrentDomain.GetAssemblies()
                   from type in assembly.GetExportedTypes()
                   where type.Name.StartsWith("Web")
                   orderby type.Name.Length
                   select type.Name;
        }

        public static void Run()
        {
            var parallelQuery = from t in GetTypes().AsParallel()
                                select EmulateProcessing(t);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(3));

            try
            {
                parallelQuery.WithDegreeOfParallelism(Environment.ProcessorCount)
                    .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                    .WithMergeOptions(ParallelMergeOptions.Default)
                    .WithCancellation(cts.Token).ForAll(Console.WriteLine);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation has been canceled");
            }

            Console.WriteLine("---");
            var unorderedQuery = from i in ParallelEnumerable.Range(1, 30) select i;
            foreach (var i in unorderedQuery)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("---");
            var orderedQuery = from i in ParallelEnumerable.Range(1, 30).AsOrdered() select i;
            foreach (var i in orderedQuery)
            {
                Console.WriteLine(i);
            }
        }
    }
}
