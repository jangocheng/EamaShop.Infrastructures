using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.Entity
{
    /// <summary>
    /// 表示具有唯一性的实体
    /// </summary>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 唯一标识Id
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}
