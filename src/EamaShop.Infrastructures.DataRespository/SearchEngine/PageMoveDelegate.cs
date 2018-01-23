using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.DataRespository
{
    /// <summary>
    /// 获取某一页数据的委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public delegate Task<IEnumerable<T>> PageMoveDelegate<T>(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken));
}
