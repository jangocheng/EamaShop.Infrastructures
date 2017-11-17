using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.Configuration.Zookeeper
{
    /// <summary>
    /// zookeeper 路径字符串
    /// </summary>
    public sealed class ZookeeperPathString
    {
        /// <summary>
        /// 路径  ==> "/demo/demo/demo"
        /// </summary>
        public string Path { get; }
        /// <summary>
        /// 初始化zookeeper的字符串 默认使用"/"拆分该字符串
        /// </summary>
        /// <param name="path"></param>
        /// <param name="spliter"></param>
        public ZookeeperPathString(string path, string spliter = "/")
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new NotSupportedException("invalid zookeeper path ,only support non-empty");
            }

            var array = path.Split(spliter);//  "/path/  /sdd/  "=>"/path/sdd"
            var sb = new StringBuilder();
            foreach (var a in array)
            {
                if (!string.IsNullOrWhiteSpace(a))
                {
                    sb.Append("/").Append(a);
                }
            }
            if (sb.Length == 0)
            {
                throw new NotSupportedException($"invalid zookeeper path of  {path}");
            }
            Path = sb.ToString();
        }
        /// <summary>
        /// zookeeperstring 2 string
        /// </summary>
        /// <param name="zPath"></param>
        public static implicit operator string(ZookeeperPathString zPath)
        {
            return zPath?.Path;
        }
        /// <summary>
        /// string 2 zookeeperstring
        /// </summary>
        /// <param name="path"></param>
        public static implicit operator ZookeeperPathString(string path)
        {
            return new ZookeeperPathString(path);
        }
        /// <summary>
        /// AspNetCore 的配置读取方式
        /// </summary>
        /// <param name="path"></param>
        /// <param name="spliter"></param>
        /// <returns></returns>
        public static ZookeeperPathString ParseFrom(string path, string spliter = ":")
        {
            return new ZookeeperPathString(path, spliter);
        }
        /// <summary>
        /// 将当前的节点转换为一系列的递进式的节点
        /// Eg.  /App/Configs/Add 
        /// ==>  /App ,/App/Configs , /App/Configs/Add
        /// </summary>
        /// <returns></returns>
        public ZookeeperPathString[] ToSpliter()
        {
            var array = Path.Split("/")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            var result = new List<ZookeeperPathString>();

            ZookeeperPathString iteration = null;

            foreach(var a in array)
            {
                if (iteration == null)
                {
                    iteration = a;
                }
                else
                {
                    iteration += a;
                }
                result.Add(iteration);
            }

            return result.ToArray();
        }
        /// <summary>
        /// 转换为 A:B:C的格式
        /// </summary>
        /// <returns></returns>
        public string ToKeyString()
        {
            var array = Path
                .Split("/")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
            if (array.Length == 1)
            {
                return array[0];
            }
            return string.Join(":", array);
        }

        public static ZookeeperPathString operator +(string a, ZookeeperPathString b)
        {
            if (a == null) return b ?? null;
            if (b == null) return a ?? null;

            var array = a.Split('/')
                .Where(x=>!string.IsNullOrWhiteSpace(x))
                .ToArray();

            return $"/{a}{b}";
        }
        public static ZookeeperPathString operator +(ZookeeperPathString a, ZookeeperPathString b)
        {
            return a.Path + b.Path;
        }
    }
}
