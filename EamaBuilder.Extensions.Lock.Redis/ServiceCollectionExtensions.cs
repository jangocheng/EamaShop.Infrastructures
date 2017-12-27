using EamaBuilder.Extensions.Lock.Abstractions;
using EamaBuilder.Extensions.Lock.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for adding services of Redis Lock
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Default name of shared lock
        /// </summary>
        public const string DefaultSharedLockName = "DEFAULT_SHARED_REDIS_LOCK";
        /// <summary>
        /// Add redis lock
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddDistributedRedisLock(this IServiceCollection services,Action<RedisLockOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }
            services.Configure(configure);

            services.TryAddSingleton(sp =>
            {
                return sp.GetRequiredService<IDistributedLockProvider>().GetLock(DefaultSharedLockName);
            });

            services.TryAddSingleton<IDistributedLockProvider, RedisLockProvider>();

            return services;
        }
    }
}
