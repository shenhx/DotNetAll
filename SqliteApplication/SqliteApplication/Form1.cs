using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SqliteApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //初始化脚本
            string initSql = "create table if not exists p_test(fid varchar(20) primary key,fname varchar(50),fage integer,fsex varchar(5));";
            this.ExcuteSqlNonTrans(initSql);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM p_test";
            //string sql = "select  from sqlite_master;";
            string connStr = @"Data Source=" + System.IO.Path.Combine(Environment.CurrentDirectory, "test.db3") + @";Initial Catalog=test.db3;Pooling=true";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                //conn.Open();
                using (SQLiteDataAdapter ap = new SQLiteDataAdapter(sql, conn))
                {
                    DataSet ds = new DataSet();
                    ap.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private int  ExcuteSqlNonTrans(string sql)
        {
            int result = -1;
            string connStr = @"Data Source=" + System.IO.Path.Combine(Environment.CurrentDirectory, "test.db3") + @";Initial Catalog=test.db3;Pooling=true";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM p_person";
            //string sql = "select  from sqlite_master;";
            string connStr = @"Data Source=D:\Projects\DotNet\Demos\KingdeeDemos\WcfDemo\WcfServiceApplication\WcfServiceApplication\bin\App_Data\kdtest.db3;Initial Catalog=kdtest.db3;Pooling=true";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                //conn.Open();
                using (SQLiteDataAdapter ap = new SQLiteDataAdapter(sql, conn))
                {
                    DataSet ds = new DataSet();
                    ap.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqliteDbContext context = new SqliteDbContext())
            {
                PersonEntity person = new PersonEntity();
                person.Id = Guid.NewGuid().ToString("N");
                person.Name = "晓明" + new Random().Next();
                person.Sex = new Random().Next() % 2 == 0?"男":"女";
                person.Age = new Random().Next();
                context.PersonDbContext.Add(person);
                context.SaveChanges();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqliteDbContext context = new SqliteDbContext())
            {
                dataGridView1.DataSource = context.PersonDbContext.ToList();
            }
        }
    }
}
