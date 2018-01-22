using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using shenhx.FirstAbpDemo.Configuration.Dto;

namespace shenhx.FirstAbpDemo.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : FirstAbpDemoAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
