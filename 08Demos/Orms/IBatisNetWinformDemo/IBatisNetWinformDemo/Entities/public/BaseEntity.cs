using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBatisNetWinformDemo.Entities
{
    /// <summary>
    /// Entity
    /// </summary>
    public class BaseEntity<T>
    {
        /// <summary>
        /// 主键字段
        /// </summary>
        public T Id { get; set; }
    }
}
