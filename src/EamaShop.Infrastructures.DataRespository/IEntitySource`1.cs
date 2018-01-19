using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.DataRespository
{
    /// <summary>
    /// A marker interface for your queryable entity.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntitySource<TEntity>
        where TEntity : class
    {

    }
}
