using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;
using System.Linq;
using System;
using System.IO;

namespace shenhx.FirstAbpDemo.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(FirstAbpDemoApplicationModule))]
    public class FirstAbpDemoWebApiModule : AbpModule
    {
        public override void PreInitialize()
        {
            //原因是在1.0后ABP开启了一个功能”Cross - Site Request Forgery”CSRF 中文叫做跨站请求伪造
            //第一种办法：修改js代码
            /*
             var csrfCookie = getCookieValue("XSRF-TOKEN");
             var csrfCookieAuth = new SwaggerClient.ApiKeyAuthorization("X-XSRF-TOKEN", csrfCookie, "header");
             swaggerUi.api.clientAuthorizations.add("X-XSRF-TOKEN", csrfCookieAuth);
             */
            //第二种办法：关闭跨域请求
            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;
            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(FirstAbpDemoApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
            //调用swagger
            this.ConfigureSwaggerUi();
        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "shenhx.FirstAbpDemo的Swagger的说明文档");
                c.ResolveConflictingActions(apiDescs => apiDescs.First());

                //添加中文注释
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string commentFileName = @"bin\shenhx.FirstAbpDemo.Application.xml";
                string commentFile = Path.Combine(baseDirectory,commentFileName);
                c.IncludeXmlComments(commentFile);

            }).EnableSwaggerUi("apis/{*assetPath}",b => {
                //b.InjectJavaScript();
                //b.InjectStylesheet();
                //替换中文
                b.InjectJavaScript(Assembly.GetExecutingAssembly(), "shenhx.FirstAbpDemo.SwaggerUi.Script.swagger.translate.js");
            });
        }
    }
}
