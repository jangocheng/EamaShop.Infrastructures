using EamaShop.Infrastructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System.Extensions
{
    /// <summary>
    /// Provides extension method for aes encrpt.
    /// </summary>
    [DebuggerStepThrough]
    public static class ClrExtensions
    {
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
    }
}
