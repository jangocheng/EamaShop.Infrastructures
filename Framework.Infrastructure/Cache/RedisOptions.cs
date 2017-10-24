using System;
using System.Collections.Generic;
using System.Text;
using Framework.Infrastructure.Serialize;

namespace Framework.Infrastructure.Cache
{
    /// <summary>
    /// Redis配置
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        /// Ip
        /// </summary>
        public string IpAddress { get; set; } = "localhost";
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 6379;
        /// <summary>
        /// 序列化方式
        /// </summary>
        public ISerializer Serializer { get; set; } = new Serializer();
    }
}
