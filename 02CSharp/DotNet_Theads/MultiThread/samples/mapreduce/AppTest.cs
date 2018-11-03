using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samples.mapreduce
{
    public class AppTest
    {
        private static readonly char[] delimiters = Enumerable.Range(0, 256)
            .Select(i => (char)i).Where(c => !char.IsLetterOrDigit(c)).ToArray();

        private const string txtToParse = @"中国 文艺工作者 代表团 11月2日 乘机 抵达 平壤 顺安国际机场，开始对朝鲜进行访问。
            当天下午，朝方 官员 与 上千名朝鲜群众 在 机场停机坪 迎接代表团一行。访朝期间，中国 文艺工作者们 将为 朝鲜人民 献上精彩的文艺演出，
            并与 朝鲜艺术家们 开展联袂演出等友好交流活动。 这是 中国 文艺工作者 代表团在机场受到朝鲜民众热烈欢迎。图为佟丽娅在机场受到朝鲜民众热烈欢迎。 
            新华社记者程大雨摄";

        public static void Run()
        {
            var q = txtToParse.Split(delimiters)
                .AsParallel()
                .MapReduce(s => s.ToLower().ToCharArray(),
                c => c,
                g => new[] { new { Char = g.Key, Count = g.Count() } })
                .Where(c => char.IsLetterOrDigit(c.Char))
                .OrderByDescending(c => c.Count);

            foreach (var info in q)
            {
                Console.WriteLine("字符 {0} 出现在字符串中 {1} {2}", info.Char, info.Count, info.Count == 1 ? "time" : "times");
            }
            Console.WriteLine("------------");
            Console.ReadLine();
            Console.Clear();
            const string searchPattern = "中国";
            var q2 = txtToParse.Split(delimiters).AsParallel().Where(s => s.Contains(searchPattern))
                .MapReduce(s => new[] { s },
                s => s, g => new[] { new { Word = g.Key, Count = g.Count() } })
                .OrderByDescending(s => s.Count);
            Console.WriteLine("Search Pattern:{0}", searchPattern);
            foreach (var info in q2)
            {
                Console.WriteLine("字符 {0} 出现在字符串中 {1} {2}", info.Word, info.Count, info.Count == 1 ? "time" : "times");
            }

            Console.WriteLine("------------");
            Console.ReadLine();
            Console.Clear();
            int halfLengthWordIndex = txtToParse.IndexOf(' ', txtToParse.Length / 2);
            using (var sw = File.CreateText("1.txt"))
            {
                sw.Write(txtToParse.Substring(0, halfLengthWordIndex));
            }

            using (var sw = File.CreateText("2.txt"))
            {
                sw.Write(txtToParse.Substring(halfLengthWordIndex));
            }

            string[] paths = new[] { ".\\" };
            Console.WriteLine("------------");
            Console.ReadLine();
            Console.Clear();
            var q3 = paths.SelectMany(p => Directory.EnumerateFiles(p, "*.txt"))
                .AsParallel()
                .MapReduce(path => File.ReadLines(path).SelectMany(line => line.Trim(delimiters).Split(delimiters)),
                word => string.IsNullOrWhiteSpace(word) ? '\t' : word.ToLower()[0],
                g => new[] { new { FirstLetter = g.Key, Count = g.Count() } })
                .Where(s => char.IsLetterOrDigit(s.FirstLetter))
                .OrderByDescending(s => s.Count);

            Console.WriteLine("words from text files");
            foreach (var info in q3)
            {
                Console.WriteLine("字符 {0} 出现在字符串中 {1} {2}", info.FirstLetter, info.Count, info.Count == 1 ? "time" : "times");
            }
        }
    }

    static class PLINQExtensions
    {
        public static ParallelQuery<TResult> MapReduce<TSource, TMapped, TKey, TResult>(this ParallelQuery<TSource> sources,
            Func<TSource, IEnumerable<TMapped>> map,
            Func<TMapped, TKey> keySelector,
            Func<IGrouping<TKey, TMapped>,
                IEnumerable<TResult>> reduce)
        {
            return sources.SelectMany(map)
                .GroupBy(keySelector)
                .SelectMany(reduce);
        }
    }
}
