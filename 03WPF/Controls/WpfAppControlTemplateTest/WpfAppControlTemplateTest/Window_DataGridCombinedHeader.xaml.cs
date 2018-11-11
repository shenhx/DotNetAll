using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
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

        private void BtnExcelOutput_OnClick(object sender, RoutedEventArgs e)
        {
            ExcelContentEntity entity = new ExcelContentEntity();
            entity.ColumnWidthArrays = new int[]{30,30,20,20,20};
            entity.HeaderRows = new List<ExcelHeaderDefinition>();
            var headerRows = entity.HeaderRows;
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(0, 0), EndIndex = new Point(1, 0), CellValue = "科室" });
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(0, 1), EndIndex = new Point(1, 1), CellValue = "日期" });
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(0, 2), EndIndex = new Point(0, 4), CellValue = "输液滴注-开始" });
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(1, 2), EndIndex = new Point(1, 2), CellValue = "应执行" });
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(1, 3), EndIndex = new Point(1, 3), CellValue = "已执行" });
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(1, 4), EndIndex = new Point(1, 4), CellValue = "执行率%" });

            var datas = (IList<TestEntity1>)dataGrid1.ItemsSource;

            var dataRows = new List<List<ExcelCellDataDefinition>>();
            foreach (var item in datas)
            {
                dataRows.Add(ConvertToExcelDataRow(item));
            }
            entity.DataRows = dataRows;
            ExcelOutputHelper.OutputExcel(@"E://1.xls", "", entity);
            // OutputExcel(@"E://1.xls","");
        }

        private List<ExcelCellDataDefinition> ConvertToExcelDataRow(TestEntity1 item)
        {
            List<ExcelCellDataDefinition> resultList = new List<ExcelCellDataDefinition>();
            resultList.Add(new ExcelCellDataDefinition { ColumnBeginEndIndex = new Point(0, 0), CellDataType = LocalCellType.String, CellValue = item.DeptName });
            resultList.Add(new ExcelCellDataDefinition { ColumnBeginEndIndex = new Point(1, 1), CellDataType = LocalCellType.String, CellValue = item.PlanTime.ToString("yyyy-MM-dd") });
            resultList.Add(new ExcelCellDataDefinition { ColumnBeginEndIndex = new Point(2, 2), CellDataType = LocalCellType.Numeric, CellValue = item.DripBeginAllCount.ToString() });
            resultList.Add(new ExcelCellDataDefinition { ColumnBeginEndIndex = new Point(3, 3), CellDataType = LocalCellType.Numeric, CellValue = item.DripBeginExecCount.ToString() });
            resultList.Add(new ExcelCellDataDefinition { ColumnBeginEndIndex = new Point(4, 4), CellDataType = LocalCellType.String, CellValue = item.DripBeginRate.ToString() });
            return resultList;
        }

        private void OutputExcel(string filenName, string sheetName)
        {
            IWorkbook wb = null;
            string strExtensionOfFilePath = System.IO.Path.GetExtension(filenName);
            if (string.IsNullOrEmpty(strExtensionOfFilePath))
            {
                throw new ParamsException("保存文件路径不合法，缺少文件扩展名称，路径为："+filenName);
            }
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "sheet1";
            }
            if (".xls".Equals(strExtensionOfFilePath.ToLower()))
            {
                wb = new HSSFWorkbook();
            }
            else
            {
                throw new NotAllowException("目前还没实现");
            }
            ICellStyle headerCellStyle = wb.CreateCellStyle();
            // 居中方式
            headerCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            headerCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            // 边框
            headerCellStyle.BorderBottom = BorderStyle.Thin;
            headerCellStyle.BorderTop = BorderStyle.Thin;
            headerCellStyle.BorderLeft = BorderStyle.Thin;
            headerCellStyle.BorderRight = BorderStyle.Thin;
            // 自动换行
            headerCellStyle.WrapText = true;

            ICellStyle dataCellStyle = wb.CreateCellStyle();
            // 居中方式
            dataCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            dataCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            // 边框
            dataCellStyle.BorderBottom = BorderStyle.Thin;
            dataCellStyle.BorderTop = BorderStyle.Thin;
            dataCellStyle.BorderLeft = BorderStyle.Thin;
            dataCellStyle.BorderRight = BorderStyle.Thin;
            // 自动换行
            dataCellStyle.WrapText = true;

            // 创建表单
            ISheet sheetOne = wb.CreateSheet(sheetName);
            
            // 设置列宽
            int[] arrColumnWidths = { 30, 30, 40 };
            for (int i = 0; i < arrColumnWidths.Length; i++)
            {
                sheetOne.SetColumnWidth(i,256 * arrColumnWidths[i]);
            }

            // 创建标题栏
            // 第一行标题
            IRow headerRow = sheetOne.CreateRow(0);
            ICell headerCell = headerRow.CreateCell(0);
            headerCell.CellStyle = headerCellStyle;
            headerCell.SetCellValue("科室");
            headerCell = headerRow.CreateCell(1);
            headerCell.CellStyle = headerCellStyle;
            headerCell.SetCellValue("日期");
            headerCell = headerRow.CreateCell(2);
            headerCell.CellStyle = headerCellStyle;
            headerCell.SetCellValue("静脉滴注-开始");
            sheetOne.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 2, 4));
            // 第二行标题
            headerRow = sheetOne.CreateRow(1);
            sheetOne.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 0, 0));
            sheetOne.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 1, 1));
            headerCell = headerRow.CreateCell(2);
            headerCell.CellStyle = headerCellStyle;
            headerCell.SetCellValue("应执行");
            headerCell = headerRow.CreateCell(3);
            headerCell.CellStyle = headerCellStyle;
            headerCell.SetCellValue("已执行");
            headerCell = headerRow.CreateCell(4);
            headerCell.CellStyle = headerCellStyle;
            headerCell.SetCellValue("执行率");
            int startIndex = 2;
            int dataRowCount = 2+startIndex;
            int dataColumnCount = 5;
            string[,] datas = new string[dataRowCount, dataColumnCount];
            IRow dataRow = null;
            ICell dataCell = null;
            for (int i = startIndex; i < dataRowCount; i++)
            {
                dataRow = sheetOne.CreateRow(i);
                for (int j = 0; j < dataColumnCount; j++)
                {
                    dataCell = dataRow.CreateCell(j);
                    dataCell.CellStyle = dataCellStyle;
                    dataCell.SetCellValue("test");
                }
            }
            using (FileStream fs = File.OpenWrite(filenName))
            {
                wb.Write(fs);//向打开的这个Excel文件中写入表单并保存。  
                fs.Flush();
            }
        }

        private void BtnExcelOutput2_OnClick(object sender, RoutedEventArgs e)
        {
            ExcelContentEntity entity = new ExcelContentEntity();
            entity.ColumnWidthArrays = new int[] { 30, 30, 20, 20, 20 };
            entity.HeaderRows = new List<ExcelHeaderDefinition>();
            var headerRows = entity.HeaderRows;
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(0, 0), EndIndex = new Point(0, 0), CellValue = "科室" });
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(0, 1), EndIndex = new Point(0, 1), CellValue = "日期" });
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(0, 2), EndIndex = new Point(0, 2), CellValue = "应执行" });
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(0, 3), EndIndex = new Point(0, 3), CellValue = "已执行" });
            headerRows.Add(new ExcelHeaderDefinition { StartIndex = new Point(0, 4), EndIndex = new Point(0, 4), CellValue = "执行率%" });

            var datas = (IList<TestEntity1>)dataGrid1.ItemsSource;

            var dataRows = new List<List<ExcelCellDataDefinition>>();
            foreach (var item in datas)
            {
                dataRows.Add(ConvertToExcelDataRow(item));
            }
            entity.DataRows = dataRows;
            ExcelOutputHelper.OutputExcel(@"E://2.xls", "", entity);
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
