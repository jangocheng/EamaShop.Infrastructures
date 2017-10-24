using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Framework.Infrastructure.Configuration
{
    /// <summary>
    /// 配置中心
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// 添加配置中心支持
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddConfigurationCenter(this IConfigurationBuilder builder)
        {
            
            builder.Add(new ZookeeperConfigurationSource(new ZookeeperOptions()));
            return builder;
        }
    }
}
