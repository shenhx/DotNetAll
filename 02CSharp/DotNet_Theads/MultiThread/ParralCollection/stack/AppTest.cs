﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParralCollection.stack
{
    public class AppTest
    {

        static async Task RunProgram()
        {
            var taskStack = new ConcurrentStack<CustomTask>();
            var cts = new CancellationTokenSource();
            var taskSource = Task.Run(() => TaskProducer(taskStack));

            Task[] processors = new Task[4];
            int length = 4;
            for (int i = 0; i < length; i++)
            {
                string processorId = i.ToString();
                processors[i - 1] = Task.Run(() => TaskProcessor(taskStack, "Processor" + processorId, cts.Token));
            }

            await taskSource;
            cts.CancelAfter(TimeSpan.FromSeconds(2));
            await Task.WhenAll(processors);

        }

        public static void Run()
        {
            Task t = RunProgram();
            t.Wait();
        }

        private static async void TaskProcessor(ConcurrentStack<CustomTask> taskStack, string name, CancellationToken token)
        {
            CustomTask workItem;
            bool dequeueSuccesful = false;

            await GetRandomDelay();
            do
            {
                dequeueSuccesful = taskStack.TryPop(out workItem);
                if (dequeueSuccesful)
                {
                    Console.WriteLine("{0} {1}", workItem.Id, name);
                }
            } while (!token.IsCancellationRequested);
        }

        private static Task GetRandomDelay()
        {
            int delay = new Random(DateTime.Now.Millisecond).Next(1, 500);
            return Task.Delay(delay);
        }

        private static async void TaskProducer(ConcurrentStack<CustomTask> taskQueue)
        {
            int length = 20;
            for (int i = 0; i < length; i++)
            {
                await Task.Delay(50);
                var workItem = new CustomTask { Id = i };
                taskQueue.Push(workItem);
                Console.WriteLine(workItem.Id);
            }
        }
    }

    internal class CustomTask
    {
        public int Id { get; internal set; }
    }
}
