using System;
using System.Collections.Generic;
using System.Text;

namespace EamaBuilder.Extensions.Lock.Redis
{
    /// <summary>
    /// Redis Lock Configurations
    /// </summary>
    public class RedisLockOptions
    {
        /// <summary>
        /// Configuration for connection.
        /// </summary>
        public string Configuration { get; set; }
        /// <summary>
        /// instance name.
        /// </summary>
        public string InstanceName { get; set; }
    }
}
