using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Linq;

namespace Benchmark
{

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                args = new string[] {"0"};
            }

            // Switcherは複数ベンチマークを作りたい場合ベンリ。
            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(SplitBenchmark),
            });

            switcher.Run(args); // 走らせる
        }
    }

    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            Add(MarkdownExporter.GitHub); // ベンチマーク結果を書く時に出力させとくとベンリ
            Add(MemoryDiagnoser.Default);

            // ShortRunを使うとサクッと終わらせられる、デフォルトだと本気で長いので短めにしとく。
            // ShortRunは LaunchCount=1  TargetCount=3 WarmupCount = 3 のショートカット
            // Add(Job.ShortRun);
        }
    }

    [Config(typeof(BenchmarkConfig))]
    public class SplitBenchmark
    {
        private const int IterationCount = 10000;

        private IEnumerable<MockObject> _enum;

        [GlobalSetup]
        public void Setup()
        {
            _enum = Enumerable.Range(0, IterationCount)
                .Select(v => new MockObject() {Value = v})
                .ToList();
        }

        [Benchmark]
        public List<MockObject> Linq()
        {
            var list = new List<MockObject>();
            var segment = _enum.Take(100);
            var cnt = 1;
            while (segment.Any())
            {
                list.AddRange(segment);
                segment = _enum.Skip(cnt++ * 100).Take(100);
            }
            return list;
        }

        [Benchmark]
        public List<MockObject> Split()
        {
            var list = new List<MockObject>();
            foreach (var segment in _enum.Split(100))
            {
                list.AddRange(segment);
            }
            return list;
        }
    }

    public class MockObject
    {
        public int Value { get; set; }
    }

}