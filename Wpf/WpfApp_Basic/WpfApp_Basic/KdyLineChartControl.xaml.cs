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
            //设定元素宽高
            BottomGrid.Height = AxisX.Height;
            LeftGrid.Width = AxisY.Width;
            LeftGrid.Height = this.Height - BottomGrid.Height;
            LeftGrid.Margin = new Thickness(0, 0, 0, BottomGrid.Height);
            var axisXModel = AxisX;
            double actualWidth = 60 * (axisXModel.Datas.Count);
            if (actualWidth < this.Width)
            {
                actualWidth = this.Width - LeftGrid.Width;
            }
            MainGridFrom0To1.Width = actualWidth;
            MainGridForRow1.Width = actualWidth;
            BottomGrid.Width = actualWidth;
            MainGridFrom0To1.Height = this.Width - BottomGrid.Height;
            MainGridForRow1.Height = this.Width - BottomGrid.Height;
            //计算高度
            if (double.IsNaN(this.BottomGrid.Height))
                this.BottomGrid.Height = 0d;
            if (double.IsNaN(this.LeftGrid.Width))
                this.LeftGrid.Width = 0d;
            //计算高度
            if (double.IsNaN(this.BottomGrid.Height))
                this.BottomGrid.Height = 0d;

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
            if (axisXModel.Datas.Count > 0 && AxisY.Titles.Count > 0)
            {
                int count = axisXModel.Datas.Count;
                for (int i = 0; i < count; i++)
                {
                    BottomGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                int index = 0;
                
                var yStartPos = this.Height - this.BottomGrid.Height;
                var xEndPos = this.MainGridFrom0To1.Width;
                var xPartDistance = xEndPos / AxisX.Datas.Count;
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
                    textblock.VerticalAlignment = VerticalAlignment.Center;
                    textblock.TextAlignment = TextAlignment.Center;
                    textblock.HorizontalAlignment = HorizontalAlignment.Center;
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
            if (axisYModel.Titles.Count > 1)
            {
                int gridRows = axisYModel.Titles.Count-1;
                for (int i = 0; i < gridRows; i++)
                {
                    LeftGrid.RowDefinitions.Add(new RowDefinition());
                }
                int index = 0;
               
                //纵坐标高度
                var mainGrdHeight = this.Height - this.BottomGrid.Height;
                var partDistance = mainGrdHeight / (axisYModel.Titles.Count-1);//每份距离
                Canvas lineCanvas = new Canvas();
                foreach (var title in axisYModel.Titles)
                {
                    var textblock = new TextBlock();
                    textblock.Text = title.Name;
                    textblock.Foreground = axisYModel.ForeGround;
                    textblock.Width = axisYModel.Width;
                    textblock.HorizontalAlignment = HorizontalAlignment.Center;
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
                        line.X2 = this.MainGridForRow1.Width;
                        line.Y1 = mainGrdHeight - index * partDistance;
                        if (line.Y1 < 0)
                            line.Y1 = 0;
                        line.Y2 = line.Y1;
                        //Console.WriteLine("{4}:{0}-{1}-{2}-{3}，{5}", line.X1, line.X2, line.Y1, line.Y2, title.Name,partDistance);

                        if (index < gridRows)
                        {
                            line.VerticalAlignment = VerticalAlignment.Bottom;
                            line.Margin = new Thickness(0, 0, 0, -thickness);//因为设置在行底部还不够,必须往下移
                        }
                        else
                        {
                            line.VerticalAlignment = VerticalAlignment.Top;
                            line.Margin = new Thickness(0, thickness, 0, 0);
                        }
                        lineCanvas.Children.Add(line);
                    }
                    index++;
                }
                if (lineCanvas.Children.Count > 0)
                {
                    MainGridForRow1.Children.Add(lineCanvas);
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
