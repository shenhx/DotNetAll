using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContextConsoleApplication
{
    public class ServiceProxyRequest : Request
    {
        public string TicketId { get; set; }
        public double Price { get; set; }
    }
}
