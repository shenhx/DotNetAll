using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread.readerwriterlock
{
    public class TestApp
    {
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static Dictionary<int, int> _dicts = new Dictionary<int, int>();

        static void Read()
        {
            Console.WriteLine("开始读取字典数据");
            while (true)
            {
                try
                {
                    _rw.EnterReadLock();
                    foreach (var key in _dicts.Keys)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }
                }
                finally
                {
                    _rw.ExitReadLock();
                }
            }
        }

        static void Write(string threadName)
        {
            Console.WriteLine("开始写入字典数据");
            while (true)
            {
                try
                {
                    int newKey = new Random().Next(250);
                    _rw.EnterUpgradeableReadLock();
                    if (!_dicts.ContainsKey(newKey))
                    {
                        try
                        {
                            _rw.EnterWriteLock();
                            _dicts[newKey] = 1;
                            Console.WriteLine("new key {0} has been added by {1}",newKey,threadName);
                        }
                        finally
                        {
                            _rw.ExitWriteLock();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }
                finally
                {
                    _rw.ExitUpgradeableReadLock();
                }
            }
        }

        public static void Run()
        {
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(() => Write("t1")) { IsBackground = true }.Start();
            new Thread(() => Write("t2")) { IsBackground = true }.Start();
            Thread.Sleep(TimeSpan.FromSeconds(30));
        }

    }
}
