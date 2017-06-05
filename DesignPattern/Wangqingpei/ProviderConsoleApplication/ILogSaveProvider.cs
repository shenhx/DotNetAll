using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProviderConsoleApplication
{
    public interface ILogSaveProvider
    {
        bool SaveLog(LogEntity log);
    }
}
