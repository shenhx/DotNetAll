using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Ranking
{
    public class HanNuoRanking
    {
        /*
         * 思路分析：从左到右 A  B  C 柱 大盘子在下, 小盘子在上, 借助B柱将所有盘子从A柱移动到C柱, 期间只有一个原则: 大盘子只能在小盘子的下面.
         */

        /// <summary>
        /// 汉诺牌数量
        /// </summary>
        public int DiskQuantity { get; set; }

        /// <summary>
        /// 移动汉诺牌
        /// </summary>
        /// <param name="DiskQuantity"></param>
        /// <param name="PositionA"></param>
        /// <param name="PositionB"></param>
        /// <param name="PositionC"></param>
        public void MoveDisk(int DiskQuantity, string PositionA, string PositionB, string PositionC)
        {
            if (DiskQuantity <= 0)
            {
                Console.WriteLine("参数:{0}非法！",DiskQuantity);
                return;
            }
            else if (DiskQuantity == 1)
            {
                Console.WriteLine("Move disk from position {0} to {1}.", PositionA, PositionC);
                return;
            }
            else
            {
                Console.WriteLine("第一步");
                MoveDisk(DiskQuantity - 1, PositionA, PositionC, PositionB);
                Console.WriteLine("第二步");
                MoveDisk(1, PositionA, PositionB, PositionC);
                Console.WriteLine("第三步");
                MoveDisk(DiskQuantity - 1, PositionB, PositionA, PositionC);
            }
        }
    }
}
