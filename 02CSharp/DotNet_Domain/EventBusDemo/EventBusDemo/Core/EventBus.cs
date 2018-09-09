using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventBusDemo.Core
{
    public class EventBus
    {
        #region 私有字段

        private static EventBus m_eventBus = null;

        /// <summary>
        /// 领域模型事件句柄字典，用于存储领域模型的句柄
        /// </summary>
        private static Dictionary<Type, List<object>> m_dictEventHandler = new Dictionary<Type, List<object>>();

        private static readonly object m_syncRoot = new object();

        #endregion

        #region 构造函数

        private EventBus()
        {

        }

        public static EventBus Instance
        {
            get
            {
                return m_eventBus ?? (m_eventBus = new EventBus());
            }
        }

        /// <summary>
        /// 通过ＸＭＬ文件初始化事件总线，订阅信自在ＸＭＬ里配置
        /// </summary>
        /// <returns></returns>
        public static EventBus InstanceFromXml()
        {
            if (m_eventBus == null)
            {
                XElement root = XElement.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "eventbus.xml"));
                foreach (var evt in root.Elements("Event"))
                {
                    List<object> handlers = new List<object>();
                    Type pulishEventType = Type.GetType(evt.Element("PulishEvent").Value);
                    foreach (var subscritedEvt in evt.Elements("SubscribedEvents"))
                    {
                        foreach (var concreteEvt in subscritedEvt.Elements("SubscribedEvent"))
                        {
                            handlers.Add(Type.GetType(concreteEvt.Value));
                        }
                    }
                    m_dictEventHandler[pulishEventType] = handlers;
                }
                m_eventBus = new EventBus();
            }
            return m_eventBus;
        }

        #endregion

        #region 订阅事件

        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent
        {
            lock (m_syncRoot)
            {
                var eventType = typeof(TEvent);
                if (m_dictEventHandler.ContainsKey(eventType))
                {
                    var handers = m_dictEventHandler[eventType];
                    if (handers != null)
                    {
                        handers.Add(eventHandler);
                    }
                    else
                    {
                        handers = new List<object> {eventHandler };
                    }
                }
                else
                {
                    m_dictEventHandler.Add(eventType, new List<object> { eventHandler });
                }
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> eventhandlerAction)
            where TEvent : IEvent
        {
            Subscribe<TEvent>(new ActionDelegatedEventHandler<TEvent>(eventhandlerAction));
        }

        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : IEvent
        {
            foreach (var eventHandler in eventHandlers)
            {
                Subscribe<TEvent>(eventHandler);
            }
        }

        #endregion

        #region 发布事件
        
        public void Publish<TEvent>(TEvent evt,Action<TEvent,bool,Exception> callback)
            where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if(m_dictEventHandler.ContainsKey(eventType) && m_dictEventHandler[eventType] != null
                && m_dictEventHandler[eventType].Count > 0)
            {
                var handlers = m_dictEventHandler[eventType];
                try
                {
                    foreach (var handler in handlers)
                    {
                        var eventHandler = handler as IEventHandler<TEvent>;
                        eventHandler.Handle(evt);
                        callback(evt,true,null);
                    }
                }
                catch (Exception ex)
                {
                    callback(evt, false, ex);
                }
            }
            else
            {
                callback(evt, false, null);
            }
        }

        #endregion

        #region 取消订阅事件


        public void UnSubscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : IEvent
        {
            lock (m_syncRoot)
            {
                var eventType = typeof(TEvent);
                if (m_dictEventHandler.ContainsKey(eventType))
                {
                    var handlers = m_dictEventHandler[eventType];
                    if (handlers != null && handlers.Exists(p => eventHandlerEquals(p, eventHandler)))
                    {
                        var handlerToRemove = handlers.First(p => eventHandlerEquals(p,eventHandler));
                        handlers.Remove(handlerToRemove);
                    }
                }
            }
        }

        private readonly Func<object, object, bool> eventHandlerEquals = (o1, o2) =>
        {
            var o1Type = o1.GetType();
            var o2Type = o2.GetType();
            return o1Type == o2Type;
        };

        #endregion
    }
}
