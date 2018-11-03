using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace samples.lazy
{
    public class AppTest
    {
public static void Run()
        {
            var t = ProcessAsynchronously();
            t.GetAwaiter().GetResult();
        }

        static async Task ProcessAsynchronously()
        {
            var unsafeState = new UnsafeState();
            Task[] tasks = new Task[4];
            int length = 4;
            for (int i = 0; i < length; i++)
            {
                tasks[i] = Task.Run(() => Worker(unsafeState));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("-------------");
            var firstState = new DoubleCheckedLocking();
            for (int i = 0; i < length; i++)
            {
                tasks[i] = Task.Run(() => Worker(firstState));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("-------------");
            var secondState = new BCLDoubleChecked();
            for (int i = 0; i < length; i++)
            {
                tasks[i] = Task.Run(() => Worker(secondState));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("-------------");
            var thirdState = new Lazy<ValueToAccess>(Compute);
            for (int i = 0; i < length; i++)
            {
                tasks[i] = Task.Run(() => Worker(thirdState));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("-------------");
            var fourState = new BCLThreadSafeFactory();
            for (int i = 0; i < length; i++)
            {
                tasks[i] = Task.Run(() => Worker(fourState));
            }
            await Task.WhenAll(tasks);
        }

        private static void Worker(Lazy<ValueToAccess> state)
        {
            Console.WriteLine("{0} {1} {2}", state.Value.Text, Thread.CurrentThread.ManagedThreadId,Thread.CurrentThread.IsThreadPoolThread);
        }

        private static ValueToAccess Compute()
        {
            Console.WriteLine("{0} {1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            return new ValueToAccess(string.Format("{0} {1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread));
        }

        private static void Worker(IHasValue state)
        {
            Console.WriteLine("{0} {1} {2}", state.Value.Text, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }

        class BCLThreadSafeFactory : IHasValue
        {
            public BCLThreadSafeFactory()
            {
            }

            private ValueToAccess _value;

            public ValueToAccess Value
            {
                get
                {
                    return LazyInitializer.EnsureInitialized(ref _value, Compute);
                }
            }
        }

        class BCLDoubleChecked : IHasValue
        {
            public BCLDoubleChecked()
            {
            }
            private object _syncRoot = new object();
            private ValueToAccess _value;
            private bool _initialized = false;

            public ValueToAccess Value
            {
                get
                {
                    return LazyInitializer.EnsureInitialized(ref _value, ref _initialized, ref _syncRoot, Compute);
                }
            }
            
        }

        class DoubleCheckedLocking : IHasValue
        {
            public DoubleCheckedLocking()
            {
            }
            private object _syncRoot = new object();
            private volatile ValueToAccess _value;

            public ValueToAccess Value
            {
                get
                {
                    if (_value == null)
                    {
                        lock (_syncRoot)
                        {
                            if (_value == null)
                            {
                                _value = Compute();
                            }
                        }
                    }
                    return _value;
                }
            }
        }

        class UnsafeState : IHasValue
        {
            private ValueToAccess _value;

            public UnsafeState()
            {
            }

            public ValueToAccess Value
            {
                get
                {
                    if (_value == null)
                    {
                        _value = Compute();
                    }
                    return _value;
                }
            }
        }
    }

    

    internal interface IHasValue
    {
        ValueToAccess Value { get; }
    }

    public class ValueToAccess
    {
        private readonly string _text;

        public ValueToAccess(string text)
        {
            _text = text;
        }

        public string Text {
            get { return _text; }
        }
    }
}
