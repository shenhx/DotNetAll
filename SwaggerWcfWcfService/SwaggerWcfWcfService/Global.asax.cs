using SwaggerWcf;
using SwaggerWcf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SwaggerWcfWcfService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("v1/rest", new WebServiceHostFactory(), typeof(IService1)));
            RouteTable.Routes.Add(new ServiceRoute("api-docs", new WebServiceHostFactory(), typeof(SwaggerWcfEndpoint)));

            var info = new Info
            {
                Description = "Sample Service to test SwaggerWCF",
                Version = "0.0.1"
            };

            var security = new SecurityDefinitions
            {
                {
                    "api-gateway",new SecurityAuthorization
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

            //
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