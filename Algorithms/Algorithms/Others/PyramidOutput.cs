using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Others
{
    public class Pyramid
    {
        /// <summary>
        /// 金字塔层数
        /// </summary>
        /// <param name="i"></param>
        public void Output(int i)
        {
            Console.WriteLine();
            for (int j = 1; j <= i; j++)
            {
                StringBuilder sb = new StringBuilder();
                //输出i-j个空格，然后在输出j
                for (int l = 0; l < i - j; l++)
                {
                    sb.Append(" ");
                }
                for (int K = 0; K < j; K++)
                {
                    if(K != 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(j);
                }
                Console.Write(sb.ToString());
                Console.WriteLine();
            }
        }

    }
}
