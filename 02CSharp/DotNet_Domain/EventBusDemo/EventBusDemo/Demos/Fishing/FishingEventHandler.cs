using EventBusDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo.Demos.Fishing
{
    public class FishingEventHandler : IEventHandler<FishManEvent>
    {
        public void Handle(FishManEvent evt)
        {
            Console.WriteLine("{0}钓到一条鱼！",evt.Name);
        }
    }
}
