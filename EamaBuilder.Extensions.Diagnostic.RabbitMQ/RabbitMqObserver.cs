using System;
using System.Collections.Generic;
using System.Text;

namespace EamaBuilder.Extensions.Diagnostic.RabbitMQ
{
    internal class RabbitMqObserver : IObserver<KeyValuePair<string, object>>
    {
        public void OnCompleted()
        {
            //
        }

        public void OnError(Exception error)
        {
            //
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            //
        }
    }
}
