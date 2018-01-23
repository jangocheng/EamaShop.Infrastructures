using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.DataRespository
{
    /// <summary>
    /// 翻页者
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageEnumerator<T> : IPageEnumerator<T>
    {
        private readonly int _startPageIndex;
        private int _currentPageIndex;
        private readonly int _pageSize;
        private readonly PageMoveDelegate<T> _move;
        private readonly int _total;
        /// <summary>
        /// 初始化枚举迭代器的新实例
        /// </summary>
        /// <param name="move">获取下一页数据的委托</param>
        /// <param name="startPageIndex">起始页</param>
        /// <param name="pageSize">每个页面的数据数量</param>
        /// <param name="total"></param>
        public PageEnumerator(PageMoveDelegate<T> move,
            int startPageIndex,
            int pageSize,
            int total)
        {
            _move = move ?? throw new ArgumentNullException(nameof(move));

            _startPageIndex = startPageIndex < 0
                ? throw new ArgumentOutOfRangeException(nameof(startPageIndex))
                : startPageIndex;

            _pageSize = pageSize < 0
                ? throw new ArgumentOutOfRangeException(nameof(pageSize))
                : pageSize;

            _total = total < 0
                ? throw new ArgumentOutOfRangeException(nameof(total))
                : total;

            _currentPageIndex = startPageIndex;

            _currentPageDatas = move(startPageIndex, pageSize, CancellationToken.None)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            TotalPageCount = (int)Math.Ceiling((total * 1.0) / pageSize);
        }
        private IEnumerable<T> _currentPageDatas;
        /// <inheritdoc />
        public IEnumerable<T> CurrentPageDatas => _currentPageDatas;
        /// <inheritdoc />
        /// <inheritdoc />
        public int CurrentPageIndex => _currentPageIndex;
        /// <inheritdoc />
        public int PageSize => _pageSize;
        /// <inheritdoc />
        public int TotalPageCount { get; }

        public async Task<bool> MoveNextPage(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _currentPageIndex++;

            if (((_currentPageIndex - 2) * _pageSize) > _total)
            {
                return false;
            }
            _currentPageDatas = await _move(_currentPageIndex, _pageSize, cancellationToken);

            return true;
        }

        /// <inheritdoc />
        public void Reset()
        {
            _currentPageIndex = _startPageIndex;
        }
    }
}
