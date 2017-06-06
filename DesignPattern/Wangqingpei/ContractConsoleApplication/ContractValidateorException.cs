using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractConsoleApplication
{
    public class ContractValidateorException : Exception
    {
        public ContractValidateorException(string msg,ContractBase contract): base(msg)
        {
            this.CurrentContract = contract;
        }

        public ContractBase CurrentContract { get; private set; }
    }
}
