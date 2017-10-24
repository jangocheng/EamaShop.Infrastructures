using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Cache
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
        public static async Task<T> GetAsync<T>(this IAsyncCache cache, string key)
            => (T) await cache.GetAsync(key, typeof(T));
        
        /// <summary>
        /// 获取缓存键相关联的值，如果不存在，则调用委托添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="aquire"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IAsyncCache cache, string key, Func<T> aquire)
        {
            var isSet = await cache.ContainsAsync(key);
            if (isSet)
            {
                return await cache.GetAsync<T>(key);
            }
            var data = aquire();
            await cache.SetAsync<T>(key, data);
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
        public static async Task SetAsync<T>(this IAsyncCache cache, string key, T value) =>
            await cache.SetAsync(key, value, typeof(T));
        /// <summary>
        /// 将指定的缓存键的值设置为指定值，并设置为指定过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public static async Task SetAsync<T>(this IAsyncCache cache, string key, T value, TimeSpan expire) =>
           await cache.SetAsync(key, value, typeof(T), expire);

    }
}
