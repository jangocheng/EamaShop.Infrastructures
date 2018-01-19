using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.EventBus
{
    /// <summary>
    /// Gets rabbit mq keys.
    /// </summary>
    public static class RabbitMQDefaults
    {
        /// <summary>
        /// Gets the key to get rabbit mq connection user name from IConfiguration or <see cref="Environment"/>
        /// </summary>
        public const string UserNameKey = "RABBITMQ_USERNAME";
        /// <summary>
        /// Gets the key to get rabbit mq connection password from IConfiguration or <see cref="Environment"/>
        /// </summary>
        public const string PasswordKey = "RABBITMQ_PASSWORD";
        /// <summary>
        /// Gets the key to get rabbit mq client publish retry time from IConfiguration or <see cref="Environment"/>
        /// </summary>
        public const string PublishRetryTimeKey = "RABBITMQ_PUBLISH_RETRY_TIME";
        /// <summary>
        /// Gets the key to get rabbit mq client retry-connect server times.
        /// </summary>
        public const string ConnectRetryTimeKey = "RABBITMQ_CONNECT_RETRY_TIME";
        /// <summary>
        /// Gets the key to get rabbit mq connect server host.
        /// </summary>
        public const string HostKey = "RABBITMQ_HOST";
        /// <summary>
        /// Gets the key to get rabbit mq connect server queue port.
        /// </summary>
        public const string PortKey = "RABBITMQ_PORT";
    }
}
