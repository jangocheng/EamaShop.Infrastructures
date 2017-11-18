using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace System.Linq
{
    /// <summary>
    /// 延迟查询扩展
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 将字段排序的字符串转换为表达式 如果不存在，则返回null
        /// </summary>
        /// <typeparam name="TEntity">定义该属性的类型</typeparam>
        /// <param name="propertyName">属性的名称的字符串表达形式</param>
        /// <returns></returns>
        public static Expression<Func<TEntity, object>> CreateOrderExp<TEntity>(this string propertyName)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();

            var properties = typeInfo.DeclaredProperties;

            foreach (var p in properties)
            {
                if (p.Name == propertyName)
                {
                    // x=>x.Name
                    var parameter = Expression.Parameter(typeInfo, "x");

                    var body = Expression.Convert(Expression.MakeMemberAccess(parameter, p), typeof(object));

                    return Expression.Lambda<Func<TEntity, object>>(body, parameter);
                }
            }

            return null;
        }
        /// <summary>
        /// 按指定的字段名排序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string fieldName)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var field = fieldName.CreateOrderExp<TEntity>();

            if (field == null) throw new ArgumentException($"字段{fieldName}不存在");

            return source.OrderBy(field);
        }
        /// <summary>
        /// 按指定的字段名排序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderByDescending<TEntity>(this IQueryable<TEntity> source, string fieldName)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var field = fieldName.CreateOrderExp<TEntity>();

            if (field == null) throw new ArgumentException($"字段{fieldName}不存在");

            return source.OrderByDescending(field);
        }
        /// <summary>
        /// 按指定的字段集合进行排序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderByFields<TEntity>(this IQueryable<TEntity> source, params string[] fields)
        {
            return fields.Aggregate(source, (current, next) => source.OrderBy(next));
        }
        /// <summary>
        /// 按指定的字段集合进行排序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderByFieldsDescending<TEntity>(this IQueryable<TEntity> source, params string[] fields)
        {
            return fields.Aggregate(source, (current, next) => source.OrderByDescending(next));
        }
    }
}
