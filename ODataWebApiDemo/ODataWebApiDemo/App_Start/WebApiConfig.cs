using ODataWebApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace ODataWebApiDemo
{
    public static class WebApiConfig
    {
        //public static void Register(HttpConfiguration config)
        //{
        //// Web API 配置和服务

        //// Web API 路由
        //config.MapHttpAttributeRoutes();

        //config.Routes.MapHttpRoute(
        //    name: "DefaultApi",
        //    routeTemplate: "api/{controller}/{id}",
        //    defaults: new { id = RouteParameter.Optional }
        //);
        //}

        public static void Register(HttpConfiguration config)
        {

            //新代码
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Person>("Persons");
            config.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: "odata",
                model: builder.GetEdmModel()
                );
        }
    }
}
