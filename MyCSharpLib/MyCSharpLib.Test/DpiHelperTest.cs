using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;

namespace MyCSharpLib.Test
{
    /// <summary>
    /// DpiHelperTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DpiHelperTest
    {
        public DpiHelperTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }
        DpiHelper dpiHelper = new DpiHelper();

        [TestMethod]
        public void WriteLineDpi()
        {
            Tuple<float, float> tuple = dpiHelper.GetDpiByDrawing();
            Console.WriteLine(tuple.Item1);
            Console.WriteLine(tuple.Item2);
        }
    }
}
