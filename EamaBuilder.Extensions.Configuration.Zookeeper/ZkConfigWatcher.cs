using Microsoft.Extensions.Primitives;
using org.apache.zookeeper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration.Zookeeper
{
    /// <summary>
    /// zookeeper 配置中心监听者
    /// </summary>
    /// <inheritdoc cref="IChangeToken" />
    internal sealed class ZkConfigWatcher : Watcher
    {
        /// <summary>
        /// 配置发送改变时的信号
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public override Task process(WatchedEvent @event)
        {
            return OnChangedAsync(@event);
        }

        public event Func<WatchedEvent, Task> OnChangedAsync;
    }
}
