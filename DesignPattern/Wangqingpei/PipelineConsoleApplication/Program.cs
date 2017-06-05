using PipelineConsoleApplication.ClientTypeModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Request request = new Request()
            {
                Content = new RequestContent() { Content = "query order method" },
                Head = new StringBuilder(),
                ClientType = RequestClientTypeFactory.CreateRequestClientTypeForNet2()
            };
            ClientModule.ClientPipelineObject pipe = new ClientModule.ClientPipelineObject();
            pipe.AddModule(ClientModule.ClientPipelineModules.CheckRequestCOntent);
            pipe.AddModule(ClientModule.ClientPipelineModules.AddRequestHead);
            pipe.AddModule(ClientModule.ClientPipelineModules.TransferRequestFormat);
            pipe.AddModule(ClientModule.ClientPipelineModules.ReduceRequest);
            pipe.RunPipeline(request);
            //-------以上方法为客户端
            IbuildOperationLogicPipelineObject clientType = OPerationLogicPipelineObjectFactory.Create(request.ClientType);
            OperationLogicPipelineObject pipeline = clientType.BuildOperationPipeline(request);
            pipeline.RunPipeline(request);
        }
    }
}
