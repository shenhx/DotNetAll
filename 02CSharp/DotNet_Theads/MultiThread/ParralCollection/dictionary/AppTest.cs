using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParralCollection.dictionary
{
    public class AppTest
    {
        const string Item = "Dictionary item";
        public static string CurrentItem;

        public static void Run()
        {
            var concurrentDictionary = new ConcurrentDictionary<int, string>();
            var dictionary = new Dictionary<int, string>();

            var sw = new Stopwatch();
            sw.Start();
            int length = 1000000;
            for (int i = 0; i < length; i++)
            {
                lock (dictionary)
                {
                    dictionary[i] = Item;
                }
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            sw.Reset();
            sw.Restart();
            for (int i = 0; i < length; i++)
            {
                concurrentDictionary[i] = Item;
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            sw.Restart();
            for (int i = 0; i < length; i++)
            {
                lock (dictionary)
                {
                    CurrentItem = dictionary[i];
                }
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            sw.Restart();
            for (int i = 0; i < length; i++)
            {
                CurrentItem = concurrentDictionary[i];
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }
}
