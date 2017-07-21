using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBatisNetWinformDemo.Entities
{
    /// <summary>
    /// 标准化参数配置类
    /// </summary>
    public class ParameterEntity<T> where T : class
    {
        /// <summary>
        /// XML入参
        /// </summary>
        public string xmlParam { get; set; }
        /// <summary>
        /// 返回代码0 成功，-1失败
        /// </summary>
        public int returnNo { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回游标
        /// </summary>
        public object curTemp { get; set; }
    }
}
