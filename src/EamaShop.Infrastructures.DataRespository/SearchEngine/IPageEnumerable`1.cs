using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.DataRespository
{
    /// <summary>
    /// 公开枚举数，支持指定类型的集合上的一个分页方式的异步迭代。
    /// </summary>
    /// <typeparam name="T">集合的类型</typeparam>
    public interface IPageEnumerable<T>
    {
        /// <summary>
        /// 获取用于在指定类似的集合上迭代的分页迭代器
        /// </summary>
        /// <returns></returns>
        IPageEnumerator<T> GetEnumerator();
    }
}
