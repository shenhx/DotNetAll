using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassivationConsoleApplication
{
    public class Order
    {
        public string OrderId { get; set; }
        public CustomerInfo Customer { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
