using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.DataRespository
{
    /// <summary>
    /// 表示翻页器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPageEnumerator<out T>
    {
        /// <summary>
        /// 获取当前页面的数据
        /// </summary>
        IEnumerable<T> CurrentPageDatas { get; }
        /// <summary>
        /// 当前页面的页码
        /// </summary>
        int CurrentPageIndex { get; }
        /// <summary>
        /// 翻页器
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPageCount { get; }
        /// <summary>
        /// 翻到下一页
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> MoveNextPage(CancellationToken cancellationToken);
        /// <summary>
        /// 回到起始页码
        /// </summary>
        void Reset();
    }
}
