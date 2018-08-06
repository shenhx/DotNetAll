using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Threading;

namespace BenchmarkDotNetTest
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

        private bool _canExecute = true;

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _canExecute;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _canExecute = false;
            try
            {
                var summary = BenchmarkRunner.Run<BenchMarkBussinessTest>();
                
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                _canExecute = true;
            }
        }
    }

    public class BenchMarkBussinessTest
    {
        [Benchmark]
        public void SleepLongTime()
        {
            Thread.Sleep(2990);
        }

    }
}
