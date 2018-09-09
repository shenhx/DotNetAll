using EventBusDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo.Demos.register
{
    public class RegisterProgram : IDemo
    {
        public void PrintTest()
        {
            var sendEmailHandler = new UserAddedEventHandlerSendEmail();
            var sendMessageHandler = new UserAddedEventHandlerSendMessage();
            var sendRedbagsHandler = new UserAddedEventHandlerSendRedbags();
            EventBus.Instance.Subscribe(sendEmailHandler);
            EventBus.Instance.Subscribe(sendMessageHandler);
            EventBus.Instance.Subscribe<OrderGeneratorEvent>(sendRedbagsHandler);
            var userGrneratorEvent = new UserGeneratorEvent() { UserId = Guid.NewGuid()};
            System.Console.WriteLine("{0}注册成功", userGrneratorEvent.UserId);
            // 发布
            EventBus.Instance.Publish(userGrneratorEvent, CallBack);
            var orderGeneratorEvent = new OrderGeneratorEvent { OrderId = Guid.NewGuid() };
            System.Console.WriteLine("{0}下单成功", orderGeneratorEvent.OrderId);
            EventBus.Instance.Publish(orderGeneratorEvent, CallBack);
            Console.ReadKey();
        }

        private static void CallBack(OrderGeneratorEvent orderGeneratorEvent, bool result, Exception ex)
        {
            System.Console.WriteLine("用户下单订阅事件执行成功");
        }

        public static void CallBack(UserGeneratorEvent userGenerator, bool result, Exception ex)
        {
            System.Console.WriteLine("用户注册订阅事件执行成功");
        }
    }
}
