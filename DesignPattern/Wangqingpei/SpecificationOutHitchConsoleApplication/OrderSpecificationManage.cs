using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecificationOutHitchConsoleApplication
{
    public delegate bool OrderSpecifcationIndex(Order order);

    /// <summary>
    /// 订单规则管理器
    /// </summary>
    public class OrderSpecificationManage : IDisposable
    {
        public Dictionary<CustomerTypeEnum,OrderSpecifcationIndex> Specification { get; set; }

        public OrderSpecifcationIndex GetSpecificationWithCustomerType(CustomerTypeEnum customerType)
        {
            if (this.Specification.ContainsKey(customerType))
            {
                return this.Specification[customerType];
            }
            return null;
        }

        public void Dispose()
        {
            OrderSpecificationManagerFactory.FreezeOrderSpecificationManagerObject(this);
        }
    }
}
