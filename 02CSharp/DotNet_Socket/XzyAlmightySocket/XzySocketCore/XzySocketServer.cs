using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static XzySocketCore.WebSocketHelper;

namespace XzySocketCore
{
    public class XzySocketServer
    {
        #region 私有变量
        Socket serverSocket;
        /// <summary>
        /// 连接的客户端信息
        /// </summary>
        List<LinkSocket> sockets = new List<LinkSocket>();
        /// <summary>
        /// 接收的包大小
        /// </summary>
        private static byte[] result = new byte[1024];
        /// <summary>
        /// 单例
        /// </summary>
        private static volatile XzySocketServer _instance = null;
        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object LockHelper = new object();

        #endregion

        #region 公有变量
        /// <summary>
        /// 接受消息的委托
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="id">发送消息的客户端ID</param>
        public delegate void ReceiveMessageHandler(string msg,string id);
        /// <summary>
        /// 接受消息委托
        /// </summary>
        public event ReceiveMessageHandler receiveMessage;
        /// <summary>
        /// 创建单例
        /// </summary>
        public static XzySocketServer GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockHelper)
                    {
                        if (_instance == null)
                        {
                            _instance = new XzySocketServer();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>  
        /// 监听客户端连接  
        /// </summary>  
        private void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                if (sockets.Where(p => p.ClientSocket.Equals(clientSocket)).ToList().Count == 0)
                {
                    LinkSocket link = new LinkSocket();
                    link.ClientSocket = clientSocket;
                    sockets.Add(link);
                    Thread receiveThread = new Thread(ReceiveMessage);
                    receiveThread.IsBackground = true;
                    receiveThread.Start(clientSocket);
                }
            }
        }
        /// <summary>
        /// 接受消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            StringBuilder data = new StringBuilder();
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据  
                    int receiveNumber = myClientSocket.Receive(result);
                    //判断客户端有内容
                    if (receiveNumber > 0)
                    {
                        LinkSocket tempSocket = sockets.Where(p => p.ClientSocket.Equals(myClientSocket)).ToList()?.First();
                        if (tempSocket != null)
                        {
                            if (tempSocket.Type == XzySocketType.Client || tempSocket.Type == XzySocketType.Null)
                            {
                                #region 判断来源于PC端socket
                                string str = Encoding.UTF8.GetString(result, 0, receiveNumber); 
                                //websocket握手
                                if (tempSocket.Type == XzySocketType.Null&&str.IndexOf("GET / HTTP/1.1") != -1)
                                {
                                    myClientSocket.Send(PackHandShakeData(GetSecKeyAccetp(result, receiveNumber)));
                                    continue;
                                }
                                data.Append(str);
                                if (str.IndexOf("@XEnd@")!=-1)
                                {
                                    string endStr = data.ToString();
                                    string[] pcstrs = endStr.Split(new string[] { "@Xzy@" }, StringSplitOptions.None);
                                    data = new StringBuilder();
                                    if (pcstrs[0].ToUpper().IndexOf("P_SETID") != -1)
                                    {
                                        tempSocket.Type = XzySocketType.Client;
                                        tempSocket.ID = pcstrs[1].Substring(0, pcstrs[1].Length - 6);
                                        continue;
                                    }
                                    else if (pcstrs[0].IndexOf("MSG") != -1)
                                    {
                                        receiveMessage(pcstrs[1].Substring(0, pcstrs[1].Length-6),tempSocket.ID);
                                        continue;
                                    }
                                }
                                #endregion
                            }
                            if (tempSocket.Type == XzySocketType.Web || tempSocket.Type == XzySocketType.Null)
                            {
                                #region 判断来源于websocket
                                string webstr = AnalyticData(result, receiveNumber);
                               
                                //可能是websocket 掉线
                                if (receiveNumber == 8 && webstr == "\u0003�")
                                {
                                    //websocket 掉线后如果还停留在扫码轮询的页面 发送给客户端中断线程。
                                    LinkSocket tempWeb = tempSocket;
                                    if (sockets.Where(p => p.ID.IndexOf(tempWeb.ID) != -1).ToList().Count > 0)
                                    {
                                        sockets.Remove(tempWeb);
                                        continue;
                                    }
                                }

                                data.Append(webstr);
                                if (webstr.IndexOf("@XEnd@") != -1)
                                {
                                    string endStr = data.ToString();
                                    string[] webstrs = endStr.Split(new string[] { "@Xzy@" }, StringSplitOptions.None);
                                    data = new StringBuilder();
                                    if (webstrs[0].ToUpper().IndexOf("W_SETID") != -1)
                                    {
                                        tempSocket.Type = XzySocketType.Web;
                                        tempSocket.ID = webstrs[1];
                                        continue;
                                    }
                                    else if (webstrs[0].IndexOf("MSG") != -1)
                                    {
                                        receiveMessage(webstrs[1].Substring(0, webstrs[1].Length-6), tempSocket.ID);
                                        continue;
                                    }
                                }
                                #endregion
                            }    
                        }
                    }
                    else
                    {
                        if (sockets.Where(p => p.ClientSocket.Equals(myClientSocket)).ToList().Count == 0)
                        {
                            //web端已经下线，中断线程
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (sockets.Where(p => p.ClientSocket.Equals(myClientSocket)).ToList().Count > 0)
                    {
                        LinkSocket socket = sockets.Where(p => p.ClientSocket.Equals(myClientSocket))?.First();
                        if (socket != null)
                        {
                            sockets.Remove(socket);
                        }          
                    }
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }
        #endregion

        #region 共有方法
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="port">端口</param>
        public void Init(IPAddress ip, int port)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, port));  //绑定IP地址：端口  
            serverSocket.Listen(100);    //设定最多100个排队连接请求  
            //通过Clientsoket发送数据  
            Thread myThread = new Thread(ListenClientConnect);
            myThread.IsBackground = true;
            myThread.Start();
        }
        /// <summary>
        /// 获取当前所有在线用户
        /// </summary>
        /// <returns></returns>
        public List<LinkSocket> GetAllList()
        {
            return sockets;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="socket">客户端socket对象</param>
        /// <param name="context">消息内容</param>
        public void SendMessage(LinkSocket socket,string context)
        {
            if (socket.Type==XzySocketType.Web)
            {
                socket.ClientSocket.Send(PackData(context + "@XEnd@"));
            }
            else if(socket.Type==XzySocketType.Client)
            {
                socket.ClientSocket.Send(Encoding.UTF8.GetBytes(context + "@XEnd@"));
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="id">根据唯一ID</param>
        /// <param name="context">内容</param>
        public void SendMessage(string id, string context)
        {
            LinkSocket socket = sockets.Where(p => p.ID == id).ToList()?.First();
            if(socket!=null)
            {
                if (socket.Type == XzySocketType.Web)
                {
                    socket.ClientSocket.Send(PackData(context + "@XEnd@"));
                }
                else if (socket.Type == XzySocketType.Client)
                {
                    socket.ClientSocket.Send(Encoding.UTF8.GetBytes(context + "@XEnd@"));
                }
            }
        }
        #endregion
    }
}
