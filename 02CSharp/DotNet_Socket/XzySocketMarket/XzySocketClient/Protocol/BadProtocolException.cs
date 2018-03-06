using System;

namespace XzySocketClient.Protocol
{
    /// <summary>
    /// bad protocol exception
    /// </summary>
    public sealed class BadProtocolException : ApplicationException
    {
        /// <summary>
        /// new
        /// </summary>
        public BadProtocolException()
            : base("bad protocol.")
        {
        }
        /// <summary>
        /// new
        /// </summary>
        /// <param name="message"></param>
        public BadProtocolException(string message)
            : base(message)
        {
        }
    }
}