using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using SwaggerWcf.Models;
using SwaggerWcf;

namespace WcfServiceDemo
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //添加路由地址前缀，如有多个接口，需要添加多个
            RouteTable.Routes.Add(new ServiceRoute("v1/rest", new WebServiceHostFactory(), typeof(Service1)));
            //swagger测试地址：http://localhost:1370/api-docs，端口号可能会变
            RouteTable.Routes.Add(new ServiceRoute("api-docs", new WebServiceHostFactory(), typeof(SwaggerWcfEndpoint)));

            var info = new Info
            {
                Description = "Sample Service to test SwaggerWCF",
                Version = "0.0.1"
                // etc
            };

            var security = new SecurityDefinitions
            {
                {
                    "api-gateway", new SecurityAuthorization
                    {
                        Type = "oauth2",
                        Name = "api-gateway",
                        Description = "Forces authentication with credentials via an api gateway",
                        Flow = "implicit",
                        Scopes = new Dictionary<string, string>
                        {
                            { "fu", "use fu scope"},
                            { "bar", "use bar scope"},
                        },
                        AuthorizationUrl = "http://yourapi.net/oauth/token"
                    }
                }
            };
            //配置Swagger
            SwaggerWcfEndpoint.Configure(info, security);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
