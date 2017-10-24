using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 表达式树的扩展
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// 以And的形式连接俩个lambda2元表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> source,
            Expression<Func<T, bool>> right)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.MakeBinary(ExpressionType.AndAlso, source, right), source.Parameters);
        }
    }
}
