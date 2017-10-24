using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.Configuration
{
    /// <summary>
    /// 配置中心Zookeeper的配置项
    /// </summary>
    public class ZookeeperOptions
    {
        /// <summary>
        /// Ip
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan SessionTimeout { get; set; }

        public string RootPath { get; set; } = "configs";
    }
}
