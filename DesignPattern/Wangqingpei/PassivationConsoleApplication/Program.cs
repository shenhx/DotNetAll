using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace PassivationConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //序列化
            Order order = new Order
            {
                Items = new List<OrderItem>(),
                Customer = new CustomerInfo() { Email = "text@test.com", Name = "test", Phone = "12345678901" }
            };
            order.Items.Add(new OrderItem()
            {
                Number = 10,
                Product = new ProductModel() { Name = "p1", Price = 500 }
            });
            order.Items.Add(new OrderItem()
            {
                Number = 9,
                Product = new ProductModel() { Name = "p2", Price = 100 }
            });
            OrderExamineApproveManager approveFlows = OrderExamineApproveManager.CreateFlows();
            using (Stream stream1 = File.Open("orderChecks.xml", FileMode.Create))
            {
                BinaryFormatter format = new BinaryFormatter();
                format.Serialize(stream1, approveFlows);
            }

            //反序列化
            using (Stream stream2 = File.Open("orderChecks.xml", FileMode.Open))
            {
                BinaryFormatter format = new BinaryFormatter();
                var approveFlws = format.Deserialize(stream2) as OrderExamineApproveManager;
                approveFlows.RunFlows(order);
            }
        }
    }
}
