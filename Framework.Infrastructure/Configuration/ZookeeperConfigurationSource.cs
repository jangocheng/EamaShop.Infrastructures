using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Framework.Infrastructure.Configuration
{
    /// <inheritdoc />
    /// <summary>
    /// 基于zookeeper的配置中心
    /// </summary>
    public class ZookeeperConfigurationSource:IConfigurationSource
    {
        private ZookeeperOptions _options;
        /// <inheritdoc />
        public ZookeeperConfigurationSource(ZookeeperOptions options)
        {
            Check.NotNull(options, nameof(options));
            _options = options;
        }
        /// <inheritdoc />
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ZookeeperConfigurationProvider(_options);
        }
    }
}
