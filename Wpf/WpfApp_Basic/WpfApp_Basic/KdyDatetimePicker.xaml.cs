using System;
using System.Collections.Generic;
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
    /// KdyDatetimePicker.xaml 的交互逻辑
    /// </summary>
    [ToolboxBitmap(typeof(KdyDatetimePicker), "time.png")]
    public partial class KdyDatetimePicker : UserControl
    {
        public KdyDatetimePicker()
        {
            InitializeComponent();
            Init();
            this.DataContext = this;
        }

        private void Init()
        {
            if (Value == null)
                Value = DateTime.Now;
            calendar.SelectedDate = Value;
            TimeHour.Text = Value.Hour.ToString("00");
            TimeMinutes.Text = Value.Minute.ToString("00");
            TimeSeconds.Text = Value.Second.ToString("00");
        }

        #region 依赖属性

        #region 日期格式
        public static readonly DependencyProperty DateTimeFormatProperty =
            DependencyProperty.Register("DateTimeFormat",
            typeof(string),
            typeof(KdyDatetimePicker),
            new PropertyMetadata("yyyy-MM-dd HH:mm:ss", OnFormatChanged));
        public string DateTimeFormat
        {
            set { SetValue(DateTimeFormatProperty, value); }
            get { return (string)GetValue(DateTimeFormatProperty); }
        }

        private static void OnFormatChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (!"yyyy-MM-dd HH:mm:ss".Equals(args.NewValue) && !"yyyy/MM/dd HH:mm:ss".Equals(args.NewValue))
            {
                throw new Exception("日期格式不对，请检查！");
            }
        }
        #endregion

        #region 时间控件的值

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(DateTime?), typeof(KdyDatetimePicker),
            new PropertyMetadata(DateTime.Now, (s, e) =>
            {
                //其他逻辑
                DateTime dtNewValue;
                if(e.NewValue != null)
                {
                    DateTime.TryParse(e.NewValue.ToString(), out dtNewValue);
                    if(dtNewValue == null)
                    {
                        throw new Exception("日期设置不对，请检查！");
                    }
                }
            }));

        public string DateTimeFormatValue
        {
            get { return Value.ToString(DateTimeFormat); }
        }
        
        /// <summary>
        /// 设置或者获取日期时间的值
        /// </summary>
        public DateTime Value
        {
            set
            {
                SetValue(ValueProperty, value);
            }
            get
            {
                return (DateTime)GetValue(ValueProperty);
            }
        }

        #endregion

        #endregion

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            popChioce.IsOpen = true;
            Init();
        }

        private void TimeOk_Click(object sender, RoutedEventArgs e)
        {
            string dateTimeStr = string.Format("{0} {1}:{2}:{3}", calendar.SelectedDate.Value.ToString("yyyy-MM-dd"), TimeHour.Text, TimeMinutes.Text, TimeSeconds.Text);
            Value = Convert.ToDateTime(dateTimeStr);
            Part_DateTimePickerTextBlock.Text = Value.ToString(DateTimeFormat);
            popChioce.IsOpen = false;
        }
    }
}
