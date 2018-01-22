using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
            //List<string> list = new List<string>();
            //list.Add("nema");
            //list.Add("nema1");
            //list.Add("nema2");
            //list.Add("nema3");
            ////第一种方式
            //StringBuilder sb = new StringBuilder();
            //list.ForEach(p => 
            //{
            //    if (sb.Length > 0) sb.Append(",");
            //    sb.Append(p);
            //});
            //Console.WriteLine(sb.ToString());
            ////第二种方式
            //Console.WriteLine(string.Join(",",list.ToArray()));
        }
    }
}
