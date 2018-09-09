using EventBusDemo.Demos;
using EventBusDemo.Demos.Fishing;
using EventBusDemo.Demos.register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IDemo demo = Factory();
            demo.PrintTest();
        }

        private static IDemo Factory()
        {
            //return new RegisterProgram();
            return new FishingProgram();
        } 
    }
}
