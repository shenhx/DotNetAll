using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace samples.blockingcollection
{
    public class AppTest
    {
        private const int CollectionsNumber = 4;
        private const int Count = 10;

        class PipelineWorker<TInput, TOutput>
        {
            Func<TInput, TOutput> _processor = null;
            Action<TInput> _outputProcessor = null;
            BlockingCollection<TInput>[] _input;
            CancellationToken _token;

            public BlockingCollection<TOutput>[] Output { get; private set; }
            public string Name { get; private set; }

            public PipelineWorker(BlockingCollection<TInput>[] input,
                Func<TInput, TOutput> processor,
                CancellationToken token,
                string name)
            {
                _input = input;
                Output = new BlockingCollection<TOutput>[_input.Length];
                int length = Output.Length;
                for (int i = 0; i < length; i++)
                {
                    Output[i] = null == input[i] ? null : new BlockingCollection<TOutput>(Count);
                }
                _processor = processor;
                _token = token;
                Name = name;
            }

            public PipelineWorker(BlockingCollection<TInput>[] input,
                Action<TInput> renderer,
                CancellationToken token,
                string name)
            {
                _input = input;
                _outputProcessor = renderer;
                _token = token;
                Name = name;
                Output = null;
            }

            public void Run()
            {
                Console.WriteLine("{0} 正在运行。。。", this.Name);
                while (!_input.All(bc => bc.IsCompleted) && !_token.IsCancellationRequested)
                {
                    TInput receivedItem;
                    int i = BlockingCollection<TInput>.TryTakeFromAny(_input, out receivedItem, 50, _token);
                    if (i > 0)
                    {
                        if (Output != null)
                        {
                            TOutput outputItem = _processor(receivedItem);
                            BlockingCollection<TOutput>.AddToAny(Output, outputItem);
                            Console.WriteLine("{0} {1} {2}", Name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
                            Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        }
                        else
                        {
                            _outputProcessor(receivedItem);
                        }
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(50));
                    }
                }
                if(Output != null)
                {
                    foreach (var bc in Output)
                    {
                        bc.CompleteAdding();
                    }
                }
            }


        }
        public static void Run()
        {
            var cts = new CancellationTokenSource();

            Task.Run(() => {
                if(Console.ReadKey().KeyChar == 'C')
                {
                    cts.Cancel();
                }
            });
            var sourceArrays = new BlockingCollection<int>[CollectionsNumber];
            int length = sourceArrays.Length;
            for (int i = 0; i < length; i++)
            {
                sourceArrays[i] = new BlockingCollection<int>(Count);
                var filter1 = new PipelineWorker<int, decimal>(sourceArrays, (n) => Convert.ToDecimal(n * 0.97), cts.Token, "fileter1");

                var filter2 = new PipelineWorker<decimal, string>(filter1.Output, (s) => string.Format("--{0}---", s), cts.Token, "filter2");

                var filter3 = new PipelineWorker<string, string>(filter2.Output, (s) => Console.WriteLine("the final result is {0} on threadid {1}", s, Thread.CurrentThread.ManagedThreadId), cts.Token, "filter3");

                try
                {
                    Parallel.Invoke(() =>
                    {
                        Parallel.For(0, sourceArrays.Length * Count, (j, state) =>
                        {
                            if (cts.Token.IsCancellationRequested)
                            {
                                state.Stop();
                            }

                            int k = BlockingCollection<int>.TryAddToAny(sourceArrays, j);
                            if(k >= 0)
                            {
                                Console.WriteLine("add {0} to source data on threadid {1}",j , Thread.CurrentThread.ManagedThreadId);
                                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                            }
                        });
                        foreach (var arr in sourceArrays)
                        {
                            arr.CompleteAdding();
                        }
                    },
                    
                    () => filter1.Run(),
                    () => filter2.Run(),
                    () => filter3.Run()
                    );
                }
                catch (AggregateException ex)
                {
                    foreach (var e in ex.InnerExceptions)
                    {
                        Console.WriteLine(ex.Message + ex.StackTrace);
                    }
                }
                if (cts.Token.IsCancellationRequested)
                {
                    Console.WriteLine("取消");
                }
                else
                {
                    Console.WriteLine("完成");
                }
            }
        }
    }
}
