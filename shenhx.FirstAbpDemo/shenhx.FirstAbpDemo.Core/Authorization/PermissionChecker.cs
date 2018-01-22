using Abp.Authorization;
using shenhx.FirstAbpDemo.Authorization.Roles;
using shenhx.FirstAbpDemo.Authorization.Users;

namespace shenhx.FirstAbpDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
