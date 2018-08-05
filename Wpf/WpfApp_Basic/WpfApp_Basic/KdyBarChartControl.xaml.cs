using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KDY.IP.DOC.Uc
{
    /// <summary>
    /// 显示病人列表
    /// </summary>
    /// <param name="key"></param>
    public delegate void ShowPatientsDelegate(object tag);

    /// <summary>
    /// KdyBarChartControl.xaml 的交互逻辑
    /// </summary>
    public partial class KdyBarChartControl : UserControl
    {
        public KdyBarChartControl()
        {
            InitializeComponent();
        }

        public ShowPatientsDelegate ShowPatientsEvent;

        public AxisYModel AxisY
        {
            get { return (AxisYModel)GetValue(AxisYProperty); }
            set { SetValue(AxisYProperty, value); }
        }

        public static readonly DependencyProperty AxisYProperty = DependencyProperty.Register("AxisY",
        typeof(AxisYModel), typeof(KdyBarChartControl),
        new PropertyMetadata(new AxisYModel()));

        public AxisXModel AxisX
        {
            get { return (AxisXModel)GetValue(AxisXProperty); }
            set { SetValue(AxisXProperty, value); }
        }

        public static readonly DependencyProperty AxisXProperty = DependencyProperty.Register("AxisX",
        typeof(AxisXModel), typeof(KdyBarChartControl),
        new PropertyMetadata(new AxisXModel()));
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }
        public static readonly DependencyProperty HeaderHeightProperty = DependencyProperty.Register("HeaderHeight",
        typeof(double), typeof(KdyBarChartControl), new PropertyMetadata(10.0));

        private void BarChartControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            //设定元素宽高
            BottomGrid.Height = AxisX.Height;
            LeftGrid.Width = AxisY.Width;
            LeftGrid.Height = this.ActualHeight - BottomGrid.Height;
            LeftGrid.Margin = new Thickness(0, 0, 0, BottomGrid.Height);
            var axisXModel = AxisX;

            double actualWidth = 60 * (axisXModel.Datas.Count);
            if (actualWidth < this.ActualWidth)
            {
                actualWidth = this.ActualWidth - LeftGrid.Width;
            }
            MainGridFrom0To1.Width = actualWidth;
            MainGridForRow1.Width = actualWidth;
            BottomGrid.Width = actualWidth;
            MainGridFrom0To1.Height = this.ActualHeight - BottomGrid.Height;
            MainGridForRow1.Height = this.ActualHeight - BottomGrid.Height;
            
            //清除数据
            if(MainGridFrom0To1.Children != null)
                MainGridFrom0To1.Children.Clear();
            if (BottomGrid.Children != null)
                BottomGrid.Children.Clear();
            if (LeftGrid.Children != null)
                LeftGrid.Children.Clear();
            if (MainGridForRow1.Children != null)
                MainGridForRow1.Children.Clear();

            //绘画内容
            SetYTitlesContent();

            SetXDatasContent();
        }

        private void SetXDatasContent()
        {
            var axisXModel = AxisX;
            if (axisXModel.Datas.Count > 0)
            {
                int count = axisXModel.Datas.Count;
                for (int i = 0; i < count; i++)
                {
                    BottomGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    MainGridFrom0To1.ColumnDefinitions.Add(new ColumnDefinition());
                }
                int index = 0;
                foreach (var data in axisXModel.Datas)
                {
                    //底部
                    var textblock = new TextBlock();
                    textblock.Text = data.Name;
                    textblock.Foreground = axisXModel.ForeGround;
                    textblock.VerticalAlignment = VerticalAlignment.Center;
                    textblock.TextAlignment = TextAlignment.Center;
                    textblock.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetColumn(textblock, index);
                    BottomGrid.Children.Add(textblock);
                    
                    //主内容
                    var stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Vertical;

                    //var tbl = new TextBlock();
                    //tbl.Height = 15;
                    //tbl.Margin = new Thickness(0, 0, 0, 5);
                    //tbl.Text = data.Value.ToString();
                    //tbl.Foreground = axisXModel.ForeGround;
                    //tbl.HorizontalAlignment = HorizontalAlignment.Center;
                    //stackPanel.Children.Add(tbl);

                    //改为按钮形式
                    var btn = new Button();
                    btn.Width = data.BarWidth;
                    btn.BorderThickness = new Thickness(0);
                    btn.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    double maxValue = AxisY.Titles.Max(i => i.Value);
                    btn.Height = (data.Value / maxValue) * (this.ActualHeight - BottomGrid.Height);
                    btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEC8B"));
                    btn.HorizontalAlignment = HorizontalAlignment.Center;
                    btn.MouseDoubleClick += Btn_MouseDoubleClick;
                    btn.Margin = new Thickness(0, 0, 10, 0);
                    btn.Tag = data.Tag;//附带信息
                    stackPanel.Children.Add(btn);

                    //stackPanel.Margin = new Thickness(0, 0, -btn.Width / 2, 0);
                    stackPanel.VerticalAlignment = VerticalAlignment.Bottom;
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;

                    Grid.SetColumn(stackPanel, index);
                    MainGridFrom0To1.Children.Add(stackPanel);
                    index++;
                }
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ShowPatientsEvent != null && sender.GetType() == typeof(Button))
            {
                e.Handled = true;
                ShowPatientsEvent(((Button)sender).Tag);
            }
        }

        private void SetYTitlesContent()
        {
            var axisYModel = AxisY;
            if (axisYModel.Titles.Count > 0)
            {
                int gridRows = axisYModel.Titles.Count-1;//总数-1
                for (int i = 0; i < gridRows; i++)
                {
                    LeftGrid.RowDefinitions.Add(new RowDefinition());
                    MainGridForRow1.RowDefinitions.Add(new RowDefinition());
                }
                int index = 0;
                foreach (var title in axisYModel.Titles)
                {
                    double thickness = Convert.ToDouble(title.LineHeight);
                    var textblock = new TextBlock();
                    textblock.Text = title.Name;
                    textblock.Foreground = axisYModel.ForeGround;
                    textblock.HorizontalAlignment = HorizontalAlignment.Center;
                    textblock.Height = title.LabelHeight;
                    
                    if (index < gridRows)
                    {
                        textblock.Margin = new Thickness(0, 5, 0, -title.LabelHeight / 2);//因为设置在行底部还不够,必须往下移
                        textblock.VerticalAlignment = VerticalAlignment.Bottom;
                        Grid.SetRow(textblock, gridRows - index - 1);
                    }
                    else
                    {
                        textblock.Margin = new Thickness(0, -title.LabelHeight / 2, 5, 0);//最后一个,设置在顶部
                        textblock.VerticalAlignment = VerticalAlignment.Top;
                        Grid.SetRow(textblock, 0);
                    }
                    LeftGrid.Children.Add(textblock);

                    var border = new Border();
                    border.Height = title.LineHeight;
                    border.BorderBrush = title.LineBrush ;
                    
                    if (index < gridRows)
                    {
                        border.BorderThickness = new Thickness(0, 0, 0, thickness);
                        border.VerticalAlignment = VerticalAlignment.Bottom;
                        border.Margin = new Thickness(0, 0, 0, -thickness);//因为设置在行底部还不够,必须往下移
                        Grid.SetRow(border, gridRows - index - 1);
                    }
                    else
                    {
                        border.BorderThickness = new Thickness(0, thickness, 0, 0);
                        border.VerticalAlignment = VerticalAlignment.Top;
                        border.Margin = new Thickness(0, 4*thickness, 0, 0);//最后一个,设置在顶部
                        Grid.SetRow(border, 0);
                    }
                    MainGridForRow1.Children.Add(border);
                    index++;
                }
            }
        }
    }
    public class AxisXModel
    {
        private double _height = 20;
        /// <summary>
        /// 高度
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
            set { _height = value; }
        }

        private Brush _foreGround = Brushes.Black;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Brush ForeGround
        {
            get { return _foreGround; }
            set { _foreGround = value; }
        }

        List<AxisXDataModel> _datas = new List<AxisXDataModel>();
        /// <summary>
        /// 数据
        /// </summary>
        public List<AxisXDataModel> Datas
        {
            get { return _datas; }
            set { _datas = value; }
        }

        public Brush BackGround { get; set; }
    }
    public class AxisYModel
    {
        private double _width = 20;
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width { get { return _width; } set { _width = value; } }

        private Brush _foreGround = Brushes.Black;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Brush ForeGround { get { return _foreGround; } set { _foreGround = value; } }

        List<AxisYDataModel> _titles = new List<AxisYDataModel>();
        /// <summary>
        /// 左侧标题列表
        /// </summary>
        public List<AxisYDataModel> Titles
        {
            get { return _titles; }
            set { _titles = value; }
        }
    }
    public class AxisXDataModel : DataModel
    {
        private double _labelWidth = 20;
        /// <summary>
        /// 底部标签-单个宽度
        /// </summary>
        public double LabelWidth
        {
            get { return _labelWidth; }
            set { _labelWidth = value; }
        }
        private double _barWidth = 20;
        /// <summary>
        /// Bar宽度
        /// </summary>
        public double BarWidth
        {
            get { return _barWidth; }
            set { _barWidth = value; }
        }

        private Color _fillBrush = Colors.Blue;
        /// <summary>
        /// Bar填充颜色
        /// </summary>
        public Color FillBrush
        {
            get
            {
                return _fillBrush;
            }
            set { _fillBrush = value; }
        }

        private Color _fillEndBrush = Colors.Blue;

        public Color FillEndBrush
        {
            get
            {
                return _fillEndBrush;
            }
            set { _fillEndBrush = value; }
        }
    }
    public class AxisYDataModel : DataModel
    {
        private double _labelHeight = 15;
        /// <summary>
        /// 左侧标题栏-单个标题高度
        /// </summary>
        public double LabelHeight
        {
            get { return _labelHeight; }
            set { _labelHeight = value; }
        }
        private double _lineHeight = 0.5;
        /// <summary>
        /// GridLine高度
        /// </summary>
        public double LineHeight
        {
            get { return _lineHeight; }
            set { _lineHeight = value; }
        }

        private Brush _lineBrush = Brushes.Blue;
        /// <summary>
        /// Bar填充颜色
        /// </summary>
        public Brush LineBrush
        {
            get { return _lineBrush; }
            set { _lineBrush = value; }
        }

        private bool _isLineVisible = true;
        /// <summary>
        /// 线是否可见
        /// </summary>
        public bool IsLineVisible
        {
            get { return _isLineVisible; }
            set { _isLineVisible = value; }
        }
    }
    public class DataModel
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 隐藏的数据，便于
        /// </summary>
        public object Tag { get; set; }
    }

    ///// <summary>
    ///// 表格数据
    ///// </summary>
    //public class DataModel
    //{
    //    /// <summary>
    //    /// 显示名称
    //    /// </summary>
    //    public string ColumnName { get; set; }

    //    /// <summary>
    //    /// 值
    //    /// </summary>
    //    public double ColumnValue { get; set; }

    //    /// <summary>
    //    /// 隐藏的数据，便于
    //    /// </summary>
    //    public object Tag { get; set; }
    //}
}
