using EamaShop.Infrastructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace System.Extensions
{
    /// <summary>
    /// Provides extension method for aes encrpt.
    /// </summary>
    [DebuggerStepThrough]
    public static class ClrExtensions
    {
        private static MD5 Md5Encryptor { get; } = MD5.Create();
        private static string Md5CharDelimited => "-";
        /// <summary>
        /// transform this source string as aes source string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static AesSourceString AsAesSourceString(this string source)
        {
            return source;
        }
        /// <summary>
        /// transform this secret string as aes secret string.
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static AesSecretString AsAesSecretString(this string secret)
        {
            return secret;
        }
        /// <summary>
        /// check the string is a absolute uri string.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsAbsoluteUriString(this string source)
        {
            return Uri.IsWellFormedUriString(source, UriKind.Absolute);
        }
        /// <summary>
        /// Encrypt given source string to a 32-bit md5 string.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Md5(this string source)
        => source == null
            ? throw new ArgumentNullException(nameof(source))
            : BitConverter.ToString(Md5Encryptor.ComputeHash(Encoding.UTF8.GetBytes(source))).Replace(Md5CharDelimited, string.Empty);
    }
}
