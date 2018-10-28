using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.autoset
{
    /// <summary>
    /// 使用系统内核时间，等待时间不能过长
    /// </summary>
    public class TestApp
    {
        private static AutoResetEvent _workerEvent = new AutoResetEvent(false);
        private static AutoResetEvent _mainEvent = new AutoResetEvent(false);

        /*
            等待
            开始调用
            Process完成
            等待主进程完成
            第一个完成
            等待
            主进程完成
            第二个也完成
         */

        static void Process(int seconds)
        {
            Console.WriteLine("开始调用");
            Thread.Sleep(seconds);
            Console.WriteLine("Process完成");
            _workerEvent.Set();
            Console.WriteLine("等待主进程完成");
            _mainEvent.WaitOne();
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("主进程完成");
            _workerEvent.Set();
        }

        public static void Run()
        {
            var t = new Thread(() => Process(10));
            t.Start();
            Console.WriteLine("等待");
            _workerEvent.WaitOne();
            Console.WriteLine("第一个完成");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            _mainEvent.Set();
            Console.WriteLine("等待");
            _workerEvent.WaitOne();
            Console.WriteLine("第二个也完成");

        }
    }
}
