using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using shenhx.FirstAbpDemo.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace shenhx.FirstAbpDemo.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<FirstAbpDemo.EntityFramework.FirstAbpDemoDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FirstAbpDemo";
        }

        protected override void Seed(FirstAbpDemo.EntityFramework.FirstAbpDemoDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
