using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Others
{
    public class PrimeNumber
    {
        /*
         * 质数概念：
         * 质数也成为素数，质数就是这个数除了1和他本身两个因数以外，没有其他因数的数，叫做质数，和他相反的是合数，
         * 就是除了1和他本身两个因数以外，还友其他因数的数叫做合数。
         */

        public void CalPrimeNumber(long x)
        {
            long sum = 1;
            byte row = 1;
            Console.Write("\n");
            for (long a = 3; a < x + 1; a++)
            {
                bool flag = true;
                for (long b = 2; b < (a / 2) + 1; b++)
                {
                    if (a % b != 0) continue;
                    flag = false;
                    break;
                }
                if (flag)
                {
                    if (row == 10) 
                    { 
                        Console.WriteLine(); 
                        row = 0; 
                    }
                    if (sum == 1) 
                        Console.Write("{0,7}", 2);
                    Console.Write("{0,7}", a);
                    sum++; 
                    row++;
                }
            }
            Console.WriteLine("\n\n{0} 以内共有 {1} 个质数\n", x, sum);
        }
    }
}
