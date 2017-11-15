using Framework.Infrastructure.DataPages;
using Framework.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 表示可读可写的仓储
    /// </summary>
    public interface IWritableRespository<TEntity, TListModel, TEnumerable, TCondtion, TPrimaryKey> 
        : IRespository<TEntity, TListModel, TEnumerable, TCondtion, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>, IStatusRecord
        where TListModel : class
        where TEnumerable : IEnumerable<TListModel>
        where TCondtion : class, IPageSearch, IOrderPageSearch
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
        /// 保存所做的所有修改
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
