using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineConsoleApplication.ClientModule
{
    public delegate void ClientPipelineObjectModules(Request request);
    /// <summary>
    /// 客户端管道对象
    /// </summary>
    public class ClientPipelineObject
    {
        private ClientPipelineObjectModules modules;

        public void AddModule(ClientPipelineObjectModules module)
        {
            modules += module;
        }

        public void RunPipeline(Request request)
        {
            modules(request);
        }
    }
}
