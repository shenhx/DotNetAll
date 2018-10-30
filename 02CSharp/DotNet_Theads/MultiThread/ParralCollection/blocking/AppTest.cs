using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParralCollection.blocking
{
    public class AppTest
    {
        static async Task RunProgram(IProducerConsumerCollection<CustomTask> collection = null)
        {
            var taskCollection = new BlockingCollection<CustomTask>();
            if (null != collection)
            {
                taskCollection = new BlockingCollection<CustomTask>(collection);
            }
            var taskSource = Task.Run(() => TaskProducer(taskCollection));
            Task[] processors = new Task[4];
            int length = 4;
            for (int i = 1; i < length; i++)
            {
                string processorId = "processor" + i;
                processors[i - 1] = Task.Run(() => TaskProcessor(taskCollection, processorId));
            }
            await taskSource;
            await Task.WhenAll(processors);
        }

        private static async void TaskProcessor(BlockingCollection<CustomTask> taskCollection, string name)
        {
            await GetRandomDelay();
            foreach (var item in taskCollection.GetConsumingEnumerable())
            {
                Console.WriteLine("{0} {1}", item.Id, name);
                await GetRandomDelay();
            }
        }

        private static Task GetRandomDelay()
        {
            int delay = new Random(DateTime.Now.Millisecond).Next(1, 500);
            return Task.Delay(delay);
        }

        private static async void TaskProducer(BlockingCollection<CustomTask> taskCollection)
        {
            int length = 20;
            for (int i = 0; i < length; i++)
            {
                await Task.Delay(20);
                var workItem = new CustomTask { Id = i.ToString() };
                taskCollection.Add(workItem);
                Console.WriteLine(workItem.Id);
            }
            taskCollection.CompleteAdding();
        }
        public static void Run()
        {
            Console.WriteLine();
            Task t = RunProgram();
            t.Wait();

            Console.WriteLine();
            t = RunProgram(new ConcurrentStack<CustomTask>());
            t.Wait();
        }

    }

    internal class CustomTask
    {
        public string Id { get; set; }
    }
}
