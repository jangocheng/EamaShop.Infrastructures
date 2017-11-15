using Framework.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var json = await cache.GetStringAsync(key);

            return json.DeserializeJsonWithType<T>();
        }
        /// <summary>
        /// 获取缓存键相关联的值，如果不存在，则调用委托添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="aquire"></param>
        /// <param name="expireIn">有效时常 为null则是永久有效</param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key, Func<T> aquire, TimeSpan? expireIn = null)
        {
            var isSet = (await cache.GetStringAsync(key)) != null;
            if (isSet)
            {
                return await cache.GetAsync<T>(key);
            }
            var data = aquire();
            await cache.SetAsync(key, data);
            return data;
        }
        /// <summary>
        /// 将指定的缓存键的值设置为指定值,该值永不过期
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="cache"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value) =>
            await cache.SetStringAsync(key, value.SerializeJsonWithType());
        /// <summary>
        /// 将指定的缓存键的值设置为指定值，并设置为指定过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan expire) =>
           await cache.SetStringAsync(key, value.SerializeJsonWithType(), CreateOption(expire));

        private static DistributedCacheEntryOptions CreateOption(TimeSpan timeSpan)
        {
            return new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddTicks(timeSpan.Ticks)
            };
        }

    }
}
