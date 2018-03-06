﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GodSharp.Sockets
{
    /// <summary>
    /// Socket Server
    /// </summary>
    public partial class SocketServer:SocketBase
    {
        private Dictionary<Guid, TcpListener> listeners;
        private Dictionary<string, Guid> clientMap;
        private List<TcpSender> clients;

        /// <summary>
        /// Gets or sets the server identifier.
        /// </summary>
        /// <value>
        /// The server identifier.
        /// </value>
        public string ServerId { get; set; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        //public SenderCollection Items { get; private set; }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public List<TcpSender> Clients { get => clients; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SocketServer"/> is blocking.
        /// </summary>
        /// <value>
        ///   <c>true</c> if blocking; otherwise, <c>false</c>.
        /// </value>
        public bool Blocking => socket.Blocking;

        /// <summary>
        /// Gets a value indicating whether this <see cref="SocketServer"/> is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if running; otherwise, <c>false</c>.
        /// </value>
        public bool Running { get; private set; } = false;

        /// <summary>
        /// Gets or sets the on closed.
        /// </summary>
        /// <value>
        /// The on closed.
        /// </value>
        public new Action<TcpSender> OnClosed { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketServer"/> class.default host is 0.0.0.0,default port is 7788.
        /// </summary>
        public SocketServer()
        {
            base.OnClosed = OnClosedFun;

            Host = "0.0.0.0";
            Port = 7788;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketServer"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        public SocketServer(int port) : this()
        {
            try
            {
                SetPort(port);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        public SocketServer(string host, int port=7788) : this()
        {
            try
            {
                SetHost(host);

                SetPort(port);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listens the specified backlog.
        /// </summary>
        /// <param name="backlog">The backlog.</param>
        public void Listen(int backlog=100)
        {
            try
            {
                IPAddress ip = string.IsNullOrEmpty(Host) ? IPAddress.Any : IPAddress.Parse(Host);

                socket.Bind(new IPEndPoint(ip, Port));
                socket.Listen(backlog);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Listens the specified <see cref="EndPoint"/> and backlog.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        /// <param name="backlog">The backlog.</param>
        /// <exception cref="ArgumentNullException">endPoint</exception>
        public void Listen(EndPoint endPoint,int backlog=100)
        {
            try
            {
                if (endPoint==null)
                {
                    throw new ArgumentNullException(nameof(endPoint));
                }

                this.Host = endPoint.GetHost();
                this.Port = endPoint.GetPort();

                socket.Bind(endPoint);
                socket.Listen(backlog);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        /// <summary>
        /// Start Socket client.
        /// </summary>
        public override void Start()
        {
            try
            {
                Accept();
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            try
            {
                foreach (var item in listeners)
                {
                    item.Value?.Stop();
                }

                clients.Clear();
                clientMap.Clear();
                listeners.Clear();

                if (socket.Connected)
                {
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Accepts this instance.
        /// </summary>
        private void Accept()
        {
            try
            {
                Running = true;
                Thread thread = new Thread(AcceptFun) { IsBackground = true };
                thread.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Accepts the fun.
        /// </summary>
        private void AcceptFun()
        {
            listeners = new Dictionary<Guid, TcpListener>();
            clientMap = new Dictionary<string, Guid>();
            clients = new List<TcpSender>();

            while (Running)
            {
                try
                {
                    Socket _socket = socket.Accept();

                    // keep alive
                    if (KeepAlive)
                    {
                        try
                        {
                            _socket.KeepAlive();
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            Console.WriteLine(ex.Message);
#endif
                        } 
                    }

                    if (!_socket.Connected)
                    {
                        continue;
                    }

                    string id = $"{_socket.RemoteEndPoint.GetHost()}:{_socket.RemoteEndPoint.GetPort()}";

                    if (clientMap?.ContainsKey(id) ==true)
                    {
                        continue;
                    }

                    TcpListener listener = new TcpListener(this, _socket, TcpListenerType.Server);
                    listener.Start();
                    
                    listeners.Add(listener.Guid, listener);

                    clientMap.Add(id, listener.Guid);
                    
                    clients.Add(listener.Sender);

                    OnConnected?.Invoke(listener.Sender);
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine(ex.Message);
#endif
                    throw new Exception(ex.Message, ex);
                }
            }
            Running = false;
        }

        /// <summary>
        /// Called when [closed fun].
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void OnClosedFun(TcpSender sender)
        {
            string id = $"{sender.LocalEndPoint.GetHost()}:{sender.LocalEndPoint.GetPort()}";

            if (this.clientMap.ContainsKey(id))
            {
                this.clientMap.Remove(id);
            }

            if (clients.Contains(sender))
            {
                clients.Remove(sender);
            }

            if (this.listeners.ContainsKey(sender.Guid))
            {
                this.listeners.Remove(sender.Guid);
            }

            this.OnClosed?.Invoke(sender);
        }

        /// <summary>
        /// Gets the <see cref="TcpSender"/> with the specified unique identifier.
        /// </summary>
        /// <value>
        /// The <see cref="TcpSender"/>.
        /// </value>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public TcpSender this[Guid guid]
        {
            get { return this.listeners[guid].Sender; }
        }

        /// <summary>
        /// Gets the <see cref="TcpSender"/> with the specified host.
        /// </summary>
        /// <value>
        /// The <see cref="TcpSender"/>.
        /// </value>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public TcpSender this[string host, int port]
        {
            get
            {
                string k = $"{host.Trim()}:{port}";

                if (!clientMap.ContainsKey(k))
                {
                    return null;
                }

                Guid guid = clientMap[k];

                if (!listeners.ContainsKey(guid))
                {
                    return null;
                }

                return this.listeners[guid].Sender;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="GodSharp.Sockets.TcpSender" /> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="GodSharp.Sockets.TcpSender" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public TcpSender this[int index]
        {
            get
            {
                if (index < 0 || index > clients.Count - 1)
                {
                    return null;
                }

                return clients[index];
            }
        }
    }
}