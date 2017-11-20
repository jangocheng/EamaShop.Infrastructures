using Microsoft.Extensions.Configuration.Zookeeper;
using org.apache.zookeeper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static org.apache.zookeeper.ZooDefs;

namespace org.apache.zookeeper
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ZookeeperExtensions
    {
        /// <summary>
        /// 创建指定的节点
        /// </summary>
        /// <param name="keeper"></param>
        /// <param name="path">路径</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static async Task CreateAsync(this ZooKeeper keeper, ZookeeperPathString path, byte[] data)
        {
            if (keeper == null) throw new ArgumentNullException(nameof(keeper));

            if (path == null) throw new ArgumentNullException(nameof(path));

            if (data == null) throw new ArgumentNullException(nameof(data));
            if (!await keeper.ExistAsync(path))
            {
                await keeper.createAsync(path, data, Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
            }
            //var spliter = path.ToSpliter();

            //foreach (var spl in spliter)
            //{
            //    if (!await keeper.ExistAsync(spl))
            //    {
            //        await keeper.createAsync(path, data, Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
            //    }
            //}
        }
        /// <summary>
        /// 是否存在指定的节点
        /// </summary>
        /// <param name="keeper"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<bool> ExistAsync(this ZooKeeper keeper, ZookeeperPathString path)
        {
            if (keeper == null) throw new ArgumentNullException(nameof(keeper));

            if (path == null) throw new ArgumentNullException(nameof(path));
            return (await keeper.existsAsync(path)) != null;
        }
        /// <summary>
        /// 获取指定节点上的数据，如果不存在，则创建并添加
        /// </summary>
        /// <param name="keeper"></param>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetOrAddAsync(this ZooKeeper keeper, ZookeeperPathString path, byte[] data)
        {
            if (await keeper.ExistAsync(path))
            {
                return (await keeper.getDataAsync(path, true)).Data;
            }
            else
            {
                await keeper.CreateAsync(path, data);
                return data;
            }
        }
        /// <summary>
        /// 获取指定父节点下的子节点
        /// </summary>
        /// <param name="keeper"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<IReadOnlyList<string>> GetChildren(this ZooKeeper keeper, ZookeeperPathString path)
        {
            if (await keeper.ExistAsync(path))
            {

                var chid = await keeper.getChildrenAsync(path, true);

                return chid.Children;
            }
            return Array.Empty<string>();
        }
        /// <summary>
        /// 设置指定的节点为指定值，如果不存在，会创建该节点
        /// </summary>
        /// <param name="keeper"></param>
        /// <param name="path"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static async Task SetDataAsync(this ZooKeeper keeper, ZookeeperPathString path, byte[] values)
        {
            if (await keeper.ExistAsync(path))
            {
                await keeper.setDataAsync(path, values);
            }
            else
            {
                await keeper.CreateAsync(path, values);
            }
        }
    }
}
