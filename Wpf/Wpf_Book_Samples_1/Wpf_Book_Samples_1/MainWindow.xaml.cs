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

namespace Wpf_Book_Samples_1
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
        
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter != null && !string.IsNullOrEmpty(e.Parameter.ToString()))
            {
                e.CanExecute = true;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (e.Parameter)
            {
                case "Binding":
                    {
                        break;
                    }
                case "Command":
                    {
                        break;
                    }
                case "DeepInAnimation":
                    {
                        Window_DeepInAnimation deepInAnimation = new Window_DeepInAnimation();
                        deepInAnimation.ShowDialog();
                        break;
                    }
                case "ControlTemplate":
                    {
                        Window_ControlTemplate _ControlTemplate = new Window_ControlTemplate();
                        _ControlTemplate.ShowDialog();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("没有找到合适的窗口");
                        break;
                    }
            }
            
        }
    }
}
