using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    /// <inheritdoc cref="IAggregate" />
    /// <summary>
    /// 文章分类
    /// </summary>
    public class ArticleCategory:BaseAggregate,IAggregate
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级分类的Id
        /// </summary>
        public long ParentId { get; set; }
    }
}
