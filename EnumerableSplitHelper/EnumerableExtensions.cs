using System.Collections.Generic;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// コレクションを size ごとに分割して列挙します
        /// </summary>
        /// <param name="that">分割するコレクション</param>
        /// <param name="size">分割後のサイズ</param>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> that, int size)
        {
            if (that == null)
            {
                throw new ArgumentNullException(nameof(that));
            }

            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            using (var enumerator = that.GetEnumerator())
            {
                // allocate buffer
                T[] buffer = new T[size];

                bool hasNext = enumerator.MoveNext();
                
                while (hasNext)
                {
                    int count = 0;
                    while (count < size)
                    {
                        buffer[count++] = enumerator.Current;
                        if (!enumerator.MoveNext())
                        {
                            hasNext = false;
                            break;
                        }
                    }
                    
                    var result = new T[count];
                    Array.Copy(buffer, result, count);
                    yield return result;
                }
            }
        }
    }
}