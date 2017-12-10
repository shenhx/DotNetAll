using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationOne.Models
{
    /// <summary>
    /// 订单模型
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>

        public string Description { get; set; }
        /// <summary>
        /// 金额
        /// </summary>

        public decimal Amount { get; set; }
    }
}