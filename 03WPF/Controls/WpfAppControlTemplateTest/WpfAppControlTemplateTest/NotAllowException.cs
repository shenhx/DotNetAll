using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfAppControlTemplateTest
{
    public class NotAllowException : Exception
    {
        // 摘要: 
        //     初始化 System.Exception 类的新实例。
        public NotAllowException():base(){}
        //
        // 摘要: 
        //     使用指定的错误消息初始化 System.Exception 类的新实例。
        //
        // 参数: 
        //   message:
        //     描述错误的消息。
        public NotAllowException(string message)
            : base(message)
        {

        }
    }
}
