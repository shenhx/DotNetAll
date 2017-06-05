using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipelineConsoleApplication.ClientModule
{
    /// <summary>
    /// 客户端modules
    /// </summary>
    public class ClientPipelineModules
    {
        public static void CheckRequestCOntent(Request request)
        {
            if (request == null || request.Content == null || string.IsNullOrEmpty(request.Content.Content))
            {
                throw new InvalidOperationException("无效请求");
            }
        }

        public static void AddRequestHead(Request request)
        {
            request.Head.Append("Request source:SOA Client");
        }

        public static void TransferRequestFormat(Request request)
        {
            request.Content.Content = TransferRequestForRest.Transfer(request.Content.Content);
        }

        public static void ReduceRequest(Request request)
        {
            ReduceRequestBody.Reduce(request);
        }
    }
}
