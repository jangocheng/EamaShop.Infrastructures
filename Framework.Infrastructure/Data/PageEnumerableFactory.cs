﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.Data
{
    /// <inheritdoc />
    public class PageEnumerableFactory : IPageEnumerableFactory
    {
        /// <inheritdoc />
        public IPageEnumerable<TElemet> CreatePageEnumerable<TElemet>(long total, IEnumerable<TElemet> sources) where TElemet : class
        {
            Checker.NotNull(sources, nameof(sources));
            return new PageEnumerable<TElemet>(total, sources);
        }
    }
}