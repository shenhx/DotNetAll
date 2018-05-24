using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpLib.Test
{
    [TestClass]
    public class Assembly_ReflectiingTest_Test
    {
        [TestMethod]
        public void InitlizeObjectUsingReflecting()
        {
            Type[] types = Assembly.Load(typeof(IReflectingTest).Assembly.FullName).GetExportedTypes();
            foreach (Type type in types)
            {
                if(type.GetInterface(typeof(IReflectingTest).Name) == typeof(IReflectingTest))
                {
                    if (type.IsAbstract)
                    {
                        Console.WriteLine("{0}类型为抽象类，不可初始化！",type.Name);
                        continue;
                    }
                    IReflectingTest test =  Activator.CreateInstance(type) as IReflectingTest;
                    if(test != null)
                    {
                        Console.WriteLine();
                        test.SayHello();
                        test = null;
                    }
                    
                }
            }
        }
    }
}
