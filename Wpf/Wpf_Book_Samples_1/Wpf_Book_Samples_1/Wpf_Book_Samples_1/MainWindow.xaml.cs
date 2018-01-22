using Sample.DependencyObjectTest;
using Sample.RouteEventTest;
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
            this.SayHello += MainWindow_SayHello;
            ////引发路由事件
            RoutedEventArgs e = new RoutedEventArgs(MyRouteEvent.SayHelloEvent, this);
            base.RaiseEvent(e);
        }

        private void MainWindow_SayHello(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
            //throw new Exception("qqq");
        }

        //一般在自定义控件中使用，在这里定义会出现线程占用问题。
        ////第二步：注册依赖项属性
        //static MainWindow()
        //{
        //    //共享的依赖属性
        //    MainWindow.IsDefaultProperty = MyDependencyObject.IsDefaultProperty.AddOwner(typeof(MainWindow));

        //    //共享路由事件
        //    MainWindow.SayHelloEvent = MyRouteEvent.SayHelloEvent.AddOwner(typeof(MainWindow));
        //}

        #region 依赖属性
        //第一步：定义依赖属性(属性名称+Property)
        public static readonly DependencyProperty IsDefaultProperty;

        

        //第三步：添加属性包装器
        public bool IsDefault
        {
            get { return (bool)GetValue(IsDefaultProperty); }
            set { SetValue(IsDefaultProperty, value); }
        }
        #endregion

        #region 共享路由事件
        //第一步：定义路由事件
        public static readonly RoutedEvent SayHelloEvent;
        
        //按传统方式封装事件
        public event RoutedEventHandler SayHello
        {
            add { base.AddHandler(MyRouteEvent.SayHelloEvent, value); }
            remove { base.RemoveHandler(MyRouteEvent.SayHelloEvent, value); }
        }
        #endregion

        /// <summary>
        /// todo:需要改造成命令模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window_Binding binding = new Window_Binding();
            binding.ShowDialog();
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
                        Window_Binding binding = new Window_Binding();
                        binding.ShowDialog();
                        break;
                    }
                case "Command":
                    {
                        Window_Command command= new Window_Command();
                        command.ShowDialog();
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
