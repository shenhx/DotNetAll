using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace webApiJwtAuthDemo
{
    public class FilterConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            // api 添加认证保护
            config.Filters.Add(new System.Web.Http.AuthorizeAttribute());
        }
    }
}
