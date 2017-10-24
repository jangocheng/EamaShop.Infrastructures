using System;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Cache
{
    /// <summary>
    /// 定义异步缓存的基础行为
    /// </summary>
    public interface IAsyncCache
    {
        /// <summary>
        /// 以异步的方式，从缓存中获取指定键的指定类型的对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="modelType">对象的类型</param>
        /// <returns></returns>
        Task<object> GetAsync(string key, Type modelType);
        /// <summary>
        /// 以异步的方式，讲指定的键的值设置为指定值，如果键不存在，则会创建键后设置；
        /// 如果未设置过期时间，则一直不过期
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="modelType">值的类型</param>
        /// <param name="expire">有效时间</param>
        /// <returns></returns>
        Task SetAsync(string key, object value, Type modelType, TimeSpan? expire = null);
        /// <summary>
        /// 以异步的方式，从缓存里删除指定键和值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);
        /// <summary>
        /// 异步获取缓存是否存在指定的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> ContainsAsync(string key);
    }
}
