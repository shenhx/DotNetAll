using EventBusDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo.Demos.Fishing
{
    public class FishManEvent : IEvent
    {
        public string Name { get; set; }
    }
}
