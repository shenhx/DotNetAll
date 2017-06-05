using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageComponentConsoleApplication
{
    /// <summary>
    /// 语句组件基类
    /// </summary>
    [Serializable]
    public abstract class LanguageComponent
    {
        public virtual void Run(LanguageComponentManager trachMark) { }

        public virtual void Run(object parameter, LanguageComponentManager trackMark) { }
    }
}
