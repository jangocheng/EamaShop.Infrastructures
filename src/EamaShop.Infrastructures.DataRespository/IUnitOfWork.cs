using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.DataRespository
{
    /// <summary>
    /// 表示一个工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 保存该工作单元的所有更改
        /// </summary>
        /// <param name="cancellationToken">取消任务的token</param>
        /// <returns></returns>
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveEntitiesAsync();
    }
}
