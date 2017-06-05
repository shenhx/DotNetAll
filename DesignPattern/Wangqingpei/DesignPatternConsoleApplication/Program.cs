using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<EmployeeFactory.EmployeeCreatePrameterContex, Employee.EmployeeAddress> 
                addressFactory = (ctx) => new Employee.EmployeeAddress()
            {
                Address1 = ctx.AddressString.Split('、')[0],
                Address2 = ctx.AddressString.Split('、')[1],
            };
            Employee emp = EmployeeFactory.CreatEmployee("shx", "广州、茂名", addressFactory);
            //if (emp != null)
            //{
            //    Console.WriteLine("emp创建成功！");
            //}
            //else
            //{
            //    Console.WriteLine("emp创建失败！");
            //}
            emp.EmployeeChangeNameEvent +=emp_EmployeeChangeNameEvent;
            emp.ChangeName("ssss");
            Console.ReadKey();
        }

        private static void emp_EmployeeChangeNameEvent(string name)
        {
            Console.WriteLine("修改后的名称：{0}",name);
        }
    }
}
