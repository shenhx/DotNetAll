using System;

namespace ContractConsoleApplication
{
    public class Validator
    {
        public Func<object, bool> Validat { get; set; }
        public ValidatorContrainer Container { get; internal set; }

        public bool IsTrue()
        {
            if (Validat != null)
            {
                return this.Validat(Container.ValidatorObject);
            }
            return true;

        }
    }
}