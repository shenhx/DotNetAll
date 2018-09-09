using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo.Core
{
    public sealed class ActionDelegatedEventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IEvent
    {
        #region 私有字段

        private readonly Action<TEvent> m_action;

        #endregion

        #region 构造函数

        public ActionDelegatedEventHandler(Action<TEvent> action)
        {
            this.m_action = action;
        }

        #endregion

        #region 公开方法

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(this,obj))
                return true;
            if(obj == null)
                return false;
            ActionDelegatedEventHandler<TEvent> other = obj as ActionDelegatedEventHandler<TEvent>;
            if(other == null)
                return false;
            return Delegate.Equals(this.m_action,other.m_action);

        }

        #endregion

        #region IEventHandler接口实现
        public void Handle(TEvent evt)
        {
            m_action(evt);
        }
        #endregion

    }
}
