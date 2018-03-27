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
    /// Window_Command.xaml 的交互逻辑
    /// </summary>
    public partial class Window_Command : Window
    {
        public Window_Command()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtText.Text))
            {
                e.CanExecute = false;
                return;
            }
           e.CanExecute = true;
            
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(txtText.Text);
        }

        private void CommandBinding_Executed2(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(txtText.Text+"nihao");
            //MessageBox.Show(e.Parameter.ToString());
        }
    }

    public class MyDataCommand
    {
        public static RoutedUICommand _enterQuery;

        static MyDataCommand()
        {
            InputGestureCollection inputGestureCollection = new InputGestureCollection();
            inputGestureCollection.Add(new KeyGesture(Key.Enter, ModifierKeys.None));
            _enterQuery = new RoutedUICommand("EnterQuery", "EnterQuery", typeof(MyDataCommand), inputGestureCollection);
        }

        /// <summary>
        /// 回车搜索命令
        /// </summary>
        public static RoutedUICommand EnterQuery
        {
            get { return _enterQuery; }
        }
    }
}
