using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NlogToMySql
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

            // 初始化nlog组件信息
            var log = NLog.LogManager.GetCurrentClassLogger();
            log.Error("niijffdfdf");

            Application.Run(new Form1());
        }
    }
}
