using Framework.Infrastructure.DataPages;
using Framework.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 分页获取数据 主键为long类型的读写仓储
    /// </summary>
    /// <typeparam name="TEntity">仓储里的数据实体</typeparam>
    /// <typeparam name="TListModel">仓储分页/批量 搜索时返回的数据</typeparam>
    /// <typeparam name="TCondtion">仓储分页/批量 搜索时的条件</typeparam>
    public interface IWritableRespository<TEntity, TListModel, TCondtion>
        : IRespository<TEntity, TListModel, TCondtion>,
        IRespository<TEntity, TListModel, IPageEnumerable<TListModel>, TCondtion, long>,
        IWritableRespository<TEntity, TListModel, IPageEnumerable<TListModel>, TCondtion, long>
        where TEntity : IEntity, IStatusRecord
        where TListModel : class
        where TCondtion : class, IPageSearch, IOrderPageSearch
    {
    }
}
