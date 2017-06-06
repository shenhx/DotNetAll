using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractConsoleApplication
{
public    class ProductStock
    {
        public int GetStockByProductCode(string pCode)
        {
            lock (ProductStockLocalMember.LocalStockMember)
            {
                if(ProductStockLocalMember.LocalStockMember.ContainsKey(pCode))
                {
                    return ProductStockLocalMember.LocalStockMember[pCode];
                }
                return 0;
            }
        }
    }
}
