using EventBusDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo.Demos.register
{
    public class OrderGeneratorEvent : IEvent
    {
        public Guid OrderId { get; set; }
    }
}
