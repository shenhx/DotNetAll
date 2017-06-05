using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProviderConsoleApplication
{
    public class LogEntityFactory
    {
        internal static LogEntity CreateLogEntity(string message, string logType, string loglevel)
        {
            LogEntity log = new LogEntity();
            log.Content = new LogContent();
            log.Content.Message = message;
            log.LogTypeName = logType;
            log.Level = loglevel;
            return log;
        }
    }
}
