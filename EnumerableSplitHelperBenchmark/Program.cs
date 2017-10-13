using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumerableSplitHelperBenchmark {
    class Program {
        static void Main(string[] args) {

            // Switcherは複数ベンチマークを作りたい場合ベンリ。
            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(SplitBenchmark)
            });

            // 今回は一個だけなのでSwitcherは不要ですが。
            args = new string[] { "0" };
            switcher.Run(args); // 走らせる
        }
    }

    public class BenchmarkConfig : ManualConfig {
        public BenchmarkConfig() {
            Add(MarkdownExporter.GitHub); // ベンチマーク結果を書く時に出力させとくとベンリ
            Add(MemoryDiagnoser.Default);

            // ShortRunを使うとサクッと終わらせられる、デフォルトだと本気で長いので短めにしとく。
            // ShortRunは LaunchCount=1  TargetCount=3 WarmupCount = 3 のショートカット
            Add(Job.ShortRun);
        }
    }

    [Config(typeof(BenchmarkConfig))]
    public class SplitBenchmark {

        private static readonly int iterationCount = 100000;

        private IEnumerable<MockObject> _enum;

        [GlobalSetup]
        public void Setup() {
            var list = new List<MockObject>();
            for(int i = 0; i < iterationCount; i ++) {
                list.Add(new MockObject { Value = i });
            }
            _enum = list;
        }

        [Benchmark]
        public MockObject Linq() {
            var segment = _enum.Take(100);
            var cnt = 1;
            while(segment.Any()) {
                foreach(var item in segment) {
                    if(item.Value == iterationCount - 1) {
                        return item;
                    }
                }
                segment = _enum.Skip(cnt++ * 100).Take(100);
            }
            return null;
        }

        [Benchmark]
        public MockObject Split() {
            foreach(var segment in _enum.Split(100)) {
                foreach(var item in segment) {
                    if(item.Value == iterationCount - 1) {
                        return item;
                    }
                }
            }
            return null;
        }

    }

    public class MockObject {
        public int Value { get; set; }
    }
}
