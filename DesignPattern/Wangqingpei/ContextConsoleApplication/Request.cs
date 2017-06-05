using System;

namespace ContextConsoleApplication
{
    public class Request
    {
        public Guid LogTrackId { get; internal set; }
        public Guid TransactionId { get; internal set; }
    }
}