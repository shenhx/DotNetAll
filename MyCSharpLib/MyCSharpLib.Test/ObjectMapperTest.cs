using System;
using System.Linq.Expressions;
using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyCSharpLib;

namespace MyCSharpLib.Test
{
    [TestClass]
    public class ObjectMapperTest
    {
        ObjectMapper<Male, FeMale> objectMapper = new ObjectMapper<Male, FeMale>();
        Male male = new Male { Name = "shenhx", Age = 12 };

        [TestMethod]
        public void ReflectionObject_Test()
        {
            Console.WriteLine("ReflectionObject_Test开始时间：{0}",DateTime.Now);
            FeMale feMale = objectMapper.ReflectionObject(male);
            Console.WriteLine("ReflectionObject_Test结束时间：{0}", DateTime.Now);
        }

        [TestMethod]
        public void SerializeObject_Test()
        {
            Console.WriteLine("SerializeObject_Test开始时间：{0}", DateTime.Now);
            FeMale feMale = objectMapper.SerializeObject(male);
            Console.WriteLine("SerializeObject_Test结束时间：{0}", DateTime.Now);
        }

        [TestMethod]
        public void TransByCopyObject_Test()
        {
            Console.WriteLine("TransByCopyObject_Test开始时间：{0}", DateTime.Now);
            ObjectMapperByDictCache objectMapper2 = new ObjectMapperByDictCache();
            FeMale feMale = objectMapper2.TransByCopyObject<Male,FeMale>(male);
            Assert.IsNotNull(feMale);
            Console.WriteLine("TransByCopyObject_Test结束时间：{0}", DateTime.Now);
        }

        [TestMethod]
        public void TransByGenericObject_Test()
        {
            ObjectMapperGenericType<Male, FeMale> objectMapper3 = new ObjectMapperGenericType<Male, FeMale>();
            Console.WriteLine("TransByGenericObject_Test开始时间：{0}", DateTime.Now);
            FeMale feMale = objectMapper3.TransByGenericObject(male);
            Console.WriteLine("TransByGenericObject_Test结束时间：{0}", DateTime.Now);

        }

        [TestMethod]
        public void Lambda_Test()
        {
            Expression<Func<Male, FeMale>> person = (x) => new FeMale { Age = x.Age,Name = x.Name};
            var f = person.Compile();
            FeMale feMale = f(male);
        }

        internal class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }

        internal class Male : Person
        {
        }

        internal class FeMale : Person
        {

        }
    }
}
