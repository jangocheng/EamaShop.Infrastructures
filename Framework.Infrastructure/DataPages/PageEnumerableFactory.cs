using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.DataPages
{
    /// <inheritdoc />
    public class PageEnumerableFactory : IPageEnumerableFactory
    {
        /// <inheritdoc />
        public IPageEnumerable<TElemet> CreatePageEnumerable<TElemet>(long total, IEnumerable<TElemet> sources) where TElemet : class
        {
            Check.NotNull(sources, nameof(sources));
            return new PageEnumerable<TElemet>(total, sources);
        }
    }
}
