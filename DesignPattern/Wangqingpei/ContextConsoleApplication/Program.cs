using System;
using System.Collections.Generic;
using System.Linq;

namespace ContextConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProxy proxy = new ServiceProxy();
            using(SoaServiceCallContext soaContext = new SoaServiceCallContext(true, true))
            {
                soaContext.BeginRecordLogTrackEvent += SoaContext_BeginRecordLogTrackEvent;
                soaContext.TransactionEndEvent += SoaContext_TransactionEndEvent;
                proxy.SetTicketPrice("123456", 12.4);
                proxy.UpdateTIcketCache("123456", 32.4);
            }
        }

        private static void SoaContext_TransactionEndEvent(TransactionActionInfo arg)
        {
            Console.WriteLine(arg);
        }

        private static void SoaContext_BeginRecordLogTrackEvent(LogTrackLocation arg)
        {
            throw new NotImplementedException();
        }
    }
}
