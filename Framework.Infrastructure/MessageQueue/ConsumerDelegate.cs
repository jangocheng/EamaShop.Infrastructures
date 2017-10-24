using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.MessageQueue
{
    /// <summary>
    /// 表示消息队列消费者的委托
    /// </summary>
    /// <typeparam name="TMessage">消息类型</typeparam>
    /// <param name="model">消息</param>
    /// <param name="ack">确认消息的委托</param>
    public delegate void ConsumerDelegate<in TMessage>(TMessage model, Action ack);

}
