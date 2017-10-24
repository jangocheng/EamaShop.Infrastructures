using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.MessageQueue
{
    /// <summary>
    /// 事件发布者
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// 发布一个事件消息
        /// </summary>
        /// <returns></returns>
        Task PublishAsync();
    }
}
