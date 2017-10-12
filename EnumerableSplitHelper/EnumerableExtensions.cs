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

            // はじめに size ぶんのバッファを確保
            T[] buffer = new T[size];

            bool hasNext = true;
            
            // バッファをつぎの要素で埋める関数
            // 何個埋めたか返す
            int FillNext(IEnumerator<T> enumerator)
            {
                int count = 0;
                while (count < size)
                {
                    if (enumerator.MoveNext())
                    {
                        buffer[count++] = enumerator.Current;
                    }
                    else
                    {
                        hasNext = false;
                        break;
                    }
                }

                return count;
            }

            using (var e = that.GetEnumerator())
            {
                while (hasNext)
                {
                    int length = FillNext(e);
                    var result = new T[length];
                    Array.Copy(buffer, result, length);
                    yield return result;
                }
            }
        }
    }
}