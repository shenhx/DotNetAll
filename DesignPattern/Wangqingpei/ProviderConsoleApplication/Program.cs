using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProviderConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            LogEntity log = LogEntityFactory.CreateLogEntity("1212121212", LogType.Exception, LogLevel.Graveness);
            log.Content.LogTrackInfo = "Program.Main";
            ILogSaveProvider saveProvider = new LogSaveLocalhostProvider();
            saveProvider.SaveLog(log);
            Console.ReadKey();
        }
    }
}
