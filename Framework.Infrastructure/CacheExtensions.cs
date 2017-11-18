using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Caching.Distributed
{
    /// <summary>
    /// 定义缓存的扩展
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 从缓存获取指定键的对象
        /// </summary>
        /// <typeparam name="T">获取对象的类型</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">缓存的键</param>
        /// <param name="token">可选的.一个用于取消线程操作的 <see cref="CancellationToken"/> 对象</param>
        /// <returns>对象</returns>
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key, CancellationToken token = default(CancellationToken))
        {
            var json = await cache.GetStringAsync(key);

            return json.DeserializeJsonWithType<T>();
        }
        /// <summary>
        /// 获取缓存键相关联的值，如果不存在，则调用委托添加到缓存中，该值永不过期
        /// </summary>
        /// <typeparam name="T">获取对象的类型</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">缓存的键</param>
        /// <param name="aquire">获取缓存的值的委托</param>
        /// <param name="token">可选的.一个用于取消线程操作的 <see cref="CancellationToken"/> 对象</param>
        /// <returns></returns>
        public static async Task<T> GetOrAddAsync<T>(this IDistributedCache cache, string key, Func<T> aquire, CancellationToken token = default(CancellationToken))
        {
            var json = await cache.GetStringAsync(key, token);

            if (json != null) return json.DeserializeJsonWithType<T>();

            var data = aquire();
            await cache.SetAsync(key, data, token);
            return data;
        }

        /// <summary>
        /// 获取缓存键相关联的值，如果不存在，则调用委托添加到缓存中，并在指定时间过期
        /// </summary>
        /// <typeparam name="T">获取对象的类型</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">缓存的键</param>
        /// <param name="aquire">获取缓存的值的委托</param>
        /// <param name="timeSpan">过期的间隔</param>
        /// <param name="token">可选的.一个用于取消线程操作的 <see cref="CancellationToken"/> 对象</param>
        /// <returns></returns>
        public static async Task<T> GetOrAddAsync<T>(this IDistributedCache cache, string key, Func<T> aquire, TimeSpan timeSpan, CancellationToken token = default(CancellationToken))
        {
            var json = await cache.GetStringAsync(key, token);

            if (json != null) return json.DeserializeJsonWithType<T>();

            var data = aquire();
            await cache.SetAsync(key, data, timeSpan, token);
            return data;
        }
        /// <summary>
        /// 获取缓存键相关联的值，如果不存在，则添加进去
        /// </summary>
        /// <typeparam name="T">获取对象的类型</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">缓存的键</param>
        /// <param name="data">缓存的值</param>
        /// <param name="token">可选的.一个用于取消线程操作的 <see cref="CancellationToken"/> 对象</param>
        /// <returns></returns>
        public static async Task<T> GetOrAddAsync<T>(this IDistributedCache cache, string key, T data, CancellationToken token = default(CancellationToken))
        {
            var isSet = (await cache.GetStringAsync(key)) != null;
            if (isSet)
            {
                return await cache.GetAsync<T>(key);
            }
            await cache.SetAsync(key, data);
            return data;
        }
        /// <summary>
        /// 获取缓存键相关联的值，如果不存在，添加并设置过期时间
        /// </summary>
        /// <typeparam name="T">获取对象的类型</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">缓存的键</param>
        /// <param name="timeSpan">过期的间隔</param>
        /// <param name="data">缓存的值</param>
        /// <param name="token">可选的.一个用于取消线程操作的 <see cref="CancellationToken"/> 对象</param>
        /// <returns></returns>
        public static async Task<T> GetOrAddAsync<T>(this IDistributedCache cache, string key, T data, TimeSpan timeSpan, CancellationToken token = default(CancellationToken))
        {
            var isSet = (await cache.GetStringAsync(key)) != null;
            if (isSet)
            {
                return await cache.GetAsync<T>(key);
            }
            await cache.SetAsync(key, data, timeSpan);
            return data;
        }
        /// <summary>
        /// 将指定的缓存键的值设置为指定值,该值永不过期
        /// </summary>
        /// <typeparam name="T">获取对象的类型</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">缓存的键</param>
        /// <param name="value">缓存的值</param>
        /// <param name="token">可选的.一个用于取消线程操作的 <see cref="CancellationToken"/> 对象</param>
        /// <returns>线程对象</returns>
        public static async Task SetAsync<T>(this IDistributedCache cache,
            string key,
            T value,
            CancellationToken token = default(CancellationToken)) =>
            await cache.SetStringAsync(key, value.SerializeJsonWithType(), token);
        /// <summary>
        /// 将指定的缓存键的值设置为指定值，并设置为指定过期时间
        /// </summary>
        /// <typeparam name="T">获取对象的类型</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">缓存的键</param>
        /// <param name="value">缓存的值</param>
        /// <param name="expire"></param>
        /// <param name="token">可选的.一个用于取消线程操作的 <see cref="CancellationToken"/> 对象</param>
        /// <returns>线程对象</returns>
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan expire, CancellationToken token = default(CancellationToken)) =>
           await cache.SetStringAsync(key, value.SerializeJsonWithType(), CreateOption(expire), token);

        private static DistributedCacheEntryOptions CreateOption(TimeSpan timeSpan)
        {
            return new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddTicks(timeSpan.Ticks)
            };
        }

    }
}
