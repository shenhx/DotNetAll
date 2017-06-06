using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductStockService productStockService = ServiceFactory.Create<IProductStockService>();
            var request = new ProductStockServiceContract()
            {
                Contract = new SearchProductStockRequestContract()
                {
                    ProductCode = "001"
                }
            };
            request.Contract.FirstValidator();
            var response = productStockService.SearchProductStockNumberByCode(request);
        }
    }
}
