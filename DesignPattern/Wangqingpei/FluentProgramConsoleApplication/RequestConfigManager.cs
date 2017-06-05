using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentProgramConsoleApplication
{
    /// <summary>
    /// 传统的上下文配置管理器
    /// </summary>
    public class RequestConfigManager
    {
        public static RequestConfigManager configManager = new RequestConfigManager();

        public RequestConfigManager SetGlobalRequestFormatJson()
        {
            if (string.IsNullOrEmpty(RequestConfigContext.configContext.Formal))
            {
                RequestConfigContext.configContext.Formal = "Json";
            }
            return this;
        }

        public RequestConfigManager SetGlobalRequestFormatXml()
        {
            if (string.IsNullOrEmpty(RequestConfigContext.configContext.Formal))
            {
                RequestConfigContext.configContext.Formal = "Xml";
            }
            return this;
        }

        public RequestConfigManager SetGlobalRequestProtocol(RequestProtocol protocol)
        {
            RequestConfigContext.configContext.Protocol = protocol;
            return this;
        }

        public RequestConfigManager SetGlobalRequstSize(int size)
        {
            RequestConfigContext.configContext.Size = size;
            return this;
        }
    }
}
