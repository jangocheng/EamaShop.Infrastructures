using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// 文章
    /// </summary>
    public class Article:BaseAggregate,IAggregate
    {
        /// <summary>
        /// 文章的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 文章的所属的类目Id
        /// </summary>
        public long CategoryId { get; set; }
        /// <summary>
        /// 文章的作者Id
        /// </summary>
        public long AuthorId { get; set; }
        /// <summary>
        /// 文章的内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public User Author { get; set; }
        /// <summary>
        /// 分类信息
        /// </summary>
        public ArticleCategory Category { get; set; }
    }
}
