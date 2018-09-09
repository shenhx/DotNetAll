using EventBusDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo.Demos.register
{
    public class UserGeneratorEvent : IEvent
    {
        public Guid UserId { get; set; }
    }
}
