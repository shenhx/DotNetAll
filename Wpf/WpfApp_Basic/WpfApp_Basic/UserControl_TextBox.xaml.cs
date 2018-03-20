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

namespace WpfApp_Basic
{
    /// <summary>
    /// UserControl_TextBox.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_TextBox : UserControl
    {
        public UserControl_TextBox()
        {
            InitializeComponent();
        }

        public DependencyProperty WaterTextProperty = DependencyProperty.Register("WaterText",typeof(string),typeof(UserControl_TextBox));
        /// <summary>
        /// 水印文字
        /// </summary>
        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty,value); }
        }

        public DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(UserControl_TextBox));
        /// <summary>
        /// 文本控件的值
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private void btnClearText_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
