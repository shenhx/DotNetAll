using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.couter
{
    public class CounterNoLock : CouterBase
    {
        private int _count;
        public int Count { get { return _count; } }
        public override void Increment()
        {
            Interlocked.Increment(ref _count);
        }

        public override void Descrement()
        {
            Interlocked.Decrement(ref _count);
        }
    }
}
