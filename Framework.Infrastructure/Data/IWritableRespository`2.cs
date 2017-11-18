using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// 表示可读可写的仓储
    /// </summary>
    public interface IWritableRespository<TEntity, TPrimaryKey> 
        : IRespository<TEntity,  TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>, IStatusRecord
    {
        /// <summary>
        /// 获取仓储的工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// 更新指定的实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);
        /// <summary>
        /// 删除指定的数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveAsync(TEntity entity);
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);
    }
}
