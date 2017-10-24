using System;
using System.Collections.Generic;
using System.Text;
using Framework.Domain;

namespace Framework.Respository
{
    public interface IArticleCategoryRespository:IRespository<ArticleCategory>
    {
        /// <summary>
        /// 获取指定类型下的子级分类
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IEnumerable<ArticleCategory> FindByParentId(long parentId);
    }
}
