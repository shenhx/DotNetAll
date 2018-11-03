using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncIO.file
{
    public class AppTest
    {
        const int BUFFER_SIZE = 4096;

        async static Task ProcessAsynchronousIO()
        {
            using (var stream = new FileStream("t1.txt", FileMode.Create, FileAccess.ReadWrite,
                FileShare.None, BUFFER_SIZE))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(CreateFileContent());
                var writeTask = Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);
                await writeTask;
            }

            using (var stream = new FileStream("t2.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None, BUFFER_SIZE, FileOptions.Asynchronous))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(CreateFileContent());
                var writeTask = Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);
                await writeTask;
            }

            using (var stream = File.Create("t3.txt", BUFFER_SIZE, FileOptions.Asynchronous))
            {
                using (var sw = new StreamWriter(stream))
                {
                    await sw.WriteAsync(CreateFileContent());
                }
            }

            using (var sw = new StreamWriter("t4.txt", true))
            {
                await sw.WriteAsync(CreateFileContent());
            }

            Task<long>[] readTasks = new Task<long>[4];
            int length = 4;
            for (int i = 0; i < length; i++)
            {
                readTasks[i] = SumFileContent(string.Format("test{0}.txt", i + 1));
            }
            long[] sums = await Task.WhenAll(readTasks);
            Console.WriteLine(sums.Sum());

            Task[] deleteTasks = new Task[4];
            for (int i = 0; i < length; i++)
            {
                string fileName = string.Format("test{0}.txt", i + 1);
                deleteTasks[i] = SimulateAsynchronousDelete(fileName);
            }
            await Task.WhenAll(deleteTasks);


        }

        private static Task SimulateAsynchronousDelete(string fileName)
        {
            return Task.Run(() => File.Delete(fileName));
        }

        async static Task<long> SumFileContent(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None, BUFFER_SIZE, FileOptions.Asynchronous))
            {
                using (var sr = new StreamReader(stream))
                {
                    long sum = 0;
                    while (sr.Peek() > -1)
                    {
                        string line = await sr.ReadLineAsync();
                        sum += long.Parse(line);
                    }
                    return sum;
                }
            }
        }

        private static string CreateFileContent()
        {
            var sb = new StringBuilder();
            int length = 100000;
            for (int i = 0; i < length; i++)
            {
                sb.AppendFormat("{0}", new Random(i).Next(0, 99999));
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static void Run()
        {
            var t = ProcessAsynchronousIO();
            t.GetAwaiter().GetResult();
        }

    }
}
