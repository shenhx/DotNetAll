using KDY.IP.DOC.Uc;
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
using System.Windows.Shapes;

namespace WpfApp_Basic
{
    /// <summary>
    /// Window_LineControl.xaml 的交互逻辑
    /// </summary>
    public partial class Window_LineControl : Window
    {
        public Window_LineControl()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            lineChartControl.AxisX = new LineChartAxisXModel();
            lineChartControl.AxisX.BackGround = Brushes.Blue;
            lineChartControl.AxisYMaxValue = 1.0d;
            lineChartControl.AxisX.Datas.Add(new LineChartAxisXDataModel() { LabelWidth = 40, Name="呼吸内科",Value=0.2 });
            lineChartControl.AxisX.Datas.Add(new LineChartAxisXDataModel() { LabelWidth = 40, Name="呼吸外科",Value=0.3 });
            lineChartControl.AxisX.Datas.Add(new LineChartAxisXDataModel() { LabelWidth = 40, Name="生理内科",Value=0.6 });
            lineChartControl.AxisX.Datas.Add(new LineChartAxisXDataModel() { LabelWidth = 40, Name="神经内科",Value=1.0 });
            lineChartControl.AxisX.Datas.Add(new LineChartAxisXDataModel() { LabelWidth = 40, Name="神经外科",Value=0.6 });

            lineChartControl.AxisY = new LineChartAxisYModel();
            lineChartControl.AxisY.Titles.Add(new LineChartAxisYDataModel() { Name="0",Value=0, IsLineVisible= true, LineBrush = Brushes.Blue, LineHeight = 1 });
            lineChartControl.AxisY.Titles.Add(new LineChartAxisYDataModel() { Name="0.2",Value=0.2, IsLineVisible=true, LineBrush = Brushes.Blue, LineHeight = 1 });
            lineChartControl.AxisY.Titles.Add(new LineChartAxisYDataModel() { Name="0.4",Value=0.4, IsLineVisible=true,LineBrush = Brushes.Blue,LineHeight=1  });
            lineChartControl.AxisY.Titles.Add(new LineChartAxisYDataModel() { Name="0.6",Value=0.6, IsLineVisible= true, LineBrush = Brushes.Blue, LineHeight = 1 });
            lineChartControl.AxisY.Titles.Add(new LineChartAxisYDataModel() { Name="0.8",Value=0.8, IsLineVisible= true, LineBrush = Brushes.Blue, LineHeight = 1 });
            lineChartControl.AxisY.Titles.Add(new LineChartAxisYDataModel() { Name="1.0",Value=1, IsLineVisible= true, LineBrush = Brushes.Blue, LineHeight = 1 });
            lineChartControl.InitControl();
        }
    }
}
