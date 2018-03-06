using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XzySocketCore
{
    public class XzySocketClient
    {
        #region 私有变量
        Socket clientSocket;
        private static byte[] result = new byte[1024];
        IPAddress ip;
        int port;
        string id;
        #endregion

        #region 共有变量
        /// <summary>
        /// 掉线重练时间默认1000毫秒
        /// </summary>
        public int reconnectTime = 1000;
        /// <summary>
        /// 接受消息的委托
        /// </summary>
        /// <param name="msg"></param>
        public delegate void ReceiveMessageHandler(string msg);
        public event ReceiveMessageHandler receiveMessage;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">端口</param>
        /// <param name="id">客户端ID</param>
        public XzySocketClient(IPAddress ip, int port, string id) {
            this.ip = ip;
            this.port = port;
            this.id = id;
        }

        #region 私有方法
        /// <summary>
        /// 掉线重连
        /// </summary>
        /// <returns></returns>
        private bool Reconnect()
        {
            try
            {
                Thread.Sleep(reconnectTime);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(ip, port));
                clientSocket.Send(Encoding.UTF8.GetBytes("P_SETID@Xzy@" +id+ "@XEnd@"));
                return true;
            }
            catch (Exception ex1)
            {
                return false;
            }
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init() {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 22222)); //配置服务器IP与端口  
                clientSocket.Send(Encoding.UTF8.GetBytes("P_SETID@Xzy@" + id+ "@XEnd@"));
                StringBuilder data = new StringBuilder();
                Task receiveThread = Task.Run(() =>
                {
                    while (true)
                    {
                        try
                        {
                            int receiveLength = clientSocket.Receive(result);
                            if (receiveLength > 0)
                            {
                                string str = Encoding.UTF8.GetString(result, 0, receiveLength);
                                data.Append(str);
                                if (str.IndexOf("@XEnd@") != -1)
                                {
                                    string endStr = data.ToString();
                                    data = new StringBuilder();
                                    receiveMessage(endStr.Substring(0, endStr.Length -6));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            while (!Reconnect())
                            {

                            }
                        }
                    }
                });
            }
            catch
            {
                Task receiveThread = Task.Run(() =>
                {
                    while (!Reconnect())
                    {

                    }
                });
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="context">消息内容</param>
        public void SendMessage(string context)
        {
            clientSocket.Send(Encoding.UTF8.GetBytes("MSG@Xzy@" + context+"@XEnd@"));
        }
        #endregion 
    }
}
