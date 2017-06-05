using System;
using System.Linq;

namespace PassivationConsoleApplication
{
    [Serializable]
    internal class Infomationer
    {
        internal bool CheckPrices(Order order, ref OrderExamineApproveManagerHandler manager)
        {
            if(order.Items.Any(item => item.Product.Price <= 0) ? false : true)
            {
                manager -= this.CheckPrices;//将自己从流程中移除
                return true;
            }
            return false;
        }

        internal bool CheckNumbers(Order order, ref OrderExamineApproveManagerHandler manager)
        {
            if (order.Items.Any(item => item.Number >= 10) ? false : true)
            {
                manager -= this.CheckNumbers;//将自己从流程中移除
                return true;
            }
            return false;
        }
    }
}