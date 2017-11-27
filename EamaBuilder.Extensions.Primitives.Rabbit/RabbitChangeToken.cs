using MediatR;
using Microsoft.Extensions.Primitives;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EamaBuilder.Extensions.Primitives.Rabbit
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RabbitChangeToken : IChangeToken
    {
        private bool _started;
        private event Action Handlers;
        private readonly IConnectionFactory _factory;
        private volatile IConnection _connection;
        private IModel _channel;

        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        private IDisposable _disposer;
        protected RabbitChangeToken(IConnectionFactoryBuilder factoryBuilder)
        {
            _factory = factoryBuilder.Build();
        }
        public bool HasChanged { get; }
        public bool ActiveChangeCallbacks { get; }

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            Handlers += () => callback(state);

            Connect();

            return _disposer;
        }
        private void Connect()
        {
            if (_connection != null)
            {
                return;
            }

            _connectionLock.Wait();

            try
            {
                if (_connection == null)
                {
                    _connection = _factory.CreateConnection();
                    _channel = _connection.CreateModel();

                    if (QueueName == null)
                    {
                        throw new InvalidOperationException("Current ");
                    }

                   
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }
        protected abstract string QueueName { get; }
        private class Disposer : IDisposable
        {
            public Disposer(IConnection connection)
            {
                RbMqConnection = ArgumentUtilities.NotNull(connection, nameof(connection));
            }
            public IConnection RbMqConnection { get; }
            public void Dispose()
            {
                RbMqConnection.Dispose();

            }
        }
    }
}
