using System.Linq;
using shenhx.FirstAbpDemo.EntityFramework;
using shenhx.FirstAbpDemo.MultiTenancy;

namespace shenhx.FirstAbpDemo.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly FirstAbpDemoDbContext _context;

        public DefaultTenantCreator(FirstAbpDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
