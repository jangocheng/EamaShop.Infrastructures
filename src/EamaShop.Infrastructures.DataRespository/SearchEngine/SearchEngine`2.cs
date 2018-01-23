using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.DataRespository
{
    /// <summary>
    /// 表示搜索引擎
    /// </summary>
    /// <typeparam name="TListModel">搜索的列表对象</typeparam>
    /// <typeparam name="TConditions">条件</typeparam>
    public abstract class SearchEngine<TListModel, TConditions>
        where TListModel : ISearchMetadata
        where TConditions : class, IEquatable<TConditions>
    {

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="conditions">条件对象</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的条数</param>
        /// <param name="cancellationToken">总数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="pageIndex"/>  或 <paramref name="pageSize"/> 小于零
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="conditions"/>不能为null</exception>
        public virtual async Task<IPageEnumerable<TListModel>> SearchAsync(TConditions conditions,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Task<IEnumerable<TListModel>> Search(int index,
                int size,
                CancellationToken token = default(CancellationToken))
            {
                token.ThrowIfCancellationRequested();

                return SearchCoreAsync(conditions, index, size, token);
            }

            var total = await SearchTotal(conditions);

            return new PageEnumerable<TListModel>(Search, pageIndex, pageSize, total);
        }
        /// <summary>
        /// 按条件搜索数据集合
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task<IEnumerable<TListModel>> SearchCoreAsync(TConditions conditions,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 按条件获取数据总数
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task<int> SearchTotal(TConditions conditions, CancellationToken cancellationToken = default(CancellationToken));
    }
}
