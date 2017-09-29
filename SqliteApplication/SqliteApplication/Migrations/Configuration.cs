using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace SqliteApplication.Migrations
{
    internal sealed class Configuration 
        : DbMigrationsConfiguration<SqliteDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
