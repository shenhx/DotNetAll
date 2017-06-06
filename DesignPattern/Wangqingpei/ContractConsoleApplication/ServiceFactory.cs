using System;

namespace ContractConsoleApplication
{
    public class ServiceFactory
    {
        public static T Create<T>()
        {
            return default(T);
        }
    }
}