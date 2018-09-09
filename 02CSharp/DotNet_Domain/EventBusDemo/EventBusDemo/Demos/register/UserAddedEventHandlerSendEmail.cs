using EventBusDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo.Demos.register
{
    /// <summary>
    /// send email
    /// </summary>
    public class UserAddedEventHandlerSendEmail : IEventHandler<UserGeneratorEvent>
    {

        public void Handle(UserGeneratorEvent tEvent)
        {
            System.Console.WriteLine(string.Format("{0}的邮件已发送", tEvent.UserId));
        }
    }

    /// <summary>
    /// send message.
    /// </summary>
    public class UserAddedEventHandlerSendMessage : IEventHandler<UserGeneratorEvent>
    {

        public void Handle(UserGeneratorEvent tEvent)
        {
            System.Console.WriteLine(string.Format("{0}的短信已发送", tEvent.UserId));
        }
    }

    /// <summary>
    /// red bags.
    /// </summary>
    public class UserAddedEventHandlerSendRedbags : IEventHandler<UserGeneratorEvent>, IEventHandler<OrderGeneratorEvent>
    {
        public void Handle(OrderGeneratorEvent tEvent)
        {
            System.Console.WriteLine(string.Format("{0}的下单红包已发送", tEvent.OrderId));
        }

        public void Handle(UserGeneratorEvent tEvent)
        {
            System.Console.WriteLine(string.Format("{0}的注册红包已发送", tEvent.UserId));
        }
    }
}
