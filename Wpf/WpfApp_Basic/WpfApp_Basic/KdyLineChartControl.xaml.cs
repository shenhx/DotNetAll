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
    /// KdyLineChartControl.xaml 的交互逻辑
    /// </summary>
    public partial class KdyLineChartControl : UserControl
    {
        public KdyLineChartControl()
        {
            InitializeComponent();
        }

        public LineChartAxisYModel AxisY
        {
            get { return (LineChartAxisYModel)GetValue(AxisYProperty); }
            set { SetValue(AxisYProperty, value); }
        }

        public static readonly DependencyProperty AxisYProperty = DependencyProperty.Register("AxisY",
        typeof(LineChartAxisYModel), typeof(KdyLineChartControl),
        new PropertyMetadata(new LineChartAxisYModel()));

        public LineChartAxisXModel AxisX
        {
            get { return (LineChartAxisXModel)GetValue(AxisXProperty); }
            set { SetValue(AxisXProperty, value); }
        }

        public static readonly DependencyProperty AxisXProperty = DependencyProperty.Register("AxisX",
        typeof(LineChartAxisXModel), typeof(KdyLineChartControl),
        new PropertyMetadata(new LineChartAxisXModel()));
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }
        public static readonly DependencyProperty HeaderHeightProperty = DependencyProperty.Register("HeaderHeight",
        typeof(double), typeof(KdyLineChartControl), new PropertyMetadata(10.0));
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header",
        typeof(string), typeof(KdyLineChartControl), new PropertyMetadata());

        public static readonly DependencyProperty AxisYMaxValueProperty = DependencyProperty.Register("AxisYMaxValue", typeof(double), typeof(KdyLineChartControl), new PropertyMetadata(1.0, (s, e) => {
            if (0.Equals(e.NewValue))
            {
                throw new DivideByZeroException();
            }
            double newVal = 0;
            double.TryParse(e.NewValue.ToString(), out newVal);
            if (newVal <= 0)
            {
                throw new Exception("纵坐标值不能设置为小于0！");
            }
        }));

        /// <summary>
        /// Y坐标值，默认从0开始计算
        /// </summary>
        public double AxisYMaxValue
        {
            get { return (double)GetValue(AxisYMaxValueProperty); }
            set { SetValue(AxisYMaxValueProperty, value); }
        }

        /// <summary>
        /// 入口
        /// </summary>
        public void InitControl()
        {
            BottomGrid.Height = AxisX.Height;
            LeftGrid.Width = AxisY.Width;

            //清除数据
            this.FlushAll();

            //绘画内容
            SetYTitlesContent();

            SetXDatasContent();
        }

        /// <summary>
        /// 出口
        /// </summary>
        public void FlushAll()
        {
            if (MainGridFrom0To1.Children != null)
                MainGridFrom0To1.Children.Clear();
            if (BottomGrid.Children != null)
                BottomGrid.Children.Clear();
            if (LeftGrid.Children != null)
                LeftGrid.Children.Clear();
            if (MainGridForRow1.Children != null)
                MainGridForRow1.Children.Clear();
            if (BottomGrid.ColumnDefinitions != null)
                BottomGrid.ColumnDefinitions.Clear();
            if (MainGridFrom0To1.ColumnDefinitions != null)
                MainGridFrom0To1.ColumnDefinitions.Clear();
            if (LeftGrid.RowDefinitions != null)
                LeftGrid.RowDefinitions.Clear();
            if (MainGridForRow1.RowDefinitions != null)
                MainGridForRow1.RowDefinitions.Clear();
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
                }
                int index = 0;
                
                //计算高度
                if (double.IsNaN(this.HeaderGrid.Height))
                    this.HeaderGrid.Height = 0d;
                if (double.IsNaN(this.BottomGrid.Height))
                    this.BottomGrid.Height = 0d;
                if (double.IsNaN(this.LeftGrid.Width))
                    this.LeftGrid.Width = 0d;
                if (double.IsNaN(this.RightGrid.Width))
                    this.RightGrid.Width = 0d;

                var yStartPos = this.Height - this.HeaderGrid.Height - this.BottomGrid.Height;
                var xEndPos = this.Width - this.LeftGrid.Width - this.RightGrid.Width;
                var xPartDistance = xEndPos / 5;
                double x = 0, y = 0;
                double lastX = 0, lastY = 0;
                PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();

                //画线
                Path path = new Path();
                path.Stroke = axisXModel.BackGround;
                PathFigure pathFigure = new PathFigure();
                pathFigure.IsClosed = false;

                foreach (var data in axisXModel.Datas)
                {
                    //底部
                    var textblock = new TextBlock();
                    textblock.Text = data.Name;
                    textblock.Foreground = axisXModel.ForeGround;
                    textblock.VerticalAlignment = VerticalAlignment.Top;
                    textblock.TextAlignment = TextAlignment.Center;
                    textblock.HorizontalAlignment = HorizontalAlignment.Center;
                    double textBlockWidth = data.LabelWidth;
                    textblock.Width = data.LabelWidth;
                    textblock.Margin = new Thickness(0, 5, 0, 0);
                    Grid.SetColumn(textblock, index);
                    BottomGrid.Children.Add(textblock);

                    //主内容
                    //使用QuadraticBezierSegment对象
                    y = ((AxisYMaxValue - data.Value) / AxisYMaxValue) * yStartPos;
                    x = (index + 0.5) * xPartDistance;
                    if (index > 0) //第一点不画线
                    {
                        QuadraticBezierSegment quadraticBezierSegment = new QuadraticBezierSegment();
                        quadraticBezierSegment.Point1 = new Point(lastX,lastY);
                        quadraticBezierSegment.Point2 = new Point(x,y);
                        pathSegmentCollection.Add(quadraticBezierSegment);
                    }
                    else
                    {
                        pathFigure.StartPoint = new Point(x, y);//起点
                    }

                    lastX = x;
                    lastY = y;
                    index++;
                }
                pathFigure.Segments = pathSegmentCollection;
                PathGeometry pathGeometry = new PathGeometry() {
                    Figures = new PathFigureCollection()
                    {
                        pathFigure
                    }
                    
                };
                path.Data = pathGeometry;
                Canvas canvas = new Canvas();
                canvas.Children.Add(path);
                
                //标注圈圈
                var rectangle = new Rectangle();
                //标注起点
                rectangle.Fill = Brushes.Gray;
                rectangle.Stroke = Brushes.Blue;
                rectangle.Height = 5;
                rectangle.Width = 5;
                canvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, pathFigure.StartPoint.X);
                Canvas.SetTop(rectangle, pathFigure.StartPoint.Y);

                foreach (QuadraticBezierSegment item in pathSegmentCollection)
                {
                    rectangle = new Rectangle();
                    Canvas.SetLeft(rectangle, item.Point2.X);
                    Canvas.SetTop(rectangle, item.Point2.Y);
                    rectangle.Fill = Brushes.Gray;
                    rectangle.Stroke = Brushes.Blue;
                    rectangle.Height = 5;
                    rectangle.Width = 5;
                    canvas.Children.Add(rectangle);
                }
                
                MainGridFrom0To1.Children.Add(canvas);
            }
        }

        private void SetYTitlesContent()
        {
            var axisYModel = AxisY;
            if (axisYModel.Titles.Count > 0)
            {
                int gridRows = axisYModel.Titles.Count - 1;
                for (int i = 0; i < gridRows; i++)
                {
                    LeftGrid.RowDefinitions.Add(new RowDefinition());
                    MainGridForRow1.RowDefinitions.Add(new RowDefinition());
                }
                int index = 0;
                //计算高度
                if (double.IsNaN(this.HeaderGrid.Height))
                    this.HeaderGrid.Height = 0d;
                if (double.IsNaN(this.BottomGrid.Height))
                    this.BottomGrid.Height = 0d;
                //纵坐标高度
                var startPos = this.Height - this.HeaderGrid.Height - this.BottomGrid.Height;
                var partDistance = startPos / 5;//每份距离
                foreach (var title in axisYModel.Titles)
                {
                    var textblock = new TextBlock();
                    textblock.Text = title.Name;
                    textblock.Foreground = axisYModel.ForeGround;
                    textblock.HorizontalAlignment = HorizontalAlignment.Right;
                    textblock.Height = title.LabelHeight;
                    if (index < gridRows)
                    {
                        textblock.VerticalAlignment = VerticalAlignment.Bottom;
                        textblock.Margin = new Thickness(0, 0, 0, -title.LabelHeight / 2);
                        Grid.SetRow(textblock, gridRows - index - 1);
                    }
                    else
                    {
                        textblock.VerticalAlignment = VerticalAlignment.Top;
                        textblock.Margin = new Thickness(0, -title.LabelHeight / 2, 0, 0);
                        Grid.SetRow(textblock, 0);
                    }
                    LeftGrid.Children.Add(textblock);

                    if (title.IsLineVisible)
                    {
                        var line = new Line();
                        line.Stroke = title.LineBrush;
                        line.StrokeDashArray = new DoubleCollection();
                        line.StrokeDashArray.Add(2);
                        line.StrokeDashArray.Add(4);
                        double thickness = Convert.ToDouble(title.LineHeight) / 2;
                        line.StrokeThickness = thickness;
                        line.X1 = 0;
                        line.X2 = this.Width - LeftGrid.Width ;
                        line.Y1 = startPos - index * partDistance;
                        line.Y2 = line.Y1;
                        
                        if (index < gridRows)
                        {
                            line.VerticalAlignment = VerticalAlignment.Bottom;
                            line.Margin = new Thickness(0, 0, 0, -thickness);//因为设置在行底部还不够,必须往下移
                            Grid.SetRow(line, gridRows - index - 1);
                        }
                        else
                        {
                            line.VerticalAlignment = VerticalAlignment.Top;
                            line.Margin = new Thickness(0, -thickness, 0, 0);//最后一个,设置在顶部
                            Grid.SetRow(line, 0);
                        }
                        Grid.SetColumn(line, 0);
                        Grid.SetColumnSpan(line, AxisX.Datas.Count + 1);
                        MainGridForRow1.Children.Add(line);
                    }
                    index++;
                }
            }
        }
    }

    /// <summary>
    /// X轴
    /// </summary>
    public class LineChartAxisXModel
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

        List<LineChartAxisXDataModel> _datas = new List<LineChartAxisXDataModel>();
        /// <summary>
        /// 数据
        /// </summary>
        public List<LineChartAxisXDataModel> Datas
        {
            get { return _datas; }
            set { _datas = value; }
        }

        public Brush BackGround { get; set; }
    }

    /// <summary>
    /// Y轴
    /// </summary>
    public class LineChartAxisYModel
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

        List<LineChartAxisYDataModel> _titles = new List<LineChartAxisYDataModel>();
        /// <summary>
        /// 左侧标题列表
        /// </summary>
        public List<LineChartAxisYDataModel> Titles
        {
            get { return _titles; }
            set { _titles = value; }
        }
    }

    /// <summary>
    /// X轴数据模型
    /// </summary>
    public class LineChartAxisXDataModel : DataModel
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

    /// <summary>
    /// Y轴数据模型
    /// </summary>
    public class LineChartAxisYDataModel : DataModel
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
        private double _lineHeight = 1;
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
    
}
