using System;
using System.Collections.Generic;
using System.Text;

namespace EamaBuilder.Extensions.Lock.Redis
{
    /// <summary>
    /// Gets the keys to get configuration from Environment or IConfiguration.
    /// </summary>
    public static class RedisDefaults
    {
        /// <summary>
        /// 连接字符串配置Key
        /// </summary>
        public const string ConfigurationKey = "REDIS_CONF";
        /// <summary>
        /// redis实例key
        /// </summary>
        public const string InstanceNameKey = "REDIS_INSTANCE";
    }
}
