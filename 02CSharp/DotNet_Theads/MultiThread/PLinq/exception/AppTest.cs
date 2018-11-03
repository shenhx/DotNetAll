using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLinq.exception
{
public    class AppTest
    {
        public static void Run()
        {
            IEnumerable<int> numbers = Enumerable.Range(-5, 10);
            var query = from number in numbers
                        select 100 / number;
            try
            {
                foreach (var n in query)
                {
                    Console.WriteLine(n);
                }
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Divided by zero!");
            }
            Console.WriteLine("----");

            var parallelQuery = from number in numbers.AsParallel()
                                select 100 / number;
            try
            {
                parallelQuery.ForAll(Console.WriteLine);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Divided by zero!");
            }
            catch(AggregateException e)
            {
                e.Flatten().Handle(ex => {
                    if(ex is DivideByZeroException)
                    {
                        Console.WriteLine("Divided by zero by AggregateException!");
                        return true;
                    }
                    return false;
                });
            }
        }
    }
}
