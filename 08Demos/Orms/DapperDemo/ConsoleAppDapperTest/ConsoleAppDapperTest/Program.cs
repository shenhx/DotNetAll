using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Dapper;

namespace ConsoleAppDapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var list =  con.Query<LisInfo>("select * from LisInfo where ipno = @IpNo and createdate > convert(datetime,@CreateDate,120)", new { IpNo = "123457",CreateDate = DateTime.Now.AddDays(-100).ToString("yyyy-MM-dd") });
            if(list != null && list.Count()>0)
            {
                foreach (var item in list)
                {
                    Console.WriteLine(item.IpNo);
                    Console.WriteLine(item.CreateDate);
                    Console.WriteLine("/");
                }
                
            }
            else
            {
                Console.WriteLine("没有发现记录！");
            }
            Console.ReadKey();
        }
    }

    public class LisInfo
    {
        public int Id { get; set; }
        public string IpNo { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
