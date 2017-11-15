using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 手机号码字符串
    /// </summary>
    public sealed class PhoneString
    {
        #region CheckRule
        /// <summary>
        /// 手机号码错误的字符串
        /// </summary>
        public static string ErrorMsg { get; set; } = "无效的手机号码 :";
        /// <summary>
        /// 验证手机号码的规则 默认正则
        /// </summary>
        public static Func<string, bool> Rule { get; set; } = (p) => Regex.IsMatch(p, "^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$");
        #endregion
        /// <summary>
        /// 获取一个值，该值指示 手机号码的值
        /// </summary>

        public string PhoneValue { get; }

        /// <summary>
        /// 初始化 <see cref="PhoneString"/> 类型的新实例
        /// </summary>
        /// <param name="phone">手机号码</param>
        ///<exception cref="ArgumentException">手机号码不合法引发的异常</exception>
        public PhoneString(string phone)
        {
            if (!Rule(phone))
            {
                throw new ArgumentException($"{ErrorMsg} {phone}");
            }
            PhoneValue = phone;
        }
        /// <summary>
        /// 隐式转换为 <see cref="string"/>
        /// </summary>
        /// <param name="phone">手机号码</param>
        public static implicit operator string(PhoneString phone)
        {
            return phone?.PhoneValue;
        }
        /// <summary>
        /// 隐式转换为 <see cref="PhoneString"/>
        /// </summary>
        /// <param name="phone"></param>
        public static implicit operator PhoneString(string phone)
        {
            return new PhoneString(phone);
        }
        /// <summary>
        /// 比较俩个手机号码是否一样
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is PhoneString un && PhoneValue.Equals(un)) || (obj is string sv && PhoneValue.Equals(sv));
        }
        /// <summary>
        /// 获取手机号码对象实例的hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return PhoneValue.GetHashCode();
        }
        /// <summary>
        /// 转换为字符串手机号码
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return PhoneValue;
        }
    }
}
