using System;
using XzySocketBase;

namespace XzySocketServer
{
    /// <summary>
    /// socket service interface.
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface ISocketService<TMessage> where TMessage : class, Messaging.IMessage
    {
        /// <summary>
        /// 当建立socket连接时，会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        void OnConnected(IConnection connection);
        /// <summary>
        /// 发送回调
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="packet"></param>
        /// <param name="isSuccess"></param>
        void OnSendCallback(IConnection connection, Packet packet, bool isSuccess);
        /// <summary>
        /// 当接收到客户端新消息时，会调用此方法.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="message"></param>
        void OnReceived(IConnection connection, TMessage message);
        /// <summary>
        /// 当socket连接断开时，会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        void OnDisconnected(IConnection connection, Exception ex);
        /// <summary>
        /// 当发生异常时，会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        void OnException(IConnection connection, Exception ex);
    }
}