using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContextConsoleApplication
{
    public class ServiceProxy : ServiceContextManager
    {
        public void SetTicketPrice(string ticketId, double price)
        {
            base.ApperceiveContext(new ServiceProxyRequest() { TicketId = ticketId, Price = price });
        }
        
        public void UpdateTIcketCache(string ticketId, double price)
        {
            base.ApperceiveContext(new ServiceProxyRequest() { TicketId = ticketId, Price = price });
        }
    }
}
