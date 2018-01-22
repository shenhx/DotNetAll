using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using shenhx.FirstAbpDemo.Roles.Dto;
using shenhx.FirstAbpDemo.Users.Dto;

namespace shenhx.FirstAbpDemo.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        /// <summary>
        /// ��ȡ���н�ɫ
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}