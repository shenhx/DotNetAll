
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

using System.Data.Entity.Migrations;

namespace SqliteApplication.Initlize
{
    /// <summary>
    /// p_person表初始化
    /// todo:在SQLite数据库中没办法实现
    /// </summary>
    public class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable("p_person", c => new 
            {
                fid = c.String(),
                fname = c.String(),
                fsex = c.String(),
                fage = c.Int()
            }).PrimaryKey(c => c.fid);
        }

        public override void Down()
        {
            DropTable("p_person");
        }
    }
}
