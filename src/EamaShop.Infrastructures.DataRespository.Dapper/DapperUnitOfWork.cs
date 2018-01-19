using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.DataRespository.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private DbConnection _connection;
        public DapperUnitOfWork(DbConnection connection)
        {
            TransactionId = Guid.NewGuid();
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
        public Guid TransactionId { get; }

        public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public int SaveEntitiesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
