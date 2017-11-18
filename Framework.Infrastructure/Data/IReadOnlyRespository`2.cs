using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// 表示一个只读的仓储
    /// </summary>
    /// <typeparam name="TEntity">仓储里的数据实体</typeparam>
    /// <typeparam name="TPrimaryKey">仓储里的数据实体的主键类型</typeparam>
    public interface IReadOnlyRespository<TEntity, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>, IStatusRecord
    {
        /// <summary>
        /// 获取指定id的数据
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TPrimaryKey primaryKey);
    }
}
