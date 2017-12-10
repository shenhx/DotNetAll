using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplicationDemo
{
    public class Worker
    {
        /// <summary>
        /// Eratosthenes帅选质数
        /// 算法描述：
        /// 首先需要得到在某个数字范围内的所有整数的列表，然后剔除所有小于或等于最大数平方根的素数倍数，剩下的就是素数。
        /// </summary>
        /// <param name="fromNumber"></param>
        /// <param name="toNumber"></param>
        /// <returns></returns>
        public static int[] FindPrimes(int fromNumber, int toNumber)
        {
            List<int> integerList = new List<int>();
            for (int i = fromNumber; i <= toNumber ; i++)
            {
                
            }
            return integerList.ToArray();
        }
    }
}
