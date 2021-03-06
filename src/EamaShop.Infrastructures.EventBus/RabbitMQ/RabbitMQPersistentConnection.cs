﻿using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using System.IO;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using Microsoft.Extensions.Options;

namespace EamaShop.Infrastructures
{
    /// <inheritdoc />
    public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private IConnection _connection;
        private readonly IConnectionFactory _factory;
        private readonly ILogger<RabbitMQPersistentConnection> _logger;
        private readonly int _retryCount;
        private readonly object _sync = new object();
        /// <inheritdoc />
        public RabbitMQPersistentConnection(IConnectionFactory factory, ILogger<RabbitMQPersistentConnection> logger, IOptions<RabbitMQEventBusOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = options.Value.ConnectRetryCount;
        }
        /// <inheritdoc />
        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        /// <inheritdoc />
        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        /// <inheritdoc />
        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ Client is trying to connect...");

            lock (_sync)
            {
                var time = 0;
                while (!IsConnected && time < _retryCount)
                {
                    try
                    {
                        _connection = _factory.CreateConnection();
                    }
                    catch (BrokerUnreachableException ex)
                    {
                        _logger.LogWarning("RabbitMQ Client has connect fail {0} times", time);
                        _logger.LogWarning(ex.ToString());
                    }
                    catch (SocketException ex)
                    {
                        _logger.LogWarning("RabbitMQ Client has connect fail {0} times", time);
                        _logger.LogWarning(ex.ToString());
                    }
                    finally
                    {
                        time++;
                    }
                }
                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    _logger.LogInformation($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");
                    return true;
                }
                else
                {
                    _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");
                    return false;
                }
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            if (_disposed) return;

            _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }
        #region IDisposable Support
        private bool _disposed = false; // To detect redundant calls

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    try
                    {
                        _connection.Dispose();
                    }
                    catch (IOException ex)
                    {
                        _logger.LogCritical(ex.ToString());
                    }

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RabbitMQPersistentConnection() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        /// <inheritdoc />
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
