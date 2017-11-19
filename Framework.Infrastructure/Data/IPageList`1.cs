using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// 分页的不可直接枚举的数据结构
    /// </summary>
    public interface IPageList<out TData> where TData : class
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        long Total { get; }
        /// <summary>
        /// 数据集合
        /// </summary>
        IEnumerable<TData> Datas { get; }
    }
}
