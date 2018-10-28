using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.spin
{
    public class TestApp
    {
        static volatile bool _isCompleted = false;

        static void UserModeWait()
        {
            while (!_isCompleted)
            {
                Console.WriteLine(".");
            }
            Console.WriteLine();
            Console.WriteLine("wating is completed");
        }

        static void HybridSpinWait()
        {
            var w = new SpinWait();
            while (!_isCompleted)
            {
                w.SpinOnce();
                Console.WriteLine(w.NextSpinWillYield);
            }
            Console.WriteLine("wating is completed");
        }

        public static void Run()
        {
            var t1 = new Thread(UserModeWait);
            var t2 = new Thread(HybridSpinWait);
            Console.WriteLine("running user mode waiting");
            t1.Start();
            Thread.Sleep(20);
            _isCompleted = true;
            Thread.Sleep(TimeSpan.FromSeconds(1));
            _isCompleted = false;
            Console.WriteLine("runing hybrid spinwaite");
            t2.Start();
            Thread.Sleep(5);
            _isCompleted = true;
        }
    }
}
