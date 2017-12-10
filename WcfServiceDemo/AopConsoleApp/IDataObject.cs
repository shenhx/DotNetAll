using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopConsoleApp
{
    /// <summary>
    /// 数据对象接口；
    /// </summary>
    public interface IDataObject
    {
        /// <summary>
        /// 计算结果；
        /// </summary>
        int Compute();
    }
}
