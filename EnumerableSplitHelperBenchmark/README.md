# Benchmark

``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 8.1 (6.3.9600)
Processor=Intel Core i5-5200U CPU 2.20GHz (Broadwell), ProcessorCount=4
Frequency=2143475 Hz, Resolution=466.5321 ns, Timer=TSC
  [Host]   : .NET Framework 4.7 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2114.0 DEBUG  [AttachedDebugger]
  ShortRun : .NET Framework 4.7 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2114.0

Job=ShortRun  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
 | Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
 |------- |----------:|----------:|----------:|-------:|----------:|
 |   Linq | 130.96 us | 301.91 us | 17.058 us | 1.2207 |   1.95 KB |
 |  Split |  37.32 us |  58.65 us |  3.314 us | 2.7466 |   4.29 KB |