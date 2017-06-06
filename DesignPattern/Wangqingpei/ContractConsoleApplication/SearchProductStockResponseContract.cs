using System;

namespace ContractConsoleApplication
{
    [Serializable]
    public class SearchProductStockResponseContract : ContractBase
    {
        public string ProductCode { get; set; }
        public int StockNumber { get; set; }
        [NonSerialized]
        public SearchProductStockRequestContract Request;

        protected override bool ImplelementAfterValidator()
        {
            var validator = ProductCode.BeginValidator().IsNullOrEmpty().StartWith("P").EndValidator();
            return ProductStockLocalMember.LocalStockMember.ContainsKey(ProductCode)
                && validator.IsTrue();
        }


    }
}