using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.DataPages
{
    /// <summary>
    /// 提供创建分页枚举的工厂
    /// </summary>
    public interface IPageEnumerableFactory
    {
        /// <summary>
        /// 创建分页的枚举
        /// </summary>
        /// <typeparam name="TElemet"></typeparam>
        /// <param name="total"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        IPageEnumerable<TElemet> CreatePageEnumerable<TElemet>(long total, IEnumerable<TElemet> sources) where TElemet : class;
    }
}
