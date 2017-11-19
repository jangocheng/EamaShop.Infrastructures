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
    public sealed class ZkConfigWatcher : Watcher
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
        /// <summary>
        /// 当配置发生改变时触发该事件
        /// </summary>
        public event Func<WatchedEvent, Task> OnChangedAsync;
    }
}
