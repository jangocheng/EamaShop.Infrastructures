using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using org.apache.zookeeper;

namespace Framework.Infrastructure.Configuration
{
    /// <inheritdoc cref="IChangeToken" />
    public class ZookeeperReloadToken : Watcher, IChangeToken, IDisposable
    {
        private readonly ZooKeeper z;
        /// <inheritdoc />
        public ZookeeperReloadToken(ZookeeperOptions options)
        {
            // 初始化Zookeeper 监听=
            z = new ZooKeeper($"{options.Host}:{options.Port}", options.SessionTimeout.Milliseconds,
                this);
        }

        private event Action _fires = () => { };
        /// <inheritdoc />
        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            _fires += () => callback(state);
            return this;
        }

        public bool HasChanged { get; private set; }
        public bool ActiveChangeCallbacks { get; private set; }

        /// <inheritdoc />
        public void Dispose()
        {
            _fires = () => { };
            z.closeAsync().GetAwaiter().GetResult();
        }

        public override Task process(WatchedEvent @event)
        {
            return Task.Factory.StartNew(_fires);
        }
    }

}
