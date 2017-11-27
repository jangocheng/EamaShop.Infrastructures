using System;
using System.Collections.Generic;
using System.Text;

namespace EamaBuilder.Extensions.Primitives.Rabbit
{
    public sealed class RabbitMqOptions
    {
        public string Host { get; set; } = "localhost";

        public string UserName { get; set; } = "guest";

        public string Password { get; set; } = "guest";

        public int Port { get; set; } = 5672;
    }
}
