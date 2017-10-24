using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure.Serialize;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Framework.Infrastructure.Cache
{
    /// <inheritdoc />
    /// <summary>
    /// 基于redis实现的异步缓存
    /// </summary>
    public class RedisAsyncCache : IAsyncCache
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly ISerializer _serializer;

        /// <summary>
        /// 初始化 <see cref="RedisAsyncCache"/> 类型的新实例
        /// </summary>
        public RedisAsyncCache(IOptions<RedisOptions> options)
        {
            Check.NotNull(options, nameof(options));
            _connection = ConnectionMultiplexer.Connect(new ConfigurationOptions()
            {
                EndPoints = { options.Value.IpAddress }
            });
            _serializer = options.Value.Serializer;
        }

        /// <inheritdoc />
        public async Task<object> GetAsync(string key, Type modelType)
        {
            string json = await _connection.GetDatabase().StringGetAsync(key);
            return _serializer.Deserialize(json, modelType);
        }

        /// <inheritdoc />
        public async Task SetAsync(string key, object value, Type modelType, TimeSpan? expire = null)
        {
            var json = _serializer.Serialize(value, modelType);
            await _connection.GetDatabase().StringSetAsync(key, json, expire);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(string key)
        {
            await _connection.GetDatabase().KeyDeleteAsync(key);
        }

        /// <inheritdoc />
        public async Task<bool> ContainsAsync(string key)
        {
            return await _connection.GetDatabase().KeyExistsAsync(key);
        }
    }
}
