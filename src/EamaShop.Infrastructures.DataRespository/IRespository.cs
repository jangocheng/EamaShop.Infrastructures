using System;
using System.Data;

namespace EamaShop.Infrastructures.DataRespository
{
    /// <summary>
    /// 表示存储数据的数据仓储
    /// </summary>
    public interface IRespository
    {
        /// <summary>
        /// 获取实体数据源，用于查询或操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TEntitySource">数据源的类型</typeparam>
        /// <returns></returns>
        TEntitySource GetEntitySource<TEntitySource, TEntity>()
            where TEntity : class
            where TEntitySource : IEntitySource<TEntity>;
        /// <summary>
        /// 获取事务工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
