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
 | Method |       Mean |      Error |     StdDev |    Gen 0 | Allocated |
 |------- |-----------:|-----------:|-----------:|---------:|----------:|
 |   Linq | 850.628 ms | 674.602 ms | 38.1163 ms | 125.0000 | 203.51 KB |
 |  Split |   3.752 ms |   2.567 ms |  0.1450 ms | 273.4375 | 421.99 KB |