using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageComponentConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order() { CustomerInfo = new Customer() { CunstomerType = CustomerTypeEnum.Vip } };
            order.Add(new OrderItem()
            {
                ProductInfo = new Product() { pId = "123456", pName = "1", pPrice = 100 }
            });
            order.Add(new OrderItem()
            {
                ProductInfo = new Product() { pId = "234567", pName = "2", pPrice = 3600 }
            });
            using(var language = LanguageComponentManagerFactory.RebuildLanguageComponent())
            {
                language.Resume();
            }
            Console.ReadKey();
        }
    }
}
