using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.MessageQueue
{
    /// <summary>
    /// 消费者的扩展
    /// </summary>
    public static class ConsumerExtensions
    {
        /// <summary>
        /// 向消费者中添加单个消息处理程序，且在消息处理程序未发生异常时，自动确认消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="consumer"></param>
        /// <param name="onReceived"></param>
        public static void AddConsumerHandler<TMessage>(this IConsumer<TMessage> consumer, Action<TMessage> onReceived)
        {
            Check.NotNull(consumer, nameof(consumer));
            Check.NotNull(onReceived, nameof(onReceived));
            consumer.Received += (model, ack) =>
            {
                onReceived(model);
                ack();
            };
        }
        /// <summary>
        /// 异步启动消费者，并返回启动的线程
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="consumer"></param>
        /// <returns></returns>
        public static Task RunAsync<TMessage>(this IConsumer<TMessage> consumer)
        {
            return Task.Factory.StartNew(consumer.Run);
        }

        /// <summary>
        /// 异步启动消费者，注册委托处理程序，并返回启动的线程
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="consumer"></param>
        /// <param name="onReceived"></param>
        /// <returns></returns>
        public static Task RunAsync<TMessage>(this IConsumer<TMessage> consumer, Action<TMessage> onReceived)
        {
            consumer.AddConsumerHandler(onReceived);
            return consumer.RunAsync();
        }
        /// <summary>
        /// 启动消费者，注册委托处理程序
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="consumer"></param>
        /// <param name="onReceived"></param>
        public static void Run<TMessage>(this IConsumer<TMessage> consumer, Action<TMessage> onReceived)
        {
            consumer.AddConsumerHandler(onReceived);
            consumer.Run();
        }
    }
}
