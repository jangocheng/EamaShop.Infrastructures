using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.Serialize
{
    /// <summary>
    /// 为可传输数据提供序列化
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// 转换为String类型的Json数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        string Serialize(object value,Type modelType);
        /// <summary>
        /// 从Json转换为元数据
        /// </summary>
        /// <param name="json"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        object Deserialize(string json, Type modelType);
        /// <summary>
        /// 二进制支持
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        byte[] Serialize(object obj);
        /// <summary>
        /// 二进制反序列化支持
        /// </summary>
        /// <param name="buffers"></param>
        /// <returns></returns>
        object Deserialize(byte[] buffers);
    }
}
