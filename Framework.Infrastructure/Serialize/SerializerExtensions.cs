using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.Serialize
{
    /// <summary>
    /// 序列化扩展
    /// </summary>
    public static class SerializerExtensions
    {
        /// <summary>
        /// 反序列化指定的json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this ISerializer serializer, string json) =>
            (T)serializer?.Deserialize(json, typeof(T));
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializer"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize<T>(this ISerializer serializer, T value) =>
            serializer?.Serialize(value, typeof(T));
    }
}
