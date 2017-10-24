using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.MessageQueue
{
    /// <summary>
    /// 事件订阅者
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    public interface ISubscriber<TEventData>
    {
        /// <summary>
        /// 订阅指定的事件
        /// </summary>
        /// <param name="handler"></param>
        void RegisterEventHandler(EventHandler<TEventData> handler);
    }
}
