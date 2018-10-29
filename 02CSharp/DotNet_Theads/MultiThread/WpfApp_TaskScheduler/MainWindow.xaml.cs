using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Threading;

namespace WpfApp_TaskScheduler
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Task<string> TaskMethod()
        {
            return TaskMethod(TaskScheduler.Default);
        }

        private void btnSync_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = string.Empty;
            try
            {
                string result = TaskMethod().Result;
                txtContent.Text = result;
            }
            catch (Exception ex)
            {
                txtContent.Text = ex.InnerException.Message;
            }
        }

        private void btnAsync_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = string.Empty;
            Mouse.OverrideCursor = Cursors.Wait;
            Task<string> task = TaskMethod();
            task.ContinueWith(t =>
            {
                txtContent.Text = t.Exception.InnerException.Message;
                Mouse.OverrideCursor = null;
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void btnSyncOk_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = string.Empty;
            Mouse.OverrideCursor = Cursors.Wait;
            Task<string> task = TaskMethod(TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith(t => Mouse.OverrideCursor = null, CancellationToken.None,
                TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private Task<string> TaskMethod(TaskScheduler taskScheduler)
        {
            Task delay = Task.Delay(TimeSpan.FromSeconds(5));
            return delay.ContinueWith(t =>
            {
                string str = string.Format("{0} {1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
                txtContent.Text = str;
                return str;
            }, taskScheduler);
        }
    }
}
