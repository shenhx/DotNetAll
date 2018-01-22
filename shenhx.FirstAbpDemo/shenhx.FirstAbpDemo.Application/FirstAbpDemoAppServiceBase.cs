using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using shenhx.FirstAbpDemo.Authorization.Users;
using shenhx.FirstAbpDemo.MultiTenancy;
using shenhx.FirstAbpDemo.Users;
using Microsoft.AspNet.Identity;

namespace shenhx.FirstAbpDemo
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class FirstAbpDemoAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected FirstAbpDemoAppServiceBase()
        {
            LocalizationSourceName = FirstAbpDemoConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}