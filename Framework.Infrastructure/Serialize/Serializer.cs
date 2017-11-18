using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Framework.Infrastructure.Serialize
{
    /// <inheritdoc />
    /// <summary>
    /// 默认数据序列化器
    /// </summary>
    public class Serializer : ISerializer
    {


        /// <inheritdoc />
        public string Serialize(object value, Type modelType)
        {
            return value?.SerializeJsonWithType(modelType);
        }

        /// <inheritdoc />
        public object Deserialize(string json, Type modelType)
        {
            if (string.IsNullOrWhiteSpace(json)) return null;
            return json.DeserializeJsonWithType(modelType);
        }

        /// <inheritdoc />
        public byte[] Serialize(object obj)
        {
            if (obj == null) return null;
            using (var ms = new MemoryStream())
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        /// <inheritdoc />
        public object Deserialize(byte[] buffers)
        {
            if (buffers == null) return null;
            using (var ms = new MemoryStream(buffers))
            {
                var b = new BinaryFormatter();
                return b.Deserialize(ms);
            }
        }
    }
}
