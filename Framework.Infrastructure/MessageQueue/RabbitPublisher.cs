using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.MessageQueue
{
    /// <inheritdoc />
    public class RabbitPublisher:IPublisher
    {
        /// <inheritdoc />
        public Task PublishAsync()
        {
            throw new NotImplementedException();
        }
    }
}
