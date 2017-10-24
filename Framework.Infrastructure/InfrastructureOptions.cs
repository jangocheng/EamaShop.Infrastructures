using System;
using System.Collections.Generic;
using System.Text;
using Framework.Infrastructure.Cache;
using Framework.Infrastructure.MessageQueue;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 基础设施的配置信息
    /// </summary>
    public sealed class InfrastructureOptions
    {
        private Action<RedisOptions> _redis;
        private Action<RabbitMqOptions> _rabbit;
        /// <summary>
        /// redis配置
        /// </summary>
        public Action<RedisOptions> RedisConfigure
        {
            get => _redis;
            set => _redis = value ?? throw new ArgumentException(Resources.ConfigureActionCannotBeNull(nameof(RedisConfigure)));
        }
        /// <summary>
        /// rabbit 配置
        /// </summary>
        public Action<RabbitMqOptions> RabbitConfigure
        {
            get => _rabbit;
            set => _rabbit = value ?? throw new ArgumentException(Resources.ConfigureActionCannotBeNull(nameof(RabbitConfigure)));
        }
    }
}
