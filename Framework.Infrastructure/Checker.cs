using System;
using System.Collections.Generic;
using System.Text;
using Framework.Infrastructure.Exceptions;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 验证对象
    /// </summary>
    internal static class Checker
    {
        /// <summary>
        /// 对象不可能为null,如果为null 抛出 <see cref="ArgumentNullException"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="parameterName"></param>
        public static void NotNull<T>(T obj, string parameterName) where T : class
        {
            if (obj != null) return;
            NotNull(parameterName, nameof(parameterName));
            throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// 对象不可能为null,如果为null 抛出 <see cref="ArgumentNullException"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="parameterName"></param>
        public static void NotNull<T>(IEnumerable<T> array, string parameterName) where T : class
        {
            foreach (var a in array)
            {
                NotNull(a, parameterName);
            }
        }
        /// <summary>
        /// 对象不可能为null,如果为null 抛出 <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameterName"></param>
        public static void NotNull(string obj, string parameterName)
        {
            NotNull<string>(obj, parameterName);
        }
        /// <summary>
        /// 对象不可能为null,如果为null 抛出 <see cref="BizException"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="document"></param>
        public static void BizNotNull<T>(T obj, string document) where T : class
        {
            if (obj == null)
            {
                BizShortCircuit(document, 1);
            }
        }
        /// <summary>
        /// 使用指定的错误信息和错误代码抛出 <see cref="BizException"/> 异常
        /// </summary>
        /// <param name="document"></param>
        public static void BizShortCircuit(string document, int code)
        {
            NotNull(document, nameof(document));
            throw new BizException(document, code);
        }

    }
}
