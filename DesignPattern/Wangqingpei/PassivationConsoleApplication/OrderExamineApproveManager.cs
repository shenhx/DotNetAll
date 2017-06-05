using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassivationConsoleApplication
{
    /// <summary>
    /// 订单审批过程索引
    /// </summary>
    /// <param name="order"></param>
    /// <param name="manager"></param>
    /// <returns></returns>
    public delegate bool OrderExamineApproveManagerHandler(Order order, ref OrderExamineApproveManagerHandler manager);

    /// <summary>
    /// 订单审批管理器
    /// </summary>
    [Serializable]
    public class OrderExamineApproveManager
    {
        public static OrderExamineApproveManager CreateFlows()
        {
            OrderExamineApproveManager result = new OrderExamineApproveManager();
            //绑定信息员审批流程
            Infomationer infoMationer = new Infomationer();
            result.Flows += infoMationer.CheckPrices;
            result.Flows += infoMationer.CheckNumbers;

            //绑定业务经理审批流程
            BusinessManager businessManager = new BusinessManager();
            result.Flows += businessManager.CallPhoneConfirm;
            result.Flows += businessManager.SendEmailNotice;

            //绑定总经理审批流程
            GeneralManager generalManager = new GeneralManager();
            result.Flows += generalManager.FinalConfirm;
            result.Flows += generalManager.SignAndRecord;

            return result;
        }

        /// <summary>
        /// 流程索引
        /// </summary>
        public OrderExamineApproveManagerHandler Flows;

        public void RunFlows(Order order)
        {
            this.Flows(order, ref this.Flows);
        }
    }
}
