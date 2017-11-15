using Framework.Infrastructure.Serialize;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.MessageQueue
{
    /// <summary>
    /// rabbit mq 消息队列
    /// </summary>
    public abstract class RabbitMQ
    {
        /// <summary>
        /// 通讯数据的序列化器 默认json
        /// </summary>
        protected virtual ISerializer Serializer { get; }
        /// <summary>
        /// 数据编码 默认utf8
        /// </summary>
        protected virtual Encoding Encoding { get; }
        /// <summary>
        /// 用于创建链接的工厂
        /// </summary>
        protected IConnectionFactory ConnectionFactory { get; }
        /// <summary>
        /// 队列名称生成逻辑委托
        /// </summary>
        protected Func<Type,string> QueueSelector { get; }
        /// <summary>
        /// 初始化<see cref="RabbitMQ"/>的新实例
        /// </summary>
        /// <param name="options"></param>
        protected RabbitMQ(IOptions<RabbitMqOptions> options)
        {
            Checker.NotNull(options, nameof(options));
            Serializer = options.Value.Serializer;
            Encoding = options.Value.Encoding;
            ConnectionFactory = new ConnectionFactory
            {
                UserName = options.Value.UserName,
                Password = options.Value.Password,
                HostName = options.Value.IpAddress,
                Port = options.Value.Port
            };
            QueueSelector = options.Value.QueueSelector;
        }
        /// <summary>
        /// 创建一个连接
        /// </summary>
        /// <returns></returns>
        protected IConnection CreateOpenConnection()
        {
            return ConnectionFactory.CreateConnection();
        }
        
    }
}
