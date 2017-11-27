using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class LinqExtensions
    {
        /// <summary>
        /// 返回序列中通过使用指定的非重复元素 <see cref="Func{TSource,TSource, TResult}"/> 对值进行比较。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="equality"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> equality)
        {
            return source.Distinct(new DelegateComparer<TSource>(equality));
        }
        /// <summary>
        /// 返回序列中通过使用指定的非重复元素 <see cref="Func{TSource, TResult}"/> 对值进行比较。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="equality"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, Func<TSource, object> equality)
        {
            return source.Distinct(new DelegateComparer<TSource>(equality));
        }
        private class DelegateComparer<T> : IEqualityComparer<T>
        {
            private Func<T, T, bool> _equality;
            public DelegateComparer(Func<T, object> evluation)
            {
                if (evluation == null)
                {
                    throw new ArgumentNullException(nameof(evluation));
                }

                _equality = (source, des) =>
                {
                    var value = evluation(source);
                    var value2 = evluation(des);
                    return value?.Equals(value2) == true;
                };
            }
            public DelegateComparer(Func<T, T, bool> equality)
            {
                _equality = equality ?? throw new ArgumentNullException(nameof(equality));
            }
            public bool Equals(T x, T y)
            {
                return _equality(x, y);
            }

            public int GetHashCode(T obj)
            {
                return 1;
            }
        }
        private class DelegateEvluationComparer<T> : IEqualityComparer<T>
        {
            public bool Equals(T x, T y)
            {
                throw new NotImplementedException();
            }

            public int GetHashCode(T obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
