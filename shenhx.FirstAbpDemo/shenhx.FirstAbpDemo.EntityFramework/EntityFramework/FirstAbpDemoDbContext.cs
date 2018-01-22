using System.Data.Common;
using Abp.Zero.EntityFramework;
using shenhx.FirstAbpDemo.Authorization.Roles;
using shenhx.FirstAbpDemo.Authorization.Users;
using shenhx.FirstAbpDemo.MultiTenancy;

namespace shenhx.FirstAbpDemo.EntityFramework
{
    public class FirstAbpDemoDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public FirstAbpDemoDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in FirstAbpDemoDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of FirstAbpDemoDbContext since ABP automatically handles it.
         */
        public FirstAbpDemoDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public FirstAbpDemoDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public FirstAbpDemoDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
