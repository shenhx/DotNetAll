using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace XzySocketCore
{
    public enum XzySocketType
    {
        Null=0,
        Client = 1,
        Web = 2,
    }
    /// <summary>
    /// 记录链接socket对象
    /// </summary>
    public class LinkSocket
    {
        /// <summary>
        /// socket对象
        /// </summary>
        public Socket ClientSocket = null;
        /// <summary>
        /// 判断是web还是pc
        /// </summary>
        public XzySocketType Type = XzySocketType.Null;
        /// <summary>
        /// 客户端ID需要保持唯一
        /// </summary>
        public string ID = "";
    }
}
