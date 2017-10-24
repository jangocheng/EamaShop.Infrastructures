using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.DataPages
{
    /// <inheritdoc />
    /// <summary>
    /// 表示分页枚举
    /// </summary>
    /// <typeparam name="TElemet"></typeparam>
    public interface IPageEnumerable<out TElemet> : IEnumerable<TElemet> where TElemet : class
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        long Total { get; }
        /// <summary>
        /// 数据集合
        /// </summary>
        IEnumerable<TElemet> Datas { get; }
    }
}
