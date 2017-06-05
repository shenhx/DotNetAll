using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineConsoleApplication.ClientTypeModules
{
    public class ClientTypeForNet2Type : IbuildOperationLogicPipelineObject
    {
        public OperationLogicPipelineObject BuildOperationPipeline(Request request)
        {
            var result = new OperationLogicPipelineObject();
            result.Add(reqObject =>
            {

            });
            result.Add(reqObject =>
            {

            });
            return result;
        }
    }
}
