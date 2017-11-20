using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using org.apache.zookeeper;
using System.Linq;

namespace Microsoft.Extensions.Configuration.Zookeeper
{
    /// <inheritdoc />
    internal class ZookeeperConfigurationProvider : ConfigurationProvider, IConfigurationProvider
    {
        public override void Set(string key, string value)
        {
            if (Source.ReadOnlyConfiguration)
            {
                return;
            }
            // set zookeeper
            var path = ZookeeperPathString.ParseFrom(key);
            if (!path.Path.StartsWith(Source.Path)) path = Source.Path + path;

            //ZooKeeper.Using(Source.ConnectionString, Source.SessionTimeout, null, LoadAsync);
            _client.SetDataAsync(path, Source.DataEncoding.GetBytes(value))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            base.Set(key, value);
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public ZookeeperConfigurationSource Source { get; }
        /// <inheritdoc />
        public ZookeeperConfigurationProvider(ZookeeperConfigurationSource source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrWhiteSpace(source.ConnectionString))
                throw new NotSupportedException("zookeeper 的连接字符串不能是空白的字符串");

            if (source.DataEncoding == null)
            {
                throw new NotSupportedException("zookeeper 的数据传输编码不能为null");
            }

            if (source.Path == null)
            {
                throw new NotSupportedException("未配置zookeeper配置中心的配置节点路径Path");
            }

            if (source.SessionTimeout < 0)
            {
                throw new NotSupportedException("zookeeper 的session超时时间不能小于0");
            }
            var watcher = new ZkConfigWatcher();
            watcher.OnChangedAsync += OnKeyChanged;
            _client = new ZooKeeper(source.ConnectionString, source.SessionTimeout, watcher);
        }
        private readonly ZooKeeper _client;


        /// <inheritdoc />
        public override void Load()
        => LoadAsync(_client).ConfigureAwait(false).GetAwaiter().GetResult();

        private async Task LoadAsync(ZooKeeper keeper)
        {
            await keeper.GetOrAddAsync(Source.Path, new byte[0]);
            await Loop(keeper, Source.Path);
        }
        private async Task Loop(ZooKeeper keeper, ZookeeperPathString path)
        {
            var data = await keeper.getDataAsync(path, true);

            var sValue = Source.DataEncoding.GetString(data.Data);

            Data.TryAdd(path.ToKeyString(), sValue);

            if (data.Stat.getNumChildren() > 0)
            {
                var childs = await keeper.getChildrenAsync(path, true);
                foreach (var c in childs.Children)
                {
                    await Loop(keeper, path + c);
                }
            }
        }
        private async Task OnKeyChanged(WatchedEvent args)
        {
            var eventType = args.get_Type();
            ZookeeperPathString path = args.getPath();
            var key = path.ToKeyString();
            string value;
            DataResult result;
            switch (eventType)
            {
                case Watcher.Event.EventType.None:
                    break;
                case Watcher.Event.EventType.NodeCreated:
                    result = await _client.getDataAsync(path);
                    value = Source.DataEncoding.GetString(result.Data);
                    Data.TryAdd(key, value);
                    break;
                case Watcher.Event.EventType.NodeDeleted:
                    Data.Remove(key);
                    break;
                case Watcher.Event.EventType.NodeDataChanged:
                    result = await _client.getDataAsync(path);
                    value = Source.DataEncoding.GetString(result.Data);
                    Data[key] = value;
                    break;
                // ignore nochild changed every node was be watched
                case Watcher.Event.EventType.NodeChildrenChanged:
                default:
                    break;
            }
        }
    }
}
