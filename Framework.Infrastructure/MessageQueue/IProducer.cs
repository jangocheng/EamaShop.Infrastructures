using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.MessageQueue
{
    /// <inheritdoc />
    /// <summary>
    /// 消息生产者
    /// </summary>
    public interface IProducer<in TMessage>:IDisposable
    {
        /// <summary>
        /// 推送消息到消息队列中
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendAsync(TMessage message);
    }
}
