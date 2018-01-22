using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using shenhx.FirstAbpDemo.EntityFramework;

namespace shenhx.FirstAbpDemo.Migrator
{
    [DependsOn(typeof(FirstAbpDemoDataModule))]
    public class FirstAbpDemoMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<FirstAbpDemoDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}