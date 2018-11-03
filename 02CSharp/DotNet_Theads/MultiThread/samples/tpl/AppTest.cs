using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace samples.tpl
{
    public class AppTest
    {

        async static Task ProcessAsynchronously()
        {
            var cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                if (Console.ReadKey().KeyChar == 'c')
                {
                    cts.Cancel();
                }
            });
            var inputBlock = new BufferBlock<int>(new DataflowBlockOptions
            {
                BoundedCapacity = 5,
                CancellationToken = cts.Token
            });

            var filter1Block = new TransformBlock<int, decimal>(n =>
            {
                decimal result = Convert.ToDecimal(n * 0.97);
                Console.WriteLine("{0} {1} {2}", result, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                return result;
            }, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 4,
                CancellationToken = cts.Token
            });

            var filter2Block = new TransformBlock<decimal, string>(n =>
            {
                string result = string.Format("--{0}--", n);
                Console.WriteLine("{0} {1} {2}", result, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                return result;
            }, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 4,
                CancellationToken = cts.Token
            });

            var outputBlock = new ActionBlock<string>(a =>
            {
                Console.WriteLine("{0} {1} {2}", a, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            }, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 4,
                CancellationToken = cts.Token
            });

            inputBlock.LinkTo(filter1Block, new DataflowLinkOptions { PropagateCompletion = true });
            filter1Block.LinkTo(filter2Block, new DataflowLinkOptions { PropagateCompletion = true });
            filter2Block.LinkTo(outputBlock, new DataflowLinkOptions { PropagateCompletion = true });


            try
            {
                Parallel.For(0, 20, new ParallelOptions { MaxDegreeOfParallelism = 4, CancellationToken = cts.Token },
                    i =>
                    {
                        Console.WriteLine("add {0} to source data on threadid {1}", i, Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    });
                inputBlock.Complete();
                await outputBlock.Completion;
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("取消");
            }
        }

        public static void Run()
        {
            var t = ProcessAsynchronously();
            t.GetAwaiter().GetResult();
        }

    }
}
