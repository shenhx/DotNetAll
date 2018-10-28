using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiThread
{
    class Program
    {
        static void Main(string[] args)
        {
            // couter.TestApp.Run(args);
            // mutex.TestApp.Run();
            // semaphore.TestApp.Run();
            // autoset.TestApp.Run();
            // manualreset.TestApp.Run();
            // countdown.TestApp.Run();
            // barrier.TestApp.Run();
            readerwriterlock.TestApp.Run();
            Console.ReadKey();
        }
    }
}
