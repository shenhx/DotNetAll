using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Ranking
{
    /// <summary>
    /// 拓扑排序
    /// <seealso cref="https://mp.weixin.qq.com/s/gdHfCiDLwhzXhh7Vhj7sLw"/>
    /// </summary>
    public sealed class TopologicalRanking
    {

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDependencies"></param>
        /// <returns></returns>
        public static IList<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>();
            foreach (var item in source)
            {
                Visit(item, getDependencies, sorted, visited);
            }
            return sorted;
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="getDependencies"></param>
        /// <param name="sorted"></param>
        /// <param name="visited"></param>
        public static void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcessing = false;
            var alreadyVisited = visited.TryGetValue(item, out inProcessing);

            if (alreadyVisited)
            {
                if (inProcessing)
                {
                    // 不符合DAG图
                    throw new ArgumentException("Cyclic dependency found.");
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        Visit(dependency,getDependencies,sorted,visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    
    }

    public class Item
    {
        public string Name { get; set; }
        public Item[] Dependencies { get; set; }
        public Item(string name, params Item[] dependencies)
        {
            this.Name = name;
            this.Dependencies = dependencies;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
