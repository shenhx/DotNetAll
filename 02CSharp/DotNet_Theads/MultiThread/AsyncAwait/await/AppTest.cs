using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.await
{
    /// <summary>
    /// 
    /// <![CDATA[不能把控制台程序的Main方法标记为async。不能在catch、finally、lock或unsafe代码块中使用await操作符。不允许对任何异步函数使用ref或out参数。]]>
    /// </summary>
    public class AppTest
    {
        static Task AsynchronyWithTPL()
        {
            Task<string> t = GetInfoAsync("Task1");
            // 不推荐使用
            Task t2 = t.ContinueWith(task => Console.WriteLine(t.Result), TaskContinuationOptions.NotOnFaulted);
            Task t3 = t.ContinueWith(task => Console.WriteLine(t.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted);
            return Task.WhenAny(t2, t3);
        }

        async static Task AsynchronyWithAwait()
        {
            try
            {
                string result = await GetInfoAsync("Task2");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        async static Task<string> GetInfoAsync(string name)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return string.Format("{0} {1} {2}",name,Thread.CurrentThread.ManagedThreadId,Thread.CurrentThread.IsThreadPoolThread);
        }

        public static void Run()
        {
            Task t = AsynchronyWithTPL();
            t.Wait();

            t = AsynchronyWithAwait();
            t.Wait();
        }
    }
}
