using EamaShop.Infrastructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    /// <summary>
    /// Provides extension method for aes encrpt.
    /// </summary>
    [DebuggerStepThrough]
    public static class SystemExtensions
    {
        /// <summary>
        /// MD5 加密的提供程序
        /// </summary>
        public static readonly MD5 Md5Encryptor = MD5.Create();

        private static string Md5CharDelimited => "-";

        #region AES 对称可逆加密
        /// <summary>
        /// 将当前字符串使用 AES 加密为密文
        /// </summary>
        /// <param name="source">需要加密的字符串</param>
        /// <param name="key">对称加密的密钥</param>
        /// <returns>加密后的密文</returns>
        /// <exception cref="ArgumentOutOfRangeException"> <paramref name="key"/> 的长度应该是32位</exception>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> 或 <paramref name="source"/>不能为 <see langword="null"/></exception>
        public static string AESEncrpty(this string source, string key)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }


            if (key.Length != 32)
            {
                throw new ArgumentOutOfRangeException(nameof(key), "the length of the key used encrpty must be 32");
            }

            using (var provider = Aes.Create())
            {
                provider.Key = Encoding.UTF8.GetBytes(key);
                provider.Mode = CipherMode.ECB;
                provider.Padding = PaddingMode.PKCS7;
                using (var transform = provider.CreateEncryptor())
                {
                    var input = Encoding.UTF8.GetBytes(source);
                    var results = transform.TransformFinalBlock(input, 0, input.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        /// <summary>
        /// 将被 AES 对称可逆加密出的密文字符串解密为明文
        /// </summary>
        /// <param name="secret">需要加密的密文</param>
        /// <param name="key">对称加密的密钥</param>
        /// <returns>解密后的明文</returns>
        /// <exception cref="ArgumentOutOfRangeException"> <paramref name="key"/> 的长度应该是32位</exception>
        /// <exception cref="ArgumentNullException"><paramref name="key"/>或<paramref name="secret"/>不能为 <see langword="null"/> </exception>
        public static string AESDecrpty(this string secret, string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key.Length != 32)
            {
                throw new ArgumentOutOfRangeException(nameof(key), "the length of the key used encrpty must be 32");
            }
            if (secret == null)
            {
                throw new ArgumentNullException(nameof(secret));
            }
            using (var aesProvider = Aes.Create())
            {
                aesProvider.Key = Encoding.UTF8.GetBytes(key);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (var cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(secret);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
            }
        }
        #endregion

        #region MD5
        /// <summary>
        /// 将当前的字符串加密为 md5
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source can not be null</exception>
        public static string Md5(this string source)
        => source == null
            ? throw new ArgumentNullException(nameof(source))
            : BitConverter.ToString(Md5Encryptor.ComputeHash(Encoding.UTF8.GetBytes(source)))
            .Replace(Md5CharDelimited, string.Empty); 
        #endregion

        #region JSON Providers
        /// <summary>
        /// 将当前的对象转换为 JSON 形式的字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>序列化的 JSON 字符串</returns>
        /// <exception cref="ArgumentNullException"> obj 不能为 <see langword="null"/> </exception>
        public static string ToJson(this object obj)
            => obj == null ? null : JsonConvert.SerializeObject(obj);
        /// <summary>
        /// 将当前的 JSON 字符串转换为指定的 .NET 强类型对象
        /// </summary>
        /// <typeparam name="T">要转换的对象的 .NET 强类型</typeparam>
        /// <param name="json">JSON 字符串</param>
        /// <returns>序列化后的强类型对象</returns>
        /// <exception cref="ArgumentNullException"> json 不能为 <see langword="null"/> </exception>
        public static T FromJson<T>(this string json)
        => json == null
            ? throw new ArgumentNullException(nameof(json))
            : JsonConvert.DeserializeObject<T>(json);
        #endregion
    }
}
