using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplicationDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                int tmp = i;
                ThreadPool.QueueUserWorkItem((obj) => {
                    SingletonClass.Instance().OutPut(tmp);
                });
            }
        }
    }


    internal class SingletonClass
    {
        private SingletonClass() { }

        private readonly static object _syncRoot = new object();
        private static SingletonClass _singletonClass = null;

        public static SingletonClass Instance()
        {
            if(_singletonClass == null)
            {
                lock (_syncRoot)
                {
                    if (_singletonClass == null)
                    {
                        _singletonClass = new SingletonClass();
                    }
                }
            }
            return _singletonClass;
        }

        public void OutPut(int i)
        {
            Thread.Sleep(5000);
            Console.WriteLine("时间：{0},调用ID：{1}，线程ID：{2}", DateTime.Now, i, Thread.GetDomainID());
        }
    }
}
