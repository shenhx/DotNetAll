using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecificationOutHitchConsoleApplication
{
public    class OrderBusiness
    {
        private OrderSpecificationManage OrderSpecManager;

        public OrderBusiness(OrderSpecificationManage orderSpceManager)
        {
            this.OrderSpecManager = orderSpceManager;
        }

        public void SubmitOrder(Order order)
        {
            var spec = OrderSpecManager.GetSpecificationWithCustomerType(order.CustomerInfo.CustomerType);
            if(order.CustomerInfo.CustomerType == CustomerTypeEnum.Vip && spec(order))
            {

            }
            else if (order.CustomerInfo.CustomerType == CustomerTypeEnum.Normal && spec(order))
            {

            }

        }

    }
}
