using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentProgramConsoleApplication
{
    public static class RequestConfigManagerEx
    {
        public static RequestConfigManager SetGlobalRequestFormatBinary(this RequestConfigManager configManager)
        {
            if (string.IsNullOrEmpty(RequestConfigContext.configContext.Formal))
            {
                RequestConfigContext.configContext.Formal = "Binary";
            }
            return configManager;
        }
    }
}
