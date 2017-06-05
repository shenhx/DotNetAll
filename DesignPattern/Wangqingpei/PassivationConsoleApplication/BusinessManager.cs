using System;

namespace PassivationConsoleApplication
{
    [Serializable]
    public class BusinessManager
    {

        internal bool CallPhoneConfirm(Order order, ref OrderExamineApproveManagerHandler manager)
        {
            if (OrderExamineApproveHelper.CallPhoneConfirm(order.Customer.Phone))
            {
                manager -= this.CallPhoneConfirm;
                return true;
            }
            return false;
        }

        internal bool SendEmailNotice(Order order, ref OrderExamineApproveManagerHandler manager)
        {
            if (OrderExamineApproveHelper.SendMail(order.Customer.Email))
            {
                manager -= this.SendEmailNotice;
                return true;
            }
            return false;
        }
    }
}