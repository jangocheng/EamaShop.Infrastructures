using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaBuilder.Extensions.Primitives.Rabbit
{
    /// <summary>
    /// A builder for build <see cref="IConnectionFactory"/>
    /// </summary>
    public interface IConnectionFactoryBuilder
    {
        /// <summary>
        /// build a new <see cref="IConnectionFactory"/>
        /// </summary>
        /// <returns></returns>
        IConnectionFactory Build();
    }
}
