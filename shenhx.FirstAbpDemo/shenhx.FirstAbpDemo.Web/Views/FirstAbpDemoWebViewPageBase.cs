using Abp.Web.Mvc.Views;

namespace shenhx.FirstAbpDemo.Web.Views
{
    public abstract class FirstAbpDemoWebViewPageBase : FirstAbpDemoWebViewPageBase<dynamic>
    {

    }

    public abstract class FirstAbpDemoWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected FirstAbpDemoWebViewPageBase()
        {
            LocalizationSourceName = FirstAbpDemoConsts.LocalizationSourceName;
        }
    }
}