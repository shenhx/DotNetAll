using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.syncvoid
{
    public class AppTest
    {
        async static Task AsyncTaskWithErrors()
        {
            string result = await GetInfoAsync("AsyncTaskException",2);
            Console.WriteLine(result);
        }

        /// <summary>
        /// async void建议只是在事件中使用
        /// </summary>
        async static void AsyncVoidWithErrors()
        {
            string result = await GetInfoAsync("AsyncVoidException", 2);
            Console.WriteLine(result);
        }

        async static Task AsyncTask()
        {
            string result = await GetInfoAsync("AsyncTask", 2);
            Console.WriteLine(result);
        }

        private static async void AsyncVoid()
        {
            string result = await GetInfoAsync("AsyncVoid", 2);
            Console.WriteLine(result);
        }


        private static async Task<string> GetInfoAsync(string name, int seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            if (name.Contains("Exception"))
            {
                throw new Exception(string.Format("Boom from {0}",name)); 
            }
            return string.Format("{0} {1} {2}",name,Thread.CurrentThread.ManagedThreadId,Thread.CurrentThread.IsThreadPoolThread);
        }

        public static void Run()
        {
            Task t = AsyncTask();
            t.Wait();

            AsyncVoid();
            Thread.Sleep(TimeSpan.FromSeconds(3));

            t = AsyncTaskWithErrors();
            while (!t.IsFaulted)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            // 找台虚拟机测试下这个代码
            //try
            //{
            //    AsyncVoidWithErrors();
            //    Thread.Sleep(TimeSpan.FromSeconds(3));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            //int[] numbers = new[] { 1,2,3,4,5};
            //Array.ForEach(numbers, async number => {
            //    await Task.Delay(TimeSpan.FromSeconds(1));
            //    if (number == 3)
            //        throw new Exception("Boom");
            //    Console.WriteLine(number);
            //});
            
        }

    }
}
