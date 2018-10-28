using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiThread.couter
{
    /// <summary>
    /// 不是线性安全的类
    /// </summary>
    public class UnSafeCouter : CouterBase
    {
        private int _count;
        public int Count { get { return _count; } }
        public override void Increment()
        {
            _count++;
        }

        public override void Descrement()
        {
            _count--;
        }
    }
}
