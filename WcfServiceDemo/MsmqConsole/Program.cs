using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.Text;
using WcfServiceMsmq;

namespace MsmqConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var queueName = ConfigurationManager.AppSettings["queueName"];
            if (!MessageQueue.Exists(queueName))
            {
                MessageQueue.Create(queueName, true);
            }

            ServiceHost host = new ServiceHost(typeof(Order));
            host.Open();

            Console.WriteLine("WCF 服务已经开启");
            Console.ReadKey();
        }
    }
}
