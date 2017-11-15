using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Framework.Infrastructure.Serialize;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Framework.Infrastructure.MessageQueue
{
    /// <inheritdoc />
    /// <summary>
    /// 基于rabbit实现的消息队列消息生产者
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public class RabbitMqProducer<TMessage> : IProducer<TMessage>
    {
        private readonly ISerializer _serializer;
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly QueueDeclareOk _queueInfo;
        private readonly Encoding _encoding;

        /// <inheritdoc />
        public RabbitMqProducer(IOptions<RabbitMqOptions> options)
        {
            Checker.NotNull(options, nameof(options));
            _encoding = options.Value?.Encoding ?? throw new ArgumentException("配置错误，编码格式不能为空", nameof(options.Value));

            _serializer = options.Value.Serializer ?? throw new ArgumentException("配置错误，序列化器不能为空");
            _factory = new ConnectionFactory
            {
                UserName = options.Value.UserName,
                Password = options.Value.Password,
                HostName = options.Value.IpAddress,
                Port = options.Value.Port
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            var queue = options.Value.QueueSelector?.Invoke(typeof(TMessage)) ?? throw new ArgumentException("配置错误，队列名称生成器不能为空", nameof(options.Value.QueueSelector));
            _queueInfo = _channel.QueueDeclare(queue, true, false, false);
        }

        /// <inheritdoc />
        public Task SendAsync(TMessage message)
        {
            return Task.Factory.StartNew(() =>
             {
                 var properties = _channel.CreateBasicProperties();
                 properties.Persistent = true;
                 var json = _serializer.Serialize<TMessage>(message);
                 var body = _encoding.GetBytes(json);
                 _channel.BasicPublish("", _queueInfo.QueueName, properties, body);
             });
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}
