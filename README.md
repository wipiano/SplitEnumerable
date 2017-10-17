# SplitEnumerable
Split IEnumerable&lt;T&gt; safety

This `Split<T>` method won't cause multiple enumeration.

## install

```sh
PM> Install-Package EnumerableSplitHelper -Version 1.0.2
```

or 

```
> dotnet add package EnumerableSplitHelper --version 1.0.2
```

or 

```
> paket add EnumerableSplitHelper --version 1.0.2
```

## sample

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var splitted = EnumerateNumber().Take(77).Split(5);

            foreach (IEnumerable<int> chunk in splitted)
            {
                Console.WriteLine(string.Join(", ", chunk));
            }
        }

        private static IEnumerable<int> EnumerateNumber()
        {
            int n = 0;
            while (true)
            {
                yield return n++;
            }
        }
    }
}
```

## Benchmark

``` ini

BenchmarkDotNet=v0.10.9, OS=ubuntu 16.04
Processor=Intel Core i7-6700 CPU 3.40GHz (Skylake), ProcessorCount=8
.NET Core SDK=2.0.0
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
 |       Method |     Mean |     Error |    StdDev |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
 |------------- |---------:|----------:|----------:|--------:|--------:|--------:|----------:|
 | LinqSkipTake | 215.5 us | 5.2655 us | 5.6341 us | 41.5039 | 41.5039 | 41.5039 | 270.42 KB |
 |        Split | 163.4 us | 0.2437 us | 0.2161 us | 64.4531 | 32.2266 | 32.2266 | 280.03 KB |
