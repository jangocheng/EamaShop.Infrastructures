using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// A event relationship manager.
    /// </summary>
    public interface IEventHandlerManager : IDisposable
    {
        /// <summary>
        /// Add a new handler
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        void AddHandler<TEvent, THandler>()
            where THandler : IEventBusEventHandler<TEvent>
            where TEvent : IEventMetadata;
        /// <summary>
        /// Remove a exist handler.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        void RemoveHandler<TEvent, THandler>()
            where THandler : IEventBusEventHandler<TEvent>
            where TEvent : IEventMetadata;
        /// <summary>
        /// Get all handlers.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        IEnumerable<Type> GetHandlers(string eventName, out Type eventType);
    }
}
