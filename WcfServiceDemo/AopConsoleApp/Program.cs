using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataObject dtObj = DataObject.CreateDataObjec2(10, 6);
            int result = dtObj.Compute();

            Console.ReadLine();

        }
    }
}
