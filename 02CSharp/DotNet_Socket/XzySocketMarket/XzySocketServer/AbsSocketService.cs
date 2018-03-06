using System;
using XzySocketBase;

namespace XzySocketServer
{
    /// <summary>
    /// abstract socket service interface.
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public abstract class AbsSocketService<TMessage> : ISocketService<TMessage>
        where TMessage : class, Messaging.IMessage
    {
        /// <summary>
        /// 当建立socket连接时，会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        public virtual void OnConnected(IConnection connection)
        {
        }
        /// <summary>
        /// 发送回调
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="packet"></param>
        /// <param name="isSuccess"></param>
        public virtual void OnSendCallback(IConnection connection, Packet packet, bool isSuccess)
        {
        }
        /// <summary>
        /// 当接收到客户端新消息时，会调用此方法.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="message"></param>
        public virtual void OnReceived(IConnection connection, TMessage message)
        {
        }
        /// <summary>
        /// 当socket连接断开时，会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        public virtual void OnDisconnected(IConnection connection, Exception ex)
        {
        }
        /// <summary>
        /// 当发生异常时，会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        public virtual void OnException(IConnection connection, Exception ex)
        {
        }
    }
}