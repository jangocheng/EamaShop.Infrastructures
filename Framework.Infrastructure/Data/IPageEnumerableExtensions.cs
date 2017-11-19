using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// 分页数据结构的扩展
    /// </summary>
    public static class IPageEnumerableExtensions
    {
        /// <summary>
        /// 转换为不可枚举的结构，可作为前端展示
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IPageList<TData> ToPageList<TData>(this IPageEnumerable<TData> page)
            where TData : class
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            return new PageDirectList<TData>(page.Total, page.Datas.ToArray());
        }
        private class PageDirectList<TData> : IPageList<TData> where TData : class
        {
            public PageDirectList(long total, TData[] datas)
            {
                Total = total;
                Datas = datas;
            }
            public long Total { get; }
            public IEnumerable<TData> Datas { get; }
        }
    }
}
