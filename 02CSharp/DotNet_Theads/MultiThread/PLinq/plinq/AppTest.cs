using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLinq.plinq
{
    public class AppTest
    {
        static void PrintInfo(string typeName)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(150));
            Console.WriteLine(string.Format("{0} {1} {2}", typeName,
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread));
        }

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
                   select type.Name;
        }

        public static void Run()
        {
            var sw = new Stopwatch();
            sw.Start();
            var query = from t in GetTypes()
                        select EmulateProcessing(t);
            foreach (var typeName in query)
            {
                PrintInfo(typeName);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("-----------------");
            //Console.Clear();

            sw.Reset();
            sw.Start();
            var parallelQuery = from t in
                                    ParallelEnumerable.AsParallel(GetTypes())
                                select EmulateProcessing(t);
            foreach (var typeName in query)
            {
                PrintInfo(typeName);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("-----------------");

            sw.Reset();
            sw.Start();
            parallelQuery = from t in GetTypes().AsParallel()
                            select EmulateProcessing(t);
            parallelQuery.ForAll(PrintInfo);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("-----------------");

            sw.Reset();
            sw.Start();
            query = from t in GetTypes().AsParallel().AsSequential()
                    select EmulateProcessing(t);
            foreach (var typeName in query)
            {
                PrintInfo(typeName);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("-----------------");

        }


    }
}
