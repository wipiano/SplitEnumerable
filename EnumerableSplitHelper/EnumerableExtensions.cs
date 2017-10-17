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

            return that.SplitInner(size);
        }

        private static IEnumerable<IEnumerable<T>> SplitInner<T>(this IEnumerable<T> that, int size)
        {
            using (var enumerator = that.GetEnumerator())
            {
                bool hasNext = enumerator.MoveNext();
                
                while (hasNext)
                {
                    int count = 0;
                    var block = new T[size];

                    while (count < size)
                    {
                        block[count++] = enumerator.Current;
                        if (!enumerator.MoveNext())
                        {
                            hasNext = false;
                            break;
                        }
                    }
                    
                    // 最後の block の場合は resize して返す
                    if (hasNext)
                    {
                        yield return block;
                    }
                    else
                    {
                        Array.Resize(ref block, count);
                        yield return block;
                    }
                }
            }
        }
    }
}