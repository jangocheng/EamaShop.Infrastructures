using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// 表示具有唯一值自增类型的Id
    /// </summary>
    public interface IEntity : IEntity<long>
    {

    }
}
