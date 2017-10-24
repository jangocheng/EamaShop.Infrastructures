using System;
using System.Collections.Generic;
using System.Text;
using Framework.Infrastructure.DataPages;

namespace Framework.Infrastructure.DataPages
{
    /// <inheritdoc />
    /// <summary>
    /// 分页搜索条件的抽象基类
    /// </summary>
    public abstract class PageSearch : IPageSearch
    {
        private int _pageIndex = 1;
        private int _pageSize = 10;

        /// <inheritdoc />
        public virtual int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value <= 0 ? 1 : value;
        }

        /// <inheritdoc />
        public virtual int PageSize
        {
            get => _pageSize;
            set => _pageSize = value <= 0 ? 10 : value;
        }

    }
}
