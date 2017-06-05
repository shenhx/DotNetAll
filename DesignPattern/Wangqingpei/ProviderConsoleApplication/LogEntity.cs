using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProviderConsoleApplication
{
    public class LogEntity
    {
        public string LogTypeName { get; set; }

        public string Level { get; set; }

        public LogContent Content { get; set; }
    }
}
