using System.Configuration;

namespace XzySocketServer.Config
{
    /// <summary>
    /// socket server config.
    /// </summary>
    public class SocketServerConfig : ConfigurationSection
    {
        /// <summary>
        /// 服务器集合。
        /// </summary>
        [ConfigurationProperty("servers", IsRequired = true)]
        public ServerCollection Servers
        {
            get { return this["servers"] as ServerCollection; }
        }
    }
}