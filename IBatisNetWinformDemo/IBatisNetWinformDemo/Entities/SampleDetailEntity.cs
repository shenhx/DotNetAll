using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBatisNetWinformDemo.Entities
{
    /// <summary>
    /// SampleDetailEntity实体
    /// </summary>
    public class SampleDetailEntity : BaseEntity<long>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
