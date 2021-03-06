﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaBuilder.Extensions.Lock.Abstractions
{
    /// <summary>
    /// 表示用于获取分布式锁的提供程序
    /// </summary>
    public interface IDistributedLockProvider
    {
        /// <summary>
        /// 创建或获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDistributedLock GetLock(string name);
        /// <summary>
        /// 异步创建或获取锁
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IDistributedLock> GetLockAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
    }
}
