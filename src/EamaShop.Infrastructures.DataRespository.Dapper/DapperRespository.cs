using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EamaShop.Infrastructures.DataRespository.Dapper
{
    public abstract class DapperRespository : IRespository
    {
        protected IReadOnlyDictionary<Type, object> EntitySources { get; }
        protected DbConnection DbConnection => _connection;
        private readonly DbConnection _connection;
        protected DapperRespository(DbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            UnitOfWork = new DapperUnitOfWork(connection);
            Configure(new DapperInternalEntitySourceContainer());

        }
        public IUnitOfWork UnitOfWork { get; }

       

        protected abstract void Configure(EntitySourceContainer sourceContainer);

        public TEntitySource GetEntitySource<TEntitySource, TEntity>()
            where TEntitySource : IEntitySource<TEntity>
            where TEntity : class
        {
            throw new NotImplementedException();
        }

        private class DapperInternalEntitySourceContainer : EntitySourceContainer
        {
           
            public override void AddEntitySource<TEntity>(IEntitySource<TEntity> entitySource)
            {

            }
        }
    }
}
