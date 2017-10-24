using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// 将指定的字符串经过Md5进行加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Md5(string source)
        {
            Check.NotNull(source, nameof(source));
            var bytes = Encoding.Default.GetBytes(source);
            MD5 md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", "");
        }
    }
}
