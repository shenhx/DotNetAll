using Abp.Application.Services;
using Abp.Application.Services.Dto;
using shenhx.FirstAbpDemo.MultiTenancy.Dto;

namespace shenhx.FirstAbpDemo.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
