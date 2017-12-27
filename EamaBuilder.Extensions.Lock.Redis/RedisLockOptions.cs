using System;
using System.Collections.Generic;
using System.Text;

namespace EamaBuilder.Extensions.Lock.Redis
{
    public class RedisLockOptions
    {
        public string Configuration { get; set; }

        public string InstanceName { get; set; }
    }
}
