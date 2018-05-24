using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBatisNetWinformDemo.Entities
{
    /// <summary>
    /// SampleMainEntity实体
    /// </summary>
    public class SampleMainEntity : BaseEntity<long>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string SampleType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 明细表集合
        /// </summary>
        public List<SampleDetailEntity> DetailList { get; set; }
    }
}
