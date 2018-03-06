using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XzySocketBase;
using XzySocketServer;
using XzySocketServer.Messaging;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            SocketServerManager.Init();
            SocketServerManager.Start();

            //每隔10秒强制断开所有连接
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1000 * 10);
                    IHost host;
                    if (SocketServerManager.TryGetHost("quickStart", out host))
                    {
                        var arr = host.ListAllConnection();
                        foreach (var c in arr) c.BeginDisconnect();
                    }
                }
            });

        }
    }

    public class MyService : AbsSocketService<CommandLineMessage>
    {
        public override void OnConnected(IConnection connection)
        {
            base.OnConnected(connection);
           
            connection.BeginReceive();
        }

        public override void OnReceived(IConnection connection, CommandLineMessage message)
        {
            base.OnReceived(connection, message);

            switch (message.CmdName)
            {
                case "message":
                    message.Reply(connection, "echo_reply " + message.Parameters[0]);
                    break;
                case "init":
                    message.Reply(connection, "init_reply ok");
                    break;
                default:
                    message.Reply(connection, "error unknow command ");
                    break;
            }
        }

        public override void OnDisconnected(IConnection connection, Exception ex)
        {
            base.OnDisconnected(connection, ex);   
        }

        public override void OnException(IConnection connection, Exception ex)
        {
            base.OnException(connection, ex);
        }
    }

}
