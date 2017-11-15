using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Infrastructure.DataPages
{
    /// <inheritdoc />
    /// <summary>
    /// 分页枚举的默认实现
    /// </summary>
    /// <typeparam name="TElemet"></typeparam>
    internal class PageEnumerable<TElemet> : IPageEnumerable<TElemet> where TElemet : class
    {
        private readonly TElemet[] _elemets;
        /// <summary>
        /// 初始化 <see cref="PageEnumerable{TElemet}"/> 类型的新实例
        /// </summary>
        public PageEnumerable(long total, IEnumerable<TElemet> datas)
        {
            Checker.NotNull(datas, nameof(datas));
            _elemets = datas.ToArray();
            Total = total;
        }

        /// <inheritdoc />
        public IEnumerator<TElemet> GetEnumerator()
        {
            return Datas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public long Total { get; }

        /// <inheritdoc />
        public IEnumerable<TElemet> Datas => _elemets;
    }
}
