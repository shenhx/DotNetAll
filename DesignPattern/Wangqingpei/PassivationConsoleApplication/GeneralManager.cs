using System;

namespace PassivationConsoleApplication
{
    [Serializable]
    public class GeneralManager
    {
        internal bool FinalConfirm(Order order, ref OrderExamineApproveManagerHandler manager)
        {
            if (OrderExamineApproveHelper.CallPhoneConfirm(order.Customer.Phone))
            {
                manager -= this.FinalConfirm;
                return true;
            }
            return false;
        }

        internal bool SignAndRecord(Order order, ref OrderExamineApproveManagerHandler manager)
        {
            if (OrderExamineApproveHelper.SignAndRecord(order.OrderId))
            {
                manager -= this.SignAndRecord;
                return true;
            }
            return false;

        }
    }
}