using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XzySocketClient;
using XzySocketClient.Messaging;
using XzySocketClient.Protocol;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string MyId = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            MyId=Guid.NewGuid().ToString();
        }

        static private long index = 0;
        SocketClient<CommandLineMessage> client;
        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(txtAddress.Text);
            int port = int.Parse(txtPort.Text);
            client = new SocketClient<CommandLineMessage>(new CommandLineProtocol(), 1024, 1024, 3000, 3000);
            client.TryRegisterEndPoint(MyId, new[] { new IPEndPoint(ip, port) },
                    connection =>
                    {
                        var source = new TaskCompletionSource<bool>();
                        var request = client.NewRequest("init", Encoding.UTF8.GetBytes("init" + System.Environment.NewLine), 3000,
                            ex => source.TrySetException(ex),
                            message => source.TrySetResult(true));
                        connection.BeginSend(request);
                        return source.Task;
                    });

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => Do(client,txtContext.Text));
        }

        static private async Task Do(SocketClient<CommandLineMessage> client,string context)
        {
            try {
                (await Echo(client, context)).ToString();
            }
            catch (Exception ex)
            {
                
            }
        }

        static public Task<bool> Echo(SocketClient<CommandLineMessage> client,string context)
        {
            var source = new TaskCompletionSource<bool>();
            var guid = Guid.NewGuid().ToString();
            client.Send(client.NewRequest("message", Encoding.UTF8.GetBytes("echo" + guid + System.Environment.NewLine), 3000,
                ex => source.TrySetException(ex),
                message => source.TrySetResult(message.Parameters[0] == guid)));
            return source.Task;
        }
    }
}
