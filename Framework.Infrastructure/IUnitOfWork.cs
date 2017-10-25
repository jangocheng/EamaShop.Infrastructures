using System;
using System.Collections.Generic;
using System.Text;
using Framework.Domain;
using Framework.Domain.Abstractions;

namespace Framework.Infrastructure
{
    /// <inheritdoc />
    /// <summary>
    /// 允许注册执行操作的数据事务单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 注册指定的实体用于添加到数据库中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool RegisterAdd<T>(T entity) where T : class, IAggregate;
        /// <summary>
        /// 注册指定的实体用于从数据库中更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool RegisterUpdate<T>(T entity) where T : class, IAggregate;
        /// <summary>
        /// 注册指定的实体用于从数据库中删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="logic">是否是逻辑删除</param>
        /// <returns></returns>
        bool RegisterDelete<T>(T entity, bool logic = true) where T : class, IAggregate;

        /// <summary>
        /// 将局部更新的操作注册到工作单元
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyNames">需要更新的字段集合</param>
        /// <returns></returns>
        bool RegisterUpdateOnly<T>(T entity, params string[] propertyNames) where T : class, IAggregate;
        /// <summary>
        /// 提交该次工作单元的所有数据操作
        /// </summary>
        /// <returns></returns>
        bool Commit();
    }
}
