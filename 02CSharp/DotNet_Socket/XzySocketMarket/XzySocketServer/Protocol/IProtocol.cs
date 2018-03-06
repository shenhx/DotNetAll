using XzySocketBase;
using System;

namespace XzySocketServer.Protocol
{
    /// <summary>
    /// tcp协议接口
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IProtocol<TMessage> where TMessage : class, Messaging.IMessage
    {
        /// <summary>
        /// parse protocol message
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="buffer"></param>
        /// <param name="maxMessageSize"></param>
        /// <param name="readlength"></param>
        /// <returns></returns>
        TMessage Parse(IConnection connection, ArraySegment<byte> buffer,
            int maxMessageSize, out int readlength);
    }
}