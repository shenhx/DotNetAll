using System;
using System.Text;
using XzySocketBase;

namespace XzySocketServer.Messaging
{
    /// <summary>
    /// command line message.
    /// </summary>
    public class CommandLineMessage : Messaging.IMessage
    {
        #region Public Members
        /// <summary>
        /// get the current command name.
        /// </summary>
        public readonly string CmdName;
        /// <summary>
        /// 参数
        /// </summary>
        public readonly string[] Parameters;
        #endregion

        #region Constructors
        /// <summary>
        /// new
        /// </summary>
        /// <param name="cmdName"></param>
        /// <param name="parameters"></param>
        /// <exception cref="ArgumentNullException">cmdName is null</exception>
        public CommandLineMessage(string cmdName, params string[] parameters)
        {
            if (cmdName == null) throw new ArgumentNullException("cmdName");

            this.CmdName = cmdName;
            this.Parameters = parameters;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// reply
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException">connection is null</exception>
        public void Reply(IConnection connection, string value)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            connection.BeginSend(ToPacket(value));
        }
        /// <summary>
        /// to <see cref="SocketBase.Packet"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">value is null</exception>
        static public Packet ToPacket(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            return new Packet(Encoding.UTF8.GetBytes(string.Concat(value, Environment.NewLine)));
        }
        #endregion
    }
}