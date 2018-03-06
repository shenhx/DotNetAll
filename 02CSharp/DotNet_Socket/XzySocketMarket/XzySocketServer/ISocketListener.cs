using System;
using System.Net;
using XzySocketBase;

namespace XzySocketServer
{
    /// <summary>
    /// socket listener
    /// </summary>
    public interface ISocketListener
    {
        /// <summary>
        /// socket accepted event
        /// </summary>
        event Action<ISocketListener, IConnection> Accepted;

        /// <summary>
        /// get endpoint
        /// </summary>
        EndPoint EndPoint { get; }
        /// <summary>
        /// start listen
        /// </summary>
        void Start();
        /// <summary>
        /// stop listen
        /// </summary>
        void Stop();
    }
}