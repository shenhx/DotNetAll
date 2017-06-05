using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineConsoleApplication.ClientTypeModules
{
    public interface IbuildOperationLogicPipelineObject
    {
        OperationLogicPipelineObject BuildOperationPipeline(Request request);
    }
}
