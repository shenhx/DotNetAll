using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentContext context = new StudentContext();
            var students = context.Students;
            Student student = new Student();
            student.Id = students.Count() + 1;
            student.Name = "小铭";
            context.Students.Add(student);
            context.SaveChanges();
            foreach (Student stu in context.Students)
            {
                Console.Write("学生{0}姓名：{1}",stu.Id,stu.Name);
                Console.WriteLine();
            }
            Console.WriteLine("读取完毕！");
            Console.ReadKey();
        }
    }
}
