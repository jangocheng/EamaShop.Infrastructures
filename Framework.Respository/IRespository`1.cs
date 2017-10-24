using System;
using System.Linq.Expressions;
using Framework.Domain;

namespace Framework.Respository
{
    /// <inheritdoc />
    /// <summary>
    /// 提供对某个数据类型对象的通用操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRespository<TEntity> : IDisposable where TEntity : class, IAggregate
    {
        TEntity Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        TEntity Find(long primaryKey);
        /// <summary>
        /// 局部更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parameterNames"></param>
        /// <returns></returns>
        bool UpdateOnly(TEntity entity, params string[] parameterNames);
        /// <summary>
        /// 局部更新指定实体，使用指定的Selector搜寻和指定当前实体的属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selectors"></param>
        /// <returns></returns>
        bool UpdateOnly(TEntity entity, params Expression<Func<TEntity, object>>[] selectors);
    }
}
