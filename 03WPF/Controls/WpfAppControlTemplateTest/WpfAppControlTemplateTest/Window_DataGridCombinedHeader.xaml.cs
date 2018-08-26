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

namespace WpfAppControlTemplateTest
{
    /// <summary>
    /// Window_DataGridCombinedHeader.xaml 的交互逻辑
    /// </summary>
    public partial class Window_DataGridCombinedHeader : Window
    {
        public Window_DataGridCombinedHeader()
        {
            InitializeComponent();
        }

        private void BtnQuery_OnClick(object sender, RoutedEventArgs e)
        {
            List<TestEntity1> list = new List<TestEntity1>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new TestEntity1 { DeptName = "科室" + i, PlanTime = DateTime.Now.AddDays(i), DripBeginAllCount = 10 * i, DripBeginExecCount = 10 * i, DripBeginRate = "10%" });
            }
            dataGrid1.ItemsSource = list;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = dataGrid1.SelectedIndex;
            if (index >= 0)
            {
            }
             //DataGridRow row = dataGrid1.select as DataGridRow;
             //if (row != null)
             //{
             //    row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3cffaa"));
             //}
        }

        private void dataGrid1_Selected(object sender, RoutedEventArgs e)
        {
            var obj = sender;
            e.Handled = true;
        }

        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //if (e.RemovedCells != null && e.RemovedCells.Count > 0)
            //{
            //    e.RemovedCells.ToList().ForEach(p => {
            //        if(p.Column.CellStyle != null)
            //        {
            //            p.Column.CellStyle = null;
            //        }
            //    });
            //}
            //if (e.AddedCells != null && e.AddedCells.Count > 0)
            //{
            //    e.AddedCells.ToList().ForEach(p => {
            //        if (p.Column.GetType() == typeof(DataGridTemplateColumn))
            //        {
            //            if (p.Column.CellStyle == null)
            //            {
            //                var style = new Style();
            //                style.Setters.Add(new Setter(ForegroundProperty, new SolidColorBrush(Colors.White)));
            //                var dataGridColumn = p.Column;
            //                dataGridColumn.CellStyle = style;
            //            }
            //        }
            //    });
            //}
        }
    }

    public class TestEntity1
    {
        public string DeptName { get; set; }
        public DateTime PlanTime { get; set; }
        public int DripBeginAllCount { get; set; }
        public int DripBeginExecCount { get; set; }
        public string DripBeginRate { get; set; }
    }
}
