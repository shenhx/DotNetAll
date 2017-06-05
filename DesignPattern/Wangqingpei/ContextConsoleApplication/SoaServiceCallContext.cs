using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContextConsoleApplication
{
    /// <summary>
    /// 服务调用上下文事件委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arg"></param>
    public delegate void SoaServiceCallContextHander<T>(T arg);

    public class SoaServiceCallContext : IDisposable
    {
        internal static SoaServiceCallContext _context;

        public bool Transaction { get; private set; }

        public bool LogTrack { get; private set; }

        public SoaServiceCallContext(bool transaction,bool logtrack)
        {
            this.Transaction = transaction;
            this.LogTrack = logtrack;
            SoaServiceCallContext._context = this;
        }

        private SoaServiceCallContextHander<LogTrackLocation> beginRecordLogTrackHander;
        /// <summary>
        /// 服务调用上下文已经开始记录跟踪日志
        /// </summary>

        public event SoaServiceCallContextHander<LogTrackLocation> BeginRecordLogTrackEvent
        {
            add
            {
                if (this.beginRecordLogTrackHander == null)
                    this.beginRecordLogTrackHander += value;
            }
            remove
            {
                if (this.beginRecordLogTrackHander != null)
                    this.beginRecordLogTrackHander -= value;
            }
        }

        private SoaServiceCallContextHander<TransactionActionInfo> transactionEndHander;

        public event SoaServiceCallContextHander<TransactionActionInfo> TransactionEndEvent
        {
            add
            {
                if(this.transactionEndHander == null)
                    this.transactionEndHander += value;
            }
            remove
            {
                if (this.transactionEndHander != null)
                    this.transactionEndHander -= value;
            }
        }

        public void Dispose()
        {
            this.beginRecordLogTrackHander = null;
            this.transactionEndHander = null;
        }
    }
}
