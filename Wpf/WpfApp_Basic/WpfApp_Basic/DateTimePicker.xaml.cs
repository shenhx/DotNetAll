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
using System.Drawing;

namespace WpfApp_Basic
{

    //[ToolboxBitmap(typeof(DateTimePicker), "DateTimePicker.bmp")]
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>    
    public partial class DateTimePicker : UserControl
    {
        public DateTimePicker()
        {
            InitializeComponent();
        }

        #region 日期格式
        public static readonly DependencyProperty DateTimeFormatProperty =
            DependencyProperty.Register("DateTimeFormat",
            typeof(string),
            typeof(DateTimePicker),
            new PropertyMetadata("yyyy-MM-dd hh:mm:ss", OnFormatChanged));
        public string DateTimeFormat
        {
            set { SetValue(DateTimeFormatProperty, value); }
            get { return (string)GetValue(DateTimeFormatProperty); }
        }
        private static void OnFormatChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if(!"yyyy-MM-dd hh:mm:ss".Equals(args.NewValue) && !"yyyy/MM/dd hh:mm:ss".Equals(args.NewValue))
            {
                throw new Exception("日期格式不对，请检查！");
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txt"></param>
        public DateTimePicker(string txt)
            : this()
        {
        }

        #region 事件

        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (popChioce.IsOpen == true)
            {
                popChioce.IsOpen = false;
            }
            TDateTimeView dtView = new TDateTimeView(textBlock1.Text);// TDateTimeView  构造函数传入日期时间
            dtView.DateTimeOK += (dateTimeStr) => //TDateTimeView 日期时间确定事件
            {
                textBlock1.Text = dateTimeStr;
                DateTime = Convert.ToDateTime(dateTimeStr);
                popChioce.IsOpen = false;//TDateTimeView 所在pop  关闭
            };
            popChioce.Child = dtView;
            popChioce.IsOpen = true;
        }

        /// <summary>
        /// DateTimePicker 窗体登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            textBlock1.Text = dt.ToString(DateTimeFormat);
            DateTime = dt;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime DateTime { get; set; }

        #endregion
    }
}