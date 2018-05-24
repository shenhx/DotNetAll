using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClrViaCsharp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Byte b = 100;
            //b = checked((byte)(b+200));//会抛出异常
            //b = (Byte)checked(b + 200);
            Console.WriteLine(b);
            Console.WriteLine(checked(b + 200));
            Console.ReadKey();
        }
    }
}
