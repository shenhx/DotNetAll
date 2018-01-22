using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using shenhx.FirstAbpDemo.Roles.Dto;

namespace shenhx.FirstAbpDemo.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
