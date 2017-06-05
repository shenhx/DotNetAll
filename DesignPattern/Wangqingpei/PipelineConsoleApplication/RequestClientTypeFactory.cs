using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineConsoleApplication
{
    public class RequestClientTypeFactory
    {
        public static RequestClientType CreateRequestClientTypeForApp()
        {
            return new RequestClientType { type = RequestClientType.App };
        }

        public static RequestClientType CreateRequestClientTypeForNet2()
        {
            return new RequestClientType { type = RequestClientType.NetClient };
        }

    }
}
