using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using shenhx.FirstAbpDemo.Authorization.Users;
using shenhx.FirstAbpDemo.Editions;

namespace shenhx.FirstAbpDemo.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }
    }
}