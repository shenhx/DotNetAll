using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBatisNetWinformDemo.Entities
{
    /// <summary>
    /// Sample汇总
    /// </summary>
    public class SampleSummaryEntity : BaseEntity<long>
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品列表
        /// </summary>
        public List<SampleMainEntity> SampleMainList { get; set; }
    }
}
