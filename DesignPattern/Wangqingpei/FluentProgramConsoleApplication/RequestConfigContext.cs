using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentProgramConsoleApplication
{
    public class RequestConfigContext
    {
        public static RequestConfigContext configContext = new RequestConfigContext();
        /// <summary>
        /// 
        /// </summary>
        public string Formal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 协议类型
        /// </summary>
        public RequestProtocol Protocol { get; set; }
        /// <summary>
        /// 请求上下文的安全检查
        /// </summary>
        public Func<RequestContext,bool> SafetyChecks{ get; set; }
    }
}
