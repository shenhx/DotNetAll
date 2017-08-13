using SwaggerWcf.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceDemo
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [SwaggerWcf("/v1/rest")]
    public class Service1 : IService1
    {
        /// <summary>
        /// 返回Hello World
        /// </summary>
        /// <returns></returns>
        [SwaggerWcfTag("Test")]
        [SwaggerWcfResponse(HttpStatusCode.OK, "hello World!")]
        [SwaggerWcfResponse(HttpStatusCode.NoContent, "error", true)]
        public string SayHelloWorld()
        {
            return "Hello World!";
        }
    }
}
