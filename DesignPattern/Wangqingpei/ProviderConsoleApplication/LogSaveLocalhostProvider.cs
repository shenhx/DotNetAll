using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProviderConsoleApplication
{
    public class LogSaveLocalhostProvider : LogSaveBaseProvider
    {
        protected override bool DoSaveLog(LogEntity log)
        {
            Console.WriteLine("已经保存成功，保存内容"+ log.Content.Message);
            return true;
        }

        protected override bool ValidateLogEntity(LogEntity log)
        {
            if (base.ValidateLogEntity(log))
            {
                if (string.IsNullOrEmpty(log.Content.LogTrackInfo)) return false;
            }
            return true;
        }

        protected override void FormatLogContent(LogEntity log)
        {
            log.Content.Message = log.Content.Message.Replace("\\", "--");
        }
    }
}
