using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLinq.partition
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
            Console.WriteLine(string.Format("{0} {1} {2} {3}", typeName,
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread,
                typeName.Length % 2 == 0 ? "even" : "odd"));
            return typeName;
        }

        static IEnumerable<string> GetTypes()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetExportedTypes());

            return from type in types
                   where
            type.Name.StartsWith("Web")
                   select type.Name;
        }

        public class StringPartitioner : Partitioner<string>
        {
            private readonly IEnumerable<string> _data;

            public StringPartitioner(IEnumerable<string> data)
            {
                this._data = data;
            }

            public override bool SupportsDynamicPartitions
            {
                get
                {
                    return false;
                }
            }

            public override IList<IEnumerator<string>> GetPartitions(int partitionCount)
            {
                var result = new List<IEnumerator<string>>(2);
                result.Add(CreateEnumerator(true));
                result.Add(CreateEnumerator(false));
                return result;
            }

            private IEnumerator<string> CreateEnumerator(bool isEven)
            {
                foreach (var d in this._data)
                {
                    if(!(d.Length %2 == 0 ^ isEven))
                    {
                        yield return d;
                    }
                }
            }
        }

        public static void Run()
        {
            var partitioner = new StringPartitioner(GetTypes());
            var parallelQuery = from t in partitioner.AsParallel()
                                select EmulateProcessing(t);

            parallelQuery.ForAll(PrintInfo);
        }
    }
}
