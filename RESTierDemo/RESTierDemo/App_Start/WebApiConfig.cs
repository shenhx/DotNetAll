using Microsoft.Restier.Providers.EntityFramework;
using Microsoft.Restier.Publishers.OData;
using Microsoft.Restier.Publishers.OData.Batch;
using RESTierDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Extensions;

namespace RESTierDemo
{
    public static class WebApiConfig
    {
        //public static void Register(HttpConfiguration config)
        //{
        //    // Web API 配置和服务

        //    // Web API 路由
        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );
        //}

        public async static void Register(HttpConfiguration config)
        {
            // enable query options for all properties
            config.Filter().Expand().Select().OrderBy().MaxTop(null).Count();
            await config.MapRestierRoute<EntityFrameworkApi<AdventureWorksLT>>(
                "AdventureWorksLT",
                "api/AdventureWorksLT",
                new RestierBatchHandler(GlobalConfiguration.DefaultServer));
        }
    }
}
