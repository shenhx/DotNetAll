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
using System.Windows.Shapes;

namespace WpfApp_Basic
{
    /// <summary>
    /// Window_TextBox.xaml 的交互逻辑
    /// </summary>
    public partial class Window_TextBox : Window
    {
        public Window_TextBox()
        {
            InitializeComponent();
        }

        private void KdyTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(txtHello.Text+"---"+txtHello);
        }
    }
}
