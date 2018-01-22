using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace shenhx.FirstAbpDemo.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : FirstAbpDemoControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}