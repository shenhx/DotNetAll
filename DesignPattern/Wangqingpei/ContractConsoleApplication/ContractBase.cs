using System;

namespace ContractConsoleApplication
{
    [Serializable]
    public abstract class ContractBase
    {
        public ContractBase FirstValidator()
        {
            if (this.ImplelementFirstValidator())
            {
                return this;
            }

            throw new ContractValidateorException("前置条件检查器检查失败",this);

        }

        public ContractBase AfterValidator()
        {
            if (this.ImplelementAfterValidator())
            {
                return this;
            }

            throw new ContractValidateorException("后置条件检查器检查失败", this);
        }

        protected virtual bool ImplelementAfterValidator()
        {
            return true;

        }

        protected virtual bool ImplelementFirstValidator()
        {
            return true;
        }
    }
}