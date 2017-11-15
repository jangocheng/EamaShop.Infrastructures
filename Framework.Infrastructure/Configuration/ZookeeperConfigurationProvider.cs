using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using org.apache.zookeeper;

namespace Framework.Infrastructure.Configuration
{
    /// <inheritdoc />
    public class ZookeeperConfigurationProvider : IConfigurationProvider
    {

        private readonly ZookeeperOptions _options;
        private readonly ZooKeeper _keeper;

        /// <inheritdoc />
        public ZookeeperConfigurationProvider(ZookeeperOptions options)
        {
            Checker.NotNull(options, nameof(options));
            _options = options;

        }
        private readonly ConcurrentDictionary<string, string> _temps = new ConcurrentDictionary<string, string>();
        /// <inheritdoc />
        public bool TryGet(string key, out string value)
        {
            return _temps.TryGetValue(key, out value);
        }

        /// <inheritdoc />
        public void Set(string key, string value)
        {
            // set global and reload all
            _temps.AddOrUpdate(key, value, (k, val) => value);
        }

        /// <inheritdoc />
        public IChangeToken GetReloadToken()
        {
            var token = new ZookeeperReloadToken(_options);
            ChangeToken.OnChange(() => token, Load);
            return token;
        }

        /// <inheritdoc />
        public void Load()
        {
            _temps.Clear();
            LoadAsync().GetAwaiter().GetResult();
        }

        private async Task LoadAsync()
        {
            var childrens = await _keeper.getChildrenAsync("/" + _options.RootPath);
            foreach (var c in childrens.Children)
            {
                var data = await _keeper.getDataAsync($"/{_options.RootPath}/{c}");
                _temps.TryAdd(c, Encoding.UTF8.GetString(data.Data));
            }
        }
        /// <inheritdoc />
        public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            throw new NotImplementedException();
        }
    }
}
