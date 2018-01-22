using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_OrderLabel_Print
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        OrderLabelPrintViewModel _viewModel = null;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new OrderLabelPrintViewModel();
        }
        
        private void Btn_Print_Click(object sender, RoutedEventArgs e)
        {
            //Print("1", "Microsoft XPS Document Writer");
            Print("1", "ZDesigner GK888t(EPL)");
        }

        private void Print(string pageDirection,string printerName)
        {
            PrintDialog print = new PrintDialog();
            var orderLableModels = _viewModel.AdviceLables;
           //打印队列
           LocalPrintServer localPrintServer = new LocalPrintServer();
            PrintQueueCollection printQueues = localPrintServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });

            if (printQueues == null || !printQueues.Any())
            {
                MessageBox.Show("PrintQueues为空，打印出错！");
                return;
            }

            //设置打印机信息
            PrintQueue queue = printQueues.FirstOrDefault(p => p.Name == printerName);
            if(queue == null)
            {
                MessageBox.Show("获取打印机驱动失败！");
            }
            print.PrintQueue = queue;
            int recordCount = _viewModel.AdviceLables.Count();


            FixedPage orderLabelPrintPage = null;
            //找程序目录下，如果找不到就读程序预制的打印样式文件。
            Uri printViewUrl = null;
            try
            {
                printViewUrl = new Uri("/WpfApp_OrderLabel_Print;component/OrderLabelPage.xaml", UriKind.RelativeOrAbsolute);
                orderLabelPrintPage = (FixedPage)Application.LoadComponent(printViewUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载打印资源错误，原因：{0}，请联系系统管理员！", ex.Message));
                return;
            }

            double height = orderLabelPrintPage.Height;
            double width = orderLabelPrintPage.Width;
            //打印纸张方向
            if ("0".Equals(pageDirection))
            {
                print.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;  //横印
                print.PrintTicket.PageMediaSize = new PageMediaSize(width, height);
            }
            else
            {
                print.PrintTicket.PageMediaSize = new PageMediaSize(height, width);
                print.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;  //横印
            }

            #region 直接打印
            FixedDocument fixedDocument = new FixedDocument();
            PageContent pageContent = new PageContent();

            ((IAddChild)pageContent).AddChild(orderLabelPrintPage);
            fixedDocument.Pages.Add(pageContent);
            
            TextBlock TbAdvType = (TextBlock)orderLabelPrintPage.FindName("TbAdvType");
            TextBlock tbBedNo = (TextBlock)orderLabelPrintPage.FindName("TbBedNo");
            TextBlock tbPatName = (TextBlock)orderLabelPrintPage.FindName("TbPatName");
            TextBlock tbPatSex = (TextBlock)orderLabelPrintPage.FindName("TbPatSex");
            Image imgCode = (Image)orderLabelPrintPage.FindName("ImgCode");
            TextBlock tbIpNo = (TextBlock)orderLabelPrintPage.FindName("TbIpNo");
            TextBlock tbPatAge = (TextBlock)orderLabelPrintPage.FindName("TbPatAge");
            TextBlock TbDeptName = (TextBlock)orderLabelPrintPage.FindName("TbDeptName");
            TextBlock TbRePrint = (TextBlock)orderLabelPrintPage.FindName("TbRePrint");

            StackPanel SpOrderList = (StackPanel)orderLabelPrintPage.FindName("SpOrderList");
            //样式
            Style ItemNameTextBlockStyle = (Style)orderLabelPrintPage.TryFindResource("ItemNameTextBlockStyle");
            Style DosageTextBlockStyle = (Style)orderLabelPrintPage.TryFindResource("DosageTextBlockStyle");
            //其他资源
            int iMaxItemWordCount = (int)orderLabelPrintPage.TryFindResource("MaxItemWordCount");
            int iMaxDosageWordCount = (int)orderLabelPrintPage.TryFindResource("MaxQuantityWordCount");

            TextBlock tbFreq = (TextBlock)orderLabelPrintPage.FindName("TbFreq");
            TextBlock tbSeq = (TextBlock)orderLabelPrintPage.FindName("TbSeq");
            TextBlock tbExecDate = (TextBlock)orderLabelPrintPage.FindName("TbExecDate");
            TextBlock tbUsage = (TextBlock)orderLabelPrintPage.FindName("TbUsage");

            for (int i = 0; i < recordCount; i++)
            {
                OrderLableInfo orderLable = orderLableModels[i];
                #region 第一部分
                TbAdvType.Text = orderLable.AdvListInfo.AdvTypeName;
                tbBedNo.Text = orderLable.AdvListInfo.BedNo;
                tbPatName.Text = orderLable.AdvListInfo.PatName;
                tbPatSex.Text = orderLable.AdvListInfo.PatSex;
                tbIpNo.Text = orderLable.AdvListInfo.IpNo;
                tbPatAge.Text = orderLable.AdvListInfo.PatAge;
                imgCode.Source = ConvertBytesToBitImage(orderLable.AdviceLabelBytes);
                TbDeptName.Text = orderLable.AdvListInfo.DeptName;
                TbRePrint.Visibility = orderLable.RePrint ? Visibility.Visible : Visibility.Hidden;
                #endregion

                #region 第二部分
                SpOrderList.Children.Clear();
                foreach (AdviceInfo advice in orderLable.AdvListInfos)
                {
                    Tuple<bool, string, string, string, string> tuple = HandleOrderContent(advice,iMaxItemWordCount,iMaxDosageWordCount);
                    if (tuple.Item1)
                    {
                        SpOrderList.Children.Add(GetWrapContentStackPanel(tuple.Item2,tuple.Item3, tuple.Item4,tuple.Item5, ItemNameTextBlockStyle, DosageTextBlockStyle));
                    }
                    else
                    {
                        SpOrderList.Children.Add(GetSingleContentStackPanel(tuple.Item2,tuple.Item4,ItemNameTextBlockStyle,DosageTextBlockStyle));
                    }
                }
                #endregion

                #region 第三部分
                tbFreq.Text = orderLable.AdvListInfo.FrequenName;
                tbSeq.Text = orderLable.AdvListInfo.Seq;
                tbExecDate.Text = orderLable.AdvListInfo.ExecTime == null ? "" : orderLable.AdvListInfo.ExecTime.Value.ToString("yyyy-MM-dd");
                tbUsage.Text = orderLable.AdvListInfo.UsageName;
                #endregion

                //documentViewer.Document = fixedDocument;
                print.PrintDocument(fixedDocument.DocumentPaginator, "贴瓶单");
            }

            #endregion
        }

        /// <summary>
        /// 将图像字节数组转成BitImage
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private BitmapImage ConvertBytesToBitImage(byte[] bytes)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.EndInit();
            return bitmapImage;
        }

        /// <summary>
        /// 医嘱内容超长换行处理
        /// </summary>
        /// <remarks>
        /// 处理思路：
        /// 1.首先判断医嘱项目名称，如果全部字数不超过itemMaxWordCount的话，则不做换行处理，返回当前内容
        /// 2.如果名字超长，但是超长不多于2个字，一样不做换行处理
        /// </remarks>
        /// <param name="advice"></param>
        /// <param name="maxItemWordCount"></param>
        /// <param name="maxDosageWordCount"></param>
        /// <returns></returns>
        private static Tuple<bool,string,string,string,string> HandleOrderContent(AdviceInfo advice,int maxItemWordCount=15,int maxDosageWordCount=3)
        {
            if(advice.ItemName.Length > maxItemWordCount + 2)
            {
                //需要计算是否需要换行（理论这里是需要换行的，但是不排除存在英文名称的药品名称）
                return GetSplitOrder(advice,maxItemWordCount,maxDosageWordCount);
            }
            else
            {
                return new Tuple<bool, string, string, string, string>(false,advice.ItemName,"",advice.Dosage,"");
            }
        }

        /// <summary>
        /// 切割
        /// </summary>
        /// <param name="advice"></param>
        /// <param name="maxItemWordCount"></param>
        /// <param name="maxDosageWordCount"></param>
        /// <returns></returns>
        private static Tuple<bool,string, string, string, string> GetSplitOrder(AdviceInfo advice, int maxItemWordCount, int maxDosageWordCount)
        {
            Tuple<bool, string, string, string, string> tuple = null;
            double itemNameLengthRecorder = 0.0;
            int itemNameIndex = 0;
            foreach (char charOfName in advice.ItemName)
            {
                if ((int)charOfName > 127)
                {
                    itemNameLengthRecorder += 1;
                }
                else
                {
                    itemNameLengthRecorder += 0.5;
                }
                itemNameIndex++;
                if (itemNameLengthRecorder >= maxItemWordCount - 0.5)
                {
                    break;
                }
                
            }
            if(itemNameLengthRecorder <= maxItemWordCount && itemNameIndex <= maxItemWordCount)
            {
                //不需要换行
                tuple = new Tuple<bool, string, string, string, string>(false,advice.ItemName,"",advice.Dosage,"");
            }
            else
            {
                string dosage1 = string.Empty;
                string dosage2 = string.Empty;
                if (advice.Dosage.Length > maxDosageWordCount)
                {
                    double dosageLengthRecorder = 0.0;
                    int dosageIndex = 0;
                    foreach (char charOfName in advice.Dosage)
                    {
                        if ((int)charOfName > 127)
                        {
                            dosageLengthRecorder += 1;
                        }
                        else
                        {
                            dosageLengthRecorder += 0.5;
                        }
                        dosageIndex++;
                        if (dosageLengthRecorder >= maxDosageWordCount)
                        {
                            break;
                        }
                        
                    }
                    if (dosageLengthRecorder <= maxDosageWordCount && dosageIndex <= maxDosageWordCount)
                    {
                        dosage1 = advice.Dosage;
                    }
                    else
                    {
                        dosage1 = advice.Dosage.Substring(0, dosageIndex);
                        dosage2 = advice.Dosage.Substring(dosageIndex);
                    }
                }
                else
                {
                    dosage1 = advice.Dosage;
                }
                tuple = new Tuple<bool, string, string, string, string>(true, advice.ItemName.Substring(0, itemNameIndex), advice.ItemName.Substring(itemNameIndex), dosage1, dosage2);
            }
            return tuple;
        }

        /// <summary>
        /// 获取对应的文本框
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        private static TextBlock GetTextBlock(Style style,string text)
        {
            TextBlock textBlock = new TextBlock();
            if (style != null)
                textBlock.Style = style;
            textBlock.Text = text;
            return textBlock;
        }

        /// <summary>
        /// 返回包含在StackPanel中换行的医嘱内容
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <param name="dosage1"></param>
        /// <param name="dosage2"></param>
        /// <param name="itemTbStyle"></param>
        /// <param name="dosageTbStyle"></param>
        /// <returns></returns>
        private static StackPanel GetWrapContentStackPanel(string item1,string item2,string dosage1,string dosage2,Style itemTbStyle,Style dosageTbStyle)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;//按照“项目名称 | 用量”格式拼接
            StackPanel itemStackPanel = new StackPanel();
            itemStackPanel.Orientation = Orientation.Vertical;
            itemStackPanel.Children.Add(GetTextBlock(itemTbStyle,item1));
            itemStackPanel.Children.Add(GetTextBlock(itemTbStyle,item2));
            StackPanel dosageStackPanel = new StackPanel();
            dosageStackPanel.Orientation = Orientation.Vertical;
            dosageStackPanel.Children.Add(GetTextBlock(dosageTbStyle, dosage1));
            dosageStackPanel.Children.Add(GetTextBlock(dosageTbStyle, dosage2));
            stackPanel.Children.Add(itemStackPanel);
            stackPanel.Children.Add(GetTextBlock(null, " | "));
            stackPanel.Children.Add(dosageStackPanel);
            return stackPanel;
        }

        /// <summary>
        /// 返回包含在StackPanel中单行的医嘱内容
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dosage"></param>
        /// <param name="itemTbStyle"></param>
        /// <param name="dosageTbStyle"></param>
        /// <returns></returns>
        private static StackPanel GetSingleContentStackPanel(string item, string dosage, Style itemTbStyle, Style dosageTbStyle)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;//按照“项目名称 | 用量”格式拼接
            stackPanel.Children.Add(GetTextBlock(itemTbStyle, item));
            stackPanel.Children.Add(GetTextBlock(null, " | "));
            stackPanel.Children.Add(GetTextBlock(dosageTbStyle, dosage));
            return stackPanel;
        }
    }
}
