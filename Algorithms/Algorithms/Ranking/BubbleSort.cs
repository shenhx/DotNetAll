using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Ranking
{
    public class BubbleSort
    {
        /*
         * 冒泡排序：
         */

        public void Sort(List<int> list)
        {
            int i, j, temp;
            bool done = false;
            j = 1;
            int recCount = list.Count();
            while ((j < recCount) && (!done))
            {
                //避免已经排序好的数组多次操作，提高冒泡排序效率
                done = true;
                for (i = 0; i < recCount - j; i++)
                {
                    if (list[i] > list[i + 1])
                    {
                        done = false;
                        temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                    }
                }
                j++;
            }
        }
    }
}
