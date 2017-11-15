using Framework.Infrastructure.DataPages;
using Framework.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 表示一个只读的仓储
    /// </summary>
    /// <typeparam name="TEntity">仓储里的数据实体</typeparam>
    /// <typeparam name="TListModel">仓储分页/批量 搜索时返回的数据</typeparam>
    /// <typeparam name="TEnumerable">仓储分页/批量 搜索时返回的列表类型</typeparam>
    /// <typeparam name="TCondtion">仓储分页/批量 搜索时的条件</typeparam>
    /// <typeparam name="TPrimaryKey">仓储里的数据实体的主键类型</typeparam>
    public interface IRespository<TEntity, TListModel, TEnumerable, TCondtion, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>, IStatusRecord
        where TListModel : class
        where TEnumerable : IEnumerable<TListModel>
        where TCondtion : class, IPageSearch, IOrderPageSearch
    {
        

        /// <summary>
        /// 获取指定id的数据
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TPrimaryKey primaryKey);
        /// <summary>
        /// 根据条件批量获取数据列表
        /// </summary>
        /// <param name="condtions"></param>
        /// <returns></returns>
        Task<TEnumerable> GetListAsync(TCondtion condtions);
    }
}
