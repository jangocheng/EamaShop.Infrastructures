using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Framework.Domain;
using Framework.Infrastructure;
using Framework.Infrastructure.DataPages;

namespace Framework.Respository
{
    public interface IArticleRepository : IRespository<Article>
    {
        /// <summary>
        /// 检索查询满足条件的文章列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="includeCate"></param>
        /// <param name="includeAuthor"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        IEnumerable<Article> SearchDesc(Expression<Func<Article, bool>> condition, IPageSearch page, bool includeCate = true, bool includeAuthor = true, params Expression<Func<Article, object>>[] orders);

        IEnumerable<Article> Search(Expression<Func<Article, bool>> condition, IPageSearch page, bool includeCate = true, bool includeAuthor = true,
            params Expression<Func<Article, object>>[] orders);

        long Count(Expression<Func<Article, bool>> condition);
    }
}
