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
