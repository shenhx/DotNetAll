using SqliteApplication.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SqliteApplication
{
    [DbConfigurationType(typeof(SQLiteConfiguration))]
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext()
            : base("DbConn") 
        { 
            Database.SetInitializer<SqliteDbContext>(null); 
        }

        /// <summary>
        /// Person访问表
        /// </summary>
        public DbSet<PersonEntity> PersonDbContext { set; get; }
    }
}
