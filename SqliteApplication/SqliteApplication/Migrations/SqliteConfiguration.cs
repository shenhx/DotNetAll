using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SqliteApplication.Migrations
{
    public class SQLiteConfiguration : DbConfiguration
    {
        public SQLiteConfiguration()
        {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            Type t = Type.GetType("System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6");
            FieldInfo fi = t.GetField("Instance", BindingFlags.NonPublic | BindingFlags.Static);
            SetProviderServices("System.Data.SQLite", (System.Data.Entity.Core.Common.DbProviderServices)fi.GetValue(null));
            //使用自定义SQLite代码覆盖MigrationSqlGenerator
            SetMigrationSqlGenerator("System.Data.SQLite", () => 
            {
                return new MigrationSqLiteGenerator();
            });
        }
    }
}
