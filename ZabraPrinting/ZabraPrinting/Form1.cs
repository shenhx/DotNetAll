using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;

namespace ZabraPrinting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class LocalReportHelper : IDisposable
        {

            #region Properties

            public String LocalReportPath { get; set; }
            public LocalReport LocalReportInstance { get; private set; }
            public GraphDeviceInfo ExportImageDeviceInfo { get; set; }
            public PrintLog PrintLogInformation { get; set; }
            public int PrintTypeNO { get; set; }

            #endregion

            private Stream _stream;
            private int _bmpDPI;
            private DataSet _ds;

            public LocalReportHelper(String reportPath)
                : this(reportPath, new GraphDeviceInfo()
                {
                    ColorDepth = 1,
                    DpiX = 203,
                    DpiY = 203,
                    PageWidth = 8f,
                    PageHeight = 12.2f,
                    MarginTop = 0.2f
                }) { }

            public LocalReportHelper(String reportPath, GraphDeviceInfo deviceInfo)
            {
                this._bmpDPI = 96;

                this.LocalReportPath = reportPath;
                this.ExportImageDeviceInfo = deviceInfo;
                this.LocalReportInstance = new LocalReport() { ReportPath = reportPath };
                this.LocalReportInstance.SubreportProcessing += new SubreportProcessingEventHandler(LocalReportInstance_SubreportProcessing);
            }

            private void LocalReportInstance_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
            {
                foreach (DataTable dt in this._ds.Tables)
                {
                    e.DataSources.Add(new ReportDataSource(dt.TableName, dt));
                }
            }

            public void Dispose()
            {
                this.ExportImageDeviceInfo = null;
                this.LocalReportInstance = null;
                this.LocalReportPath = null;

                this._ds = null;
            }

            public void AddReportParameter(string paramName, string value)
            {
                this.LocalReportInstance.SetParameters(new ReportParameter(paramName, value));
            }

            #region Export hang title image by reporting services

            private void AddWashIcones()
            {
                this.LocalReportInstance.EnableExternalImages = true;
                this.LocalReportInstance.SetParameters(new ReportParameter("AppPath", Application.StartupPath));
            }

            private void AddBarcodesAssembly()
            {
                //this.LocalReportInstance.AddTrustedCodeModuleInCurrentAppDomain(ConfigurationManager.AppSettings["BarcodesAssembly"]);
            }

            private Stream CreateStreamCallBack(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
            {
                string tempFolderPath = string.Concat(Environment.GetEnvironmentVariable("TEMP"), "/");
                this._stream = new FileStream(string.Concat(tempFolderPath, name, ".", fileNameExtension), FileMode.Create);
                return this._stream;
            }
            private void Export()
            {
                if (string.IsNullOrEmpty(this.LocalReportPath) ||
                    this.LocalReportInstance == null ||
                    this.ExportImageDeviceInfo == null)
                {
                    throw new InvalidExpressionException("Invalid parameters");
                }
                if (this.PrintTypeNO >= -1 || this.PrintTypeNO == -3)
                {
                    this.AddBarcodesAssembly();
                    if (this.PrintTypeNO >= 0)
                    {
                        this.AddWashIcones();
                    }
                }
                if (this.PrintTypeNO >= 0)
                {
                    this.LocalReportInstance.DataSources.Clear();
                    foreach (DataTable dt in this._ds.Tables)
                    {
                        this.LocalReportInstance.DataSources.Add(new ReportDataSource(dt.TableName, dt));
                    }
                }
                this.LocalReportInstance.Refresh();

                if (this._stream != null)
                {
                    this._stream.Close();
                    this._stream = null;
                }
                Warning[] warnings;
                this.LocalReportInstance.Render(
                    "Image",
                    this.ExportImageDeviceInfo.ToString(),
                    PageCountMode.Actual,
                    CreateStreamCallBack,
                    out warnings);
                this._stream.Position = 0;
            }
            private void SaveLog()
            {
                using (Bitmap image = (Bitmap)Image.FromStream(this._stream))
                {
                    image.SetResolution(96, 96);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Jpeg);
                        this.PrintLogInformation.BarcodeImage = ms.ToArray();
                    }
                }
                LogHelper.AddLog(this.PrintLogInformation);
                if (this._stream != null)
                {
                    this._stream.Close();
                    this._stream = null;
                }
            }

            #endregion

            #region Windows driver print

            private void PrintPage(object sender, PrintPageEventArgs ev)
            {
                Bitmap bmp = (Bitmap)Image.FromStream(this._stream);
                bmp.SetResolution(this._bmpDPI, this._bmpDPI);
                ev.Graphics.DrawImage(bmp, 0, 0);
                ev.HasMorePages = false;
            }
            /// <summary>  
            /// Print by windows driver  
            /// </summary>  
            /// <param name="printerSettings">.net framework PrinterSettings class, including some printer information</param>  
            /// <param name="bmpDPI">the bitmap image resoluation, dots per inch</param>  
            public void WindowsDriverPrint(PrinterSettings printerSettings, int bmpDPI, DataSet ds)
            {
                this._ds = ds;
                this.Export();
                if (this._stream == null)
                {
                    return;
                }
                this._bmpDPI = bmpDPI;

                PrintDocument printDoc = new PrintDocument();
                printDoc.DocumentName = "条码打印";
                printDoc.PrinterSettings = printerSettings;
                printDoc.PrintController = new StandardPrintController();
                if (!printDoc.PrinterSettings.IsValid)
                {
                    if (this._stream != null)
                    {
                        this._stream.Close();
                        this._stream = null;
                    }
                    MessageBox.Show("Printer found errors, Please contact your administrators！", "Print Error");
                    return;
                }
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                printDoc.Print();
                this.SaveLog();
            }

            #endregion

            #region Original print

            /// <summary>  
            /// Send the file to the printer or use the printer command  
            /// </summary>  
            /// <param name="deviceType">The port(LPT,COM,DRV) which the device connecting</param>  
            /// <param name="copies">Total number for print</param>  
            /// <param name="ds">The report datasource</param>  
            /// <param name="printParam">Print parameters</param>  
            /// <param name="printLanguage">The built-in printer programming language, you can choose EPL or ZPL</param>  
            public void OriginalPrint(DeviceType deviceType,
                DataSet ds,
                int copies = 1,
                string printParam = null,
                ProgrammingLanguage printLanguage = ProgrammingLanguage.ZPL)
            {
                this._ds = ds;
                this.Export();
                if (this._stream == null)
                {
                    return;
                }
                int port = 1;
                int.TryParse(printParam, out port);
                int length = (int)this._stream.Length;
                byte[] bytes = new byte[length];
                this._stream.Read(bytes, 0, length);
                ZebraPrintHelper.Copies = copies;
                ZebraPrintHelper.PrinterType = deviceType;
                ZebraPrintHelper.PrinterName = printParam;
                ZebraPrintHelper.Port = port;
                ZebraPrintHelper.PrinterProgrammingLanguage = printLanguage;
                ZebraPrintHelper.PrintGraphics(bytes);
                this.SaveLog();
            }

            #endregion

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
