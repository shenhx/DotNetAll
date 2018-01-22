using System.Threading.Tasks;
using Abp.Application.Services;
using shenhx.FirstAbpDemo.Configuration.Dto;

namespace shenhx.FirstAbpDemo.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}