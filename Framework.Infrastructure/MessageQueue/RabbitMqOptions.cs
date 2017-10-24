using System;
using System.Collections.Generic;
using System.Text;
using Framework.Infrastructure.Serialize;

namespace Framework.Infrastructure.MessageQueue
{
    /// <summary>
    /// 表示rabbit消息队列配置
    /// </summary>
    public class RabbitMqOptions
    {

        /// <summary>
        /// 获取或设置一个值，该值指示消息的数据编码格式
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 获取或设置一个值，该值指示 Rabbit 服务器的地址，默认localhost
        /// </summary>
        public string IpAddress { get; set; } = "localhost";

        /// <summary>
        /// 获取或设置一个值，该值指示Rabbit使用的端口号，默认 5673
        /// </summary>
        public int Port { get; set; } = 5672;

        /// <summary>
        /// 获取或设置一个值，该值指示 Rabbit 队列的连接密码
        /// </summary>
        public string Password { get; set; } = "guest";

        /// <summary>
        /// 获取或设置一个值，该值指示 Rabbit 队列的连接用户名
        /// </summary>
        public string UserName { get; set; } = "guest";
        /// <summary>
        /// 表示消息队列名称的生成规则
        /// </summary>
        public Func<Type, string> QueueSelector { get; set; } =
        (type) => type.FullName;

        /// <summary>
        /// 设置序列化方式
        /// </summary>
        public ISerializer Serializer { get; set; } = new Serializer();
    }
}
