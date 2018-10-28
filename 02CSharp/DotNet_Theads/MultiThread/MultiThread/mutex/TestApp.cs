using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.mutex
{
    /// <summary>
    /// 适用于多个实例间的同步互斥
    /// </summary>
    public class TestApp
    {
        private static readonly string MUTEX_NAME = "TEST-MUTEX";

        static void Test()
        {
            using (var mutex = new Mutex(false, MUTEX_NAME))
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(5), true))
                {
                    Console.WriteLine("第二个程序运行！");
                }
                else
                {
                    Console.WriteLine("正在运行，稍后！");
                    Console.ReadLine();
                    Console.WriteLine(DateTime.Now);
                    mutex.ReleaseMutex();
                }
            }
        }

        public static void Run()
        {
            Test();
        }
    }
}
