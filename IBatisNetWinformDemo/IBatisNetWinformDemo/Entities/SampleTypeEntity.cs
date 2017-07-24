using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBatisNetWinformDemo.Entities
{
    /// <summary>
    /// SampleTypeEntity实体
    /// </summary>
    public class SampleTypeEntity : BaseEntity<long>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
