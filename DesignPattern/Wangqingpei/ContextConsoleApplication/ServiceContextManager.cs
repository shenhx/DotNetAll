using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContextConsoleApplication
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceContextManager
    {
        public SoaServiceCallContext CurrentContext
        {
            get { return SoaServiceCallContext._context; }
        }

        protected void ApperceiveContext(Request request)
        {
            if(SoaServiceCallContext._context != null)
            {
                request.LogTrackId = CurrentContext.LogTrack ? Guid.NewGuid() : new Guid();

                request.TransactionId = CurrentContext.Transaction ? Guid.NewGuid() : new Guid();
            }
        }
    }
}
