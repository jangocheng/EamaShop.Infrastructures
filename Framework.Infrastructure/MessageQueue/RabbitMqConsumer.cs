using System;
using System.Collections.Generic;
using System.Text;
using Framework.Infrastructure.Serialize;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Framework.Infrastructure.MessageQueue
{
    /// <inheritdoc />
    /// <summary>
    /// 基于rabbit实现的消息队列消费者
    /// </summary>
    public class RabbitMqConsumer<TMessage> : IConsumer<TMessage>
    {
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _eventing;
        private readonly Encoding _encoding;
        private readonly ISerializer _serializer;
        private bool _started;
        private readonly object _sync = new object();
        private readonly QueueDeclareOk _queueInfo;

        /// <summary>
        /// 初始化 <see cref="RabbitMqConsumer{TMessage}"/> 类型的新实例
        /// </summary>
        public RabbitMqConsumer(IOptions<RabbitMqOptions> options)
        {
            Checker.NotNull(options, nameof(options));
            _serializer = options.Value.Serializer;
            _encoding = options.Value.Encoding;
            _factory = new ConnectionFactory
            {
                UserName = options.Value.UserName,
                Password = options.Value.Password,
                HostName = options.Value.IpAddress,
                Port = options.Value.Port
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            var queue = options.Value.QueueSelector?.Invoke(typeof(TMessage));
            _queueInfo = _channel.QueueDeclare(queue, true, false, false);
            _eventing = new EventingBasicConsumer(_channel);

        }

        /// <inheritdoc />
        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }

        /// <inheritdoc />
        public event ConsumerDelegate<TMessage> Received;

        /// <inheritdoc />
        public void Run()
        {
            if (_started) throw new InvalidOperationException("current consumer has been run");
            _eventing.Received += (sender, message) =>
            {
                void Accept()
                {
                    _channel.BasicAck(message.DeliveryTag, false);
                }
                var json = _encoding.GetString(message.Body);
                var model = _serializer.Deserialize<TMessage>(json);
                Received?.Invoke(model, Accept);
            };
            _channel.BasicConsume(_queueInfo.QueueName, false, _eventing);
            _started = true;
        }
    }
}
