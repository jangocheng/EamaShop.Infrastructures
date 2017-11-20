using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Zookeeper;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// 配置中心
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// 添加Zookeeper的配置中心
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="path">配置中心的配置节点，该节点下的所有子节点会被递归作为配置</param>
        /// <param name="sessionTimeout">session的过期时间</param>
        /// <param name="encoding">数据的编码格式</param>
        /// <param name="readOnly">是否是只读的，对于管理配置的，设置为false，对于使用配置的应用，则为true</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddZookeeper(this IConfigurationBuilder builder,
            string connectionString,
            ZookeeperPathString path,
            Encoding encoding,
            int sessionTimeout,
            bool readOnly)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var source = new ZookeeperConfigurationSource
            {
                ConnectionString = connectionString,
                DataEncoding = encoding,
                SessionTimeout = sessionTimeout,
                Path = path
            };
            return builder.Add(source);
        }
        /// <summary>
        /// 添加Zookeeper的配置中心
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="path">配置中心的配置节点，该节点下的所有子节点会被递归作为配置</param>
        /// <param name="sessionTimeout">session的过期时间</param>
        /// <param name="encoding">数据的编码格式</param>
        /// <param name="readOnly">是否是只读的，对于管理配置的，设置为false，对于使用配置的应用，则为true</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddZookeeper(this IConfigurationBuilder builder,
            string connectionString,
            string path = "/ZkAppConfigs",
            string encoding = "utf-8",
            int sessionTimeout = 50000,
            bool readOnly = true)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var source = new ZookeeperConfigurationSource
            {
                ConnectionString = connectionString,
                DataEncoding = Encoding.GetEncoding(encoding),
                SessionTimeout = sessionTimeout,
                Path = path
            };
            return builder.Add(source);
        }
    }
}
