using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XzySocketCore;

namespace XzyClient
{
    public partial class Form1 : Form
    {
        XzySocketClient socketClient;
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            socketClient.SendMessage(txtContext.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(txtIp.Text);
            socketClient = new XzySocketClient(ip, 22222, Guid.NewGuid().ToString());
            socketClient.Init();
            socketClient.receiveMessage += receiveMessage;
        }

        /// <summary>
        /// 接收服务端消息
        /// </summary>
        /// <param name="msg"></param>
        private void receiveMessage(string msg)
        {
            ListViewItem item = new ListViewItem(new string[] { DateTime.Now.ToString(), msg });
            this.listView1.Items.Insert(0, item);
        }
    }
}
