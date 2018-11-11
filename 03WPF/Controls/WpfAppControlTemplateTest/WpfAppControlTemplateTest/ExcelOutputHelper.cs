using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfAppControlTemplateTest
{
    /// <summary>
    /// 依赖NPOI
    /// </summary>
    public class ExcelOutputHelper
    {
        /// <summary>
        /// 创建workbook
        /// </summary>
        /// <param name="filenName"></param>
        /// <param name="sheetName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static IWorkbook CreateFactory(string filenName)
        {
            string strExtensionOfFilePath = System.IO.Path.GetExtension(filenName);
            if (string.IsNullOrEmpty(strExtensionOfFilePath))
            {
                throw new ParamsException("保存文件路径不合法，缺少文件扩展名称，路径为：" + filenName);
            }
            if (".xls".Equals(strExtensionOfFilePath.ToLower()))
            {
                IWorkbook wb = new HSSFWorkbook();
                return wb;
            }
            else
            {
                throw new NotAllowException("目前还没实现其他格式的导出！");
            }
        }

        /// <summary>
        /// 检查Excel输出内容是否有效
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static bool ValidateExcelContent(ExcelContentEntity entity)
        {
            if (entity == null)
            {
                throw new NotAllowException("ExcelContentEntity不允许为空！");
            }
            if (entity.ColumnWidthArrays == null || entity.ColumnWidthArrays.Count() <= 0)
            {
                throw new NotAllowException("ColumnWidthArrays没有定义");
            }
            if (entity.HeaderRows == null || entity.HeaderRows.Count <= 0)
            {
                throw new NotAllowException("HeaderRows没有定义");
            }
            if (entity.DataRows == null || entity.DataRows.Count <= 0)
            {
                throw new NotAllowException("DataRows没有定义");
            }
            return true;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="filenName"></param>
        /// <param name="sheetName"></param>
        /// <param name="entity"></param>
        public static void OutputExcel(string filenName, string sheetName, ExcelContentEntity entity)
        {
            IWorkbook wb = CreateFactory(filenName);
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "sheet1";
            }
            ValidateExcelContent(entity); // 检查Excel的内容
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
            headerCellStyle.WrapText = false;
            headerCellStyle.FillBackgroundColor = 1;

            // 字符串或者其他
            ICellStyle dataStringCellStyle = wb.CreateCellStyle();
            // 居中方式
            dataStringCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            dataStringCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            // 边框
            dataStringCellStyle.BorderBottom = BorderStyle.Thin;
            dataStringCellStyle.BorderTop = BorderStyle.Thin;
            dataStringCellStyle.BorderLeft = BorderStyle.Thin;
            dataStringCellStyle.BorderRight = BorderStyle.Thin;
            // 自动换行
            dataStringCellStyle.WrapText = true;

            // number类型
            ICellStyle dataNumberCellStyle = wb.CreateCellStyle();
            // 居中方式
            dataNumberCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            dataNumberCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            // 边框
            dataNumberCellStyle.BorderBottom = BorderStyle.Thin;
            dataNumberCellStyle.BorderTop = BorderStyle.Thin;
            dataNumberCellStyle.BorderLeft = BorderStyle.Thin;
            dataNumberCellStyle.BorderRight = BorderStyle.Thin;
            // 自动换行
            dataNumberCellStyle.WrapText = true;

            // 创建表单
            ISheet sheetOne = wb.CreateSheet(sheetName);

            // 设置列宽
            int[] arrColumnWidths = entity.ColumnWidthArrays;
            for (int i = 0; i < arrColumnWidths.Length; i++)
            {
                sheetOne.SetColumnWidth(i, 256 * arrColumnWidths[i]);
            }

            IRow dataRow = null;
            ICell dataCell = null;

            // 创建标题栏
            int rowHeaderMaxIndex = entity.GetHeaderRowCount();
            for (int i = 0; i < rowHeaderMaxIndex; i++)
            {
                dataRow = sheetOne.CreateRow(i);
            }
            foreach (var item in entity.HeaderRows)
            {
                for (double i = item.StartIndex.X; i <= item.EndIndex.X; i++)
                {
                    dataRow = sheetOne.GetRow((int)i);
                    for (double j = item.StartIndex.Y; j <= item.EndIndex.Y; j++)
                    {
                        dataCell = dataRow.CreateCell((int)j);
                        dataCell.CellStyle = headerCellStyle;
                    }
                }
                dataRow = sheetOne.GetRow((int)item.StartIndex.X);
                dataCell = dataRow.GetCell((int)item.StartIndex.Y);
                dataCell.SetCellValue(item.CellValue);
                if (item.IsMergeRowOrCell())
                {
                    sheetOne.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress((int)item.StartIndex.X, (int)item.EndIndex.X, (int)item.StartIndex.Y, (int)item.EndIndex.Y));
                }
            }
            // 数据行
            int dataRowMaxIndex = entity.GetDataRowCount();
            
            for (int i = 0; i < dataRowMaxIndex; i++)
            {
                dataRow = sheetOne.CreateRow(i + rowHeaderMaxIndex);
                foreach (var dataItem in entity.DataRows[i])
                {
                    dataCell = dataRow.CreateCell((int)dataItem.ColumnBeginEndIndex.X,(CellType)dataItem.CellDataType);
                    if (dataItem.CellDataType == LocalCellType.Numeric)
                    {
                        dataCell.CellStyle = dataNumberCellStyle;
                    }
                    else
                    {
                        dataCell.CellStyle = dataStringCellStyle;
                    }
                    dataCell.SetCellValue(dataItem.CellValue);
                    if (dataItem.IsMergeRowOrCell())
                    {
                        for (double j = dataItem.ColumnBeginEndIndex.X + 1; j <= dataItem.ColumnBeginEndIndex.Y; j++)
                        {
                            dataRow.CreateCell((int)j);
                        }
                        sheetOne.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(i, i, (int)dataItem.ColumnBeginEndIndex.X, (int)dataItem.ColumnBeginEndIndex.Y));
                    }
                }
            }
            using (FileStream fs = File.OpenWrite(filenName))
            {
                wb.Write(fs);//向打开的这个Excel文件中写入表单并保存。  
                fs.Flush();
            }
        }

    }


    public class ExcelContentEntity
    {
        public int[] ColumnWidthArrays { get; set; }
        /// <summary>
        /// 标题行
        /// </summary>
        public List<ExcelHeaderDefinition> HeaderRows { get; set; }
        /// <summary>
        /// 数据行
        /// </summary>
        public List<List<ExcelCellDataDefinition>> DataRows { get; set; }
        /// <summary>
        /// 获取标题行需要创建的记录数
        /// </summary>
        /// <returns></returns>
        public int GetHeaderRowCount()
        {
            return (int)this.HeaderRows.Select(p => p.EndIndex.X).Max<double>() + 1;
        }
        /// <summary>
        /// 数据行
        /// </summary>
        /// <returns></returns>
        public int GetDataRowCount()
        {
            return (int)this.DataRows.Count();
        }
    }

    public class ExcelHeaderDefinition
    {
        /// <summary>
        /// 开始坐标
        /// </summary>
        public Point StartIndex { get; set; }
        /// <summary>
        /// 终点坐标
        /// </summary>
        public Point EndIndex { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string CellValue { get; set; }
        /// <summary>
        /// 列宽
        /// </summary>
        public int ColumnWidth { get; set; }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <returns></returns>
         public bool IsMergeRowOrCell()
        {
            if (this.StartIndex.X != this.EndIndex.X || this.StartIndex.Y != this.EndIndex.Y)
            {
                return true;
            }
            return false;
        }
    }

    public class ExcelCellDataDefinition
    {
        /// <summary>
        /// 开始坐标
        /// </summary>
        public Point ColumnBeginEndIndex { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public LocalCellType CellDataType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string CellValue { get; set; }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <returns></returns>
        public bool IsMergeRowOrCell()
        {
            if (this.ColumnBeginEndIndex.X != this.ColumnBeginEndIndex.Y)
            {
                return true;
            }
            return false;
        }
    }

    public enum LocalCellType
    {
        Unknown = -1,
        Numeric = 0,
        String = 1,
        Formula = 2,
        Blank = 3,
        Boolean = 4,
        Error = 5,
    }
}
