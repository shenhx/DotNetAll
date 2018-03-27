using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
    /// KdyTextBox.xaml 的交互逻辑
    /// </summary>
    [ToolboxBitmap(typeof(KdyTextBox))]
    public partial class KdyTextBox : UserControl, INotifyPropertyChanged, ICommandSource
    {
        public KdyTextBox()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        static KdyTextBox()
        {
            DependencyProperty.RegisterAttached("TextAlignment", typeof(TextAlignment),typeof(KdyTextBox));
            //DependencyProperty.RegisterAttached("Height", typeof(double),typeof(KdyTextBox));
            //DependencyProperty.RegisterAttached("Width", typeof(double),typeof(KdyTextBox));
        }

        public TextAlignment TextAlignment { get; set; }
        //public double Height { get; set; }
        //public double Width { get; set; }

        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register("PlaceHolder", typeof(string), typeof(KdyTextBox), new PropertyMetadata("请输入内容..."));
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public string Text
        {
            get { return txtControl.Text; }
        }

        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public IInputElement CommandTarget { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class TextBoxHelper
    {
        #region 附加属性 IsClearButton

        /// <summary>
        /// 附加属性，是否带清空按钮
        /// </summary>
        public static readonly DependencyProperty IsClearButtonProperty =
            DependencyProperty.RegisterAttached("IsClearButton", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false, ClearText));


        public static bool GetIsClearButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsClearButtonProperty);
        }

        public static void SetIsClearButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsClearButtonProperty, value);
        }

        #endregion

        #region 回调函数和清空输入框内容的实现
        /// <summary>
        /// 回调函数若附加属性IsClearButton值为True则挂载清空TextBox内容的函数
        /// </summary>
        /// <param name="d">属性所属依赖对象</param>
        /// <param name="e">属性改变事件参数</param>
        private static void ClearText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button btn = d as Button;
            if (d != null && e.OldValue != e.NewValue)
            {
                btn.Click -= ClearTextClicked;
                if ((bool)e.NewValue)
                {
                    btn.Click += ClearTextClicked;
                }
            }
        }

        /// <summary>
        /// 清空应用该附加属性的父TextBox内容函数
        /// </summary>
        /// <param name="sender">发送对象</param>
        /// <param name="e">路由事件参数</param>
        public static void ClearTextClicked(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                var parent = VisualTreeHelper.GetParent(btn);
                while (!(parent is TextBox))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                TextBox txt = parent as TextBox;
                if (txt != null)
                {
                    txt.Clear();
                }
            }
        }

        #endregion
    }
}
