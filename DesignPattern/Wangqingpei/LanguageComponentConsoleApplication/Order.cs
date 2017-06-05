using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageComponentConsoleApplication
{
    public class Order : List<OrderItem>, IEnumerable<OrderItem>
    {
        /// <summary>
        /// 客户信息
        /// </summary>
        public Customer CustomerInfo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderNo { get; set; }

    }
}
