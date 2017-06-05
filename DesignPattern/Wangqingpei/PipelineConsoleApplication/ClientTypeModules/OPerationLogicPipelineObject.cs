using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineConsoleApplication.ClientTypeModules
{
    public delegate void OperationLogicPipelineObjectModules(Request request);
    public class OperationLogicPipelineObject
    {
        private OperationLogicPipelineObjectModules modules;
        internal void Add(OperationLogicPipelineObjectModules module)
        {
            this.modules = module;
        }

        public void RunPipeline(Request request)
        {
            this.modules(request);
        }
    }
}
