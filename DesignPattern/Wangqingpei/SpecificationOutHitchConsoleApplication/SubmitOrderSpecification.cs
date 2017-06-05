using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecificationOutHitchConsoleApplication
{
    public class SubmitOrderSpecification
    {
        /// <summary>
        /// 检查订单是否Vip用户提交
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool CheckSubmitVipOrder(Order order)
        {
            if(order.CustomerInfo.CustomerType == CustomerTypeEnum.Vip)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查订单是否普通用户提交
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool CheckSubmitNormalOrder(Order order)
        {
            if (order.CustomerInfo.CustomerType == CustomerTypeEnum.Normal)
            {
                return true;
            }
            return false;
        }

    }
}
