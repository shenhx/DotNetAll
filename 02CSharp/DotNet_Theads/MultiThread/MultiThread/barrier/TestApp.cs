using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.barrier
{
    public class TestApp
    {
        static Barrier _barrier = new Barrier(2, b => {
            Console.WriteLine("{0}",b.CurrentPhaseNumber+1);
        });

        static void PlayMusic(string name,string message,int seconds)
        {
            int length = 3;
            for (int i = 1; i < length; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} {1}",name,message);
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} ends {1}", name, message);
                _barrier.SignalAndWait();
            }
        }

        public static void Run()
        {
            var t1 = new Thread(() => PlayMusic("1","doleimi",5));
            var t2 = new Thread(() => PlayMusic("2", "sofala", 2));
            t1.Start();
            t2.Start();
        }

    }
}
