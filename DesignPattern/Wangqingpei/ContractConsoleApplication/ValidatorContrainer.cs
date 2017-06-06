using System;
using System.Collections.Generic;
using System.Linq;

namespace ContractConsoleApplication
{
    /// <summary>
    /// 验证容器
    /// </summary>
    public class ValidatorContrainer : List<Validator>
    {
        public object ValidatorObject { get; internal set; }
        public bool IsTrue()
        {
            return this.All(validator => validator.IsTrue());
        }
    }
}