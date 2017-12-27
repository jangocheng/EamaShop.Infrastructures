using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// Represent a persistent connction for RabbitMQ.
    /// <para></para>
    /// Do not use this interface directly in your application code.
    /// </summary>
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        /// <summary>
        /// Gets a value represent is current connection has connected and open.
        /// </summary>
        bool IsConnected { get; }
        /// <summary>
        /// Try Connection RabbitMQ Server.
        /// </summary>
        /// <returns>if connecte successfully return <see langword="true"/> else reutrn <see langword="false"/></returns>
        bool TryConnect();

        /// <summary>
        /// Create a <see cref="IModel"/> by using this connection.
        /// </summary>
        /// <returns></returns>
        IModel CreateModel();
    }
}
