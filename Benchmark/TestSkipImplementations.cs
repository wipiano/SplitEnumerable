using System;
using System.Collections.Generic;

namespace Benchmark
{
    internal static class TestSkipImplementations
    {
        internal static IEnumerable<IEnumerable<T>> ArrayBufferSplit<T>(this IEnumerable<T> source, int count)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var buffer = new T[count];
                    
                    var i = 0;
                    while (i < count)
                    {
                        buffer[i] = enumerator.Current;
                        if(!enumerator.MoveNext()) break;
                        i++;
                    }

                    if (i < count - 1)
                    {
                        var newSize = i;
                        var newArray = new T[newSize];
                        Array.Copy(buffer, 0, newArray, 0, newSize);
                        buffer = newArray;
                    }

                    yield return buffer;
                }
            }
        }
        
        internal static IEnumerable<IEnumerable<T>> RxTake<T>(this IEnumerable<T> source, int count, int skip)
        {
            var buffers = new Queue<IList<T>>(Math.Max(1, count - skip) + 1);

            var i = 0;
            foreach (var item in source)
            {
                if (i % skip == 0)
                {
                    buffers.Enqueue(new List<T>(count));
                }

                foreach (var buffer in buffers)
                {
                    buffer.Add(item);
                }

                if (buffers.Count > 0 && buffers.Peek().Count == count)
                {
                    yield return buffers.Dequeue();
                }

                i++;
            }

            while (buffers.Count > 0)
            {
                yield return buffers.Dequeue();
            }
        }

        internal static IEnumerable<IEnumerable<T>> RxSimpleTake<T>(this IEnumerable<T> source, int count, int skip)
        {
            var buffer = new List<T>(count);

            foreach (var item in source)
            {
                buffer.Add(item);
                if (buffer.Count == count)
                {
                    yield return buffer;
                    buffer = new List<T>(count);
                }
            }

            if (buffer.Count > 0)
            {
                yield return buffer;
            }
        }

    }
}