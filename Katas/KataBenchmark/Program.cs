using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace KataBenchmark
{
    public class SumFractionsBenchmark
    {
        int[,] data = new int[,] { { 1, 2 }, { 2, 9 }, { 3, 18 }, { 4, 24 }, { 6, 48 } };

        [Benchmark()]
        public string My() => SumFractions.SumFracts(data);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SumFractionsBenchmark>();
            System.Console.WriteLine(summary);
        }
    }
}
