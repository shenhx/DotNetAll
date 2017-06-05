using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineConsoleApplication
{
public    class Request
    {
        public StringBuilder Head { get; set; }
        public RequestClientType ClientType { get; set; }
        public RequestContent Content { get; set; }
    }
}
