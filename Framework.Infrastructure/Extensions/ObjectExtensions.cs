using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Framework.Infrastructure.Extensions
{
    /// <summary>
    /// Object对象扩展
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 将对象转换为Json字符串，其中不包含类型元数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeJson(this object obj) => JsonConvert.SerializeObject(obj);


        private static readonly JsonSerializerSettings Setting = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        /// <summary>
        /// 将对象转换为Json字符串，其中包含类型元数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeJsonWithType<T>(this T obj) => SerializeJsonWithType(obj, typeof(T));
        /// <summary>
        /// 将对象转换为Json字符串，其中包含类型元数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public static string SerializeJsonWithType(this object obj, Type modelType) => JsonConvert.SerializeObject(obj, modelType, Setting);
        /// <summary>
        /// 将json字符串反序列化为对象，如果json为空，则返回null，如果不合法，则抛异常
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object DeserializeJson(this string json) => string.IsNullOrWhiteSpace(json) ? null : JsonConvert.DeserializeObject(json);
        /// <summary>
        /// 将json字符串反序列化为对象，如果json为空，则返回null，如果不合法，则抛异常
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(this string json) where T : class =>
            string.IsNullOrWhiteSpace(json) ? null : JsonConvert.DeserializeObject<T>(json);
        /// <summary>
        /// 将json字符串的所有值填充到指定的对象中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(this string json, T target) where T : class
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            JsonConvert.PopulateObject(json, target);
            return target;
        }
        /// <summary>
        /// 加密指定的字符串到MD5
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Md5(this string source)
        {
            Checker.NotNull(source, nameof(source));
            var bytes = Encoding.Default.GetBytes(source);
            MD5 md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", "");
        }
        /// <summary>
        /// 使用RSA算法解密当前文本
        /// </summary>
        /// <param name="source">需要加密的字符串文本</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static string DeRSA(this string source, string privateKey, string encoding = "utf-8")
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (privateKey == null) throw new ArgumentNullException(nameof(privateKey));

            var rsa = RSAUtil.CreateRsaFromPrivateKey(privateKey);

            var encoder = Encoding.GetEncoding(encoding);

            var data = Convert.FromBase64String(source);

            var result = rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);

            return encoder.GetString(result);
        }
        
        /// <summary>
        /// 使用RSA算法加密当前文本 
        /// </summary>
        /// <param name="source">需要加密的字符串文本</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static string RSA(this string source, string publicKey, string encoding = "utf-8")
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (publicKey == null) throw new ArgumentNullException(nameof(publicKey));

            var rsa = RSAUtil.CreateRsaFromPublicKey(publicKey);

            var encoder = Encoding.GetEncoding(encoding);

            var data = encoder.GetBytes(source);

            var result = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);

            return Convert.ToBase64String(result);

        }
        /// <summary>
        /// 将json字符串反序列化为指定类型的对象，如果json为空，则返回null，如果不合法，则抛异常
        /// </summary>
        /// <param name="json"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public static object DeserializeJsonWithType(this string json, Type modelType) =>
            string.IsNullOrWhiteSpace(json) ? null : JsonConvert.DeserializeObject(json, modelType, Setting);
        /// <summary>
        /// 将json字符串反序列化为指定类型的对象，如果json为空，则返回null，如果不合法，则抛异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeJsonWithType<T>(this string json) => string.IsNullOrWhiteSpace(json) ? default(T) : (T)DeserializeJsonWithType(json, typeof(T));
    }
}
