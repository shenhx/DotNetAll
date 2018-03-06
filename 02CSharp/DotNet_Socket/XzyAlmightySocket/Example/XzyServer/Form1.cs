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

namespace XzyServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XzySocketServer.GetInstance.Init(IPAddress.Any, 22222);
            XzySocketServer.GetInstance.receiveMessage += myReceiveMessage;
        }

        private void myReceiveMessage(string msg,string id)
        {
            ListViewItem item = new ListViewItem(new string[] { DateTime.Now.ToString(),id, msg });
            this.listView1.Items.Insert(0, item);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string[] strs = comboBox1.SelectedValue.ToString().Split('|');
            XzySocketServer.GetInstance.SendMessage(strs[1], txtContext.Text);
        }

        /// <summary>
        /// 下拉框显示当前在线的链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            List<LinkSocket> sockets = XzySocketServer.GetInstance.GetAllList();
            List<string> temp = new List<string>();
            foreach (var s in sockets)
            {
                if (s.Type==XzySocketType.Web)
                {
                    temp.Add(s.ClientSocket.RemoteEndPoint.ToString() + " 网页端 ID|" + s.ID);
                }
                else if(s.Type==XzySocketType.Client)
                {
                    temp.Add(s.ClientSocket.RemoteEndPoint.ToString() + " PC端 ID|" + s.ID);
                }
            }       
            comboBox1.DataSource = temp;
        }
    }
}
