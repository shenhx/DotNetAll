using Abp.MultiTenancy;
using shenhx.FirstAbpDemo.Authorization.Users;

namespace shenhx.FirstAbpDemo.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}