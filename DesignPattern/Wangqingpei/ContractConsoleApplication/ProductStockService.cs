using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractConsoleApplication
{
    public class ProductStockService : IProductStockService
    {
        public ProductStockServiceContract SearchProductStockNumberByCode(ProductStockServiceContract requestContract)
        {
            ProductStock stock = new ProductStock();
            var stockNumber = stock.GetStockByProductCode(requestContract.RequestContract.ProductCode);
            var response = new SearchProductStockResponseContract()
            {
                ProductCode = requestContract.RequestContract.ProductCode,
                Request = requestContract.RequestContract,
                StockNumber = stockNumber
            };
            var result = new ProductStockServiceContract() { Contract = response };
            result.Contract.AfterValidator();
            return result;
        }
    }
}
