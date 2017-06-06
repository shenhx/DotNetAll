using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractConsoleApplication
{
    public class ProductStockLocalMember : Dictionary<string,int>
    {
        private static ProductStockLocalMember stockLocalMember = new ProductStockLocalMember();

        static ProductStockLocalMember()
        {
            stockLocalMember.Add("P001",10);
            stockLocalMember.Add("P002",20);
            stockLocalMember.Add("P003",30);
            stockLocalMember.Add("P004",40);
        }

        public static ProductStockLocalMember LocalStockMember
        {
            get { return stockLocalMember; }
        }
    }
}
