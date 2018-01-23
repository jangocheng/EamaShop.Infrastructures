using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.DataRespository
{
    /// <summary>
    /// 翻页提供程序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PageEnumerable<T> : IPageEnumerable<T>
    {
        private const string ERROR_STARTPAGE = "无效的起始页，起始页必须大于0";
        private const string ERROR_PAGESIZE = "无效的页尺寸，每页的数据尺寸必须大于0";
        private const string ERROR_TOTAL = "无效的数据总数，总数必须大于0";
        private readonly PageMoveDelegate<T> _move;
        private readonly int _startPageIndex;
        private readonly int _pageSize;
        private readonly int _total;
        /// <summary>
        /// 初始化分页的枚举
        /// </summary>
        /// <param name="move">用于开始翻页的委托</param>
        /// <param name="startPageIndex">起始页</param>
        /// <param name="pageSize">每页获取数据的数量</param>
        /// <param name="total"></param>
        public PageEnumerable(PageMoveDelegate<T> move,
            int startPageIndex,
            int pageSize,
            int total)
        {
            _move = move ?? throw new ArgumentNullException(nameof(move));

            _total = total < 0
                ? throw new ArgumentOutOfRangeException(nameof(total), ERROR_TOTAL)
                : total;

            _startPageIndex = startPageIndex < 1
                ? throw new ArgumentOutOfRangeException(nameof(startPageIndex), ERROR_STARTPAGE)
                : startPageIndex;

            _pageSize = pageSize < 0
                ? throw new ArgumentOutOfRangeException(nameof(pageSize), ERROR_PAGESIZE)
                : pageSize;
        }

        /// <inheritdoc />
        public IPageEnumerator<T> GetEnumerator()
        {
            return new PageEnumerator<T>(_move, _startPageIndex, _pageSize, _total);
        }


    }
}
