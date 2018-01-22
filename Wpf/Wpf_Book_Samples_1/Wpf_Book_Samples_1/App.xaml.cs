using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Wpf_Book_Samples_1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 存在问题：就是当e.Handled=true的时候，原来的界面不见了，变成后台进程退不出。
        /// 解决办法：研究微软企业库，看看统一错误处理程序是如何监控并捕获到错误消息的。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
            //e.Handled = false;
        }
    }
}
