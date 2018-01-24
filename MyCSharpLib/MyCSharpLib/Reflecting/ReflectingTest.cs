using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ReflectingTest : ReflectingTestBase, IReflectingTest
    {
        public override void SayHello()
        {
            Console.WriteLine("hello world!");
            base.SayHello();
        }
    }
}
