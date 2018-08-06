using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ThoughtWorks.QRCode;

namespace WpfApp_OrderLabel_Print
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void btnGenerator_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtQrCodeText.Text))
                return;
            ThoughtWorks.QRCode.Codec.QRCodeEncoder qrCodeEncoder = new ThoughtWorks.QRCode.Codec.QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
            var image =  qrCodeEncoder.Encode(txtQrCodeText.Text);
            imgQrCode.Source = BitmapToBitmapImage(image);
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png); // 坑点：格式选Bmp时，不带透明度

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog print = new PrintDialog();
            //打印队列
            LocalPrintServer localPrintServer = new LocalPrintServer();
            PrintQueueCollection printQueues = localPrintServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });

            if (printQueues == null || !printQueues.Any())
            {
                MessageBox.Show("PrintQueues为空，打印出错！");
                return;
            }

            //设置打印机信息
            PrintQueue queue = printQueues.FirstOrDefault(p => p.Name.Equals(txtPrinterName.Text));
            if (queue == null)
            {
                MessageBox.Show("获取打印机驱动失败！");
            }
            print.PrintQueue = queue;
            
            double height = imgQrCode.Height;
            double width = imgQrCode.Width;
            //打印纸张方向
            if ("0".Equals("0"))
            {
                print.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;  //竖印
                print.PrintTicket.PageMediaSize = new PageMediaSize(width, height);
            }
            else
            {
                print.PrintTicket.PageMediaSize = new PageMediaSize(height, width);
                print.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;  //横印
            }

            #region 直接打印
            print.PrintVisual(imgQrCode, "测试");
            #endregion
        }
    }
}
