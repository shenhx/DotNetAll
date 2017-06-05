using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineConsoleApplication.ClientTypeModules
{
    public class OPerationLogicPipelineObjectFactory
    {
        public static IbuildOperationLogicPipelineObject Create(RequestClientType clientType)
        {
            if (clientType.type == RequestClientType.App)
                return new ClientTypeForAppType();
            else if (clientType.type == RequestClientType.NetClient)
                return new ClientTypeForNet2Type();
            return null;
        }
    }
}
