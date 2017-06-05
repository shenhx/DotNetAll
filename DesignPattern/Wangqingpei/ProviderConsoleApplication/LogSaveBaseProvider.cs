using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ProviderConsoleApplication
{
    /**
     * 说明：
     * 1.不能将SaveLog直接暴露在外部给提供程序实现者使用，否则将失去最高的控制权。
     */
    /// <summary>
    /// 
    /// </summary>
    public abstract class LogSaveBaseProvider : ILogSaveProvider
    {

        public bool SaveLog(LogEntity log)
        {
            if (!this.IsSaveLogWithConfiguration(log)) return false;
            if (!this.ValidateLogEntity(log)) return false;
            this.FormatLogContent(log);
            return this.DoSaveLog(log);
        }


        protected virtual bool IsSaveLogWithConfiguration(LogEntity log)
        {
            string logType = ConfigurationManager.AppSettings["LogType"];
            if (log.LogTypeName.Equals(logType))
            {
                return true;
            }
            return false;
        }

        protected virtual bool ValidateLogEntity(LogEntity log)
        {
            if (log == null || log.Content == null)
            {
                return false;
            }
            return true;
        }

        protected virtual void FormatLogContent(LogEntity log)
        {
            
        }

        protected abstract bool DoSaveLog(LogEntity log);

    }
}
