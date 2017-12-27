using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// Represent a handler that can handling event.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventBusEventHandler<TEvent>
    {
        /// <summary>
        /// Handling event on event fired by event bus.
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task HandleAsync(TEvent @event);
    }
}
