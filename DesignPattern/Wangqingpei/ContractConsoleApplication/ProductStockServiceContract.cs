using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractConsoleApplication
{
    public class ProductStockServiceContract
    {
        public ContractBase Contract { get; set; }

        public SearchProductStockRequestContract RequestContract
        {
            get { return Contract as SearchProductStockRequestContract; }
        }

        public SearchProductStockResponseContract ResponseContract
        {
            get { return Contract as SearchProductStockResponseContract; }
        }
    }
}
