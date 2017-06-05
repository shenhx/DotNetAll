using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentProgramConsoleApplication
{
    public class RequestContext
    {
        public string Formal { get; set; }
        public int Size{ get; set; }
        //不要直接设计协议类型，需要封装成类
        public RequestProtocol Protocol{ get; set; }
        public string Content { get; set; }
    }
}
