using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 用户名字符串 
    /// </summary>
    public sealed class UserNameString
    {
        #region Check Rule
        /// <summary>
        /// 用户名格式不合法的错误字符串
        /// </summary>
        public static string ErrorMsg = "无效的用户名，只能包含数字和字母，且长度为6~18位";
        /// <summary>
        /// 获取或设置用户名检查规则 默认 数字或字母 6~18位
        /// </summary>
        public static Func<string, bool> Rule { get; set; } = (u) =>
        {
            return Regex.IsMatch(u, Pattern);
        };
        private const string Pattern = "'^[a-zA-Z0-9]{6,18}$/";
        #endregion
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; }
        /// <summary>
        /// 使用指定的用户名初始化 <see cref="UserNameString"/> 的新实例
        /// </summary>
        /// <param name="userName"></param>
        public UserNameString(string userName)
        {
            if (Rule(userName))
            {
                throw new ArgumentException(ErrorMsg);
            }

            UserName = userName;
        }
        /// <summary>
        /// 隐式转换为 <see cref="string"/>
        /// </summary>
        /// <param name="userName">用户名</param>
        public static implicit operator string(UserNameString userName)
        {
            return userName?.UserName;
        }
        /// <summary>
        /// 隐式转换为 <see cref="UserNameString"/>
        /// </summary>
        /// <param name="userName"></param>
        public static implicit operator UserNameString(string userName)
        {
            return new UserNameString(userName);
        }
        /// <summary>
        /// 比较俩个用户名是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is UserNameString un && UserName.Equals(un)) || (obj is string sv && UserName.Equals(sv));
        }
        /// <summary>
        /// 获取该用户名实例的hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }
        /// <summary>
        /// 获取用户名的字符串表达形式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return UserName;
        }
    }
}
