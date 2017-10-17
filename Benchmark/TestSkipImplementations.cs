using System;
using System.Collections.Generic;

namespace Benchmark
{
    internal static class TestSkipImplementations
    {
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