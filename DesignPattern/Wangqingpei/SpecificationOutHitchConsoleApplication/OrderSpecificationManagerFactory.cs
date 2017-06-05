using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SpecificationOutHitchConsoleApplication
{
    public class OrderSpecificationManagerFactory
    {
        public const string SpecificationFileName = "OrderSubmitSpec.xml";
        /// <summary>
        /// 创建第一个用于检查提交订单相关的规则管理器
        /// </summary>
        /// <returns></returns>
        public static OrderSpecificationManage CreateNewOrderSpecificationManager()
        {
            OrderSpecificationManage result = new OrderSpecificationManage()
            {
                Specification = new Dictionary<CustomerTypeEnum, OrderSpecifcationIndex>()
            };
            SubmitOrderSpecification submitOrderSpec = new SubmitOrderSpecification();
            result.Specification.Add(CustomerTypeEnum.Vip, submitOrderSpec.CheckSubmitVipOrder);
            result.Specification.Add(CustomerTypeEnum.Normal, submitOrderSpec.CheckSubmitNormalOrder);
            return result;
        }

        /// <summary>
        /// 重建规则管理器
        /// </summary>
        /// <returns></returns>
        public static OrderSpecificationManage RebuildOrderSPecificationManager()
        {
            using (Stream stream = File.Open(SpecificationFileName, FileMode.Open))
            {
                BinaryFormatter format = new BinaryFormatter();
                return format.Deserialize(stream) as OrderSpecificationManage;
            };
        }

        /// <summary>
        /// 冻结规则管理器
        /// </summary>
        /// <param name="manager"></param>
        public static void FreezeOrderSpecificationManagerObject(OrderSpecificationManage manager)
        {
            using (Stream stream = File.Open(SpecificationFileName, FileMode.Create))
            {
                BinaryFormatter format = new BinaryFormatter();
                format.Serialize(stream, manager);
            }
        }
    }
}
