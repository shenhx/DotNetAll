using EventBusDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusDemo.Demos.Fishing
{
    public class FishingProgram : IDemo
    {
        public void PrintTest()
        {
            Console.WriteLine("开始下钓");
            var rand = new Random();
            
            // 等待鱼上钓的通知
            FishingEventHandler fishingEventHandler = new FishingEventHandler();
            EventBus.Instance.Subscribe(fishingEventHandler);

            while (rand.Next(1,4) % 2 == 0)
            {

                Console.WriteLine("铃铛：叮叮叮，鱼儿咬钩了");
                FishManEvent fishManEvent = new FishManEvent { Name = "shx"};
                // 发布消息
                EventBus.Instance.Publish(fishManEvent, Callback);
            }
            Console.ReadKey();
        }

        public static void Callback(FishManEvent fishManEvent, bool result, Exception ex)
        {
            Console.WriteLine("钓鱼完成");
        }
    }
}
