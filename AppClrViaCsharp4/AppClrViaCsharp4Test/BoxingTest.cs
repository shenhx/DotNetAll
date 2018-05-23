using AppClrViaCsharp4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClrViaCsharp4Test
{
    [TestClass]
    public class BoxingTest
    {
        [TestMethod]
        public void Boxing_Test_1()
        {
            Point p = new Point(1,1);
            Console.WriteLine(p);//1,1

            p.Change(2, 2);
            Console.WriteLine(p);//2,2

            object o = p;
            Console.Write(o);//2,2

            ((Point)o).Change(3, 3);
            Console.WriteLine(o);//2,2
        }
    }
}
