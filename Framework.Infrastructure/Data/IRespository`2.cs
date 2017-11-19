using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// 表示可读可写的仓储
    /// </summary>
    public interface IRespository<TEntity, TPrimaryKey> 
        : IReadOnlyRespository<TEntity,  TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>, IStatusRecord
    {
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
        /// <summary>
        /// 保存在此仓储的所有更改
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
