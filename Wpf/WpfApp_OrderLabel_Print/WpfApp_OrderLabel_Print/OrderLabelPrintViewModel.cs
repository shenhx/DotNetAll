using KDY.Framework.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace WpfApp_OrderLabel_Print
{
    public class OrderLabelPrintViewModel : ViewModelBase
    {
        /// <summary>
        /// 二维码生成工具
        /// </summary>
        private static QRCodeEncoder codeEncoder = new QRCodeEncoder();

        /// <summary>
        /// 执行单内容初始化
        /// </summary>
        private ObservableCollection<OrderLableInfo> _adviceLables = new ObservableCollection<OrderLableInfo>
        {
            new OrderLableInfo
            {
                Selected = true,
                RePrint = true,
                AdvListInfo = new AdviceInfo()
                {
                    BedNo = "01床",
                    PatAge = "21岁",
                    PatName = "小五1111",
                    ExecTime = DateTime.Now,
                    UsageName = "静滴",
                    FrequenName = "bid",
                    PatSex = "男",
                    IpNo = "623654",
                    Seq = "1",
                    DeptName = "呼吸内科",
                    AdvTypeName = "长嘱"
                },
                AdviceLabelBytes = ImageToBytes(codeEncoder.Encode(new {FBILLNO = "1234561211",
                    FPATNO = "1234567811" }.ToString())),
                AdvListInfos = new ObservableCollection<AdviceInfo>()
                {
                    new AdviceInfo()
                    {
                        ItemName = "一二三四五六七八九十零一二三四五六七八九十零",
                        Dosage = "100ml"
                    },
                    new AdviceInfo()
                    {
                        ItemName = "1生理盐水静脉注errrrrsdsds射你好热热热热好红红火火液",
                        Dosage = "f高100ml"
                    },
                    //new AdviceInfo()
                    //{
                    //    ItemName = "生理盐水静脉注射液",
                    //    Dosage = "50ml"
                    //},
                    //new AdviceInfo()
                    //{
                    //    ItemName = "生理盐水静脉注errrrrsdsds射你好热热热热好红红火火液",
                    //    Dosage = "50ml"
                    //},
                    //new AdviceInfo()
                    //{
                    //    ItemName = "生理盐水静脉注射液",
                    //    Dosage = "50ml"
                    //},
                    new AdviceInfo()
                    {
                        ItemName = "生理盐水静脉注射液",
                        Dosage = "50ml"
                    }
                }
            }//,
            //new OrderLableInfo
            //{
            //    Selected = false,
            //    RePrint = false,
            //    AdvListInfo = new AdviceInfo()
            //    {
            //        BedNo = "02床",
            //        PatAge = "25岁",
            //        PatName = "小六",
            //        ExecTime = DateTime.Now,
            //        UsageName = "静滴",
            //        FrequenName = "bid",
            //        PatSex = "男",
            //        IpNo = "623654",
            //        DeptName = "呼吸内科",
            //        AdvTypeName = "临嘱",
            //        Seq = "1"
            //    },
            //    AdviceLabelBytes = ImageToBytes(codeEncoder.Encode(new {FBILLNO = "12345612",
            //        FPATNO = "12345678" }.ToString())),
            //    AdvListInfos = new ObservableCollection<AdviceInfo>()
            //    {
            //        new AdviceInfo()
            //        {
            //            ItemName = "葡萄糖静脉注射液",
            //            Dosage = "100ml"
            //        },
            //        new AdviceInfo()
            //        {
            //            ItemName = "生理盐水静脉注射液",
            //            Dosage = "50ml"
            //        }
            //    }
            //},
            //new OrderLableInfo
            //{
            //    Selected = true,
            //    RePrint = true,
            //    AdvListInfo = new AdviceInfo()
            //    {
            //        BedNo = "03床",
            //        PatAge = "17岁",
            //        PatName = "小四",
            //        ExecTime = DateTime.Now,
            //        UsageName = "静滴",
            //        FrequenName = "bid",
            //        PatSex = "男",
            //        IpNo = "623654",
            //        Seq = "1",
            //        DeptName = "呼吸内科",
            //        AdvTypeName = "长嘱"
            //    },
            //    AdviceLabelBytes = ImageToBytes(codeEncoder.Encode(new {FBILLNO = "12345612",
            //        FPATNO = "12345678" }.ToString())),
            //    AdvListInfos = new ObservableCollection<AdviceInfo>()
            //    {
            //        new AdviceInfo()
            //        {
            //            ItemName = "葡萄糖静脉注射液",
            //            Dosage = "100ml"
            //        },
            //        new AdviceInfo()
            //        {
            //            ItemName = "生理盐水静脉注射液",
            //            Dosage = "50ml"
            //        }
            //    }
            //},
            //new OrderLableInfo
            //{
            //    Selected = true,
            //    RePrint = true,
            //    AdvListInfo = new AdviceInfo()
            //    {
            //        BedNo = "05床",
            //        PatAge = "22岁",
            //        PatName = "小三",
            //        ExecTime = DateTime.Now,
            //        UsageName = "静滴",
            //        FrequenName = "bid",
            //        PatSex = "男",
            //        IpNo = "623654",
            //        Seq = "1",
            //        DeptName = "呼吸内科",
            //        AdvTypeName = "临嘱"
            //    },
            //    AdviceLabelBytes = ImageToBytes(codeEncoder.Encode(new {FBILLNO = "12345612",
            //        FPATNO = "12345678" }.ToString())),
            //    AdvListInfos = new ObservableCollection<AdviceInfo>()
            //    {
            //        new AdviceInfo()
            //        {
            //            ItemName = "葡萄糖静脉注射液",
            //            Dosage = "100ml"
            //        },
            //        new AdviceInfo()
            //        {
            //            ItemName = "生理盐水静脉注射液",
            //            Dosage = "50ml"
            //        }
            //    }
            //}
        };

        /// <summary>
        /// 执行单列表
        /// </summary>
        public ObservableCollection<OrderLableInfo> AdviceLables
        {
            get { return _adviceLables; }
            set { _adviceLables = value; }
        }

        /// <summary>
        /// 图像转为数组
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                else
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

    }
}
