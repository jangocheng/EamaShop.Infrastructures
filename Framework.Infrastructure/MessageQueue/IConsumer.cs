using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.MessageQueue
{
    /// <inheritdoc />
    /// <summary>
    /// 消费者实例，表示对消息队列具有消费信息能力的消费者
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IConsumer<out TMessage>:IDisposable
    {
        /// <summary>
        /// 表示消费者的处理程序
        /// </summary>
        event ConsumerDelegate<TMessage> Received;
        /// <summary>
        /// 消费者开始启动，进行消费任务,该方法是非阻塞的
        /// </summary>
        void Run();
    }
}
