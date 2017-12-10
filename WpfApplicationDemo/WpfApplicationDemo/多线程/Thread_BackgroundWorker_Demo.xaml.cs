using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplicationDemo
{
    /// <summary>
    /// Thread_BackgroundWorker_Demo.xaml 的交互逻辑
    /// </summary>
    public partial class Thread_BackgroundWorker_Demo : Window
    {
        private BackgroundWorker _backgroundworker = null; 
        public Thread_BackgroundWorker_Demo()
        {
            InitializeComponent();
            _backgroundworker = (BackgroundWorker)this.FindResource("backgroundWorker");
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            FindPrimesInput input = (FindPrimesInput)e.Argument;
            int[] primes = Worker.FindPrimes(input.From,input.To);
            e.Result = primes;
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressbar.Value = e.ProgressPercentage;
        }

        private void cmdFind_Click(object sender, RoutedEventArgs e)
        {
            cmdFind.IsEnabled = false;
            cmdCancel.IsEnabled = true;
            txtResult.Text = string.Empty;
            int from, to = 0;
            if (!Int32.TryParse(txtFrom.Text, out from))
            {
                MessageBox.Show("Invalid From Value!");
                return;
            }
            if (!Int32.TryParse(txtTo.Text, out to))
            {
                MessageBox.Show("Invalid To Value!");
                return;
            }
            FindPrimesInput input = new FindPrimesInput(from,to);
            _backgroundworker.RunWorkerAsync(input);
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            _backgroundworker.CancelAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Invalid From Value!");
            }
            else
            {
                int[] primes = (int[])e.Result;
                foreach (var prime in primes)
                {
                    txtResult.Text += prime + ",";
                }
            }
            cmdFind.IsEnabled = true;
            cmdCancel.IsEnabled = false;
            progressbar.Value = 0;
        }
    }
}
