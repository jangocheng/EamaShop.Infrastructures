using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;

namespace EamaBuilder.Extensions.Primitives.Rabbit
{
    internal class ConnectionFactoryBuilder : IConnectionFactoryBuilder
    {
        private readonly RabbitMqOptions _options;
        public ConnectionFactoryBuilder(IOptions<RabbitMqOptions> options)
        {
            _options = ArgumentUtilities.NotNull(options, nameof(options)).Value;
        }
        public IConnectionFactory Build()
        {
            return new ConnectionFactory()
            {
                HostName = _options.Host,
                Password = _options.Password,
                UserName = _options.UserName,
                Port = _options.Port
            };
        }
    }
}
