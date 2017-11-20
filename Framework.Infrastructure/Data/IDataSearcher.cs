using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// 提供对某个数据类型直接访问搜索的能力
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TConditions"></typeparam>
    public interface IDataSearcher<TData, TConditions>
        where TData : class, ISearchableMetadata
        where TConditions : IPageSearch, IOrderPageSearch
    {
        /// <summary>
        /// 搜索指定的数据
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IPageEnumerable<TData> Search(TConditions conditions);
        /// <summary>
        /// 异步搜索指定的数据
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        Task<IPageEnumerable<TData>> SearchAsync(TConditions conditions);
    }
}
