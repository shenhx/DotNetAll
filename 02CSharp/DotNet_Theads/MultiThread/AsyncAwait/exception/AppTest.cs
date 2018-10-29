using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait.exception
{
    public class AppTest
    {
        async static Task AsynchronousProcessing()
        {
            try
            {
                string result = await GetInfoAsync("Task1", 2);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("${ex}", ex);
            }

            Task<string> t1 = GetInfoAsync("Task1", 3);
            Task<string> t2 = GetInfoAsync("Task2", 2);
            try
            {
                string[] results = await Task.WhenAll(t1, t2);
                Console.WriteLine(results.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("${ex}", ex);
            }

            // 另一种写法
            t1 = GetInfoAsync("Task1", 3);
            t2 = GetInfoAsync("Task2", 2);
            Task<string[]> t3 = Task.WhenAll(t1, t2);
            try
            {
                string[] results = await t3;
                Console.WriteLine(results.Length);
            }
            catch (Exception)
            {
                var ae = t3.Exception.Flatten();
                var exceptions = ae.InnerExceptions;
                Console.WriteLine(exceptions.Count);
                foreach (var e in exceptions)
                {
                    Console.WriteLine(e);
                    Console.WriteLine();
                }
            }

        }

        async static Task<string> GetInfoAsync(string name, int seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            throw new Exception("boom from {0}", name);
        }

        public static void Run()
        {
            Task t = AsynchronousProcessing();
            t.Wait();
        }

    }
}
