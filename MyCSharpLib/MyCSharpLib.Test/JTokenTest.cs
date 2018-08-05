using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace MyCSharpLib.Test
{
    [TestClass]
    public class JTokenTest
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            string json = "[{\"Id\":\"150079\",\"SignID\":\"zyhs01\",\"SignName\":\"沈鸿校\",\"SignDateTime\":\"2018-06-27 12:19:35\",\"RecSignType\":\"0\",\"SignStyle\":\"1\",\"SignElementID\":\"fb0cbef4c2c2431aa1f9c1e7fee19cb2\"},{\"Id\":\"150076\",\"SignID\":\"zyhs01\",\"SignName\":\"沈鸿校\",\"SignDateTime\":\"2018-06-27 12:15:22\",\"RecSignType\":\"0\",\"SignStyle\":\"1\",\"SignElementID\":\"05faba3e286b4e9c90165f06817d1ee4\"},{\"Id\":\"150077\",\"SignID\":\"zyhs01\",\"SignName\":\"沈鸿校\",\"SignDateTime\":\"2018-06-27 12:16:01\",\"RecSignType\":\"0\",\"SignStyle\":\"1\",\"SignElementID\":\"fb0cbef4c2c2431aa1f9c1e7fee19cb2\"},{\"Id\":\"150078\",\"SignID\":\"zyhs01\",\"SignName\":\"沈鸿校\",\"SignDateTime\":\"2018-06-27 12:16:10\",\"RecSignType\":\"0\",\"SignStyle\":\"1\",\"SignElementID\":\"fb0cbef4c2c2431aa1f9c1e7fee19cb2\"},{\"Id\":\"150080\",\"SignID\":\"zyhs01\",\"SignName\":\"沈鸿校\",\"SignDateTime\":\"2018-06-27 12:20:06\",\"RecSignType\":\"0\",\"SignStyle\":\"1\",\"SignElementID\":\"fb0cbef4c2c2431aa1f9c1e7fee19cb2\"},{\"Id\":\"150058\",\"SignID\":\"zyhs01\",\"SignName\":\"沈鸿校\",\"SignDateTime\":\"2018-06-27 10:25:50\",\"RecSignType\":\"0\",\"SignStyle\":\"1\",\"SignElementID\":\"fb0cbef4c2c2431aa1f9c1e7fee19cb2\"},{\"Id\":\"150549\",\"SignID\":\"zyhs01\",\"SignName\":\"沈鸿校\",\"SignDateTime\":\"2018-06-27 15:01:19\",\"RecSignType\":\"0\",\"SignStyle\":\"1\",\"SignElementID\":\"a66a0aa5f3964fea98cb8013b288de89\"}]";
            JToken jToken = JToken.Parse(json);
            foreach (var item in jToken.Children())
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
