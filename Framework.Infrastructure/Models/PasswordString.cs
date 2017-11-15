using Framework.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 密码明文字符串
    /// </summary>
    public sealed class PasswordString
    {
        /// <summary>
        /// 密码只能是6到20位
        /// </summary>
        public static string ErrorMsg { get; set; } = "密码只能是6到20位";
        /// <summary>
        /// 密码验证，如果false 抛出 ArgumentException
        /// </summary>
        public static Func<string, bool> Rule { get; set; } = (p) =>
          {
              if (p.Length < 6 || p.Length > 20)
              {
                  return false;
              }
              return true;
          };
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <param name="password">密码原文内容</param>
        /// <exception cref="ArgumentNullException">密码不能为空</exception>
        /// <exception cref="ArgumentException">密码只能是6到20位</exception>
        public PasswordString(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "密码不能为空");
            }
            if (!Rule(password))
            {
                throw new ArgumentException(ErrorMsg);
            }
            Password = password;
        }
        /// <summary>
        /// 隐式转换为 <see cref="String"/>
        /// </summary>
        /// <param name="password">明文密码对象</param>
        public static implicit operator string(PasswordString password)
        {
            return password?.Password;
        }
        /// <summary>
        /// 隐式转换为密码的明文表达形式
        /// </summary>
        /// <param name="password">明文密码的字符串表达形式</param>
        public static implicit operator PasswordString(string password)
        {
            return new PasswordString(password);
        }
        /// <summary>
        /// 比较俩个明文密码是否相同
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is PasswordString un && Password.Equals(un)) || (obj is string sv && Password.Equals(sv));
        }
        /// <summary>
        /// 获取明文密码的hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Password.GetHashCode();
        }
        /// <summary>
        /// 获取明文密码字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Password;
        }
        /// <summary>
        /// 获取原文密码加密后的密文字符串
        /// </summary>
        /// <param name="salt">盐</param>
        /// <returns>加密后的密码</returns>
        public string ComputedWithSalt(string salt)
        {
            return $"{Password}+{salt}-{Password}".Md5();
        }
        /// <summary>
        /// 当前明文密码是否正确
        /// </summary>
        /// <param name="secret">密文Md5摘要</param>
        /// <param name="salt">盐</param>
        /// <returns>如果密码正确，则返回true，否则返回false</returns>
        public bool EqualsSecret(string secret, string salt)
        {
            return ComputedWithSalt(salt).Equals(secret);
        }
    }
}
