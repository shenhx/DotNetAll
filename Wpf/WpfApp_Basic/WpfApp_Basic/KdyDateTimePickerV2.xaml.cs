using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    /// KdyDateTimePickerV2.xaml 的交互逻辑
    /// </summary>
    public partial class KdyDateTimePickerV2 : UserControl,INotifyPropertyChanged
    {
        public KdyDateTimePickerV2()
        {
            InitializeComponent();
            Init_Command();
            Init_Control();
            this.DataContext = this;
        }

        private void Init_Control()
        {
            if ("yyyy-MM-dd".Equals(DateTimeFormat) //短日期
            || "yyyy/MM/dd".Equals(DateTimeFormat))
            {
                spTimeControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                spTimeControl.Visibility = Visibility.Visible;
            }
            if (Value == null)
                Value = DateTime.Now;
        }

        #region 初始化Command对象
        private void Init_Command()
        {
            YearCommand = new DelegateCommand();
            YearCommand.ExecuteAction = new Action<object>(OperateYearCommnad);

            MonthCommand = new DelegateCommand();
            MonthCommand.ExecuteAction = new Action<object>(OperaterMonthCommand);

            ChooseDateCommand = new DelegateCommand();
            ChooseDateCommand.ExecuteAction = new Action<object>(ChooseCurrentDate);

            HideCalendarCommand = new DelegateCommand();
            HideCalendarCommand.ExecuteAction = new Action<object>(DateTimeSelect_Completed);
        }

        /// <summary>
        /// 上下年浏览
        /// </summary>
        /// <param name="parameter"></param>
        private void OperateYearCommnad(object parameter)
        {
            if ("Pre".Equals(parameter))
            {
                CurrentYear -= 1;
            }
            else if ("Next".Equals(parameter))
            {
                CurrentYear += 1;
            }
            else
            {
                //do nothing now...
            }
        }

        /// <summary>
        /// 上下月浏览
        /// </summary>
        /// <param name="parameter"></param>
        private void OperaterMonthCommand(object parameter)
        {
            if ("Pre".Equals(parameter))
            {
               CurrentMonth -= 1;
            }
            else if ("Next".Equals(parameter))
            {
                CurrentMonth += 1;
            }
            else
            {
                //do nothing now...
            }
        }

        /// <summary>
        /// 选中缓存当前日期
        /// </summary>
        /// <param name="parameter"></param>
        private void ChooseCurrentDate(object parameter)
        {
            string dateString = string.Format("{0}-{1}-{2}",CurrentYear,CurrentMonth,parameter);
            SelectDate = Convert.ToDateTime(dateString);
        }

        /// <summary>
        /// 点击确定/取消
        /// </summary>
        /// <param name="parameter"></param>
        private void DateTimeSelect_Completed(object parameter)
        {
            if ("Ok".Equals(parameter))
            {
                string dateString = string.Format("{0} {1}:{2}:{3}", SelectDate.ToString("yyyy-MM-dd"),HourText,MinuteText,SecondText);
                Value = Convert.ToDateTime(dateString);
            }
            IsDateTimeControlOpen = false;
        }

        #endregion

        #region 依赖属性

        #region 日期格式
        public static readonly DependencyProperty DateTimeFormatProperty =
            DependencyProperty.Register("DateTimeFormat",
            typeof(string),
            typeof(KdyDateTimePickerV2),
            new PropertyMetadata("yyyy-MM-dd HH:mm:ss", OnFormatChanged));
        public string DateTimeFormat
        {
            set { SetValue(DateTimeFormatProperty, value); }
            get { return (string)GetValue(DateTimeFormatProperty); }
        }
        
        private static void OnFormatChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (!"yyyy-MM-dd HH:mm:ss".Equals(args.NewValue)//24小时制度
                && !"yyyy/MM/dd HH:mm:ss".Equals(args.NewValue)
                && !"yyyy-MM-dd hh:mm:ss".Equals(args.NewValue)//12小时制度
                && !"yyyy/MM/dd hh:mm:ss".Equals(args.NewValue)
                && !"yyyy/MM/dd".Equals(args.NewValue) //短日期
                && !"yyyy-MM-dd".Equals(args.NewValue))
            {
                throw new Exception("日期格式不对，请检查！");
            }
        }
        #endregion

        #region 时间控件的值

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(DateTime), typeof(KdyDateTimePickerV2),
            new PropertyMetadata(DateTime.Now, (s, e) =>
            {
                //其他逻辑
                DateTime dtNewValue;
                if (e.NewValue != null)
                {
                    DateTime.TryParse(e.NewValue.ToString(), out dtNewValue);
                    if (dtNewValue == null)
                    {
                        throw new Exception("日期设置不对，请检查！");
                    }
                }
            }));
        
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
                return (DateTime)GetValue(ValueProperty);;
            }
        }

        #endregion

        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            CutoffDayBegin = DateTime.Now.AddDays(-7);
            CutoffDayEnd = DateTime.Now.AddDays(7);
            ConvertBack_ChooseDateTime();
            IsDateTimeControlOpen = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region 属性
        private bool _isDateTimeControlOpen;
        public bool IsDateTimeControlOpen
        {
            get { return _isDateTimeControlOpen; }
            set
            {
                _isDateTimeControlOpen = value;
                OnPropertyChanged("IsDateTimeControlOpen");
            }
        }

        public ObservableCollection<DateTime> _days = new ObservableCollection<DateTime>();
        /// <summary>
        /// 日期，显示7*6天
        /// </summary>
        public ObservableCollection<DateTime> Days
        {
            get
            {
                return _days;
            }
        }

        public string DateTimeFormatValue
        {
            get { return Value.ToString(DateTimeFormat); }
        }

        private int _currentYear = DateTime.Now.Year;
        /// <summary>
        /// 当前浏览的年份
        /// </summary>
        public int CurrentYear
        {
            get
            {
                return _currentYear;
            }
            set
            {
                if (_currentYear != value && value > 1978 && value < 9999)
                {
                    _currentYear = value;
                    OnPropertyChanged("CurrentYear");
                    Init_Calendar(_currentYear, CurrentMonth);
                }
            }
        }

        private int _currentMonth = 1;
        /// <summary>
        /// 当前浏览的月份
        /// </summary>
        public int CurrentMonth
        {
            get
            {
                return _currentMonth;
            }
            set
            {
                if (_currentMonth != value && value < 13 && value > 0)
                {
                    _currentMonth = value;
                    OnPropertyChanged("CurrentMonth");
                    Init_Calendar(CurrentYear, _currentMonth);
                }
            }
        }

        private string _hourText;
        /// <summary>
        /// 显示时
        /// </summary>
        public string HourText
        {
            get { return _hourText; }
            set
            {
                _hourText = value;
                OnPropertyChanged("HourText");
            }
        }
        private string _minuteText;
        /// <summary>
        /// 显示分
        /// </summary>
        public string MinuteText
        {
            get { return _minuteText; }
            set
            {
                _minuteText = value;
                OnPropertyChanged("MinuteText");
            }
        }
        private string _secondText;
        /// <summary>
        /// 显示秒
        /// </summary>
        public string SecondText
        {
            get { return _secondText; }
            set
            {
                _secondText = value;
                OnPropertyChanged("SecondText");
            }
        }

        /// <summary>
        /// 当前选中的日期
        /// </summary>
        public DateTime SelectDate { get; set; }

        private DateTime _cutoffDayBegin;
        public DateTime CutoffDayBegin
        {
            get { return _cutoffDayBegin; }
            set
            {
                _cutoffDayBegin = value;
                OnPropertyChanged("CutoffDayBegin");
            }
        }

        private DateTime _cutoffDayEnd;
        public DateTime CutoffDayEnd
        {
            get { return _cutoffDayEnd; }
            set
            {
                _cutoffDayEnd = value;
                OnPropertyChanged("CutoffDayEnd");
            }
        }
        #endregion

        #region 事件绑定
        /// <summary>
        /// 上一年/下一年
        /// </summary>
        public DelegateCommand YearCommand { get; set; }

        /// <summary>
        /// 上一个月/下一个月
        /// </summary>
        public DelegateCommand MonthCommand { get; set; }

        /// <summary>
        /// 选择日期
        /// </summary>
        public DelegateCommand ChooseDateCommand { get; set; }

        /// <summary>
        /// 确定/取消
        /// </summary>
        public DelegateCommand HideCalendarCommand { get; set; }

        #endregion

        /// <summary>
        /// 格式化日期控件
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        private void Init_Calendar(int year, int month)
        {
            _days.Clear();
            DateTime datetime = new DateTime(year, month, 1);
            int week = (int)datetime.DayOfWeek;
            datetime = datetime.AddDays(1 - week);
            for (int i = 0; i < 42; i++)
            {
                _days.Add(datetime.AddDays(i));
            }
            OnPropertyChanged("Days");
        }

        /// <summary>
        /// 格式化显示时间控件
        /// </summary>
        /// <param name="dt"></param>
        private void Init_TimeCtrl(DateTime dt)
        {
            HourText = dt.Hour.ToString().PadLeft(2, '0');
            MinuteText = dt.Minute.ToString().PadLeft(2, '0');
            SecondText = dt.Second.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 将已有的日期时间反转为显示
        /// </summary>
        /// <param name="obj"></param>
        public void ConvertBack_ChooseDateTime()
        {
            DateTime dt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(DateTimeFormatValue))
            {
                if(!DateTime.TryParse(DateTimeFormatValue, out dt))
                {
                    dt = DateTime.Now;
                }
            }
            if (dt == DateTime.MinValue)
            {
                dt = DateTime.Now;
            }
            SelectDate = dt;
            CurrentMonth = dt.Month;
            CurrentYear = dt.Year;
            Init_TimeCtrl(dt);//初始化时间控件
        }
    }

    #region ViewModel相关信息

    /// <summary>
    /// 命令代理类型
    /// </summary>
    public class DelegateCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            if (this.CanExecuteFunc == null)
            {
                return true;
            }

            return this.CanExecuteFunc(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (this.ExecuteAction == null)
            {
                return;
            }
            this.ExecuteAction(parameter);
        }

        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteFunc { get; set; }
    }

    #endregion


    #region 转换类
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeToForegroundMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dt = new DateTime();
            if (values[0] is DateTime)
            {
                dt = (DateTime)values[0];
            }
            int currentMonth;
            Int32.TryParse(values[1].ToString(), out currentMonth);
            if (dt.Month == currentMonth)
            {
                if (dt.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    return new SolidColorBrush(Colors.White);
                }
                else
                {
                    return new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                return new SolidColorBrush(Colors.Gray);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DateTimeToEnableMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            DateTime currentDay, cutoffDayBegin, cutoffDayEnd;
            if (values[0] is DateTime && values[1] is DateTime && values[2] is DateTime)
            {
                currentDay = (DateTime)values[0];
                cutoffDayBegin = (DateTime)values[1];
                cutoffDayEnd = (DateTime)values[2];
                if (DateTime.Compare(currentDay, cutoffDayBegin) >= 0 && DateTime.Compare(currentDay, cutoffDayEnd) <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DateTimeToDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                DateTime dt = (DateTime)(value);
                string strDay = dt.Day.ToString();
                return strDay;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DateTimeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                DateTime dt = (DateTime)(value);
                if(DateTime.Compare(dt.Date,DateTime.Now.Date) == 0)
                {
                    return new SolidColorBrush(Colors.Blue);
                }
                else
                {
                    return new SolidColorBrush(Colors.White);
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
