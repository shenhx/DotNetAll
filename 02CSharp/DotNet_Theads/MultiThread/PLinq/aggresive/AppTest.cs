using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLinq.aggresive
{
    public class AppTest
    {
        static ConcurrentDictionary<char, int> AccumulateLettersInfomation(ConcurrentDictionary<char, int> taskTotal, string item)
        {
            foreach (var c in item)
            {
                if (taskTotal.ContainsKey(c))
                {
                    taskTotal[c] = taskTotal[c] + 1;
                }
                else
                {
                    taskTotal[c] = 1;
                }
            }
            return taskTotal;
        }

        static ConcurrentDictionary<char, int> MergeAccumulators(ConcurrentDictionary<char, int> total,
            ConcurrentDictionary<char, int> totalTask)
        {
            foreach (var key in totalTask.Keys)
            {
                if (total.ContainsKey(key))
                {
                    total[key] = total[key] + totalTask[key];
                }
                else
                {
                    total[key] = totalTask[key];
                }
            }
            return total;
        }

        static IEnumerable<string> GetTypes()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetExportedTypes());

            return from type in types
                   where type.Name.StartsWith("Web")
                   select type.Name;
        }

        public static void Run()
        {
            var parallelQuery = from t in GetTypes().AsParallel() select t;

            var parallelAggregator = parallelQuery.Aggregate(() => new ConcurrentDictionary<char, int>(),
                (taskTotal, item) => AccumulateLettersInfomation(taskTotal, item),
                (total, taskTotal) => MergeAccumulators(total, taskTotal),
                total => total);

            var orderedKeys = from k in parallelAggregator.Keys
                              orderby parallelAggregator[k] descending
                              select k;
            foreach (var c in orderedKeys)
            {
                Console.WriteLine(c + ":" + parallelAggregator[c]);
            }
        }

    }
}
