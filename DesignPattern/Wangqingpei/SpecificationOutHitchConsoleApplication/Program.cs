using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecificationOutHitchConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = OrderSpecificationManagerFactory.CreateNewOrderSpecificationManager();
            var orderList = new List<Order>()
            {
                    new Order(){CustomerInfo = new Customer(){CustomerType = CustomerTypeEnum.Vip}},
                    new Order(){CustomerInfo = new Customer(){CustomerType = CustomerTypeEnum.Normal}}
            };
            OrderBusiness business = new OrderBusiness(manager);
            using (manager)
            {
                orderList.ForEach(p => business.SubmitOrder(p));
            }
        }
    }
}
