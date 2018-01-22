using System.Threading.Tasks;
using Abp.Application.Services;
using shenhx.FirstAbpDemo.Sessions.Dto;

namespace shenhx.FirstAbpDemo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
