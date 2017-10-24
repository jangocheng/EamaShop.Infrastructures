using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Infrastructure.Extensions
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举的 <see cref="DescriptionAttribute"/> 的描述信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum e)
        {
            Check.NotNull(e, nameof(e));
            var typeInfo = e.GetType().GetTypeInfo();
            var description = typeInfo.GetCustomAttribute<DescriptionAttribute>();
            return description?.Description;
        }
    }
}
