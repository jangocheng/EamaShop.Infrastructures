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
        /// 以异步的方式保存在该工作单元的所有更改
        /// </summary>
        /// <param name="cancellationToken">取消任务的token</param>
        /// <returns>保存成功的实体数</returns>
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 以同步的方式保存在该工作单元的所有更改
        /// </summary>
        /// <returns>保存成功的实体数</returns>
        int SaveEntitiesAsync();
        /// <summary>
        /// 获取强类型的实体仓储
        /// </summary>
        /// <typeparam name="TRespository">仓储的类型</typeparam>
        /// <returns>仓储</returns>
        TRespository GetRespository<TRespository>()
            where TRespository : IRespository;
    }
}
