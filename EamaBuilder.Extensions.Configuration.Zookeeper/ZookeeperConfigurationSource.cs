using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using org.apache.zookeeper;
namespace Microsoft.Extensions.Configuration.Zookeeper
{
    /// <inheritdoc />
    /// <summary>
    /// 基于zookeeper的配置中心
    /// </summary>
    internal class ZookeeperConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// 路径
        /// </summary>
        public ZookeeperPathString Path { get; set; }
        /// <summary>
        /// 数据编码
        /// </summary>
        public Encoding DataEncoding { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// session的超时时间
        /// </summary>
        public int SessionTimeout { get; set; }
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnlyConfiguration { get; set; }
        /// <inheritdoc />
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ZookeeperConfigurationProvider(this);
        }
    }
}
