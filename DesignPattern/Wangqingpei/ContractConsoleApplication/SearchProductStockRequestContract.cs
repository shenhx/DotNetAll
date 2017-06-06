namespace ContractConsoleApplication
{
    public class SearchProductStockRequestContract : ContractBase
    {
        public string ProductCode { get; set; }

        protected override bool ImplelementFirstValidator()
        {
            var validator = this.ProductCode.BeginValidator().IsNullOrEmpty().StartWith("P").EndValidator();
            return validator.IsTrue();
        }
    }
}