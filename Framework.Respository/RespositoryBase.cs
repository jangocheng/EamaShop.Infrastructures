using System;
using System.Collections.Generic;
using System.Text;
using Framework.Domain;
using Framework.Infrastructure;

namespace Framework.Respository
{
    public abstract class RespositoryBase<TEntity> where TEntity : class, IAggregate
    {

        public abstract bool Update(TEntity entity);

        public virtual bool Delete(TEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            entity.Status = Status.Delete;
            return Update(entity);
        }
    }
}
