using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.DataRespository.Dapper
{
    public abstract class EntitySourceContainer
    {
        public abstract void AddEntitySource<TEntity>(IEntitySource<TEntity> entitySource)
            where TEntity : class;
    }
}
