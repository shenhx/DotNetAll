using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo.Core
{
    /// <summary>
    /// 事件处理接口
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="evt">行为</param>
        void Handle(TEvent evt);

    }
}
