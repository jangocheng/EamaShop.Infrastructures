using Framework.Infrastructure.Serialize;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.MessageQueue
{
    /// <summary>
    /// rabbit mq 的观察者
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RabbitObservable<T> : RabbitMQ, IObservable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public RabbitObservable(IOptions<RabbitMqOptions> options) : base(options)
        {
        }

        /// <summary>
        /// 订阅该时间
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            //启动消费者 消费指定的交换机
            var connection = CreateOpenConnection();
            var channel = connection.CreateModel();
            var exchange = QueueSelector(typeof(T));
            channel.ExchangeDeclare(exchange, ExchangeType.Fanout, true, false);
            var queue = channel.QueueDeclare("", true, false, false);
            channel.QueueBind(queue.QueueName, exchange, "");
            var eventing = new EventingBasicConsumer(channel);
            eventing.Received += (sender, arg) =>
            {
                var json = Encoding.GetString(arg.Body);
                var model = Serializer.Deserialize<T>(json);
                observer.OnNext(model);
                observer.OnCompleted();
                if(arg.BasicProperties.Headers.TryGetValue("ExceptionHeader",out var obj))
                {
                    observer.OnError((Exception)obj);
                }
            };
            channel.BasicConsume(queue.QueueName, true, eventing);
            return new RabbitDisposableObserver(channel,connection);
        }

       

        private class RabbitDisposableObserver : IDisposable
        {
            private readonly IConnection _connection;
            private readonly IModel _channel;
            public RabbitDisposableObserver(IModel channel,IConnection connection)
            {
                _connection = connection;
                _channel = channel;
            }
            public void Dispose()
            {
                _connection.Dispose();
                _channel.Dispose();
            }
        }
    }
}
