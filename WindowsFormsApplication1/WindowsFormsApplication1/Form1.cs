using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextReader txtReader = new StreamReader("xxpacs.xml",Encoding.UTF8);
            StringBuilder sbResult = new StringBuilder();
            string content = string.Empty;
            while( !string.IsNullOrEmpty(content = txtReader.ReadLine()))
            {
                if (content.Contains("result") && content.Contains("column"))
                {
                    string[] arr = content.Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries);
                    sbResult.Append(content.Replace("column=\"\"", arr[1].Replace("property", "column"))).AppendLine();
                }
                else
                {
                    sbResult.Append(content).AppendLine();
                }
                
            }
            textBox1.Text = sbResult.ToString();
            txtReader = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("xxpacs1.xml",false))
            {
                sw.WriteLine(textBox1.Text);
                sw.Flush();
            }
        }
    }
}
