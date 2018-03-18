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
            Init(DateTime.Now);
        }

        private void Init(DateTime? dtValue)
        {
            if (dtValue == null)
                dtValue = DateTime.Now;
            SelectDateTime = dtValue.Value;
            calendar.SelectedDate = SelectDateTime;
            TimeHour.Text = SelectDateTime.Hour.ToString("00");
            TimeMinutes.Text = SelectDateTime.Minute.ToString("00");
            TimeSeconds.Text = SelectDateTime.Second.ToString("00");
        }

        #region 属性

        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime SelectDateTime { get; set; }

        #endregion

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

        #endregion

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            popChioce.IsOpen = true;
            Init(SelectDateTime);
        }

        private void TimeOk_Click(object sender, RoutedEventArgs e)
        {
            string dateTimeStr = string.Format("{0} {1}:{2}:{3}", calendar.SelectedDate.Value.ToString("yyyy-MM-dd"), TimeHour.Text, TimeMinutes.Text, TimeSeconds.Text);
            SelectDateTime = Convert.ToDateTime(dateTimeStr);
            Part_DateTimePickerTextBlock.Text = SelectDateTime.ToString(DateTimeFormat);
            popChioce.IsOpen = false;
        }
    }
}
