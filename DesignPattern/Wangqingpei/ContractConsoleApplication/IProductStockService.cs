using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractConsoleApplication
{
    public interface IProductStockService
    {
        /// <summary>
        /// 查询商品库存
        /// </summary>
        /// <param name="productStock"></param>
        /// <returns></returns>
        ProductStockServiceContract SearchProductStockNumberByCode(ProductStockServiceContract productStockContract);
    }
}
