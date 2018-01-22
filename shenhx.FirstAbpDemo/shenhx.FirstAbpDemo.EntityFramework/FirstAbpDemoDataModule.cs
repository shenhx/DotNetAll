using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using shenhx.FirstAbpDemo.EntityFramework;

namespace shenhx.FirstAbpDemo
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(FirstAbpDemoCoreModule))]
    public class FirstAbpDemoDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<FirstAbpDemoDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
